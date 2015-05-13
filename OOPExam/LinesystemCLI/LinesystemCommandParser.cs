using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using OOPExam.Linesystem;
namespace OOPExam.LinesystemCLI
{
  public class LinesystemCommandParser
  {
    public LinesystemCommandParser(LineSystem linesystem)
    {
      LS = linesystem;
      adminCommands.Add("q", Tuple.Create<Action, Predicate<string>>(() => LS.Close(),input => true));
      adminCommands.Add("quit", Tuple.Create<Action, Predicate<string>>(() => LS.Close(), input => true));
      adminCommands.Add("activate", Tuple.Create<Action, Predicate<string>>(
        () => LS.SetProductActive(int.Parse(adminInput[1]),true),
        input => adminInputInt.IsMatch(input))
      );
      adminCommands.Add("deactivate", Tuple.Create<Action, Predicate<string>>(
        () => LS.SetProductActive(int.Parse(adminInput[1]), false),
        input => adminInputInt.IsMatch(input))
      );
      adminCommands.Add("crediton", Tuple.Create<Action, Predicate<string>>(
        () => LS.SetProductCredit(int.Parse(adminInput[1]), true),
        input => adminInputInt.IsMatch(input))
      );
      adminCommands.Add("creditoff", Tuple.Create<Action, Predicate<string>>(
        () => LS.SetProductCredit(int.Parse(adminInput[1]), false),
        input => adminInputInt.IsMatch(input))
      );
      adminCommands.Add("addcredit", Tuple.Create<Action, Predicate<string>>(
        () => LS.AddCreditsToUser(adminInput[1], int.Parse(adminInput[2])),
        input => adminInputStrInt.IsMatch(input))
      );
    }
    LineSystem LS;
    string[] adminInput = new string[16];
    Dictionary<string, Tuple<Action, Predicate<string>>> adminCommands = new Dictionary<string, Tuple<Action, Predicate<string>>>();

    readonly static Regex quickbuy = new Regex(@"^[0-9a-z_]* [0-9]*$");
    readonly static Regex userinfo = new Regex(@"^[0-9a-z_]*$");
    readonly static Regex multibuy = new Regex(@"^[0-9a-z_]* [0-9]* [0-9]*$");
    readonly static Regex admin = new Regex(@"^:");
    readonly static Regex adminInputInt = new Regex(@"^[^ ]* [0-9]*$");
    readonly static Regex adminInputStrInt = new Regex(@"^[^ ]* [^ ]* [0-9]*$");
    public bool ParseInput(string input){
      if (admin.IsMatch(input))
      {
        adminInput = input.Split(' ');
        adminInput[0] = adminInput[0].Substring(1);
        if (!adminCommands.ContainsKey(adminInput[0])) return false;
        if (!adminCommands[adminInput[0]].Item2(input)) return false;
        adminCommands[adminInput[0]].Item1();
        return true;
      }
      else if (quickbuy.IsMatch(input))
      {
        string[] processedCommand = input.Split(' ');
        LS.BuyProduct(processedCommand[0], int.Parse(processedCommand[1]));
      }
      else if (userinfo.IsMatch(input))
      {
        LS.GetUserInfo(input);
      }
      else if (multibuy.IsMatch(input))
      {
        string[] processedCommand = input.Split(' ');
        LS.BuyMultipleProduct(processedCommand[0], int.Parse(processedCommand[2]), int.Parse(processedCommand[1]));
      }
      else if (input == "list") ;
      else return false;
      return true;
    }
  }
}
