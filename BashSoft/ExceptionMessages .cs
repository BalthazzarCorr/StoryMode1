using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BashSoft
{
	public static class ExceptionMessages
	{
		public const string ExampleExceptionMessage = "Example message";
		public const string DataAlreadyInitializedExeption = "Data already initalized";

		public const string DataNotInitializedExeptionMessage =
			"The data structure must be initialised first in order to make any operations with it.";

		public const string InexistingStudentInDataBase = "The user name for the student you are trying to get does not exist!";
	}
}
