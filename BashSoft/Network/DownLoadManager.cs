using System.Threading.Tasks;
using System.Net;
using BashSoft.Exceptions;


namespace BashSoft.Network
{
    class DownLoadManager
   {
      public static class DownloadManager
      {
         public static void Download(string fileUrl)
         {
            WebClient webClient = new WebClient();
            try
            {
               OutputWriter.WriteMessageNewLine("Started downloading: ");

               string nameOfFile = ExtractNameOfFile(fileUrl);
               string downloadPath = SessionData.currentPath + "/" + nameOfFile;

               webClient.DownloadFile(fileUrl, downloadPath);
               OutputWriter.WriteMessageNewLine("Download complete");
            }
            catch (WebException ex)
            {

               OutputWriter.DisplayExpetion(ex.Message);
            }
         }

         public static void DownloadAsync(string fileUrl)
         {
            Task.Run(() => Download(fileUrl));
         }

         private static string ExtractNameOfFile(string fileUrl)
         {
            int indexOfLastSlash = fileUrl.LastIndexOf(@"/");
            if (indexOfLastSlash != -1)
            {
               return fileUrl.Substring(indexOfLastSlash + 1);
            }
            else
            {
               throw new InvalidPathException();
            }
         }
      }
   }
}
