using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VkNet;
using Vulkan;
using System.IO;
using VkNet.Enums.Filters;
using VkNet.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using VkNet.Model.RequestParams;
using VkNet.Enums.SafetyEnums;
using System.Threading;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;
using System.Text.RegularExpressions;
using System.Net.Http;
using GPO.MongoDB;


namespace GPO
{

    public partial class Form1 : Form
    {
        VkApi api = new VkApi();
        long User_Indentity = 0;
        

        


        

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public int MyCounterMembers()
        {
            var offset = 0;
            var groupMembers = api.Groups.GetMembers(new GroupsGetMembersParams()
            {
                GroupId = idGroupBox.Text,
                Offset = offset
            });
            int count = groupMembers.Count();
            //var user_ids = new List<long>();//список id пользователей
            foreach (var user in groupMembers)
            {
               // user_ids.Add(user.Id);//добавляем id список с id пользователей
                var _user = api.Users.Get(new long[] { user.Id }).FirstOrDefault();
                if (_user == null)
                    return -1;
                /*listBox1.Items.Add(_user.FirstName + " " + _user.LastName + " " + _user.Id);*///вывод id в listbox
            }
            offset = 1000;
            do
            {
                groupMembers = api.Groups.GetMembers(new GroupsGetMembersParams()
                {
                    GroupId = idGroupBox.Text,
                    Offset = offset
                });
                foreach (var user in groupMembers)
                {
                    //user_ids.Add(user.Id);
                    var _user = api.Users.Get(new long[] { user.Id }).FirstOrDefault();
                    if (_user == null)
                    {
                        return -1;
                    }
                    //listBox1.Items.Add(_user.FirstName + " " + _user.LastName + " " + user.Id);
                }
                count += groupMembers.Count;
                offset += 1000;
            }
            while (offset < 25000 && count >= offset);

            return count;
        }

        /*авторизация для того, чтобы получить опции АПИ ВК */
        private void Auth_Click(object sender, EventArgs e)
        {
            ApiAuthParams parameters = new ApiAuthParams()
            {
                AccessToken = "0de720100de720100de72010fd0d9ca82500de70de720106fc1dfd4e135711be7310dda"
            };
            api.Authorize(parameters);

            MessageBox.Show("Поздравляем!!! Авторизация прошла успешно");
            
        }
        /*Получение информации о группе: id, count of members, name*/
        public void getGroupInfo()
        {
            var group = api.Groups.GetById(null, idGroupBox.Text, null).FirstOrDefault();
            if (group == null)
            {
                return;
            }
            int cntMem = getCountOfGroup(); 
            int? cntMem2 = group.MembersCount;
            listBox1.Items.Add("Идентификатор группы: "+group.Id);
            listBox1.Items.Add("Название: " + group.Name);
            listBox1.Items.Add("Количество участников: " + cntMem + "||" + cntMem2);
            listBox1.Items.Add("--------------------------------------------------------------------------------");
        }



        //----------------------------------------------------------------------------------
        /* excecute выборка информации о друзьях зданных пльзователей в ids для построения соц графа.  by
         по принципу кто кому друг, нужно получить структуру. 
         возвращает список из словарей
        */
        // ids - строка с идентификаторами пользователей, для которых нужно получить список друзей. 
        // ofset - в метод передается полный перечень пользователей, а ofseе смещается относительно начала массива и берет 25 из них.

        /*Вывод в лист бокс участников группы*/
        public void getMembers()
        {

            var offset = 0;
            var groupMembers = api.Groups.GetMembers(new GroupsGetMembersParams()
            {
                GroupId = idGroupBox.Text,
                Offset = offset,
                Sort = GroupsSort.IdAsc,
            });
            int count = groupMembers.Count();
            var user_ids = new List<long>();//список id пользователей
            foreach (var user in groupMembers)
            {
                user_ids.Add(user.Id);//добавляем id список с id пользователей
                var _user = api.Users.Get(new long[] { user.Id }).FirstOrDefault();
                if (_user == null)
                    return;
                listBox1.Items.Add(_user.FirstName + " " + _user.LastName + " " + _user.Id);//вывод id в listbox
            }
            offset = 1000;
            do
            {
                groupMembers = api.Groups.GetMembers(new GroupsGetMembersParams()
                {
                    GroupId = idGroupBox.Text,
                    Offset = offset,
                    Sort = GroupsSort.IdAsc,
                });
                foreach (var user in groupMembers)
                {
                    user_ids.Add(user.Id);
                    var _user = api.Users.Get(new long[] { user.Id }).FirstOrDefault();
                    if (_user == null)
                    {
                        return;
                    }
                    listBox1.Items.Add(_user.FirstName + " " + _user.LastName + " " + user.Id);
                }
                count += groupMembers.Count;
                offset += 1000;
            }
            while (offset < 25000 && count >= offset);
            //----------------------------------------------------------------------------------


            var maxtrixOfCrossedFriends = new int[user_ids.Count, user_ids.Count];//матрица для графа
            for (int i = 0; i < user_ids.Count - 1; i++)
            {
                var polsovatel = api.Users.Get(new long[] { user_ids[i] }).FirstOrDefault();
                if (polsovatel.IsClosed == true)
                {
                    for (int k = 0; k < user_ids.Count; k++)
                    {
                        maxtrixOfCrossedFriends[i, k] = 0;
                    }
                    continue;
                }
                if (polsovatel.IsDeactivated == true)
                {
                    for (int k = 0; k < user_ids.Count; k++)
                    {
                        maxtrixOfCrossedFriends[i, k] = 0;
                    }
                    continue;
                }
                Thread.Sleep(300);
                var users_Friends = api.Friends.Get(new VkNet.Model.RequestParams.FriendsGetParams
                {
                    UserId = user_ids[i],
                });
                List<long> userFriends = new List<long>();
                foreach (var user_F in users_Friends)
                {
                    userFriends.Add(user_F.Id);
                }
                for (int j = 0; j < user_ids.Count - 1; j++)
                {
                    if (userFriends.Contains(user_ids[j]) == true)
                    {
                        maxtrixOfCrossedFriends[i, j] = 1;
                    }
                    else
                    {
                        maxtrixOfCrossedFriends[i, j] = 0;
                    }
                }
            }
            string mt = "";
            for (int i = 0; i < user_ids.Count - 1; i++)
            {
                for (int j = 0; j < user_ids.Count - 1; j++)
                {
                    mt += maxtrixOfCrossedFriends[i, j].ToString();
                }
                mt += "\n";
            }
            //MessageBox.Show(mt);
            ///Создание файла с расширением graphML, после того как мы создали матрицу. 
            ///Здесь мы берем список группы и создаем в файле узлы, а потом берем матрицу и создаем ребра графа,
            ///где sourse(i-строка), а targer(j-столбец).
            string path = @"C:\Users\user\Desktop\VKAPIGraphml.graphml";
            string text = "<?xml version='1.0' encoding='utf-8'?>\n" +
                "<graphml xmlns=\"http://graphml.graphdrawing.org/xmlns\"\nxmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"\n" +
                "xsi:schemaLocation=\"http://graphml.graphdrawing.org/xmlns http://graphml.graphdrawing.org/xmlns/1.0/graphml.xsd\"><graph edgedefault=\"undirected\">\n";
            foreach (var item in user_ids)
            {
                text += $"<node id=\"{item}\"/>\n";
            }
            for (int i = 0; i < user_ids.Count - 1; i++)
            {
                for (int j = 0; j < user_ids.Count - 1; j++)
                {
                    if (maxtrixOfCrossedFriends[i, j] == 1)
                    {
                        text += $"<edge source=\"{user_ids[i]}\" target=\"{user_ids[j]}\"/>\n";
                    }
                }
            }
            text += "</graph>\n</graphml>";
            using (StreamWriter streamWriter = new StreamWriter(path, false, System.Text.Encoding.Default))
            {
                streamWriter.WriteLine(text);
            }
            MessageBox.Show(text);
        }







        /*Получение количества участников группы, !!!на прямую не получается взять его!!!*/
        public int getCountOfGroup()
        {
            //return MyCounterMembers();
            int count = 0;

            try
            {
                var groupMembers = api.Groups.GetMembers(new GroupsGetMembersParams()
                {
                    GroupId = idGroupBox.Text,
                });
                count = groupMembers.Count;
                return count;
            }
            catch
            {
                MessageBox.Show("Unable to get count of members");
                return count;
            }

        }
        private void gettingMembers_Click(object sender, EventArgs e)
        {
            getGroupInfo();
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            User_Indentity = long.Parse(listBox1.Text.Substring(listBox1.Text.LastIndexOf(" ")));
            MessageBox.Show(User_Indentity.ToString());
        }
        private void ShowMembers_Click(object sender, EventArgs e)
        {
            getMembers();
            listBox1.Text = User_Indentity.ToString();
            
            
            //listBox1.Items.Add("Список пользователей: " + GetMembersExecute("0de720100de720100de72010fd0d9ca82500de70de720106fc1dfd4e135711be7310dda", idGroupBox.Text));
        }

        private void idGroupBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void MongoButton_Click(object sender, EventArgs e)
        {
            #region Информация о группе
            var group = api.Groups.GetById(null, idGroupBox.Text, null).FirstOrDefault();

            var client1 = new MyMongoClients();

            client1.MyClient1(group.Id, getCountOfGroup(), group.Name);

            MessageBox.Show("Данные о группе сохранены в базу");

            #endregion

            #region Записываем в монго инфу об участниках группы
            client1.MyClient3(group.Id);
            var offset = 0;
            var groupMembers = api.Groups.GetMembers(new GroupsGetMembersParams()
            {
                GroupId = idGroupBox.Text,
                Offset = offset,
                Sort = GroupsSort.IdAsc,
            });
            int count = groupMembers.Count();
            var user_ids = new List<long>();//список id пользователей
            foreach (var user in groupMembers)
            {
                user_ids.Add(user.Id);//добавляем id список с id пользователей
                var _user = api.Users.Get(new long[] { user.Id }).FirstOrDefault();
                if (_user == null)
                    return;
                client1.MyClient2(_user.FirstName, _user.LastName, _user.Id, offset + 1000);//вывод id в listbox
            }
            offset = 1000;
            do
            {
                groupMembers = api.Groups.GetMembers(new GroupsGetMembersParams()
                {
                    GroupId = idGroupBox.Text,
                    Offset = offset,
                    Sort = GroupsSort.IdAsc,
                });
                foreach (var user in groupMembers)
                {
                    user_ids.Add(user.Id);
                    var _user = api.Users.Get(new long[] { user.Id }).FirstOrDefault();
                    if (_user == null)
                    {
                        return;
                    }
                    client1.MyClient2(_user.FirstName,_user.LastName, _user.Id, offset + 1000);
                }
                count += groupMembers.Count;
                offset += 1000;
            }
            while (offset < 25000 && count >= offset);
            #endregion


            #region Работа с друзьями
            var maxtrixOfCrossedFriends = new int[user_ids.Count, user_ids.Count];//матрица для графа
            for (int i = 0; i < user_ids.Count - 1; i++)
            {
                var polsovatel = api.Users.Get(new long[] { user_ids[i] }).FirstOrDefault();
                if (polsovatel.IsClosed == true)
                {
                    for (int k = 0; k < user_ids.Count; k++)
                    {
                        maxtrixOfCrossedFriends[i, k] = 0;
                    }
                    continue;
                }
                if (polsovatel.IsDeactivated == true)
                {
                    for (int k = 0; k < user_ids.Count; k++)
                    {
                        maxtrixOfCrossedFriends[i, k] = 0;
                    }
                    continue;
                }
                Thread.Sleep(300);
                var users_Friends = api.Friends.Get(new VkNet.Model.RequestParams.FriendsGetParams
                {
                    UserId = user_ids[i],
                });
                List<long> userFriends = new List<long>();
                foreach (var user_F in users_Friends)
                {
                    userFriends.Add(user_F.Id);
                }
                for (int j = 0; j < user_ids.Count - 1; j++)
                {
                    if (userFriends.Contains(user_ids[j]) == true)
                    {
                        maxtrixOfCrossedFriends[i, j] = 1;
                    }
                    else
                    {
                        maxtrixOfCrossedFriends[i, j] = 0;
                    }
                }
            }
            string mt = "";
            for (int i = 0; i < user_ids.Count - 1; i++)
            {
                for (int j = 0; j < user_ids.Count - 1; j++)
                {
                    mt += maxtrixOfCrossedFriends[i, j].ToString();
                }
                mt += "\n";
            }

            string text = "<?xml version='1.0' encoding='utf-8'?>\n" +
                "<graphml xmlns=\"http://graphml.graphdrawing.org/xmlns\"\nxmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"\n" +
                "xsi:schemaLocation=\"http://graphml.graphdrawing.org/xmlns http://graphml.graphdrawing.org/xmlns/1.0/graphml.xsd\"><graph edgedefault=\"undirected\">\n";
            foreach (var item in user_ids)
            {
                text += $"<node id=\"{item}\"/>\n";
            }
            for (int i = 0; i < user_ids.Count - 1; i++)
            {
                for (int j = 0; j < user_ids.Count - 1; j++)
                {
                    if (maxtrixOfCrossedFriends[i, j] == 1)
                    {
                        text += $"<edge source=\"{user_ids[i]}\" target=\"{user_ids[j]}\"/>\n";
                    }
                }
            }
            text += "</graph>\n</graphml>";

            client1.MyClient4(group.Id, text);
        }
        #endregion
    }
}
