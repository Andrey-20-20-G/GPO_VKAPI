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
using System.IO;
using VkNet.Enums.Filters;
using VkNet.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using xNet;
using VkNet.Model.RequestParams;
using VkNet.Enums.SafetyEnums;

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
        /*авторизация для того, чтобы получить опции АПИ ВК */
        private void Auth_Click(object sender, EventArgs e)
        {
            ApiAuthParams parameters = new ApiAuthParams()
            {
                AccessToken = "0de720100de720100de72010fd0d9ca82500de70de720106fc1dfd4e135711be7310dda"
            };
            api.Authorize(parameters);
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
            listBox1.Items.Add("Идентификатор группы: "+group.Id);
            listBox1.Items.Add("Название: " + group.Name);
            listBox1.Items.Add("Количество участников: " + cntMem);
            listBox1.Items.Add("--------------------------------------------------------------------------------");
        }
        /*Вывод в лист бокс участников группы*/
        public void getMembers()
        {
            var groupMembers = api.Groups.GetMembers(new GroupsGetMembersParams()
            {
                GroupId = idGroupBox.Text,
                Sort = GroupsSort.IdAsc,
            });
            var user_ids = new List<long>();//список id пользователей
            foreach (var user in groupMembers)
            {
                user_ids.Add(user.Id);//добавляем id список с id пользователей
                //var _user = api.Users.Get(new long[] { user.Id }).FirstOrDefault();
                //if (_user == null)
                //    return;
                //listBox1.Items.Add(_user.FirstName + " " + _user.LastName + " " + _user.Id);//вывод id в listbox
            }
            var maxtrixOfCrossedFriends = new int [user_ids.Count,user_ids.Count];//матрица для графа
            for (int i = 0; i < user_ids.Count; i++)
            {
                var polsovatel = api.Users.Get(new long[] {user_ids[i]}).FirstOrDefault();
                if (polsovatel.IsClosed == true)
                {
                    for (int k = 0; k < user_ids.Count; k++)
                    {
                        maxtrixOfCrossedFriends[i, k] = 0;
                    }
                    break;
                }
                if (polsovatel.IsDeactivated == true)
                {
                    for (int k = 0; k < user_ids.Count; k++)
                    {
                        maxtrixOfCrossedFriends[i, k] = 0;
                    }
                    break;
                }
                var users_Friends = api.Friends.Get(new VkNet.Model.RequestParams.FriendsGetParams
                {
                    UserId = user_ids[i],
                });
                for (int j = 0; j < users_Friends.Count; j++)
                {
                    if (user_ids.Contains(users_Friends[j].Id)==true)
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
            for (int i=0; i< user_ids.Count; i++)
            {
                for (int j = 0; j < user_ids.Count; j++)
                {
                    if (maxtrixOfCrossedFriends[i, j]==1)
                    {
                        mt += maxtrixOfCrossedFriends[i, j].ToString();
                    }
                }
            }
            MessageBox.Show(mt);
        }
        /*Получение количества участников группы, !!!на прямую не получается взять его!!!*/
        public int getCountOfGroup()
        {
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
        }
    }
}
