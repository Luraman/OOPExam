using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPExam.Linesystem
{
  class SeasonalProduct : Product
  {
    public SeasonalProduct(int productid, string productname, int price) : base(productid, productname, price){}
    public DateTimeOffset SeasonStartDate;
    public DateTimeOffset SeasonEndDate;
    public override bool Active { get { return (DateTimeOffset.UtcNow >= SeasonStartDate || SeasonStartDate == null) &&
                                               (DateTimeOffset.UtcNow <= SeasonEndDate || SeasonStartDate == null); } }
  }
}