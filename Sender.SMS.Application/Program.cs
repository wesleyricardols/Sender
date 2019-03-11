using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Timers;
using System.Web.Script.Serialization;

namespace Sender.SMS.Application
{
    public class Program
    {
        private static System.Timers.Timer _Timer;
        private static bool Launched = false;

        static void Main(string[] args)
        {
            SetTimer();
            WaitUntilItIsLaunched:
            if (!Launched)
            {
                System.Threading.Thread.Sleep(100);
                goto WaitUntilItIsLaunched;
            }
        }
        private static void SetTimer()
        {
            DateTime tenOClock = DateTime.Today.AddHours(05.50);

            if (DateTime.Now > tenOClock)
                tenOClock = tenOClock.AddDays(1.0);

            _Timer = new System.Timers.Timer();
            _Timer.Elapsed += new ElapsedEventHandler(SendSMS);
            _Timer.Interval = (double)(tenOClock - DateTime.Now).TotalMilliseconds;
            _Timer.Enabled = true;
        }
        private static void SendSMS(object source, ElapsedEventArgs e)
        {
            string bodyMessage = "Mensagem a ser enviada por SMS";
            ReceiverInfo Receiver = new ReceiverInfo("cellNumber", bodyMessage, Properties.Settings.Default["API"].ToString());

            try
            {
                WebClient client = new WebClient();
                Stream stream = client.OpenRead(Receiver.UrlAPI);
                StreamReader reader = new StreamReader(stream);

                string response = reader.ReadToEnd();
                MessageResponse messageResponse = new JavaScriptSerializer().Deserialize<MessageResponse>(response);

                if (messageResponse.messages.Any(a => a.accepted))
                {
                    Console.WriteLine(string.Format("Data/Hora: {0} - Mensagem entregue com sucesso!", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
