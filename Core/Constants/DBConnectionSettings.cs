using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Core.Constants
{
    public class DBConnectionSettings
    {
        public string MSSQLConnectionString { get; set; }
        public int? MSSQLCommandTimeOut { get; set; }

        public string MongoDBConnection { get; set; }
        public string MongoDBName { get; set; }
        public int MongoDBTokenValidity { get; set; }
    }
}
