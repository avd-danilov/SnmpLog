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
            this.button1 = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.opnfileconfToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.groupBoxConfigure = new System.Windows.Forms.GroupBox();
            this.ButtonDeleteSw = new System.Windows.Forms.Button();
            this.comboBoxSwitchs = new System.Windows.Forms.ComboBox();
            this.textBoxIP = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.labelIP = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.ButtonAddSw = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBoxConfigure.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(735, 559);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(150, 36);
            this.button1.TabIndex = 0;
            this.button1.Text = "Запустить сервер";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(446, 543);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(283, 52);
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
            this.menuStrip1.Size = new System.Drawing.Size(1397, 28);
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
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(65, 24);
            this.menuToolStripMenuItem.Text = "Меню";
            // 
            // configureToolStripMenuItem
            // 
            this.configureToolStripMenuItem.Name = "configureToolStripMenuItem";
            this.configureToolStripMenuItem.Size = new System.Drawing.Size(294, 26);
            this.configureToolStripMenuItem.Text = "Режим конфигурации";
            this.configureToolStripMenuItem.Click += new System.EventHandler(this.configureToolStripMenuItem_Click);
            // 
            // opnfileconfToolStripMenuItem
            // 
            this.opnfileconfToolStripMenuItem.Enabled = false;
            this.opnfileconfToolStripMenuItem.Name = "opnfileconfToolStripMenuItem";
            this.opnfileconfToolStripMenuItem.Size = new System.Drawing.Size(294, 26);
            this.opnfileconfToolStripMenuItem.Text = "Открыть файл конфигурации";
            this.opnfileconfToolStripMenuItem.Click += new System.EventHandler(this.opnfileconfToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(294, 26);
            this.exitToolStripMenuItem.Text = "Выход";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 707);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1397, 26);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(34, 20);
            this.toolStripStatusLabel1.Text = "ПО:";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // groupBoxConfigure
            // 
            this.groupBoxConfigure.Controls.Add(this.ButtonDeleteSw);
            this.groupBoxConfigure.Controls.Add(this.comboBoxSwitchs);
            this.groupBoxConfigure.Controls.Add(this.textBoxIP);
            this.groupBoxConfigure.Controls.Add(this.comboBox1);
            this.groupBoxConfigure.Controls.Add(this.textBoxName);
            this.groupBoxConfigure.Controls.Add(this.label4);
            this.groupBoxConfigure.Controls.Add(this.labelIP);
            this.groupBoxConfigure.Controls.Add(this.labelName);
            this.groupBoxConfigure.Controls.Add(this.ButtonAddSw);
            this.groupBoxConfigure.Location = new System.Drawing.Point(70, 225);
            this.groupBoxConfigure.Name = "groupBoxConfigure";
            this.groupBoxConfigure.Size = new System.Drawing.Size(370, 370);
            this.groupBoxConfigure.TabIndex = 8;
            this.groupBoxConfigure.TabStop = false;
            // 
            // ButtonDeleteSw
            // 
            this.ButtonDeleteSw.Location = new System.Drawing.Point(153, 323);
            this.ButtonDeleteSw.Name = "ButtonDeleteSw";
            this.ButtonDeleteSw.Size = new System.Drawing.Size(149, 23);
            this.ButtonDeleteSw.TabIndex = 15;
            this.ButtonDeleteSw.Text = "Удалить устройство";
            this.ButtonDeleteSw.UseVisualStyleBackColor = true;
            this.ButtonDeleteSw.Click += new System.EventHandler(this.buttonDeleteSw_Click);
            // 
            // comboBoxSwitchs
            // 
            this.comboBoxSwitchs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSwitchs.FormattingEnabled = true;
            this.comboBoxSwitchs.Location = new System.Drawing.Point(26, 323);
            this.comboBoxSwitchs.Name = "comboBoxSwitchs";
            this.comboBoxSwitchs.Size = new System.Drawing.Size(121, 24);
            this.comboBoxSwitchs.TabIndex = 14;
            // 
            // textBoxIP
            // 
            this.textBoxIP.Location = new System.Drawing.Point(189, 156);
            this.textBoxIP.Name = "textBoxIP";
            this.textBoxIP.Size = new System.Drawing.Size(158, 22);
            this.textBoxIP.TabIndex = 13;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Qtech 4410",
            "Qtech 8330"});
            this.comboBox1.Location = new System.Drawing.Point(189, 118);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(158, 24);
            this.comboBox1.TabIndex = 11;
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(189, 83);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(158, 22);
            this.textBoxName.TabIndex = 9;
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
            this.columnHeader1,
            this.columnHeader2});
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(909, 50);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(488, 654);
            this.listView1.TabIndex = 10;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Тип";
            this.columnHeader1.Width = 77;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Данные";
            this.columnHeader2.Width = 197;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1397, 733);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.groupBoxConfigure);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "SnmpLog";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBoxConfigure.ResumeLayout(false);
            this.groupBoxConfigure.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripMenuItem opnfileconfToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.GroupBox groupBoxConfigure;
        private System.Windows.Forms.Button ButtonAddSw;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelIP;
        private System.Windows.Forms.Label labelName;
        private TextBox textBoxIP;
        private ComboBox comboBoxSwitchs;
        private Button ButtonDeleteSw;
        private ListView listView1;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
    }
}

