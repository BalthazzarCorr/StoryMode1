using BashSoft.Exceptions;
using BashSoft.IO.Commands;
using BashSoft.Network;

namespace BashSoft
{
   internal class DownloadRequestCommand : Command
   {
      public DownloadRequestCommand(string input, string[] data, Tester judge, StudentsRepository repository, IOManager inputOutputManager) : base(input, data, judge, repository, inputOutputManager)
      {
      }

      public override void Execute()
      {
         if (this.Data.Length != 2)
         {
            throw new InvalidCommandException(this.Input);

         }
         string url = this.Data[1];
         DownLoadManager.DownloadManager.Download(url);
      }
   }
}