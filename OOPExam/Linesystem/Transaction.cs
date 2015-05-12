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
    abstract class Transaction
    {
      protected Transaction(User transactionuser, DateTimeOffset date, int amount)
      {
        TransactionID = CreateID();
        TransactionUser = transactionuser;
        Date = date;
        Amount = amount;
      }
      private static int IDCount = 0;

      public int TransactionID { get; protected set; }
      public User TransactionUser
      {
        get { return TransactionUser; }
        set
        {
          if (value != null) TransactionUser = value;
          else throw new ArgumentNullException();
        }
      }
      public DateTimeOffset Date
      {
        get { return Date; }
        set
        {
          if (value != null) Date = value;
          else throw new ArgumentNullException();
        }
      }
      public int Amount;

      protected int CreateID()
      {
        return IDCount++;
      }
      public override string ToString()
      {
        return String.Join(" ", TransactionID, Amount, Date);
      }
      public abstract void Execute();
    }
  }
}