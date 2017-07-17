using System;
using BashSoft.Exceptions;
using BashSoft.IO.Commands;

namespace BashSoft
{
   internal class TraverseFoldersCommand : Command
   {
      public TraverseFoldersCommand(string input, string[] data, Tester judge, StudentsRepository repository, IOManager inputOutputManager) : base(input, data, judge, repository, inputOutputManager)
      {
      }

      public override void Execute()
      {
         if (this.Data.Length != 2)
         {
            throw new InvalidCommandException(this.Input);
         }
         if (Data.Length == 1)
         {
            int depth;
            bool hasParser = int.TryParse(Data[0], out depth);
            if (hasParser)
            {
               this.InputOutputManager.TraverseDirectory(depth);
            }
            else
            {
               OutputWriter.DisplayExpetion(ExceptionMessages.UnableToParseNumber);
            }
         }
         else if (Data.Length == 2)
         {
            int depth;
            bool hasParesd = int.TryParse(Data[1], out depth);

            if (hasParesd)
            {
               this.InputOutputManager.TraverseDirectory(depth);
            }
            else
            {
               OutputWriter.DisplayExpetion(ExceptionMessages.UnableToParseNumber);
            }
         }
      }
   }
}