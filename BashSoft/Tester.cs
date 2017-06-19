using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BashSoft
{
	public static class Tester
	{
		public static void CompareContent(string userOutputPath, string expectedOutputPath)
		{

			OutputWriter.WriteMessageNewLine("Reading files...");


			try
			{

				string mismatchPath = GetMismatchPath(expectedOutputPath);

				string[] actualOutputLines = File.ReadAllLines(userOutputPath);

				string[] expectedOutputLines = File.ReadAllLines(expectedOutputPath);

				bool hasMismatch;

				string[] mistmatches = GetLinesWithPossibleMismatches(actualOutputLines, expectedOutputLines, out hasMismatch);

				PrintOutput(mistmatches, hasMismatch, mismatchPath);
				OutputWriter.WriteMessageNewLine("Files read!");
			}
			catch (FileNotFoundException)
			{
				OutputWriter.DisplayExpetion(ExceptionMessages.InvalidPath);
			}

		}

		private static void PrintOutput(string[] mismatches, bool hasMismatch, string mismatchPath)
		{
			if (hasMismatch)
			{
				foreach (var line in mismatches)
				{
					OutputWriter.WriteMessageNewLine(line);
				}
				try
				{
					File.WriteAllLines(mismatchPath, mismatches);
				}
				catch (DirectoryNotFoundException)
				{
					OutputWriter.DisplayExpetion(ExceptionMessages.InvalidPath);
				}
				return;
			}
			
				OutputWriter.WriteMessageNewLine("Files are identical. There are no mismatches.");
			

		}

		private static string[] GetLinesWithPossibleMismatches(
			string[] actualOutputLines, string[] expectedOutputLines, out bool hasMismatch)
		{
			hasMismatch = false;

			string output = string.Empty;

			string[] mismatches = new string[actualOutputLines.Length];

			OutputWriter.WriteMessageNewLine("Comparing files...");


			int minOutputLines = actualOutputLines.Length;

			if (actualOutputLines.Length != expectedOutputLines.Length)
			{
				hasMismatch = true;
				minOutputLines = Math.Min(actualOutputLines.Length, expectedOutputLines.Length);
				OutputWriter.DisplayExpetion(ExceptionMessages.ComparisonOfFilesWithDifferentSizes);
			}

			for (int index = 0; index < minOutputLines; index++)
			{
				string actualLine = actualOutputLines[index];
				string expectedLine = expectedOutputLines[index];

				

				if (!actualLine.Equals(expectedLine))
				{
					output = string.Format("Mismatch at line {0} -- expected: \"{1}\", actual :\"{2}\"",index,expectedLine,actualLine);

					output += Environment.NewLine;

					hasMismatch = true;
				}
				else
				{
					output = actualLine;

					output += Environment.NewLine;
				}
				mismatches[index] = output;
			}
			return mismatches;
		}

		private static string GetMismatchPath(string expectedOutputPath)
		{
			int indexOf = expectedOutputPath.LastIndexOf('\\');
			string directoryPath = expectedOutputPath.Substring(0, indexOf);
			string finalPath = directoryPath + @"\Mismatches.txt";
			return finalPath;
		}




	}

}
