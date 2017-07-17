using BashSoft.Exceptions;
using BashSoft.IO.Commands;

namespace BashSoft
{
   internal class ChangePathAbsoluteCommand : Command
   {
      public ChangePathAbsoluteCommand(string input, string[] data, Tester judge, StudentsRepository repository, IOManager inputOutputManager) : base(input, data, judge, repository, inputOutputManager)
      {
      }

      public override void Execute()
      {
         if (this.Data.Length != 2 )
         {
            throw new InvalidCommandException(this.Input);
         }
         string absolutePath = this.Data[1];
         this.InputOutputManager.ChaneCurrentDirectoryAbsolute(absolutePath);
      }
   }
}