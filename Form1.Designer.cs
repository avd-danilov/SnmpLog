using System.Windows.Forms;

namespace SnmpLog
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.opnfileconfToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.groupBoxConfigure = new System.Windows.Forms.GroupBox();
            this.textBoxUpdTime = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ConnectGridView = new System.Windows.Forms.DataGridView();
            this.SwitchA = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.SwitchNumPortA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SwitchB = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.SwitchNumPortB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonSaveConn = new System.Windows.Forms.Button();
            this.ButtonDeleteSw = new System.Windows.Forms.Button();
            this.comboBoxSwitchs = new System.Windows.Forms.ComboBox();
            this.textBoxIpSw = new System.Windows.Forms.TextBox();
            this.comboBoxTypeSw = new System.Windows.Forms.ComboBox();
            this.textBoxNameSw = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.labelIP = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.ButtonAddSw = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnPorts = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnState = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnData = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pictureBoxWire = new System.Windows.Forms.PictureBox();
            this.timer_update = new System.Windows.Forms.Timer(this.components);
            this.columnSTP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.menuStrip1.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.groupBoxConfigure.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ConnectGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxWire)).BeginInit();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(12, 398);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(860, 300);
            this.richTextBox1.TabIndex = 4;
            this.richTextBox1.Text = "";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1397, 30);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuProgram";
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configureToolStripMenuItem,
            this.opnfileconfToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(65, 26);
            this.menuToolStripMenuItem.Text = "Меню";
            // 
            // configureToolStripMenuItem
            // 
            this.configureToolStripMenuItem.Name = "configureToolStripMenuItem";
            this.configureToolStripMenuItem.Size = new System.Drawing.Size(294, 26);
            this.configureToolStripMenuItem.Text = "Режим конфигурации";
            this.configureToolStripMenuItem.Click += new System.EventHandler(this.ConfigureToolStripMenuItem_Click);
            // 
            // opnfileconfToolStripMenuItem
            // 
            this.opnfileconfToolStripMenuItem.Enabled = false;
            this.opnfileconfToolStripMenuItem.Name = "opnfileconfToolStripMenuItem";
            this.opnfileconfToolStripMenuItem.Size = new System.Drawing.Size(294, 26);
            this.opnfileconfToolStripMenuItem.Text = "Открыть файл конфигурации";
            this.opnfileconfToolStripMenuItem.Click += new System.EventHandler(this.OpnfileconfToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(294, 26);
            this.exitToolStripMenuItem.Text = "Выход";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel,
            this.toolStripStatusLabel1});
            this.statusStrip.Location = new System.Drawing.Point(0, 706);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1397, 26);
            this.statusStrip.TabIndex = 7;
            this.statusStrip.Text = "statusStrip";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(34, 20);
            this.toolStripStatusLabel.Text = "ПО:";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 20);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // groupBoxConfigure
            // 
            this.groupBoxConfigure.Controls.Add(this.textBoxUpdTime);
            this.groupBoxConfigure.Controls.Add(this.label1);
            this.groupBoxConfigure.Controls.Add(this.ConnectGridView);
            this.groupBoxConfigure.Controls.Add(this.buttonSaveConn);
            this.groupBoxConfigure.Controls.Add(this.ButtonDeleteSw);
            this.groupBoxConfigure.Controls.Add(this.comboBoxSwitchs);
            this.groupBoxConfigure.Controls.Add(this.textBoxIpSw);
            this.groupBoxConfigure.Controls.Add(this.comboBoxTypeSw);
            this.groupBoxConfigure.Controls.Add(this.textBoxNameSw);
            this.groupBoxConfigure.Controls.Add(this.label4);
            this.groupBoxConfigure.Controls.Add(this.labelIP);
            this.groupBoxConfigure.Controls.Add(this.labelName);
            this.groupBoxConfigure.Controls.Add(this.ButtonAddSw);
            this.groupBoxConfigure.Location = new System.Drawing.Point(895, 48);
            this.groupBoxConfigure.Name = "groupBoxConfigure";
            this.groupBoxConfigure.Size = new System.Drawing.Size(490, 650);
            this.groupBoxConfigure.TabIndex = 8;
            this.groupBoxConfigure.TabStop = false;
            // 
            // textBoxUpdTime
            // 
            this.textBoxUpdTime.Location = new System.Drawing.Point(429, 26);
            this.textBoxUpdTime.Name = "textBoxUpdTime";
            this.textBoxUpdTime.Size = new System.Drawing.Size(45, 22);
            this.textBoxUpdTime.TabIndex = 18;
            this.textBoxUpdTime.Text = "5";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(253, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(170, 16);
            this.label1.TabIndex = 17;
            this.label1.Text = "Период обновления, сек.";
            // 
            // ConnectGridView
            // 
            this.ConnectGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ConnectGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SwitchA,
            this.SwitchNumPortA,
            this.SwitchB,
            this.SwitchNumPortB});
            this.ConnectGridView.Location = new System.Drawing.Point(6, 291);
            this.ConnectGridView.MultiSelect = false;
            this.ConnectGridView.Name = "ConnectGridView";
            this.ConnectGridView.RowHeadersWidth = 51;
            this.ConnectGridView.RowTemplate.Height = 24;
            this.ConnectGridView.Size = new System.Drawing.Size(403, 353);
            this.ConnectGridView.TabIndex = 12;
            // 
            // SwitchA
            // 
            this.SwitchA.HeaderText = "Свитч";
            this.SwitchA.MinimumWidth = 6;
            this.SwitchA.Name = "SwitchA";
            this.SwitchA.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SwitchA.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.SwitchA.Width = 125;
            // 
            // SwitchNumPortA
            // 
            dataGridViewCellStyle1.Format = "0";
            dataGridViewCellStyle1.NullValue = null;
            this.SwitchNumPortA.DefaultCellStyle = dataGridViewCellStyle1;
            this.SwitchNumPortA.HeaderText = "№ порта";
            this.SwitchNumPortA.MinimumWidth = 6;
            this.SwitchNumPortA.Name = "SwitchNumPortA";
            this.SwitchNumPortA.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.SwitchNumPortA.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.SwitchNumPortA.Width = 50;
            // 
            // SwitchB
            // 
            this.SwitchB.HeaderText = "Свитч";
            this.SwitchB.MinimumWidth = 6;
            this.SwitchB.Name = "SwitchB";
            this.SwitchB.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SwitchB.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.SwitchB.Width = 125;
            // 
            // SwitchNumPortB
            // 
            dataGridViewCellStyle2.Format = "0";
            this.SwitchNumPortB.DefaultCellStyle = dataGridViewCellStyle2;
            this.SwitchNumPortB.HeaderText = "№ порта";
            this.SwitchNumPortB.MinimumWidth = 6;
            this.SwitchNumPortB.Name = "SwitchNumPortB";
            this.SwitchNumPortB.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.SwitchNumPortB.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.SwitchNumPortB.Width = 50;
            // 
            // buttonSaveConn
            // 
            this.buttonSaveConn.Location = new System.Drawing.Point(6, 265);
            this.buttonSaveConn.Name = "buttonSaveConn";
            this.buttonSaveConn.Size = new System.Drawing.Size(172, 23);
            this.buttonSaveConn.TabIndex = 16;
            this.buttonSaveConn.Text = "Сохранить соединения";
            this.buttonSaveConn.UseVisualStyleBackColor = true;
            this.buttonSaveConn.Click += new System.EventHandler(this.ButtonSaveConn_Click);
            // 
            // ButtonDeleteSw
            // 
            this.ButtonDeleteSw.Location = new System.Drawing.Point(198, 212);
            this.ButtonDeleteSw.Name = "ButtonDeleteSw";
            this.ButtonDeleteSw.Size = new System.Drawing.Size(149, 23);
            this.ButtonDeleteSw.TabIndex = 15;
            this.ButtonDeleteSw.Text = "Удалить устройство";
            this.ButtonDeleteSw.UseVisualStyleBackColor = true;
            this.ButtonDeleteSw.Click += new System.EventHandler(this.ButtonDeleteSw_Click);
            // 
            // comboBoxSwitchs
            // 
            this.comboBoxSwitchs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSwitchs.FormattingEnabled = true;
            this.comboBoxSwitchs.Location = new System.Drawing.Point(14, 212);
            this.comboBoxSwitchs.Name = "comboBoxSwitchs";
            this.comboBoxSwitchs.Size = new System.Drawing.Size(176, 24);
            this.comboBoxSwitchs.TabIndex = 14;
            // 
            // textBoxIpSw
            // 
            this.textBoxIpSw.Location = new System.Drawing.Point(189, 156);
            this.textBoxIpSw.Name = "textBoxIpSw";
            this.textBoxIpSw.Size = new System.Drawing.Size(158, 22);
            this.textBoxIpSw.TabIndex = 13;
            this.textBoxIpSw.Text = "127.0.0.1";
            // 
            // comboBoxTypeSw
            // 
            this.comboBoxTypeSw.FormattingEnabled = true;
            this.comboBoxTypeSw.Items.AddRange(new object[] {
            "QSW4610",
            "QSW8330"});
            this.comboBoxTypeSw.Location = new System.Drawing.Point(189, 118);
            this.comboBoxTypeSw.Name = "comboBoxTypeSw";
            this.comboBoxTypeSw.Size = new System.Drawing.Size(158, 24);
            this.comboBoxTypeSw.TabIndex = 11;
            // 
            // textBoxNameSw
            // 
            this.textBoxNameSw.Location = new System.Drawing.Point(189, 83);
            this.textBoxNameSw.Name = "textBoxNameSw";
            this.textBoxNameSw.Size = new System.Drawing.Size(158, 22);
            this.textBoxNameSw.TabIndex = 9;
            this.textBoxNameSw.Text = "Name";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 121);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(136, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "Модель устройства";
            // 
            // labelIP
            // 
            this.labelIP.AutoSize = true;
            this.labelIP.Location = new System.Drawing.Point(86, 159);
            this.labelIP.Name = "labelIP";
            this.labelIP.Size = new System.Drawing.Size(61, 16);
            this.labelIP.TabIndex = 2;
            this.labelIP.Text = "IP адрес";
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(35, 86);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(112, 16);
            this.labelName.TabIndex = 1;
            this.labelName.Text = "Имя устройства";
            // 
            // ButtonAddSw
            // 
            this.ButtonAddSw.Location = new System.Drawing.Point(28, 21);
            this.ButtonAddSw.Name = "ButtonAddSw";
            this.ButtonAddSw.Size = new System.Drawing.Size(174, 32);
            this.ButtonAddSw.TabIndex = 0;
            this.ButtonAddSw.Text = "Добавить устройство";
            this.ButtonAddSw.UseVisualStyleBackColor = true;
            this.ButtonAddSw.Click += new System.EventHandler(this.ButtonAddSw_Click);
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnPorts,
            this.columnState,
            this.columnSTP,
            this.columnData});
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(895, 47);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(490, 650);
            this.listView1.TabIndex = 10;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnPorts
            // 
            this.columnPorts.Text = "Порт";
            this.columnPorts.Width = 45;
            // 
            // columnState
            // 
            this.columnState.Text = "Состояние";
            this.columnState.Width = 120;
            // 
            // columnData
            // 
            this.columnData.Text = "Данные";
            this.columnData.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnData.Width = 197;
            // 
            // pictureBoxWire
            // 
            this.pictureBoxWire.BackColor = System.Drawing.SystemColors.ControlLight;
            this.pictureBoxWire.Location = new System.Drawing.Point(12, 47);
            this.pictureBoxWire.Name = "pictureBoxWire";
            this.pictureBoxWire.Size = new System.Drawing.Size(860, 326);
            this.pictureBoxWire.TabIndex = 11;
            this.pictureBoxWire.TabStop = false;
            this.pictureBoxWire.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            // 
            // columnSTP
            // 
            this.columnSTP.Text = "STP статус";
            this.columnSTP.Width = 120;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1397, 732);
            this.Controls.Add(this.groupBoxConfigure);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.pictureBoxWire);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "SnmpLog";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.groupBoxConfigure.ResumeLayout(false);
            this.groupBoxConfigure.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ConnectGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxWire)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolStripMenuItem opnfileconfToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.GroupBox groupBoxConfigure;
        private System.Windows.Forms.Button ButtonAddSw;
        private System.Windows.Forms.ComboBox comboBoxTypeSw;
        private System.Windows.Forms.TextBox textBoxNameSw;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelIP;
        private System.Windows.Forms.Label labelName;
        private TextBox textBoxIpSw;
        private ComboBox comboBoxSwitchs;
        private Button ButtonDeleteSw;
        private ListView listView1;
        private ColumnHeader columnPorts;
        private ColumnHeader columnState;
        private PictureBox pictureBoxWire;
        private DataGridView ConnectGridView;
        private Button buttonSaveConn;
        private DataGridViewComboBoxColumn SwitchA;
        private DataGridViewTextBoxColumn SwitchNumPortA;
        private DataGridViewComboBoxColumn SwitchB;
        private DataGridViewTextBoxColumn SwitchNumPortB;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private Timer timer_update;
        private TextBox textBoxUpdTime;
        private Label label1;
        private ColumnHeader columnData;
        private ColumnHeader columnSTP;
    }
}

