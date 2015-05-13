﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace OOPExam.Linesystem
{
  public partial class LineSystem
  {
    public LineSystem(ILinesystemUI ui, string catalogfileaddress, int transactioncount, int usercount) {
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
    public LineSystem(ILinesystemUI ui, string catalogfileaddress) : this(ui, catalogfileaddress, 0, 0) { }

    public ILinesystemUI UI;
    System.IO.StreamWriter logfile = new System.IO.StreamWriter(String.Format(@"{0}\logfile.txt", System.Environment.CurrentDirectory));
    int nextTransactionId = 0;
    int nextUserId = 0;
    int nextProductId = 0;

    Dictionary<int, User> Users = new Dictionary<int, User>();
    Dictionary<int, Product> Products = new Dictionary<int, Product>();
    Dictionary<int, Transaction> Transactions = new Dictionary<int, Transaction>();

    string ValidatePurchase(User user, Product product)
    {
      if (!product.CanBeBoughtOnCredit && user.Balance < product.Price) return string.Format("{0} has insufficient balance", user.Username);
      if (!product.Active) return string.Format("Product: \"{0} (id:{1})\" isn't available", product.Name, product.ID);
      return null;
    }
    public void BuyProduct(string username, int productid)
    {
      User user = GetUser(username);
      if (user == null) {
        UI.DisplayError(string.Format("User: \"{0}\" wasn't found", user.Username));
        return;
      }
      Product product = GetProduct(productid);
      if (product == null)
      {
        UI.DisplayError(string.Format("Productid: {0} wasn't found", productid));
        return;
      }
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
      if (user.Balance <= 5000) UI.DisplayUserLowBalance(user.Username, user.Balance);
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
      Products.Concat(addedProducts);
      if (addedNextProductId > nextProductId) nextProductId = addedNextProductId;
      return null;
    }
  }
}
