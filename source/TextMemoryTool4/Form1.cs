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

		// 変数
		// TextBoxに変更があるかを保存
		public static bool flagChange = false;
		public static bool flagSave = false;

		private void Form1_Load(object sender, EventArgs e)
		{
			// iniファイルからの読み込み
			InifileUtils ini = new InifileUtils();

			// iniファイルからタブ名を読み込んで設定
			tabControl1.TabPages[0].Text = ini.getValueString("Tab0", "Text");
			tabControl1.TabPages[1].Text = ini.getValueString("Tab1", "Text");
			tabControl1.TabPages[2].Text = ini.getValueString("Tab2", "Text");
		}

		// Form1終了時のイベント
		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			// TextBoxに変更があった場合
			if (flagChange == true)
			{
				// メッセージボックスを表示
				DialogResult result = MessageBox.Show("変更を上書き保存しますか？","Save Dialog",
					MessageBoxButtons.YesNo,			// Yes No のダイアログ
					MessageBoxIcon.None,				// アイコンなし
					MessageBoxDefaultButton.Button1);	// デフォルト選択

				// 押したボタンでの分岐
				if (result == DialogResult.Yes)
				{
					//「はい」が選択された時
					flagSave = true;
				}
				else
				{
					//「いいえ」が選択された時
					flagSave = false;
				}
			}
		}
	}
}
