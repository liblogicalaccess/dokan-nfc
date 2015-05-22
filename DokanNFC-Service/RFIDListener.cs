using LibLogicalAccess;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DokanNFC
{
    public class RFIDListener
    {
        protected static readonly ILog log = LogManager.GetLogger(typeof(RFIDListener));

        public RFIDListener()
        {
            DokanNFCConfig config = DokanNFCConfig.GetSingletonInstance();
            this.readerProviderName = config.ReaderProvider;
            this.readerUnitName = config.ReaderUnit;
        }

        public RFIDListener(string readerProviderName, string readerUnitName)
        {
            this.readerProviderName = readerProviderName;
            this.readerUnitName = readerUnitName;
        }

        string readerProviderName;

        string readerUnitName;

        Thread listeningThrd;

        IReaderProvider readerProvider;

        IReaderUnit readerUnit;

        public bool IsRunning { get; protected set; }

        public void Start()
        {
            listeningThrd = new Thread(new ThreadStart(Listen));
            listeningThrd.Start();
        }

        public void Stop()
        {
            if (listeningThrd != null)
            {
                IsRunning = false;
                listeningThrd = null;
            }
        }

        protected void Listen()
        {
            IsRunning = true;

            log.Info(String.Format("Listening on {0} reader {1}...", readerProviderName, readerUnitName));

            readerProvider = DokanNFCConfig.CreateReaderProviderFromName(readerProviderName);
            readerUnit = DokanNFCConfig.CreateReaderUnitFromName(readerProvider, readerUnitName);

            if (readerProvider != null && readerUnit != null)
            {
                if (readerUnit.ConnectToReader())
                {
                    // TODO: mount Dokan drive here
                    do
                    {
                        if (readerUnit.WaitInsertion(500))
                        {
                            log.Info("Card inserted.");
                            if (readerUnit.Connect())
                            {
                                IChip chip = readerUnit.GetSingleChip();
                                // TODO: notify Dokan of card insertion

                                readerUnit.Disconnect();
                                while (!readerUnit.WaitRemoval(500) && IsRunning) ;
                                log.Info("Card removed.");
                                // TODO: notify Dokan of card removal
                            }
                            else
                            {
                                log.Error("Cannot connect to the card.");
                            }
                        }
                    } while (IsRunning);

                    // TODO: unmount Dokan drive here
                    readerUnit.DisconnectFromReader();
                }
                else
                {
                    log.Error("Cannot connect to the reader.");
                }

                GC.KeepAlive(readerUnit);
                GC.KeepAlive(readerProvider);
            }
            else
            {
                log.Error("Error in RFID reader resource allocation.");
            }
        }
    }
}
