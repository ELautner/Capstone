using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSS.Data.DTOs
{
   public class CareSiteAccessDTO
    {
        public int accesscodeid { get; set;  }
        public int caresiteid { get; set; }
        public DateTime dateused { get; set; }
    }
}
