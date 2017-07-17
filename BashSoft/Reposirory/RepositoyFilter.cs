using System;
using System.Collections.Generic;
using System.Linq;


namespace BashSoft
{
	public  class RepositoryFilter
	{
		public  void FilterAndTake(Dictionary<string,double> studentWithMarks,string wantedFilter,int studentToTake)
		{
			if (wantedFilter == "excellent")
			{
				FilterAndTake(studentWithMarks, x => x>=5,studentToTake);
			}
			else if (wantedFilter == "average")
			{
				FilterAndTake(studentWithMarks,x=> x<5 && x>=3.5,studentToTake);
			}
			else if (wantedFilter == "poor")
			{
				FilterAndTake(studentWithMarks,x=> x < 3.5,studentToTake);
			}
			else
			{
				throw  new ArgumentException(ExceptionMessages.InvalidStudentFilter);
			}
		}

		private  void FilterAndTake(Dictionary<string, double> studentWithMarks, Predicate<double> givenFilter,
			int studentToTake)
		{
			int counterForPrinted = 0;

			foreach (var studentMark in studentWithMarks)
			{
				if (counterForPrinted == studentToTake)
				{
					break;
				}
				
				if (givenFilter(studentMark.Value))
				{
					OutputWriter.DisplayStudent(new KeyValuePair<string, double>(studentMark.Key,studentMark.Value));
					counterForPrinted++;
				}
			}
		}

		



	

		
	}
}
