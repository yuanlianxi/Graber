using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;

namespace Graber
{
    internal class GraberWebClient
    {
        private HttpWebRequest webRequest;
        private string url;
        private Encoding encoding;

        public GraberWebClient(string url, Encoding encoding)
        {
            this.url = url;
            this.encoding = encoding;
            webRequest = HttpWebRequest.Create(url) as HttpWebRequest;
        }

        public IAsyncResult BeginDownString(AsyncCallback callBack, object state)
        {
            return webRequest.BeginGetResponse(callBack, state);
        }
        public Stream DownStream()
        {
            HttpWebResponse webResponse = webRequest.GetResponse() as HttpWebResponse;
            return webResponse.GetResponseStream();
        }
        public HttpWebResponse GetResponse()
        {
            return webRequest.GetResponse() as HttpWebResponse;
        }
        public string DownString()
        {
            HttpWebResponse webResponse = webRequest.GetResponse() as HttpWebResponse;
            return GetString(webResponse);
        }

        public string EndDownString(IAsyncResult iresult)
        {
            HttpWebResponse webResponse = webRequest.EndGetResponse(iresult) as HttpWebResponse;
            return GetString(webResponse);
        }

        public string GetString(HttpWebResponse webResponse)
        {
            Stream stream = webResponse.GetResponseStream();
            Encoding encoding = Encoding.GetEncoding(webResponse.CharacterSet ?? "gb2312");
            string contentEncoding = webResponse.ContentEncoding.ToLower();
            if (contentEncoding.Contains("gzip"))
            {
                stream = new GZipStream(stream, CompressionMode.Decompress);
            }
            else if (contentEncoding.Contains("deflate"))
            {
                stream = new GZipStream(stream, CompressionMode.Decompress);
            }
            StreamReader reader = new StreamReader(stream, encoding);
            return reader.ReadToEnd();
        }
    }
}