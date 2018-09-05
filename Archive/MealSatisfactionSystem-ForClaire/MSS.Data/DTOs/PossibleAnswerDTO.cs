using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSS.Data.DTOs
{
    public class PossibleAnswerDTO
    {
        public int possibleanswerid { get; set; }
        public string possibleanswertext { get; set; }
        public bool activeyn { get; set; }
        public int surveyquestion { get; set; }

    }
}
