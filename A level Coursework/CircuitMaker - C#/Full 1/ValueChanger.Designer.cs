namespace Full_1
{
    partial class ValueChanger
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
            this.textBoxResistance = new System.Windows.Forms.TextBox();
            this.textBoxVoltage = new System.Windows.Forms.TextBox();
            this.textBoxCurrent = new System.Windows.Forms.TextBox();
            this.labelResistance = new System.Windows.Forms.Label();
            this.labelVoltage = new System.Windows.Forms.Label();
            this.labelCurrent = new System.Windows.Forms.Label();
            this.buttonSet = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.comboBoxR = new System.Windows.Forms.ComboBox();
            this.comboBoxV = new System.Windows.Forms.ComboBox();
            this.comboBoxC = new System.Windows.Forms.ComboBox();
            this.buttonFill = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxResistance
            // 
            this.textBoxResistance.Location = new System.Drawing.Point(12, 27);
            this.textBoxResistance.Name = "textBoxResistance";
            this.textBoxResistance.Size = new System.Drawing.Size(205, 20);
            this.textBoxResistance.TabIndex = 0;
            // 
            // textBoxVoltage
            // 
            this.textBoxVoltage.Location = new System.Drawing.Point(12, 70);
            this.textBoxVoltage.Name = "textBoxVoltage";
            this.textBoxVoltage.Size = new System.Drawing.Size(205, 20);
            this.textBoxVoltage.TabIndex = 1;
            // 
            // textBoxCurrent
            // 
            this.textBoxCurrent.Location = new System.Drawing.Point(12, 111);
            this.textBoxCurrent.Name = "textBoxCurrent";
            this.textBoxCurrent.Size = new System.Drawing.Size(205, 20);
            this.textBoxCurrent.TabIndex = 2;
            // 
            // labelResistance
            // 
            this.labelResistance.AutoSize = true;
            this.labelResistance.Location = new System.Drawing.Point(12, 9);
            this.labelResistance.Name = "labelResistance";
            this.labelResistance.Size = new System.Drawing.Size(60, 13);
            this.labelResistance.TabIndex = 3;
            this.labelResistance.Text = "Resistance";
            // 
            // labelVoltage
            // 
            this.labelVoltage.AutoSize = true;
            this.labelVoltage.Location = new System.Drawing.Point(12, 52);
            this.labelVoltage.Name = "labelVoltage";
            this.labelVoltage.Size = new System.Drawing.Size(43, 13);
            this.labelVoltage.TabIndex = 4;
            this.labelVoltage.Text = "Voltage";
            // 
            // labelCurrent
            // 
            this.labelCurrent.AutoSize = true;
            this.labelCurrent.Location = new System.Drawing.Point(12, 95);
            this.labelCurrent.Name = "labelCurrent";
            this.labelCurrent.Size = new System.Drawing.Size(41, 13);
            this.labelCurrent.TabIndex = 5;
            this.labelCurrent.Text = "Current";
            // 
            // buttonSet
            // 
            this.buttonSet.Location = new System.Drawing.Point(9, 139);
            this.buttonSet.Name = "buttonSet";
            this.buttonSet.Size = new System.Drawing.Size(81, 27);
            this.buttonSet.TabIndex = 6;
            this.buttonSet.Text = "Set";
            this.buttonSet.UseVisualStyleBackColor = true;
            this.buttonSet.Click += new System.EventHandler(this.buttonSet_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(189, 139);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(81, 27);
            this.buttonCancel.TabIndex = 7;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // comboBoxR
            // 
            this.comboBoxR.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxR.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.comboBoxR.FormattingEnabled = true;
            this.comboBoxR.Location = new System.Drawing.Point(223, 23);
            this.comboBoxR.Name = "comboBoxR";
            this.comboBoxR.Size = new System.Drawing.Size(47, 24);
            this.comboBoxR.TabIndex = 8;
            // 
            // comboBoxV
            // 
            this.comboBoxV.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxV.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.comboBoxV.FormattingEnabled = true;
            this.comboBoxV.Location = new System.Drawing.Point(223, 66);
            this.comboBoxV.Name = "comboBoxV";
            this.comboBoxV.Size = new System.Drawing.Size(47, 24);
            this.comboBoxV.TabIndex = 9;
            // 
            // comboBoxC
            // 
            this.comboBoxC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxC.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.comboBoxC.FormattingEnabled = true;
            this.comboBoxC.Location = new System.Drawing.Point(223, 109);
            this.comboBoxC.Name = "comboBoxC";
            this.comboBoxC.Size = new System.Drawing.Size(47, 24);
            this.comboBoxC.TabIndex = 10;
            // 
            // buttonFill
            // 
            this.buttonFill.Location = new System.Drawing.Point(96, 139);
            this.buttonFill.Name = "buttonFill";
            this.buttonFill.Size = new System.Drawing.Size(81, 27);
            this.buttonFill.TabIndex = 11;
            this.buttonFill.Text = "Fill";
            this.buttonFill.UseVisualStyleBackColor = true;
            this.buttonFill.Click += new System.EventHandler(this.buttonFill_Click);
            // 
            // ValueChanger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(282, 173);
            this.Controls.Add(this.buttonFill);
            this.Controls.Add(this.comboBoxC);
            this.Controls.Add(this.comboBoxV);
            this.Controls.Add(this.comboBoxR);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSet);
            this.Controls.Add(this.labelCurrent);
            this.Controls.Add(this.labelVoltage);
            this.Controls.Add(this.labelResistance);
            this.Controls.Add(this.textBoxCurrent);
            this.Controls.Add(this.textBoxVoltage);
            this.Controls.Add(this.textBoxResistance);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ValueChanger";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Value Changer";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ValueChanger_FormClosed);
            this.Load += new System.EventHandler(this.ValueChanger_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxResistance;
        private System.Windows.Forms.TextBox textBoxVoltage;
        private System.Windows.Forms.TextBox textBoxCurrent;
        private System.Windows.Forms.Label labelResistance;
        private System.Windows.Forms.Label labelVoltage;
        private System.Windows.Forms.Label labelCurrent;
        private System.Windows.Forms.Button buttonSet;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.ComboBox comboBoxR;
        private System.Windows.Forms.ComboBox comboBoxV;
        private System.Windows.Forms.ComboBox comboBoxC;
        private System.Windows.Forms.Button buttonFill;
    }
}