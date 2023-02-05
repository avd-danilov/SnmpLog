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

namespace SnmpLog
{
    public partial class Form1 : Form
    {
        public struct _updsctruct
        {
            public SnmpV1TrapPacket snmp_packet;
            public EndPoint snmp_source_ip;
        }

        public class MySwitchs
        {
            public IPAddress IPAddress { get; set; }
            public string Name { get; set; }

            public MySwitchs(string Name, IPAddress IPAddress)
            {   
                this.Name = Name;
                this.IPAddress = IPAddress;
            }
        }
        SnmpV1TrapPacket pk2 = new SnmpV1TrapPacket();


        public List<MySwitchs> activeSwitchs = new List<MySwitchs>(10);
        public UInt16 i = 0;
        bool moveobj = false;
        public Form1()
        {
            InitializeComponent();
            openFileDialog1.Filter = "XML files(*.xml)|*.xml";
            openFileDialog1.Title = "Открыть конфигурацию";
            openFileDialog1.FileName = "";
            saveFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            groupBoxConfigure.Visible = false;
            
        }
        private async void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.AppendText(activeSwitchs.Count.ToString());
            richTextBox1.AppendText(activeSwitchs[0].Name);
            var progress_recievedata = new Progress<_updsctruct>(s => Upd_Form(s));
            await Task.Run(() => snmpserver.TrapReseive(progress_recievedata));


        }
        void Upd_Form(_updsctruct a)
        {
            SoundPlayer player = new SoundPlayer();
            player.Stream = Properties.Resources.audio_editor_output;
            
            var builder01 = new StringBuilder();
            builder01.Append(System.DateTime.Now + ": " + a.snmp_packet.Pdu.Generic + a.snmp_packet.Pdu.AgentAddress.ToString() + "\r\n");
            richTextBox1.AppendText(builder01.ToString());

            Console.WriteLine("** SNMP Version 1 TRAP received from {0}:", a.snmp_source_ip.ToString());
            Console.WriteLine("*** Trap generic: {0}", a.snmp_packet.Pdu.Generic);
            Console.WriteLine("*** Trap specific: {0} {1}", a.snmp_packet.Pdu.Specific, a.snmp_packet.Pdu.Enterprise);
            Console.WriteLine("*** Agent address: {0}", a.snmp_packet.Pdu.AgentAddress.ToString());
            Console.WriteLine("*** Timestamp: {0}", a.snmp_packet.Pdu.TimeStamp.ToString());
            Console.WriteLine("*** VarBind count: {0}", a.snmp_packet.Pdu.VbList.Count);
            Console.WriteLine("*** VarBind content:");
            foreach (Vb v in a.snmp_packet.Pdu.VbList)
            {
                Console.WriteLine("**** {0} {1}: {2}", v.Oid.ToString(), SnmpConstants.GetTypeName(v.Value.Type), v.Value.ToString());
            }
            Console.WriteLine("** End of SNMP Version 1 TRAP data.");

            foreach (MySwitchs d in activeSwitchs)
            {
                if (d.IPAddress.ToString() == a.snmp_packet.Pdu.AgentAddress.ToString())
                {
                    PictureBox actpctbx = Controls[(d.Name)] as PictureBox;
                    actpctbx.Image = global::SnmpLog.Properties.Resources.sw_ok;
                    player.Play();
                    if (actpctbx.Created == true)
                    {
                        this.TopMost = true;
                        this.TopMost = false;
                    }
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
            }
            else
            {
                configureToolStripMenuItem.CheckState = CheckState.Checked;
                opnfileconfToolStripMenuItem.Enabled = true;
                groupBoxConfigure.Visible = true;
            }
        }

        private void AddSwitch()
        {
            
            if(string.IsNullOrEmpty(textBoxName.Text) == true && string.IsNullOrEmpty(textBoxIP.Text) == true)
            {
                MessageBox.Show("Заполнены не все поля");
                return;
            }


            try
            {
                activeSwitchs.Add(new MySwitchs(textBoxName.Text, Parse(textBoxIP.Text)));
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
                Name = textBoxName.Text,


            };
            Controls.Add(pctbx);
            pctbx.MouseDown += Pctbx_MouseDown;
            pctbx.MouseMove += Pctbx_MouseMove;
            pctbx.MouseUp += Pctbx_MouseUp;
            pctbx.Paint += Pctbx_Paint;

            comboBoxSwitchs.Items.Add(textBoxName.Text);

            i++;
        }



        private void opnfileconfToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            openFileDialog1.ShowDialog();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddSwitch();
        }

        private void Pctbx_Paint(object sender, PaintEventArgs e)
        {
            Pen myPen = new Pen(Color.Gray, 1);
            myPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            e.Graphics.DrawRectangle(myPen, 0, 0, 109, 69);
            Font font = new Font("Arial", 13, FontStyle.Bold);
            StringFormat stringFormat= new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            e.Graphics.DrawString(activeSwitchs[0].Name, font,Brushes.DarkGray, 55,47, stringFormat); //++ sender.GetType().GetProperty("Name").GetValue(sender, null) as string
        }

        private void Pctbx_MouseUp(object sender, MouseEventArgs e)
        {
            moveobj = false;
        }

        private void Pctbx_MouseMove(object sender, MouseEventArgs e)
        {
            if (moveobj && configureToolStripMenuItem.CheckState == CheckState.Checked &&
                Cursor.Position.X > ActiveForm.Location.X && 
                Cursor.Position.X < ActiveForm.Location.X + ActiveForm.Size.Width &&
                Cursor.Position.Y > ActiveForm.Location.Y + 100 &&
                Cursor.Position.Y < ActiveForm.Location.Y + ActiveForm.Size.Height-60)
            sender.GetType().GetProperty("Location").SetValue(sender, new Point(Cursor.Position.X-DesktopLocation.X-50, Cursor.Position.Y-DesktopLocation.Y-50));
        }

        private void Pctbx_MouseDown(object sender, MouseEventArgs e)
        {
            moveobj = true;
        }

        
        private void switch_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Ok" + sender);
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
                    activeSwitchs.RemoveAt(activeSwitchs.IndexOf(CurrSwitch));
                    comboBoxSwitchs.Items.Remove(comboBoxSwitchs.SelectedItem);
                    comboBoxSwitchs.Text = "";
                    break;
                }
            }
        }
    }
    }


    class snmpserver 
    {
        public static void TrapReseive(IProgress<_updsctruct> progress1) 
        {
            // Construct a socket and bind it to the trap manager port 162
            bool run = true;
            MessageBox.Show("Запускаю...");

            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 162);
            EndPoint ep = (EndPoint)ipep;
            try
            {
                socket.Bind(ep);
            }
            catch(Exception ex)
            {
                run =false;
                MessageBox.Show(ex.Message);
                
            }
            // Disable timeout processing. Just block until packet is received
            socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 0);
            
            int inlen = -1;
            while (run)
            {
                byte[] indata = new byte[16 * 1024];
                // 16KB receive buffer int inlen = 0;
                IPEndPoint peer = new IPEndPoint(IPAddress.Any, 0);
                EndPoint inep = (EndPoint)peer;
                try
                {
                    inlen = socket.ReceiveFrom(indata, ref inep);
                }
                catch (Exception ex)
                {


                    Console.WriteLine("Exception {0}", ex.Message);
                    inlen = -1;
                }
                if (inlen > 0)
                {
                    // Check protocol version int
                    int ver = SnmpPacket.GetProtocolVersion(indata, inlen);
                    if (ver == (int)SnmpVersion.Ver1)
                    {
                        _updsctruct toForm;
                        // Parse SNMP Version 1 TRAP packet
                        SnmpV1TrapPacket pkt = new SnmpV1TrapPacket();
                        pkt.decode(indata, inlen);
                        toForm.snmp_packet = pkt;
                        toForm.snmp_source_ip = inep;
                        progress1.Report(toForm);
                    }
                    else
                    {
                        // Parse SNMP Version 2 TRAP packet
                        SnmpV2Packet pkt = new SnmpV2Packet();
                        pkt.decode(indata, inlen);
                        Console.WriteLine("** SNMP Version 2 TRAP received from {0}:", inep.ToString());
                        if ((SnmpSharpNet.PduType)pkt.Pdu.Type != PduType.V2Trap)
                        {
                            Console.WriteLine("*** NOT an SNMPv2 trap ****");
                        }
                        else
                        {
                            Console.WriteLine("*** Community: {0}", pkt.Community.ToString());
                            Console.WriteLine("*** VarBind count: {0}", pkt.Pdu.VbList.Count);
                            Console.WriteLine("*** VarBind content:");
                            foreach (Vb v in pkt.Pdu.VbList)
                            {
                                Console.WriteLine("**** {0} {1}: {2}",
                                   v.Oid.ToString(), SnmpConstants.GetTypeName(v.Value.Type), v.Value.ToString());
                            }
                            Console.WriteLine("** End of SNMP Version 2 TRAP data.");
                        }
                    }
                }
                else
                {
                    if (inlen == 0)
                        Console.WriteLine("Zero length packet received.");
                }
            }
        }
    }
   

