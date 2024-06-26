using System;
using System.Windows.Forms;
using System.Linq;
using VideoOS.Platform;
using VideoOS.Platform.Transact.Services.Client;
using VideoOS.Platform.Transact.Services.DataContracts;
using VideoOS.Platform.UI;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;

namespace TransactClient
{
    public partial class MainForm : Form
    {
        private Item _item;
        private ITransactQueryClient _client;
        private StartLiveSessionResult _session;
        private CancellationTokenSource _cts;

        public MainForm()
        {
            InitializeComponent();

            // Initializing the transaction client
            InitClient();

            // Updating state of controls
            UpdateControlsState();
        }

        private async void OnClose(object sender, EventArgs e)
        {
            // Stoping the live transaction session
            await StopLiveTransactions();

            // Closing the transaction client
            CloseClient();

            // Shutting SDK down properly
            VideoOS.Platform.SDK.Environment.RemoveAllServers();

            Close();
        }
        private async void buttonPick_Click(object sender, EventArgs e)
        {
            // Using ItemPickerWpfWindow component to pick elements of the type TransactionSource
            ItemPickerWpfWindow itemPicker = new ItemPickerWpfWindow()
            {
                KindsFilter = new List<Guid> { Kind.TransactSource },
                Items = Configuration.Instance.GetItems(ItemHierarchy.SystemDefined)
            };

            if (itemPicker.ShowDialog().Value)
            {
                // Remembering the selected item
                this._item = itemPicker.SelectedItems.First();

                // Displaying selected item text
                labelSelectedItem.Text = "Source: " + _item.Name;

                // Stopping the previous live session if already started
                await StopLiveTransactions();

                // Starting new live session
                await StartLiveTransactions();
            }
        }

        private async void buttonSearch_Click(object sender, EventArgs e)
        {
            await Search();
        }

        private async void textSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                await Search();
            }
        }

        private async void buttonLive_Click(object sender, EventArgs e)
        {
            // Check if we have client and item
            if (!CheckItemReady())
            {
                return;
            }

            // Check if the live session already running
            if (_session != null)
            {
                return;
            }

            // Starting live session
            await StartLiveTransactions();
        }

        private async Task StartLiveTransactions()
        {
            try
            {
                // Starting live session and telling it to fetch 100 last lines
                _session = await _client.StartLiveSessionAsync(new StartLiveSessionParameters
                {
                    SourceId = _item.FQID.ObjectId,
                    PrecedingLineCount = 100,
                });

                // Updating state of controls
                UpdateControlsState();

                // Creating cancelation source, so we could cancel the task and starting the task of updating the live transactions
                _cts = new CancellationTokenSource();
                await UpdateLiveTransactions(_cts.Token);
            }
            catch (OperationCanceledException) { }
        }

        private async Task StopLiveTransactions()
        {
            // Stop the live update task
            if (_cts != null)
            {
                _cts.Cancel();
                _cts.Dispose();
                _cts = null;
            }

            // Close the session
            if (_session != null)
            {
                await _client.StopLiveSessionAsync(new StopLiveSessionParameters
                {
                    SessionId = this._session.SessionId
                });
                _session = null;
            }

            // Clearing the text
            this.textTransactions.Clear();

            // Updating state of controls
            UpdateControlsState();
        }

        private void InitClient()
        {
            // Creating transaction query client
            _client = new TransactQueryClient(EnvironmentManager.Instance.CurrentSite.ServerId);
        }

        private void CloseClient()
        {
            // Closing the client
            if (_client != null)
            {
                _client.Close();
                _client = null;
            }
        }

        private async Task UpdateLiveTransactions(CancellationToken token)
        {
            // Displaying already prefetched lines from the sessions
            DisplayTextLines(_session.TransactionLines.Select(s => s.Content));

            // Starting a loop to update lines every 50 milliseconds
            const double timeout = 500;
            while (!token.IsCancellationRequested)
            {
                // Getting the new lines since last call of this method
                GetLiveLinesResult result = await _client.GetLiveLinesAsync(new GetLiveLinesParameters
                {
                    SessionId = this._session.SessionId,
                    Timeout = TimeSpan.FromMilliseconds(timeout)
                });

                // Check for if this function is terminated
                token.ThrowIfCancellationRequested();

                // Drawing new text lines
                DisplayTextLines(result.TransactionLines.Select(s => s.Content));
            }
        }

        private void DisplayTextLines(IEnumerable<string> transactionLines)
        {
            // No need to do anything, if we have no new information
            if (!transactionLines.Any())
            {
                return;
            }

            const int maximumBufferSize = 50000;

            // Joining the received lines into text with line breaks and updating the text of the UI control
            var text = string.Join("", transactionLines.Select(s => s + "\r\n"));
            this.textTransactions.Text += text; // TODO: trim text
            if (this.textTransactions.Text.Length > maximumBufferSize)
            {
                this.textTransactions.Text = this.textTransactions.Text.Substring(this.textTransactions.Text.Length - maximumBufferSize);
            }

            // Scrolling to bottom
            this.textTransactions.SelectionStart = this.textTransactions.Text.Length;
            this.textTransactions.ScrollToCaret();
        }

        private async Task Search()
        {
            // Check if we have client and item
            if (!CheckItemReady())
            {
                return;
            }

            // Check the text is not empty
            var searchText = this.textSearch.Text.Trim();
            if (string.IsNullOrEmpty(searchText))
            {
                return;
            }

            // Closing the live session
            await StopLiveTransactions();
            
            // Fetching the search information
            GetExtendedTransactionLinesResult result = await _client.GetExtendedTransactionLinesAsync(new GetExtendedTransactionLinesParameters
            {
                SourceIds = new[] { _item.FQID.ObjectId },
                Substring = searchText,
                UtcTo = DateTime.Now.ToUniversalTime(),
                Count = 1000,
            });

            // Displaying information in the UI text control
            textTransactions.Text = result.TransactionLines.Any() ? "Search result:\r\n" : "Nothing found";
            DisplayTextLines(result.TransactionLines.Select(s => s.Content));
        }

        private bool CheckItemReady()
        {
            return _client != null && _item != null;
        }

        private void UpdateControlsState()
        {
            buttonSearch.Enabled = textSearch.Enabled = this._item != null;
            buttonLive.Enabled = this._item != null && this._session == null;
        }

    }
}
