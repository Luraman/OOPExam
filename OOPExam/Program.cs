using OOPExam.Linesystem;
using OOPExam.LinesystemCLI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPExam
{
  class Program
  {
    static void Main(string[] args)
    {
      LineSystem system = new LineSystem(new LineSystemCLI(), String.Format(@"{0}\products.csv", Environment.CurrentDirectory));
      system.UI.Start();
    }
  }
}
