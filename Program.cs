// cording UTF-16（にするとうまく行きそう）

using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Windows.Forms;
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
                    // HeadersプロパティからSet-Cookieの値を取得する
                    var cookie = response.Headers.GetValues("Set-Cookie").First();
                    Console.WriteLine("content ->> " + content);
                    Console.WriteLine("cookie --> "+cookie +"\n");

                    // 2. Cookieの設定
                    Console.WriteLine(response);
                    var content_after = new FormUrlEncodedContent(new Dictionary<string, string>
                    {
                        { "key", "value" }
                       });
                    // リクエストのCookieヘッダに1.で取得した値を設定する
                    content_after.Headers.Add("Cookie", cookie);
                    
                    Console.WriteLine("content ->>" + content + "\n");
                    var response_after = await client.PostAsync($"https://judgeapi.u-aizu.ac.jp/session", content_after);

                    Console.WriteLine(response_after);
                    cc = handler.CookieContainer;
                    //return
                    var a = await response.Content.ReadAsStringAsync();
                    //Console.WriteLine(a + "a");
                }
            }
            //CookieCollection cookies = cc.GetCookies(new Uri("https://judgeapi.u-aizu.ac.jp/session"));
            //Console.WriteLine("cookie is " + cc.ToString());
            //Console.WriteLine("cookies is " +cookies.ToString());

            //foreach (Cookie c in cookies)
            //{
                //Console.WriteLine("クッキー名:" + c.Name.ToString());
                //Console.WriteLine("クッキーを使うサイトのドメイン名:" + c.Domain.ToString());
                //Console.WriteLine("クッキー発行日時:" + c.TimeStamp.ToString() + Environment.NewLine);
            //}

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
            var path = "/Users/kenta/Projects/3_17_aoj/3_17_aoj/data.txt";
            File.AppendAllText(path, str + "\n");
            if (!File.Exists(path))
            {
                //ここにファイルIO処理を記述する
            }
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

            /***
            string[] AtoD = new string[] { "A", "B", "C", "D" };
            for (int i = 1; i <= 11; i++) {
                for(int j=0;j<= 3; j++)
                {
                    var uli_path = "https://judgeapi.u-aizu.ac.jp/resources/descriptions/ja/ITP1_" + i +"_"+ AtoD[j];
                    var problem = "ITP1_" + i + "_" + AtoD[j];
                    var path = "/Users/kenta/Projects/3_17_aoj/3_17_aoj/data.txt";
                    File.AppendAllText(path, problem + "\n");
                    obj.Get(uli_path);
                }
            }
            ***/

            string json1 = "";
            var result = JsonConvert.DeserializeObject<Result>(json1);
        }
        public class Result
        {
            public string language { get; set; }
            public string html { get; set; }
            public List<Commentary> commentaries { get; set; }
            public int solvedUser { get; set; }
            public double successRate { get; set; }
            public double score { get; set; }
            public object recommend { get; set; }
            public int recommendations { get; set; }
            public object bookmarks { get; set; }
            public bool isSolved { get; set; }
            public object source { get; set; }
            public string problem_id { get; set; }
            public int time_limit { get; set; }
            public int memory_limit { get; set; }
            public long created_at { get; set; }
            public long server_time { get; set; }
        }

        public class Commentary
        {
            public string type { get; set; }
            public string pattern { get; set; }
            public List<string> filter { get; set; }
        }
    }

}
