using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace PithyLink.Web.Tests
{
    public class FakeHttpMessageHandler : HttpMessageHandler
    {
        public static string RedirectRequest = "http://test.me/redirect";
        public static string PerminentRedirectRequest = "http://test.me/perminentRedirect";
        public static string UnsupportedRequest = "mailto://test@test.me";
        public static string InvalidRequst = "NotAValidUrl";
        public static string EmptyRequest = string.Empty;
        public static string LoopbackRequest = "http://localhost:5000";
        public static string LoopbackSslRequest = "https://localhost:5001";
        public static string ValidRequest = "http://test.me";

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpResponseMessage response = new HttpResponseMessage
            {
                RequestMessage = new HttpRequestMessage(HttpMethod.Get, request.RequestUri.AbsoluteUri)
            };

            response.StatusCode = request.RequestUri.AbsoluteUri == RedirectRequest
                ? HttpStatusCode.Redirect
                : request.RequestUri.AbsoluteUri == PerminentRedirectRequest
                    ? HttpStatusCode.PermanentRedirect
                    : request.RequestUri.AbsoluteUri == UnsupportedRequest
                        ? HttpStatusCode.BadRequest
                        : request.RequestUri.AbsoluteUri == InvalidRequst
                            ? HttpStatusCode.BadRequest
                            : request.RequestUri.AbsoluteUri == EmptyRequest
                                ? HttpStatusCode.BadRequest
                                : HttpStatusCode.OK;

            return Task.FromResult(response);
        }
    }
}
