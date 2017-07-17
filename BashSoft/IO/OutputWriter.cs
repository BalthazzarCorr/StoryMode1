using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BashSoft
{
	public static class OutputWriter
	{
		public static void WriteMessage(string message)
		{
			Console.Write($"{message}");
		}


		public static void WriteMessageNewLine(string message)
		{
			Console.WriteLine($"{message}");
		}

		public static void WriteEmptyLine()
		{
			Console.WriteLine();
		}

		public static void DisplayExpetion(string message)
		{
			ConsoleColor currentColor = Console.ForegroundColor;

			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine(message);

			Console.ForegroundColor = currentColor;
		}

		public static void DisplayStudent(KeyValuePair<string, double> student)
		{
			OutputWriter.WriteMessageNewLine($"{student.Key} - {student.Value}");
		}
	}
}
