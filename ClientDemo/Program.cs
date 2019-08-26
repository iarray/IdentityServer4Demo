using IdentityModel.Client;
using System;
using System.Net.Http;

namespace ClientDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new HttpClient();
            //不带账户密码去请求认证服务器
            //var  noAuthToGet = client.GetDiscoveryDocumentAsync("http://localhost:5000").Result;

            //if(noAuthToGet.IsError)
            //{
            //    Console.WriteLine("No Auth To Get Status Code = " + noAuthToGet.Error);
            //}
             
            // 带上账户去请求,获取access token
            var tokenResponse = client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = "http://localhost:5000/connect/token",
                ClientId = "client",
                ClientSecret = "secret",
                Scope = "api1"
            }).Result;

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);                
            }
            Console.WriteLine(tokenResponse.Json);

            //没有设置token的时候访问api
            string apiUrl = "http://localhost:6000/api/values";
            var withoutTokenResp = client.GetAsync(apiUrl).Result;
            if(withoutTokenResp.StatusCode != System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine("withoutToken  Response Status Code = " + withoutTokenResp.StatusCode );
            }

            //将得到的access token 请求api
            client.SetBearerToken(tokenResponse.AccessToken);
            var apiRes = client.GetStringAsync(apiUrl).Result;

            Console.WriteLine("Request http://localhost:6000/api/values ");
            Console.WriteLine(apiRes);

            //通过 postman 验证 token
            //在HttpHeader增加Authorization值为Bearer AccessToken的值 
            //例如Authorization:BearereyJhbGciOiJSUzI1NiIsImtpZCI6ImI0ZjEyMzI5YWJkZDlmYzIzZTU4MmJmM2ZkNjVlOTRkIiwidHlwIjoiSldUIn0

            Console.ReadKey();
        }
    }
}
