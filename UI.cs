using SnmpSharpNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SnmpLog.Form1;
using System.Windows.Forms;

namespace SnmpLog
{
     public class UI
    {
        //public static void Upd_Switch(UpdSwStruct fromSnmp)  //вызывается с помощью trap
        //{

        //    //statusStrip.Items.Add(fromSnmp.strip.Items.ToString());
        //    //player.Stream = Properties.Resources.audio_editor_output;
        //    var builder01 = new StringBuilder();

        //    Console.WriteLine("** SNMP Version 1 TRAP received from {0}:", fromSnmp.snmp_source_ip.ToString());
        //    Console.WriteLine("*** Trap generic: {0}", fromSnmp.snmp_packet.Pdu.Generic);
        //    Console.WriteLine("*** Trap specific: {0} {1}", fromSnmp.snmp_packet.Pdu.Specific, fromSnmp.snmp_packet.Pdu.Enterprise);
        //    Console.WriteLine("*** Agent address: {0}", fromSnmp.snmp_packet.Pdu.AgentAddress.ToString());
        //    Console.WriteLine("*** Timestamp: {0}", fromSnmp.snmp_packet.Pdu.TimeStamp.ToString());
        //    Console.WriteLine("*** VarBind count: {0}", fromSnmp.snmp_packet.Pdu.VbList.Count);
        //    Console.WriteLine("*** VarBind content:");

        //    foreach (Vb v in fromSnmp.snmp_packet.Pdu.VbList)
        //    {
        //        if (v.Oid.ToString() == "1.3.6.1.4.1.27514.101.120.1")          // Сообщение от порта
        //        {
        //            builder01.Append(fromSnmp.snmp_packet.Pdu.AgentAddress.ToString() + " " + v.Value.ToString() + "\r\n");
        //            fromSnmp.Message = builder01.ToString();

        //        }
        //        Console.WriteLine("**** {0} {1}: {2}", v.Oid.ToString(), SnmpConstants.GetTypeName(v.Value.Type), v.Value.ToString());
        //    }
        //    Console.WriteLine("** End of SNMP Version 1 TRAP data.");
        //    //foreach (MySwitchs d in activeSwitchs)
        //    //{
        //    //    if (d.IPAddress.ToString() == fromSnmp.snmp_packet.Pdu.AgentAddress.ToString())
        //    //    {
        //    //        PictureBox actpctbx = Controls[(d.Name)] as PictureBox;
        //    //        actpctbx.Image = global::SnmpLog.Properties.Resources.sw_ok;
        //    //        player.Play();
        //    //        if (actpctbx.Created == true)
        //    //        {
        //    //            this.TopMost = true;
        //    //            this.TopMost = false;
        //    //        }
        //    //    }
        //    //}
        //    //DrawWireSwitch();
        //}

        //public static void Upd_Switch(UpdSwStruct fromSnmp) //Вызывается с периодичностью
        //{

        //    //statusStrip.Items.Add(fromSnmp.strip.Items.ToString());

        //    //player.Stream = Properties.Resources.audio_editor_output;
        //    var builder01 = new StringBuilder();
        //    selectedSwitch.State = true;
        //    Console.WriteLine("** SNMP Version 1 TRAP received from {0}:", fromSnmp.snmp_source_ip.ToString());
        //    Console.WriteLine("*** Trap generic: {0}", fromSnmp.snmp_packet.Pdu.Generic);
        //    Console.WriteLine("*** Trap specific: {0} {1}", fromSnmp.snmp_packet.Pdu.Specific, fromSnmp.snmp_packet.Pdu.Enterprise);
        //    Console.WriteLine("*** Agent address: {0}", fromSnmp.snmp_packet.Pdu.AgentAddress.ToString());
        //    Console.WriteLine("*** Timestamp: {0}", fromSnmp.snmp_packet.Pdu.TimeStamp.ToString());
        //    Console.WriteLine("*** VarBind count: {0}", fromSnmp.snmp_packet.Pdu.VbList.Count);
        //    Console.WriteLine("*** VarBind content:");
        //    foreach (Vb v in fromSnmp.snmp_packet.Pdu.VbList)
        //    {
        //        if (v.Oid.ToString() == "1.3.6.1.4.1.27514.101.120.1")
        //        {
        //            foreach (MySwitchs d in fromSnmp.activeswitchs)
        //            {
        //                if (d.IPAddress.ToString() == fromSnmp.snmp_packet.Pdu.AgentAddress.ToString())
        //                {
        //                    builder01.Append(System.DateTime.Now + "    " + d.Name + fromSnmp.snmp_packet.Pdu.AgentAddress.ToString() + " " + v.Value.ToString() + "\r\n");
        //                    fromSnmp.message = builder01.ToString();

        //                }

        //            }

        //        }
        //        Console.WriteLine("**** {0} {1}: {2}", v.Oid.ToString(), SnmpConstants.GetTypeName(v.Value.Type), v.Value.ToString());

        //    }
        //    Console.WriteLine("** End of SNMP Version 1 TRAP data.");
        //    //foreach (MySwitchs d in activeSwitchs)
        //    //{
        //    //    if (d.IPAddress.ToString() == fromSnmp.snmp_packet.Pdu.AgentAddress.ToString())
        //    //    {
        //    //        PictureBox actpctbx = Controls[(d.Name)] as PictureBox;
        //    //        actpctbx.Image = global::SnmpLog.Properties.Resources.sw_ok;
        //    //        player.Play();
        //    //        if (actpctbx.Created == true)
        //    //        {
        //    //            this.TopMost = true;
        //    //            this.TopMost = false;
        //    //        }
        //    //    }
        //    //}
        //    //DrawWireSwitch();
        //}
    }
}

    
