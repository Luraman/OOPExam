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
      LineSystem system = new LineSystem(new LineSystemCLI(), null);
      system.UI.Start();
    }
  }
}
