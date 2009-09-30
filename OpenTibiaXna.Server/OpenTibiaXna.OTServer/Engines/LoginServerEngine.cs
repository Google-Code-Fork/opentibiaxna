using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using OpenTibiaXna.Helpers.ServerSettings;
using System.Net;
using OpenTibiaXna.OTServer.Logging;
using OpenTibiaXna.OTServer.Objects;

namespace OpenTibiaXna.OTServer.Engines
{
    public class LoginServerEngine
    {
        #region SingleTon

        private LoginServerEngine()
        {
            this.LoginServerPort = SettingsManager.GetLoginServerPort();
            this.IsStarted = false;

            try
            {
                LoggingEngine.LogStart("Starting login server");
                this.LoginServerListener = new TcpListener(IPAddress.Any,
                                                          this.LoginServerPort);
                this.LoginServerListener.Start();
                this.LoginServerListener.BeginAcceptSocket(new AsyncCallback(LoginListenerCallback), this.LoginServerListener);
                this.IsStarted = true;
                LoggingEngine.LogDone();
            }
            catch (SocketException ex)
            {
                if (ex.NativeErrorCode.Equals(10048)) // 10048 Port is already in use
                    LoggingEngine.LogError(new LogErrorException(String.Format("Login server is already running on {0} port.", this.LoginServerPort), ex));
            }
        }

        public static LoginServerEngine Instance 
        {
            get
            {
                if (fInstance == null)
                    fInstance = new LoginServerEngine();

                return fInstance;
            }
        }
        private static LoginServerEngine fInstance;

        #endregion

        private void LoginListenerCallback(IAsyncResult ar)
        {
            Connection connection = new Connection(ServerEngine.Game);
            connection.LoginListenerCallback(ar);
            ServerEngine.Connections.Add(connection);

            this.LoginServerListener.BeginAcceptSocket(new AsyncCallback(LoginListenerCallback), this.LoginServerListener);
            LoggingEngine.LogMessage("New client connected to login server.");
        }

        public TcpListener LoginServerListener { get; set; }

        public int LoginServerPort { get; set; }

        public bool IsStarted { get; set; }
    }
}
