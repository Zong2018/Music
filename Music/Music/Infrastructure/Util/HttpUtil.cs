using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Music.Infrastructure.HttpUtil
{
    public class HttpUtil
    {
        private HttpClient httpClient;

        public HttpUtil(int timeOut = 300)
        {
            HttpClientHandler handler = new HttpClientHandler() { UseCookies = false };
            httpClient = new HttpClient(handler);
            httpClient.Timeout = new TimeSpan(0, 0, 0, timeOut); //超时时间
        }

        public HttpUtil(Dictionary<string,string>header, int timeOut = 300,string contentType = "application/json;charset=utf-8")
        {
            HttpClientHandler handler = new HttpClientHandler() { UseCookies = false };
            httpClient = new HttpClient(handler);
            httpClient.Timeout = new TimeSpan(0, 0, 0, timeOut); //超时时间
            foreach(var d in header)
            {
                httpClient.DefaultRequestHeaders.Add(d.Key, d.Value);
            }
           httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header
        }

        /// <summary>
        /// 异步Get请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="isDialog"></param>
        /// <returns></returns>
        public async Task<string> Get(string url)
        {
            HttpResponseMessage response;
            try
            {
                response = await httpClient.GetAsync(new Uri(url));
                Console.WriteLine("URL:" + url);
            }
            catch
            {
                return null;
            }
            string result = await response.Content.ReadAsStringAsync();
            return result;
        }

        /// <summary>
        /// 异步Get请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="isDialog"></param>
        /// <returns></returns>
        public async Task<string> Get(string url,Dictionary<string,string>headers)
        {
            HttpResponseMessage response;
            try
            {
                httpClient.DefaultRequestHeaders.Clear();
                foreach (var d in headers)
                {
                    httpClient.DefaultRequestHeaders.Add(d.Key, d.Value);
                }
                response = await httpClient.GetAsync(new Uri(url));
                Console.WriteLine("URL:" + url);
            }
            catch
            {
                return null;
            }
            string result = await response.Content.ReadAsStringAsync();
            return result;
        }
        /// <summary>
        /// 异步Post请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="paramList"></param>
        /// <param name="isDialog"></param>
        /// <returns></returns>
        public async Task<string> Post(string url, List<KeyValuePair<String, String>> paramList, bool isDialog = false)
        {

            HttpResponseMessage response;
            try
            {
                response = await httpClient.PostAsync(new Uri(url), new FormUrlEncodedContent(paramList));
            }
            catch
            {
                return null;
            }
            string result = await response.Content.ReadAsStringAsync();
            return result;
        }
        /// <summary>
        /// json格式上传
        /// </summary>
        /// <param name="url"></param>
        /// <param name="json"></param>
        /// <returns></returns>
        public async Task<string> PostJson(string url, string json)
        {
            string responseBody;
            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            try
            {
                HttpResponseMessage response = await httpClient.PostAsync(new Uri(url), content);
                response.EnsureSuccessStatusCode();
                responseBody = await response.Content.ReadAsStringAsync();
            }
            catch
            {
                return null;
            }
            return responseBody;
        }
    }
}
