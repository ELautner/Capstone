using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSS.Data.DTOs
{
   /* 
   AUTHOR: Ashley Valberg
   DATE CREATED: MARCH 3 2018

   SimpleUnit
   The SimpleUnit classes purpose it to act as a POCO for the Unit table in the database.

   Properties:
   public string unitname { get; set; }
   public int unitid { get; set; }
   public bool activeyn { get; set; }
   public int caresiteid { get; set; }

   */
    public class UnitDTO
    {
        public string unitname { get; set; }
        public int unitid { get; set; }
        public bool activeyn { get; set; }
        public int caresiteid { get; set; }
    }
}
