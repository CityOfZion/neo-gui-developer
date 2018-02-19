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
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.button8 = new System.Windows.Forms.Button();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.button7 = new System.Windows.Forms.Button();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.tabControl1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox1.SuspendLayout();
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
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // tabPage3
            // 
            resources.ApplyResources(this.tabPage3, "tabPage3");
            this.tabPage3.Controls.Add(this.button8);
            this.tabPage3.Controls.Add(this.textBox9);
            this.tabPage3.Controls.Add(this.label10);
            this.tabPage3.Controls.Add(this.comboBox1);
            this.tabPage3.Controls.Add(this.label9);
            this.tabPage3.Controls.Add(this.button7);
            this.tabPage3.Controls.Add(this.textBox8);
            this.tabPage3.Controls.Add(this.label8);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // button8
            // 
            resources.ApplyResources(this.button8, "button8");
            this.button8.Name = "button8";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // textBox9
            // 
            resources.ApplyResources(this.textBox9, "textBox9");
            this.textBox9.Name = "textBox9";
            this.textBox9.ReadOnly = true;
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // comboBox1
            // 
            resources.ApplyResources(this.comboBox1, "comboBox1");
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // button7
            // 
            resources.ApplyResources(this.button7, "button7");
            this.button7.Name = "button7";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // textBox8
            // 
            resources.ApplyResources(this.textBox8, "textBox8");
            this.textBox8.Name = "textBox8";
            this.textBox8.ReadOnly = true;
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // tabPage1
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
            // openFileDialog2
            // 
            resources.ApplyResources(this.openFileDialog2, "openFileDialog2");
            this.openFileDialog2.DefaultExt = "abi.json";
            // 
            // InvokeContractDialog
            // 
            this.AcceptButton = this.btnInvoke;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button4;
            this.Controls.Add(this.txtInvokeOutput);
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
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
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
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.OpenFileDialog openFileDialog2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.Label label10;
    }
}