namespace MasonProtector
{
    partial class Builder
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Builder));
            this.MASON_panelOuterBorder = new System.Windows.Forms.Panel();
            this.MASON_panelWindowFrame = new System.Windows.Forms.Panel();
            this.MASON_panelClientArea = new System.Windows.Forms.Panel();
            this.MASON_panelContent = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.MASON_toggleStringEncryption = new System.Windows.Forms.CheckBox();
            this.MASON_toggleConstantsEncoding = new System.Windows.Forms.CheckBox();
            this.MASON_toggleIntEncoding = new System.Windows.Forms.CheckBox();
            this.MASON_toggleFieldEncryption = new System.Windows.Forms.CheckBox();
            this.MASON_toggleVMObfuscation = new System.Windows.Forms.CheckBox();
            this.MASON_togglePolymorphicEncryption = new System.Windows.Forms.CheckBox();
            this.MASON_toggleMutationEncoding = new System.Windows.Forms.CheckBox();
            this.MASON_toggleCrossReferenceEncryption = new System.Windows.Forms.CheckBox();
            this.MASON_toggleResourceProtection = new System.Windows.Forms.CheckBox();
            this.MASON_toggleStringComposition = new System.Windows.Forms.CheckBox();
            this.MASON_toggleRuntimeEncryption = new System.Windows.Forms.CheckBox();
            this.MASON_toggleArrayEncryption = new System.Windows.Forms.CheckBox();
            this.MASON_toggleDelegateEncryption = new System.Windows.Forms.CheckBox();
            this.MASON_toggleMethodBodyEncryption = new System.Windows.Forms.CheckBox();
            this.MASON_groupProtections = new System.Windows.Forms.GroupBox();
            this.MASON_toggleControlFlow = new System.Windows.Forms.CheckBox();
            this.MASON_toggleProxyCalls = new System.Windows.Forms.CheckBox();
            this.MASON_toggleCalliConversion = new System.Windows.Forms.CheckBox();
            this.MASON_toggleLocal2Field = new System.Windows.Forms.CheckBox();
            this.MASON_toggleOpaquePredicates = new System.Windows.Forms.CheckBox();
            this.MASON_toggleReferenceProxy = new System.Windows.Forms.CheckBox();
            this.MASON_toggleCallHiding = new System.Windows.Forms.CheckBox();
            this.MASON_toggleMethodScattering = new System.Windows.Forms.CheckBox();
            this.MASON_toggleControlFlowFlattening2 = new System.Windows.Forms.CheckBox();
            this.MASON_toggleStackUnderflow = new System.Windows.Forms.CheckBox();
            this.MASON_toggleBranchConfusion = new System.Windows.Forms.CheckBox();
            this.MASON_toggleNumericObfuscation = new System.Windows.Forms.CheckBox();
            this.MASON_toggleCodeVirtualization = new System.Windows.Forms.CheckBox();
            this.MASON_toggleJunkCode = new System.Windows.Forms.CheckBox();
            this.MASON_numJunkClasses = new System.Windows.Forms.NumericUpDown();
            this.MASON_groupRenamer = new System.Windows.Forms.GroupBox();
            this.MASON_txtRandomChars = new System.Windows.Forms.TextBox();
            this.MASON_lblChars = new System.Windows.Forms.Label();
            this.MASON_txtRenamePrefix = new System.Windows.Forms.TextBox();
            this.MASON_lblPrefix = new System.Windows.Forms.Label();
            this.MASON_numRenameLength = new System.Windows.Forms.NumericUpDown();
            this.MASON_lblRenameLength = new System.Windows.Forms.Label();
            this.MASON_toggleRenameEvents = new System.Windows.Forms.CheckBox();
            this.MASON_toggleRenameProperties = new System.Windows.Forms.CheckBox();
            this.MASON_toggleRenameFields = new System.Windows.Forms.CheckBox();
            this.MASON_toggleRenameMethods = new System.Windows.Forms.CheckBox();
            this.MASON_toggleRenameTypes = new System.Windows.Forms.CheckBox();
            this.MASON_toggleRenameNamespaces = new System.Windows.Forms.CheckBox();
            this.MASON_toggleEnableRenamer = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.MASON_toggleAntiTamper = new System.Windows.Forms.CheckBox();
            this.MASON_toggleAntiVM = new System.Windows.Forms.CheckBox();
            this.MASON_toggleAntiDump = new System.Windows.Forms.CheckBox();
            this.MASON_toggleAntiDebug = new System.Windows.Forms.CheckBox();
            this.MASON_toggleAntiDe4dot = new System.Windows.Forms.CheckBox();
            this.MASON_toggleAntiILDasm = new System.Windows.Forms.CheckBox();
            this.MASON_toggleAntiMemoryDump = new System.Windows.Forms.CheckBox();
            this.MASON_toggleAntiHook = new System.Windows.Forms.CheckBox();
            this.MASON_toggleAntiHttp = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.MASON_toggleHideMethods = new System.Windows.Forms.CheckBox();
            this.MASON_toggleFakeAttributes = new System.Windows.Forms.CheckBox();
            this.MASON_toggleWatermark = new System.Windows.Forms.CheckBox();
            this.MASON_toggleTokenConfusion = new System.Windows.Forms.CheckBox();
            this.MASON_toggleTypeScrambler = new System.Windows.Forms.CheckBox();
            this.MASON_toggleInvalidMetadata = new System.Windows.Forms.CheckBox();
            this.MASON_toggleDnSpyCrasher = new System.Windows.Forms.CheckBox();
            this.MASON_toggleEntryPointMover = new System.Windows.Forms.CheckBox();
            this.MASON_toggleMethodInliner = new System.Windows.Forms.CheckBox();
            this.MASON_toggleSelectAll = new System.Windows.Forms.CheckBox();
            this.MASON_lblStatus = new System.Windows.Forms.Label();
            this.MASON_progressBar = new System.Windows.Forms.ProgressBar();
            this.MASON_btnBuild = new System.Windows.Forms.Button();
            this.MASON_btnSelectFile = new System.Windows.Forms.Button();
            this.MASON_txtFilePath = new System.Windows.Forms.TextBox();
            this.MASON_panelTitleBar = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.MASON_btnClose = new System.Windows.Forms.Button();
            this.MASON_btnMaximize = new System.Windows.Forms.Button();
            this.MASON_btnMinimize = new System.Windows.Forms.Button();
            this.MASON_lblTitle = new System.Windows.Forms.Label();
            this.MASON_panelOuterBorder.SuspendLayout();
            this.MASON_panelWindowFrame.SuspendLayout();
            this.MASON_panelClientArea.SuspendLayout();
            this.MASON_panelContent.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.MASON_groupProtections.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MASON_numJunkClasses)).BeginInit();
            this.MASON_groupRenamer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MASON_numRenameLength)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.MASON_panelTitleBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // MASON_panelOuterBorder
            // 
            this.MASON_panelOuterBorder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(78)))), ((int)(((byte)(152)))));
            this.MASON_panelOuterBorder.Controls.Add(this.MASON_panelWindowFrame);
            this.MASON_panelOuterBorder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MASON_panelOuterBorder.Location = new System.Drawing.Point(0, 0);
            this.MASON_panelOuterBorder.Name = "MASON_panelOuterBorder";
            this.MASON_panelOuterBorder.Size = new System.Drawing.Size(544, 510);
            this.MASON_panelOuterBorder.TabIndex = 0;
            this.MASON_panelOuterBorder.Paint += new System.Windows.Forms.PaintEventHandler(this.MASON_panelOuterBorder_Paint);
            // 
            // MASON_panelWindowFrame
            // 
            this.MASON_panelWindowFrame.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(84)))), ((int)(((byte)(227)))));
            this.MASON_panelWindowFrame.Controls.Add(this.MASON_panelClientArea);
            this.MASON_panelWindowFrame.Controls.Add(this.MASON_panelTitleBar);
            this.MASON_panelWindowFrame.Location = new System.Drawing.Point(3, 3);
            this.MASON_panelWindowFrame.Name = "MASON_panelWindowFrame";
            this.MASON_panelWindowFrame.Padding = new System.Windows.Forms.Padding(4, 0, 4, 4);
            this.MASON_panelWindowFrame.Size = new System.Drawing.Size(538, 501);
            this.MASON_panelWindowFrame.TabIndex = 0;
            this.MASON_panelWindowFrame.Paint += new System.Windows.Forms.PaintEventHandler(this.MASON_panelWindowFrame_Paint);
            // 
            // MASON_panelClientArea
            // 
            this.MASON_panelClientArea.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(60)))), ((int)(((byte)(165)))));
            this.MASON_panelClientArea.Controls.Add(this.MASON_panelContent);
            this.MASON_panelClientArea.Location = new System.Drawing.Point(4, 30);
            this.MASON_panelClientArea.Name = "MASON_panelClientArea";
            this.MASON_panelClientArea.Padding = new System.Windows.Forms.Padding(3);
            this.MASON_panelClientArea.Size = new System.Drawing.Size(530, 469);
            this.MASON_panelClientArea.TabIndex = 1;
            this.MASON_panelClientArea.Paint += new System.Windows.Forms.PaintEventHandler(this.MASON_panelClientArea_Paint);
            // 
            // MASON_panelContent
            // 
            this.MASON_panelContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.MASON_panelContent.Controls.Add(this.groupBox2);
            this.MASON_panelContent.Controls.Add(this.MASON_groupProtections);
            this.MASON_panelContent.Controls.Add(this.MASON_groupRenamer);
            this.MASON_panelContent.Controls.Add(this.groupBox1);
            this.MASON_panelContent.Controls.Add(this.groupBox3);
            this.MASON_panelContent.Controls.Add(this.MASON_lblStatus);
            this.MASON_panelContent.Controls.Add(this.MASON_progressBar);
            this.MASON_panelContent.Controls.Add(this.MASON_btnBuild);
            this.MASON_panelContent.Controls.Add(this.MASON_btnSelectFile);
            this.MASON_panelContent.Controls.Add(this.MASON_txtFilePath);
            this.MASON_panelContent.Location = new System.Drawing.Point(3, 2);
            this.MASON_panelContent.Name = "MASON_panelContent";
            this.MASON_panelContent.Size = new System.Drawing.Size(524, 462);
            this.MASON_panelContent.TabIndex = 0;
            this.MASON_panelContent.Paint += new System.Windows.Forms.PaintEventHandler(this.MASON_panelContent_Paint);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.MASON_toggleStringEncryption);
            this.groupBox2.Controls.Add(this.MASON_toggleConstantsEncoding);
            this.groupBox2.Controls.Add(this.MASON_toggleIntEncoding);
            this.groupBox2.Controls.Add(this.MASON_toggleFieldEncryption);
            this.groupBox2.Controls.Add(this.MASON_toggleVMObfuscation);
            this.groupBox2.Controls.Add(this.MASON_togglePolymorphicEncryption);
            this.groupBox2.Controls.Add(this.MASON_toggleMutationEncoding);
            this.groupBox2.Controls.Add(this.MASON_toggleCrossReferenceEncryption);
            this.groupBox2.Controls.Add(this.MASON_toggleResourceProtection);
            this.groupBox2.Controls.Add(this.MASON_toggleStringComposition);
            this.groupBox2.Controls.Add(this.MASON_toggleRuntimeEncryption);
            this.groupBox2.Controls.Add(this.MASON_toggleArrayEncryption);
            this.groupBox2.Controls.Add(this.MASON_toggleDelegateEncryption);
            this.groupBox2.Controls.Add(this.MASON_toggleMethodBodyEncryption);
            this.groupBox2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.groupBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.groupBox2.Location = new System.Drawing.Point(10, 40);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(268, 165);
            this.groupBox2.TabIndex = 25;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = " Encryption ";
            // 
            // MASON_toggleStringEncryption
            // 
            this.MASON_toggleStringEncryption.AutoSize = true;
            this.MASON_toggleStringEncryption.Checked = true;
            this.MASON_toggleStringEncryption.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MASON_toggleStringEncryption.Cursor = System.Windows.Forms.Cursors.Cross;
            this.MASON_toggleStringEncryption.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.MASON_toggleStringEncryption.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.MASON_toggleStringEncryption.ForeColor = System.Drawing.Color.Black;
            this.MASON_toggleStringEncryption.Location = new System.Drawing.Point(12, 20);
            this.MASON_toggleStringEncryption.Name = "MASON_toggleStringEncryption";
            this.MASON_toggleStringEncryption.Size = new System.Drawing.Size(100, 18);
            this.MASON_toggleStringEncryption.TabIndex = 0;
            this.MASON_toggleStringEncryption.Text = "String Encrypt";
            // 
            // MASON_toggleConstantsEncoding
            // 
            this.MASON_toggleConstantsEncoding.AutoSize = true;
            this.MASON_toggleConstantsEncoding.Cursor = System.Windows.Forms.Cursors.Cross;
            this.MASON_toggleConstantsEncoding.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.MASON_toggleConstantsEncoding.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.MASON_toggleConstantsEncoding.ForeColor = System.Drawing.Color.Black;
            this.MASON_toggleConstantsEncoding.Location = new System.Drawing.Point(145, 20);
            this.MASON_toggleConstantsEncoding.Name = "MASON_toggleConstantsEncoding";
            this.MASON_toggleConstantsEncoding.Size = new System.Drawing.Size(101, 18);
            this.MASON_toggleConstantsEncoding.TabIndex = 1;
            this.MASON_toggleConstantsEncoding.Text = "Constants Enc";
            // 
            // MASON_toggleIntEncoding
            // 
            this.MASON_toggleIntEncoding.AutoSize = true;
            this.MASON_toggleIntEncoding.Checked = true;
            this.MASON_toggleIntEncoding.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MASON_toggleIntEncoding.Cursor = System.Windows.Forms.Cursors.Cross;
            this.MASON_toggleIntEncoding.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.MASON_toggleIntEncoding.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.MASON_toggleIntEncoding.ForeColor = System.Drawing.Color.Black;
            this.MASON_toggleIntEncoding.Location = new System.Drawing.Point(12, 40);
            this.MASON_toggleIntEncoding.Name = "MASON_toggleIntEncoding";
            this.MASON_toggleIntEncoding.Size = new System.Drawing.Size(92, 18);
            this.MASON_toggleIntEncoding.TabIndex = 2;
            this.MASON_toggleIntEncoding.Text = "Int Encoding";
            // 
            // MASON_toggleFieldEncryption
            // 
            this.MASON_toggleFieldEncryption.AutoSize = true;
            this.MASON_toggleFieldEncryption.Cursor = System.Windows.Forms.Cursors.Cross;
            this.MASON_toggleFieldEncryption.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.MASON_toggleFieldEncryption.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.MASON_toggleFieldEncryption.ForeColor = System.Drawing.Color.Black;
            this.MASON_toggleFieldEncryption.Location = new System.Drawing.Point(145, 40);
            this.MASON_toggleFieldEncryption.Name = "MASON_toggleFieldEncryption";
            this.MASON_toggleFieldEncryption.Size = new System.Drawing.Size(94, 18);
            this.MASON_toggleFieldEncryption.TabIndex = 3;
            this.MASON_toggleFieldEncryption.Text = "Field Encrypt";
            // 
            // MASON_toggleVMObfuscation
            // 
            this.MASON_toggleVMObfuscation.AutoSize = true;
            this.MASON_toggleVMObfuscation.Cursor = System.Windows.Forms.Cursors.Cross;
            this.MASON_toggleVMObfuscation.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.MASON_toggleVMObfuscation.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.MASON_toggleVMObfuscation.ForeColor = System.Drawing.Color.Black;
            this.MASON_toggleVMObfuscation.Location = new System.Drawing.Point(12, 60);
            this.MASON_toggleVMObfuscation.Name = "MASON_toggleVMObfuscation";
            this.MASON_toggleVMObfuscation.Size = new System.Drawing.Size(107, 18);
            this.MASON_toggleVMObfuscation.TabIndex = 4;
            this.MASON_toggleVMObfuscation.Text = "VM Obfuscation";
            // 
            // MASON_togglePolymorphicEncryption
            // 
            this.MASON_togglePolymorphicEncryption.AutoSize = true;
            this.MASON_togglePolymorphicEncryption.Cursor = System.Windows.Forms.Cursors.Cross;
            this.MASON_togglePolymorphicEncryption.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.MASON_togglePolymorphicEncryption.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.MASON_togglePolymorphicEncryption.ForeColor = System.Drawing.Color.Black;
            this.MASON_togglePolymorphicEncryption.Location = new System.Drawing.Point(145, 60);
            this.MASON_togglePolymorphicEncryption.Name = "MASON_togglePolymorphicEncryption";
            this.MASON_togglePolymorphicEncryption.Size = new System.Drawing.Size(109, 18);
            this.MASON_togglePolymorphicEncryption.TabIndex = 5;
            this.MASON_togglePolymorphicEncryption.Text = "Polymorphic Enc";
            // 
            // MASON_toggleMutationEncoding
            // 
            this.MASON_toggleMutationEncoding.Cursor = System.Windows.Forms.Cursors.Cross;
            this.MASON_toggleMutationEncoding.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.MASON_toggleMutationEncoding.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.MASON_toggleMutationEncoding.ForeColor = System.Drawing.Color.Black;
            this.MASON_toggleMutationEncoding.Location = new System.Drawing.Point(12, 80);
            this.MASON_toggleMutationEncoding.Name = "MASON_toggleMutationEncoding";
            this.MASON_toggleMutationEncoding.Size = new System.Drawing.Size(115, 18);
            this.MASON_toggleMutationEncoding.TabIndex = 6;
            this.MASON_toggleMutationEncoding.Text = "Mutation Encode";
            // 
            // MASON_toggleCrossReferenceEncryption
            // 
            this.MASON_toggleCrossReferenceEncryption.AutoSize = true;
            this.MASON_toggleCrossReferenceEncryption.Cursor = System.Windows.Forms.Cursors.Cross;
            this.MASON_toggleCrossReferenceEncryption.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.MASON_toggleCrossReferenceEncryption.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.MASON_toggleCrossReferenceEncryption.ForeColor = System.Drawing.Color.Black;
            this.MASON_toggleCrossReferenceEncryption.Location = new System.Drawing.Point(145, 80);
            this.MASON_toggleCrossReferenceEncryption.Name = "MASON_toggleCrossReferenceEncryption";
            this.MASON_toggleCrossReferenceEncryption.Size = new System.Drawing.Size(96, 18);
            this.MASON_toggleCrossReferenceEncryption.TabIndex = 7;
            this.MASON_toggleCrossReferenceEncryption.Text = "CrossRef Enc";
            // 
            // MASON_toggleResourceProtection
            // 
            this.MASON_toggleResourceProtection.AutoSize = true;
            this.MASON_toggleResourceProtection.Cursor = System.Windows.Forms.Cursors.Cross;
            this.MASON_toggleResourceProtection.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.MASON_toggleResourceProtection.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.MASON_toggleResourceProtection.ForeColor = System.Drawing.Color.Black;
            this.MASON_toggleResourceProtection.Location = new System.Drawing.Point(12, 100);
            this.MASON_toggleResourceProtection.Name = "MASON_toggleResourceProtection";
            this.MASON_toggleResourceProtection.Size = new System.Drawing.Size(132, 18);
            this.MASON_toggleResourceProtection.TabIndex = 8;
            this.MASON_toggleResourceProtection.Text = "Resources Compress";
            // 
            // MASON_toggleStringComposition
            // 
            this.MASON_toggleStringComposition.AutoSize = true;
            this.MASON_toggleStringComposition.Cursor = System.Windows.Forms.Cursors.Cross;
            this.MASON_toggleStringComposition.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.MASON_toggleStringComposition.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.MASON_toggleStringComposition.ForeColor = System.Drawing.Color.Black;
            this.MASON_toggleStringComposition.Location = new System.Drawing.Point(145, 100);
            this.MASON_toggleStringComposition.Name = "MASON_toggleStringComposition";
            this.MASON_toggleStringComposition.Size = new System.Drawing.Size(107, 18);
            this.MASON_toggleStringComposition.TabIndex = 9;
            this.MASON_toggleStringComposition.Text = "String Compose";
            // 
            // MASON_toggleRuntimeEncryption
            // 
            this.MASON_toggleRuntimeEncryption.Cursor = System.Windows.Forms.Cursors.Cross;
            this.MASON_toggleRuntimeEncryption.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.MASON_toggleRuntimeEncryption.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.MASON_toggleRuntimeEncryption.ForeColor = System.Drawing.Color.Black;
            this.MASON_toggleRuntimeEncryption.Location = new System.Drawing.Point(12, 120);
            this.MASON_toggleRuntimeEncryption.Name = "MASON_toggleRuntimeEncryption";
            this.MASON_toggleRuntimeEncryption.Size = new System.Drawing.Size(112, 18);
            this.MASON_toggleRuntimeEncryption.TabIndex = 10;
            this.MASON_toggleRuntimeEncryption.Text = "Runtime Encrypt";
            // 
            // MASON_toggleArrayEncryption
            // 
            this.MASON_toggleArrayEncryption.AutoSize = true;
            this.MASON_toggleArrayEncryption.Cursor = System.Windows.Forms.Cursors.Cross;
            this.MASON_toggleArrayEncryption.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.MASON_toggleArrayEncryption.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.MASON_toggleArrayEncryption.ForeColor = System.Drawing.Color.Black;
            this.MASON_toggleArrayEncryption.Location = new System.Drawing.Point(145, 120);
            this.MASON_toggleArrayEncryption.Name = "MASON_toggleArrayEncryption";
            this.MASON_toggleArrayEncryption.Size = new System.Drawing.Size(99, 18);
            this.MASON_toggleArrayEncryption.TabIndex = 11;
            this.MASON_toggleArrayEncryption.Text = "Array Encrypt";
            // 
            // MASON_toggleDelegateEncryption
            // 
            this.MASON_toggleDelegateEncryption.AutoSize = true;
            this.MASON_toggleDelegateEncryption.Cursor = System.Windows.Forms.Cursors.Cross;
            this.MASON_toggleDelegateEncryption.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.MASON_toggleDelegateEncryption.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.MASON_toggleDelegateEncryption.ForeColor = System.Drawing.Color.Black;
            this.MASON_toggleDelegateEncryption.Location = new System.Drawing.Point(12, 140);
            this.MASON_toggleDelegateEncryption.Name = "MASON_toggleDelegateEncryption";
            this.MASON_toggleDelegateEncryption.Size = new System.Drawing.Size(95, 18);
            this.MASON_toggleDelegateEncryption.TabIndex = 12;
            this.MASON_toggleDelegateEncryption.Text = "Delegate Enc";
            // 
            // MASON_toggleMethodBodyEncryption
            // 
            this.MASON_toggleMethodBodyEncryption.AutoSize = true;
            this.MASON_toggleMethodBodyEncryption.Cursor = System.Windows.Forms.Cursors.Cross;
            this.MASON_toggleMethodBodyEncryption.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.MASON_toggleMethodBodyEncryption.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.MASON_toggleMethodBodyEncryption.ForeColor = System.Drawing.Color.Black;
            this.MASON_toggleMethodBodyEncryption.Location = new System.Drawing.Point(145, 140);
            this.MASON_toggleMethodBodyEncryption.Name = "MASON_toggleMethodBodyEncryption";
            this.MASON_toggleMethodBodyEncryption.Size = new System.Drawing.Size(112, 18);
            this.MASON_toggleMethodBodyEncryption.TabIndex = 13;
            this.MASON_toggleMethodBodyEncryption.Text = "MethodBody Enc";
            // 
            // MASON_groupProtections
            // 
            this.MASON_groupProtections.BackColor = System.Drawing.Color.Transparent;
            this.MASON_groupProtections.Controls.Add(this.MASON_toggleControlFlow);
            this.MASON_groupProtections.Controls.Add(this.MASON_toggleProxyCalls);
            this.MASON_groupProtections.Controls.Add(this.MASON_toggleCalliConversion);
            this.MASON_groupProtections.Controls.Add(this.MASON_toggleLocal2Field);
            this.MASON_groupProtections.Controls.Add(this.MASON_toggleOpaquePredicates);
            this.MASON_groupProtections.Controls.Add(this.MASON_toggleReferenceProxy);
            this.MASON_groupProtections.Controls.Add(this.MASON_toggleCallHiding);
            this.MASON_groupProtections.Controls.Add(this.MASON_toggleMethodScattering);
            this.MASON_groupProtections.Controls.Add(this.MASON_toggleControlFlowFlattening2);
            this.MASON_groupProtections.Controls.Add(this.MASON_toggleStackUnderflow);
            this.MASON_groupProtections.Controls.Add(this.MASON_toggleBranchConfusion);
            this.MASON_groupProtections.Controls.Add(this.MASON_toggleNumericObfuscation);
            this.MASON_groupProtections.Controls.Add(this.MASON_toggleCodeVirtualization);
            this.MASON_groupProtections.Controls.Add(this.MASON_toggleJunkCode);
            this.MASON_groupProtections.Controls.Add(this.MASON_numJunkClasses);
            this.MASON_groupProtections.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.MASON_groupProtections.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.MASON_groupProtections.Location = new System.Drawing.Point(10, 206);
            this.MASON_groupProtections.Name = "MASON_groupProtections";
            this.MASON_groupProtections.Size = new System.Drawing.Size(268, 165);
            this.MASON_groupProtections.TabIndex = 3;
            this.MASON_groupProtections.TabStop = false;
            this.MASON_groupProtections.Text = " Obfuscation ";
            this.MASON_groupProtections.Paint += new System.Windows.Forms.PaintEventHandler(this.MASON_groupProtections_Paint);
            // 
            // MASON_toggleControlFlow
            // 
            this.MASON_toggleControlFlow.AutoSize = true;
            this.MASON_toggleControlFlow.Checked = true;
            this.MASON_toggleControlFlow.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MASON_toggleControlFlow.Cursor = System.Windows.Forms.Cursors.Cross;
            this.MASON_toggleControlFlow.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.MASON_toggleControlFlow.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.MASON_toggleControlFlow.ForeColor = System.Drawing.Color.Black;
            this.MASON_toggleControlFlow.Location = new System.Drawing.Point(11, 20);
            this.MASON_toggleControlFlow.Name = "MASON_toggleControlFlow";
            this.MASON_toggleControlFlow.Size = new System.Drawing.Size(92, 18);
            this.MASON_toggleControlFlow.TabIndex = 2;
            this.MASON_toggleControlFlow.Text = "Control Flow";
            // 
            // MASON_toggleProxyCalls
            // 
            this.MASON_toggleProxyCalls.AutoSize = true;
            this.MASON_toggleProxyCalls.Cursor = System.Windows.Forms.Cursors.Cross;
            this.MASON_toggleProxyCalls.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.MASON_toggleProxyCalls.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.MASON_toggleProxyCalls.ForeColor = System.Drawing.Color.Black;
            this.MASON_toggleProxyCalls.Location = new System.Drawing.Point(145, 20);
            this.MASON_toggleProxyCalls.Name = "MASON_toggleProxyCalls";
            this.MASON_toggleProxyCalls.Size = new System.Drawing.Size(85, 18);
            this.MASON_toggleProxyCalls.TabIndex = 16;
            this.MASON_toggleProxyCalls.Text = "Proxy Calls";
            // 
            // MASON_toggleCalliConversion
            // 
            this.MASON_toggleCalliConversion.Cursor = System.Windows.Forms.Cursors.Cross;
            this.MASON_toggleCalliConversion.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.MASON_toggleCalliConversion.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.MASON_toggleCalliConversion.ForeColor = System.Drawing.Color.Black;
            this.MASON_toggleCalliConversion.Location = new System.Drawing.Point(11, 40);
            this.MASON_toggleCalliConversion.Name = "MASON_toggleCalliConversion";
            this.MASON_toggleCalliConversion.Size = new System.Drawing.Size(115, 18);
            this.MASON_toggleCalliConversion.TabIndex = 21;
            this.MASON_toggleCalliConversion.Text = "Calli Conversion";
            // 
            // MASON_toggleLocal2Field
            // 
            this.MASON_toggleLocal2Field.Cursor = System.Windows.Forms.Cursors.Cross;
            this.MASON_toggleLocal2Field.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.MASON_toggleLocal2Field.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.MASON_toggleLocal2Field.ForeColor = System.Drawing.Color.Black;
            this.MASON_toggleLocal2Field.Location = new System.Drawing.Point(145, 40);
            this.MASON_toggleLocal2Field.Name = "MASON_toggleLocal2Field";
            this.MASON_toggleLocal2Field.Size = new System.Drawing.Size(100, 18);
            this.MASON_toggleLocal2Field.TabIndex = 22;
            this.MASON_toggleLocal2Field.Text = "Local to Field";
            // 
            // MASON_toggleOpaquePredicates
            // 
            this.MASON_toggleOpaquePredicates.AutoSize = true;
            this.MASON_toggleOpaquePredicates.Cursor = System.Windows.Forms.Cursors.Cross;
            this.MASON_toggleOpaquePredicates.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.MASON_toggleOpaquePredicates.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.MASON_toggleOpaquePredicates.ForeColor = System.Drawing.Color.Black;
            this.MASON_toggleOpaquePredicates.Location = new System.Drawing.Point(11, 60);
            this.MASON_toggleOpaquePredicates.Name = "MASON_toggleOpaquePredicates";
            this.MASON_toggleOpaquePredicates.Size = new System.Drawing.Size(95, 18);
            this.MASON_toggleOpaquePredicates.TabIndex = 30;
            this.MASON_toggleOpaquePredicates.Text = "Opaque Pred";
            // 
            // MASON_toggleReferenceProxy
            // 
            this.MASON_toggleReferenceProxy.AutoSize = true;
            this.MASON_toggleReferenceProxy.Cursor = System.Windows.Forms.Cursors.Cross;
            this.MASON_toggleReferenceProxy.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.MASON_toggleReferenceProxy.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.MASON_toggleReferenceProxy.ForeColor = System.Drawing.Color.Black;
            this.MASON_toggleReferenceProxy.Location = new System.Drawing.Point(145, 60);
            this.MASON_toggleReferenceProxy.Name = "MASON_toggleReferenceProxy";
            this.MASON_toggleReferenceProxy.Size = new System.Drawing.Size(80, 18);
            this.MASON_toggleReferenceProxy.TabIndex = 31;
            this.MASON_toggleReferenceProxy.Text = "Ref Proxy";
            // 
            // MASON_toggleCallHiding
            // 
            this.MASON_toggleCallHiding.AutoSize = true;
            this.MASON_toggleCallHiding.Cursor = System.Windows.Forms.Cursors.Cross;
            this.MASON_toggleCallHiding.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.MASON_toggleCallHiding.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.MASON_toggleCallHiding.ForeColor = System.Drawing.Color.Black;
            this.MASON_toggleCallHiding.Location = new System.Drawing.Point(11, 80);
            this.MASON_toggleCallHiding.Name = "MASON_toggleCallHiding";
            this.MASON_toggleCallHiding.Size = new System.Drawing.Size(81, 18);
            this.MASON_toggleCallHiding.TabIndex = 32;
            this.MASON_toggleCallHiding.Text = "Call Hiding";
            // 
            // MASON_toggleMethodScattering
            // 
            this.MASON_toggleMethodScattering.AutoSize = true;
            this.MASON_toggleMethodScattering.Cursor = System.Windows.Forms.Cursors.Cross;
            this.MASON_toggleMethodScattering.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.MASON_toggleMethodScattering.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.MASON_toggleMethodScattering.ForeColor = System.Drawing.Color.Black;
            this.MASON_toggleMethodScattering.Location = new System.Drawing.Point(145, 80);
            this.MASON_toggleMethodScattering.Name = "MASON_toggleMethodScattering";
            this.MASON_toggleMethodScattering.Size = new System.Drawing.Size(94, 18);
            this.MASON_toggleMethodScattering.TabIndex = 33;
            this.MASON_toggleMethodScattering.Text = "Meth Scatter";
            // 
            // MASON_toggleControlFlowFlattening2
            // 
            this.MASON_toggleControlFlowFlattening2.AutoSize = true;
            this.MASON_toggleControlFlowFlattening2.Cursor = System.Windows.Forms.Cursors.Cross;
            this.MASON_toggleControlFlowFlattening2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.MASON_toggleControlFlowFlattening2.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.MASON_toggleControlFlowFlattening2.ForeColor = System.Drawing.Color.Black;
            this.MASON_toggleControlFlowFlattening2.Location = new System.Drawing.Point(11, 100);
            this.MASON_toggleControlFlowFlattening2.Name = "MASON_toggleControlFlowFlattening2";
            this.MASON_toggleControlFlowFlattening2.Size = new System.Drawing.Size(88, 18);
            this.MASON_toggleControlFlowFlattening2.TabIndex = 34;
            this.MASON_toggleControlFlowFlattening2.Text = "CF Flatten2";
            // 
            // MASON_toggleStackUnderflow
            // 
            this.MASON_toggleStackUnderflow.AutoSize = true;
            this.MASON_toggleStackUnderflow.Cursor = System.Windows.Forms.Cursors.Cross;
            this.MASON_toggleStackUnderflow.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.MASON_toggleStackUnderflow.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.MASON_toggleStackUnderflow.ForeColor = System.Drawing.Color.Black;
            this.MASON_toggleStackUnderflow.Location = new System.Drawing.Point(145, 100);
            this.MASON_toggleStackUnderflow.Name = "MASON_toggleStackUnderflow";
            this.MASON_toggleStackUnderflow.Size = new System.Drawing.Size(110, 18);
            this.MASON_toggleStackUnderflow.TabIndex = 35;
            this.MASON_toggleStackUnderflow.Text = "Stack Underflow";
            // 
            // MASON_toggleBranchConfusion
            // 
            this.MASON_toggleBranchConfusion.AutoSize = true;
            this.MASON_toggleBranchConfusion.Cursor = System.Windows.Forms.Cursors.Cross;
            this.MASON_toggleBranchConfusion.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.MASON_toggleBranchConfusion.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.MASON_toggleBranchConfusion.ForeColor = System.Drawing.Color.Black;
            this.MASON_toggleBranchConfusion.Location = new System.Drawing.Point(11, 120);
            this.MASON_toggleBranchConfusion.Name = "MASON_toggleBranchConfusion";
            this.MASON_toggleBranchConfusion.Size = new System.Drawing.Size(108, 18);
            this.MASON_toggleBranchConfusion.TabIndex = 36;
            this.MASON_toggleBranchConfusion.Text = "Branch Confuse";
            // 
            // MASON_toggleNumericObfuscation
            // 
            this.MASON_toggleNumericObfuscation.AutoSize = true;
            this.MASON_toggleNumericObfuscation.Cursor = System.Windows.Forms.Cursors.Cross;
            this.MASON_toggleNumericObfuscation.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.MASON_toggleNumericObfuscation.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.MASON_toggleNumericObfuscation.ForeColor = System.Drawing.Color.Black;
            this.MASON_toggleNumericObfuscation.Location = new System.Drawing.Point(145, 120);
            this.MASON_toggleNumericObfuscation.Name = "MASON_toggleNumericObfuscation";
            this.MASON_toggleNumericObfuscation.Size = new System.Drawing.Size(107, 18);
            this.MASON_toggleNumericObfuscation.TabIndex = 37;
            this.MASON_toggleNumericObfuscation.Text = "Numeric Obfusc";
            // 
            // MASON_toggleCodeVirtualization
            // 
            this.MASON_toggleCodeVirtualization.AutoSize = true;
            this.MASON_toggleCodeVirtualization.Cursor = System.Windows.Forms.Cursors.Cross;
            this.MASON_toggleCodeVirtualization.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.MASON_toggleCodeVirtualization.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.MASON_toggleCodeVirtualization.ForeColor = System.Drawing.Color.Black;
            this.MASON_toggleCodeVirtualization.Location = new System.Drawing.Point(11, 140);
            this.MASON_toggleCodeVirtualization.Name = "MASON_toggleCodeVirtualization";
            this.MASON_toggleCodeVirtualization.Size = new System.Drawing.Size(90, 18);
            this.MASON_toggleCodeVirtualization.TabIndex = 38;
            this.MASON_toggleCodeVirtualization.Text = "Code Virtual";
            // 
            // MASON_toggleJunkCode
            // 
            this.MASON_toggleJunkCode.AutoSize = true;
            this.MASON_toggleJunkCode.BackColor = System.Drawing.Color.Transparent;
            this.MASON_toggleJunkCode.Cursor = System.Windows.Forms.Cursors.Cross;
            this.MASON_toggleJunkCode.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.MASON_toggleJunkCode.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.MASON_toggleJunkCode.ForeColor = System.Drawing.Color.Black;
            this.MASON_toggleJunkCode.Location = new System.Drawing.Point(145, 140);
            this.MASON_toggleJunkCode.Name = "MASON_toggleJunkCode";
            this.MASON_toggleJunkCode.Size = new System.Drawing.Size(54, 18);
            this.MASON_toggleJunkCode.TabIndex = 5;
            this.MASON_toggleJunkCode.Text = "Junk";
            this.MASON_toggleJunkCode.UseVisualStyleBackColor = false;
            // 
            // MASON_numJunkClasses
            // 
            this.MASON_numJunkClasses.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.MASON_numJunkClasses.Location = new System.Drawing.Point(199, 139);
            this.MASON_numJunkClasses.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.MASON_numJunkClasses.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.MASON_numJunkClasses.Name = "MASON_numJunkClasses";
            this.MASON_numJunkClasses.Size = new System.Drawing.Size(65, 21);
            this.MASON_numJunkClasses.TabIndex = 8;
            this.MASON_numJunkClasses.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // MASON_groupRenamer
            // 
            this.MASON_groupRenamer.BackColor = System.Drawing.Color.Transparent;
            this.MASON_groupRenamer.Controls.Add(this.MASON_txtRandomChars);
            this.MASON_groupRenamer.Controls.Add(this.MASON_lblChars);
            this.MASON_groupRenamer.Controls.Add(this.MASON_txtRenamePrefix);
            this.MASON_groupRenamer.Controls.Add(this.MASON_lblPrefix);
            this.MASON_groupRenamer.Controls.Add(this.MASON_numRenameLength);
            this.MASON_groupRenamer.Controls.Add(this.MASON_lblRenameLength);
            this.MASON_groupRenamer.Controls.Add(this.MASON_toggleRenameEvents);
            this.MASON_groupRenamer.Controls.Add(this.MASON_toggleRenameProperties);
            this.MASON_groupRenamer.Controls.Add(this.MASON_toggleRenameFields);
            this.MASON_groupRenamer.Controls.Add(this.MASON_toggleRenameMethods);
            this.MASON_groupRenamer.Controls.Add(this.MASON_toggleRenameTypes);
            this.MASON_groupRenamer.Controls.Add(this.MASON_toggleRenameNamespaces);
            this.MASON_groupRenamer.Controls.Add(this.MASON_toggleEnableRenamer);
            this.MASON_groupRenamer.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.MASON_groupRenamer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.MASON_groupRenamer.Location = new System.Drawing.Point(286, 40);
            this.MASON_groupRenamer.Name = "MASON_groupRenamer";
            this.MASON_groupRenamer.Size = new System.Drawing.Size(230, 158);
            this.MASON_groupRenamer.TabIndex = 4;
            this.MASON_groupRenamer.TabStop = false;
            this.MASON_groupRenamer.Text = " Renamer ";
            this.MASON_groupRenamer.Paint += new System.Windows.Forms.PaintEventHandler(this.MASON_groupRenamer_Paint);
            this.MASON_groupRenamer.Enter += new System.EventHandler(this.MASON_groupRenamer_Enter);
            // 
            // MASON_txtRandomChars
            // 
            this.MASON_txtRandomChars.Enabled = false;
            this.MASON_txtRandomChars.Font = new System.Drawing.Font("Tahoma", 7F);
            this.MASON_txtRandomChars.Location = new System.Drawing.Point(50, 131);
            this.MASON_txtRandomChars.Name = "MASON_txtRandomChars";
            this.MASON_txtRandomChars.Size = new System.Drawing.Size(166, 19);
            this.MASON_txtRandomChars.TabIndex = 12;
            this.MASON_txtRandomChars.Text = resources.GetString("MASON_txtRandomChars.Text");
            // 
            // MASON_lblChars
            // 
            this.MASON_lblChars.AutoSize = true;
            this.MASON_lblChars.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.MASON_lblChars.ForeColor = System.Drawing.Color.Black;
            this.MASON_lblChars.Location = new System.Drawing.Point(10, 134);
            this.MASON_lblChars.Name = "MASON_lblChars";
            this.MASON_lblChars.Size = new System.Drawing.Size(39, 13);
            this.MASON_lblChars.TabIndex = 11;
            this.MASON_lblChars.Text = "Chars:";
            // 
            // MASON_txtRenamePrefix
            // 
            this.MASON_txtRenamePrefix.Enabled = false;
            this.MASON_txtRenamePrefix.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.MASON_txtRenamePrefix.Location = new System.Drawing.Point(156, 101);
            this.MASON_txtRenamePrefix.Name = "MASON_txtRenamePrefix";
            this.MASON_txtRenamePrefix.Size = new System.Drawing.Size(60, 21);
            this.MASON_txtRenamePrefix.TabIndex = 10;
            this.MASON_txtRenamePrefix.Text = " $MASON~";
            // 
            // MASON_lblPrefix
            // 
            this.MASON_lblPrefix.AutoSize = true;
            this.MASON_lblPrefix.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.MASON_lblPrefix.ForeColor = System.Drawing.Color.Black;
            this.MASON_lblPrefix.Location = new System.Drawing.Point(115, 104);
            this.MASON_lblPrefix.Name = "MASON_lblPrefix";
            this.MASON_lblPrefix.Size = new System.Drawing.Size(39, 13);
            this.MASON_lblPrefix.TabIndex = 9;
            this.MASON_lblPrefix.Text = "Prefix:";
            // 
            // MASON_numRenameLength
            // 
            this.MASON_numRenameLength.Enabled = false;
            this.MASON_numRenameLength.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.MASON_numRenameLength.Location = new System.Drawing.Point(56, 101);
            this.MASON_numRenameLength.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.MASON_numRenameLength.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.MASON_numRenameLength.Name = "MASON_numRenameLength";
            this.MASON_numRenameLength.Size = new System.Drawing.Size(50, 21);
            this.MASON_numRenameLength.TabIndex = 8;
            this.MASON_numRenameLength.Value = new decimal(new int[] {
            12,
            0,
            0,
            0});
            // 
            // MASON_lblRenameLength
            // 
            this.MASON_lblRenameLength.AutoSize = true;
            this.MASON_lblRenameLength.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.MASON_lblRenameLength.ForeColor = System.Drawing.Color.Black;
            this.MASON_lblRenameLength.Location = new System.Drawing.Point(10, 104);
            this.MASON_lblRenameLength.Name = "MASON_lblRenameLength";
            this.MASON_lblRenameLength.Size = new System.Drawing.Size(44, 13);
            this.MASON_lblRenameLength.TabIndex = 7;
            this.MASON_lblRenameLength.Text = "Length:";
            // 
            // MASON_toggleRenameEvents
            // 
            this.MASON_toggleRenameEvents.AutoSize = true;
            this.MASON_toggleRenameEvents.Cursor = System.Windows.Forms.Cursors.Cross;
            this.MASON_toggleRenameEvents.Enabled = false;
            this.MASON_toggleRenameEvents.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.MASON_toggleRenameEvents.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.MASON_toggleRenameEvents.ForeColor = System.Drawing.Color.Black;
            this.MASON_toggleRenameEvents.Location = new System.Drawing.Point(136, 80);
            this.MASON_toggleRenameEvents.Name = "MASON_toggleRenameEvents";
            this.MASON_toggleRenameEvents.Size = new System.Drawing.Size(65, 18);
            this.MASON_toggleRenameEvents.TabIndex = 6;
            this.MASON_toggleRenameEvents.Text = "Events";
            // 
            // MASON_toggleRenameProperties
            // 
            this.MASON_toggleRenameProperties.AutoSize = true;
            this.MASON_toggleRenameProperties.Cursor = System.Windows.Forms.Cursors.Cross;
            this.MASON_toggleRenameProperties.Enabled = false;
            this.MASON_toggleRenameProperties.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.MASON_toggleRenameProperties.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.MASON_toggleRenameProperties.ForeColor = System.Drawing.Color.Black;
            this.MASON_toggleRenameProperties.Location = new System.Drawing.Point(136, 60);
            this.MASON_toggleRenameProperties.Name = "MASON_toggleRenameProperties";
            this.MASON_toggleRenameProperties.Size = new System.Drawing.Size(81, 18);
            this.MASON_toggleRenameProperties.TabIndex = 5;
            this.MASON_toggleRenameProperties.Text = "Properties";
            // 
            // MASON_toggleRenameFields
            // 
            this.MASON_toggleRenameFields.AutoSize = true;
            this.MASON_toggleRenameFields.Checked = true;
            this.MASON_toggleRenameFields.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MASON_toggleRenameFields.Cursor = System.Windows.Forms.Cursors.Cross;
            this.MASON_toggleRenameFields.Enabled = false;
            this.MASON_toggleRenameFields.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.MASON_toggleRenameFields.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.MASON_toggleRenameFields.ForeColor = System.Drawing.Color.Black;
            this.MASON_toggleRenameFields.Location = new System.Drawing.Point(136, 40);
            this.MASON_toggleRenameFields.Name = "MASON_toggleRenameFields";
            this.MASON_toggleRenameFields.Size = new System.Drawing.Size(59, 18);
            this.MASON_toggleRenameFields.TabIndex = 4;
            this.MASON_toggleRenameFields.Text = "Fields";
            // 
            // MASON_toggleRenameMethods
            // 
            this.MASON_toggleRenameMethods.AutoSize = true;
            this.MASON_toggleRenameMethods.Checked = true;
            this.MASON_toggleRenameMethods.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MASON_toggleRenameMethods.Cursor = System.Windows.Forms.Cursors.Cross;
            this.MASON_toggleRenameMethods.Enabled = false;
            this.MASON_toggleRenameMethods.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.MASON_toggleRenameMethods.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.MASON_toggleRenameMethods.ForeColor = System.Drawing.Color.Black;
            this.MASON_toggleRenameMethods.Location = new System.Drawing.Point(28, 80);
            this.MASON_toggleRenameMethods.Name = "MASON_toggleRenameMethods";
            this.MASON_toggleRenameMethods.Size = new System.Drawing.Size(73, 18);
            this.MASON_toggleRenameMethods.TabIndex = 3;
            this.MASON_toggleRenameMethods.Text = "Methods";
            // 
            // MASON_toggleRenameTypes
            // 
            this.MASON_toggleRenameTypes.AutoSize = true;
            this.MASON_toggleRenameTypes.Checked = true;
            this.MASON_toggleRenameTypes.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MASON_toggleRenameTypes.Cursor = System.Windows.Forms.Cursors.Cross;
            this.MASON_toggleRenameTypes.Enabled = false;
            this.MASON_toggleRenameTypes.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.MASON_toggleRenameTypes.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.MASON_toggleRenameTypes.ForeColor = System.Drawing.Color.Black;
            this.MASON_toggleRenameTypes.Location = new System.Drawing.Point(28, 60);
            this.MASON_toggleRenameTypes.Name = "MASON_toggleRenameTypes";
            this.MASON_toggleRenameTypes.Size = new System.Drawing.Size(61, 18);
            this.MASON_toggleRenameTypes.TabIndex = 2;
            this.MASON_toggleRenameTypes.Text = "Types";
            // 
            // MASON_toggleRenameNamespaces
            // 
            this.MASON_toggleRenameNamespaces.AutoSize = true;
            this.MASON_toggleRenameNamespaces.Checked = true;
            this.MASON_toggleRenameNamespaces.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MASON_toggleRenameNamespaces.Cursor = System.Windows.Forms.Cursors.Cross;
            this.MASON_toggleRenameNamespaces.Enabled = false;
            this.MASON_toggleRenameNamespaces.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.MASON_toggleRenameNamespaces.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.MASON_toggleRenameNamespaces.ForeColor = System.Drawing.Color.Black;
            this.MASON_toggleRenameNamespaces.Location = new System.Drawing.Point(28, 40);
            this.MASON_toggleRenameNamespaces.Name = "MASON_toggleRenameNamespaces";
            this.MASON_toggleRenameNamespaces.Size = new System.Drawing.Size(92, 18);
            this.MASON_toggleRenameNamespaces.TabIndex = 1;
            this.MASON_toggleRenameNamespaces.Text = "Namespaces";
            // 
            // MASON_toggleEnableRenamer
            // 
            this.MASON_toggleEnableRenamer.AutoSize = true;
            this.MASON_toggleEnableRenamer.Cursor = System.Windows.Forms.Cursors.Cross;
            this.MASON_toggleEnableRenamer.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.MASON_toggleEnableRenamer.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.MASON_toggleEnableRenamer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.MASON_toggleEnableRenamer.Location = new System.Drawing.Point(12, 22);
            this.MASON_toggleEnableRenamer.Name = "MASON_toggleEnableRenamer";
            this.MASON_toggleEnableRenamer.Size = new System.Drawing.Size(124, 18);
            this.MASON_toggleEnableRenamer.TabIndex = 0;
            this.MASON_toggleEnableRenamer.Text = "Enable Renamer";
            this.MASON_toggleEnableRenamer.CheckedChanged += new System.EventHandler(this.MASON_toggleEnableRenamer_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.MASON_toggleAntiTamper);
            this.groupBox1.Controls.Add(this.MASON_toggleAntiVM);
            this.groupBox1.Controls.Add(this.MASON_toggleAntiDump);
            this.groupBox1.Controls.Add(this.MASON_toggleAntiDebug);
            this.groupBox1.Controls.Add(this.MASON_toggleAntiDe4dot);
            this.groupBox1.Controls.Add(this.MASON_toggleAntiILDasm);
            this.groupBox1.Controls.Add(this.MASON_toggleAntiMemoryDump);
            this.groupBox1.Controls.Add(this.MASON_toggleAntiHook);
            this.groupBox1.Controls.Add(this.MASON_toggleAntiHttp);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.groupBox1.Location = new System.Drawing.Point(286, 199);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(230, 126);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = " Antis ";
            // 
            // MASON_toggleAntiTamper
            // 
            this.MASON_toggleAntiTamper.AutoSize = true;
            this.MASON_toggleAntiTamper.Cursor = System.Windows.Forms.Cursors.Cross;
            this.MASON_toggleAntiTamper.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.MASON_toggleAntiTamper.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.MASON_toggleAntiTamper.ForeColor = System.Drawing.Color.Black;
            this.MASON_toggleAntiTamper.Location = new System.Drawing.Point(6, 20);
            this.MASON_toggleAntiTamper.Name = "MASON_toggleAntiTamper";
            this.MASON_toggleAntiTamper.Size = new System.Drawing.Size(90, 18);
            this.MASON_toggleAntiTamper.TabIndex = 14;
            this.MASON_toggleAntiTamper.Text = "Anti Tamper";
            // 
            // MASON_toggleAntiVM
            // 
            this.MASON_toggleAntiVM.AutoSize = true;
            this.MASON_toggleAntiVM.Cursor = System.Windows.Forms.Cursors.Cross;
            this.MASON_toggleAntiVM.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.MASON_toggleAntiVM.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.MASON_toggleAntiVM.ForeColor = System.Drawing.Color.Black;
            this.MASON_toggleAntiVM.Location = new System.Drawing.Point(120, 20);
            this.MASON_toggleAntiVM.Name = "MASON_toggleAntiVM";
            this.MASON_toggleAntiVM.Size = new System.Drawing.Size(112, 18);
            this.MASON_toggleAntiVM.TabIndex = 6;
            this.MASON_toggleAntiVM.Text = "Anti Virt Machine";
            // 
            // MASON_toggleAntiDump
            // 
            this.MASON_toggleAntiDump.AutoSize = true;
            this.MASON_toggleAntiDump.Cursor = System.Windows.Forms.Cursors.Cross;
            this.MASON_toggleAntiDump.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.MASON_toggleAntiDump.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.MASON_toggleAntiDump.ForeColor = System.Drawing.Color.Black;
            this.MASON_toggleAntiDump.Location = new System.Drawing.Point(6, 40);
            this.MASON_toggleAntiDump.Name = "MASON_toggleAntiDump";
            this.MASON_toggleAntiDump.Size = new System.Drawing.Size(81, 18);
            this.MASON_toggleAntiDump.TabIndex = 13;
            this.MASON_toggleAntiDump.Text = "Anti Dump";
            // 
            // MASON_toggleAntiDebug
            // 
            this.MASON_toggleAntiDebug.AutoSize = true;
            this.MASON_toggleAntiDebug.Checked = true;
            this.MASON_toggleAntiDebug.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MASON_toggleAntiDebug.Cursor = System.Windows.Forms.Cursors.Cross;
            this.MASON_toggleAntiDebug.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.MASON_toggleAntiDebug.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.MASON_toggleAntiDebug.ForeColor = System.Drawing.Color.Black;
            this.MASON_toggleAntiDebug.Location = new System.Drawing.Point(120, 40);
            this.MASON_toggleAntiDebug.Name = "MASON_toggleAntiDebug";
            this.MASON_toggleAntiDebug.Size = new System.Drawing.Size(85, 18);
            this.MASON_toggleAntiDebug.TabIndex = 1;
            this.MASON_toggleAntiDebug.Text = "Anti Debug";
            // 
            // MASON_toggleAntiDe4dot
            // 
            this.MASON_toggleAntiDe4dot.AutoSize = true;
            this.MASON_toggleAntiDe4dot.Cursor = System.Windows.Forms.Cursors.Cross;
            this.MASON_toggleAntiDe4dot.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.MASON_toggleAntiDe4dot.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.MASON_toggleAntiDe4dot.ForeColor = System.Drawing.Color.Black;
            this.MASON_toggleAntiDe4dot.Location = new System.Drawing.Point(6, 60);
            this.MASON_toggleAntiDe4dot.Name = "MASON_toggleAntiDe4dot";
            this.MASON_toggleAntiDe4dot.Size = new System.Drawing.Size(89, 18);
            this.MASON_toggleAntiDe4dot.TabIndex = 15;
            this.MASON_toggleAntiDe4dot.Text = "Anti De4dot";
            // 
            // MASON_toggleAntiILDasm
            // 
            this.MASON_toggleAntiILDasm.AutoSize = true;
            this.MASON_toggleAntiILDasm.Cursor = System.Windows.Forms.Cursors.Cross;
            this.MASON_toggleAntiILDasm.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.MASON_toggleAntiILDasm.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.MASON_toggleAntiILDasm.ForeColor = System.Drawing.Color.Black;
            this.MASON_toggleAntiILDasm.Location = new System.Drawing.Point(120, 60);
            this.MASON_toggleAntiILDasm.Name = "MASON_toggleAntiILDasm";
            this.MASON_toggleAntiILDasm.Size = new System.Drawing.Size(89, 18);
            this.MASON_toggleAntiILDasm.TabIndex = 40;
            this.MASON_toggleAntiILDasm.Text = "Anti ILDasm";
            // 
            // MASON_toggleAntiMemoryDump
            // 
            this.MASON_toggleAntiMemoryDump.AutoSize = true;
            this.MASON_toggleAntiMemoryDump.Cursor = System.Windows.Forms.Cursors.Cross;
            this.MASON_toggleAntiMemoryDump.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.MASON_toggleAntiMemoryDump.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.MASON_toggleAntiMemoryDump.ForeColor = System.Drawing.Color.Black;
            this.MASON_toggleAntiMemoryDump.Location = new System.Drawing.Point(6, 80);
            this.MASON_toggleAntiMemoryDump.Name = "MASON_toggleAntiMemoryDump";
            this.MASON_toggleAntiMemoryDump.Size = new System.Drawing.Size(103, 18);
            this.MASON_toggleAntiMemoryDump.TabIndex = 41;
            this.MASON_toggleAntiMemoryDump.Text = "Anti MemDump";
            // 
            // MASON_toggleAntiHook
            // 
            this.MASON_toggleAntiHook.AutoSize = true;
            this.MASON_toggleAntiHook.Cursor = System.Windows.Forms.Cursors.Cross;
            this.MASON_toggleAntiHook.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.MASON_toggleAntiHook.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.MASON_toggleAntiHook.ForeColor = System.Drawing.Color.Black;
            this.MASON_toggleAntiHook.Location = new System.Drawing.Point(6, 100);
            this.MASON_toggleAntiHook.Name = "MASON_toggleAntiHook";
            this.MASON_toggleAntiHook.Size = new System.Drawing.Size(78, 18);
            this.MASON_toggleAntiHook.TabIndex = 60;
            this.MASON_toggleAntiHook.Text = "Anti Hook";
            // 
            // MASON_toggleAntiHttp
            // 
            this.MASON_toggleAntiHttp.AutoSize = true;
            this.MASON_toggleAntiHttp.Cursor = System.Windows.Forms.Cursors.Cross;
            this.MASON_toggleAntiHttp.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.MASON_toggleAntiHttp.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.MASON_toggleAntiHttp.ForeColor = System.Drawing.Color.Black;
            this.MASON_toggleAntiHttp.Location = new System.Drawing.Point(120, 80);
            this.MASON_toggleAntiHttp.Name = "MASON_toggleAntiHttp";
            this.MASON_toggleAntiHttp.Size = new System.Drawing.Size(75, 18);
            this.MASON_toggleAntiHttp.TabIndex = 61;
            this.MASON_toggleAntiHttp.Text = "Anti Http";
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.Transparent;
            this.groupBox3.Controls.Add(this.MASON_toggleHideMethods);
            this.groupBox3.Controls.Add(this.MASON_toggleFakeAttributes);
            this.groupBox3.Controls.Add(this.MASON_toggleWatermark);
            this.groupBox3.Controls.Add(this.MASON_toggleTokenConfusion);
            this.groupBox3.Controls.Add(this.MASON_toggleTypeScrambler);
            this.groupBox3.Controls.Add(this.MASON_toggleInvalidMetadata);
            this.groupBox3.Controls.Add(this.MASON_toggleDnSpyCrasher);
            this.groupBox3.Controls.Add(this.MASON_toggleEntryPointMover);
            this.groupBox3.Controls.Add(this.MASON_toggleMethodInliner);
            this.groupBox3.Controls.Add(this.MASON_toggleSelectAll);
            this.groupBox3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.groupBox3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.groupBox3.Location = new System.Drawing.Point(286, 326);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(230, 130);
            this.groupBox3.TabIndex = 50;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = " Stealth ";
            this.groupBox3.Paint += new System.Windows.Forms.PaintEventHandler(this.MASON_groupProtections_Paint);
            // 
            // MASON_toggleHideMethods
            // 
            this.MASON_toggleHideMethods.AutoSize = true;
            this.MASON_toggleHideMethods.Cursor = System.Windows.Forms.Cursors.Cross;
            this.MASON_toggleHideMethods.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.MASON_toggleHideMethods.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.MASON_toggleHideMethods.ForeColor = System.Drawing.Color.Black;
            this.MASON_toggleHideMethods.Location = new System.Drawing.Point(6, 20);
            this.MASON_toggleHideMethods.Name = "MASON_toggleHideMethods";
            this.MASON_toggleHideMethods.Size = new System.Drawing.Size(97, 18);
            this.MASON_toggleHideMethods.TabIndex = 17;
            this.MASON_toggleHideMethods.Text = "Hide Methods";
            // 
            // MASON_toggleFakeAttributes
            // 
            this.MASON_toggleFakeAttributes.AutoSize = true;
            this.MASON_toggleFakeAttributes.Cursor = System.Windows.Forms.Cursors.Cross;
            this.MASON_toggleFakeAttributes.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.MASON_toggleFakeAttributes.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.MASON_toggleFakeAttributes.ForeColor = System.Drawing.Color.Black;
            this.MASON_toggleFakeAttributes.Location = new System.Drawing.Point(120, 20);
            this.MASON_toggleFakeAttributes.Name = "MASON_toggleFakeAttributes";
            this.MASON_toggleFakeAttributes.Size = new System.Drawing.Size(82, 18);
            this.MASON_toggleFakeAttributes.TabIndex = 18;
            this.MASON_toggleFakeAttributes.Text = "Fake Attrs";
            // 
            // MASON_toggleWatermark
            // 
            this.MASON_toggleWatermark.AutoSize = true;
            this.MASON_toggleWatermark.Checked = true;
            this.MASON_toggleWatermark.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MASON_toggleWatermark.Cursor = System.Windows.Forms.Cursors.Cross;
            this.MASON_toggleWatermark.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.MASON_toggleWatermark.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.MASON_toggleWatermark.ForeColor = System.Drawing.Color.Black;
            this.MASON_toggleWatermark.Location = new System.Drawing.Point(6, 42);
            this.MASON_toggleWatermark.Name = "MASON_toggleWatermark";
            this.MASON_toggleWatermark.Size = new System.Drawing.Size(85, 18);
            this.MASON_toggleWatermark.TabIndex = 19;
            this.MASON_toggleWatermark.Text = "Watermark";
            // 
            // MASON_toggleTokenConfusion
            // 
            this.MASON_toggleTokenConfusion.AutoSize = true;
            this.MASON_toggleTokenConfusion.Cursor = System.Windows.Forms.Cursors.Cross;
            this.MASON_toggleTokenConfusion.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.MASON_toggleTokenConfusion.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.MASON_toggleTokenConfusion.ForeColor = System.Drawing.Color.Black;
            this.MASON_toggleTokenConfusion.Location = new System.Drawing.Point(120, 42);
            this.MASON_toggleTokenConfusion.Name = "MASON_toggleTokenConfusion";
            this.MASON_toggleTokenConfusion.Size = new System.Drawing.Size(104, 18);
            this.MASON_toggleTokenConfusion.TabIndex = 42;
            this.MASON_toggleTokenConfusion.Text = "Token Confuse";
            // 
            // MASON_toggleTypeScrambler
            // 
            this.MASON_toggleTypeScrambler.AutoSize = true;
            this.MASON_toggleTypeScrambler.Cursor = System.Windows.Forms.Cursors.Cross;
            this.MASON_toggleTypeScrambler.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.MASON_toggleTypeScrambler.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.MASON_toggleTypeScrambler.ForeColor = System.Drawing.Color.Black;
            this.MASON_toggleTypeScrambler.Location = new System.Drawing.Point(6, 64);
            this.MASON_toggleTypeScrambler.Name = "MASON_toggleTypeScrambler";
            this.MASON_toggleTypeScrambler.Size = new System.Drawing.Size(106, 18);
            this.MASON_toggleTypeScrambler.TabIndex = 43;
            this.MASON_toggleTypeScrambler.Text = "Type Scrambler";
            // 
            // MASON_toggleInvalidMetadata
            // 
            this.MASON_toggleInvalidMetadata.AutoSize = true;
            this.MASON_toggleInvalidMetadata.Cursor = System.Windows.Forms.Cursors.Cross;
            this.MASON_toggleInvalidMetadata.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.MASON_toggleInvalidMetadata.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.MASON_toggleInvalidMetadata.ForeColor = System.Drawing.Color.Black;
            this.MASON_toggleInvalidMetadata.Location = new System.Drawing.Point(120, 64);
            this.MASON_toggleInvalidMetadata.Name = "MASON_toggleInvalidMetadata";
            this.MASON_toggleInvalidMetadata.Size = new System.Drawing.Size(91, 18);
            this.MASON_toggleInvalidMetadata.TabIndex = 44;
            this.MASON_toggleInvalidMetadata.Text = "Invalid Meta";
            // 
            // MASON_toggleDnSpyCrasher
            // 
            this.MASON_toggleDnSpyCrasher.AutoSize = true;
            this.MASON_toggleDnSpyCrasher.Cursor = System.Windows.Forms.Cursors.Cross;
            this.MASON_toggleDnSpyCrasher.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.MASON_toggleDnSpyCrasher.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.MASON_toggleDnSpyCrasher.ForeColor = System.Drawing.Color.Black;
            this.MASON_toggleDnSpyCrasher.Location = new System.Drawing.Point(6, 86);
            this.MASON_toggleDnSpyCrasher.Name = "MASON_toggleDnSpyCrasher";
            this.MASON_toggleDnSpyCrasher.Size = new System.Drawing.Size(94, 18);
            this.MASON_toggleDnSpyCrasher.TabIndex = 70;
            this.MASON_toggleDnSpyCrasher.Text = "DnSpy Crash";
            // 
            // MASON_toggleEntryPointMover
            // 
            this.MASON_toggleEntryPointMover.AutoSize = true;
            this.MASON_toggleEntryPointMover.Cursor = System.Windows.Forms.Cursors.Cross;
            this.MASON_toggleEntryPointMover.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.MASON_toggleEntryPointMover.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.MASON_toggleEntryPointMover.ForeColor = System.Drawing.Color.Black;
            this.MASON_toggleEntryPointMover.Location = new System.Drawing.Point(120, 86);
            this.MASON_toggleEntryPointMover.Name = "MASON_toggleEntryPointMover";
            this.MASON_toggleEntryPointMover.Size = new System.Drawing.Size(97, 18);
            this.MASON_toggleEntryPointMover.TabIndex = 71;
            this.MASON_toggleEntryPointMover.Text = "EntryPt Move";
            // 
            // MASON_toggleMethodInliner
            // 
            this.MASON_toggleMethodInliner.AutoSize = true;
            this.MASON_toggleMethodInliner.Cursor = System.Windows.Forms.Cursors.Cross;
            this.MASON_toggleMethodInliner.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.MASON_toggleMethodInliner.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.MASON_toggleMethodInliner.ForeColor = System.Drawing.Color.Black;
            this.MASON_toggleMethodInliner.Location = new System.Drawing.Point(6, 108);
            this.MASON_toggleMethodInliner.Name = "MASON_toggleMethodInliner";
            this.MASON_toggleMethodInliner.Size = new System.Drawing.Size(101, 18);
            this.MASON_toggleMethodInliner.TabIndex = 72;
            this.MASON_toggleMethodInliner.Text = "Method Inliner";
            // 
            // MASON_toggleSelectAll
            // 
            this.MASON_toggleSelectAll.Cursor = System.Windows.Forms.Cursors.Cross;
            this.MASON_toggleSelectAll.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.MASON_toggleSelectAll.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.MASON_toggleSelectAll.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.MASON_toggleSelectAll.Location = new System.Drawing.Point(120, 108);
            this.MASON_toggleSelectAll.Name = "MASON_toggleSelectAll";
            this.MASON_toggleSelectAll.Size = new System.Drawing.Size(74, 18);
            this.MASON_toggleSelectAll.TabIndex = 24;
            this.MASON_toggleSelectAll.Text = "Select All";
            this.MASON_toggleSelectAll.CheckedChanged += new System.EventHandler(this.MASON_toggleSelectAll_CheckedChanged);
            // 
            // MASON_lblStatus
            // 
            this.MASON_lblStatus.Cursor = System.Windows.Forms.Cursors.Help;
            this.MASON_lblStatus.Font = new System.Drawing.Font("Tahoma", 7.25F, System.Drawing.FontStyle.Italic);
            this.MASON_lblStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.MASON_lblStatus.Location = new System.Drawing.Point(8, 370);
            this.MASON_lblStatus.Name = "MASON_lblStatus";
            this.MASON_lblStatus.Size = new System.Drawing.Size(270, 19);
            this.MASON_lblStatus.TabIndex = 5;
            this.MASON_lblStatus.Text = "Copyright © MasonGroup Battal ,Turki";
            this.MASON_lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.MASON_lblStatus.Click += new System.EventHandler(this.MASON_lblStatus_Click);
            // 
            // MASON_progressBar
            // 
            this.MASON_progressBar.Location = new System.Drawing.Point(10, 392);
            this.MASON_progressBar.Name = "MASON_progressBar";
            this.MASON_progressBar.Size = new System.Drawing.Size(268, 20);
            this.MASON_progressBar.TabIndex = 6;
            this.MASON_progressBar.Click += new System.EventHandler(this.MASON_progressBar_Click);
            // 
            // MASON_btnBuild
            // 
            this.MASON_btnBuild.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(242)))), ((int)(((byte)(237)))));
            this.MASON_btnBuild.Cursor = System.Windows.Forms.Cursors.Hand;
            this.MASON_btnBuild.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.MASON_btnBuild.Location = new System.Drawing.Point(10, 414);
            this.MASON_btnBuild.Name = "MASON_btnBuild";
            this.MASON_btnBuild.Size = new System.Drawing.Size(268, 42);
            this.MASON_btnBuild.TabIndex = 7;
            this.MASON_btnBuild.Text = "Protect";
            this.MASON_btnBuild.UseVisualStyleBackColor = false;
            this.MASON_btnBuild.Click += new System.EventHandler(this.MASON_btnBuild_Click);
            // 
            // MASON_btnSelectFile
            // 
            this.MASON_btnSelectFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(242)))), ((int)(((byte)(237)))));
            this.MASON_btnSelectFile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.MASON_btnSelectFile.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.MASON_btnSelectFile.Location = new System.Drawing.Point(436, 9);
            this.MASON_btnSelectFile.Name = "MASON_btnSelectFile";
            this.MASON_btnSelectFile.Size = new System.Drawing.Size(80, 23);
            this.MASON_btnSelectFile.TabIndex = 2;
            this.MASON_btnSelectFile.Text = "Browse...";
            this.MASON_btnSelectFile.UseVisualStyleBackColor = false;
            this.MASON_btnSelectFile.Click += new System.EventHandler(this.MASON_btnSelectFile_Click);
            // 
            // MASON_txtFilePath
            // 
            this.MASON_txtFilePath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(242)))), ((int)(((byte)(237)))));
            this.MASON_txtFilePath.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.MASON_txtFilePath.Location = new System.Drawing.Point(10, 10);
            this.MASON_txtFilePath.Name = "MASON_txtFilePath";
            this.MASON_txtFilePath.ReadOnly = true;
            this.MASON_txtFilePath.Size = new System.Drawing.Size(420, 21);
            this.MASON_txtFilePath.TabIndex = 1;
            this.MASON_txtFilePath.Tag = "";
            this.MASON_txtFilePath.Text = "Select your file or drag and drop";
            // 
            // MASON_panelTitleBar
            // 
            this.MASON_panelTitleBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(238)))));
            this.MASON_panelTitleBar.Controls.Add(this.pictureBox1);
            this.MASON_panelTitleBar.Controls.Add(this.MASON_btnClose);
            this.MASON_panelTitleBar.Controls.Add(this.MASON_btnMaximize);
            this.MASON_panelTitleBar.Controls.Add(this.MASON_btnMinimize);
            this.MASON_panelTitleBar.Controls.Add(this.MASON_lblTitle);
            this.MASON_panelTitleBar.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.MASON_panelTitleBar.Location = new System.Drawing.Point(-9, 0);
            this.MASON_panelTitleBar.Name = "MASON_panelTitleBar";
            this.MASON_panelTitleBar.Size = new System.Drawing.Size(554, 30);
            this.MASON_panelTitleBar.TabIndex = 0;
            this.MASON_panelTitleBar.Paint += new System.Windows.Forms.PaintEventHandler(this.MASON_PanelTitleBar_Paint);
            this.MASON_panelTitleBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MASON_PanelTitleBar_MouseDown);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Default;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(15, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(22, 20);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // MASON_btnClose
            // 
            this.MASON_btnClose.BackColor = System.Drawing.Color.Transparent;
            this.MASON_btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.MASON_btnClose.FlatAppearance.BorderSize = 0;
            this.MASON_btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MASON_btnClose.Font = new System.Drawing.Font("Marlett", 9F);
            this.MASON_btnClose.ForeColor = System.Drawing.Color.White;
            this.MASON_btnClose.Location = new System.Drawing.Point(524, 4);
            this.MASON_btnClose.Name = "MASON_btnClose";
            this.MASON_btnClose.Size = new System.Drawing.Size(21, 21);
            this.MASON_btnClose.TabIndex = 3;
            this.MASON_btnClose.Text = "r";
            this.MASON_btnClose.UseVisualStyleBackColor = false;
            this.MASON_btnClose.Click += new System.EventHandler(this.MASON_btnClose_Click);
            this.MASON_btnClose.Paint += new System.Windows.Forms.PaintEventHandler(this.MASON_ButtonCloseXP_Paint);
            // 
            // MASON_btnMaximize
            // 
            this.MASON_btnMaximize.BackColor = System.Drawing.Color.Transparent;
            this.MASON_btnMaximize.Cursor = System.Windows.Forms.Cursors.No;
            this.MASON_btnMaximize.Enabled = false;
            this.MASON_btnMaximize.FlatAppearance.BorderSize = 0;
            this.MASON_btnMaximize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MASON_btnMaximize.Font = new System.Drawing.Font("Marlett", 9F);
            this.MASON_btnMaximize.ForeColor = System.Drawing.Color.White;
            this.MASON_btnMaximize.Location = new System.Drawing.Point(501, 4);
            this.MASON_btnMaximize.Name = "MASON_btnMaximize";
            this.MASON_btnMaximize.Size = new System.Drawing.Size(21, 21);
            this.MASON_btnMaximize.TabIndex = 2;
            this.MASON_btnMaximize.Text = "1";
            this.MASON_btnMaximize.UseVisualStyleBackColor = false;
            this.MASON_btnMaximize.Click += new System.EventHandler(this.MASON_btnMaximize_Click);
            this.MASON_btnMaximize.Paint += new System.Windows.Forms.PaintEventHandler(this.MASON_ButtonXP_Paint);
            // 
            // MASON_btnMinimize
            // 
            this.MASON_btnMinimize.BackColor = System.Drawing.Color.Transparent;
            this.MASON_btnMinimize.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.MASON_btnMinimize.FlatAppearance.BorderSize = 0;
            this.MASON_btnMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MASON_btnMinimize.Font = new System.Drawing.Font("Marlett", 9F);
            this.MASON_btnMinimize.ForeColor = System.Drawing.Color.White;
            this.MASON_btnMinimize.Location = new System.Drawing.Point(478, 4);
            this.MASON_btnMinimize.Name = "MASON_btnMinimize";
            this.MASON_btnMinimize.Size = new System.Drawing.Size(21, 21);
            this.MASON_btnMinimize.TabIndex = 1;
            this.MASON_btnMinimize.Text = "0";
            this.MASON_btnMinimize.UseVisualStyleBackColor = false;
            this.MASON_btnMinimize.Click += new System.EventHandler(this.MASON_btnMinimize_Click);
            this.MASON_btnMinimize.Paint += new System.Windows.Forms.PaintEventHandler(this.MASON_ButtonXP_Paint);
            // 
            // MASON_lblTitle
            // 
            this.MASON_lblTitle.AutoSize = true;
            this.MASON_lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.MASON_lblTitle.Cursor = System.Windows.Forms.Cursors.Default;
            this.MASON_lblTitle.Font = new System.Drawing.Font("Trebuchet MS", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MASON_lblTitle.ForeColor = System.Drawing.Color.White;
            this.MASON_lblTitle.Location = new System.Drawing.Point(37, 5);
            this.MASON_lblTitle.Name = "MASON_lblTitle";
            this.MASON_lblTitle.Size = new System.Drawing.Size(43, 20);
            this.MASON_lblTitle.TabIndex = 0;
            this.MASON_lblTitle.Text = "EIYM";
            // 
            // Builder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(78)))), ((int)(((byte)(152)))));
            this.ClientSize = new System.Drawing.Size(544, 510);
            this.Controls.Add(this.MASON_panelOuterBorder);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Builder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EIYM Protector v2.1";
            this.MASON_panelOuterBorder.ResumeLayout(false);
            this.MASON_panelWindowFrame.ResumeLayout(false);
            this.MASON_panelClientArea.ResumeLayout(false);
            this.MASON_panelContent.ResumeLayout(false);
            this.MASON_panelContent.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.MASON_groupProtections.ResumeLayout(false);
            this.MASON_groupProtections.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MASON_numJunkClasses)).EndInit();
            this.MASON_groupRenamer.ResumeLayout(false);
            this.MASON_groupRenamer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MASON_numRenameLength)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.MASON_panelTitleBar.ResumeLayout(false);
            this.MASON_panelTitleBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel MASON_panelOuterBorder;
        private System.Windows.Forms.Panel MASON_panelWindowFrame;
        private System.Windows.Forms.Panel MASON_panelTitleBar;
        private System.Windows.Forms.Panel MASON_panelClientArea;
        private System.Windows.Forms.Panel MASON_panelContent;
        private System.Windows.Forms.Label MASON_lblTitle;
        private System.Windows.Forms.Button MASON_btnMinimize;
        private System.Windows.Forms.Button MASON_btnMaximize;
        private System.Windows.Forms.Button MASON_btnClose;
        private System.Windows.Forms.TextBox MASON_txtFilePath;
        private System.Windows.Forms.Button MASON_btnSelectFile;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox MASON_toggleStringEncryption;
        private System.Windows.Forms.CheckBox MASON_toggleConstantsEncoding;
        private System.Windows.Forms.CheckBox MASON_toggleIntEncoding;
        private System.Windows.Forms.CheckBox MASON_toggleFieldEncryption;
        private System.Windows.Forms.CheckBox MASON_toggleVMObfuscation;
        private System.Windows.Forms.CheckBox MASON_togglePolymorphicEncryption;
        private System.Windows.Forms.CheckBox MASON_toggleMutationEncoding;
        private System.Windows.Forms.CheckBox MASON_toggleCrossReferenceEncryption;
        private System.Windows.Forms.CheckBox MASON_toggleResourceProtection;
        private System.Windows.Forms.CheckBox MASON_toggleStringComposition;
        private System.Windows.Forms.CheckBox MASON_toggleRuntimeEncryption;
        private System.Windows.Forms.CheckBox MASON_toggleArrayEncryption;
        private System.Windows.Forms.CheckBox MASON_toggleDelegateEncryption;
        private System.Windows.Forms.CheckBox MASON_toggleMethodBodyEncryption;
        private System.Windows.Forms.GroupBox MASON_groupProtections;
        private System.Windows.Forms.CheckBox MASON_toggleControlFlow;
        private System.Windows.Forms.CheckBox MASON_toggleProxyCalls;
        private System.Windows.Forms.CheckBox MASON_toggleCalliConversion;
        private System.Windows.Forms.CheckBox MASON_toggleLocal2Field;
        private System.Windows.Forms.CheckBox MASON_toggleOpaquePredicates;
        private System.Windows.Forms.CheckBox MASON_toggleReferenceProxy;
        private System.Windows.Forms.CheckBox MASON_toggleCallHiding;
        private System.Windows.Forms.CheckBox MASON_toggleMethodScattering;
        private System.Windows.Forms.CheckBox MASON_toggleControlFlowFlattening2;
        private System.Windows.Forms.CheckBox MASON_toggleStackUnderflow;
        private System.Windows.Forms.CheckBox MASON_toggleBranchConfusion;
        private System.Windows.Forms.CheckBox MASON_toggleNumericObfuscation;
        private System.Windows.Forms.CheckBox MASON_toggleCodeVirtualization;
        private System.Windows.Forms.CheckBox MASON_toggleJunkCode;
        private System.Windows.Forms.NumericUpDown MASON_numJunkClasses;
        private System.Windows.Forms.GroupBox MASON_groupRenamer;
        private System.Windows.Forms.CheckBox MASON_toggleEnableRenamer;
        private System.Windows.Forms.CheckBox MASON_toggleRenameNamespaces;
        private System.Windows.Forms.CheckBox MASON_toggleRenameTypes;
        private System.Windows.Forms.CheckBox MASON_toggleRenameMethods;
        private System.Windows.Forms.CheckBox MASON_toggleRenameFields;
        private System.Windows.Forms.CheckBox MASON_toggleRenameProperties;
        private System.Windows.Forms.CheckBox MASON_toggleRenameEvents;
        private System.Windows.Forms.Label MASON_lblRenameLength;
        private System.Windows.Forms.NumericUpDown MASON_numRenameLength;
        private System.Windows.Forms.Label MASON_lblPrefix;
        private System.Windows.Forms.TextBox MASON_txtRenamePrefix;
        private System.Windows.Forms.Label MASON_lblChars;
        private System.Windows.Forms.TextBox MASON_txtRandomChars;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox MASON_toggleAntiTamper;
        private System.Windows.Forms.CheckBox MASON_toggleAntiVM;
        private System.Windows.Forms.CheckBox MASON_toggleAntiDump;
        private System.Windows.Forms.CheckBox MASON_toggleAntiDebug;
        private System.Windows.Forms.CheckBox MASON_toggleAntiDe4dot;
        private System.Windows.Forms.CheckBox MASON_toggleAntiILDasm;
        private System.Windows.Forms.CheckBox MASON_toggleAntiMemoryDump;
        private System.Windows.Forms.CheckBox MASON_toggleAntiHook;
        private System.Windows.Forms.CheckBox MASON_toggleAntiHttp;
        private System.Windows.Forms.CheckBox MASON_toggleSelectAll;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox MASON_toggleHideMethods;
        private System.Windows.Forms.CheckBox MASON_toggleFakeAttributes;
        private System.Windows.Forms.CheckBox MASON_toggleWatermark;
        private System.Windows.Forms.CheckBox MASON_toggleTokenConfusion;
        private System.Windows.Forms.CheckBox MASON_toggleTypeScrambler;
        private System.Windows.Forms.CheckBox MASON_toggleInvalidMetadata;
        private System.Windows.Forms.CheckBox MASON_toggleDnSpyCrasher;
        private System.Windows.Forms.CheckBox MASON_toggleEntryPointMover;
        private System.Windows.Forms.CheckBox MASON_toggleMethodInliner;
        private System.Windows.Forms.Label MASON_lblStatus;
        private System.Windows.Forms.ProgressBar MASON_progressBar;
        private System.Windows.Forms.Button MASON_btnBuild;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}
