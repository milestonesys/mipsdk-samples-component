using ConfigAPIClient;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using VideoOS.ConfigurationAPI;
using VideoOS.Platform;
using VideoOS.Platform.UI;

namespace ConfigAPIUpdateFirmwareWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ConfigApiClient _configApiClient = new ConfigApiClient();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void FirmwareUpdateWindow_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeClient();
        }

        // Selects a firmware file to upload.
        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog { Filter = "All Files | *.*" };

            if (openFileDialog.ShowDialog() == true)
            {
                FirmwareFileTextBox.Text = openFileDialog.FileName;
            }
        }

        private async void UploadButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FirmwareFileTextBox.Text))
            {
                MessageBox.Show("Please enter the path to the file first.");
                return;
            }

            // Prepare the UI and the data
            UploadedFileStatus.Text = string.Empty;
            UploadedFileIdTextBox.Text = string.Empty;
            UploadButton.IsEnabled = false;
            var progress = new Progress<int>(progressPercent => UploadProgressBar.Value = progressPercent);
            var filePath = FirmwareFileTextBox.Text;
            // Start the file upload task
            var uploadResult = await Task.Run(() => UploadFirmwareFile(progress, filePath));

            // Display the result
            UploadButton.IsEnabled = true;
            UploadedFileStatus.Text = $"{uploadResult.Status} {uploadResult.UploadedFileId}";
            UploadedFileIdTextBox.Text = !string.IsNullOrEmpty(uploadResult.UploadedFileId) ? uploadResult.UploadedFileId : string.Empty;
        }

        // Selects a single hardware for firmware update operation.
        private void SelectHardwareButton_Click(object sender, RoutedEventArgs e)
        {
            // Item picker to select a single hardware device
            ItemPickerWpfWindow itemPicker = new ItemPickerWpfWindow();
            itemPicker.KindsFilter = new List<Guid> { Kind.Hardware };
            itemPicker.Items = Configuration.Instance.GetItems(ItemHierarchy.SystemDefined);

            if (itemPicker.ShowDialog().Value)
            {
                SelectedHardwareTextBox.Text = itemPicker.SelectedItems.First().FQID.ObjectId.ToString();
                HardwareNameTextBlock.Text = itemPicker.SelectedItems.First().Name;
            }
        }

        private async void UpdateHardwareButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(UploadedFileIdTextBox.Text) || string.IsNullOrWhiteSpace(SelectedHardwareTextBox.Text))
            {
                MessageBox.Show("Please enter uploaded file Id and select hardware first.");
                return;
            }

            // Prepare the UI and the data
            FirmwareUpdateStatus.Text = string.Empty;
            UpdateHardwareButton.IsEnabled = false;
            var progress = new Progress<int>(progressPercent => FirmwareUpgradeProgress.Value = progressPercent);
            var uploadedFileId = UploadedFileIdTextBox.Text;
            var hardwareId = SelectedHardwareTextBox.Text;
            // Start the firmware update task
            var updateResult = await Task.Run(() => UpdateFirmware(progress, uploadedFileId, hardwareId));

            // Display the result
            UpdateHardwareButton.IsEnabled = true;
            FirmwareUpdateStatus.Text = $"{updateResult.Status} {updateResult.NewFirmwareVersion}";
        }

        #region Private methods

        /// <summary>
        /// Handles the file upload operation via Config API.
        /// </summary>
        /// <param name="progress">Operation progress.</param>
        /// <param name="filePath">Path to the file that needs to be uploaded.</param>
        /// <returns>Operation status as a string and file upload Id if upload was successful.</returns>
        private FirmwareOperationResult UploadFirmwareFile(IProgress<int> progress, string filePath)
        {
            try
            {
                // Get the parent configuration item - Management Server
                var systemConfigurationItem = _configApiClient.GetItem("/");
                // Get the needed invoke info configuration item by calling the needed method
                var invokeInfoItem = _configApiClient.InvokeMethod(systemConfigurationItem, "UploadFileChunk");
                const int chunkSize = 1000 * 1024;
                Guid transferId = Guid.NewGuid();
                string uploadedFileId = string.Empty;
                long chunksSent = 0;
                progress?.Report(0);

                using (var fileStream = File.OpenRead(filePath))
                {
                    var buffer = new byte[chunkSize];
                    // Split the file content to chunks
                    long totalChunks = fileStream.Length / chunkSize + (fileStream.Length % chunkSize > 0 ? 1 : 0);
                    // Create hash checksum
                    byte[] fileChecksum = fileStream.GetSha256Hash();

                    for (int offset = 0; offset < fileStream.Length && string.IsNullOrEmpty(uploadedFileId); offset += chunkSize, chunksSent++)
                    {
                        int readBytes = fileStream.Read(buffer, 0, chunkSize);

                        // The last chunk is usually smaller than the chunkSize. We recreate the buffer accordingly for better performance.
                        if (readBytes < chunkSize)
                        {
                            buffer = new ArraySegment<byte>(buffer, 0, readBytes).ToArray();
                        }

                        // If the invoke item is null we return
                        if (invokeInfoItem == null)
                        {
                            progress?.Report(100);
                            return new FirmwareOperationResult { Status = "Failed: Invoke item is null.", UploadedFileId = string.Empty };
                        }
                        // Modify the configuration item to send the file chunk
                        ConfigurationApiHelper.SetPropertyValue(invokeInfoItem, "TransferId", transferId.ToString());
                        ConfigurationApiHelper.SetPropertyValue(invokeInfoItem, "ChunkData", Convert.ToBase64String(buffer));
                        ConfigurationApiHelper.SetPropertyValue(invokeInfoItem, "Offset", offset.ToString());
                        ConfigurationApiHelper.SetPropertyValue(invokeInfoItem, "Size", fileStream.Length.ToString());
                        ConfigurationApiHelper.SetPropertyValue(invokeInfoItem, "Checksum", Convert.ToBase64String(fileChecksum));
                        // Send the modified configuration item
                        var result = _configApiClient.InvokeMethod(invokeInfoItem, "UploadFileChunk");
                        uploadedFileId = ConfigurationApiHelper.GetPropertyValue(result, "StorageId");
                        var status = ConfigurationApiHelper.GetPropertyValue(result, "State");
                        // Check if the returned result contains the uploaded file Id
                        if (string.IsNullOrEmpty(uploadedFileId))
                        {
                            // Uploaded file Id is not returned - calculate the progress and report it
                            int currentProgress = (int)(((float)chunksSent / (float)totalChunks) * 100);
                            progress?.Report(currentProgress);
                        }
                        else
                        {
                            // If we get the uploaded file Id this means the upload is finished
                            progress?.Report(100);
                            return new FirmwareOperationResult { Status = status, UploadedFileId = uploadedFileId };
                        }
                    }
                }
                // If we get to here something went wrong with the upload
                progress?.Report(100);
                return new FirmwareOperationResult { Status = "Failed: Operation completed without returning upload Id.", UploadedFileId = string.Empty };
            }
            catch (Exception ex)
            {
                progress?.Report(100);
                return new FirmwareOperationResult { Status = $"Exception: {ex.Message}", UploadedFileId = string.Empty };
            }
        }

        /// <summary>
        /// Handles the firmware update operation of single hardware device.
        /// </summary>
        /// <param name="progress">Operation progress.</param>
        /// <param name="uploadedFileId">The unique Id of the uploaded firmware file.</param>
        /// <param name="hardwareId">The Id of the hardware that will have its firmware updated.</param>
        /// <returns>Operation status as a string and the new firmware version if update was successful.</returns>
        private FirmwareOperationResult UpdateFirmware(IProgress<int> progress, string uploadedFileId, string hardwareId)
        {
            try
            {
                progress?.Report(0);
                // Get the Recording server folder configuration item containing all Recording servers 
                var recordersConfigurationItem = _configApiClient.GetItem("/RecordingServerFolder");
                // Search for the specified hardware path 
                progress?.Report(2);
                var selectedHardwarePath = GetHardwareInvokeItemPathRecursive(recordersConfigurationItem, hardwareId);
                if (string.IsNullOrEmpty(selectedHardwarePath))
                {
                    progress?.Report(100);
                    return new FirmwareOperationResult { Status = "Failed: Cannot get the hardware configuration item.", NewFirmwareVersion = string.Empty };
                }
                progress?.Report(5);
                // Use the path found to get the hardware configuration item itself
                var hardwareConfigurationItem = _configApiClient.GetItem(selectedHardwarePath);
                // Get the needed invoke info configuration item by calling the needed method
                var invokeInfoItem = _configApiClient.InvokeMethod(hardwareConfigurationItem, "UpdateFirmwareHardware");
                if (invokeInfoItem != null)
                {
                    // Modify the invoke info item with the needed method data
                    ConfigurationApiHelper.SetPropertyValue(invokeInfoItem, "StorageId", uploadedFileId);
                    // Call the method with the provided data
                    var createTaskResult = _configApiClient.InvokeMethod(invokeInfoItem, "UpdateFirmwareHardware");
                    var taskPath = ConfigurationApiHelper.GetPropertyValue(createTaskResult, "Path");
                    if (!string.IsNullOrEmpty(taskPath))
                    {
                        var taskData = GetTaskData(taskPath);
                        while (taskData.TaskProgress < 100)
                        {
                            Thread.Sleep(2000);
                            taskData = GetTaskData(taskPath);
                            progress?.Report(taskData.TaskProgress);
                        }

                        progress?.Report(100);
                        if (taskData.TaskState == "Success")
                        {
                            return new FirmwareOperationResult { NewFirmwareVersion = taskData.NewFirmwareVersion, Status = taskData.TaskState };
                        }
                        // If we get other result than success we format it and return
                        var failedText = !string.IsNullOrEmpty(taskData.TaskErrorText) ? $"{taskData.TaskState}: {taskData.TaskErrorCode}-{taskData.TaskErrorText}" : taskData.TaskState;
                        return new FirmwareOperationResult { Status = failedText, NewFirmwareVersion = string.Empty };
                    }
                }
                // If we get to here something went wrong with the update
                progress?.Report(100);
                return new FirmwareOperationResult { Status = "Failed: Operation completed without returning the new firmware version.", NewFirmwareVersion = string.Empty };
            }
            catch (Exception ex)
            {
                progress?.Report(100);
                return new FirmwareOperationResult { Status = $"Exception: {ex.Message}", NewFirmwareVersion = string.Empty };
            }
        }

        // Initialize the Config API client
        private void InitializeClient()
        {
            _configApiClient.ServerId = EnvironmentManager.Instance.MasterSite.ServerId;
            _configApiClient.Initialize();
        }

        /// <summary>
        /// Gets the hardware configuration item path based on hardware Id. Searches recursively the entire parent node.
        /// </summary>
        /// <param name="item">The top item from </param>
        /// <param name="hardwareId">Id of the selected hardware.</param>
        /// <returns>The path to the hardware configuration item.</returns>
        private string GetHardwareInvokeItemPathRecursive(ConfigurationItem item, string hardwareId)
        {
            // If we have found the item we return its path
            if (item.Path.Contains(hardwareId) && item.ItemType == ItemTypes.Hardware)
            {
                return item.Path;
            }

            foreach (var configurationItem in _configApiClient.GetChildItems(item.Path))
            {
                // For every child item we try to find if it is the searched hardware item 
                var result = GetHardwareInvokeItemPathRecursive(configurationItem, hardwareId);
                if (!string.IsNullOrEmpty(result))
                {
                    return result;
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Gets the task status data based on its path.
        /// </summary>
        /// <param name="taskPath">Path to the task configuration item.</param>
        /// <returns>An object with the task data.</returns>
        private TaskData GetTaskData(string taskPath)
        {
            var taskItem = _configApiClient.GetItem(taskPath);
            var taskState = ConfigurationApiHelper.GetPropertyValue(taskItem, "State");
            var taskErrorCode = ConfigurationApiHelper.GetPropertyValue(taskItem, "ErrorCode");
            var taskErrorText = ConfigurationApiHelper.GetPropertyValue(taskItem, "ErrorText");
            var taskNewFirmwareVersion = ConfigurationApiHelper.GetPropertyValue(taskItem, "NewFirmwareVersion");
            int.TryParse(ConfigurationApiHelper.GetPropertyValue(taskItem, "Progress"), out var taskProgress);
            return new TaskData
            {
                TaskErrorCode = taskErrorCode,
                TaskErrorText = taskErrorText,
                TaskProgress = taskProgress,
                TaskState = taskState,
                NewFirmwareVersion = taskNewFirmwareVersion
            };
        }

        #endregion
    }

    /// <summary>
    /// Contains helper methods for work with configuration item properties.
    /// </summary>
    internal static class ConfigurationApiHelper
    {
        internal static Property GetProperty(ConfigurationItem item, string key)
        {
            IEnumerable<Property> properties = item.Properties.Where(p => (string.Compare(p.Key, key, StringComparison.InvariantCultureIgnoreCase) == 0));
            return properties.FirstOrDefault();
        }

        internal static string GetPropertyValue(ConfigurationItem item, string key)
        {
            Property property = GetProperty(item, key);
            return property != null ? property.Value : string.Empty;
        }

        internal static void SetPropertyValue(ConfigurationItem item, string key, string value)
        {
            Property property = GetProperty(item, key);
            if (property != null) { property.Value = value; }
            else { Trace.WriteLine("Unable to set value for:" + key); }
        }
    }

    /// <summary>
    /// Class used to generate and return a hash of a specified file stream.
    /// </summary>
    internal static class HashHelper
    {
        public static byte[] GetSha256Hash(this Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            if (!stream.CanRead)
            {
                throw new ArgumentException("The stream is not readable.", nameof(stream));
            }

            var pos = stream.Position;
            stream.Seek(0, SeekOrigin.Begin);

            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var hash = sha256.ComputeHash(stream);
                if (pos < stream.Length)
                    stream.Seek(pos, SeekOrigin.Begin);
                return hash;
            }
        }
    }

    /// <summary>
    /// Simple operation result object used for file upload and firmware update operations.
    /// </summary>
    internal class FirmwareOperationResult
    {
        public string Status { get; set; }
        public string UploadedFileId { get; set; }
        public string NewFirmwareVersion { get; set; }
    }

    /// <summary>
    /// Class used to represent the VMS task data. 
    /// </summary>
    internal class TaskData
    {
        public string TaskState { get; set; }
        public string NewFirmwareVersion { get; set; }
        public int TaskProgress { get; set; }
        public string TaskErrorCode { get; set; }
        public string TaskErrorText { get; set; }
    }
}
