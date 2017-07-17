using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BashSoft.Exceptions
{
   public class InvalidCommandException : Exception
   {

      public InvalidCommandException(string message)
         :base(message)
      {
         DisplayInvalidCommandMessage(message);
      }


      

      public void DisplayInvalidCommandMessage(string input)
      {
         OutputWriter.DisplayExpetion($"The command '{input}' is invalid ");
      }

   }
}
