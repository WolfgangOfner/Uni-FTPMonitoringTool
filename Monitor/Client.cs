namespace Monitor
{
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading;

    /// <summary>
    /// Represents our Server class.
    /// </summary>
    public class Client
    {
        private readonly UdpClient udpClient;
        IAsyncResult ar_ = null;
        int port = 0;
        Thread t = null;

        public Client(IPAddress ipAddress, int portN)
        {
           udpClient = new UdpClient(portN);
           port = portN;
        }

        internal void Start()
        {
            if (t != null)
            {
                throw new Exception("Already started, stop first");
            }
            Console.WriteLine("Started listening");
            Console.WriteLine("To end program press e");
         
                StartListening();         
        }

        private void StartListening()
        {
           ar_ = udpClient.BeginReceive(Receive, new object()); 
        }

        private void Receive(IAsyncResult ar)
        {
            IPEndPoint ip = new IPEndPoint(IPAddress.Any, port);
            byte[] bytes = udpClient.EndReceive(ar, ref ip);
            string message = Encoding.ASCII.GetString(bytes);
            Console.WriteLine(message);
            StartListening();
        }

        public void EndProgram()
        {
            try
            {
                udpClient.Close();
                Console.WriteLine("Stopped listening");
                Environment.Exit(0);
            }
            catch 
            { 
                // dont care if exception
            }
        }
    }
}