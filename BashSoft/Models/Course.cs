﻿using System;
using System.Collections.Generic;
using BashSoft.Exceptions;

namespace BashSoft.Models
{
   public class Course
   {
      private string name;

      private Dictionary<string, Student> studentsByName;

      public const int NumberOfTasksOnExam = 5;
      public const int MaxScoreOnExamTask = 100;

      public Course(string name)
      {
         this.Name = name;
         this.studentsByName = new Dictionary<string, Student>();
      }

      public string Name {
         get { return this.name; }
         private set {
            if (string.IsNullOrEmpty(value))
            {
               throw new InvalidStringException();
            }

            this.name = value;
         }
      }

      public IReadOnlyDictionary<string,Student> StudentByName {
         get { return studentsByName; }
      }

      public void EnrollStudent(Student student)
      {
         if (this.studentsByName.ContainsKey(student.UserName))
         {
            throw new DuplicateEntryInStructureException(student.UserName,this.Name)
            ;
         }
         this.studentsByName.Add(student.UserName, student);
      }

   }
}
