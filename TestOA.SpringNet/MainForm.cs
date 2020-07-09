using Spring.Context;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestOA.SpringNet
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //创建一个IApplicationContext接口对象
            IApplicationContext ctx = ContextRegistry.GetContext();
            //通过ctx读取配置创建对应的对象
            IUserInfoService UserInfoService = (IUserInfoService)ctx.GetObject("UserInfoService");
            MessageBox.Show(UserInfoService.ShowMsg());
        }
    }
}
