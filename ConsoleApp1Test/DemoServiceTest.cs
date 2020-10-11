using System;
using System.Threading.Tasks;
using ConsoleApp1;
using NUnit.Framework;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace ConsoleApp1Test
{
    public class DemoServiceTest
    {
        private WireMockServer _wireMock;
        private readonly int _wireMockPort = 5001;

        [SetUp]
        public void SetUp()
        {
            _wireMock = WireMockServer.Start(_wireMockPort, true);
        }

        [TearDown]
        public void TearDown()
        {
            _wireMock.Dispose();
        }

        [Test]
        public async Task Test_QueryGitHub_Works()
        {
            //to make this test pass on Windows all you need is run
            //dotnet dev-certs https --trust
            //however on Linux or in Docker the things aren't so simple

            _wireMock.Given(Request.Create().UsingGet().WithPath("/"))
                .RespondWith(Response.Create().WithStatusCode(200).WithBody("Here we are"));

            var service = new DemoService(new Uri($"https://localhost:{_wireMockPort}/"));

            var str = await service.QueryAzureSearch();

            Assert.AreEqual("Here we are", str);
        }
    }
}