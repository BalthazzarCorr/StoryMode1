using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BashSoft
{
	public static class CommandInterpreter
	{
		public static void InterpredCommand(string input)
		{
			string[] data = input.Split(' ');
			string command = data[0];
			switch (command)
			{
				case "open":
					TryOpenFile(data);
					break;
				case "mkdir":
					TryCreateDirectory(data);
					break;
				case "ls":
					TryTraveseFolders(input,data);
					break;
				case "cmp":
					TryCompareFiles(input, data);
					break;
				case "cdRel":
					TryChangePathRelatively(input, data);
					break;
				case "cdAbs":
					TryChangePathAbsolute(input, data);
					break;
				case "readDb":
					TryReadDatabaseFromFile(input, data);
					break;
				case "help":
					TryGetHelp();
					break;
				case "filter":
					break;
				case "order":
					break;
				case "decoder":
					break;
				case "download":
					break;
				case "downloadAsynch":
					break;
				default:
					DisplayInvalidCommandMessage(input);
					break;
			}
		}


		private static void TryOpenFile(string[] data)
		{
			if (data.Length == 2)
			{

				string fileName = data[1];
				Process.Start(SessionData.currentPath + "\\" + fileName);

			}
		}

		private static void TryCreateDirectory(string[] data)
		{

			if (data.Length == 2)
			{
				string folderName = data[1];
				IOManager.CreateDirectoryInCurrenntFolder(folderName);
			}
		}

		private static void TryTraveseFolders(string input, string[] data)
		{
			if (data.Length == 1)
			{
				int depth;
				bool hasParser = int.TryParse(data[0], out depth);
				if (hasParser)
				{
					IOManager.TraverseDirectory(depth);
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
					IOManager.TraverseDirectory(depth);
				}
				else
				{
					OutputWriter.DisplayExpetion(ExceptionMessages.UnableToParseNumber);
				}
			}
		}

		private static void TryCompareFiles(string input, string[] data)
		{
			if (data.Length == 3)
			{
				string firstPath = data[1];
				string secondPath = data[2];
				Tester.CompareContent(firstPath, secondPath);
			}
		}

		private static void TryChangePathRelatively(string input, string[] data)
		{
			string realPath = data[1];
			IOManager.ChaneCurrentDirectoryRelative(realPath);
		}

		private static void TryChangePathAbsolute(string input, string[] data)
		{
			string absPath = data[1];
			IOManager.ChaneCurrentDirectoryAbsolute(absPath);
		}

		private static void TryReadDatabaseFromFile(string input, string[] data)
		{
			string fileName = data[1];
			StudentsRepository.InitilizeData(fileName);
		}

		private static void TryGetHelp()
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

		private static void DisplayInvalidCommandMessage(string input)
		{
			OutputWriter.DisplayExpetion($"The command '{input}' is invalid");
		}






	}
}
