using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Converter
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
			AcceptButton = button1;
		}

		public static double ConvertToDecimal(string number, int baseS)
		{
			double res1 = 0;
			double res2 = 0;
			string[] parts = number.Split(',');
			int len1 = parts[0].Length;
			
			for (int i = 0; i < len1; i++)
			{
				res1 += Math.Pow(baseS, i) * char.GetNumericValue(parts[0][len1 - i - 1]);
			}
			if (parts.Length != 1 && parts[1].Length != 0)
			{
				int len2 = parts[1].Length;
				for (int i = 0; i < len2; i++)
				{
					res2 += Math.Pow(baseS, -(i + 1)) * char.GetNumericValue(parts[1][i]);
				}
			}
			return res1 + res2;
		}

		public static double ConvertSixToDecimal(string number)
		{
			double res1 = 0;
			double res2 = 0;
			string[] parts = number.Split(',');

			Dictionary<char, int> dictionary = new Dictionary<char, int>();
			dictionary.Add('0', 0);
			dictionary.Add('1', 1);
			dictionary.Add('2', 2);
			dictionary.Add('3', 3);
			dictionary.Add('4', 4);
			dictionary.Add('5', 5);
			dictionary.Add('6', 6);
			dictionary.Add('7', 7);
			dictionary.Add('8', 8);
			dictionary.Add('9', 9);
			dictionary.Add('A', 10);
			dictionary.Add('B', 11);
			dictionary.Add('C', 12);
			dictionary.Add('D', 13);
			dictionary.Add('E', 14);
			dictionary.Add('F', 15);

			int len1 = parts[0].Length;
			
			for (int i = 0; i < len1; i++)
			{
				res1 += Math.Pow(16, i) * dictionary[parts[0][len1 - i - 1]];
			}
			if (parts.Length != 1 && parts[1].Length != 0)
			{
				int len2 = parts[1].Length;
				for (int i = 0; i < len2; i++)
				{
					res2 += Math.Pow(16, -(i + 1)) * dictionary[parts[1][i]];
				}
			}
			return Math.Round(res1 + res2, 6);
		}

		public static string TenToTwo(string number)
		{
			string result = "";
			string[] parts = number.Split(',');
			if (parts[0].Length != 0)
			{
				int part = Convert.ToInt32(parts[0]);
				while (part >= 1)
				{
					int o = part % 2;
					part = (part - o) / 2;
					result += o;
				}
			}
			result = result == "" ? "0" : new string(result.ToCharArray().Reverse().ToArray());
			//result = new string(result.ToCharArray().Reverse().ToArray());
			if (parts.Length != 1 && parts[1].Length != 0)
			{
				result += ',';
				int part = Convert.ToInt32(parts[1]);
				int count = parts[1].Length;
				for (int i = 0; i < 6; i++)
				{
					part *= 2;
					if (part.ToString().Length > count)
					{
						part = Convert.ToInt32(part.ToString().Remove(0, 1));
						result += '1';
					}
					else
						result += '0';
				}
			}
			return result;
		}

		public static string TenToEight(string number)
		{
			string result = "";
			string[] parts = number.Split(',');
			if (parts[0].Length != 0)
			{
				int part = Convert.ToInt32(parts[0]);
				while (part >= 1)
				{
					int o = part % 8;
					part = (part - o) / 8;
					result += o;
				}
			}
			result = result == "" ? "0" : new string(result.ToCharArray().Reverse().ToArray());
			//result = new string(result.ToCharArray().Reverse().ToArray());
			if (parts.Length != 1 && parts[1].Length != 0)
			{
				result += ',';
				int part = Convert.ToInt32(parts[1]);
				int count = parts[1].Length;
				for (int i = 0; i < 6; i++)
				{
					part *= 8;
					if (part.ToString().Length > count)
					{
						result += part.ToString()[0];
						part = Convert.ToInt32(part.ToString().Remove(0, 1));
					}
					else
						result += '0';
				}
			}
			return result;
		}

		public static string TenToSix(string number)
		{
			string result = "";
			string[] parts = number.Split(',');
			Dictionary<int, string> dictionary = new Dictionary<int, string>();
			dictionary.Add(0, "0");
			dictionary.Add(1, "1");
			dictionary.Add(2, "2");
			dictionary.Add(3, "3");
			dictionary.Add(4, "4");
			dictionary.Add(5, "5");
			dictionary.Add(6, "6");
			dictionary.Add(7, "7");
			dictionary.Add(8, "8");
			dictionary.Add(9, "9");
			dictionary.Add(10, "A");
			dictionary.Add(11, "B");
			dictionary.Add(12, "C");
			dictionary.Add(13, "D");
			dictionary.Add(14, "E");
			dictionary.Add(15, "F");

			if (parts[0].Length != 0)
			{
				int part = Convert.ToInt32(parts[0]);
				while (part >= 1)
				{
					int o = part % 16;
					part = (part - o) / 16;
					result += dictionary[o];
				}
			}

			result = result == "" ? "0" : new string(result.ToCharArray().Reverse().ToArray());

			if (parts.Length != 1 && parts[1].Length != 0)
			{
				result += ',';
				int part = int.Parse(parts[1]);
				int count = parts[1].Length;
				for (int i = 0; i < 6; i++)
				{
					part *= 16;
					if (part.ToString().Length - count == 1)
					{
						result += dictionary[Convert.ToInt32(char.GetNumericValue(part.ToString()[0]))];
						part = Convert.ToInt32(part.ToString().Remove(0, 1));
					}
					else if (part.ToString().Length - count == 2)
					{
						result += dictionary[Convert.ToInt32(part.ToString().Substring(0, 2))];
						part = Convert.ToInt32(part.ToString().Remove(0, 2));
					}
					else
						result += '0';
				}
			}
			return result;
		}

		

		private void button1_Click(object sender, EventArgs e)
		{
			List<string> list = new List<string>() { "2", "8", "10", "16" };
			string number = textBox1.Text;
			int baseFrom = Convert.ToInt32(comboBox1.Text);
			int baseTo = Convert.ToInt32(comboBox2.Text);
			string res = "";
			if(baseFrom == 10)
			{
				switch (baseTo)
				{
					case 2:
						res = TenToTwo(number);
						break;
					case 8:
						res = TenToEight(number);
						break;
					case 16:
						res = TenToSix(number);
						break;
				}
			}
			else if(baseFrom == 2)
			{
				string n = ConvertToDecimal(number, 2).ToString();
				switch (baseTo)
				{
					case 10:
						res = n;
						break;
					case 8:
						res = TenToEight(n);
						break;
					case 16:
						res = TenToSix(n);
						break;
				}
			}
			else if (baseFrom == 8)
			{
				string n = ConvertToDecimal(number, 8).ToString();
				switch (baseTo)
				{
					case 10:
						res = n;
						break;
					case 2:
						res = TenToTwo(n);
						break;
					case 16:
						res = TenToSix(n);
						break;
				}
			}
			else if (baseFrom == 16)
			{
				string n = ConvertSixToDecimal(number).ToString();
				switch (baseTo)
				{
					case 10:
						res = n;
						break;
					case 2:
						res = TenToTwo(n);
						break;
					case 8:
						res = TenToEight(n);
						break;
				}
			}
			textBox2.Text = res;
		}

    
    }
}
