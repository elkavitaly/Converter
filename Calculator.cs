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
	public partial class Calculator : Form
	{
		public Calculator()
		{
			InitializeComponent();
			AcceptButton = button1;
		}

		public double ConvertToDecimal(string number, int baseS)
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

		public double ConvertSixToDecimal(string number)
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

		public string TenToTwo(string number)
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

		public string TenToEight(string number)
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

		

		private string CalculateNumber(string str, int baseS)
		{
			char oper = ' ';

			for (int i = 0; i < str.Length; i++)
			{
				if (str[i] == '+' || str[i] == '-' || str[i] == '*' || str[i] == '/')
					oper = str[i];
			}

			string[] arr = str.Split(oper);
			arr[0] = arr[0].Replace(" ", string.Empty);
			arr[1] = arr[1].Replace(" ", string.Empty);
			string res = "";
			double firstValue, secondValue, result = 0;

			switch (baseS)
			{
				case 2:
					firstValue = ConvertToDecimal(arr[0], 2);
					secondValue = ConvertToDecimal(arr[1], 2);
					
					switch (oper)
					{
						case '+':
							result = firstValue + secondValue;
							break;
						case '-':
							if (firstValue < secondValue)
								return "Wrong data";
							result = firstValue - secondValue;
							break;
						case '*':
							result = firstValue * secondValue;
							break;
						case '/':
							result = Math.Round(firstValue / secondValue, 6);
							break;
					}
					res = TenToTwo(result.ToString());
					break;
				case 8:
					firstValue = ConvertToDecimal(arr[0], 8);
					secondValue = ConvertToDecimal(arr[1], 8);
					switch (oper)
					{
						case '+':
							result = firstValue + secondValue;
							break;
						case '-':
							if (firstValue < secondValue)
								return "Wrong data";
							result = firstValue - secondValue;
							break;
						case '*':
							result = firstValue * secondValue;
							break;
						case '/':
							result = Math.Round(firstValue / secondValue, 6);
							break;
					}
					res = TenToEight(result.ToString());
					break;
				case 16:
					firstValue = ConvertSixToDecimal(arr[0]);
					secondValue = ConvertSixToDecimal(arr[1]);
					
					switch (oper)
					{
						case '+':
							result = firstValue + secondValue;
							break;
						case '-':
							if (firstValue < secondValue)
								return "Wrong data";
							result = firstValue - secondValue;
							break;
						case '*':
							result = firstValue * secondValue;
							break;
						case '/':
							result = Math.Round(firstValue / secondValue, 6);
							break;
					}
					res = TenToSix(result.ToString());
					break;

				case 10:
					firstValue = Convert.ToDouble(arr[0]);
					secondValue = Convert.ToDouble(arr[1]);

					switch (oper)
					{
						case '+':
							result = firstValue + secondValue;
							break;
						case '-':
							if (firstValue < secondValue)
								return "Wrong data";
							result = firstValue - secondValue;
							break;
						case '*':
							result = firstValue * secondValue;
							break;
						case '/':
							result = Math.Round(firstValue / secondValue, 6);
							break;
					}
					res = result.ToString();
					break;
			}
			return res;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			textBox2.Text = CalculateNumber(textBox1.Text, Convert.ToInt32(comboBox1.Text));
		}
	}
}
