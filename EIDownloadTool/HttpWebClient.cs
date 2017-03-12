using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
namespace EHdownloadTool
{
    class HttpWebClient : WebClient
    {
        private Uri responseUri;
        private CookieContainer cookieContainer;


        public HttpWebClient()
        {
            this.cookieContainer = new CookieContainer();
        }

        public HttpWebClient(CookieContainer cookieContainer)
        {
            this.cookieContainer = cookieContainer;
        }

        public CookieContainer Cookies
        {
            get
            {
                return this.cookieContainer;
            }
            set
            {
                this.cookieContainer = value;
            }
        }

        protected override WebRequest GetWebRequest(Uri address)
        {
            //throw new Exception();
            WebRequest request;
            request = base.GetWebRequest(address);
            //判斷是不是HttpWebRequest.只有HttpWebRequest才有此属性
            if (request is HttpWebRequest)
            {
                HttpWebRequest httpRequest = request as HttpWebRequest;
                httpRequest.CookieContainer = this.cookieContainer;
            }
            return request;
        }

        public Uri ResponseUri
        {
            get
            {
                return responseUri;
            }
        }

        protected override WebResponse GetWebResponse(WebRequest webRequest)
        {
            try
            {
                WebResponse webResponse = base.GetWebResponse(webRequest);
                responseUri = webResponse.ResponseUri;
                return webResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}