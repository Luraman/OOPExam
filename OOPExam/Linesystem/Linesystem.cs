using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace OOPExam.Linesystem
{
  public partial class Linesystem
  {
    public Linesystem(int transactioncount, int usercount) {
      nextTransactionId = transactioncount;
      nextUserId = usercount;
    }
    public Linesystem() : this(0, 0) {}

    System.IO.StreamWriter logfile = new System.IO.StreamWriter(@"Data\logfile.txt", true);
    int nextTransactionId = 0;
    int nextUserId = 0;
    int nextProductId = 0;

    Dictionary<int, User> Users = new Dictionary<int, User>();
    Dictionary<int, Product> Products = new Dictionary<int, Product>();
    Dictionary<int, Transaction> Transactions = new Dictionary<int, Transaction>();

    string ValidatePurchase(User user, Product product)
    {
      if (!product.CanBeBoughtOnCredit && user.Balance < product.Price) return "Insufficient balance";
      if (!product.Active) return "Product isn't available";
      return null;
    }

    void BuyProduct(User user, Product product)
    {
      ExecuteTransaction(new BuyTransaction(nextTransactionId++, user, product));
    }
    void AddCreditsToUser(User user, int amount)
    {
      ExecuteTransaction(new InsertCashTransaction(nextTransactionId++, user, amount));
    }
    void ExecuteTransaction(Transaction transaction)
    {
      transaction.Execute();
      Transactions.Add(transaction.ID, transaction);
      logfile.WriteLine(transaction);
    }
    Product GetProduct(int id)
    {
      return Products.ElementAtOrDefault(id).Value;
    }
    User GetUser(string username)
    {
      return Users.Select(x => x.Value).FirstOrDefault(x => x.Username == username);
    }
    List<Transaction> GetUserTransactions(User user)
    {
      return Transactions.Select(x => x.Value).Where(x => x.User == user).ToList();
    }
    List<Product> GetActiveProducts()
    {
      return Products.Select(x => x.Value).Where(x => x.Active).ToList();
    }
    string ValidateProduct(int id, string name, int price)
    {
      if (id < nextProductId && Products.ContainsKey(id)) return "Id already in use";
      return Product.ValidateProduct(name, price);
    }
    void AddProduct(int id, string name, int price) {
      Products.Add(id, new Product(id, name, price));
      nextProductId = id + 1;
    }
    void AddProduct(string name, int price)
    {
      AddProduct(nextProductId, name, price);
    }
    string ImportProducts(string fileaddress)
    {
      var tagRemover = new Regex("<.*>");
      var splitter = new Regex(";");
      var addedProducts = new Dictionary<int, Product>();

      using (var catalog = new System.IO.StreamReader(fileaddress, true))
      {
        catalog.ReadLine();
        string rawData = catalog.ReadLine();
        while (rawData != null)
        {
          string[] processedData = splitter.Split(tagRemover.Replace(rawData, ""));
          string result = ValidateProduct(int.Parse(processedData[0]), processedData[1], int.Parse(processedData[2]));
          if (result != null) return String.Format("Product with id {0}: {1}", processedData[0], result);
          addedProducts.Add(int.Parse(processedData[0]), new Product(int.Parse(processedData[0]), processedData[1], int.Parse(processedData[2])));
          rawData = catalog.ReadLine();
        }
      }
      Products.Concat(addedProducts);
      return null;
    }
  }
}
