﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPExam.Linesystem
{
  public partial class LineSystem
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
      public int ID;
      public string Name;
      public int Price;
      public virtual bool Active { get; set; }
      public bool CanBeBoughtOnCredit = false;

      public static string ValidateProduct(string name, int price)
      {
        if (String.IsNullOrWhiteSpace(name)) return "Missing name";
        if (price < 0) return "Price can't be negative";
        return null;
      }
      public override string ToString()
      {
        return String.Format("{0}: {1} | Price: {2} credits", ID, Name, Price);
      }
    }
  }
}
