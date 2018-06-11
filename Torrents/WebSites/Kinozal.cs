using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Torrents.WebSites
{
    /// <summary>
    /// Сайт kinozal.tv
    /// </summary>
    public class Kinozal
    {
        WebRequest webRequest;
        HttpWebResponse httpWebResponse;

        /// <summary>
        /// Конструктор
        /// </summary>
        public Kinozal()
        { 
            webRequest = WebRequest.Create("http://kinozal.tv");
            httpWebResponse = (HttpWebResponse)webRequest.GetResponse();
        }

        /// <summary>
        /// Вход в систему
        /// </summary>
        public void SignIn()
        { }

        /// <summary>
        /// Поиск
        /// </summary>
        /// <param name="Query"></param>
        public List<TorrentModel> Search(string Query)
        {
            webRequest = WebRequest.Create($"http://kinozal.tv/browse.php?s={Query}&g=0&t=1&page=0");
            httpWebResponse = (HttpWebResponse)webRequest.GetResponse();
            var responseFromServer = new StreamReader(httpWebResponse.GetResponseStream(), Encoding.Default).ReadToEnd();
            string[] list = responseFromServer.Split(new char[] { '<', '>' });

            List<TorrentModel> TorrentList = new List<TorrentModel>();
            for (int i = 0; i < list.Length - 1; i++)
            {
                if (list[i].Contains("/details.php?id="))
                {
                    var torrent = new TorrentModel();
                    string[] url = list[i].Split(new char[] { '"', '/' });
                    torrent.UrlTorrent = $"http://kinozal.tv/{url[2]}";
                    torrent.Name = list[++i];
                    i += 8;
                    torrent.Size = list[i];
                    i += 4;
                    torrent.Distribute = Convert.ToInt16(list[i]);
                    i += 4;
                    torrent.Download = Convert.ToInt16(list[i]);
                    TorrentList.Add(torrent);
                }
                else
                    continue;
            }

            return TorrentList;
        }
    }
}
