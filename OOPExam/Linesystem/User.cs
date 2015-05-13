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
    public class User : IEqualityComparer<User>, IComparable<User>
    {
      public User(int id, string firstname, string lastname, string username, string email)
      {
        ID = id;
        Firstname = firstname;
        Lastname = lastname;
        Username = username;
        Email = email;
      }

      static readonly string usernameValidator = @"^[0-9a-zA-Z_]*$";
      static readonly string emailValidator = @"^[0-9a-zA-Z_\x2D.]+@[0-9a-zA-Z][0-9a-zA-Z\x2D]*\x2E[0-9a-zA-Z\x2D]*[0-9a-zA-Z]$";

      public static string ValidateUser(string firstname, string lastname, string username, string email)
      {
        if (String.IsNullOrWhiteSpace(firstname)) return "Missing firstname";
        if (String.IsNullOrWhiteSpace(lastname)) return "Missing lastname";
        if (!Regex.IsMatch(username, usernameValidator)) return "Username contains invalid characters";
        if (!Regex.IsMatch(email, emailValidator)) return "Invalid emailaddress";
        return null;
      }

      public int ID { get; private set; }
      public string Firstname;
      public string Lastname;
      public string Username;
      public string Email;
      public int Balance;

      public override string ToString()
      {
        return String.Join(" ", Firstname, Lastname, Email);
      }

      public bool Equals(User x, User y)
      {
        return x.ID == y.ID;
      }

      public int GetHashCode(User obj)
      {
        return ID;
      }

      public int CompareTo(User user)
      {
        return ID - user.ID;
      }
    }
  }
}
