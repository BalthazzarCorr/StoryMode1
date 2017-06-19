using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BashSoft
{
	public static class InputReader
	{
		public static void StartReadingCommands()
		{
			OutputWriter.WriteMessage($"{SessionData.currentPath}>");
			string input = Console.ReadLine();
			input = input.Trim();

			while (input!=endCommand)
			{
				
				CommandInterpreter.InterpredCommand(input);
				OutputWriter.WriteMessage($"{SessionData.currentPath}>");
				input = Console.ReadLine();
				input = input.Trim();
			}
		}


		

		private const string endCommand = "quit";
	}
}
