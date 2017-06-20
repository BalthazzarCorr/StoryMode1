using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BashSoft
{
	public  class SortedRepositrory
	{
		
		public static void OrderAndTake(Dictionary<string, List<int>> wantedData, string comparison, int studentsToTake)
		{
			comparison = comparison.ToLower();

			if (comparison == "ascending")
			{
				PrintStudents(wantedData.OrderBy(x=> x.Value.Sum()).Take(studentsToTake).ToDictionary(pair=> pair.Key,pair=>pair.Value));
			}
			else if (comparison == "descending")
			{
				PrintStudents(wantedData.OrderByDescending(x=>x.Value.Sum()).Take(studentsToTake).ToDictionary(pair=>pair.Key,pair=> pair.Value));
			}
			else
			{
				OutputWriter.DisplayExpetion(ExceptionMessages.InvalidComparisonQuery);
			}

		}

		

		private static void PrintStudents(Dictionary<string, List<int>> studentsSorted)
		{
			foreach (KeyValuePair<string,List<int>> keyValuePair in studentsSorted)
			{
				OutputWriter.DisplayStudent(keyValuePair);	
			}
		}

		//private static Dictionary<string, List<int>> GetSortedStudents(Dictionary<string, List<int>> studentWanted,
		//	int takeCount, Func<KeyValuePair<string, List<int>>, KeyValuePair<string, List<int>>, int> Comparison)
		//{
		//	int valueTaken = 0;
		//	Dictionary<string, List<int>> studentsSorted = new Dictionary<string, List<int>>();
		//	KeyValuePair<string, List<int>> nextInOrder = new KeyValuePair<string, List<int>>();

		//	bool isSorted = false;

		//	while (valueTaken < takeCount)
		//	{
		//		isSorted = true;
		//		foreach (var studentWithScore in studentWanted)
		//		{
		//			if (!String.IsNullOrEmpty(nextInOrder.Key))
		//			{
		//				int comparisonResult = Comparison(studentWithScore, nextInOrder);

		//				if (comparisonResult>= 0 && !studentsSorted.ContainsKey(studentWithScore.Key))
		//				{
		//					nextInOrder = studentWithScore;
		//					isSorted = false;
		//				}
		//			}
		//			else
		//			{
		//				if (!studentsSorted.ContainsKey(studentWithScore.Key))
		//				{
		//					nextInOrder = studentWithScore;
		//					isSorted = false;
		//				}
		//			}
		//		}
		//		if (!isSorted)
		//		{
		//			studentsSorted.Add(nextInOrder.Value);
		//			valueTaken++;
		//			nextInOrder = new KeyValuePair<string, List<int>>();
		//		}
		//	}
		//	return studentsSorted;
		//}

		//private static int CompareInOrder(KeyValuePair<string, List<int>> firstValue,
		//	KeyValuePair<string, List<int>> secondValue)
		//{
		//	int totalOfFirstMark = 0;
		//	foreach (int i in firstValue.Value)
		//	{
		//		totalOfFirstMark = +i;
		//	}

		//	int totalOfSecondMark = 0;

		//	foreach (int i in secondValue.Value)
		//	{
		//		totalOfSecondMark += i;
		//	}
		//	return totalOfSecondMark.CompareTo(totalOfFirstMark);
		//}

		//private static int CompareDescendingOrder(KeyValuePair<string, List<int>> firstValue,
		//	KeyValuePair<string, List<int>> secondValue)
		//{
		//	int totalOfFirstMark = 0;
		//	foreach (int i in firstValue.Value)
		//	{
		//		totalOfFirstMark = +i;
		//	}

		//	int totalOfSecondMark = 0;

		//	foreach (int i in secondValue.Value)
		//	{
		//		totalOfSecondMark += i;
		//	}
		//	return totalOfFirstMark.CompareTo(totalOfSecondMark);
		//}
	}
}
