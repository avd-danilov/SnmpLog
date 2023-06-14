using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using SnmpSharpNet;
using System.Runtime.CompilerServices;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using System.Diagnostics;
using System.Xml.Linq;
using static SnmpLog.Form1;
using System.Media;
using SnmpLog.Properties;
using static System.Net.IPAddress;
using static System.Windows.Forms.MaskedTextBox;
using System.Reflection;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using SnmpLog;
using TextBox = System.Windows.Forms.TextBox;
using ListView = System.Windows.Forms.ListView;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;
using System.Drawing.Drawing2D;
using System.IO;
using System.Xml;
using System.Collections;
using System.Runtime.Remoting.Contexts;

namespace SnmpLog
{
    public partial class Form1 : Form
    {
        public struct UpdSwStruct
        {

            public SnmpV1TrapPacket snmp_packet;
            public EndPoint snmp_source_ip;
            public string Message;

        }
        public struct ConnectionsLine
        {
            public string Switch_A;
            public string Port_A;
            public string Switch_B;
            public string Port_B;
        }

        public class MySwitchs
        {
            public IPAddress IPAddress { get; set; }
            public string Name { get; set; }
            public string Type { get; set; }
            public Point Location { get; set; }
            public bool State { get; set; }
            public bool Updated { get; set; }
            public List<bool> Ports { get; set; }
            public List<int> Stp { get; set; }
            public string Uptime { get; set; }


            public MySwitchs(string Name, IPAddress IPAddress, string Type, Point Location, bool State, bool Updated, List<bool> Ports, string Uptime, List<int> Stp)
            {
                this.Name = Name;
                this.IPAddress = IPAddress;
                this.Type = Type;
                this.Location = Location;
                this.State = State;
                this.Updated = Updated;
                this.Ports = Ports;
                this.Uptime = Uptime;
                this.Stp = Stp;
            }
        }

        public List<MySwitchs> activeSwitchs = new List<MySwitchs>(15);
        public List<ConnectionsLine> activeConnections = new List<ConnectionsLine>(50);
        public UInt16 i = 0;
        bool moveobj = false;
        public static bool run = true;
        public XDocument xdocSwitch;
        public XElement switches = new XElement("switches");
        public XElement connections = new XElement("connections");
        public SoundPlayer player = new SoundPlayer(Properties.Resources.audio_editor_output);

        public string LogName;
        public StringBuilder LogNameBilder = new StringBuilder();
        public StreamWriter LogStream;

        public class DrawLines
        {
            public Pen pen_ok = new Pen(Color.LightGreen)
            {
                Width = 8,
                StartCap = System.Drawing.Drawing2D.LineCap.Round,
                EndCap = System.Drawing.Drawing2D.LineCap.Round
            };
            public Pen pen_bad = new Pen(Color.FromArgb(160, 255, 127, 127))
            {
                Width = 8,
                StartCap = System.Drawing.Drawing2D.LineCap.Round,
                EndCap = System.Drawing.Drawing2D.LineCap.Round,
                DashStyle = DashStyle.Dot,
            };
            public Pen pen_warn = new Pen(Color.FromArgb(160, 255, 255, 127))
            {
                Width = 8,
                StartCap = System.Drawing.Drawing2D.LineCap.Round,
                EndCap = System.Drawing.Drawing2D.LineCap.Round,
                DashStyle = DashStyle.Dot,
            };

            public void CreateWire(Point sw1, Point sw2, Pen pen, Graphics graphics)
            {
                //graphics.DrawLine(pen_clear, sw1.X + 10, sw1.Y - 12, sw2.X + 10, sw2.Y - 12);
                graphics.DrawLine(pen, sw1.X + 42, sw1.Y - 12, sw2.X + 42, sw2.Y - 12);
            }
        }




        public Form1()
        {
            InitializeComponent();
            openFileDialog1.Filter = "XML files(*.xml)|*.xml";
            openFileDialog1.Title = "Открыть конфигурацию";
            openFileDialog1.FileName = "";
            saveFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            groupBoxConfigure.Visible = false;
        }

        public void DrawWireSwitch(List<ConnectionsLine> connections)
        {
            DrawLines drawWire = new DrawLines();
            Graphics activememo = pictureBoxWire.CreateGraphics();
            activememo.Clear(System.Drawing.SystemColors.ControlLight);

            foreach (ConnectionsLine line in connections)
            {
                int SwNumA = 999, SwNumB = 999;
                foreach (MySwitchs switchs in activeSwitchs)
                {
                    if (line.Switch_A == switchs.Name) SwNumA = activeSwitchs.IndexOf(switchs);
                    if (line.Switch_B == switchs.Name) SwNumB = activeSwitchs.IndexOf(switchs);
                }

                if(SwNumA != 999 && SwNumB != 999)
                {
                    if (activeSwitchs[SwNumA].Ports[int.Parse(line.Port_A)-1] &&
                        activeSwitchs[SwNumB].Ports[int.Parse(line.Port_B)-1] &&
                        activeSwitchs[SwNumA].Stp[int.Parse(line.Port_A) - 1] == 5 &&
                        activeSwitchs[SwNumB].Stp[int.Parse(line.Port_B) - 1] == 5)
                    drawWire.CreateWire(activeSwitchs[SwNumA].Location, activeSwitchs[SwNumB].Location, drawWire.pen_ok, activememo);
                    else if(activeSwitchs[SwNumA].Ports[int.Parse(line.Port_A) - 1] &&
                            activeSwitchs[SwNumB].Ports[int.Parse(line.Port_B) - 1] &&
                            (activeSwitchs[SwNumA].Stp[int.Parse(line.Port_A) - 1] == 2 ||
                            activeSwitchs[SwNumB].Stp[int.Parse(line.Port_B) - 1] == 2)
                            )
                    {
                        drawWire.CreateWire(activeSwitchs[SwNumA].Location, activeSwitchs[SwNumB].Location, drawWire.pen_warn, activememo);
                    }
                    else
                        drawWire.CreateWire(activeSwitchs[SwNumA].Location, activeSwitchs[SwNumB].Location, drawWire.pen_bad, activememo);

                }

            }
        }


        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void configureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (configureToolStripMenuItem.CheckState == CheckState.Checked)
            {
                configureToolStripMenuItem.CheckState = CheckState.Unchecked;
                opnfileconfToolStripMenuItem.Enabled = false;
                groupBoxConfigure.Visible = false;
                this.pictureBoxWire.Image = null;
                //Изменим локацию коммутаторов в XML файле
                xdocSwitch = XDocument.Load("default.xml");
                foreach (MySwitchs sw in activeSwitchs)
                {
                    var swElement = xdocSwitch.Element("switches")?
                    .Elements("switch")
                    .FirstOrDefault(s => s.Attribute("name")?.Value == sw.Name);
                    if (swElement != null)
                    {
                        var loc = swElement.Element("Location");
                        var xloc = loc.Element("X");
                        var yloc = loc.Element("Y");

                        if (xloc != null) xloc.Value = sw.Location.X.ToString();
                        if (yloc != null) yloc.Value = sw.Location.Y.ToString();
                        xdocSwitch.Save("default.xml");
                    }
                }
            }
            else
            {
                configureToolStripMenuItem.CheckState = CheckState.Checked;
                opnfileconfToolStripMenuItem.Enabled = true;
                groupBoxConfigure.Visible = true;
                this.pictureBoxWire.Image = global::SnmpLog.Properties.Resources.kletku;
                ConnectGridViev_Update();
            }
        }

        private void LoadConnectionLines()
        {
            XElement switches = xdocSwitch.Element("switches");
            if (switches != null)
            {
                activeConnections.Clear();
                // проходим по всем элементам switch
                foreach (XElement ConnUnit in switches.Elements("connections"))
                {
                    if (!ConnUnit.IsEmpty)
                    {
                        var switchNameA = ConnUnit.Element("switch_a").Value;
                        var portA = ConnUnit.Element("port_a").Value;
                        var switchNameB = ConnUnit.Element("switch_b").Value;
                        var portB = ConnUnit.Element("port_b").Value;
                        ConnectionsLine line = new ConnectionsLine();
                        line.Switch_A= switchNameA;
                        line.Switch_B= switchNameB;
                        line.Port_A= portA;
                        line.Port_B= portB;
                        activeConnections.Add(line);


                    }

                }

            }
        }
        private void LoadSwitch()
        {

            XElement switches = xdocSwitch.Element("switches");
            if (switches != null)
            {
                XElement type = new XElement("type");
                // проходим по всем элементам switch
                foreach (XElement currsw in switches.Elements("switch"))
                {
                    var name = currsw.Attribute("name");
                    var ipadd = currsw.Element("ip");
                    type = currsw.Element("type");
                    var loc = currsw.Element("Location");
                    var xloc = loc.Element("X");
                    var yloc = loc.Element("Y");

                    comboBoxSwitchs.Items.Add(name.Value);
                    SwitchA.Items.Add(name.Value);
                    SwitchB.Items.Add(name.Value);
                    PictureBox pctbx = new PictureBox()
                    {
                        Image = global::SnmpLog.Properties.Resources.sw_clear,
                        SizeMode = PictureBoxSizeMode.CenterImage,
                        Width = 110,
                        Height = 70,
                        Location = new System.Drawing.Point(Convert.ToInt32(xloc.Value), Convert.ToInt32(yloc.Value)),
                        Name = name.Value,
                    };


                    Controls.Add(pctbx);
                    pctbx.BringToFront();
                    pctbx.MouseDown += Pctbx_MouseDown;
                    pctbx.MouseMove += Pctbx_MouseMove;
                    pctbx.MouseUp += Pctbx_MouseUp;
                    pctbx.Paint += Pctbx_Paint;
                    List<bool> Ports = new List<bool>();
                    List<int> Stp = new List<int>();
                    if (type.Value == "QSW4610") while (Ports.Count < 28) 
                        {
                            Ports.Add(false);
                            Stp.Add(0);
                        }
                    if (type.Value == "QSW8330") while (Ports.Count < 56)
                        {
                            Ports.Add(false);
                            Stp.Add(0);
                        }
                    if (type.Value == "") while (Ports.Count < 32)
                        {
                            Ports.Add(false);
                            Stp.Add(0);
                        }

                    activeSwitchs.Add(new MySwitchs(name.Value, Parse(ipadd.Value), type.Value, pctbx.Location, false, false, Ports, "", Stp)); ;
                    i++;
                }
            }
            


        }
        private void AddSwitch()
        {
            foreach (MySwitchs sw in activeSwitchs)
            {
                if (textBoxNameSw.Text == sw.Name)
                {
                    MessageBox.Show("Объект с таким именем уже есть");
                    return;
                }
            }

            if (string.IsNullOrEmpty(textBoxNameSw.Text) == true && string.IsNullOrEmpty(textBoxIpSw.Text) == true)
            {
                MessageBox.Show("Заполнены не все поля");
                return;
            }


            try
            {
                Parse(textBoxIpSw.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Неверный IP");
                return;
            }

            Random rnd = new Random();
            PictureBox pctbx = new PictureBox()
            {
                Image = global::SnmpLog.Properties.Resources.sw_clear,
                SizeMode = PictureBoxSizeMode.CenterImage,
                Width = 110,
                Height = 70,
                Location = new System.Drawing.Point(rnd.Next(50, 200), rnd.Next(50, 200)),
                Name = textBoxNameSw.Text,

            };
            Controls.Add(pctbx);
            pctbx.BringToFront();
            pctbx.MouseDown += Pctbx_MouseDown;
            pctbx.MouseMove += Pctbx_MouseMove;
            pctbx.MouseUp += Pctbx_MouseUp;
            pctbx.Paint += Pctbx_Paint;

            comboBoxSwitchs.Items.Add(textBoxNameSw.Text);
            SwitchA.Items.Add(textBoxNameSw.Text);
            SwitchB.Items.Add(textBoxNameSw.Text);
            XElement switches = xdocSwitch.Element("switches");
            XElement swElement = new XElement("switch");
            XAttribute swNameElement = new XAttribute("name", textBoxNameSw.Text);
            XElement swIPElement = new XElement("ip", textBoxIpSw.Text);
            XElement swTypeElement = new XElement("type", comboBoxTypeSw.Text);
            XElement swLocationElement = new XElement("Location");
            XElement XLocElement = new XElement("X", pctbx.Location.X);
            XElement YLocElement = new XElement("Y", pctbx.Location.Y);


            swElement.Add(swNameElement);
            swElement.Add(swIPElement);
            swElement.Add(swTypeElement);
            swElement.Add(swLocationElement);
            swLocationElement.Add(XLocElement);
            swLocationElement.Add(YLocElement);

            switches.Add(swElement);
            xdocSwitch.Save("default.xml");
            List<bool> Ports = new List<bool>();
            List<int> Stp = new List<int>();
            if (comboBoxTypeSw.Text == "QSW4610") while (Ports.Count < 28)
                {
                    Ports.Add(false);
                    Stp.Add(0);
                }
            if (comboBoxTypeSw.Text == "QSW8330") while (Ports.Count < 56)
                {
                    Ports.Add(false);
                    Stp.Add(0);
                }
            if (comboBoxTypeSw.Text == "") while (Ports.Count < 32)
                {
                    Ports.Add(false);
                    Stp.Add(0);
                }


            activeSwitchs.Add(new MySwitchs(textBoxNameSw.Text, Parse(textBoxIpSw.Text), comboBoxTypeSw.Text, pctbx.Location, false, false, Ports, "", Stp)); ;

            i++;
        }

        private void opnfileconfToolStripMenuItem_Click(object sender, EventArgs e)
        {

            openFileDialog1.ShowDialog();

        }

        private void ButtonAddSw_Click(object sender, EventArgs e)
        {
            AddSwitch();
        }

        private void Pctbx_Paint(object sender, PaintEventArgs e)
        {
            Pen myPen = new Pen(Color.Gray, 1);
            myPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            e.Graphics.DrawRectangle(myPen, 0, 0, 109, 69);
            Font font = new Font("Arial", 13, FontStyle.Bold);
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            foreach (MySwitchs sw in activeSwitchs)
            {
                if (sender.GetType().GetProperty("Name").GetValue(sender, null) as string == sw.Name)
                {
                    e.Graphics.DrawString(activeSwitchs[activeSwitchs.IndexOf(sw)].Name, font, Brushes.DarkGray, 55, 47, stringFormat); //++ sender.GetType().GetProperty("Name").GetValue(sender, null) as string
                    break;
                }
            }

        }

        private async void Pctbx_MouseUp(object sender, MouseEventArgs e)
        {
            moveobj = false;
            sender.GetType().GetProperty("BackColor").SetValue(sender, System.Drawing.SystemColors.Control);

            if (configureToolStripMenuItem.CheckState == CheckState.Unchecked)
            {

                PictureBox actpctbx = sender as PictureBox;
                foreach (MySwitchs switchs in activeSwitchs)
                {
                    if (switchs.Name == actpctbx.Name)
                    {
                        listView1.Items.Clear();

                        List<ListViewItem> myItems = new List<ListViewItem>();
                       //List<ListViewItem.ListViewSubItem> listViewSubItems = new List<ListViewItem.ListViewSubItem>();
                        for (int i = 0; i < switchs.Ports.Count; i++)
                        {
                            myItems.Add(new ListViewItem((i + 1).ToString()));
                            if (switchs.Ports[i])
                            {
                                myItems[myItems.Count - 1].SubItems.Add(new ListViewItem.ListViewSubItem());
                                myItems[myItems.Count - 1].SubItems[1].Text = "Up";
                                myItems[myItems.Count - 1].BackColor = Color.LightGreen;
                            }

                            else
                            {
                                myItems[myItems.Count - 1].SubItems.Add(new ListViewItem.ListViewSubItem());
                                myItems[myItems.Count - 1].SubItems[1].Text = "Down";
                            }
                            myItems[myItems.Count - 1].SubItems.Add(new ListViewItem.ListViewSubItem());
                            if (switchs.Stp[i] == 1) myItems[myItems.Count - 1].SubItems[2].Text = "Disabled";
                            if (switchs.Stp[i] == 2) myItems[myItems.Count - 1].SubItems[2].Text = "Blocking";
                            if (switchs.Stp[i] == 5) myItems[myItems.Count - 1].SubItems[2].Text = "Forwarding";


                        }

                        myItems[0].SubItems.Add(new ListViewItem.ListViewSubItem());
                        myItems[0].SubItems[3].Text = switchs.Name;
                        myItems[1].SubItems.Add(new ListViewItem.ListViewSubItem());
                        myItems[1].SubItems[3].Text = switchs.IPAddress.ToString();
                        myItems[2].SubItems.Add(new ListViewItem.ListViewSubItem());
                        myItems[2].SubItems[3].Text = switchs.Uptime;
                        listView1.Items.AddRange(myItems.ToArray());
                        break;
                    }
                }

            }

        }

        private void Pctbx_MouseMove(object sender, MouseEventArgs e)
        {
            if (moveobj && configureToolStripMenuItem.CheckState == CheckState.Checked &&
                //Cursor.Position.X > ActiveForm.Location.X && 
                //Cursor.Position.X < ActiveForm.Location.X + ActiveForm.Size.Width - listView1.Width - 60 &&
                //Cursor.Position.Y > ActiveForm.Location.Y + 100 &&
                //Cursor.Position.Y < ActiveForm.Location.Y + ActiveForm.Size.Height - richTextBox1.Height - 60
                Cursor.Position.X > ActiveForm.Location.X + pictureBoxWire.Location.X + 50 &&
                Cursor.Position.X < ActiveForm.Location.X + pictureBoxWire.Size.Width - 50 &&
                Cursor.Position.Y > ActiveForm.Location.Y + pictureBoxWire.Location.Y + 50 &&
                Cursor.Position.Y < ActiveForm.Location.Y + pictureBoxWire.Size.Height + 25
                )
                sender.GetType().GetProperty("Location").SetValue(sender, new Point(Cursor.Position.X - DesktopLocation.X - 62, Cursor.Position.Y - DesktopLocation.Y - 72));
            foreach (MySwitchs sw in activeSwitchs)
            {
                if ((sender as PictureBox).Name == sw.Name)
                {
                    activeSwitchs[activeSwitchs.IndexOf(sw)].Location = (sender as PictureBox).Location;
                    break;
                }
            }
        }

        private void Pctbx_MouseDown(object sender, MouseEventArgs e)
        {
            moveobj = true;
            sender.GetType().GetProperty("BackColor").SetValue(sender, System.Drawing.SystemColors.AppWorkspace);
        }


        private void buttonDeleteSw_Click(object sender, EventArgs e)
        {
            if (comboBoxSwitchs.Text == "") return;
            PictureBox actpctbx = Controls[(comboBoxSwitchs.Text)] as PictureBox;
            actpctbx.Dispose();
            foreach (MySwitchs CurrSwitch in activeSwitchs)
            {

                if (CurrSwitch.Name == comboBoxSwitchs.Text)
                {
                    XElement switches = xdocSwitch.Element("switches");
                    foreach (XElement connect in switches.Elements("connections"))
                    {
                        if (!connect.IsEmpty)
                        {
                            var switchNameA = connect.Element("switch_a").Value;
                            var switchNameB = connect.Element("switch_b").Value;
                            if (CurrSwitch.Name == switchNameA || CurrSwitch.Name == switchNameB)
                                connect.Remove();

                        }

                    }

                    
                    foreach (XElement currsw in switches.Elements("switch"))
                    {
                        var name = currsw.Attribute("name");
                        if (CurrSwitch.Name == name.Value)
                        {
                            MessageBox.Show(name.ToString());
                            currsw.Remove();
                            SwitchA.Items.Remove(name.Value);
                            SwitchB.Items.Remove(name.Value);
                        }

                    }


                    switches.Save("default.xml");
                    activeSwitchs.RemoveAt(activeSwitchs.IndexOf(CurrSwitch));
                    comboBoxSwitchs.Items.Remove(comboBoxSwitchs.SelectedItem);
                    comboBoxSwitchs.Text = "";


                    ConnectGridViev_Update();
                    break;
                }

            }

        }

        private void ConnectGridViev_Update()
        {
            while (ConnectGridView.Rows.Count > 1)
            {
                ConnectGridView.Rows.Remove(ConnectGridView.Rows[0]);
            }

            xdocSwitch = XDocument.Load("default.xml");
            XElement switches = xdocSwitch.Element("switches");
            if (!switches.IsEmpty)
            {
                // проходим по всем элементам connections
                foreach (XElement connect in switches.Elements("connections"))
                {
                    if (!connect.IsEmpty)
                    {
                        var switchNameA = connect.Element("switch_a").Value;
                        var portA = connect.Element("port_a").Value;
                        var switchNameB = connect.Element("switch_b").Value;
                        var portB = connect.Element("port_b").Value;

                        ConnectGridView.Rows.Add(switchNameA, portA, switchNameB, portB);
                        
                    }
                       

                }

            }
        }
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            listView1.Items.Clear();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {

            pictureBoxWire.Width = listView1.Location.X - 36;
            pictureBoxWire.Height = richTextBox1.Location.Y - 60;
            LogNameBilder.AppendFormat("{0:D2}_{1:D2}_{2}_log.csv", System.DateTime.Now.Date.Day, System.DateTime.Now.Date.Month, System.DateTime.Now.Date.Year);
            LogName = LogNameBilder.ToString();
            LogStream = new StreamWriter(LogName, true, System.Text.Encoding.Default);

            if (File.Exists("default.xml"))
            {
                xdocSwitch = XDocument.Load("default.xml");
            }

            else
            {
                MessageBox.Show("Файл настроек не найден");
                xdocSwitch = new XDocument();
                xdocSwitch.Add(switches);
                XElement swConn = new XElement("connections");
                XElement swRoot = xdocSwitch.Element("switches");
                switches.Add(swConn);
                xdocSwitch.Save("default.xml");
            }
            LoadSwitch();
            LoadConnectionLines();
            UpdateUI();
            await Task.Run(() => WhoIs());
            var progress_recievedata = new Progress<UpdSwStruct>(s => Upd_Switch(s));
            await Task.Run(() => SnmpFunction.TrapReseive(progress_recievedata));

        }


        private async void WhoIs()
        {
            while (true)
            {
                if (configureToolStripMenuItem.CheckState == CheckState.Unchecked)
                {
                    foreach (MySwitchs switchs in activeSwitchs)
                    {
                        await Task.Run(() => SnmpFunction.GetSnmp(switchs));
                        await Task.Delay(100);
                    }

                }
                await Task.Delay(int.Parse(textBoxUpdTime.Text) * 1000);
            }
        }
        private async void UpdateUI()
        {

            while (true)
            {
                if (configureToolStripMenuItem.CheckState == CheckState.Unchecked) {

                    foreach (MySwitchs switchs in activeSwitchs)
                    {

                        PictureBox actpctbx = Controls[(switchs.Name)] as PictureBox;

                        if (switchs.Updated == true)
                        {
                            if (switchs.State == true)
                            {
                                actpctbx.Image = global::SnmpLog.Properties.Resources.sw_ok;
                            }
                            else
                            {
                                actpctbx.Image = global::SnmpLog.Properties.Resources.sw_bad;
                            }
                            switchs.Updated = false;
                        }

                    }
                    if(activeConnections.Count != 0)
                    DrawWireSwitch(activeConnections);
                }
                await Task.Delay(1000);
            }
                
            

        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            listView1.Location = new Point(this.Size.Width - 490 - 30, 47);
            listView1.Size = new Size(490, this.Size.Height - 130);
            groupBoxConfigure.Location = listView1.Location;
            richTextBox1.Location = new Point(12, this.Size.Height - 380);
            pictureBoxWire.Width = listView1.Location.X - 36;
            pictureBoxWire.Height = richTextBox1.Location.Y - 60;

        }






        private void buttonSaveConn_Click(object sender, EventArgs e)
        {

            xdocSwitch = XDocument.Load("default.xml");
            XElement root = xdocSwitch.Root;
            var connections = root.Elements("connections");
            connections.Remove();
            
            //xdocSwitch.Save("default.xml");
            try

            {
                root.Add(connections);
                foreach (DataGridViewRow RowConn in ConnectGridView.Rows)
                {

                    if (RowConn.Cells[0].Value == null) 
                      continue;
                    if (RowConn.IsNewRow)
                        continue;

                    XElement swConnElement = new XElement("connections");
                    XElement swConnA = new XElement("switch_a");
                    swConnA.Value = RowConn.Cells[0].Value.ToString();
                    XElement swPortA = new XElement("port_a");
                    swPortA.Value = RowConn.Cells[1].Value.ToString();
                    XElement swConnB = new XElement("switch_b");
                    swConnB.Value = RowConn.Cells[2].Value.ToString();
                    XElement swPortB = new XElement("port_b");
                    swPortB.Value = RowConn.Cells[3].Value.ToString();
                    swConnElement.Add(swConnA);
                    swConnElement.Add(swConnB);
                    swConnElement.Add(swPortA);
                    swConnElement.Add(swPortB);
                    root.Add(swConnElement);
                }
                root.Save("default.xml");
            }

            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при заполнении полей");
                return;

            }
            LoadConnectionLines();
            ConnectGridViev_Update();
        }


        public void Upd_Switch (UpdSwStruct fromSnmp)
        {
            int t=999;
            string StringPortNum;
            string StringPortState;
            string StringSearchEth = "1/0/";
            string StringSearchState = "state to ";

            foreach (MySwitchs CurrentSwitch in activeSwitchs)
            {
                if (fromSnmp.Message == "trap" && CurrentSwitch.IPAddress.ToString() == fromSnmp.snmp_packet.Pdu.AgentAddress.ToString())
                {
                    player.Play();
                    this.TopMost = true;
                    this.TopMost = false;
                }

                if (CurrentSwitch.IPAddress.ToString() == fromSnmp.snmp_packet.Pdu.AgentAddress.ToString())
                {
                    CurrentSwitch.State = true;
                    CurrentSwitch.Updated = true;
                    t = activeSwitchs.IndexOf(CurrentSwitch);
                }

            }
            if (t == 999) return;
            var builderText = new StringBuilder();

            Console.WriteLine("** SNMP Version 1 TRAP received from {0}:", fromSnmp.snmp_source_ip.ToString());
            Console.WriteLine("*** Trap generic: {0}", fromSnmp.snmp_packet.Pdu.Generic);
            Console.WriteLine("*** Trap specific: {0} {1}", fromSnmp.snmp_packet.Pdu.Specific, fromSnmp.snmp_packet.Pdu.Enterprise);
            Console.WriteLine("*** Agent address: {0}", fromSnmp.snmp_packet.Pdu.AgentAddress.ToString());
            Console.WriteLine("*** Timestamp: {0}", fromSnmp.snmp_packet.Pdu.TimeStamp.ToString());
            Console.WriteLine("*** VarBind count: {0}", fromSnmp.snmp_packet.Pdu.VbList.Count);
            Console.WriteLine("*** VarBind content:");

            foreach (Vb v in fromSnmp.snmp_packet.Pdu.VbList)
            {
                if (v.Oid.ToString() == "1.3.6.1.4.1.27514.101.120.1")          // Находим Сообщение от порта
                {
                    StringPortNum = v.Value.ToString();                            // Найдем в тексте Номер порта
                    StringPortNum = StringPortNum.Substring(StringPortNum.LastIndexOf(StringSearchEth)+4,2);
                    StringPortNum = StringPortNum.Trim();

                    StringPortState = v.Value.ToString();                           // Найдем в тексте статус порта
                    StringPortState = StringPortState.Substring(StringPortState.LastIndexOf(StringSearchState)+9);
                    if (StringPortState == "discarding!")
                        activeSwitchs[t].Ports[int.Parse(StringPortNum)-1] = false;
                    if (StringPortState == "learning!")
                        activeSwitchs[t].Ports[int.Parse(StringPortNum)-1] = false;
                    if (StringPortState == "forwarding!")
                        activeSwitchs[t].Ports[int.Parse(StringPortNum)-1] = true;

                    builderText.Append(System.DateTime.Now + "    " + activeSwitchs[t].Name + fromSnmp.snmp_packet.Pdu.AgentAddress.ToString() + " " + v.Value.ToString() + "\r\n");
                    richTextBox1.AppendText(builderText.ToString());
                    builderText.Clear();
                    builderText.AppendFormat("{0}\t{1}\tПорт №{2:D2}\t{3}\r\n", System.DateTime.Now, activeSwitchs[t].Name, StringPortNum, StringPortState);
                    LogStream.Write(builderText);
                    LogStream.Flush();

                }
                Console.WriteLine("**** {0} {1}: {2}", v.Oid.ToString(), SnmpConstants.GetTypeName(v.Value.Type), v.Value.ToString());
            }
            Console.WriteLine("** End of SNMP Version 1 TRAP data.");
        }
    }
}



