using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;

namespace Graber
{
    class GrabResultProcessor
    {
        public static string Process(string content)
        {

            if (Regex.IsMatch(content, "^http://"))
            {
                return GetResult(content);
            }
            return content;
        }

        private static string GetResult(string url)
        {
            GraberWebClient webClient = new GraberWebClient(url, null);
            HttpWebResponse webResponse = webClient.GetResponse();
            string contentType = webResponse.ContentType.ToLower();
            
            string type = contentType.Substring(0, contentType.IndexOf('/'));
            if (string.IsNullOrEmpty(type))
            {
                type = "text";
            }
            string result = url;
            switch (type)
            {
                case "text": result = webClient.GetString(webResponse).Trim(); break;
                case "image": result = CommonTool.GetTextFromImage(webResponse.GetResponseStream()).Trim(); break;
                default:
                    break;
            }
            return result;
        }
    }
}
