using System;
using System.IO;
using System.ServiceProcess;
using System.Timers;

namespace StefansService
{
    public partial class Service1 : ServiceBase
    {
        Timer timer = new Timer(); // name space(using System.Timers;)  
        public Service1()
        {
            InitializeComponent();
        }
        protected override void OnStart(string[] args)
        {
            WriteToFile("Service is started at " + DateTime.Now + FindAllMacAddressesFromDhcp.GetAllMacAddresses());
            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
            timer.Interval = 600000; //number in milliseconds  
            timer.Enabled = true;
        }
        protected override void OnStop()
        {
            WriteToFile($"Service is stopped at {DateTime.Now}");
        }
        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            WriteToFile($"Service is recall at { DateTime.Now} [{FindAllMacAddressesFromDhcp.GetAllMacAddresses()}]");
        }
        public static void WriteToFile(string Message)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\ServiceLog_" + 
                DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";

            if (!File.Exists(filepath))
            {
                // Create a file to write to.   
                using (StreamWriter sw = File.CreateText(filepath))
                {
                    sw.WriteLine($"{Message}");
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }

        }
    }
}