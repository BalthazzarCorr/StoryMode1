using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
namespace BashSoft
{
	public static class StudentsRepository
	{
		public static bool isDataInitialized = false;
		private static Dictionary<string, Dictionary<string, List<int>>> studentsByCourse;

		public static void InitilizeData()
		{
			if (!isDataInitialized)
			{
				OutputWriter.WriteMessage("Reading data...");			
				studentsByCourse = new Dictionary<string, Dictionary<string, List<int>>>();
				ReadData();

			}
			else
			{
				OutputWriter.WriteMessageNewLine(ExceptionMessages.DataAlreadyInitializedExeption);
			}
		}

		private static void ReadData()
		{
			string input = Console.ReadLine();

			while (!string.IsNullOrEmpty(input))
			{
				string[] tokens = input.Split(' ');

				string course = tokens[0];

				string studen = tokens[1];

				int mark = int.Parse(tokens[2]);

				if (!studentsByCourse.ContainsKey(course))
				{
					studentsByCourse.Add(course,new Dictionary<string, List<int>>());
					
				}
				if (!studentsByCourse[course].ContainsKey(studen))
				{
					studentsByCourse[course].Add(studen,new List<int>());
				}

				studentsByCourse[course][studen].Add(mark);

				input= Console.ReadLine();
			}

			isDataInitialized = true;

			OutputWriter.WriteMessageNewLine("Data read!");
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
