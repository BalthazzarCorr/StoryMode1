using BashSoft.Exceptions;
using BashSoft.IO.Commands;

namespace BashSoft
{
   internal class PrintOrderedStudentsCommand : Command
   {
      public PrintOrderedStudentsCommand(string input, string[] data, Tester judge, StudentsRepository repository, IOManager inputOutputManager) : base(input, data, judge, repository, inputOutputManager)
      {
      }

      public override void Execute()
      {
         if (this.Data.Length != 5)
         {
            throw new InvalidCommandException(this.Input);

         }
         this.DisplayOrder();
      }

      public void DisplayOrder()
      {
         string courseName = this.Data[1];
         string comparison = this.Data[2].ToLower();
         string takeCommand = this.Data[3].ToLower();
         string takeQuantity = this.Data[4].ToLower();
         this.TryParseParametersForOrderAndTake(takeCommand, takeQuantity, courseName, comparison);

      }

      private void TryParseParametersForOrderAndTake(string takeCommand, string takeQuantity,
         string courseName, string comparison)
      {
         if (takeCommand == "take")
         {
            if (takeQuantity == "all")
            {
               this.Repository.OrderAndTake(courseName, comparison);
            }
            else
            {
               int studentsToTake;
               bool hasParsed = int.TryParse(takeQuantity, out studentsToTake);
               if (hasParsed)
               {
                  this.Repository.OrderAndTake(courseName, comparison, studentsToTake);
               }
               else
               {
                  OutputWriter.DisplayExpetion(ExceptionMessages.InvalidTakeQuantityParameter);
               }
            }
         }
         else
         {
            OutputWriter.DisplayExpetion(ExceptionMessages.InvalidTakeCommandParameter);
         }
      }
   }
}
