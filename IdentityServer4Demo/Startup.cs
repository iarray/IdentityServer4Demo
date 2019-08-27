using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServer4Demo
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //依赖注入 
            services.AddIdentityServer()
                    .AddDeveloperSigningCredential()  //开发环境中使用,每次启动时,为令牌签名创建了一个临时密钥.在生成环境需要一个持久化的密钥
                    .AddInMemoryIdentityResources(Config.GetIdentityResources())  //为每个访问的用户添加身份资源(如id,电话,邮箱. 最低要求添加一个唯一id)
                    .AddInMemoryApiResources(Config.GetApis())
                    .AddTestUsers(Config.GetUsers())    //密码模式授权
                    .AddInMemoryClients(Config.GetClients()) //客户端模式授权
             ;
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            } 
            app.UseIdentityServer();
        }
    }
}
