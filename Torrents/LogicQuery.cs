using System.IO;
using System.Net;
using System.Text;

namespace Torrents
{
    public class LogicQuery
    {
        HttpWebRequest webRequest = null;
        HttpWebResponse webResponse = null;

        /// <summary>
        /// Метод для POST запросов на сайт
        /// </summary>
        /// <param name="url">Адрес: https://{Адрес сайта}/{страница}.php</param>
        /// <param name="param">Параметры: {параметр1}={значение1}&{параметр2}={значение2}</param>
        /// <returns>Возвращает страницу в виде HTML</returns>
        public string POST_Query(string url, string param)
        {
            webRequest = (HttpWebRequest)WebRequest.Create(url);

            var data = Encoding.ASCII.GetBytes(param);

            webRequest.Method = "POST";
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.ContentLength = data.Length;

            using (var stream = webRequest.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            webResponse = (HttpWebResponse)webRequest.GetResponse();

            var page = new StreamReader(webResponse.GetResponseStream(), Encoding.UTF8).ReadToEnd();

            return page;
        }

        /// <summary>
        /// Метод для GET запросов на сайт
        /// </summary>
        /// <param name="url">Адрес с параметрами: https://{Адрес сайта}/{страница}.php?{параметр1}={значение1}&{параметр2}={значение2}</param>
        /// <returns>Возвращает страницу в виде HTML</returns>
        public string GET_Query(string url)
        {
            webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.MaximumAutomaticRedirections = 4;
            webRequest.MaximumResponseHeadersLength = 4;
            webRequest.Credentials = CredentialCache.DefaultCredentials;

            webResponse = (HttpWebResponse)webRequest.GetResponse();

            Stream stream = webResponse.GetResponseStream();

            StreamReader streamReader = new StreamReader(stream, Encoding.UTF8);

            var page = streamReader.ReadToEnd();

            webResponse.Close();
            streamReader.Close();

            return page;
        }
    }
}