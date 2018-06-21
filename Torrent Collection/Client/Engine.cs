using MonoTorrent.BEncoding;
using MonoTorrent.Client;
using MonoTorrent.Client.Tracker;
using MonoTorrent.Common;
using System;
using System.Text;
using System.Threading;
using Torrent_Collection.Model;

namespace Torrent_Collection.Client
{
    /// <summary>
    /// Класс с реализацие загрузки контента
    /// </summary>
    public class Engine
    {
        /// <summary>
        /// Запускает загрузку
        /// </summary>
        /// <param name="TorrentPath">Путь к торрент файлу</param>
        /// <param name="DownloadPath">Путь загрузки</param>
        public void Start(string TorrentPath, string DownloadPath, DownloadModel downloadModel)
        {
            //Вспомогательный класс
            Top10Listener listener = new Top10Listener(10);

            TorrentManager _manager;

            Torrent _torrent = null;
            EngineSettings _engineSettings = new EngineSettings()
            {
                SavePath = DownloadPath,
                ListenPort = 31337
            };
            TorrentSettings _torrentDef = new TorrentSettings(5, 100, 0, 0);
            //Движок, реализующий функции закачки
            ClientEngine _engine = new ClientEngine(_engineSettings);
            BEncodedDictionary _fastResume;
            _fastResume = new BEncodedDictionary();

            try
            { _torrent = Torrent.Load(TorrentPath+downloadModel.NameFile); }
            catch
            { _engine.Dispose(); }

            try
            {
                downloadModel.Name = _torrent.Name;

                _manager = new TorrentManager(_torrent, DownloadPath, _torrentDef);

                _engine.Register(_manager);
                _manager.TorrentStateChanged += delegate (object o, TorrentStateChangedEventArgs e)
                {
                    lock (listener)
                        listener.WriteLine("Last status: " + e.OldState.ToString() + " Current status: " + e.NewState.ToString());
                };

                foreach (TrackerTier ttier in _manager.TrackerManager.TrackerTiers)
                {
                    foreach (Tracker tr in ttier.GetTrackers())
                    {
                        tr.AnnounceComplete += delegate (object sender, AnnounceResponseEventArgs e)
                        { listener.WriteLine(string.Format($"{e.Successful}: {e.Tracker}")); };
                    }
                }
                _manager.Start();
                int i = 0;
                bool _running = true;
                StringBuilder _stringBuilder = new StringBuilder(1024);
                while (_running)
                {
                    if ((i++) % 10 == 0)
                    {
                        if (_manager.State == TorrentState.Stopped)
                            _running = false;

                        downloadModel.Percent = Convert.ToInt16(_manager.Progress);
                        downloadModel.Upload = _manager.Peers.Seeds;
                        downloadModel.Download = _manager.Peers.Leechs;
                        var Status = _manager.State;
                    }
                }
            }
            catch { return; }
            
        }
    }
}