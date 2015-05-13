using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOPExam.Linesystem;

namespace OOPExam.LinesystemCLI
{
  class LineSystemCLI : ILineSystemUI
  {
    bool running = true;
    public LineSystemCLI() { }
    LinesystemCommandParser LCP;
    public void Start()
    {
      while (running)
      {
        string input = Console.ReadLine();
        if (!LCP.ParseInput(input)) DisplayError("Invalid input");
      }
    }

    public void Close()
    {
      Console.WriteLine("System successfully shutdown - Press any key to exit");
      Console.ReadKey(false);
      running = false;
    }

    public void DisplayError(string error)
    {
      Console.WriteLine(String.Format("Error: {0}", error));
    }


    public void DisplayUserBoughtProduct(string username, string productname, int productid)
    {
      Console.WriteLine(String.Format(@"{0} successfully bought {1} (id:{2})", username, productname, productid));
    }


    public void DisplayUserLowBalance(string username, int balance)
    {
      Console.WriteLine(String.Format("Warning: {0} only has {1} credit left on his/her account", username, balance));
    }

    public void Link(LineSystem LS)
    {
      LCP = new LinesystemCommandParser(LS);
    }


    public void DisplayUserInfo(string username, string firstname, string lastname, int balance, List<string> latesttransactions)
    {
      Console.Write("Username: {0}\nFullname: {1} {2}\nBalance: {3}\nLastest transactions:\n", username, firstname, lastname, balance);
      latesttransactions.ForEach(x => Console.WriteLine(x));
    }

    public void DisplayProducts(List<string> products)
    {
      products.ForEach(x => Console.WriteLine(x));
    }


    public void DisplayAddCredits(string username, int amount)
    {
      Console.WriteLine(String.Format("{0} credits successfully added to {1}", amount, username));
    }

    public void DisplayAddUser(string username)
    {
      Console.WriteLine(String.Format("{0} successfully added", username));
    }

    public void DisplayUpdatedProduct(string productname, int productid)
    {
      Console.WriteLine(String.Format("id {0}: \"{1}\" updated", productid, productname));
    }
  }
}
