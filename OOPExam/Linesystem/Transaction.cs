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
      public User User;
      public DateTimeOffset Date;
      public int Amount;

      public override string ToString()
      {
        return String.Join(" ", ID, Amount, Date);
      }
      public abstract void Execute();
    }
  }
}