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
    class User : IEqualityComparer<User>, IComparable<User>
    {
      public User(string firstname, string lastname, string username, string email)
      {
        UserID = userCount++;
        Firstname = firstname;
        Lastname = lastname;
        Username = username;
        Email = email;
      }

      static int userCount = 0;

      public static string ValidateNewUser(string firstname, string lastname, string username, string email)
      {
        if (String.IsNullOrWhiteSpace(firstname)) return "No firstname written";
        if (String.IsNullOrWhiteSpace(lastname)) return "No lastname written";
        if (!Regex.IsMatch(username, "^[0-9a-z_]*$")) return "Username contains invalid characters";
        if (!Regex.IsMatch(email, "^[0-9a-z_-.]+@[0-9a-z][0-9a-z\x2D]*.[0-9a-z\x2D]*[0-9a-z]$")) return "Invalid emailaddress";
        return null;
      }

      public int UserID { get; private set; }
      public string Firstname { get; private set; }
      public string Lastname { get; private set; }
      public string Username { get; private set; }
      public string Email { get; private set; }
      public int Balance { get; private set; }

      public override string ToString()
      {
        return String.Join(" ", Firstname, Lastname, Email);
      }

      public bool Equals(User x, User y)
      {
        return x.UserID == y.UserID;
      }

      public int GetHashCode(User obj)
      {
        return UserID;
      }

      public int CompareTo(User user)
      {
        return UserID - user.UserID;
      }
    }
  }
}
