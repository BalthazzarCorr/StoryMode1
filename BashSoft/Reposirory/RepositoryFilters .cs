using System;
using System.Collections.Generic;
using System.Linq;


namespace BashSoft
{
	public static class RepositoryFilters
	{
		public static void FilterAndTake(Dictionary<string,List<int>> wanterData,string wantedFilter,int studentToTake)
		{
			if (wantedFilter == "excellent")
			{
				FilterAndTake(wanterData,x => x>=5,studentToTake);
			}
			else if (wantedFilter == "average")
			{
				FilterAndTake(wanterData,x=> x<5 && x>=3.5,studentToTake);
			}
			else if (wantedFilter == "poor")
			{
				FilterAndTake(wanterData,x=> x < 3.5,studentToTake);
			}
			else
			{
				OutputWriter.DisplayExpetion(ExceptionMessages.InvalidStudentFilter);
			}
		}

		private static void FilterAndTake(Dictionary<string, List<int>> wanterData, Predicate<double> givenFilter,
			int studentToTake)
		{
			int counterForPrinted = 0;

			foreach (var userName_Points in wanterData)
			{
				if (counterForPrinted == studentToTake)
				{
					break;
				}
				double averageScore = userName_Points.Value.Average();
				double percentageOfFullfilment = averageScore / 100;
				double mark = percentageOfFullfilment * 4 + 2;

				if (givenFilter(mark))
				{
					OutputWriter.DisplayStudent(userName_Points);
					counterForPrinted++;
				}
			}
		}

		



	

		
	}
}
