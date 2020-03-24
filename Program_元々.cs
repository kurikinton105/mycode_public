// cording UTF-16（にするとうまく行きそう）

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
using System.Net.Http;

using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
//using System.Web.Script.Serialization;
//using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace _17_aoj
{
    public class SubmissionCode //コードを送るときのjsonのデータ構造
    {
        [DataMember(Name = "problemId")]
        public string ID { get; set; }

        [DataMember(Name = "language")]
        public string Language { get; set; }

        [DataMember(Name = "sourceCode")]
        public string SourceCode { get; set; }
    }


    class API
    {
        //Login
        private void Login()
        {

            string user, password;
            Console.WriteLine("username >>>");
            user = Console.ReadLine();
            Console.WriteLine("password >>>");
            password = Console.ReadLine();


            var task = Task.Run(() => {
                return Post_Login(user, password);

            });
            Console.WriteLine(task.Result);
            //間違っていた時用の例外処理
            //if (task.Result == "[{\"id\":1401,\"code\":\"USER_NOT_FOUND_ERROR\",\"message\":\"The user identified by  and the password is not found.\"}]")
            //{
            //Console.WriteLine("Wrong the ID or Password!!");

            //}
            //else
            //{
            //Console.WriteLine(task.Result);
                //Get("https://judgeapi.u-aizu.ac.jp/self");
                //Console.WriteLine("qawsedrftgyhujikolp");
            //}
            

        }

        public async Task<CookieContainer> Post_Login(string id, string pass)
        {
            CookieContainer cc;
            var parameters = new Dictionary<string, string>()
            {

            };
            var json = "{\"id\":\"" + id + "\",\"password\":\"" + pass + "\"}";
            //Console.WriteLine(json); //json確認用の表示
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            using (var handler = new HttpClientHandler())
            {

                using (var client = new HttpClient())
                {
                    var response = await client.PostAsync($"https://judgeapi.u-aizu.ac.jp/session", content);

                    cc = handler.CookieContainer;
                    //return
                    var a = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(a + "a");
                }
            }
            CookieCollection cookies = cc.GetCookies(new Uri("https://judgeapi.u-aizu.ac.jp/session"));
            Console.WriteLine("cookie is " + cc);
            Console.WriteLine("cookies is " +cookies);

            foreach (Cookie c in cookies)
            {
                Console.WriteLine("クッキー名:" + c.Name.ToString());
                Console.WriteLine("クッキーを使うサイトのドメイン名:" + c.Domain.ToString());
                Console.WriteLine("クッキー発行日時:" + c.TimeStamp.ToString() + Environment.NewLine);
            }

            Console.WriteLine("finish the login");

            return cc;
        }

        //GET関数 Get(URI)で実行
        private void Get(string URI)
        {
            var task = Task.Run(() => {
                return GET(URI);
            });

            string[] numbers = new string[100];
            String str = task.Result;
            
            System.Console.WriteLine(str);
            
            //String[] result = str.Split(':');
            //String[] result1 = result.Split('":"');
            //for (int i = 0; i < result.Length; i++)
            //{
            //Console.WriteLine("{0}", result[i]);

            //}



            //Console.WriteLine(str);


        }
        async public Task<string> GET(string URI)
        {
            using (var client = new HttpClient())
            {
                var uri = $"" + URI + "";
                var response = await client.GetAsync(uri);
                //UTF-8として変換
                
                return await response.Content.ReadAsStringAsync();
            }
        }

        //Submissions
        private void Submissions()
        {

            string problem_id, language, code;
            Console.WriteLine("Problem id>>> ITP1_1_A");
            problem_id = Console.ReadLine();
            Console.WriteLine("language >>> ");
            language = Console.ReadLine();
            Console.WriteLine("#include <iostream>\nusing namespace std;\nint main(void){\n cout << \"Hello World\" << endl;\n}");
            Console.WriteLine("your code >>>");
            code = Console.ReadLine();

            var json_code = JsonConvert.SerializeObject(code);

            var task = Task.Run(() => {
                return Post_submission(problem_id, language, code);
            });
            System.Console.WriteLine(task.Result);



        }

        async public Task<string> Post_submission(string id, string language, string code)
        {
            var parameters = new Dictionary<string, string>()
            {

            };


            //var json = "{" +
            //"problemId"+":"+ "ITP1_1_A"+","+
            //"language"+":"+"C" + "," +
            //"sourceCode" + ":" +
            //"#include \nint main(){\n  printf(\"Hello World\\n\");\n  return 0;\n}" +
            //"}";
            //var json = "{\"problemId\":\"ITP1_1_A\",\"language\":\"C++\",\"sourceCode\":\" \"}";
            var json = "{ \"problemId\":\"ITP1_1_A\",\"language\":\"C++\",\"sourceCode\":\"aa\"}";
        //var json = "{\"problemId\":\"ITP1_1_A\",\"language\":\"C++\",\"sourceCode\":\"#include <iostream>\\nusing namespace std;\\nint main(void){\\n    cout << \"Hello World\" << endl;\\n}\"}";
        Console.WriteLine(json); //json確認用の表示
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            using (var client = new HttpClient())
            {
                var response = await client.PostAsync($"https://judgeapi.u-aizu.ac.jp/submissions", content);
                return await response.Content.ReadAsStringAsync();

            }
        }


        public static void Main(string[] args)
        {
            API obj = new API();
            obj.Login();
            //obj.Get("https://judgeapi.u-aizu.ac.jp/challenges"); //challenges
            //obj.Get("https://judgeapi.u-aizu.ac.jp/courses?filter=true&lang=ja"); //Course
            //obj.Get("https://judgeapi.u-aizu.ac.jp/courses/2?lang=ja"); //プログラミング入門
            //obj.Get("https://judgeapi.u-aizu.ac.jp/resources/descriptions/ja/ITP1_1_A"); //プログラミング入門
            //obj.Get("https://judgeapi.u-aizu.ac.jp//submission_records/recent"); //findRecentSubmissionRecords
            //obj.Get("https://judgeapi.u-aizu.ac.jp/self");
            //obj.Submissions();
        }
    }

}
