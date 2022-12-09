using System.Net;

namespace StartasLamstvk.Shared.Helpers
{
    public static class HttpClientHelper
    {
        public static (bool, bool) Validate(this HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                return (true, false);
            }

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return (false, true);
            }

            return (false, false);
        }

        public static string GetUrl(this string requestUrl, string apiUrl)
        {
            var path = $"{apiUrl}{requestUrl}";

            var requestUrlStartPath = path.StartsWith("https://")
                ? "https://"
                : path.StartsWith("http://")
                    ? "http://"
                    : string.Empty;

            if (requestUrlStartPath == string.Empty)
            {
                return requestUrl;
            }

            var relativeEndpointPath = path.Split(requestUrlStartPath)[1];
            var trimmedPath = relativeEndpointPath.Replace("//", "/");
            return $"{requestUrlStartPath}{trimmedPath}";
        }
    }
}
