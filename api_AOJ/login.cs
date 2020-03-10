using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Windows.Forms;
using System.IO;
using System.Net.Http; //問題だったやつ

using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
//using System.Web.Script.Serialization;


namespace コンソール
{

    class MainClass
    {

        private void Button1_Click(string id,string pass)
        {
            var task = Task.Run(() => {
                return Post(id,pass);
            });
            System.Console.WriteLine(task.Result);

        }

        async public Task<string> Post(string id, string pass)
        {
            var parameters = new Dictionary<string, string>()
            {

            };
            var json = "{\"id\":\""+ id +"\",\"password\":\""+pass+"\"}";
            //Console.WriteLine(json); //json確認用の表示
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            using (var client = new HttpClient())
            {
                var response = await client.PostAsync($"https://judgeapi.u-aizu.ac.jp/session", content);
                return await response.Content.ReadAsStringAsync();

            }
        }
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            string id, password;
            Console.WriteLine("username >>>");
            id = Console.ReadLine();
            Console.WriteLine("password >>>");
            password =  Console.ReadLine();


            MainClass obj = new MainClass();
            obj.Button1_Click(id,password);


        }
    }

}

