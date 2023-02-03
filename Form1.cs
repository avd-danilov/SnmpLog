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


namespace SnmpLog
{
    public partial class Form1 : Form
    {
        public struct _updsctruct
        {
            public SnmpV1TrapPacket snmp_packet;
            public EndPoint snmp_source_ip;
        }
        SnmpV1TrapPacket pk2 = new SnmpV1TrapPacket();

        int i = 0;
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
            var progress_recievedata = new Progress<_updsctruct>(s => Upd_Form(s));
            await Task.Run(() => snmpserver.TrapReseive(progress_recievedata));


        }
        void Upd_Form(_updsctruct a)
        {
            if (a.snmp_packet.Pdu.AgentAddress.ToString() == "10.22.8.11")
            {
                SoundPlayer player = new SoundPlayer();

                player.Stream = Properties.Resources.audio_editor_output;
                player.Play();
                label1.Image = global::SnmpLog.Properties.Resources.руль;
            }
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


        }

        private void toolStripTextBox2_Click(object sender, EventArgs e)
        {

        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void конфигурацияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (конфигурацияToolStripMenuItem.CheckState == CheckState.Checked)
            {
                конфигурацияToolStripMenuItem.CheckState = CheckState.Unchecked;
                открытьФайлКонфигурацииToolStripMenuItem.Enabled = false;
                groupBoxConfigure.Visible = false;
            }
            else
            {
                конфигурацияToolStripMenuItem.CheckState = CheckState.Checked;
                открытьФайлКонфигурацииToolStripMenuItem.Enabled = true;
                groupBoxConfigure.Visible = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void открытьФайлКонфигурацииToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            openFileDialog1.ShowDialog();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Controls.Add(new PictureBox()
            {
                
                Image = global::SnmpLog.Properties.Resources.руль,
                Location = new System.Drawing.Point(200, 200),
                Name = "picbx_DinamicAdded" + i.ToString(),
                Size = new System.Drawing.Size(120, 120),
                TabIndex = 9,
                TabStop = false
            });
            i++;

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
   

