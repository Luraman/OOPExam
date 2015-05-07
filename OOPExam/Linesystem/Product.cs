﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPExam.Linesystem
{
  class Product
  {
    public int ProductID
    {
      get { return ProductID; }
      set
      {
        if (value >= 1) ProductID = value;
        else throw new ArgumentOutOfRangeException();
      }
    }
    public string ProductName
    {
      get { return ProductName; }
      set
      {
        if (!String.IsNullOrWhiteSpace(value)) ProductName = value;
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
    public bool Active = true;
    public bool CanBeBoughtOnCredit = false;

  }
}
