using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DokanNFC
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        static void Main()
        {
            log4net.Config.XmlConfigurator.Configure();

            string[] args = Environment.GetCommandLineArgs();
            bool startServiceMode = true;
            foreach (string p in args)
            {
                switch (p.ToLower())
                {
                        case "/debug":
                        startServiceMode = false;
                        break;
                }
            }
            if (startServiceMode)
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[] 
                { 
                    new DokanNFCSvc() 
                };
                ServiceBase.Run(ServicesToRun);
            }
            else
            {
                RFIDListener rfidListener = new RFIDListener();
                rfidListener.Start();

                while (true)
                {
                    Thread.Sleep(500);
                }
            }
        }
    }
}
