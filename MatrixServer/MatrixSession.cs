using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using VideoOS.Platform;

namespace MatrixServer
{
	class MatrixSession
	{
		Socket _session;
		MatrixService _matrixService;
		ASCIIEncoding _encoder = new ASCIIEncoding();

		internal MatrixSession(MatrixService matrixService, Socket session)
		{
			this._session = session;
			this._matrixService = matrixService;
			Thread thread = new Thread(new ThreadStart(run));
			thread.Name = "MatrixSession";
			thread.IsBackground = true;
			thread.Start();
		}

		internal String Command { get; set; }

		/// <summary>
		/// Thread to receive one Matrix command, execute it and return a response.
		/// Thread is terminated upon socket disconnect or after a 30 sec timeout.
		/// </summary>
		private void run()
		{
			string command = "";
			byte[] buffer = new byte[1000];
			try
			{
				while (_session.Connected)
				{
					if (_session.Available > 0)
					{
						int bytes = _session.Receive(buffer, buffer.Length, SocketFlags.None);

						for (int i = 0; i < bytes; i++)
						{
							command += Convert.ToChar(buffer[i]);
						}

						HandleCommand(command);
						_matrixService.ShowCommand(command);
                    }

                    Thread.Sleep(100);
				}
			}
			catch (Exception e)
			{
				EnvironmentManager.Instance.ExceptionHandler("MatrixSession", "Run", e);
			}
			if (_session.Connected)
				_session.Disconnect(false);
			_session.Close();
		}

		private void HandleCommand(string command)
		{
			int start = command.IndexOf("NetMatrixMonitorBegin");
			if (start < 0)
			{
				return;
			}
			int end = command.IndexOf("NetMatrixMonitorEnd");
			if (end < 0)
			{
				return;
			}

			Trace.WriteLine("Matrix command: " + command);
			start += "NetMatrixMonitorBegin".Length;
			string parms = command.Substring(start, end - start);
			var parm = Unpack(parms);
			string password;
			string commandId;
			password = parm[0].Trim('"');
			commandId = parm[1];

			if (password == _matrixService.Password)
			{
				switch (commandId)
				{
					case "GETSTATUS":
						GetStatus();
						break;
					case "CONNECT":
						HandleConnect(parm);
						break;
					case "CONNECTGUID":
						HandleConnectGuid(parm);
						break;
					case "CONNECTGUIDS":
						HandleConnectGuids(parm);
						break;
					case "DISCONNECT":
						DisconnectAll();
						Response("DISCONNECT", "");
						break;
				}
			}
			else
			{
				_matrixService.ShowCommand("Invalid password: Command not accepted");
			}

			_session.Disconnect(false);
		}

		private void HandleConnect(List<string> parm)
		{
			if (parm.Count >= 8)
			{
				string devAddr = parm[2];
				string devFrameRate = parm[5];
				string devResolution = parm[6];
				string devName = parm[7];
				Trace.WriteLine("MatrixSession: Old Matrix command ignored");
				Response("CONNECT", "NOT_IMPLEMENTED");
			}
		}

		private void HandleConnectGuid(List<string> parm)
		{
			if (parm.Count >= 6)
			{
				Guid devGuid = new Guid(parm[2]);
				string devFrameRate = parm[3];
				string devResolution = parm[4];
				string devName = parm[5];
				Connect(devGuid, devFrameRate, devResolution, devName);
			}
			Response("CONNECTGUID", "");
		}

		private void HandleConnectGuids(List<string> parm)
		{
			if (parm.Count >= 7)
			{
				int guidCount = int.Parse(parm[2]);
				if (parm.Count >= guidCount + 6)
				{
					var guids = new List<Guid>();
					int index = 3;
					for (; index < guidCount + 3; index++)
					{
						guids.Add(new Guid(parm[index]));
					}
					string devFrameRate = parm[index++];
					string devResolution = parm[index++];
					string devName = parm[index++];
					// the sample only shows the first camera, but of course a real solution should use them all
					Connect(guids[0], devFrameRate, devResolution, devName);
				}
			}
			Response("CONNECTGUIDS", "");
		}

		/// <summary>
		/// Unpack the command string into a list of strings.
		/// If command is semi-encrypted - it will be descrypted.
		/// </summary>
		/// <param name="command"></param>
		/// <returns></returns>
		private List<string> Unpack(string command)
		{
			var parms = new List<string>();
			int i = 0;
			string parm = "";
			bool quote = false;
			string parmTxt = command.Substring(3);

			// DTS #8851: Recieved argument contain sometimes hex-values. These values are now reverted to the ASCII representation
			command = Uri.UnescapeDataString(command);

			if (command.StartsWith("+1+"))
			{
				string decrypt = "";
				string crypt = command.Substring(3);
				for (int ix = 0; ix < crypt.Length; ix++)
				{
					decrypt += Convert.ToChar((Convert.ToByte(crypt[ix]) - 0x11) & 0xff);
				}
				parmTxt = decrypt;
			}

			while (i < parmTxt.Length)
			{
				char c = parmTxt[i++];
				if (c == '"')
				{
					quote = !quote;
				}
				else
				{
					if (!quote && c == '+')
					{
						parms.Add(parm);
						parm = "";
					}
					else
						parm += c;
				}
			}
			return parms;
		}

		private void GetStatus()
		{
			List<MatrixViewItem> list = _matrixService.MatrixViewItems;
			string windowStatus = "0";
			if (list != null && list.Count > 0)
			{
				windowStatus = "" + list.Count;
				foreach (MatrixViewItem g in list)
					windowStatus += "+" + g.CameraItem.FQID.ObjectId.ToString();
			}
			Response("GETSTATUS", windowStatus);
		}

		private void Connect(Guid devGuid, string devFrameRate, string devResolution, string devName)
		{
			Item item = Configuration.Instance.GetItem(devGuid, Kind.Camera);
			if (item != null)
			{
				List<MatrixViewItem> views = _matrixService.MatrixViewItems;
				for (int ix = views.Count - 1; ix > 0; ix--)
				{
					views[ix].CameraItem = views[ix - 1].CameraItem;
				}
				views[0].CameraItem = item;
			}
		}

		private void DisconnectAll()
		{
			if (_matrixService.MatrixViewItems != null)
			{
				foreach (MatrixViewItem item in _matrixService.MatrixViewItems)
					item.CameraItem = null;
			}
		}

		private void Response(string cmd, string tx)
		{
			string response = "NetMatrixMonitorBegin+" + cmd + "+" + tx + "+NetMatrixMonitorEnd+Version30";
			_session.Send(_encoder.GetBytes(response));
		}
	}
}