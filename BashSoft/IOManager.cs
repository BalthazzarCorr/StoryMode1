using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BashSoft
{
	public static class IOManager
	{
		public static void TraverseDirectory(int depth)
		{
			OutputWriter.WriteEmptyLine();
			int initialIdentitation = SessionData.currentPath.Split('\\').Length;
			Queue<string> subFolder = new Queue<string>();
			subFolder.Enqueue(SessionData.currentPath);

			while (subFolder.Count != 0)
			{

				string currentPath = subFolder.Dequeue();
				int indentation = currentPath.Split('\\').Length - initialIdentitation;

				if (depth - indentation < 0)
				{
					break;
				}

				

				try
				{
					foreach (var file in Directory.GetFiles(currentPath))
					{
						int indexOfLastSlash = file.LastIndexOf("\\");
						string fileName = file.Substring(indexOfLastSlash);
						OutputWriter.WriteMessageNewLine(new string('-', indexOfLastSlash) + fileName);
					}

					foreach (var directoryPath in Directory.GetDirectories(currentPath))
					{
						subFolder.Enqueue(directoryPath);
					}

					OutputWriter.WriteMessageNewLine(string.Format("{0}{1}", new string('-', indentation), currentPath));
				}
				catch (UnauthorizedAccessException)
				{
					OutputWriter.DisplayExpetion(ExceptionMessages.UnauthorizedAccessExeptionMessage);
				}
			}

			


			

		}

		public static void CreateDirectoryInCurrenntFolder(string name)
		{
			string path = SessionData.currentPath + "\\" + name;
			try
			{
				Directory.CreateDirectory(path);
			}
			catch (ArgumentException)
			{
				OutputWriter.DisplayExpetion(ExceptionMessages.ForbiddenSymbolsContainedInName);
			}
		}

		public static void ChaneCurrentDirectoryRelative(string relativePath)
		{
			if (relativePath == "..")
			{
				try
				{
					string currentPath = SessionData.currentPath;
					int indexOfLastSlash = currentPath.LastIndexOf("\\");
					string newPath = currentPath.Substring(0, indexOfLastSlash);
					SessionData.currentPath = newPath;
				}
				catch (ArgumentOutOfRangeException)
				{
					OutputWriter.DisplayExpetion(ExceptionMessages.UnabelToGoHigherInPartitionHierarchy);
				}

			}
			else
			{
				string currentPath = SessionData.currentPath;
				currentPath += "\\" + relativePath;
				ChaneCurrentDirectoryAbsolute(currentPath);
			}
		}

		public static void ChaneCurrentDirectoryAbsolute(string absolutePath)
		{
			if (!Directory.Exists(absolutePath))
			{
				OutputWriter.DisplayExpetion(ExceptionMessages.InvalidPath);
				return;
			}
			SessionData.currentPath = absolutePath;
		}
	}
}
