using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using MasonProtector.Core;

namespace MasonProtector
{
    public partial class Builder : Form
    {
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private string MASON_SelectedFilePath = string.Empty;
        private Obfuscation MASON_Obfuscator;

        public Builder()
        {
            InitializeComponent();
            MASON_InitializeStyles();
            MASON_Obfuscator = new Obfuscation();

            MASON_txtFilePath.AllowDrop = true;
            MASON_txtFilePath.DragEnter += MASON_txtFilePath_DragEnter;
            MASON_txtFilePath.DragDrop += MASON_txtFilePath_DragDrop;

            this.AllowDrop = true;
            this.DragEnter += MASON_txtFilePath_DragEnter;
            this.DragDrop += MASON_txtFilePath_DragDrop;
        }

        private void MASON_InitializeStyles()
        {
            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.UpdateStyles();
        }

        #region MASON_WINDOW_EVENTS

        private void MASON_PanelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, 0xA1, 0x2, 0);
            }
        }

        private void MASON_btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MASON_btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void MASON_btnMaximize_Click(object sender, EventArgs e)
        {
        }

        private void MASON_lblStatus_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/ALMASONYH");
        }

        #endregion

        #region MASON_FILE_SELECTION

        private void MASON_btnSelectFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog MASON_ofd = new OpenFileDialog())
            {
                MASON_ofd.Filter = "Executable Files (*.exe)|*.exe|DLL Files (*.dll)|*.dll|All Files (*.*)|*.*";
                if (MASON_ofd.ShowDialog() == DialogResult.OK)
                {
                    MASON_SelectedFilePath = MASON_ofd.FileName;
                    MASON_txtFilePath.Text = MASON_SelectedFilePath;
                    MASON_UpdateStatus("File selected");
                }
            }
        }

        private void MASON_txtFilePath_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files.Length > 0)
                {
                    string ext = Path.GetExtension(files[0]).ToLower();
                    if (ext == ".exe" || ext == ".dll")
                    {
                        e.Effect = DragDropEffects.Copy;
                        return;
                    }
                }
            }
            e.Effect = DragDropEffects.None;
        }

        private void MASON_txtFilePath_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files.Length > 0)
                {
                    string ext = Path.GetExtension(files[0]).ToLower();
                    if (ext == ".exe" || ext == ".dll")
                    {
                        MASON_SelectedFilePath = files[0];
                        MASON_txtFilePath.Text = MASON_SelectedFilePath;
                        MASON_UpdateStatus("File selected");
                    }
                }
            }
        }

        #endregion

        #region MASON_BUILD

        private void MASON_btnBuild_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(MASON_SelectedFilePath) || !File.Exists(MASON_SelectedFilePath))
            {
                MessageBox.Show("Please select a valid file!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (SaveFileDialog MASON_sfd = new SaveFileDialog())
            {
                MASON_sfd.Filter = "Executable Files (*.exe)|*.exe|DLL Files (*.dll)|*.dll";
                MASON_sfd.FileName = Path.GetFileNameWithoutExtension(MASON_SelectedFilePath) + "_protected" + Path.GetExtension(MASON_SelectedFilePath);

                if (MASON_sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        MASON_btnBuild.Enabled = false;

                        var settings = BuildSettings();

                        MASON_Obfuscator.PerformProtection(
                            MASON_SelectedFilePath,
                            MASON_sfd.FileName,
                            settings,
                            MASON_UpdateStatus,
                            MASON_UpdateProgress
                        );

                        MessageBox.Show("Protection completed!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception MASON_ex)
                    {
                        MessageBox.Show("Error:\n" + MASON_ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        MASON_btnBuild.Enabled = true;
                        MASON_progressBar.Value = 0;
                        MASON_UpdateStatus("Copyright © MasonGroup Battal ,Turki");
                    }
                }
            }
        }

        #endregion

        #region MASON_SETTINGS_BUILDER

        private ProtectionSettings BuildSettings()
        {
            return new ProtectionSettings
            {
                EnableRenamer = MASON_toggleEnableRenamer.Checked,
                RenameNamespaces = MASON_toggleRenameNamespaces.Checked,
                RenameTypes = MASON_toggleRenameTypes.Checked,
                RenameMethods = MASON_toggleRenameMethods.Checked,
                RenameFields = MASON_toggleRenameFields.Checked,
                RenameProperties = MASON_toggleRenameProperties.Checked,
                RenameEvents = MASON_toggleRenameEvents.Checked,
                RenameLength = (int)MASON_numRenameLength.Value,
                RenamePrefix = MASON_txtRenamePrefix.Text,
                RandomChars = MASON_txtRandomChars.Text,
                StringEncryption = MASON_toggleStringEncryption.Checked,
                ConstantsEncoding = MASON_toggleConstantsEncoding.Checked,
                IntEncoding = MASON_toggleIntEncoding.Checked,
                FieldEncryption = MASON_toggleFieldEncryption.Checked,
                VMObfuscation = MASON_toggleVMObfuscation.Checked,
                PolymorphicEncryption = MASON_togglePolymorphicEncryption.Checked,
                MutationEncoding = MASON_toggleMutationEncoding.Checked,
                CrossReferenceEncryption = MASON_toggleCrossReferenceEncryption.Checked,
                ResourceProtection = MASON_toggleResourceProtection.Checked,
                StringComposition = MASON_toggleStringComposition.Checked,
                RuntimeEncryption = MASON_toggleRuntimeEncryption.Checked,
                ArrayEncryption = MASON_toggleArrayEncryption.Checked,
                DelegateEncryption = MASON_toggleDelegateEncryption.Checked,
                MethodBodyEncryption = MASON_toggleMethodBodyEncryption.Checked,
                ControlFlow = MASON_toggleControlFlow.Checked,
                ProxyCalls = MASON_toggleProxyCalls.Checked,
                CalliConversion = MASON_toggleCalliConversion.Checked,
                Local2Field = MASON_toggleLocal2Field.Checked,
                OpaquePredicates = MASON_toggleOpaquePredicates.Checked,
                ReferenceProxy = MASON_toggleReferenceProxy.Checked,
                CallHiding = MASON_toggleCallHiding.Checked,
                MethodScattering = MASON_toggleMethodScattering.Checked,
                ControlFlowFlattening2 = MASON_toggleControlFlowFlattening2.Checked,
                StackUnderflow = MASON_toggleStackUnderflow.Checked,
                BranchConfusion = MASON_toggleBranchConfusion.Checked,
                NumericObfuscation = MASON_toggleNumericObfuscation.Checked,
                CodeVirtualization = MASON_toggleCodeVirtualization.Checked,
                JunkCode = MASON_toggleJunkCode.Checked,
                JunkCount = (int)MASON_numJunkClasses.Value,
                AntiDebug = MASON_toggleAntiDebug.Checked,
                AntiVM = MASON_toggleAntiVM.Checked,
                AntiDump = MASON_toggleAntiDump.Checked,
                AntiTamper = MASON_toggleAntiTamper.Checked,
                AntiDe4dot = MASON_toggleAntiDe4dot.Checked,
                AntiILDasm = MASON_toggleAntiILDasm.Checked,
                AntiMemoryDump = MASON_toggleAntiMemoryDump.Checked,
                AntiHook = MASON_toggleAntiHook.Checked,
                AntiHttp = MASON_toggleAntiHttp.Checked,
                HideMethods = MASON_toggleHideMethods.Checked,
                FakeAttributes = MASON_toggleFakeAttributes.Checked,
                Watermark = MASON_toggleWatermark.Checked,
                TokenConfusion = MASON_toggleTokenConfusion.Checked,
                TypeScrambler = MASON_toggleTypeScrambler.Checked,
                InvalidMetadata = MASON_toggleInvalidMetadata.Checked,
                DnSpyCrasher = MASON_toggleDnSpyCrasher.Checked,
                EntryPointMover = MASON_toggleEntryPointMover.Checked,
                MethodInliner = MASON_toggleMethodInliner.Checked
            };
        }

        #endregion

        #region MASON_RENAMER_TOGGLE

        private void MASON_toggleEnableRenamer_CheckedChanged(object sender, EventArgs e)
        {
            bool MASON_enabled = MASON_toggleEnableRenamer.Checked;
            MASON_toggleRenameNamespaces.Enabled = MASON_enabled;
            MASON_toggleRenameTypes.Enabled = MASON_enabled;
            MASON_toggleRenameMethods.Enabled = MASON_enabled;
            MASON_toggleRenameFields.Enabled = MASON_enabled;
            MASON_toggleRenameProperties.Enabled = MASON_enabled;
            MASON_toggleRenameEvents.Enabled = MASON_enabled;
            MASON_txtRenamePrefix.Enabled = MASON_enabled;
            MASON_txtRandomChars.Enabled = MASON_enabled;
            MASON_numRenameLength.Enabled = MASON_enabled;
        }

        #endregion

        #region MASON_HELPER_METHODS

        private void MASON_UpdateStatus(string MASON_status)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => MASON_UpdateStatus(MASON_status)));
                return;
            }
            MASON_lblStatus.Text = MASON_status;
            Application.DoEvents();
        }

        private void MASON_UpdateProgress(int MASON_value)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => MASON_UpdateProgress(MASON_value)));
                return;
            }
            MASON_progressBar.Value = Math.Min(MASON_value, 100);
            Application.DoEvents();
        }

        #endregion

        #region MASON_PAINT_EVENTS

        private void MASON_panelOuterBorder_Paint(object sender, PaintEventArgs e)
        {
            Design.DrawOuterBorder(sender as Panel, e.Graphics);
        }

        private void MASON_panelWindowFrame_Paint(object sender, PaintEventArgs e)
        {
            Design.DrawWindowFrame(sender as Panel, e.Graphics);
        }

        private void MASON_PanelTitleBar_Paint(object sender, PaintEventArgs e)
        {
            Design.DrawTitleBar(sender as Panel, e.Graphics);
        }

        private void MASON_panelClientArea_Paint(object sender, PaintEventArgs e)
        {
            Design.DrawClientArea(sender as Panel, e.Graphics);
        }

        private void MASON_panelContent_Paint(object sender, PaintEventArgs e)
        {
            Design.DrawContent(sender as Panel, e.Graphics);
        }

        private void MASON_ButtonXP_Paint(object sender, PaintEventArgs e)
        {
            Design.DrawButtonXP(sender as Button, e.Graphics);
        }

        private void MASON_ButtonCloseXP_Paint(object sender, PaintEventArgs e)
        {
            Design.DrawButtonCloseXP(sender as Button, e.Graphics);
        }

        private void MASON_groupProtections_Paint(object sender, PaintEventArgs e)
        {
            Design.DrawXPGroupBox(sender as GroupBox, e.Graphics);
        }

        private void MASON_groupRenamer_Paint(object sender, PaintEventArgs e)
        {
            Design.DrawXPGroupBox(sender as GroupBox, e.Graphics);
        }

        #endregion

        private void MASON_toggleSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            bool MASON_checked = MASON_toggleSelectAll.Checked;
            MASON_toggleStringEncryption.Checked = MASON_checked;
            MASON_toggleConstantsEncoding.Checked = MASON_checked;
            MASON_toggleIntEncoding.Checked = MASON_checked;
            MASON_toggleFieldEncryption.Checked = MASON_checked;
            MASON_toggleVMObfuscation.Checked = MASON_checked;
            MASON_togglePolymorphicEncryption.Checked = MASON_checked;
            MASON_toggleMutationEncoding.Checked = MASON_checked;
            MASON_toggleCrossReferenceEncryption.Checked = MASON_checked;
            MASON_toggleResourceProtection.Checked = MASON_checked;
            MASON_toggleStringComposition.Checked = MASON_checked;
            MASON_toggleRuntimeEncryption.Checked = MASON_checked;
            MASON_toggleArrayEncryption.Checked = MASON_checked;
            MASON_toggleDelegateEncryption.Checked = MASON_checked;
            MASON_toggleMethodBodyEncryption.Checked = MASON_checked;
            MASON_toggleControlFlow.Checked = MASON_checked;
            MASON_toggleProxyCalls.Checked = MASON_checked;
            MASON_toggleCalliConversion.Checked = MASON_checked;
            MASON_toggleLocal2Field.Checked = MASON_checked;
            MASON_toggleOpaquePredicates.Checked = MASON_checked;
            MASON_toggleReferenceProxy.Checked = MASON_checked;
            MASON_toggleCallHiding.Checked = MASON_checked;
            MASON_toggleMethodScattering.Checked = MASON_checked;
            MASON_toggleControlFlowFlattening2.Checked = MASON_checked;
            MASON_toggleStackUnderflow.Checked = MASON_checked;
            MASON_toggleBranchConfusion.Checked = MASON_checked;
            MASON_toggleNumericObfuscation.Checked = MASON_checked;
            MASON_toggleCodeVirtualization.Checked = MASON_checked;
            MASON_toggleJunkCode.Checked = MASON_checked;
            MASON_toggleAntiDebug.Checked = MASON_checked;
            MASON_toggleAntiVM.Checked = MASON_checked;
            MASON_toggleAntiDump.Checked = MASON_checked;
            MASON_toggleAntiTamper.Checked = MASON_checked;
            MASON_toggleAntiDe4dot.Checked = MASON_checked;
            MASON_toggleAntiILDasm.Checked = MASON_checked;
            MASON_toggleAntiMemoryDump.Checked = MASON_checked;
            MASON_toggleAntiHook.Checked = MASON_checked;
            MASON_toggleAntiHttp.Checked = MASON_checked;
            MASON_toggleHideMethods.Checked = MASON_checked;
            MASON_toggleFakeAttributes.Checked = MASON_checked;
            MASON_toggleWatermark.Checked = MASON_checked;
            MASON_toggleTokenConfusion.Checked = MASON_checked;
            MASON_toggleTypeScrambler.Checked = MASON_checked;
            MASON_toggleInvalidMetadata.Checked = MASON_checked;
            MASON_toggleDnSpyCrasher.Checked = MASON_checked;
            MASON_toggleEntryPointMover.Checked = MASON_checked;
            MASON_toggleMethodInliner.Checked = MASON_checked;
            MASON_toggleEnableRenamer.Checked = MASON_checked;
            if (MASON_checked)
            {
                MASON_toggleRenameNamespaces.Checked = true;
                MASON_toggleRenameTypes.Checked = true;
                MASON_toggleRenameMethods.Checked = true;
                MASON_toggleRenameFields.Checked = true;
                MASON_toggleRenameProperties.Checked = true;
                MASON_toggleRenameEvents.Checked = true;
            }
        }

        private void MASON_groupRenamer_Enter(object sender, EventArgs e)
        {
        }

        private void MASON_progressBar_Click(object sender, EventArgs e)
        {

        }
    }
}
