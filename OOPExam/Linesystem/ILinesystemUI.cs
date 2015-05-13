using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPExam.Linesystem
{
  public interface ILinesystemUI
  {
    void Start();
    void Link(LineSystem LS);
    void DisplayError(string error);
    void DisplayUserBoughtProduct(string username, string productname, int productid);
    void DisplayUserLowBalance(string username, int balance);
  }
}
