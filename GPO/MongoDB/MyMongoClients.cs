using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace GPO.MongoDB
{
    internal class MyMongoClients
    {
        public async void MyClient1(long id, int Cnt, string Name)
        {
            string myCollectionName_1 = "VKGroups";

            var con1 = new MyMongoConnection();
            var client1 = new MongoClient(con1.ConnectionString);

            var db_1 = client1.GetDatabase(con1.DatabaseName);

            var collection1 = db_1.GetCollection<MyMongoItems>(myCollectionName_1);

            var User1 = new MyMongoItems { Id = id, CountofMembers = Cnt, GroupName = Name, DateTime = DateTime.Now };
            await collection1.InsertOneAsync(User1);

            //for (int i = 0; i < 100; i++)
            //{
            //    var User1 = new MyMongoItems { Id = (int)Math.Pow(i, 2), GroupName = i, CountofMembers = $"user{i}" };
            //    await collection1.InsertOneAsync(User1);
            //}
            //var resMongo = await collection1.FindAsync(_ => true);
            //foreach (var item in resMongo.ToList())
            //{
            //    Console.WriteLine($"{item.Id} | {item.UserName} | {item.Group}");
            //}

        }

        public async void MyClient2(string fstname, string lstnm, long us_id, int _count)
        {
            int n = (int)Math.Ceiling((decimal)_count / 1000);

            
               string myCollectionName_1 = "VKMembers" + n;
            
                

            var con1 = new MyMongoConnection();
            var client1 = new MongoClient(con1.ConnectionString);

            var db_1 = client1.GetDatabase(con1.DatabaseName);
                var collection1 = db_1.GetCollection<MyMemberModel>(myCollectionName_1);

                var User1 = new MyMemberModel { FrstName = fstname, LstName = lstnm, UserId = us_id };
                await collection1.InsertOneAsync(User1);
            
        }

        public async void MyClient3(long grpid)
        {
            string myCollectionName_1 = "VKMembers";

            var con1 = new MyMongoConnection();
            var client1 = new MongoClient(con1.ConnectionString);

            var db_1 = client1.GetDatabase(con1.DatabaseName);

            var collection1 = db_1.GetCollection<MyMemberModel>(myCollectionName_1);

            var User1 = new MyMemberModel { IdGroup = grpid, DateTime = DateTime.Now};
            await collection1.InsertOneAsync(User1);
        }

        public async void MyClient4(long grpid, string file)
        {
            string myCollectionName_1 = "VKGraphMLFile";

            var con1 = new MyMongoConnection();
            var client1 = new MongoClient(con1.ConnectionString);

            var db_1 = client1.GetDatabase(con1.DatabaseName);

            var collection1 = db_1.GetCollection<MyGraphMLFileModel>(myCollectionName_1);

            var User1 = new MyGraphMLFileModel {Id = grpid, GraphMLFile = file, DateTime = DateTime.Now };
            await collection1.InsertOneAsync(User1);
        }


        //public async void MyClient2()
        //{
        //    string myCollectionName_2 = "MyUsers_Thread_2";

        //    var con2 = new MyMongoConnection();
        //    var client2 = new MongoClient(con2.ConnectionString);

        //    var db_2 = client2.GetDatabase(con2.DatabaseName);

        //    var collection2 = db_2.GetCollection<MyMongoItems>(myCollectionName_2);

        //    for (int i = 101; i < 201; i++)
        //    {
        //        var User2 = new MyMongoItems { Id = (int)Math.Pow(i, 2), Group = i, UserName = $"user{i}" };
        //        await collection2.InsertOneAsync(User2);
        //    }

        //    //var resMongo = await collection2.FindAsync(_ => true);
        //    //foreach (var item in resMongo.ToList())
        //    //{
        //    //    Console.WriteLine($"{item.Id} | {item.UserName} | {item.Group}");
        //    //}
        //}


    }
    }
