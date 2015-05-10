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
      static readonly string usernameValidator = "^[0-9a-z_]*$";
      static readonly string emailValidator = "^[0-9a-z_-.]+@[0-9a-z][0-9a-z\x2D]*.[0-9a-z\x2D]*[0-9a-z]$";

      public static string ValidateNewUser(string firstname, string lastname, string username, string email)
      {
        if (String.IsNullOrWhiteSpace(firstname)) return "No firstname written";
        if (String.IsNullOrWhiteSpace(lastname)) return "No lastname written";
        if (!Regex.IsMatch(username, usernameValidator)) return "Username contains invalid characters";
        if (!Regex.IsMatch(email, emailValidator)) return "Invalid emailaddress";
        return null;
      }

      public int UserID { get; private set; }
      public string Firstname
      {
        get { return Firstname; }
        set
        {
          if (!String.IsNullOrWhiteSpace(value)) Firstname = value;
          else throw new ArgumentException();
        }
      }
      public string Lastname
      {
        get { return Lastname; }
        set
        {
          if (!String.IsNullOrWhiteSpace(value)) Lastname = value;
          else throw new ArgumentException();
        }
      }
      public string Username
      {
        get { return Username; }
        set
        {
          if (Regex.IsMatch(value, usernameValidator)) Username = value;
          else throw new ArgumentException();
        }
      }
      public string Email
      {
        get { return Email; }
        set
        {
          if (Regex.IsMatch(value, emailValidator)) Email = value;
          else throw new ArgumentException();
        }
      }
      public int Balance;

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
