using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace WcfService
{
    public class Program
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();


        public static void Main(string[] args)
        {
            Logger?.Info("Starting wcf server...");

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            #region self hosting impl.
            string selfHostUrls = System.Configuration.ConfigurationManager.AppSettings["SelfHostUrls"]?.ToString();
            string[] urls = selfHostUrls?.Trim()?.Split(';')?.Select(x => x?.Trim())?.ToArray();
            List<Uri> uris = new List<Uri>();

            if (urls?.Count() > 0)
            {
                foreach (string url in urls)
                {
                    if (Uri.TryCreate(url, UriKind.Absolute, out var uri))
                    {
                        uris.Add(uri);
                    }
                }
            }

            if (uris.Count <= 0)
            {
                uris.Add(new Uri("http://0.0.0.0:5000"));
            }
            #endregion

            try
            {
                using (ServiceHost serviceHost = new ServiceHost(typeof(Service), uris.ToArray()))
                {
                    serviceHost.UnknownMessageReceived += ServiceHost_UnknownMessageReceived;
                    serviceHost.Opening += ServiceHost_ChannelEvent;
                    serviceHost.Opened += ServiceHost_ChannelEvent;
                    serviceHost.Closing += ServiceHost_ChannelEvent;
                    serviceHost.Closed += ServiceHost_ChannelEvent;
                    serviceHost.Faulted += ServiceHost_ChannelEvent;
                    
                    serviceHost.Open();
                    
                    Logger?.Info("The service is ready at: ");
                    foreach (var uri in uris)
                    {
                        Logger?.Info(uri.ToString());
                    }

                    Logger?.Info("Press <ENTER> to terminate service.");
                    Console.ReadLine();
                }
            }
            catch (Exception ex)
            {
                Logger?.Error(ex, ex.Message);

                //this is for autorestart the service after failure in bad state!
                Environment.Exit(-1);
            }
        }

        private static void ServiceHost_UnknownMessageReceived(object sender, UnknownMessageReceivedEventArgs e)
        {
            Logger?.Error("invalid message received: ");
            Logger?.Error(e.Message.ToString());
        }

        private static void ServiceHost_ChannelEvent(object sender, EventArgs e)
        {
            Logger?.Trace($"channel event {e.ToString()} {sender?.ToString()}");
            Logger?.Trace(e.ToString());
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = (Exception)e.ExceptionObject;
            Logger?.Error(ex, ex.Message);
        }
    }
}