using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TWAINWorkingGroupToolkit;
using System.IO;

namespace scanner.csv
{
    public partial class Configuraciones : Form
    {
        ///////////////////////////////////////////////////////////////////////////////
        // Public Methods...
        ///////////////////////////////////////////////////////////////////////////////
        #region Public Methods...

        /// <summary>
        /// Our constructor...
        /// </summary>
        /// <param name="a_twaincstool"></param>
        public Configuraciones(ref TWAINCSToolkit a_twaincstoolkit, string a_szProductDirectory)
        {
            TWAINCSToolkit.STS sts;
            string szStatus;
            string szCapability;
            string szUsrUiSettings;

            // Init stuff...
            InitializeComponent();

            // More init stuff...
            this.FormClosing += new FormClosingEventHandler(FormSetup_FormClosing);

            // Init more stuff (the order matters).  ApplicationData means the following:
            // Windows:     C:\Users\USERNAME\AppData\Roaming (or C:\Documents and Settings\USERNAME\Application Data on XP)
            // Linux:       /home/USERNAME/.config (or /root/.config for superuser)
            // Mac OS X:    /Users/USERNAME/.config (or /var/root/.config for superuser)
            m_twaincstoolkit = a_twaincstoolkit;
            m_szTwainscanFolder = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "twain"), "twaincsscan");
            m_szSettingsFolder = Path.Combine(m_szTwainscanFolder, "settings");
            m_szSettingsFolder = Path.Combine(m_szSettingsFolder, a_szProductDirectory);
            if (!Directory.Exists(m_szTwainscanFolder))
            {
                try
                {
                    Directory.CreateDirectory(m_szTwainscanFolder);
                }
                catch
                {
                    m_szTwainscanFolder = Directory.GetCurrentDirectory();
                }
            }

            // Restore values...
            m_textboxFolder.Text = RestoreFolder();
            m_textboxUseUiSettings.Text = "";

            // Make sure we prime the value...
            m_twaincstoolkit.SetImagePath(m_textboxFolder.Text, 0);

            // Check for support of Custom DS Data...
            szStatus = "";
            szCapability = "CAP_CUSTOMDSDATA";
            sts = m_twaincstoolkit.Send("DG_CONTROL", "DAT_CAPABILITY", "MSG_GETCURRENT", ref szCapability, ref szStatus);
            if ((sts != TWAINCSToolkit.STS.SUCCESS) || !szCapability.EndsWith(",1"))
            {
                m_labelUseUiSettings.Enabled = false;
                m_textboxUseUiSettings.Enabled = false;
                m_buttonSaveUiSettings.Enabled = false;
                m_buttonUseUiSettings.Enabled = false;
            }

            // Restore the last saved snapshot...
            else
            {
                m_textboxUseUiSettings.Text = RestoreSetting();
                if (m_textboxUseUiSettings.Text != "")
                {
                    szUsrUiSettings = Path.Combine(m_szSettingsFolder, m_textboxUseUiSettings.Text);
                    if (File.Exists(szUsrUiSettings))
                    {
                        m_twaincstoolkit.RestoreSnapshot(szUsrUiSettings);
                    }
                    else
                    {
                        m_textboxUseUiSettings.Text = "";
                    }
                }
            }
        }

        /// <summary>
        /// Let the caller know if we can take snapshots...
        /// </summary>
        /// <returns></returns>
        public bool IsCustomDsDataSupported()
        {
            return (m_buttonUseUiSettings.Enabled);
        }

        #endregion


        ///////////////////////////////////////////////////////////////////////////////
        // Private Methods...
        ///////////////////////////////////////////////////////////////////////////////
        #region Private Methods...

        /// <summary>
        /// Validate...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormSetup_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ((m_twaincstoolkit.GetImagePath() != "") && !Directory.Exists(m_twaincstoolkit.GetImagePath()))
            {
                MessageBox.Show("Please enter a valid Destination Folder.");
                e.Cancel = true;
            }
            if ((m_textboxUseUiSettings.Text != "") && !File.Exists(Path.Combine(m_szSettingsFolder, m_textboxUseUiSettings.Text)))
            {
                MessageBox.Show("Please enter a valid UI Setting name, or clear the entry.");
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Get the folder path...
        /// </summary>
        /// <returns></returns>
        private string RestoreFolder()
        {
            try
            {
                string szSaveSpot = m_szTwainscanFolder;
                if (!Directory.Exists(szSaveSpot))
                {
                    return ("");
                }
                string szFile = Path.Combine(szSaveSpot, "folder");
                if (File.Exists(szFile))
                {
                    return (File.ReadAllText(szFile));
                }
                return ("");
            }
            catch
            {
                return ("");
            }
        }

        /// <summary>
        /// Get the setting...
        /// </summary>
        /// <returns></returns>
        private string RestoreSetting()
        {
            try
            {
                if (!Directory.Exists(m_szSettingsFolder))
                {
                    return ("");
                }
                string szFile = Path.Combine(m_szSettingsFolder, "current");
                if (File.Exists(szFile))
                {
                    return (File.ReadAllText(szFile));
                }
                return ("");
            }
            catch
            {
                return ("");
            }
        }

        /// <summary>
        /// Remember the folder path for the next time the app runs...
        /// </summary>
        /// <param name="a_szFolder"></param>
        private void SaveFolder(string a_szFolder)
        {
            try
            {
                string szSaveSpot = m_szTwainscanFolder;
                if (!Directory.Exists(szSaveSpot))
                {
                    Directory.CreateDirectory(szSaveSpot);
                }
                File.WriteAllText(Path.Combine(szSaveSpot, "folder"), a_szFolder);
            }
            catch
            {
                // Just let it slide...
            }
        }

        /// <summary>
        /// Remember the setting for the next time the app runs...
        /// </summary>
        /// <param name="a_szFolder"></param>
        private void SaveSetting(string a_szFolder)
        {
            try
            {
                if (!Directory.Exists(m_szSettingsFolder))
                {
                    Directory.CreateDirectory(m_szSettingsFolder);
                }
                File.WriteAllText(Path.Combine(m_szSettingsFolder, "current"), a_szFolder);
            }
            catch
            {
                // Just let it slide...
            }
        }

        #endregion


        ///////////////////////////////////////////////////////////////////////////////
        // Private Form Controls...
        ///////////////////////////////////////////////////////////////////////////////
        #region Private Form Controls...

        /// <summary>
        /// Browse for a place to save image files...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_buttonBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderbrowserdialog = new FolderBrowserDialog();
            folderbrowserdialog.SelectedPath = m_textboxFolder.Text;
            folderbrowserdialog.ShowNewFolderButton = true;
            if (folderbrowserdialog.ShowDialog() == DialogResult.OK)
            {
                m_textboxFolder.Text = folderbrowserdialog.SelectedPath;
                m_twaincstoolkit.SetImagePath(folderbrowserdialog.SelectedPath, 0);
                SaveFolder(m_textboxFolder.Text);
            }
        }

        /// <summary>
        /// Delete the setting...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_buttonDeleteSetting_Click(object sender, EventArgs e)
        {
            bool blDeleted;

            // Nothing to do if it's empty...
            if (m_textboxUseUiSettings.Text == "")
            {
                return;
            }

            // If it's not real, tell the user...
            if (!File.Exists(Path.Combine(m_szSettingsFolder, m_textboxUseUiSettings.Text)))
            {
                MessageBox.Show("'" + m_textboxUseUiSettings.Text + "' not found...");
                return;
            }

            // Get confirmation...
            DialogResult dialogresult = MessageBox.Show("Delete '" + m_textboxUseUiSettings.Text + "'?", "Confirm", MessageBoxButtons.YesNo);
            if (dialogresult == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

            // Delete it...
            try
            {
                blDeleted = true;
                File.Delete(Path.Combine(m_szSettingsFolder, m_textboxUseUiSettings.Text));
            }
            catch
            {
                blDeleted = false;
                MessageBox.Show("Failed to delete setting...");
            }

            // Clear the text box...
            if (blDeleted)
            {
                m_textboxUseUiSettings.Text = "";
            }
        }

        /// <summary>
        /// Save the current driver's settings...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_buttonSaveas_Click(object sender, EventArgs e)
        {
            SaveFileDialog savefiledialog = new SaveFileDialog();
            savefiledialog.InitialDirectory = m_szSettingsFolder;
            savefiledialog.CheckFileExists = false;
            savefiledialog.CheckPathExists = true;
            savefiledialog.Filter = "Settings|*.settings";
            if (!Directory.Exists(savefiledialog.InitialDirectory))
            {
                try
                {
                    Directory.CreateDirectory(savefiledialog.InitialDirectory);
                }
                catch
                {
                    return;
                }
            }
            if (savefiledialog.ShowDialog() == DialogResult.OK)
            {
                m_textboxUseUiSettings.Text = Path.GetFileName(savefiledialog.FileName);
                m_twaincstoolkit.SaveSnapshot(Path.Combine(m_szSettingsFolder, m_textboxUseUiSettings.Text));
                SaveSetting(m_textboxUseUiSettings.Text);
            }
        }

        /// <summary>
        /// Bring up the driver's non-scanning UI...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_buttonSetup_Click(object sender, EventArgs e)
        {
            string szTwmemref = "1,0," + Handle;
            string szStatus = "";
            TWAINCSToolkit.STS sts;
            sts = m_twaincstoolkit.Send("DG_CONTROL", "DAT_USERINTERFACE", "MSG_ENABLEDSUIONLY", ref szTwmemref, ref szStatus);
        }

        /// <summary>
        /// Pick the settings for a scan session...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_buttonUseUiSettings_Click(object sender, EventArgs e)
        {
            OpenFileDialog openfiledialog = new OpenFileDialog();
            openfiledialog.InitialDirectory = m_szSettingsFolder;
            openfiledialog.CheckFileExists = true;
            openfiledialog.CheckPathExists = true;
            openfiledialog.Multiselect = false;
            openfiledialog.Filter = "Settings|*.settings";
            openfiledialog.FilterIndex = 0;
            if (!Directory.Exists(openfiledialog.InitialDirectory))
            {
                try
                {
                    Directory.CreateDirectory(openfiledialog.InitialDirectory);
                }
                catch
                {
                    MessageBox.Show("Unable to create settings folder...'" + m_szSettingsFolder + "'");
                    return;
                }
            }
            if (openfiledialog.ShowDialog() == DialogResult.OK)
            {
                m_textboxUseUiSettings.Text = Path.GetFileName(openfiledialog.FileName);
                m_twaincstoolkit.RestoreSnapshot(Path.Combine(m_szSettingsFolder, m_textboxUseUiSettings.Text));
                SaveSetting(m_textboxUseUiSettings.Text);
            }
        }

        /// <summary>
        /// Keep us updated with changes to the save image path...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_textboxFolder_TextChanged(object sender, EventArgs e)
        {
            m_twaincstoolkit.SetImagePath(m_textboxFolder.Text, 0);
            SaveFolder(m_textboxFolder.Text);
        }

        #endregion


        ///////////////////////////////////////////////////////////////////////////////
        // Private Attributes...
        ///////////////////////////////////////////////////////////////////////////////
        #region Private Attributes...

        /// <summary>
        /// The folder we persist data to...
        /// </summary>
        private string m_szTwainscanFolder;

        /// <summary>
        /// The settings folder...
        /// </summary>
        private string m_szSettingsFolder;

        /// <summary>
        /// Access to the TWAIN driver...
        /// </summary>
        private TWAINCSToolkit m_twaincstoolkit;

        #endregion

        private void M_labelSelectDestinationFolder_Click(object sender, EventArgs e)
        {

        }
    }
}
