namespace Full_1
{
    partial class CircuitForm
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
            this.panelCircuitBoard = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gridToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.drawToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cursorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.powerSupplyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resistorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wireToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wire1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wire2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wire3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fourWayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.voltmeterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ammeterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelCircuitBoard
            // 
            this.panelCircuitBoard.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelCircuitBoard.Location = new System.Drawing.Point(25, 50);
            this.panelCircuitBoard.Name = "panelCircuitBoard";
            this.panelCircuitBoard.Size = new System.Drawing.Size(1350, 550);
            this.panelCircuitBoard.TabIndex = 0;
            this.panelCircuitBoard.Paint += new System.Windows.Forms.PaintEventHandler(this.panelCircuitBoard_Paint);
            this.panelCircuitBoard.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panelCircuitBoard_MouseClick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.drawToolStripMenuItem});
            this.menuStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1416, 23);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.loadToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 19);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.loadToolStripMenuItem.Text = "Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteAllToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 19);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // deleteAllToolStripMenuItem
            // 
            this.deleteAllToolStripMenuItem.Name = "deleteAllToolStripMenuItem";
            this.deleteAllToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.deleteAllToolStripMenuItem.Text = "Delete All";
            this.deleteAllToolStripMenuItem.Click += new System.EventHandler(this.deleteAllToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gridToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 19);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // gridToolStripMenuItem
            // 
            this.gridToolStripMenuItem.Name = "gridToolStripMenuItem";
            this.gridToolStripMenuItem.Size = new System.Drawing.Size(96, 22);
            this.gridToolStripMenuItem.Text = "Grid";
            this.gridToolStripMenuItem.Click += new System.EventHandler(this.gridToolStripMenuItem_Click);
            // 
            // drawToolStripMenuItem
            // 
            this.drawToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cursorToolStripMenuItem,
            this.powerSupplyToolStripMenuItem,
            this.resistorToolStripMenuItem,
            this.wireToolStripMenuItem,
            this.voltmeterToolStripMenuItem,
            this.ammeterToolStripMenuItem});
            this.drawToolStripMenuItem.Name = "drawToolStripMenuItem";
            this.drawToolStripMenuItem.Size = new System.Drawing.Size(46, 19);
            this.drawToolStripMenuItem.Text = "Draw";
            // 
            // cursorToolStripMenuItem
            // 
            this.cursorToolStripMenuItem.Name = "cursorToolStripMenuItem";
            this.cursorToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.cursorToolStripMenuItem.Text = "Select";
            this.cursorToolStripMenuItem.Click += new System.EventHandler(this.selectToolStripMenuItem_Click);
            // 
            // powerSupplyToolStripMenuItem
            // 
            this.powerSupplyToolStripMenuItem.Name = "powerSupplyToolStripMenuItem";
            this.powerSupplyToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.powerSupplyToolStripMenuItem.Text = "Cell";
            this.powerSupplyToolStripMenuItem.Click += new System.EventHandler(this.CellToolStripMenuItem_Click);
            // 
            // resistorToolStripMenuItem
            // 
            this.resistorToolStripMenuItem.Name = "resistorToolStripMenuItem";
            this.resistorToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.resistorToolStripMenuItem.Text = "Resistor";
            this.resistorToolStripMenuItem.Click += new System.EventHandler(this.resistorToolStripMenuItem_Click);
            // 
            // wireToolStripMenuItem
            // 
            this.wireToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.wire1ToolStripMenuItem,
            this.wire2ToolStripMenuItem,
            this.wire3ToolStripMenuItem,
            this.fourWayToolStripMenuItem});
            this.wireToolStripMenuItem.Name = "wireToolStripMenuItem";
            this.wireToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.wireToolStripMenuItem.Text = "Add Wire";
            // 
            // wire1ToolStripMenuItem
            // 
            this.wire1ToolStripMenuItem.Name = "wire1ToolStripMenuItem";
            this.wire1ToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.wire1ToolStripMenuItem.Text = "Straight Wire";
            this.wire1ToolStripMenuItem.Click += new System.EventHandler(this.wire1ToolStripMenuItem_Click);
            // 
            // wire2ToolStripMenuItem
            // 
            this.wire2ToolStripMenuItem.Name = "wire2ToolStripMenuItem";
            this.wire2ToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.wire2ToolStripMenuItem.Text = "Two Way Alternate";
            this.wire2ToolStripMenuItem.Click += new System.EventHandler(this.wire2ToolStripMenuItem_Click);
            // 
            // wire3ToolStripMenuItem
            // 
            this.wire3ToolStripMenuItem.Name = "wire3ToolStripMenuItem";
            this.wire3ToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.wire3ToolStripMenuItem.Text = "Three Way";
            this.wire3ToolStripMenuItem.Click += new System.EventHandler(this.wire3ToolStripMenuItem_Click);
            // 
            // fourWayToolStripMenuItem
            // 
            this.fourWayToolStripMenuItem.Name = "fourWayToolStripMenuItem";
            this.fourWayToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.fourWayToolStripMenuItem.Text = "Four Way";
            this.fourWayToolStripMenuItem.Click += new System.EventHandler(this.fourWayToolStripMenuItem_Click);
            // 
            // voltmeterToolStripMenuItem
            // 
            this.voltmeterToolStripMenuItem.Name = "voltmeterToolStripMenuItem";
            this.voltmeterToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.voltmeterToolStripMenuItem.Text = "Voltmeter";
            this.voltmeterToolStripMenuItem.Click += new System.EventHandler(this.voltmeterToolStripMenuItem_Click);
            // 
            // ammeterToolStripMenuItem
            // 
            this.ammeterToolStripMenuItem.Name = "ammeterToolStripMenuItem";
            this.ammeterToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.ammeterToolStripMenuItem.Text = "Ammeter";
            this.ammeterToolStripMenuItem.Click += new System.EventHandler(this.ammeterToolStripMenuItem_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // CircuitForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1416, 629);
            this.Controls.Add(this.panelCircuitBoard);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "CircuitForm";
            this.ShowIcon = false;
            this.Text = "Circuit Editor";
            this.Load += new System.EventHandler(this.CircuitForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CircuitForm_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CircuitForm_KeyPress);
            this.Resize += new System.EventHandler(this.CircuitForm_Resize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelCircuitBoard;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem drawToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem powerSupplyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resistorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wireToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wire1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wire2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wire3ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem voltmeterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ammeterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cursorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fourWayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gridToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteAllToolStripMenuItem;
        private System.Windows.Forms.Timer timer1;
    }
}

