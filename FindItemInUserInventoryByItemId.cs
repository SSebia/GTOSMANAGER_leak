using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using GTOSmanagement;
using Newtonsoft.Json.Linq;

public class FindItemInUserInventoryByItemId : Form
{
	private int searchingItemID;

	private IContainer components = null;

	private ListBox lstUsersItemId;

	private Label label3;

	private Label label2;

	private Label lblTotal;

	private Label label1;

	private TextBox txtSort;

	private Button btnSort;

	private Label label4;

	private Label label5;

	private Button btnRemoveAll;

	public FindItemInUserInventoryByItemId(int srcItemId)
	{
		InitializeComponent();
		searchingItemID = srcItemId;
	}

	private bool IsDigitsOnly(string str)
	{
		foreach (char c in str)
		{
			if (c < '0' || c > '9')
			{
				return false;
			}
		}
		return true;
	}

	private void FindItemInUserInventoryByItemId_Load(object sender, EventArgs e)
	{
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Expected O, but got Unknown
		List<string> list = new List<string>();
		int num = Directory.GetFiles("inventory", "*", SearchOption.TopDirectoryOnly).Length;
		DirectoryInfo directoryInfo = new DirectoryInfo("inventory");
		for (int i = 0; i < num; i++)
		{
			FileInfo fileInfo = directoryInfo.GetFiles()[i];
			string text = File.ReadAllText("inventory/" + fileInfo.Name);
			JObject val = JObject.Parse(text);
			JArray val2 = (JArray)val.get_Item("items");
			string str = null;
			for (int j = 0; j < 200; j++)
			{
				short num2 = (short)val2.get_Item(j).get_Item((object)"itemid");
				string text2 = ((object)val2.get_Item(j).get_Item((object)"quantity")).ToString();
				short num3 = (short)val2.get_Item(j).get_Item((object)"aposition");
				if (num2 == searchingItemID)
				{
					str += $"User {fileInfo.Name} has {text2} number of {num2} items. This item is on position {num3} in JSON file.";
					list.Add(str);
					break;
				}
			}
		}
		lstUsersItemId.DataSource = list;
		lblTotal.Text = lstUsersItemId.Items.Count.ToString();
	}

	private void txtSort_Enter(object sender, EventArgs e)
	{
		if (txtSort.Text == "sort by quantity higher than...")
		{
			txtSort.Text = "";
			txtSort.ForeColor = Color.Black;
		}
	}

	private void txtSort_Leave(object sender, EventArgs e)
	{
		if (txtSort.Text == "")
		{
			txtSort.Text = "sort by quantity higher than...";
			txtSort.ForeColor = Color.Silver;
		}
	}

	private void txtSort_TextChanged(object sender, EventArgs e)
	{
	}

	private void btnSort_Click(object sender, EventArgs e)
	{
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Expected O, but got Unknown
		bool flag = false;
		if (!IsDigitsOnly(txtSort.Text))
		{
			MessageBox.Show("Only digitals are available.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			return;
		}
		lstUsersItemId.DataSource = null;
		lstUsersItemId.Items.Clear();
		List<string> list = new List<string>();
		int num = Directory.GetFiles("inventory", "*", SearchOption.TopDirectoryOnly).Length;
		DirectoryInfo directoryInfo = new DirectoryInfo("inventory");
		for (int i = 0; i < num; i++)
		{
			FileInfo fileInfo = directoryInfo.GetFiles()[i];
			string text = File.ReadAllText("inventory/" + fileInfo.Name);
			try
			{
				JObject val = JObject.Parse(text);
				JArray val2 = (JArray)val.get_Item("items");
				string str = null;
				for (int j = 0; j < 200; j++)
				{
					short num2 = (short)val2.get_Item(j).get_Item((object)"itemid");
					short num3 = (short)val2.get_Item(j).get_Item((object)"quantity");
					short num4 = (short)val2.get_Item(j).get_Item((object)"aposition");
					if (num2 == searchingItemID && num3 >= int.Parse(txtSort.Text))
					{
						str += $"User {fileInfo.Name} has {num3} number of {num2} items. This item is on position {num4} in JSON file.";
						list.Add(str);
						break;
					}
				}
			}
			catch
			{
				MessageBox.Show("An error occurred while getting information from the user's inventory JSON file.\nThis could be because the file " + fileInfo.Name + " was corrupted.\n" + fileInfo.Name + " was not added to list.", "An error occurred", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
		lstUsersItemId.DataSource = list;
		lblTotal.Text = lstUsersItemId.Items.Count.ToString();
	}

	private void lstUsersItemId_DoubleClick(object sender, EventArgs e)
	{
		if (lstUsersItemId.SelectedItem != null)
		{
			string text = lstUsersItemId.SelectedItem.ToString();
			string getusername = text.Split(' ')[1];
			int getitemdId = searchingItemID;
			int getquantity = int.Parse(text.Split(' ')[3]);
			int getpos = int.Parse(text.Split(' ')[13]);
			FindItemInUserInventoryByItemIdDoubleClick findItemInUserInventoryByItemIdDoubleClick = new FindItemInUserInventoryByItemIdDoubleClick(getusername, getitemdId, getquantity, getpos);
			findItemInUserInventoryByItemIdDoubleClick.ShowDialog();
		}
	}

	private void btnRemoveAll_Click(object sender, EventArgs e)
	{
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Expected O, but got Unknown
		DialogResult dialogResult = MessageBox.Show("Are you sure you want to remove id (" + searchingItemID + ") item in all these users inventories?\n\nMake sure that these users are offline.", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
		if (dialogResult != DialogResult.Yes)
		{
			return;
		}
		foreach (object item in lstUsersItemId.Items)
		{
			string text = item.ToString();
			string str = text.Split(' ')[1];
			string text2 = File.ReadAllText("inventory/" + str);
			try
			{
				JObject val = JObject.Parse(text2);
				JArray val2 = (JArray)val.get_Item("items");
				for (int i = 0; i < 200; i++)
				{
					short num = (short)val2.get_Item(i).get_Item((object)"itemid");
					short num2 = (short)val2.get_Item(i).get_Item((object)"quantity");
					short num3 = (short)val2.get_Item(i).get_Item((object)"aposition");
					if (num == searchingItemID)
					{
						val2.get_Item(i).set_Item((object)"itemid", JToken.op_Implicit(0));
						val2.get_Item(i).set_Item((object)"quantity", JToken.op_Implicit(0));
						File.WriteAllText("inventory/" + str, ((object)val).ToString());
						break;
					}
				}
			}
			catch
			{
				MessageBox.Show("An error occurred while getting information from the user's inventory JSON file.\nThis could be because the file " + str + " was corrupted.", "An error occurred", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
		MessageBox.Show(searchingItemID + " item was deleted from all users` inventories successfully.", "Done.", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		Close();
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
		lstUsersItemId = new System.Windows.Forms.ListBox();
		label3 = new System.Windows.Forms.Label();
		label2 = new System.Windows.Forms.Label();
		lblTotal = new System.Windows.Forms.Label();
		label1 = new System.Windows.Forms.Label();
		txtSort = new System.Windows.Forms.TextBox();
		btnSort = new System.Windows.Forms.Button();
		label4 = new System.Windows.Forms.Label();
		label5 = new System.Windows.Forms.Label();
		btnRemoveAll = new System.Windows.Forms.Button();
		SuspendLayout();
		lstUsersItemId.FormattingEnabled = true;
		lstUsersItemId.ItemHeight = 16;
		lstUsersItemId.Location = new System.Drawing.Point(19, 93);
		lstUsersItemId.Name = "lstUsersItemId";
		lstUsersItemId.Size = new System.Drawing.Size(782, 228);
		lstUsersItemId.TabIndex = 0;
		lstUsersItemId.DoubleClick += new System.EventHandler(lstUsersItemId_DoubleClick);
		label3.AutoSize = true;
		label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8f, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, 204);
		label3.Location = new System.Drawing.Point(59, 72);
		label3.Name = "label3";
		label3.Size = new System.Drawing.Size(414, 17);
		label3.TabIndex = 6;
		label3.Text = "double click on user if you want to take any actions with this user";
		label2.AutoSize = true;
		label2.Font = new System.Drawing.Font("Microsoft YaHei", 7.8f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
		label2.ForeColor = System.Drawing.SystemColors.Desktop;
		label2.Location = new System.Drawing.Point(15, 71);
		label2.Name = "label2";
		label2.Size = new System.Drawing.Size(48, 19);
		label2.TabIndex = 5;
		label2.Text = "Note:";
		lblTotal.AutoSize = true;
		lblTotal.Location = new System.Drawing.Point(96, 336);
		lblTotal.Name = "lblTotal";
		lblTotal.Size = new System.Drawing.Size(16, 17);
		lblTotal.TabIndex = 8;
		lblTotal.Text = "0";
		label1.AutoSize = true;
		label1.Location = new System.Drawing.Point(16, 336);
		label1.Name = "label1";
		label1.Size = new System.Drawing.Size(83, 17);
		label1.TabIndex = 7;
		label1.Text = "Total users:\n";
		txtSort.Font = new System.Drawing.Font("Microsoft Sans Serif", 13f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
		txtSort.ForeColor = System.Drawing.SystemColors.InactiveCaption;
		txtSort.Location = new System.Drawing.Point(19, 22);
		txtSort.Name = "txtSort";
		txtSort.Size = new System.Drawing.Size(300, 32);
		txtSort.TabIndex = 9;
		txtSort.Text = "sort by quantity higher than...";
		txtSort.TextChanged += new System.EventHandler(txtSort_TextChanged);
		txtSort.Enter += new System.EventHandler(txtSort_Enter);
		txtSort.Leave += new System.EventHandler(txtSort_Leave);
		btnSort.Cursor = System.Windows.Forms.Cursors.Hand;
		btnSort.Location = new System.Drawing.Point(324, 27);
		btnSort.Name = "btnSort";
		btnSort.Size = new System.Drawing.Size(45, 23);
		btnSort.TabIndex = 10;
		btnSort.Text = "Sort";
		btnSort.UseVisualStyleBackColor = true;
		btnSort.Click += new System.EventHandler(btnSort_Click);
		label4.AutoSize = true;
		label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8f, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, 204);
		label4.Location = new System.Drawing.Point(416, 30);
		label4.Name = "label4";
		label4.Size = new System.Drawing.Size(200, 17);
		label4.TabIndex = 12;
		label4.Text = "you can use sort several times";
		label5.AutoSize = true;
		label5.Font = new System.Drawing.Font("Microsoft YaHei", 7.8f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
		label5.ForeColor = System.Drawing.SystemColors.Desktop;
		label5.Location = new System.Drawing.Point(372, 29);
		label5.Name = "label5";
		label5.Size = new System.Drawing.Size(48, 19);
		label5.TabIndex = 11;
		label5.Text = "Note:";
		btnRemoveAll.Location = new System.Drawing.Point(695, 336);
		btnRemoveAll.Name = "btnRemoveAll";
		btnRemoveAll.Size = new System.Drawing.Size(106, 43);
		btnRemoveAll.TabIndex = 13;
		btnRemoveAll.Text = "Remove all";
		btnRemoveAll.UseVisualStyleBackColor = true;
		btnRemoveAll.Click += new System.EventHandler(btnRemoveAll_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(813, 391);
		base.Controls.Add(btnRemoveAll);
		base.Controls.Add(label4);
		base.Controls.Add(label5);
		base.Controls.Add(btnSort);
		base.Controls.Add(txtSort);
		base.Controls.Add(lblTotal);
		base.Controls.Add(label1);
		base.Controls.Add(label3);
		base.Controls.Add(label2);
		base.Controls.Add(lstUsersItemId);
		base.Name = "FindItemInUserInventoryByItemId";
		Text = "FindItemInUserInventoryByItemId";
		base.Load += new System.EventHandler(FindItemInUserInventoryByItemId_Load);
		ResumeLayout(false);
		PerformLayout();
	}
}