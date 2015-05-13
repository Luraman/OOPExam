using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using OOPExam.Linesystem;

namespace OOPExam.LinesystemCLI
{
  public class LinesystemCommandParser : ILinesystemCommandParser
  {
    public LinesystemCommandParser(LineSystem linesystem)
    {
      LS = linesystem;
    }
    public LineSystem LS;

    Regex quickbuy = new Regex(@"^[0-9a-z_]* [0-9]*$");
    public void ParseInput(string input){
      if (quickbuy.IsMatch(input)) {
        string[] processedCommand = input.Split(' ');
        LS.BuyProduct(processedCommand[0], int.Parse(processedCommand[1]));
      }
    }
  }
}
