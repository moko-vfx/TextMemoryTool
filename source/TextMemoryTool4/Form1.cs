using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextMemoryTool4
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			// iniファイルからの読み込み
			InifileUtils ini = new InifileUtils();

			// iniファイルからタブ名を読み込んで設定
			tabControl1.TabPages[0].Text = ini.getValueString("Tab0", "Text");
			tabControl1.TabPages[1].Text = ini.getValueString("Tab1", "Text");
			tabControl1.TabPages[2].Text = ini.getValueString("Tab2", "Text");
		}
	}
}
