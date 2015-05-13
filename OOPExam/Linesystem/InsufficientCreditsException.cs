using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPExam.Linesystem
{
  public partial class LineSystem
  {
    class InsufficientCreditsException : Exception
    {
      public InsufficientCreditsException(User transactionuser, Product transactionproduct)
        : base("An user bought a product while having insufficient balance.")
      {
        Data.Add("TransactionUser", transactionuser);
        Data.Add("TransactionProduct", transactionproduct);
      }
    }
  }
}