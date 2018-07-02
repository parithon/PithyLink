using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PithyLink.Web.Tests
{
    public class FakeHttpClientFactory : IHttpClientFactory
    {
        private HttpClient httpClient;

        

        public HttpClient CreateClient(string name)
        {
            if (this.httpClient == null)
            {
                this.httpClient = new HttpClient(new FakeHttpMessageHandler());
            }

            return this.httpClient;
        }
    }
}
