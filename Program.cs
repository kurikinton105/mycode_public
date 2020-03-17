using System;

namespace AOJ_Project
{
    public JObject GetResponse(string apiUrl, string jsonParameter)
    {
        JObject response = null;
        try
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiUrl);
            request.Method = "POST";
            request.ContentType = "application/json;";
            // カスタムヘッダーが必要の場合(認証トークンとか)
            //request.Headers.Add("custom-api-param", "value");

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(jsonParameter);
            }

            var httpResponse = (HttpWebResponse)request.GetResponse();

            // HttpStatusCodeの判断が必要なら
            if(httpResponse.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception("API呼び出しに失敗しました。");
            }

            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                response = JObject.Parse(streamReader.ReadToEnd());
            }

            // ex) response["status"]:"success"
        }
        catch (WebException wex)
        {
            // 200以外の場合
            if (wex.Response != null)
            {
                using (var errorResponse = (HttpWebResponse)wex.Response)
                {
                    using (var reader = new StreamReader(errorResponse.GetResponseStream()))
                    {
                        response = JObject.Parse(reader.ReadToEnd());
                    }
                }
            }
        }
    
        return response;
    }
    class Program
    {
        
        static void Main(string[] args)
        {

        var apiUrl = "http://apiurl/v1/getHogepoyo"
        var jsonParameter = new JavaScriptSerializer().Serialize(new
        {
        name = "Name",
        email = "EmailAddress",
        password = "Password",
        detail_info = new 
        {
            info1 = "info1",
            info2 = "info2"
            }
    });

    JObject response = GetResponse(apiUrl, jsonParameter);
    }
}
