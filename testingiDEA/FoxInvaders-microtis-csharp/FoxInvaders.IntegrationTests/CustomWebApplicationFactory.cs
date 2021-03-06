using System;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FoxInvaders.IntegrationTests
{
    public class CustomWebApplicationFactory : WebApplicationFactory<TestStartup>
    {
        private Action<IServiceCollection>? configure;

        protected override IHostBuilder CreateHostBuilder()
        {
            var hostBuilder = Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<TestStartup>();

                    // The ApplicationKey is expected to be the assembly name of the startup class
                    var startupAssemblyName = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
                    webBuilder.UseSetting(WebHostDefaults.ApplicationKey, startupAssemblyName);
                });

            hostBuilder.UseEnvironment(Environments.Development);

            return hostBuilder;
        }

        public void ConfigureTestServices(Action<IServiceCollection> configure)
        {
            this.configure = configure;
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services => configure?.Invoke(services));
        }
    }
}
