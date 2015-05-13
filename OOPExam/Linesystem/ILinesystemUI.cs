using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPExam.Linesystem
{
  public interface ILineSystemUI
  {
    void Start();
    void Close();
    void Link(LineSystem LS);
    void DisplayError(string error);
    void DisplayUserBoughtProduct(string username, string productname, int productid);
    void DisplayUserLowBalance(string username, int balance);
    void DisplayUserInfo(string username, string firstname, string lastname, int balance, List<string> latesttransactions);
  }
}
