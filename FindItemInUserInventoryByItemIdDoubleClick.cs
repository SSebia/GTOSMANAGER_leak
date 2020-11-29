using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Newtonsoft.Json.Linq;

public class FindItemInUserInventoryByItemIdDoubleClick : Form
{
	private string username;

	private int itemdId;

	private int quantity;

	private int pos;

	private string usernamewithoutdot;

	private IContainer components = null;

	private Label label1;

	private Label lblInfo;

	private Button btnRemoveAll;

	private Button btnRemovePart;

	private Button btnSuspend;

	private TextBox txtCount;

	private Button btnCurse;

	private Button btnOpenUsersInvJSON;

	private Button btnOpenUsersJSON;

	public FindItemInUserInventoryByItemIdDoubleClick(string getusername, int getitemdId, int getquantity, int getpos)
	{
		InitializeComponent();
		username = getusername;
		itemdId = getitemdId;
		quantity = getquantity;
		pos = getpos;
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

	private void FindItemInUserInventoryByItemIdDoubleClick_Load(object sender, EventArgs e)
	{
		lblInfo.Text = "Player: " + username + "\nItemID: " + itemdId + "\nQuantity: " + quantity + "\nPosition: " + pos;
		usernamewithoutdot = username.Split('.')[0];
	}

	private void btnRemoveAll_Click(object sender, EventArgs e)
	{
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Expected O, but got Unknown
		if (MessageBox.Show("Are you sure you want to remove all " + itemdId + " items in " + username + " account?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
		{
			string text = File.ReadAllText("inventory/" + username);
			try
			{
				JObject val = JObject.Parse(text);
				JArray val2 = (JArray)val.get_Item("items");
				for (int i = 0; i < 200; i++)
				{
					short num = (short)val2.get_Item(i).get_Item((object)"itemid");
					short num2 = (short)val2.get_Item(i).get_Item((object)"quantity");
					short num3 = (short)val2.get_Item(i).get_Item((object)"aposition");
					if (num == itemdId)
					{
						val2.get_Item(i).set_Item((object)"itemid", JToken.op_Implicit(0));
						val2.get_Item(i).set_Item((object)"quantity", JToken.op_Implicit(0));
						File.WriteAllText("inventory/" + username, ((object)val).ToString());
						MessageBox.Show(itemdId + " item was deleted from this user's inventory successfully.", "Done.", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						break;
					}
				}
			}
			catch
			{
				MessageBox.Show("An error occurred while getting information from the user's inventory JSON file.\nThis could be because the file " + username + " was corrupted.", "An error occurred", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
			Close();
		}
		else
		{
			MessageBox.Show("Remove all " + itemdId + " items was aborted.", "Aborted!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private void btnRemovePart_Click(object sender, EventArgs e)
	{
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Expected O, but got Unknown
		bool flag = false;
		if (!IsDigitsOnly(txtCount.Text))
		{
			MessageBox.Show("Only digitals are available.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			return;
		}
		short num = short.Parse(txtCount.Text);
		if (num > quantity - 1 || num < 1)
		{
			MessageBox.Show("The number should be between 1 - " + (quantity - 1) + ".", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
		else if (MessageBox.Show("Are you sure you want to remove " + quantity + " quantity of " + itemdId + " items in " + username + " account?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
		{
			string text = File.ReadAllText("inventory/" + username);
			try
			{
				JObject val = JObject.Parse(text);
				JArray val2 = (JArray)val.get_Item("items");
				for (int i = 0; i < 200; i++)
				{
					short num2 = (short)val2.get_Item(i).get_Item((object)"itemid");
					short num3 = (short)val2.get_Item(i).get_Item((object)"quantity");
					short num4 = (short)val2.get_Item(i).get_Item((object)"aposition");
					if (num2 == itemdId && num4 == pos && num3 == quantity)
					{
						num3 = (short)(num3 - num);
						val2.get_Item(i).set_Item((object)"quantity", JToken.op_Implicit(num3));
						File.WriteAllText("inventory/" + username, ((object)val).ToString());
						MessageBox.Show(num + " quantity of " + itemdId + " items successfully removed from " + username + ".\nUser has " + num3 + " quantity of " + num2 + " items.\n\nNote: if user online - kick him to update his inventory in the game!", "Succeeded!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						lblInfo.Text = "Player: " + username + "\nItemID: " + itemdId + "\nQuantity: " + num3 + "\nPosition: " + pos;
						quantity = num3;
						break;
					}
				}
			}
			catch
			{
				MessageBox.Show("An error occurred while getting information from the user's inventory JSON file.\nThis could be because the file " + username + " was corrupted.", "An error occurred", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
		else
		{
			MessageBox.Show("Remove was aborted.", "Aborted!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
	}

	private void btnSuspend_Click(object sender, EventArgs e)
	{
		if (File.Exists("timebanned/" + Path.GetFileNameWithoutExtension(username) + ".txt"))
		{
			DialogResult dialogResult = MessageBox.Show("It seems this user is already banned.\n\nWanna open ban's file?", "Error!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
			if (dialogResult == DialogResult.Yes)
			{
				Process.Start(AppDomain.CurrentDomain.BaseDirectory + "/timebanned/" + Path.GetFileNameWithoutExtension(username) + ".txt");
			}
			return;
		}
		string text = Interaction.InputBox("How much minutes suspend? (1 - 525948)", "Minutes?", "60");
		if (text.Length <= 0)
		{
			return;
		}
		bool flag = false;
		if (!IsDigitsOnly(text))
		{
			MessageBox.Show("Only digitals are available.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			return;
		}
		int num = int.Parse(text);
		if (num < 1 || num > 525948)
		{
			MessageBox.Show("Minutes should be (1 - 525948)", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			return;
		}
		long num2 = DateTimeOffset.UtcNow.ToUnixTimeSeconds() + num * 60;
		string text2 = "GTOS";
		string text3 = "banned through GTOS management app";
		string contents = num2 + Environment.NewLine + text2 + Environment.NewLine + text3;
		File.WriteAllText("timebanned/" + Path.GetFileNameWithoutExtension(username) + ".txt", contents);
		MessageBox.Show(username + " successfully suspended for " + num + " minutes.\n\nNote: if user online - kick him to update his status in the game!", "Succeeded!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
	}

	private void btnCurse_Click(object sender, EventArgs e)
	{
		if (File.Exists("cursedplayers/" + Path.GetFileNameWithoutExtension(username) + ".txt"))
		{
			DialogResult dialogResult = MessageBox.Show("It seems this user is already cursed.\n\nWanna open cursed's file?", "Error!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
			if (dialogResult == DialogResult.Yes)
			{
				Process.Start(AppDomain.CurrentDomain.BaseDirectory + "/cursedplayers/" + Path.GetFileNameWithoutExtension(username) + ".txt");
			}
			return;
		}
		string text = Interaction.InputBox("How much minutes curse? (1 - 43829)", "Minutes?", "60");
		if (text.Length <= 0)
		{
			return;
		}
		bool flag = false;
		if (!IsDigitsOnly(text))
		{
			MessageBox.Show("Only digitals are available.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			return;
		}
		int num = int.Parse(text);
		if (num < 1 || num > 43829)
		{
			MessageBox.Show("Minutes should be (1 - 43829)", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			return;
		}
		int num2 = num;
		File.WriteAllText("cursedplayers/" + Path.GetFileNameWithoutExtension(username) + ".txt", num2.ToString());
		MessageBox.Show(username + " successfully cursed for " + num + " minutes.\n\nNote: if user online - kick him to update his status in the game!", "Succeeded!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
	}

	private void btnOpenUsersInvJSON_Click(object sender, EventArgs e)
	{
		try
		{
			Process.Start("notepad++.exe", "inventory/" + username);
		}
		catch
		{
			MessageBox.Show("An error occurred while opening the user's JSON file.", "An error occurred", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void btnOpenUsersJSON_Click(object sender, EventArgs e)
	{
		try
		{
			Process.Start("notepad++.exe", "players/" + username);
		}
		catch
		{
			MessageBox.Show("An error occurred while opening the user's JSON file.", "An error occurred", MessageBoxButtons.OK, MessageBoxIcon.Hand);
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
		label1 = new System.Windows.Forms.Label();
		lblInfo = new System.Windows.Forms.Label();
		btnRemoveAll = new System.Windows.Forms.Button();
		btnRemovePart = new System.Windows.Forms.Button();
		btnSuspend = new System.Windows.Forms.Button();
		txtCount = new System.Windows.Forms.TextBox();
		btnCurse = new System.Windows.Forms.Button();
		btnOpenUsersInvJSON = new System.Windows.Forms.Button();
		btnOpenUsersJSON = new System.Windows.Forms.Button();
		SuspendLayout();
		label1.AutoSize = true;
		label1.Font = new System.Drawing.Font("Modern No. 20", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		label1.Location = new System.Drawing.Point(12, 9);
		label1.Name = "label1";
		label1.Size = new System.Drawing.Size(126, 22);
		label1.TabIndex = 0;
		label1.Text = "Information:";
		lblInfo.AutoSize = true;
		lblInfo.Location = new System.Drawing.Point(34, 42);
		lblInfo.Name = "lblInfo";
		lblInfo.Size = new System.Drawing.Size(31, 17);
		lblInfo.TabIndex = 1;
		lblInfo.Text = "info";
		btnRemoveAll.Location = new System.Drawing.Point(263, 14);
		btnRemoveAll.Name = "btnRemoveAll";
		btnRemoveAll.Size = new System.Drawing.Size(136, 45);
		btnRemoveAll.TabIndex = 2;
		btnRemoveAll.Text = "Remove All these Items";
		btnRemoveAll.UseVisualStyleBackColor = true;
		btnRemoveAll.Click += new System.EventHandler(btnRemoveAll_Click);
		btnRemovePart.Location = new System.Drawing.Point(263, 78);
		btnRemovePart.Name = "btnRemovePart";
		btnRemovePart.Size = new System.Drawing.Size(136, 45);
		btnRemovePart.TabIndex = 3;
		btnRemovePart.Text = "Remove a part of these items";
		btnRemovePart.UseVisualStyleBackColor = true;
		btnRemovePart.Click += new System.EventHandler(btnRemovePart_Click);
		btnSuspend.Location = new System.Drawing.Point(263, 141);
		btnSuspend.Name = "btnSuspend";
		btnSuspend.Size = new System.Drawing.Size(136, 45);
		btnSuspend.TabIndex = 4;
		btnSuspend.Text = "Suspend Account";
		btnSuspend.UseVisualStyleBackColor = true;
		btnSuspend.Click += new System.EventHandler(btnSuspend_Click);
		txtCount.Location = new System.Drawing.Point(405, 89);
		txtCount.Name = "txtCount";
		txtCount.Size = new System.Drawing.Size(100, 22);
		txtCount.TabIndex = 5;
		txtCount.Text = "Count";
		btnCurse.Location = new System.Drawing.Point(263, 203);
		btnCurse.Name = "btnCurse";
		btnCurse.Size = new System.Drawing.Size(136, 45);
		btnCurse.TabIndex = 6;
		btnCurse.Text = "Curse Account";
		btnCurse.UseVisualStyleBackColor = true;
		btnCurse.Click += new System.EventHandler(btnCurse_Click);
		btnOpenUsersInvJSON.Location = new System.Drawing.Point(263, 271);
		btnOpenUsersInvJSON.Name = "btnOpenUsersInvJSON";
		btnOpenUsersInvJSON.Size = new System.Drawing.Size(136, 64);
		btnOpenUsersInvJSON.TabIndex = 7;
		btnOpenUsersInvJSON.Text = "Open user's JSON inventory in notepad++";
		btnOpenUsersInvJSON.UseVisualStyleBackColor = true;
		btnOpenUsersInvJSON.Click += new System.EventHandler(btnOpenUsersInvJSON_Click);
		btnOpenUsersJSON.Location = new System.Drawing.Point(263, 351);
		btnOpenUsersJSON.Name = "btnOpenUsersJSON";
		btnOpenUsersJSON.Size = new System.Drawing.Size(136, 60);
		btnOpenUsersJSON.TabIndex = 8;
		btnOpenUsersJSON.Text = "Open user's JSON in notepad++";
		btnOpenUsersJSON.UseVisualStyleBackColor = true;
		btnOpenUsersJSON.Click += new System.EventHandler(btnOpenUsersJSON_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(529, 423);
		base.Controls.Add(btnOpenUsersJSON);
		base.Controls.Add(btnOpenUsersInvJSON);
		base.Controls.Add(btnCurse);
		base.Controls.Add(txtCount);
		base.Controls.Add(btnSuspend);
		base.Controls.Add(btnRemovePart);
		base.Controls.Add(btnRemoveAll);
		base.Controls.Add(lblInfo);
		base.Controls.Add(label1);
		base.Name = "FindItemInUserInventoryByItemIdDoubleClick";
		Text = "FindItemInUserInventoryByItemIdDoubleClick";
		base.Load += new System.EventHandler(FindItemInUserInventoryByItemIdDoubleClick_Load);
		ResumeLayout(false);
		PerformLayout();
	}
}