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

namespace SnmpLog
{
    public partial class Form1 : Form
    {
        public struct UpdSwStruct
        {
            
            public SnmpV1TrapPacket snmp_packet;
            public EndPoint snmp_source_ip;
            public StatusStrip strip;
        }

        public class MySwitchs
        {
            public IPAddress IPAddress { get; set; }
            public string Name { get; set; }
            public string Type { get; set; }
            public Point Location { get; set; }


            public MySwitchs(string Name, IPAddress IPAddress, string Type, Point Location)
            {   
                this.Name = Name;
                this.IPAddress = IPAddress;
                this.Type = Type;
                this.Location = Location;
            }
        }
        
        public List<MySwitchs> activeSwitchs = new List<MySwitchs>(15);
        public UInt16 i = 0;
        bool moveobj = false;
        public static bool run = true;
        

        public class DrawLines
        {
            public Pen pen_ok = new Pen(Color.LightGreen)
            {
                Width= 8,
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
            public Pen pen_clear = new Pen(System.Drawing.SystemColors.ControlLight)
            {
                Width = 8,
                StartCap = System.Drawing.Drawing2D.LineCap.Round,
                EndCap = System.Drawing.Drawing2D.LineCap.Round
            };

            public void CreateWire (Point sw1, Point sw2, Pen pen, Graphics graphics)
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
       
        public void DrawWireSwitch()
        {
            DrawLines drawWire = new DrawLines();
            Graphics activememo = pictureBoxWire.CreateGraphics();
            activememo.Clear(System.Drawing.SystemColors.ControlLight);
            drawWire.CreateWire(activeSwitchs[0].Location, activeSwitchs[1].Location, drawWire.pen_ok, activememo);
            drawWire.CreateWire(activeSwitchs[1].Location, activeSwitchs[2].Location, drawWire.pen_ok, activememo);
            drawWire.CreateWire(activeSwitchs[0].Location, activeSwitchs[2].Location, drawWire.pen_bad, activememo);
        }
        public void Upd_Form(UpdSwStruct fromSnmp)
        {
            
            statusStrip.Items.Add(fromSnmp.strip.Items.ToString());
            SoundPlayer player = new SoundPlayer();
            player.Stream = Properties.Resources.audio_editor_output;           
            var builder01 = new StringBuilder();
            builder01.Append(System.DateTime.Now + ": "  + fromSnmp.snmp_packet.Pdu.AgentAddress.ToString() + "\r\n");
            
            Console.WriteLine("** SNMP Version 1 TRAP received from {0}:", fromSnmp.snmp_source_ip.ToString());
            Console.WriteLine("*** Trap generic: {0}", fromSnmp.snmp_packet.Pdu.Generic);
            Console.WriteLine("*** Trap specific: {0} {1}", fromSnmp.snmp_packet.Pdu.Specific, fromSnmp.snmp_packet.Pdu.Enterprise);
            Console.WriteLine("*** Agent address: {0}", fromSnmp.snmp_packet.Pdu.AgentAddress.ToString());
            Console.WriteLine("*** Timestamp: {0}", fromSnmp.snmp_packet.Pdu.TimeStamp.ToString());
            Console.WriteLine("*** VarBind count: {0}", fromSnmp.snmp_packet.Pdu.VbList.Count);
            Console.WriteLine("*** VarBind content:");
            foreach (Vb v in fromSnmp.snmp_packet.Pdu.VbList)
            {
                if (v.Oid.ToString() == "1.3.6.1.4.1.27514.101.120.1")
                {
                    builder01.Append(System.DateTime.Now + ": " + fromSnmp.snmp_packet.Pdu.AgentAddress.ToString() + " " + v.Value.ToString() + "\r\n");
                    richTextBox1.AppendText(builder01.ToString());
                }
                    //Console.WriteLine("**** {0} {1}: {2}", v.Oid.ToString(), SnmpConstants.GetTypeName(v.Value.Type), v.Value.ToString());

            }
            Console.WriteLine("** End of SNMP Version 1 TRAP data.");
            foreach (MySwitchs d in activeSwitchs)
            {
                if (d.IPAddress.ToString() == fromSnmp.snmp_packet.Pdu.AgentAddress.ToString())
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
            DrawWireSwitch();
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
            }
            else
            {
                configureToolStripMenuItem.CheckState = CheckState.Checked;
                opnfileconfToolStripMenuItem.Enabled = true;
                groupBoxConfigure.Visible = true;
                this.pictureBoxWire.Image = global::SnmpLog.Properties.Resources.kletku;
            }
        }

        private void AddSwitch()
        {
            foreach (MySwitchs sw in activeSwitchs)
            {
                if (textBoxName.Text == sw.Name)
                {
                    MessageBox.Show("Объект с таким именем уже есть");
                    return;
                }
            }

            if (string.IsNullOrEmpty(textBoxName.Text) == true && string.IsNullOrEmpty(textBoxIP.Text) == true)
            {
                MessageBox.Show("Заполнены не все поля");
                return;
            }


            try
            {
                Parse(textBoxIP.Text);
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
            pctbx.BringToFront();
            pctbx.MouseDown += Pctbx_MouseDown;
            pctbx.MouseMove += Pctbx_MouseMove;
            pctbx.MouseUp += Pctbx_MouseUp;
            pctbx.Paint += Pctbx_Paint;

            comboBoxSwitchs.Items.Add(textBoxName.Text);
            activeSwitchs.Add(new MySwitchs(textBoxName.Text, Parse(textBoxIP.Text), comboBox1.Text, pctbx.Location));

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
            StringFormat stringFormat= new StringFormat();
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

        private void Pctbx_MouseUp(object sender, MouseEventArgs e)
        {
            moveobj = false;
            sender.GetType().GetProperty("BackColor").SetValue(sender, System.Drawing.SystemColors.Control );
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
            sender.GetType().GetProperty("Location").SetValue(sender, new Point(Cursor.Position.X-DesktopLocation.X-62, Cursor.Position.Y-DesktopLocation.Y-72));
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
           
            if (configureToolStripMenuItem.CheckState == CheckState.Unchecked)
            {

                PictureBox actpctbx = sender as PictureBox;
                foreach (MySwitchs CurrSwitch in activeSwitchs)
                {
                    if (CurrSwitch.Name == actpctbx.Name)
                    {
                        listView1.Items.Clear();
                        SnmpFunction snmpFunction = new SnmpFunction(listView1, richTextBox1);
                        snmpFunction.GetSnmp(activeSwitchs[activeSwitchs.IndexOf(CurrSwitch)]);
                        break;
                    }
                }

                
            }
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

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            listView1.Items.Clear();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            pictureBoxWire.Width = listView1.Location.X - 36;
            pictureBoxWire.Height = richTextBox1.Location.Y - 60;
            var progress_recievedata = new Progress<UpdSwStruct>(s => Upd_Form(s));
            await Task.Run(() => SnmpFunction.TrapReseive(progress_recievedata));
            
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
    }
}



    class SnmpFunction 
    {

    public static ListView statuslist;
    public static RichTextBox statusTextBox;

    public SnmpFunction(ListView list, RichTextBox statusTextbox)
    {
        statuslist = list;
        statusTextBox = statusTextbox;
    }

    public static void TrapReseive(IProgress<UpdSwStruct> progress1)
    {
        // Construct a socket and bind it to the trap manager port 162
        IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 162);
        EndPoint ep = (EndPoint)ipep;
        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

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
        socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout,0);
        
        UpdSwStruct toForm;
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
                        
                    // Parse SNMP Version 1 TRAP packet
                    SnmpV1TrapPacket pkt = new SnmpV1TrapPacket();
                    StatusStrip tool = new StatusStrip();
                    pkt.decode(indata, inlen);
                    toForm.snmp_packet = pkt;
                    toForm.snmp_source_ip = inep;
                    toForm.strip = tool;
                    tool.Items.Add("Сервер запущен");
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
        MessageBox.Show("exit");
    }


    public  void GetSnmp(MySwitchs selectedSwitch)
        {

        // SNMP community name
        OctetString community = new OctetString("public");
        // Define agent parameters class
        AgentParameters param = new AgentParameters(community);
        // Set SNMP version to 1 (or 2)
        param.Version = SnmpVersion.Ver1;
        // Construct the agent address object
        // IpAddress class is easy to use here because
        //  it will try to resolve constructor parameter if it doesn't
        //  parse to an IP address
        IpAddress agent = new IpAddress(selectedSwitch.IPAddress);
        // Construct target
        UdpTarget target = new UdpTarget((IPAddress)agent, 161, 2000, 1);
        // Pdu class used for all requests
        Pdu pdu = new Pdu(PduType.Get);
        pdu.VbList.Add("1.3.6.1.2.1.1.1.0"); //sysDescr
        //.1.3.6.1.2.1.2.2.1.2.

        pdu.VbList.Add("1.3.6.1.2.1.1.2.0"); //sysObjectID
        pdu.VbList.Add("1.3.6.1.2.1.1.3.0"); //sysUpTime
        pdu.VbList.Add("1.3.6.1.2.1.1.4.0"); //sysContact
        pdu.VbList.Add("1.3.6.1.2.1.1.5.0"); //sysName
                                             // Make SNMP request
        for (int i = 1; i <= 28; i++)
        {
            pdu.VbList.Add(".1.3.6.1.2.1.2.2.1.8." + i);
        }
        try
        {
            SnmpV1Packet result = (SnmpV1Packet)target.Request(pdu, param);
            //SnmpSharpNet.SnmpException: "Request has reached maximum retries."

            // If result is null then agent didn't reply or we couldn't parse the reply.
            if (result != null)
            {
                // ErrorStatus other then 0 is an error returned by
                // the Agent - see SnmpConstants for error definitions
                if (result.Pdu.ErrorStatus != 0)
                {
                    // agent reported an error with the request

                    Console.WriteLine("Error in SNMP reply. Error {0} index {1}",
                        result.Pdu.ErrorStatus,
                        result.Pdu.ErrorIndex);
                }
                else
                {
                    // Reply variables are returned in the same order as they were added
                    //  to the VbList

                    List<ListViewItem> myItems = new List<ListViewItem>();
                    List<ListViewItem.ListViewSubItem> listViewSubItems = new List<ListViewItem.ListViewSubItem>();
                    //ListViewItem.ListViewSubItem listViewSubItem = new ListViewItem.ListViewSubItem();
                    for (int i = 1; i <= 28; i++)
                    {
                        if (result.Pdu.VbList[i + 4].Value.ToString() == "1")
                        {
                            myItems.Add(new ListViewItem("Up"));
                            myItems[myItems.Count - 1].BackColor = Color.LightGreen;
                        }

                        else
                            myItems.Add(new ListViewItem("Down"));

                        listViewSubItems.Add(new ListViewItem.ListViewSubItem());

                        listViewSubItems[listViewSubItems.Count - 1].Text = result.Pdu.VbList[i + 4].Oid.ToString();
                        myItems[myItems.Count - 1].SubItems.Add(listViewSubItems[listViewSubItems.Count - 1]);

                        //statuslist.Items.Add(myItems[myItems.Count - 1]);

                    }
                    statuslist.Items.AddRange(myItems.ToArray());




                    Console.WriteLine("sysDescr({0}) ({1}): {2}",
                        result.Pdu.VbList[0].Oid.ToString(),
                        SnmpConstants.GetTypeName(result.Pdu.VbList[0].Value.Type),
                        result.Pdu.VbList[0].Value.ToString());
                    Console.WriteLine("sysObjectID({0}) ({1}): {2}",
                        result.Pdu.VbList[1].Oid.ToString(),
                        SnmpConstants.GetTypeName(result.Pdu.VbList[1].Value.Type),
                        result.Pdu.VbList[1].Value.ToString());
                    Console.WriteLine("sysUpTime({0}) ({1}): {2}",
                        result.Pdu.VbList[2].Oid.ToString(),
                        SnmpConstants.GetTypeName(result.Pdu.VbList[2].Value.Type),
                        result.Pdu.VbList[2].Value.ToString());
                    Console.WriteLine("sysContact({0}) ({1}): {2}",
                        result.Pdu.VbList[3].Oid.ToString(),
                        SnmpConstants.GetTypeName(result.Pdu.VbList[3].Value.Type),
                        result.Pdu.VbList[3].Value.ToString());
                    Console.WriteLine("sysName({0}) ({1}): {2}",
                        result.Pdu.VbList[4].Oid.ToString(),
                        SnmpConstants.GetTypeName(result.Pdu.VbList[4].Value.Type),
                        result.Pdu.VbList[4].Value.ToString());
                }
            }
            else
            {
                Console.WriteLine("No response received from SNMP agent.");
            }

        }
        catch(Exception e)
        {
            if (e.Message == "Request has reached maximum retries.")
                
            MessageBox.Show("Слишком много запросов SNMP");
            return;
        }
        
        
        target.Close();

    }
    }
   

