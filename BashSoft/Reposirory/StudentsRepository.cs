using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using BashSoft.Models;

namespace BashSoft
{
   public class StudentsRepository
   {
      private Dictionary<string, Course> courses;
      private Dictionary<string, Student> students;

      private bool isDataInitialized = false;

      private Dictionary<string, Dictionary<string, List<int>>> studentsByCourse;

      private RepositoryFilter filter;
      private RepositorySorter sorter;

      public StudentsRepository(RepositoryFilter filter, RepositorySorter sorter)
      {
         this.filter = filter;
         this.sorter = sorter;
         this.studentsByCourse = new Dictionary<string, Dictionary<string, List<int>>>();

      }

      public void LoadData(string fileName)
      {
         if (this.isDataInitialized)
         {

            throw  new ArgumentException(ExceptionMessages.DataAlreadyInitializedExeption);
            
         }
         this.students = new Dictionary<string, Student>();
         this.courses = new Dictionary<string, Course>();
         OutputWriter.WriteMessage("Reading data...");
         ReadData(fileName);
      }

      public void UnloadData()
      {
         if (!this.isDataInitialized)
         {
           throw new ArgumentException(ExceptionMessages.DataNotInitializedExeptionMessage);
            
         }
         this.students = null;
         this.courses = null;
         this.isDataInitialized = false;
      }

      private void ReadData(string fileName)
      {
         string path = SessionData.currentPath + "\\" + fileName;



         if (File.Exists(path))
         {
            string pattern = @"([A-Z][a-zA-Z#\++]*_[A-Z][a-z]{2}_\d{4})\s+([A-Za-z]+\d{2}_\d{2,4})\s([\s0-9]+)";

            Regex rgx = new Regex(pattern);

            string[] allInputLines = File.ReadAllLines(path);

            for (int line = 0; line < allInputLines.Length; line++)
            {
              // var
               if (!String.IsNullOrEmpty(allInputLines[line]) && rgx.IsMatch(allInputLines[line]))
               {
                  Match currentMatch = rgx.Match(allInputLines[line]);

                  string courseName = currentMatch.Groups[1].Value;
                  string username = currentMatch.Groups[2].Value;


                  string scoresStr = currentMatch.Groups[3].Value;

                  int[] scores = scoresStr.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                     .Select(int.Parse)
                     .ToArray();

                  try
                  {

                     if (scores.Any(x => x > 100 || x < 0))
                     {
                        OutputWriter.DisplayExpetion(ExceptionMessages.InvalidScore);
                     }
                     if (scores.Length > Course.NumberOfTasksOnExam)
                     {
                        OutputWriter.DisplayExpetion(ExceptionMessages.InvalidNumberOfScores);
                        continue;

                     }

                     if (!this.students.ContainsKey(username))
                     {
                        this.students.Add(username, new Student(username));
                     }

                     if (!this.courses.ContainsKey(courseName))
                     {
                        this.courses.Add(courseName, new Course(courseName));
                     }

                     Course course = this.courses[courseName];
                     Student student = this.students[username];

                     student.EnrollInCourse(course);
                     student.SetMarkOnCourse(courseName, scores);

                     course.EnrollStudent(student);
                  }
                  catch (Exception fex)
                  {
                     OutputWriter.DisplayExpetion(fex.Message + $"at line : {line}");
                  }
               }
            }

         }
         isDataInitialized = true;

         OutputWriter.WriteMessageNewLine("Data read!");
      }



      public void FilterAndTake(string courseName, string givenFilter, int? studentToTake = null)
      {
         if (IsQueryForCoursePossible(courseName))
         {
            if (studentToTake == null)
            {
               studentToTake = this.courses[courseName].StudentByName.Count;
            }
            Dictionary<string, double> marks = this.courses[courseName]
               .StudentByName.ToDictionary(x => x.Key, x => x.Value.MarksByCourseName[courseName]);
            this.filter.FilterAndTake(marks,givenFilter,studentToTake.Value);
         }
      }

      public void OrderAndTake(string courseName, string comparison, int? studentToTake = null)
      {
         if (IsQueryForCoursePossible(courseName))
         {
            if (studentToTake == null)
            {
               studentToTake = this.courses[courseName].StudentByName.Count;
            }
            Dictionary<string, double> marks = this.courses[courseName]
               .StudentByName.ToDictionary(x => x.Key, x => x.Value.MarksByCourseName[courseName]);

            this.sorter.OrderAndTake(marks,comparison,studentToTake.Value);
         }
      }

      private bool IsQueryForCoursePossible(string courseName)
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

         if (this.courses.ContainsKey(courseName))
         {
            return true;
         }
         else
         {
            OutputWriter.DisplayExpetion(ExceptionMessages.InexistingStudentInDataBase);
         }
         return false;

      }


      private bool IsQueryForStudentPossiblе(string courseName, string studentUserName)
      {
         if (this.IsQueryForCoursePossible(courseName) && this.courses[courseName].StudentByName.ContainsKey(studentUserName))
         {
            return true;
         }
         else
         {
            OutputWriter.DisplayExpetion(ExceptionMessages.InexistingStudentInDataBase);
         }
         return false;
      }



      public void GetStudentScoreFromCourse(string username, string courseName)
      {
         if (IsQueryForStudentPossiblе(courseName, username))
         {
            OutputWriter.DisplayStudent(new KeyValuePair<string, double>(username, this.courses[courseName].StudentByName[username].MarksByCourseName[courseName]));
         }
      }

      public void GetAllStudentsFromCourse(string courseName)
      {
         if (IsQueryForCoursePossible(courseName))
         {
            OutputWriter.WriteMessageNewLine($"{courseName}");

            foreach (var studentMarksEntry in this.courses[courseName].StudentByName)
            {
               this.GetStudentScoreFromCourse(courseName,studentMarksEntry.Key);
            }
         }
      }
   }
}
