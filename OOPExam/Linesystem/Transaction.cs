using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPExam.Linesystem
{
  class Transaction
  {
    static int IDCount = 0;

    public int TransactionID { get; private set; }
    public User TransactionUser
    {
      get { return TransactionUser; }
      set
      {
        if (value != null) TransactionUser = value;
        else throw new ArgumentException();
      }
    }
  }
}