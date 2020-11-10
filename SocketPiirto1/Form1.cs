using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SocketPiirto1
{
    public partial class Form1 : Form
    {
        private TcpClient client;
        private NetworkStream ns;
        public Form1()
        {
            InitializeComponent();
            connect();
        }

        private void panel_MouseMove(object sender, MouseEventArgs e)
        {
            Point point = panel.PointToClient(Cursor.Position);
            if (Control.MouseButtons == MouseButtons.Left)
            {
                textBox1.Text = point.ToString();
                sentPosition(point.ToString());

                SolidBrush brush = new SolidBrush(Color.Red);
                panel.BringToFront();
                Graphics g = panel.CreateGraphics();
                g.FillEllipse(brush, point.X, point.Y, 4, 4);
            }
        }
        private void connect()
        {
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            int port = 5000;
            client = new TcpClient();
            client.Connect(ip, port);
            Console.WriteLine("client connected!!");
            ns = client.GetStream(); 
        }
        private void disconnect()
        {
            client.Client.Shutdown(SocketShutdown.Send);
            ns.Close();
            client.Close();
            Console.WriteLine("disconnect from server!!");
            Console.ReadKey();
        }
        private void sentPosition(string position)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(position);
            if (buffer.Length > 0)
            {
                ns.Write(buffer, 0, buffer.Length);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
