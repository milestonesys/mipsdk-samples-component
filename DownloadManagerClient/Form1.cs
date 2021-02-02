using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

// This example program is not built automatically with the SDK installation
// It will only compile on a computer with the XProtect Download Manager installed
// You may alternatively copy the 3 DLLs mentioned below to your development machine manually.
// It will only run 99% ok on a computer without an XProtect Enterprise or Corporate Server installed
//
// If it does not compile for you, do the following in the Solution Explorer window
// - Remove the three SSCM references, they refer to the default install path
// - Right click References 
// - Click Add Reference and select the Browse tab
// - Browse to and select the three download manager DLLs, then click OK
//
// The DLLs are: SSCM.dll, SSCM.Interface.dll and SSCM.Interface.Retriever.dll
// They are distributed with the XProtect Download Manager, not with the SDK
// A Download Manager is an integral part of both an Enterprise and a Corporate Server
// By default it is installed in C:\Program Files\Milestone\XProtect Download Manager

namespace MyDownloadManagerClient
{
    public partial class Form1 : Form
    {
        string _lang = System.Globalization.CultureInfo.CurrentCulture.ToString();
     
        public Form1()
        {
            InitializeComponent();
        }

        private void _addButton_Click(object sender, EventArgs e)
        {

            string appName = "My Download Manager Client";
            SSCM.Interface.ISSCM_1_0 iface = SSCM.Interface.Factory.Retrieve10Interface(appName);

            if (iface == null)
            {
                System.Windows.Forms.MessageBox.Show("Internal error. Missing DLLs?");
                return;
            }

            if (!File.Exists(_installerPathTextBox.Text))
            {
                DialogResult dr = System.Windows.Forms.MessageBox.Show(
                    "Create dummy install file\r\nnamed readme.txt?", "File does not exist", MessageBoxButtons.OKCancel);
                if (dr == DialogResult.OK)
                {
                    try
                    {
                        _installerPathTextBox.Text = "readme.txt";
                        StreamWriter sw = File.CreateText(_installerPathTextBox.Text);
                        sw.WriteLine("Congratulations. Your dummy installer was invoked.");
                        sw.WriteLine("Had it been a real install package, it would install by now");
                        sw.Close();
                    }
                    catch
                    {
                        System.Windows.Forms.MessageBox.Show("Error creating dummy install file");
                        return;
                    }
                }
                else
                {
                    return;
                }
            }

            // You can assign localized names for your package
            SSCM.Interface.ISSCMResult1_0 hr;
            IDictionary<string, string> locGroups = null;
            IDictionary<string, string> locVers = null;
            IDictionary<string, string> locDisps = null;

            hr = iface.StartAddElement(_installerPathTextBox.Text, "",
                _displayNameTextBox.Text,
                _groupNameTextBox.Text, _versionTextBox.Text,
                _lang, locGroups, locVers, locDisps, true);

            if (hr.Success)
            {
                // hr.Path is returned as the path where to put the file for use by the download manager
                try
                {
                    Directory.CreateDirectory(hr.Path);
                    string to_file = Path.Combine(hr.Path, Path.GetFileName(_installerPathTextBox.Text));
                    File.Copy(_installerPathTextBox.Text, to_file);

                    hr = iface.ActionDone(hr.CommandId, "ok");
                    System.Windows.Forms.MessageBox.Show("Ok. You may now try download your file.\r\n" +
                        "Enter the download URL in your Internet Browser.\r\n" +
                        "This is http://<hostname>/installation");
                }
                catch
                {
                    System.Windows.Forms.MessageBox.Show("Error copying file to download manager's repository");
                    hr = iface.ActionDone(hr.CommandId, "failure");
                }
            }
            else
            {
                // Do nothing. Download manager already reported the error
            }
        }

        private void _removeButton_Click(object sender, EventArgs e)
        {
            string appName = "My Download Manager Client";
            SSCM.Interface.ISSCM_1_0 iface = SSCM.Interface.Factory.Retrieve10Interface(appName);

            if (iface == null)
            {
                System.Windows.Forms.MessageBox.Show("Internal error. Missing DLLs?");
                return;
            }

            SSCM.Interface.ISSCMResult1_0 hr;

            hr = iface.StartRemoveElement(_installerPathTextBox.Text,
                _groupNameTextBox.Text, _versionTextBox.Text, _lang);
            if (!hr.Success)
            {
                System.Windows.Forms.MessageBox.Show(hr.ErrorDescription);
            }
            else
            {
                try
                {
                    hr = iface.ActionDone(hr.CommandId, "ok");
                }
                catch
                {
                    System.Windows.Forms.MessageBox.Show("You have struck a bug in the Download Manager DLL\r\n" +
                        "It may be solved by updating the Download Manager\r\n" +
                        "You may succeed in removing the element from the dialog to come up next");

                    hr = iface.ShowManager();
                }
            }
        }
    }
}
