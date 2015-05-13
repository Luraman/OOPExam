using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPExam.Linesystem
{
  public partial class LineSystem
  {
    class BuyTransaction : Transaction
    {
      public BuyTransaction(int id, User user, Product product)
        : base(id, user, product.Price) {
          Product = product;
      }
      public Product Product;

      public override string ToString()
      {
        return String.Format("{0}: {1} bought \"{2}\" for {3} credits on {4}", ID, User, Product, Amount, Date);
      }
      public override void Execute()
      {
        if (!Product.CanBeBoughtOnCredit && User.Balance < Amount)
          throw new InsufficientCreditsException(User, Product);
        if (!Product.Active) throw new InactiveProductException(Product);
        User.Balance -= Amount;
      }
    }
  }
}