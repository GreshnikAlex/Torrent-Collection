using System.Collections.ObjectModel;
using Torrents.WebSites;

namespace Torrents
{
    /// <summary>
    /// Класс логики работы с торрент сайтами
    /// </summary>
    public class LogicWeb
    {
        private Rutor rutor;

        /// <summary>
        /// Конструктор логики работы с торрент сайтами
        /// </summary>
        public LogicWeb()
        {
            rutor = new Rutor();
        }

        /// <summary>
        /// Функция для поиска торрент контента
        /// </summary>
        /// <param name="Query">Строка запроса</param>
        /// <returns>Возвращает колекцию данных торрентов</returns>
        public ObservableCollection<TorrentModel> Search(string Query)
        {
            var collection = rutor.Search(Query);
            return collection;
        }
    }
}