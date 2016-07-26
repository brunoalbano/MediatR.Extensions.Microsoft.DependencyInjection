namespace MediatR.Extensions.Microsoft.DependencyInjection.Tests
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Shouldly;
    using Xunit;
    using System.Net.Http;
    using System.Threading.Tasks;
    using global::Microsoft.AspNetCore.TestHost;
    using global::Microsoft.AspNetCore.Hosting;

    public class AspNetTests : IDisposable
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public AspNetTests()
        {
            _server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        public void Dispose()
        {
            _server.Dispose();
            _client.Dispose();
        }

        [Fact]
        public async Task ShouldResolveScopedDependencyOnAspNet()
        {
            var firstId = await (await _client.GetAsync("/")).Content.ReadAsStringAsync();
            var secondId = await (await _client.GetAsync("/")).Content.ReadAsStringAsync();

            firstId.ShouldNotBe(secondId);
        }
    }
}
