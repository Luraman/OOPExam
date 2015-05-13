using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPExam.Linesystem
{
  public partial class LineSystem
  {
    class InsertCashTransaction : Transaction
    {
      public InsertCashTransaction(int id, User user, int amount)
        : base(id, user, amount) {}
      public override string ToString()
      {
        return String.Format("{0}: {1} inserted {2} on {3}", ID, User, Amount, Date);
      }
      public override void Execute()
      {
        User.Balance += Amount;
      }
    }
  }
}