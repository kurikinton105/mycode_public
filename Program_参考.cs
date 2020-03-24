using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;


namespace pixiv_login
{
    class Program
    {
        const string ID = "kurikinton105";//ログインID
        const string PASSWORD = "fvjd1790";//ログインパスワード
        const string LOGIN_ADDRESS = "https://judgeapi.u-aizu.ac.jp/session";

        static void Main(string[] args)
        {
            var p = new Program();
            var temp = p.LoginAsync().Result;
            Console.WriteLine(temp);
            //Console.ReadLine();
        }

        public async Task<CookieContainer> LoginAsync()
        {
            CookieContainer cc;
            using (var handler = new HttpClientHandler())
            {
                using (var client = new HttpClient(handler))
                {
                    //ログイン用のPOSTデータ生成
                    var content = new FormUrlEncodedContent(new Dictionary<string, string>
                    {
                        //{"id\":\"" + ID + "\",\"password\":\"" + PASSWORD}
                        { "id", ID },
                        { "password", PASSWORD }

                    });

                    //ログイン
                    await client.PostAsync(LOGIN_ADDRESS, content);

                    //クッキー保存
                    cc = handler.CookieContainer;
                }
            }

            CookieCollection cookies = cc.GetCookies(new Uri(LOGIN_ADDRESS));

            foreach (Cookie c in cookies)
            {
                Console.WriteLine("クッキー名a:" + c.Name.ToString());
                Console.WriteLine("クッキーを使うサイトのドメイン名b:" + c.Domain.ToString());
                Console.WriteLine("クッキー発行日時x:" + c.TimeStamp.ToString() + Environment.NewLine);
            }

            Console.WriteLine("ログイン処理完了a！");

            return cc;
        }
    }
}
