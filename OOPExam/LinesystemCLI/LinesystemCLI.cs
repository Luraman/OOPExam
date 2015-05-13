using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOPExam.Linesystem;

namespace OOPExam.LinesystemCLI
{
  class LineSystemCLI : ILinesystemUI
  {
    public LineSystemCLI() {}
    LinesystemCommandParser LCP;
    public void Start()
    {
      bool running = true;
      while (running)
      {
        string input = Console.ReadLine();
        LCP.ParseInput(input);
      }
    }

    public void DisplayError(string error)
    {
      Console.WriteLine(String.Format("Error: {0}", error));
    }


    public void DisplayUserBoughtProduct(string username, string productname, int productid)
    {
      Console.WriteLine(String.Format("{0} successfully bought {1} (id:{2})"), username, productname, productid);
    }


    public void DisplayUserLowBalance(string username, int balance)
    {
      Console.WriteLine(String.Format("Warning: {0} only has {1} credit left on his/her account"), username, balance);
    }

    public void Link(LineSystem LS)
    {
      LCP = new LinesystemCommandParser(LS);
    }
  }
}
