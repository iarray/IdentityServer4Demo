using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer4Demo
{
    public class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                //添加唯一标志(必须)
                new IdentityResources.OpenId()
                //添加邮箱(可选)
                //new IdentityResources.Email(),
                //添加域(可选)
                //new IdentityResources.Profile(),
                //添加电话(可选)
                //new IdentityResources.Phone(),
                //添加地址(可选)
                //new IdentityResources.Address()
            };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new List<ApiResource>
            {
                new ApiResource("api1", "My API")
            };
        }

        /// <summary>
        /// 客户端授权方式
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "client",

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = 
                        //GrantTypes.ClientCredentials //单client模式认证 
                        //GrantTypes.ResourceOwnerPassword //单密码模式认证 
                        GrantTypes.ResourceOwnerPasswordAndClientCredentials //client和密码模式认证 
                    ,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    // scopes that client has access to
                    AllowedScopes = { "api1" }
                }
            };
        }

        /// <summary>
        /// 密码授权方式
        /// </summary>
        /// <returns></returns>
        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "alice",
                    Password = "password"
                },
                new TestUser
                {
                    SubjectId = "2",
                    Username = "bob",
                    Password = "password"
                }
            };
        }
    }
}
