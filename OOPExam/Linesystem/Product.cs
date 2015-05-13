using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPExam.Linesystem
{
  public partial class Linesystem
  {
    class Product
    {
      public Product(int id, string name, int price)
      {
        ID = id;
        Name = name;
        Price = price;
        Active = true;
        CanBeBoughtOnCredit = false;
      }
      public int ID
      {
        get { return ID; }
        set
        {
          if (value >= 1) ID = value;
          else throw new ArgumentOutOfRangeException();
        }
      }
      public string Name
      {
        get { return Name; }
        set
        {
          if (!String.IsNullOrWhiteSpace(value)) Name = value;
          else throw new ArgumentException();
        }
      }
      public int Price
      {
        get { return Price; } 
        set
        {
          if (value >= 0) Price = value;
          else throw new ArgumentOutOfRangeException();
        } 
      }
      public virtual bool Active { get; set; }
      public bool CanBeBoughtOnCredit = false;

      public static string ValidateProduct(string name, int price)
      {
        if (String.IsNullOrWhiteSpace(name)) return "Missing name";
        if (price < 0) return "Price can't be negative";
        return null;
      }
    }
  }
}
