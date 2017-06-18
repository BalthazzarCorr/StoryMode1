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
		public static void TraverseDirectory(string path)
		{
			OutputWriter.WriteEmptyLine();
			int initialIdentitation = path.Split('\\').Length;
			Queue<string> subFolder = new Queue<string>();
			subFolder.Enqueue(path);

			while (subFolder.Count != 0)
			{

				string currentPath = subFolder.Dequeue();
				int indentation = currentPath.Split('\\').Length - initialIdentitation;

				

				foreach (var directoryPath in Directory.GetDirectories(currentPath))
				{
					subFolder.Enqueue(directoryPath);
				}

				OutputWriter.WriteMessageNewLine(string.Format("{0}{1}", new string('-', indentation), currentPath));

			}

			


			

		}


	}
}
