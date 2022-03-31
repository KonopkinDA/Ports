using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;

namespace WF2App
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            List<PortInfo> pi = Form1.GetOpenPort();
            dataGridView1.Rows.Add(pi.Count);
            for (int i = 0; i < pi.Count; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = pi[i].PortNumber;
                dataGridView1.Rows[i].Cells[1].Value = pi[i].Local;
                dataGridView1.Rows[i].Cells[2].Value = pi[i].Remote;
                dataGridView1.Rows[i].Cells[3].Value = pi[i].State;
            }
        }

        private static List<PortInfo> GetOpenPort()
        {
            IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] tcpEndPoints = properties.GetActiveTcpListeners();
            TcpConnectionInformation[] tcpConnections = properties.GetActiveTcpConnections();

            return tcpConnections.Select(p =>
            {
                return new PortInfo(
                    i: p.LocalEndPoint.Port,
                    local: String.Format("{0}:{1}", p.LocalEndPoint.Address, p.LocalEndPoint.Port),
                    remote: String.Format("{0}:{1}", p.RemoteEndPoint.Address, p.RemoteEndPoint.Port),
                    state: p.State.ToString());
            }).ToList();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


    }

    class PortInfo
    {
        public int PortNumber { get; set; }
        public string Local { get; set; }
        public string Remote { get; set; }
        public string State { get; set; }

        public PortInfo(int i, string local, string remote, string state)
        {
            PortNumber = i;
            Local = local;
            Remote = remote;
            State = state;
        }
    }
}
