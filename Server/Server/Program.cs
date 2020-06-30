    using System;
    using System.IO;
    using System.Runtime.Remoting;
    using System.Runtime.Remoting.Channels;
    using System.Runtime.Remoting.Channels.Tcp;
    using System.Windows.Forms;
    
    namespace ConsoleApplicationrem
    {
        public class Server
        {
                     public static void Main()
            {
                TcpChannel h = new TcpChannel(6);
                ChannelServices.RegisterChannel(h, false);
                RemotingConfiguration.RegisterWellKnownServiceType(typeof(product),"RemoteObjects", WellKnownObjectMode.Singleton);
                Console.WriteLine("The Server hasstarted");
                Console.WriteLine("Press the enter keyto stop the server ...");
                Console.ReadLine();
                //MessageBox.Show("the server has started");
                         
            }
        }
    }

