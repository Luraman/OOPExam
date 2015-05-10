using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPExam.Linesystem
{
  public partial class Linesystem
  {
    class InsertCashTransaction : Transaction
    {
      public override string ToString()
      {
        return String.Format("{0}: {1} inserted {2} on {3}", TransactionID, TransactionUser, Amount, Date);
      }
      public override void Execute()
      {
        TransactionUser.Balance += Amount;
      }
    }
  }
}