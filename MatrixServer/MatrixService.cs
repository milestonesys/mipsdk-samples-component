using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using VideoOS.Platform;

namespace MatrixServer
{
    class MatrixService
    {
        /// <summary>
        /// Contains the string resourses for this component
        /// </summary>

        volatile bool _running = false;
        ManualResetEvent _taken = new ManualResetEvent(false);

        int _port = 12345;
        string _password = "";
        Dictionary<Guid, MatrixViewItem> _matrixViewItems = new Dictionary<Guid, MatrixViewItem>();

        internal int Port
        {
            get { return _port; }
        }
        internal string Password
        {
            get { return _password; }
        }
        internal List<MatrixViewItem> MatrixViewItems
        {
            get
            {
                List<MatrixViewItem> uniqueMatrixViewItems = new List<MatrixViewItem>();
                foreach (MatrixViewItem matrixViewItem in _matrixViewItems.Values)
                {
                    if (!uniqueMatrixViewItems.Contains(matrixViewItem))
                    {
                        uniqueMatrixViewItems.Add(matrixViewItem);
                    }
                }
                return uniqueMatrixViewItems;
            }
        }

        internal void Add(Guid subscriberID, MatrixViewItem item)
        {
            _matrixViewItems.Add(subscriberID, item);
        }

        internal bool Remove(Guid subscriberID)
        {
            try
            {
                _matrixViewItems.Remove(subscriberID);
                return (_matrixViewItems.Count != 0);
            }
            catch (Exception)
            {
                return false;
            }
        }

        internal bool Contains(Guid subscriberID)
        {
            return _matrixViewItems.ContainsKey(subscriberID);
        }

        internal event EventHandler<String> ShowCommandEvent;
        internal void ShowCommand(String command)
        {
            if (ShowCommandEvent != null)
                ShowCommandEvent(this, command);
        }

        internal MatrixService(Guid subscriberID, MatrixViewItem matrixViewItem, int port, string password)
        {
            _port = port;
            _password = password;
            _matrixViewItems.Add(subscriberID, matrixViewItem);

            Thread thread = new Thread(new ThreadStart(run));
            thread.CurrentCulture = Thread.CurrentThread.CurrentCulture;
            thread.CurrentUICulture = Thread.CurrentThread.CurrentUICulture;
            thread.Name = "MatrixListenManager:" + _port;
            thread.IsBackground = true;
            thread.Start();
        }

        internal void ListenEnd()
        {
            _running = false;
            _taken.Set();
        }

        private void run()
        {
            //If there is also an IPv6 address we must listen to that too.
            IPAddress[] addresses = Dns.GetHostEntry(Environment.MachineName).AddressList;
            IPAddress IPv4address = null;
            IPAddress IPv6address = null;

            //If an IPv6 address exists then the OS supports it and it can be used.
            foreach (IPAddress ipAddress in addresses)
            {
                if (ipAddress.AddressFamily == AddressFamily.InterNetworkV6)
                {
                    IPv6address = ipAddress;
                }
                else if (ipAddress.AddressFamily == AddressFamily.InterNetwork)
                {
                    IPv4address = ipAddress;
                }
            }

            //Apperantly there are no addressess available (should be impossible), we assume IPv4.
            if (IPv4address == null && IPv6address == null)
            {
                IPv4address = IPAddress.Any;
            }

            Socket listenIPv4 = null;
            Socket listenIPv6 = null;
            try
            {
                if (IPv4address != null)
                {
                    listenIPv4 = new Socket(IPv4address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                    listenIPv4.Bind(new IPEndPoint(IPAddress.Any, _port));
                    listenIPv4.Listen(2);
                }

                if (IPv6address != null)
                {
                    listenIPv6 = new Socket(IPv6address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                    listenIPv6.Bind(new IPEndPoint(IPAddress.IPv6Any, _port));
                    listenIPv6.Listen(2);
                }

                _running = true;
            }
            catch (SocketException se)
            {
                EnvironmentManager.Instance.ExceptionDialog("MatrixService", "Run", se);
            }

            while (_running)
            {
                _taken.Reset();
                if (listenIPv4 != null)
                {
                    listenIPv4.BeginAccept(new AsyncCallback(AcceptSession), listenIPv4);
                }

                if (listenIPv6 != null)
                {
                    listenIPv6.BeginAccept(new AsyncCallback(AcceptSession), listenIPv6);
                }
                _taken.WaitOne();
            }

            if (listenIPv4 != null)
            {
                listenIPv4.Close();
            }
            if (listenIPv6 != null)
            {
                listenIPv6.Close();
            }
        }

        private void AcceptSession(IAsyncResult ar)
        {
            try
            {
                Socket callingSocket = ar.AsyncState as Socket;
                if (callingSocket != null)
                {
                    Socket session = callingSocket.EndAccept(ar);
                    _taken.Set();
                    new MatrixSession(this, session);
                }
                else
                {
                    //If null this method has been used for something it should not.
                    throw new Exception("A socket was not used as parameter.");
                }
            }
            catch (Exception)
            {
                // Happens during App close
            }
        }
    }
}
