using System.Collections.ObjectModel;
using Torrents.WebSites;

namespace Torrents
{
    public class LogicWeb
    {
        private Rutor rutor;

        public LogicWeb()
        {
            rutor = new Rutor();
        }

        public ObservableCollection<TorrentModel> Search(string Query)
        {
            return rutor.Search(Query);
        }
    }
}