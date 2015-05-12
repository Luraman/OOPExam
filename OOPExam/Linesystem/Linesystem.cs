using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPExam.Linesystem
{
  public partial class Linesystem
  {
    public Linesystem() {}

    string ValidateBuyProduct(User user, Product product)
    {
      if (!product.CanBeBoughtOnCredit && user.Balance < product.Price) return "Insufficient balance";
      if (!product.Active) return "Product isn't available";
      return null;
    }

    void BuyProduct(User user, Product product)
    {
      throw new NotImplementedException();
    }
    void AddCreditsToUser(User user, int amount)
    {
      throw new NotImplementedException();
    }
    void ExecuteTransaction(Transaction transaction)
    {
      throw new NotImplementedException();
    }
    Product GetProduct(int productid)
    {
      throw new NotImplementedException();
    }
    User GetUser(string username)
    {
      throw new NotImplementedException();
    }
    List<Transaction> GetUserTransactions(User user)
    {
      throw new NotImplementedException();
    }
    List<Product> GetActiveProducts()
    {
      throw new NotImplementedException();
    }
  }
}
