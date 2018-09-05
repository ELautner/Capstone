using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using MSS.Data;
using MSS.System;
using MSS.Data.Entities;
using MSS.System.DAL;

namespace MSS.System.BLL
{
    [DataObject]
    public class TestController
    {
        [DataObjectMethod(DataObjectMethodType.Select,false)]
        public List<unit> ListAllUnits()
        {
            using (var context = new MSSContext())
            {
                return context.units.ToList();
            }
        }
    }
}
