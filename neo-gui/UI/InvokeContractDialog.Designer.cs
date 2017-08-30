namespace Neo.UI
{
    partial class InvokeContractDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InvokeContractDialog));
            this.lblParamList = new System.Windows.Forms.Label();
            this.lblAuthor = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.txtScriptHash = new System.Windows.Forms.TextBox();
            this.lblScriptHash = new System.Windows.Forms.Label();
            this.btnLoadAVM = new System.Windows.Forms.Button();
            this.txtCustomScript = new System.Windows.Forms.TextBox();
            this.btnInvoke = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnTestScript = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabInvoke = new System.Windows.Forms.TabPage();
            this.txtParamList = new System.Windows.Forms.TextBox();
            this.btnClearScript = new System.Windows.Forms.Button();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.txtAuthor = new System.Windows.Forms.TextBox();
            this.txtVersion = new System.Windows.Forms.TextBox();
            this.btnScriptHashSearch = new System.Windows.Forms.Button();
            this.tabCustom = new System.Windows.Forms.TabPage();
            this.txtCustomScriptHash = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCustomScriptCopy = new System.Windows.Forms.TextBox();
            this.paramListViewGroup = new System.Windows.Forms.GroupBox();
            this.treeParamList = new System.Windows.Forms.TreeView();
            this.menuParamListViewItem = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuItemEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1.SuspendLayout();
            this.tabInvoke.SuspendLayout();
            this.tabCustom.SuspendLayout();
            this.paramListViewGroup.SuspendLayout();
            this.menuParamListViewItem.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblParamList
            // 
            resources.ApplyResources(this.lblParamList, "lblParamList");
            this.lblParamList.Name = "lblParamList";
            // 
            // lblAuthor
            // 
            resources.ApplyResources(this.lblAuthor, "lblAuthor");
            this.lblAuthor.Name = "lblAuthor";
            // 
            // lblVersion
            // 
            resources.ApplyResources(this.lblVersion, "lblVersion");
            this.lblVersion.Name = "lblVersion";
            // 
            // txtName
            // 
            resources.ApplyResources(this.txtName, "txtName");
            this.txtName.Name = "txtName";
            this.txtName.ReadOnly = true;
            // 
            // lblName
            // 
            resources.ApplyResources(this.lblName, "lblName");
            this.lblName.Name = "lblName";
            // 
            // txtScriptHash
            // 
            resources.ApplyResources(this.txtScriptHash, "txtScriptHash");
            this.txtScriptHash.Name = "txtScriptHash";
            this.txtScriptHash.TextChanged += new System.EventHandler(this.txtScriptHash_TextChanged);
            // 
            // lblScriptHash
            // 
            resources.ApplyResources(this.lblScriptHash, "lblScriptHash");
            this.lblScriptHash.Name = "lblScriptHash";
            // 
            // btnLoadAVM
            // 
            resources.ApplyResources(this.btnLoadAVM, "btnLoadAVM");
            this.btnLoadAVM.Name = "btnLoadAVM";
            this.btnLoadAVM.UseVisualStyleBackColor = true;
            this.btnLoadAVM.Click += new System.EventHandler(this.btnLoadAVM_Click);
            // 
            // txtCustomScript
            // 
            resources.ApplyResources(this.txtCustomScript, "txtCustomScript");
            this.txtCustomScript.Name = "txtCustomScript";
            this.txtCustomScript.TextChanged += new System.EventHandler(this.txtCustomScript_TextChanged);
            // 
            // btnInvoke
            // 
            resources.ApplyResources(this.btnInvoke, "btnInvoke");
            this.btnInvoke.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnInvoke.Name = "btnInvoke";
            this.btnInvoke.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            resources.ApplyResources(this.button4, "button4");
            this.button4.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button4.Name = "button4";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // btnTestScript
            // 
            resources.ApplyResources(this.btnTestScript, "btnTestScript");
            this.btnTestScript.Name = "btnTestScript";
            this.btnTestScript.UseVisualStyleBackColor = true;
            this.btnTestScript.Click += new System.EventHandler(this.btnTestScript_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "avm";
            resources.ApplyResources(this.openFileDialog1, "openFileDialog1");
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabInvoke);
            this.tabControl1.Controls.Add(this.tabCustom);
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // tabInvoke
            // 
            this.tabInvoke.Controls.Add(this.txtParamList);
            this.tabInvoke.Controls.Add(this.btnClearScript);
            this.tabInvoke.Controls.Add(this.lblDescription);
            this.tabInvoke.Controls.Add(this.txtDescription);
            this.tabInvoke.Controls.Add(this.lblParamList);
            this.tabInvoke.Controls.Add(this.txtAuthor);
            this.tabInvoke.Controls.Add(this.txtVersion);
            this.tabInvoke.Controls.Add(this.lblAuthor);
            this.tabInvoke.Controls.Add(this.lblVersion);
            this.tabInvoke.Controls.Add(this.lblName);
            this.tabInvoke.Controls.Add(this.txtName);
            this.tabInvoke.Controls.Add(this.btnScriptHashSearch);
            this.tabInvoke.Controls.Add(this.txtScriptHash);
            this.tabInvoke.Controls.Add(this.lblScriptHash);
            resources.ApplyResources(this.tabInvoke, "tabInvoke");
            this.tabInvoke.Name = "tabInvoke";
            this.tabInvoke.UseVisualStyleBackColor = true;
            // 
            // txtParamList
            // 
            resources.ApplyResources(this.txtParamList, "txtParamList");
            this.txtParamList.Name = "txtParamList";
            this.txtParamList.ReadOnly = true;
            // 
            // btnClearScript
            // 
            resources.ApplyResources(this.btnClearScript, "btnClearScript");
            this.btnClearScript.Name = "btnClearScript";
            this.btnClearScript.UseVisualStyleBackColor = true;
            this.btnClearScript.Click += new System.EventHandler(this.btnClearScript_Click);
            // 
            // lblDescription
            // 
            resources.ApplyResources(this.lblDescription, "lblDescription");
            this.lblDescription.Name = "lblDescription";
            // 
            // txtDescription
            // 
            resources.ApplyResources(this.txtDescription, "txtDescription");
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ReadOnly = true;
            // 
            // txtAuthor
            // 
            resources.ApplyResources(this.txtAuthor, "txtAuthor");
            this.txtAuthor.Name = "txtAuthor";
            this.txtAuthor.ReadOnly = true;
            // 
            // txtVersion
            // 
            resources.ApplyResources(this.txtVersion, "txtVersion");
            this.txtVersion.Name = "txtVersion";
            this.txtVersion.ReadOnly = true;
            // 
            // btnScriptHashSearch
            // 
            resources.ApplyResources(this.btnScriptHashSearch, "btnScriptHashSearch");
            this.btnScriptHashSearch.Image = global::Neo.Properties.Resources.search;
            this.btnScriptHashSearch.Name = "btnScriptHashSearch";
            this.btnScriptHashSearch.UseVisualStyleBackColor = true;
            this.btnScriptHashSearch.Click += new System.EventHandler(this.btnScriptHashSearch_Click);
            // 
            // tabCustom
            // 
            this.tabCustom.Controls.Add(this.txtCustomScriptHash);
            this.tabCustom.Controls.Add(this.label1);
            this.tabCustom.Controls.Add(this.btnLoadAVM);
            this.tabCustom.Controls.Add(this.txtCustomScript);
            resources.ApplyResources(this.tabCustom, "tabCustom");
            this.tabCustom.Name = "tabCustom";
            this.tabCustom.UseVisualStyleBackColor = true;
            // 
            // txtCustomScriptHash
            // 
            resources.ApplyResources(this.txtCustomScriptHash, "txtCustomScriptHash");
            this.txtCustomScriptHash.Name = "txtCustomScriptHash";
            this.txtCustomScriptHash.ReadOnly = true;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // txtCustomScriptCopy
            // 
            resources.ApplyResources(this.txtCustomScriptCopy, "txtCustomScriptCopy");
            this.txtCustomScriptCopy.Name = "txtCustomScriptCopy";
            this.txtCustomScriptCopy.ReadOnly = true;
            // 
            // paramListViewGroup
            // 
            this.paramListViewGroup.Controls.Add(this.treeParamList);
            resources.ApplyResources(this.paramListViewGroup, "paramListViewGroup");
            this.paramListViewGroup.Name = "paramListViewGroup";
            this.paramListViewGroup.TabStop = false;
            // 
            // treeParamList
            // 
            resources.ApplyResources(this.treeParamList, "treeParamList");
            this.treeParamList.Name = "treeParamList";
            this.treeParamList.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeParamList_MouseDown);
            // 
            // menuParamListViewItem
            // 
            this.menuParamListViewItem.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuParamListViewItem.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemEdit,
            this.menuItemRemove});
            this.menuParamListViewItem.Name = "menuParamListViewItem";
            resources.ApplyResources(this.menuParamListViewItem, "menuParamListViewItem");
            // 
            // menuItemEdit
            // 
            this.menuItemEdit.Name = "menuItemEdit";
            resources.ApplyResources(this.menuItemEdit, "menuItemEdit");
            this.menuItemEdit.Click += new System.EventHandler(this.menuItemEdit_Click);
            // 
            // menuItemRemove
            // 
            this.menuItemRemove.Name = "menuItemRemove";
            resources.ApplyResources(this.menuItemRemove, "menuItemRemove");
            this.menuItemRemove.Click += new System.EventHandler(this.menuItemRemove_Click);
            // 
            // InvokeContractDialog
            // 
            this.AcceptButton = this.btnInvoke;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button4;
            this.Controls.Add(this.paramListViewGroup);
            this.Controls.Add(this.txtCustomScriptCopy);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnTestScript);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.btnInvoke);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InvokeContractDialog";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.tabControl1.ResumeLayout(false);
            this.tabInvoke.ResumeLayout(false);
            this.tabInvoke.PerformLayout();
            this.tabCustom.ResumeLayout(false);
            this.tabCustom.PerformLayout();
            this.paramListViewGroup.ResumeLayout(false);
            this.menuParamListViewItem.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblScriptHash;
        private System.Windows.Forms.TextBox txtScriptHash;
        private System.Windows.Forms.Button btnScriptHashSearch;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label lblAuthor;
        private System.Windows.Forms.Label lblParamList;
        private System.Windows.Forms.TextBox txtCustomScript;
        private System.Windows.Forms.Button btnInvoke;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnTestScript;
        private System.Windows.Forms.Button btnLoadAVM;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabInvoke;
        private System.Windows.Forms.TabPage tabCustom;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Button btnClearScript;
        private System.Windows.Forms.TextBox txtCustomScriptCopy;
        private System.Windows.Forms.GroupBox paramListViewGroup;
        private System.Windows.Forms.TextBox txtAuthor;
        private System.Windows.Forms.TextBox txtVersion;
        private System.Windows.Forms.TextBox txtParamList;
        private System.Windows.Forms.ContextMenuStrip menuParamListViewItem;
        private System.Windows.Forms.ToolStripMenuItem menuItemEdit;
        private System.Windows.Forms.ToolStripMenuItem menuItemRemove;
        private System.Windows.Forms.TextBox txtCustomScriptHash;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TreeView treeParamList;
    }
}