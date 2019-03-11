using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;

namespace Converter
{
	public partial class Management : Form
	{
		public Management()
		{
			InitializeComponent();
			Manage();
		}

		private void Manage()
		{
			ManagementClass mclass = new ManagementClass("Win32_Processor");
			ManagementObjectCollection col = mclass.GetInstances();
			PropertyDataCollection prop = mclass.Properties;
			int x = 10, y = 10, i = 0;
			foreach (ManagementObject o in col)
			{
				foreach (PropertyData p in prop)
				{
					if (this.Size.Height < y + 80)
					{
						y = 10;
						x = 300;
					}
					string str = p.Name + ": " + o.Properties[p.Name].Value;
					TextBox label= new TextBox();
					label.Name = p.Name;
					label.ReadOnly = true;
					label.Text = str;
					label.Width = 250;
					label.Location = new Point(x, y + 20);
					Controls.Add(label);
					y += 20;
				}
			}
		}

       
    }
}
