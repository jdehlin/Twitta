using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;

namespace Twitta.Website.Models
{
    public class TestTable : Database.TestTable
    {
        [Editable(false)]
        public int CalculatedTotal
        {
            get
            {
                return value1 + value2;
            }
        }
    }
}