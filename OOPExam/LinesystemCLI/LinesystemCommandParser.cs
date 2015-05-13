using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OOPExam.Linesystem
{
  public class LinesystemCommandParser
  {
    Linesystem linesystem;

    Regex quickbuy = new Regex(@"^[0-9a-z_]* [0-9]*$");
    public void ParseInput(string command){
      if (quickbuy.IsMatch(command)) {
        string[] processedCommand = command.Split(' ');
        linesystem.BuyProduct(processedCommand[0], int.Parse(processedCommand[1]));
      }
    }
    public void DisplayError(string error)
    {
      throw new NotImplementedException();
    }
  }
}
