using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSS.Data.DTOs
{
    public class CareSiteDTO
    {
        public int caresiteid { get; set; }
        public string caresitename { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string province { get; set; }
        public bool activeyn { get; set; }
    }
}
