using System;
using System.Collections.ObjectModel;

namespace Torrents.WebSites
{
    public class Rutor : LogicQuery
    {
        public ObservableCollection<TorrentModel> Search(string Query)
        {
            ObservableCollection<TorrentModel> Torrents = new ObservableCollection<TorrentModel>();

            string Page = GET_Query($"http://rutor.info/search/0/0/000/2/{Query}");

            string[] pageSplit = Page.Split(new char[] { '<', '>', '"' });

            for (int i = 0; i < pageSplit.Length - 72; i++)
            {
                if (pageSplit[i].Contains("http://d.rutor.info/download/"))
                {
                    var torrent = new TorrentModel();

                    try
                    {
                        i += 24;
                        torrent.UrlTorrent = $"http://rutor.info{pageSplit[i]}";
                        i += 2;
                        torrent.Name = pageSplit[i].Replace("&#039;", "'");
                        i += 20;
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