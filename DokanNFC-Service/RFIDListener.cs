using DokanNet;
using LibLogicalAccess;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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

        DokanRFIDDriver driver;

        IChip insertedChip;

        DateTime? chipInsertionDate;

        AutoResetEvent waitRemoval;

        Thread dokanThrd;

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
            waitRemoval = new AutoResetEvent(false);
            insertedChip = null;
            chipInsertionDate = null;

            log.Info(String.Format("Listening on {0} reader {1}...", readerProviderName, readerUnitName));

            readerProvider = DokanNFCConfig.CreateReaderProviderFromName(readerProviderName);
            readerUnit = DokanNFCConfig.CreateReaderUnitFromName(readerProvider, readerUnitName);

            if (readerProvider != null && readerUnit != null)
            {
                if (readerUnit.ConnectToReader())
                {
                    DokanNFCConfig config = DokanNFCConfig.GetSingletonInstance();
                    switch (config.Mode)
                    {
                        case DisplayMode.RawRFID:
                            break;
                        case DisplayMode.NFC:
                            driver = new NFCDokanRFIDDriver(this);
                            break;
                    }

                    if (driver != null)
                    {
                        string mountPoint = config.MountPoint;
                        if (String.IsNullOrEmpty(mountPoint))
                        {
                            string[] mountPoints = DokanNFCConfig.GetAvailableMountPoints();
                            if (mountPoints.Length > 0)
                            {
                                mountPoint = mountPoints[0];
                            }
                        }

                        if (!String.IsNullOrEmpty(mountPoint))
                        {
                            DokanOptions options = DokanOptions.FixedDrive;
                            if (config.AlwaysMounted)
                            {
                                Mount(mountPoint, options);
                            }
                            else
                            {
                                options |= DokanOptions.RemovableDrive;
                            }
                            do
                            {
                                if (readerUnit.WaitInsertion(500))
                                {
                                    Thread.Sleep(400);
                                    chipInsertionDate = DateTime.Now;
                                    log.Info("Card inserted.");
                                    if (readerUnit.Connect())
                                    {
                                        insertedChip = readerUnit.GetSingleChip();
                                        if (insertedChip != null)
                                        {
                                            log.Info(String.Format("Chip type `{0}`", insertedChip.Type));
                                            if (!config.AlwaysMounted)
                                            {
                                                Mount(mountPoint, options);
                                            }

                                            while (!waitRemoval.WaitOne(500) && IsRunning) ;
                                            log.Info("Card removed.");

                                            if (!config.AlwaysMounted)
                                            {
                                                Unmount(mountPoint);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        log.Error("Cannot connect to the card.");
                                    }
                                    chipInsertionDate = null;
                                }
                            } while (IsRunning);

                            if (config.AlwaysMounted)
                            {
                                Unmount(mountPoint);
                            }
                        }
                        else
                        {
                            log.Error("No mount point.");
                        }
                    }
                    else
                    {
                        log.Error("No matching file system driver to mount.");
                    }
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

        private void Mount(string mountPoint, DokanOptions options)
        {
            dokanThrd = new Thread(delegate()
            {
                log.Info("Dokan thread started");
                driver.Mount(mountPoint, options);
            });
            dokanThrd.Start();
        }

        private void Unmount(string mountPoint)
        {
            log.Info(String.Format("Unmount {0}", mountPoint));

            try
            {
                Dokan.RemoveMountPoint(mountPoint);
                driver.Unmount(null);
                driver = null;
            }
            catch (DokanException ex)
            {
                log.Error("Unmount failed", ex);
            }

            if (dokanThrd != null && dokanThrd.IsAlive)
            {
                log.Info("Dokan thread join");
                dokanThrd.Join();
            }
            dokanThrd = null;
            log.Info("Dokan thread ended");
        }

        public void ResetCard()
        {
            waitRemoval.Set();
            insertedChip = null;
            if (driver != null)
            {
                driver.ResetCache();
            }
        }

        public IChip GetChip()
        {
            return insertedChip;
        }

        public bool ReconnectOnCard()
        {
            if (insertedChip == null)
                return false;

            bool ret = false;
            try
            {
                readerUnit.Disconnect();
                ret = readerUnit.Connect();
                if (ret)
                {
                    insertedChip = readerUnit.GetSingleChip();
                }
            }
            catch (COMException)
            {
                ret = false;
            }

            if (!ret)
            {
                ResetCard();
            }

            return ret;
        }

        public DateTime GetChipInsertionDate()
        {
            return (chipInsertionDate.HasValue) ? chipInsertionDate.Value : DateTime.Now;
        }
    }
}
