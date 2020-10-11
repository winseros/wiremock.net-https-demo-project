using System;
using System.Threading.Tasks;
using ConsoleApp1.ThirdParty;

namespace ConsoleApp1
{
    public class DemoService
    {
        private readonly Uri _baseAddress;

        public DemoService(Uri baseAddress)
        {
            _baseAddress = baseAddress;
        }

        public async Task<string> QueryAzureSearch()
        {
            using var client = new ThirdPartyClient(_baseAddress);//this client is 3rd party and has some logic hardcoded

            var result = await client.GetAsync("/");

            return await result.Content.ReadAsStringAsync();
        }
    }
}