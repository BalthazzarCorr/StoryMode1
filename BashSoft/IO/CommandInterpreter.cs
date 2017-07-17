using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BashSoft.IO.Commands;

namespace BashSoft
{
   public class CommandInterpreter
   {
      private Tester judge;
      private StudentsRepository repository;
      private IOManager inputOutputManager;

      public CommandInterpreter(Tester judge, StudentsRepository repository, IOManager inputOutputManager)
      {
         this.judge = judge;
         this.repository = repository;
         this.inputOutputManager = inputOutputManager;
      }
      public void InterpredCommand(string input)
      {
         string[] data = input.Split(' ');
         string commandName = data[0];
         try
         {
            Command command = this.ParseCommand(input, commandName, data);
            command.Execute();
         }
         catch (DirectoryNotFoundException dnfe)
         {
            OutputWriter.DisplayExpetion(dnfe.Message);
         }
         catch (ArgumentOutOfRangeException aoore)
         {
            OutputWriter.DisplayExpetion(aoore.Message);
         }
         catch (ArgumentException ae)
         {
            OutputWriter.DisplayExpetion(ae.Message);
         }
         catch (Exception e)
         {
            OutputWriter.DisplayExpetion(e.Message);
         }

      }

      private Command ParseCommand(string input, string command, string[] data)
      {
         switch (command)
         {
            case "open":

               return new OpenFileCommand(input, data, this.judge, this.repository, this.inputOutputManager);

            case "mkdir":

               return new MakeDirectoryCommand(input, data, this.judge, this.repository, this.inputOutputManager);
            case "ls":
               return new TraverseFoldersCommand(input, data, this.judge, this.repository, this.inputOutputManager);
            case "cmp":
               return new CompareFileCommand(input, data, this.judge, this.repository, this.inputOutputManager);
            case "cdrel":
               return new ChangePathRelativelyCommand(input, data, this.judge, this.repository, this.inputOutputManager);
            case "cdabs":
               return new ChangePathAbsoluteCommand(input, data, this.judge, this.repository, this.inputOutputManager);

            case "readdb":
               return new ReadDatabaseCommand(input, data, this.judge, this.repository, this.inputOutputManager);
              
            case "help":
              return new GetHelpCommand(input, data, this.judge, this.repository, this.inputOutputManager);
               
            case "show":
             return   new ShowCourseCommand(input, data, this.judge, this.repository, this.inputOutputManager);
              
            case "filter":
            return new PrintFilteredStudentsCommand(input, data, this.judge, this.repository, this.inputOutputManager);
              
            case "order":
              return new  PrintOrderedStudentsCommand(input, data, this.judge, this.repository, this.inputOutputManager);
            case "dropdb":
            return  new DropDatabaseCommand(input, data, this.judge, this.repository, this.inputOutputManager);
            case "download":
               break;
            case "downloadAsynch":
               break;
            default:
               DisplayInvalidCommandMessage(input);
               break;
         }

      }

      private void TryShowWantedData(string input, string[] data)
      {
         if (data.Length == 2)
         {
            string courseName = data[1];
            this.repository.GetAllStudentsFromCourse(courseName);
         }
         else if (data.Length == 3)
         {
            string courseName = data[1];
            string userName = data[2];

            this.repository.GetStudentScoreFromCourse(courseName, userName);
         }
         else
         {
            DisplayInvalidCommandMessage(input);
         }
      }


      //private void TryOpenFile(string[] data)
      //{
      //   if (data.Length == 2)
      //   {

      //      string fileName = data[1];
      //      Process.Start(SessionData.currentPath + "\\" + fileName);

      //   }
      //}

      private void TryCreateDirectory(string[] data)
      {

         if (data.Length == 2)
         {
            string folderName = data[1];
            this.inputOutputManager.CreateDirectoryInCurrenntFolder(folderName);
         }
      }

      private void TryTraveseFolders(string input, string[] data)
      {
         if (data.Length == 1)
         {
            int depth;
            bool hasParser = int.TryParse(data[0], out depth);
            if (hasParser)
            {
               this.inputOutputManager.TraverseDirectory(depth);
            }
            else
            {
               OutputWriter.DisplayExpetion(ExceptionMessages.UnableToParseNumber);
            }
         }
         else if (data.Length == 2)
         {
            int depth;
            bool hasParesd = int.TryParse(data[1], out depth);

            if (hasParesd)
            {
               this.inputOutputManager.TraverseDirectory(depth);
            }
            else
            {
               OutputWriter.DisplayExpetion(ExceptionMessages.UnableToParseNumber);
            }
         }
      }

      private void TryCompareFiles(string input, string[] data)
      {
         if (data.Length == 3)
         {
            string firstPath = data[1];
            string secondPath = data[2];
            this.judge.CompareContent(firstPath, secondPath);
         }
      }

      private void TryChangePathRelatively(string input, string[] data)
      {
         string realPath = data[1];
         this.inputOutputManager.ChaneCurrentDirectoryRelative(realPath);
      }

      private void TryChangePathAbsolute(string input, string[] data)
      {
         if (data.Length == 2)
         {

            string absPath = data[1]; this.inputOutputManager.ChaneCurrentDirectoryAbsolute(absPath);

         }
         else
         {
            this.DisplayInvalidCommandMessage(input);
         }
      }

      private void TryReadDatabaseFromFile(string input, string[] data)
      {
         string fileName = data[1];
         this.repository.LoadData(fileName);
      }

      private void TryDropDb(string input, string[] data)
      {
         if (data.Length != 1)
         {
            this.DisplayInvalidCommandMessage(input);
            return;
         }
         this.repository.UnloadData();
         OutputWriter.WriteMessageNewLine("Database dropped!");
      }

      private void TryFilterAndTake(string input, string[] data)
      {
         if (data.Length == 5)
         {
            string courseName = data[1];
            string filter = data[2].ToLower();
            string takeCommand = data[3].ToLower();
            string takeQuantity = data[4].ToLower();
            TryParseParametarsForFilterAndTake(takeCommand, takeQuantity, courseName, filter);
         }
      }

      private void TryParseParametarsForFilterAndTake(string takeCommand, string takeQuantity, string courseName, string filter)
      {
         if (takeCommand == "take")
         {
            if (takeQuantity == "all")
            {
               this.repository.FilterAndTake(courseName, filter);
            }
            else
            {
               int studetsToTake;
               bool hasParsed = int.TryParse(takeQuantity, out studetsToTake);

               if (hasParsed)
               {
                  this.repository.FilterAndTake(courseName, filter, studetsToTake);
               }
               else
               {
                  OutputWriter.DisplayExpetion(ExceptionMessages.InvalidTakeQuantityParameter);
               }
            }
         }
         else
         {
            OutputWriter.DisplayExpetion(ExceptionMessages.InvalidTakeQuantityParameter);
         }
      }

      private void TryOrderAndTake(string input, string[] data)
      {
         if (data.Length == 5)
         {
            string courseName = data[1];
            string filter = data[2].ToLower();
            string takeCommand = data[3].ToLower();
            string takeQuantity = data[4].ToLower();
            TryParseParametarsForOrderAndTake(takeCommand, takeQuantity, courseName, filter);
         }
      }

      private void TryParseParametarsForOrderAndTake(string takeCommand, string takeQuantity, string courseName, string filter)
      {
         if (takeCommand == "order")
         {
            if (takeQuantity == "all")
            {
               this.repository.OrderAndTake(courseName, filter);
            }
            else
            {
               int studetsToTake;
               bool hasParsed = int.TryParse(takeQuantity, out studetsToTake);

               if (hasParsed)
               {
                  this.repository.OrderAndTake(courseName, filter, studetsToTake);
               }
               else
               {
                  OutputWriter.DisplayExpetion(ExceptionMessages.InvalidTakeQuantityParameter);
               }
            }
         }
         else
         {
            OutputWriter.DisplayExpetion(ExceptionMessages.InvalidTakeQuantityParameter);
         }
      }

      private void TryGetHelp()
      {

         OutputWriter.WriteMessageNewLine($"{new string('_', 100)}");
         OutputWriter.WriteMessageNewLine(string.Format("|{0, -98}|", "make directory - mkdir: path "));
         OutputWriter.WriteMessageNewLine(string.Format("|{0, -98}|", "traverse directory - ls: depth "));
         OutputWriter.WriteMessageNewLine(string.Format("|{0, -98}|", "comparing files - cmp: path1 path2"));
         OutputWriter.WriteMessageNewLine(string.Format("|{0, -98}|", "change directory - changeDirREl:relative path"));
         OutputWriter.WriteMessageNewLine(string.Format("|{0, -98}|", "change directory - changeDir:absolute path"));
         OutputWriter.WriteMessageNewLine(string.Format("|{0, -98}|", "read students data base - readDb: path"));
         OutputWriter.WriteMessageNewLine(string.Format("|{0, -98}|",
            "filter {courseName} excelent/average/poor  take 2/5/all students - filterExcelent (the output is written on the console)"));
         OutputWriter.WriteMessageNewLine(string.Format("|{0, -98}|",
            "order increasing students - order {courseName} ascending/descending take 20/10/all (the output is written on the console)"));
         OutputWriter.WriteMessageNewLine(string.Format("|{0, -98}|",
            "download file - download: path of file (saved in current directory)"));
         OutputWriter.WriteMessageNewLine(string.Format("|{0, -98}|",
            "download file asynchronously - downloadAsynch: path of file (save in the current directory)"));
         OutputWriter.WriteMessageNewLine(string.Format("|{0, -98}|", "get help – help"));
         OutputWriter.WriteMessageNewLine($"{new string('_', 100)}");
         //OutputWriter.WriteEmptyLine();
      }

      private void DisplayInvalidCommandMessage(string input)
      {
         OutputWriter.DisplayExpetion($"The command '{input}' is invalid");
      }






   }
}
