using BashSoft.Exceptions;
using BashSoft.IO.Commands;

namespace BashSoft
{
   internal class ShowCourseCommand : Command
   {
      public ShowCourseCommand(string input, string[] data, Tester judge, StudentsRepository repository, IOManager inputOutputManager) : base(input, data, judge, repository, inputOutputManager)
      {
      }

      public override void Execute()
      {
         if (this.Data.Length == 2)
         {
            string courseName = this.Data[1];
            this.Repository.GetAllStudentsFromCourse(courseName);
         }
         else if (this.Data.Length == 3)
         {
            string courseName = this.Data[1];
            string userName = this.Data[2];
            this.Repository.GetStudentScoreFromCourse(courseName, userName);
         }
         else
         {
            throw new InvalidCommandException(this.Input);
         }
      }
   }
}