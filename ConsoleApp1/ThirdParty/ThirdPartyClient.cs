using System;
using System.Net.Http;

namespace ConsoleApp1.ThirdParty
{
    public class ThirdPartyClient: HttpClient
    {
        public ThirdPartyClient(Uri baseAddress)
        {
            if (baseAddress.Scheme != "https")
                throw new ArgumentException("Supports HTTPS only");//https check logic is hardcoded into the 3rd party client and can't be changed
            BaseAddress = baseAddress;
        }
    }
}