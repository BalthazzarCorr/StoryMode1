using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BashSoft
{
	class BashSoftProgram
	{
		static void Main(string[] args)
		{
			//IOManager.TraverseDirectory(@"C:\Users\Balth\Documents\Visual Studio 2017\Projects\BashSoft");


			StudentsRepository.InitilizeData();
			StudentsRepository.GetStudentScoreFromCourse("Ivan","Unity");
		}
	}
}
