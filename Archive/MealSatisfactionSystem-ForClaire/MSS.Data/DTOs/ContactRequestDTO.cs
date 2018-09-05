using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSS.Data.DTOs
{
    public class ContactRequestDTO
    {
        public int surveyid { get; set; }
        public DateTime date { get; set; }
        public string age { get; set; }
        public string gender { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string bednaumber { get; set; }
        public string phonenumber { get; set; }
        public string preferredcontact { get; set; }
        public bool? contactedyn { get; set; }
        public int respondenttypeid { get; set; }
        public int unitid { get; set; }

        public string unitname { get; set; }
        public string caresitename { get; set; }
    }
}
