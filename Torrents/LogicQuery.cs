using System.IO;
using System.Net;
using System.Text;

namespace Torrents
{
    /// <summary>
    /// Класс логики запросов к торрент сайтам
    /// </summary>
    public class LogicQuery
    {
        HttpWebRequest webRequest = null;
        HttpWebResponse webResponse = null;

        /// <summary>
        /// Метод для GET запросов на сайт
        /// </summary>
        /// <param name="url">Адрес с параметрами</param>
        /// <returns>Возвращает страницу в виде HTML</returns>
        public string GET(string url)
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