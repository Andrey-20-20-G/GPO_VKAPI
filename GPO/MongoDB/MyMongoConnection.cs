using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPO.MongoDB
{
    internal class MyMongoConnection
    {
        public string ConnectionString { get; private set; } //"mongodb://localhost:27017"};
        public string DatabaseName { get; private set; }//"MyOOPF_1_DB";
                                                        //public const string myCollectionName_1 = //"MyUsers_Thread_1";
                                                        //public const string myCollectionName_2 = //"MyUsers_Thread_2";


        public MyMongoConnection()
        {
            ConnectionString = "mongodb://localhost:27017";
            DatabaseName = "VK";
        }

    }
}
