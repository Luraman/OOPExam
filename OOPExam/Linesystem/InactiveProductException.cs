using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPExam.Linesystem
{
  public partial class LineSystem
  {
    class InactiveProductException : Exception
    {
      public InactiveProductException(Product transactionproduct)
        : base("An inactive product was bought")
      {
        Data.Add("TransactionProduct", transactionproduct);
      }
    }
  }
}
