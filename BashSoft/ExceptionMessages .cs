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
		public const string DataAlreadyInitializedExeption = "Data already initialized";

		public const string DataNotInitializedExeptionMessage =
			"The data structure must be initialized first in order to make any operations with it.";

		public const string InexistingStudentInDataBase = "The user name for the student you are trying to get does not exist!";

		public const string InvalidPath = "The folder/file you are trying to access at the current address, does not exist.";

		public const string UnauthorizedAccessExeptionMessage =
			"The folder/file you are trying to get access needs a higher level of rights than you currently have.";

		public const string ComparisonOfFilesWithDifferentSizes = "Files not of equal size, certain mismatch.";

		public const string ForbiddenSymbolsContainedInName = "The given name contains symbols that are not allowed to be used in names of files and folders.";

		public const string UnabelToGoHigherInPartitionHierarchy = "No more folders my dude";

		public const string UnableToParseNumber = "The sequence you've written is not a valid number.";
	}
}
