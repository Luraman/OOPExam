using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace OOPExam.Linesystem
{
  public partial class LineSystem
  {
    public LineSystem(ILineSystemUI ui, string catalogfileaddress, int transactioncount, int usercount) {
      UI = ui;
      UI.Link(this);
      if (catalogfileaddress != null)
      {
        string result = ImportProducts(catalogfileaddress);
        if (result != null) UI.DisplayError(result);
      }
      nextTransactionId = transactioncount;
      nextUserId = usercount;
    }
    public LineSystem(ILineSystemUI ui, string catalogfileaddress) : this(ui, catalogfileaddress, 0, 0) { }

    public ILineSystemUI UI;
    System.IO.StreamWriter logfile = new System.IO.StreamWriter(String.Format(@"{0}\logfile.txt", System.Environment.CurrentDirectory));
    int nextTransactionId = 0;
    int nextUserId = 0;
    int nextProductId = 1;

    Dictionary<int, User> Users = new Dictionary<int, User>();
    Dictionary<int, Product> Products = new Dictionary<int, Product>();
    Dictionary<int, Transaction> Transactions = new Dictionary<int, Transaction>();

    string ValidatePurchase(User user, Product product)
    {
      if (!product.CanBeBoughtOnCredit && user.Balance < product.Price) return string.Format("{0} has insufficient balance. ({1} credits required)", user.Username, product.Price);
      if (!product.Active) return string.Format("Product: \"{0} (id:{1})\" isn't available", product.Name, product.ID);
      return null;
    }
    public void BuyProduct(string username, int productid)
    {
      User user = GetUser(username);
      if (user == null) return;
      Product product = GetProduct(productid);
      if (product == null) return;
      string validation = ValidatePurchase(user, product);
      if (validation != null)
      {
        UI.DisplayError(validation);
        return;
      }
      BuyProduct(user, product);
    }
    void BuyProduct(User user, Product product)
    {
      ExecuteTransaction(new BuyTransaction(nextTransactionId++, user, product));
      UI.DisplayUserBoughtProduct(user.Username, product.Name, product.ID);
      CheckForLowBalance(user);
    }
    void CheckForLowBalance(User user)
    {
      if (user.Balance <= 5000) UI.DisplayUserLowBalance(user.Username, user.Balance);
    }
    public void AddCreditsToUser(string username, int amount)
    {
      User user = GetUser(username);
      if (user == null) return;
      AddCreditsToUser(user, amount);
    }
    void AddCreditsToUser(User user, int amount)
    {
      ExecuteTransaction(new InsertCashTransaction(nextTransactionId++, user, amount));
      UI.DisplayAddCredits(user.Username, amount);
    }
    void ExecuteTransaction(Transaction transaction)
    {
      transaction.Execute();
      Transactions.Add(transaction.ID, transaction);
      logfile.WriteLine(transaction);
    }
    Product GetProduct(int id)
    {
      Product product = Products.Select(kvp => kvp.Value).FirstOrDefault(iproduct => iproduct.ID == id);
      if (product == null) UI.DisplayError(string.Format("Productid: {0} wasn't found", id));
      return product;
    }
    public void AddUser(string firstname, string lastname, string username, string email)
    {
      string validation = User.ValidateUser(firstname, lastname, username, email);
      if (validation != null)
      {
        UI.DisplayError(validation);
        return;
      }
      Users.Add(nextUserId, new User(nextUserId++, firstname, lastname, username, email));
      UI.DisplayAddUser(username);
    }
    User GetUser(string username)
    {
      User user = Users.Select(kvp => kvp.Value).FirstOrDefault(iuser => iuser.Username == username);
      if (user == null) UI.DisplayError(string.Format("User: \"{0}\" wasn't found", username));
      return user;
    }
    List<Transaction> GetUserTransactions(User user)
    {
      return Transactions.Select(kvp => kvp.Value).Where(transaction => transaction.User == user).ToList();
    }
    public void OutputActiveProducts()
    {
      UI.DisplayProducts(GetActiveProducts().Select(product => product.ToString()).ToList());
    }
    List<Product> GetActiveProducts()
    {
      return Products.Select(kvp => kvp.Value).Where(product => product.Active).ToList();
    }
    string ValidateProduct(int id, string name, int price)
    {
      if (id < nextProductId && Products.ContainsKey(id)) return "Id already in use";
      return Product.ValidateProduct(name, price);
    }
    public void AddProduct(int id, string name, int price) {
      string validation = ValidateProduct(id, name, price);
      if (validation != null)
      {
        UI.DisplayError(validation);
        return;
      }
      Products.Add(id, new Product(id, name, price));
      if (id >= nextProductId) nextProductId = id + 1;
    }
    public void AddProduct(string name, int price)
    {
      AddProduct(nextProductId, name, price);
    }
    string ImportProducts(string fileaddress)
    {
      var tagRemover = new Regex("<.*?>");
      var addedProducts = new Dictionary<int, Product>();
      int addedNextProductId = 0;

      using (var catalog = new System.IO.StreamReader(fileaddress, true))
      {
        catalog.ReadLine();
        string rawData = catalog.ReadLine();
        while (rawData != null)
        {
          string[] processedData = tagRemover.Replace(rawData, "").Split(';');
          string validation = ValidateProduct(int.Parse(processedData[0]), processedData[1], int.Parse(processedData[2]));
          if (validation != null) return String.Format("Importing productcatalog failed. Product with id {0}: {1}", processedData[0], validation);
          addedProducts.Add(int.Parse(processedData[0]), new Product(int.Parse(processedData[0]), processedData[1], int.Parse(processedData[2])));
          if (int.Parse(processedData[0]) >= addedNextProductId) addedNextProductId = int.Parse(processedData[0]) + 1;
          rawData = catalog.ReadLine();
        }
      }
      Products = Products.Concat(addedProducts).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
      if (addedNextProductId > nextProductId) nextProductId = addedNextProductId;
      return null;
    }
    public void GetUserInfo(string username)
    {
      User user = GetUser(username);
      if (user == null) return;
      UI.DisplayUserInfo(user.Username, user.Firstname, user.Lastname, user.Balance,
        Transactions.Select(kvp => kvp.Value)
        .Where(transaction => transaction.User == user)
        .Take(10)
        .Select(transaction => transaction.ToString())
        .ToList()
        );
      CheckForLowBalance(user);
    }
    public void BuyMultipleProduct(string username, int productid, int multiple)
    {
      User user = GetUser(username);
      if (user == null) return;
      Product product = GetProduct(productid);
      if (product == null) return;
      if (user.Balance < product.Price * multiple)
      {
        UI.DisplayError(string.Format("{0} has insufficient balance ({1} credits required)", user.Username, product.Price * multiple));
        return;
      }
      for (int transactionCount = 0; transactionCount < multiple; transactionCount++) BuyProduct(user, product);
    }
    public void Close()
    {
      logfile.Close();
      UI.Close();
    }
    public void SetProductActive(int productid, bool active)
    {
      Product product = GetProduct(productid);
      if (product == null) return;
      product.Active = active;
      UI.DisplayUpdatedProduct(product.Name, product.ID);
    }
    public void SetProductCredit(int productid, bool active)
    {
      Product product = GetProduct(productid);
      if (product == null) return;
      product.CanBeBoughtOnCredit = active;
      UI.DisplayUpdatedProduct(product.Name, product.ID);
    }
  }
}
