namespace Neo.UI
{
    partial class InvokeContractParameterEditor
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
            this.comboTypeSelect = new System.Windows.Forms.ComboBox();
            this.lblType = new System.Windows.Forms.Label();
            this.txtParamValue = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.chkEncodeHex = new System.Windows.Forms.CheckBox();
            this.txtValuePreview = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.radioBtnFalse = new System.Windows.Forms.RadioButton();
            this.radioBtnTrue = new System.Windows.Forms.RadioButton();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboTypeSelect
            // 
            this.comboTypeSelect.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboTypeSelect.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboTypeSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboTypeSelect.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.comboTypeSelect.FormattingEnabled = true;
            this.comboTypeSelect.Items.AddRange(new object[] {
            "Array",
            "Boolean",
            "ByteArray",
            "Integer",
            "Hash160",
            "Hash256",
            "PublicKey",
            "Signature"});
            this.comboTypeSelect.Location = new System.Drawing.Point(84, 11);
            this.comboTypeSelect.Margin = new System.Windows.Forms.Padding(4);
            this.comboTypeSelect.Name = "comboTypeSelect";
            this.comboTypeSelect.Size = new System.Drawing.Size(244, 32);
            this.comboTypeSelect.TabIndex = 2;
            this.comboTypeSelect.SelectedIndexChanged += new System.EventHandler(this.comboTypeSelect_SelectIndexChanged);
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.lblType.Location = new System.Drawing.Point(15, 15);
            this.lblType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(56, 24);
            this.lblType.TabIndex = 3;
            this.lblType.Text = "Type:";
            // 
            // txtParamValue
            // 
            this.txtParamValue.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtParamValue.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.txtParamValue.Location = new System.Drawing.Point(3, 3);
            this.txtParamValue.Margin = new System.Windows.Forms.Padding(4);
            this.txtParamValue.Multiline = true;
            this.txtParamValue.Name = "txtParamValue";
            this.txtParamValue.Size = new System.Drawing.Size(518, 155);
            this.txtParamValue.TabIndex = 4;
            this.txtParamValue.TextChanged += new System.EventHandler(this.txtParamValue_TextChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.ItemSize = new System.Drawing.Size(0, 1);
            this.tabControl1.Location = new System.Drawing.Point(18, 47);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(532, 399);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 6;
            this.tabControl1.TabStop = false;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.chkEncodeHex);
            this.tabPage1.Controls.Add(this.txtValuePreview);
            this.tabPage1.Controls.Add(this.txtParamValue);
            this.tabPage1.Location = new System.Drawing.Point(4, 5);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(524, 390);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // chkEncodeHex
            // 
            this.chkEncodeHex.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.chkEncodeHex.AutoSize = true;
            this.chkEncodeHex.Location = new System.Drawing.Point(7, 360);
            this.chkEncodeHex.Name = "chkEncodeHex";
            this.chkEncodeHex.Size = new System.Drawing.Size(134, 26);
            this.chkEncodeHex.TabIndex = 6;
            this.chkEncodeHex.Text = "Hex Encode";
            this.chkEncodeHex.UseVisualStyleBackColor = true;
            this.chkEncodeHex.CheckedChanged += new System.EventHandler(this.chkEncodeHex_CheckChanged);
            // 
            // txtValuePreview
            // 
            this.txtValuePreview.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.txtValuePreview.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.txtValuePreview.Location = new System.Drawing.Point(5, 198);
            this.txtValuePreview.Margin = new System.Windows.Forms.Padding(4);
            this.txtValuePreview.Multiline = true;
            this.txtValuePreview.Name = "txtValuePreview";
            this.txtValuePreview.ReadOnly = true;
            this.txtValuePreview.Size = new System.Drawing.Size(512, 155);
            this.txtValuePreview.TabIndex = 5;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.radioBtnFalse);
            this.tabPage2.Controls.Add(this.radioBtnTrue);
            this.tabPage2.Location = new System.Drawing.Point(4, 5);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(524, 390);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // radioBtnFalse
            // 
            this.radioBtnFalse.AutoSize = true;
            this.radioBtnFalse.Location = new System.Drawing.Point(92, 17);
            this.radioBtnFalse.Name = "radioBtnFalse";
            this.radioBtnFalse.Size = new System.Drawing.Size(79, 26);
            this.radioBtnFalse.TabIndex = 1;
            this.radioBtnFalse.Text = "False";
            this.radioBtnFalse.UseVisualStyleBackColor = true;
            // 
            // radioBtnTrue
            // 
            this.radioBtnTrue.AutoSize = true;
            this.radioBtnTrue.Checked = true;
            this.radioBtnTrue.Location = new System.Drawing.Point(20, 17);
            this.radioBtnTrue.Name = "radioBtnTrue";
            this.radioBtnTrue.Size = new System.Drawing.Size(73, 26);
            this.radioBtnTrue.TabIndex = 0;
            this.radioBtnTrue.TabStop = true;
            this.radioBtnTrue.Text = "True";
            this.radioBtnTrue.UseVisualStyleBackColor = true;
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnAdd.Location = new System.Drawing.Point(363, 452);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(91, 36);
            this.btnAdd.TabIndex = 8;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(459, 452);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(91, 36);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // InvokeContractParameterEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(561, 498);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.lblType);
            this.Controls.Add(this.comboTypeSelect);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InvokeContractParameterEditor";
            this.Text = "Parameter Editor";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboTypeSelect;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.TextBox txtParamValue;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.RadioButton radioBtnFalse;
        private System.Windows.Forms.RadioButton radioBtnTrue;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtValuePreview;
        private System.Windows.Forms.CheckBox chkEncodeHex;
    }
}