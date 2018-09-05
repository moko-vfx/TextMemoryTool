using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace TextMemoryTool4
{
	public partial class UserControl1 : UserControl
	{
		// 定数
		// iniファイルで改行を表す記号をここで定義
		const string rn = "<rn>";

		// 変数
		string myName = "";		// 自身のForm1上での名前を保存
		string myText = "";		// 自身のTextBoxの内容を保存

		// コンストラクタ
		public UserControl1()
		{
			InitializeComponent();

			// コンストラクタでiniファイルからTextBoxへ文字列を読み込みたいところですが
			// 初期化タイミングでは自身のForm1上での名前をまだ取得できないので
			// UserControl1_Load イベントでiniから取得しています

			// ツール終了時にはUserControl内からForm1のCloseを取得できないのでDisposeを利用
			// こちらの記事を参考にさせていただきました
			// https://qiita.com/noobar/items/219dbf57c6ee391ee83b

			// ツール終了時の自身のコントロール解放時の挙動
			this.Disposed += (sender, args) =>
			{
				// 保存ダイアログで保存するを選んだ場合
				if (Form1.flagSave == true)
				{
					// TextBox内の文字列をiniファイルへ書き込む
					// Disposeの時点でTextBox.Textは解放済みのようなので取得できません
					// myText変数に格納している値は生きているのでそちらを利用しています
					ini.setValue(myName, "Text", myText);
				}
			};
		}

		// iniファイルへの読み書きのためのクラスからインスタンスを生成
		InifileUtils ini = new InifileUtils();

		// コントロールがロードされた際にTextBox内の文字列をiniから読み込む
		private void UserControl1_Load(object sender, EventArgs e)
		{
			// 自身のForm1上での名前を保存
			myName = this.Name;

			// iniファイルからの読み込み
			string s = ini.getValueString(myName, "Text");
			// 独自の改行記号を改行に置換
			tbText.Text = s.Replace(rn, "\r\n");

			// TextBoxの変更フラグをfalseにしておく
			Form1.flagChange = false;
		}

		// TextBoxの中身をプロパティでアクセス可能にする
		[Browsable(true)]
		public string TextBoxText
		{
			get
			{
				return this.tbText.Text;
			}
			set
			{
				this.tbText.Text = value;
			}
		}

		// CopyボタンでTextBoxの内容をクリップボードにコピー
		private void btnCopy_Click(object sender, EventArgs e)
		{
			Clipboard.SetText(tbText.Text);
		}

		// ClearボタンでTextBoxの内容を消去
		private void btnClear_Click(object sender, EventArgs e)
		{
			tbText.ResetText();
		}

		// Ctrl + A でTextBoxの内容を全選択
		private void tbText_KeyDown(object sender, KeyEventArgs e)
		{
			//Ctrl + A で全選択可能にする
			if (e.Control && e.KeyCode == Keys.A)
				tbText.SelectAll();
		}

		// テキスト内容に変化があれば変数に格納しておく
		private void tbText_TextChanged(object sender, EventArgs e)
		{
			// TextBoxの変更フラグを立てる
			Form1.flagChange = true;

			// 改行を独自に定義した改行記号に変更
			myText = tbText.Text.Replace("\r\n", rn); ;
		}
	}
}
