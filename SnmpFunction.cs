using SnmpSharpNet;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static SnmpLog.Form1;
using System.Windows.Forms;

namespace SnmpLog
{
    class SnmpFunction
    {


        public static void TrapReseive(IProgress<UpdSwStruct> progress)
        {
            // Construct a socket and bind it to the trap manager port 162
            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 162);
            EndPoint ep = (EndPoint)ipep;
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            try
            {
                socket.Bind(ep);
            }
            catch (Exception ex)
            {

                run = false;
                MessageBox.Show(ex.Message);


            }
            // Disable timeout processing. Just block until packet is received
            socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 0);

            UpdSwStruct toForm = new UpdSwStruct();
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
                        pkt.decode(indata, inlen);
                        toForm.snmp_packet = pkt;
                        toForm.snmp_source_ip = inep;
                        toForm.Message = "trap";
                        progress.Report(toForm);
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


        public static void GetSnmp(MySwitchs selectedSwitch)
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
            for (int i = 1; i <= selectedSwitch.Ports.Count; i++)
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
                    selectedSwitch.State = true;
                    selectedSwitch.Updated = true;
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
                        for (int i = 1; i <= selectedSwitch.Ports.Count; i++)
                        {
                            if (result.Pdu.VbList[i + 4].Value.ToString() == "1")
                            {
                                selectedSwitch.Ports[i-1] = true;
                            }
                            else
                                selectedSwitch.Ports[i-1] = false;
                        }
                        selectedSwitch.Uptime = result.Pdu.VbList[2].Value.ToString();
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
                    //progress.Report(toForm);
                }

            }
            catch (Exception e)
            {
                if (e.Message == "Request has reached maximum retries.")
                selectedSwitch.State = false;
                selectedSwitch.Updated = true;
                for (int i=0; i<selectedSwitch.Ports.Count; i++)
                {
                    selectedSwitch.Ports[i] = false;
                }
                //statusTextBox.AppendText("Err SNMP. Адрес не отвечает" + selectedSwitch.IPAddress.ToString() + "\r\n");

                return;
            }


            target.Close();

        }
    }
}
