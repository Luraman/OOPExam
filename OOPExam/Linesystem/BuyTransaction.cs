using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPExam.Linesystem
{
  public partial class Linesystem
  {
    class BuyTransaction : Transaction
    {
      public Product TransactionProduct;

      public override string ToString()
      {
        return String.Format("{0}: {1} bought {2} for {3} on {4}", TransactionID, TransactionUser, TransactionProduct, Amount, Date);
      }
      public override void Execute()
      {
        if (!TransactionProduct.CanBeBoughtOnCredit && TransactionUser.Balance < Amount)
          throw new InsufficientCreditsException(TransactionUser, TransactionProduct);
        if (!TransactionProduct.Active) throw new InactiveProductException(TransactionProduct);
        TransactionUser.Balance -= Amount;
      }
    }
  }
}