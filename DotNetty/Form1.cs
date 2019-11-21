using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DotNetty
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            EventBusUtil._eventBus.Register(this);
        }


        [Subscribe]
        public void StateChange(StateChange state)
        {
            if (state.IpAddress.Equals("") && state.State == 1)
            {
                Task.Run(() =>
                {

                });
            }
        }

        [Subscribe]
        public void ReceiveData(string str)
        {

        }

        public static void ShowMessage(TextBox textControl, string message)
        {
            Action<string> actionDelegate = (x) => { textControl.AppendText(x + Environment.NewLine); };
            textControl.Invoke(actionDelegate, message);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {

                new CardClient("192.168.170.177", 12000, "192.168.170.177", 10002).Connect();

            });
        }
    }
}
