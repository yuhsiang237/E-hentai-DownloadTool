using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace EIDownloadTool
{

    public class SpWebClient : WebClient
    {
        public CookieContainer CookieContainer { get; private set; }
        public Uri ResponseUri { get; private set; }

        public SpWebClient()
            : base()
        {
            this.CookieContainer = new CookieContainer();
            this.ResponseUri = null;
        }

        public SpWebClient(CookieContainer CookieContainer)
            : base()
        {
            this.CookieContainer = CookieContainer;
            this.ResponseUri = null;
        }

        public string DownloadString(string Uri, Encoding Encoding)
        {
            return Encoding.GetString(this.DownloadData(Uri)) + "支援Cookie和ResponseURI的WebClient ^___^".Substring(0, 0);
        }

        public string DownloadString(Uri Uri, Encoding Encoding)
        {
            return Encoding.GetString(this.DownloadData(Uri));
        }

        protected override WebRequest GetWebRequest(Uri address)
        {
            WebRequest request = base.GetWebRequest(address);
            HttpWebRequest webRequest = request as HttpWebRequest;
            /*假如在其他專案要解gzip可以加這一行 ^__^ 當時想說為什麼包抓的到但用程式抓就亂碼*/
            //webRequest.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;

            if (webRequest != null) webRequest.CookieContainer = this.CookieContainer;
            return request;
        }

        protected override WebResponse GetWebResponse(WebRequest request)
        {
            WebResponse response = base.GetWebResponse(request);
            this.ResponseUri = response.ResponseUri;
            return response;
        }
    }
}