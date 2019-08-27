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

            //没有设置token的时候访问api
            string apiUrl = "http://localhost:6000/api/values";
            var withoutTokenResp = client.GetAsync(apiUrl).Result;
            if (withoutTokenResp.StatusCode != System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine("withoutToken  Response Status Code = " + withoutTokenResp.StatusCode);
            }




            // 通过client模式,获取access token
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

            //将得到的access token 请求api
            client.SetBearerToken(tokenResponse.AccessToken);
            var apiRes = client.GetStringAsync(apiUrl).Result;

            Console.WriteLine("Client模式请求 http://localhost:6000/api/values ");
            Console.WriteLine(apiRes);





            //根据用户密码模式请求token
            tokenResponse = client.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = "http://localhost:5000/connect/token",
                ClientId = "client",
                ClientSecret = "secret",

                UserName = "alice",
                Password = "password",
                Scope = "api1"
            }).Result;

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }

            Console.WriteLine("密码模式请求 http://localhost:6000/api/values ");
            Console.WriteLine(tokenResponse.Json);





            //通过 postman 验证 token
            //在HttpHeader增加Authorization值为Bearer AccessToken的值 
            //例如Authorization:BearereyJhbGciOiJSUzI1NiIsImtpZCI6ImI0ZjEyMzI5YWJkZDlmYzIzZTU4MmJmM2ZkNjVlOTRkIiwidHlwIjoiSldUIn0

            Console.ReadKey();
        }
    }
}
