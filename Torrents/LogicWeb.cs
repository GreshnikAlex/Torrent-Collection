using System.Collections.Generic;
using Torrents.WebSites;

namespace Torrents
{
    public class LogicWeb
    {
        private Kinozal kinozal;

        public LogicWeb()
        {
            kinozal = new Kinozal();
        }

        public List<TorrentModel> Search(string Query)
        {
            return kinozal.Search(Query);
        }
    }
}