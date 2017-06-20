﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace BashSoft
{
	public static class StudentsRepository
	{
		public static bool isDataInitialized = false;
		private static Dictionary<string, Dictionary<string, List<int>>> studentsByCourse;

		public static void InitilizeData(string fileName)
		{
			if (!isDataInitialized)
			{
				OutputWriter.WriteMessage("Reading data...");			
				studentsByCourse = new Dictionary<string, Dictionary<string, List<int>>>();
				ReadData(fileName);

			}
			else
			{
				OutputWriter.WriteMessageNewLine(ExceptionMessages.DataAlreadyInitializedExeption);
			}
		}

		private static void ReadData(string fileName)
		{
			string path = SessionData.currentPath + "\\" + fileName;



			if (File.Exists(path))
			{
				string pattern = @"([A-Z][a-zA-Z#+]*_[A-Z][a-z]{2}_\d{4})\s+([A-Z][a-z]{0,3}\d{2}_\d{2,4})\s+(\d+)";

				Regex rgx = new Regex(pattern);

				string[] allInputLines = File.ReadAllLines(path);

				for (int line = 0; line < allInputLines.Length; line++)
				{

					if (!String.IsNullOrEmpty(allInputLines[line]) && rgx.IsMatch(allInputLines[line]))
					{
						Match currentMatch = rgx.Match(allInputLines[line]);

						string courseName = currentMatch.Groups[1].Value;
						string username = currentMatch.Groups[2].Value;
						int studentScoreOnTask;
						bool hasParsedScore = Int32.TryParse(currentMatch.Groups[3].Value, out studentScoreOnTask);

						if (hasParsedScore && studentScoreOnTask >= 0 && studentScoreOnTask <= 100)
						{
							if (!studentsByCourse.ContainsKey(courseName))
							{
								studentsByCourse.Add(courseName, new Dictionary<string, List<int>>());
							}
							if (!studentsByCourse[courseName].ContainsKey(username))
							{
								studentsByCourse[courseName].Add(username, new List<int>());
							}
							studentsByCourse[courseName][username].Add(studentScoreOnTask);
						}
					}
				}

				
			}

			

			

			isDataInitialized = true;

			OutputWriter.WriteMessageNewLine("Data read!");
		}

		public static void FilterAndTake(string courseName, string givenFilter, int? studentToTake = null)
		{
			if (StudentsRepository.IsQueryForCoursePossible(courseName))
			{
				if (studentToTake == null)
				{
					studentToTake = studentsByCourse[courseName].Count;
				}
				RepositoryFilters.FilterAndTake(studentsByCourse[courseName],givenFilter,studentToTake.Value);
			}
		}

		public static void OrderAndTake(string courseName,string comparison, int? studentToTake = null)
		{
			if (IsQueryForCoursePossible(courseName))
			{
				if (studentToTake == null)
				{
					studentToTake = studentsByCourse[courseName].Count;
				}
				SortedRepositrory.OrderAndTake(studentsByCourse[courseName],comparison,studentToTake.Value);
			}
		}

		private static bool IsQueryForCoursePossible(string courseName)
		{
			if (isDataInitialized)
			{
				return true;
			}
			else
			{
				OutputWriter.DisplayExpetion(ExceptionMessages.DataNotInitializedExeptionMessage);
			}
			//return false;

			if (studentsByCourse.ContainsKey(courseName))
			{
				return true;
			}
			else
			{
				OutputWriter.DisplayExpetion(ExceptionMessages.InexistingStudentInDataBase);
			}
			return false;

		}


		private static bool IsQueryForStudentPossiblе(string courseName,string studentUserName)
		{
			if (IsQueryForCoursePossible(courseName) && studentsByCourse[courseName].ContainsKey(studentUserName))
			{
				return true;
			}
			else
			{
				OutputWriter.DisplayExpetion(ExceptionMessages.InexistingStudentInDataBase);
			}
			return false;
		}

		

		public static void GetStudentScoreFromCourse(string username, string courseName )
		{
			if (IsQueryForStudentPossiblе(courseName,username))
			{
			OutputWriter.DisplayStudent(new KeyValuePair<string, List<int>>(username,studentsByCourse[courseName][username]));	
			}
		}

		public static void GetAllStudentsFromCourse(string courseName)
		{
			if (IsQueryForCoursePossible(courseName))
			{
				OutputWriter.WriteMessageNewLine($"{courseName}");

				foreach (var studentMarksEntry in studentsByCourse[courseName])
				{
					OutputWriter.DisplayStudent(studentMarksEntry);
				}
			}
		}
	}
}
