﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OOPExam.Linesystem
{
  public partial class Linesystem
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

      static readonly string usernameValidator = "^[0-9a-z_]*$";
      static readonly string emailValidator = "^[0-9a-z_-.]+@[0-9a-z][0-9a-z\x2D]*.[0-9a-z\x2D]*[0-9a-z]$";

      public static string ValidateUser(string firstname, string lastname, string username, string email)
      {
        if (String.IsNullOrWhiteSpace(firstname)) return "Missing firstname";
        if (String.IsNullOrWhiteSpace(lastname)) return "Missing lastname";
        if (!Regex.IsMatch(username, usernameValidator)) return "Username contains invalid characters";
        if (!Regex.IsMatch(email, emailValidator)) return "Invalid emailaddress";
        return null;
      }

      public int ID { get; private set; }
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
