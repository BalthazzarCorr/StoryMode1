
using BashSoft.Exceptions;

namespace BashSoft
{
	using System;
	using System.Collections.Generic;
	using System.IO;


	public  class IOManager
	{
		public void TraverseDirectory(int depth)
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

		public  void CreateDirectoryInCurrenntFolder(string name)
		{
			string path = SessionData.currentPath + "\\" + name;
			try
			{
				Directory.CreateDirectory(path);
			}
			catch (ArgumentException)
			{
				throw  new InvalidFileNameException();
			}
		}

		public  void ChaneCurrentDirectoryRelative(string relativePath)
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
					throw new InvalidFileNameException();
				}

			}
			else
			{
				string currentPath = SessionData.currentPath;
				currentPath += "\\" + relativePath;
				ChaneCurrentDirectoryAbsolute(currentPath);
			}
		}

		public  void ChaneCurrentDirectoryAbsolute(string absolutePath)
		{
			if (!Directory.Exists(absolutePath))
			{
				throw  new InvalidFileNameException();
				return;
			}
			SessionData.currentPath = absolutePath;
		}
	}
}
