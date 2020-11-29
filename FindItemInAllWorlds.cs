using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

public class FindItemInAllWorlds : Form
{
	private int searchingitemid;

	private IContainer components = null;

	private Label label4;

	private Label label5;

	private Button btnSort;

	private TextBox txtSort;

	private Label lblTotal;

	private Label label1;

	private Label label3;

	private Label label2;

	private ListBox lstItemsInWorld;

	public FindItemInAllWorlds(int getitemid)
	{
		InitializeComponent();
		searchingitemid = getitemid;
	}

	private void FindItemInAllWorlds_Load(object sender, EventArgs e)
	{
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Expected O, but got Unknown
		int num = 0;
		List<string> list = new List<string>();
		int num2 = Directory.GetFiles("worlds", "*", SearchOption.TopDirectoryOnly).Length;
		DirectoryInfo directoryInfo = new DirectoryInfo("worlds");
		for (int i = 0; i < num2; i++)
		{
			FileInfo fileInfo = directoryInfo.GetFiles()[i];
			string text = File.ReadAllText("worlds/" + fileInfo.Name);
			try
			{
				JObject val = JObject.Parse(text);
				JArray val2 = (JArray)val.get_Item("tiles");
				string str = null;
				foreach (JToken item in val2)
				{
					if (((object)item.get_Item((object)"fg")).ToString() == searchingitemid.ToString() || ((object)item.get_Item((object)"bg")).ToString() == searchingitemid.ToString())
					{
						num++;
					}
				}
				if (num > 0)
				{
					str += $"{fileInfo.Name} world has {num.ToString()} number of {searchingitemid.ToString()} items.";
					list.Add(str);
					num = 0;
				}
				str = null;
				val = null;
				val2 = null;
			}
			catch
			{
				MessageBox.Show("An error occurred while getting information from the world's JSON file.\nThis could be because the file " + fileInfo.Name + " was corrupted.\n" + fileInfo.Name + " was not added to list.", "An error occurred", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
			fileInfo = null;
			text = null;
		}
		lstItemsInWorld.DataSource = list;
		lblTotal.Text = lstItemsInWorld.Items.Count.ToString();
		GC.Collect();
		GC.WaitForPendingFinalizers();
	}

	private void lstItemsInWorld_DoubleClick(object sender, EventArgs e)
	{
		if (lstItemsInWorld.SelectedItem != null)
		{
			string text = lstItemsInWorld.SelectedItem.ToString();
			string str = text.Split(' ')[0];
			try
			{
				Process.Start("notepad++.exe", "worlds/" + str);
			}
			catch
			{
				MessageBox.Show("An error occurred while opening the user's txt file.", "An error occurred", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		label4 = new System.Windows.Forms.Label();
		label5 = new System.Windows.Forms.Label();
		btnSort = new System.Windows.Forms.Button();
		txtSort = new System.Windows.Forms.TextBox();
		lblTotal = new System.Windows.Forms.Label();
		label1 = new System.Windows.Forms.Label();
		label3 = new System.Windows.Forms.Label();
		label2 = new System.Windows.Forms.Label();
		lstItemsInWorld = new System.Windows.Forms.ListBox();
		SuspendLayout();
		label4.AutoSize = true;
		label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8f, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, 204);
		label4.Location = new System.Drawing.Point(408, 21);
		label4.Name = "label4";
		label4.Size = new System.Drawing.Size(200, 17);
		label4.TabIndex = 21;
		label4.Text = "you can use sort several times";
		label5.AutoSize = true;
		label5.Font = new System.Drawing.Font("Microsoft YaHei", 7.8f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
		label5.ForeColor = System.Drawing.SystemColors.Desktop;
		label5.Location = new System.Drawing.Point(364, 20);
		label5.Name = "label5";
		label5.Size = new System.Drawing.Size(48, 19);
		label5.TabIndex = 20;
		label5.Text = "Note:";
		btnSort.Cursor = System.Windows.Forms.Cursors.Hand;
		btnSort.Location = new System.Drawing.Point(316, 18);
		btnSort.Name = "btnSort";
		btnSort.Size = new System.Drawing.Size(45, 23);
		btnSort.TabIndex = 19;
		btnSort.Text = "Sort";
		btnSort.UseVisualStyleBackColor = true;
		txtSort.Font = new System.Drawing.Font("Microsoft Sans Serif", 13f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
		txtSort.ForeColor = System.Drawing.SystemColors.InactiveCaption;
		txtSort.Location = new System.Drawing.Point(11, 13);
		txtSort.Name = "txtSort";
		txtSort.Size = new System.Drawing.Size(300, 32);
		txtSort.TabIndex = 18;
		txtSort.Text = "sort by quantity higher than...";
		lblTotal.AutoSize = true;
		lblTotal.Location = new System.Drawing.Point(94, 327);
		lblTotal.Name = "lblTotal";
		lblTotal.Size = new System.Drawing.Size(16, 17);
		lblTotal.TabIndex = 17;
		lblTotal.Text = "0";
		label1.AutoSize = true;
		label1.Location = new System.Drawing.Point(8, 327);
		label1.Name = "label1";
		label1.Size = new System.Drawing.Size(88, 17);
		label1.TabIndex = 16;
		label1.Text = "Total worlds:\n";
		label3.AutoSize = true;
		label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8f, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, 204);
		label3.Location = new System.Drawing.Point(51, 63);
		label3.Name = "label3";
		label3.Size = new System.Drawing.Size(410, 17);
		label3.TabIndex = 15;
		label3.Text = "double click on item if you want to take any actions with this item";
		label2.AutoSize = true;
		label2.Font = new System.Drawing.Font("Microsoft YaHei", 7.8f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
		label2.ForeColor = System.Drawing.SystemColors.Desktop;
		label2.Location = new System.Drawing.Point(7, 62);
		label2.Name = "label2";
		label2.Size = new System.Drawing.Size(48, 19);
		label2.TabIndex = 14;
		label2.Text = "Note:";
		lstItemsInWorld.FormattingEnabled = true;
		lstItemsInWorld.ItemHeight = 16;
		lstItemsInWorld.Location = new System.Drawing.Point(11, 84);
		lstItemsInWorld.Name = "lstItemsInWorld";
		lstItemsInWorld.Size = new System.Drawing.Size(782, 228);
		lstItemsInWorld.TabIndex = 13;
		lstItemsInWorld.DoubleClick += new System.EventHandler(lstItemsInWorld_DoubleClick);
		base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(811, 358);
		base.Controls.Add(label4);
		base.Controls.Add(label5);
		base.Controls.Add(btnSort);
		base.Controls.Add(txtSort);
		base.Controls.Add(lblTotal);
		base.Controls.Add(label1);
		base.Controls.Add(label3);
		base.Controls.Add(label2);
		base.Controls.Add(lstItemsInWorld);
		base.Name = "FindItemInAllWorlds";
		Text = "FindItemInAllWorlds";
		base.Load += new System.EventHandler(FindItemInAllWorlds_Load);
		ResumeLayout(false);
		PerformLayout();
	}
}