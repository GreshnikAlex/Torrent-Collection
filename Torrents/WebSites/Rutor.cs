using System;
using System.Collections.ObjectModel;

namespace Torrents.WebSites
{
    /// <summary>
    /// Класс для работы с сайтом rutor.info
    /// </summary>
    public class Rutor : LogicQuery
    {
        private static string urlSite = "http://the-rutor.org";

        /// <summary>
        /// Метод для доступа к адресу сайта
        /// </summary>
        public static string UrlSite { get => urlSite; }

        /// <summary>
        /// Функция для реализации работы поиска по сайту
        /// </summary>
        /// <param name="Query">Строка критерия поиска</param>
        /// <returns>Колекция найденых торрентов</returns>
        public ObservableCollection<TorrentModel> Search(string Query)
        {
            ObservableCollection<TorrentModel> Torrents = new ObservableCollection<TorrentModel>();

            string Page = GET($"{UrlSite}/search/0/0/000/2/{Query}");

            string[] pageSplit = Page.Split(new char[] { '<', '>', '"' });

            for (int i = 0; i < pageSplit.Length - 56; i++)
            {
                if (pageSplit[i].Contains("/parse/d.rutor.org/download/"))
                {
                    var torrent = new TorrentModel();

                    try
                    {
                        i += 12;
                        torrent.UrlTorrent = $"{UrlSite}{pageSplit[i]}";
                        i += 2;
                        torrent.Name = pageSplit[i].Replace("&#039;", "'");
                        i += 14;
                        torrent.Size = pageSplit[i].Replace("&nbsp;", " ");
                        i += 16;
                        torrent.Distribute = Convert.ToInt32(pageSplit[i].Replace("&nbsp;", ""));
                        i += 12;
                        torrent.Download = Convert.ToInt32(pageSplit[i].Replace("&nbsp;", ""));

                        Torrents.Add(torrent);
                    }
                    catch(FormatException)
                    {
                        continue;
                    }
                }
                else
                    continue;
            }

            return Torrents;
        }
    }
}