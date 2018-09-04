﻿using System;
using System.Text;
using System.Runtime.InteropServices; // 必要

namespace TextMemoryTool4
{
	class InifileUtils
	{
		/// <summary>
		/// iniファイルのパスを保持
		/// </summary>
		public String filePath { get; set; }
		
		// ==========================================================
		[DllImport("KERNEL32.DLL")]
		public static extern uint
			GetPrivateProfileString(string lpAppName,
			string lpKeyName, string lpDefault,
			StringBuilder lpReturnedString, uint nSize,
			string lpFileName);

		[DllImport("KERNEL32.DLL")]
		public static extern uint
			GetPrivateProfileInt(string lpAppName,
			string lpKeyName, int nDefault, string lpFileName);

		[DllImport("kernel32.dll")]
		private static extern int WritePrivateProfileString(
			string lpApplicationName,
			string lpKeyName,
			string lpstring,
			string lpFileName);
		// ==========================================================

		/// <summary>
		/// コンストラクタ(デフォルト)
		/// </summary>
		public InifileUtils()
		{
			filePath = AppDomain.CurrentDomain.BaseDirectory + "Default.ini";
		}

		/// <summary>
		/// iniファイル中のセクションのキーを指定して、文字列を返す
		/// </summary>
		/// <param name="section"></param>
		/// <param name="key"></param>
		/// <returns></returns>
		public String getValueString(String section, String key)
		{
			StringBuilder sb = new StringBuilder(1024);

			GetPrivateProfileString(
				section,
				key,
				"",
				sb,
				Convert.ToUInt32(sb.Capacity),
				filePath);

			return sb.ToString();
		}

		/// <summary>
		/// 指定したセクション、キーに文字列を書き込む
		/// </summary>
		/// <param name="section"></param>
		/// <param name="key"></param>
		/// <param name="val"></param>
		public void setValue(String section, String key, String val)
		{
			WritePrivateProfileString(section, key, val, filePath);
		}
	}

	// こちらの記事を参考にさせていただきました
	// 「C#でiniファイルを取り扱うためのクラス実装例」
	// https://qiita.com/y_minowa/items/685db9926dec0d6b711b

}
