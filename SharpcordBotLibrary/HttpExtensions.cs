using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharpcord_bot_library
{
    public static class HttpContentExtensions
    {
        public static HttpRequestMessage WithHeader(this HttpRequestMessage c, string name, string value)
        {
            c.Headers.Add(name, value);
            return c;
        }
        public static HttpContent WithHeader(this HttpContent c, string name, string value)
        {
            c.Headers.Add(name, value);
            return c;
        }
        public static HttpContent AsJson(this HttpContent c)
        {
            c.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            return c;
        }
        public static HttpRequestMessage WithContent(this HttpRequestMessage c, HttpContent content)
        {
            c.Content = content;
            return c;
        }
    }
}
