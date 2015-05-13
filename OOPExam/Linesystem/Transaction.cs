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
    abstract class Transaction
    {
      protected Transaction(int id, User user, int amount)
      {
        ID = id;
        User = user;
        Date = DateTimeOffset.UtcNow;
        Amount = amount;
      }

      public int ID { get; protected set; }
      public User User
      {
        get { return User; }
        set
        {
          if (value != null) User = value;
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

      public override string ToString()
      {
        return String.Join(" ", ID, Amount, Date);
      }
      public abstract void Execute();
    }
  }
}