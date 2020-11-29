using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using GTOSmanagement;
using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;

public class ShowEverythingAssociatedWithGrowID : Form
{
	private string growid;

	private IContainer components = null;

	private ListBox lstOwned;

	private Label label1;

	private ListBox lstGems;

	private Label label2;

	private Label label3;

	private Label label4;

	private ListBox lstInventory;

	private Label label5;

	private ListBox lstAccessed;

	private Label label6;

	private Label label7;

	private Button btnDrestroyEverything;

	private Label lblTotalOwned;

	private Label label8;

	private Label lblGrowID;

	private ListBox lstAccount;

	public ShowEverythingAssociatedWithGrowID(string grow)
	{
		InitializeComponent();
		growid = grow;
	}

	private void ShowEverythingAssociatedWithGrowID_Load(object sender, EventArgs e)
	{
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Expected O, but got Unknown
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Expected O, but got Unknown
		//IL_035b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0362: Expected O, but got Unknown
		lblGrowID.Text = growid;
		lstGems.DataSource = null;
		lstGems.Items.Clear();
		string text = "";
		if (!File.Exists("gemdb/" + growid + ".txt"))
		{
			text = "[INFO]: " + growid + ".txt is not exists.";
		}
		else
		{
			string value = File.ReadAllText("gemdb/" + growid + ".txt");
			text = ((!string.IsNullOrEmpty(value)) ? (growid + ".txt") : ("[INFO]: " + growid + ".txt nothing contains."));
		}
		lstGems.Items.Add(text);
		lstAccount.DataSource = null;
		lstAccount.Items.Clear();
		DataTable dataTable = new DataTable();
		db db = new db();
		MySqlCommand val = new MySqlCommand("select * from PlayerDatabase where username=@name;", db.Connection);
		try
		{
			((DbConnection)(object)db.Connection).Open();
			val.get_Parameters().AddWithValue("@name", (object)growid);
			MySqlDataAdapter val2 = new MySqlDataAdapter(val);
			try
			{
				((DbDataAdapter)(object)val2).Fill(dataTable);
			}
			finally
			{
				((IDisposable)val2)?.Dispose();
			}
			((DbConnection)(object)db.Connection).Close();
			db = null;
		}
		finally
		{
			((IDisposable)val)?.Dispose();
		}
		if (dataTable.Rows.Count < 1)
		{
			lstAccount.Items.Add("[INFO]: " + growid + " account in mysql is not exists.");
		}
		else
		{
			foreach (DataRow row in dataTable.Rows)
			{
				for (int i = 0; i < row.ItemArray.Length; i++)
				{
					lstAccount.Items.Add(dataTable.Columns[i].ColumnName + ": " + row.ItemArray[i].ToString());
				}
			}
		}
		lstInventory.DataSource = null;
		lstInventory.Items.Clear();
		string item = "[INFO]: " + growid + ".json inventory is not exists.";
		if (File.Exists("inventory/" + growid + ".json"))
		{
			item = growid + ".json";
		}
		lstInventory.Items.Add(item);
		int num = Directory.GetFiles("worlds", "*", SearchOption.TopDirectoryOnly).Length;
		DirectoryInfo directoryInfo = new DirectoryInfo("worlds");
		int num2 = 0;
		for (int j = 0; j < num; j++)
		{
			FileInfo fileInfo = directoryInfo.GetFiles()[j];
			try
			{
				string text2 = File.ReadAllText("worlds/" + fileInfo.Name);
				string text3 = "";
				string text4 = "";
				string text5 = "";
				string text6 = "";
				JObject val3 = JObject.Parse(text2);
				JArray val4 = (JArray)val3.get_Item("access");
				text3 = (string)val3.get_Item("name");
				text5 = (string)val3.get_Item("Displayowner");
				text4 = (string)val3.get_Item("owner");
				foreach (JToken item2 in val4)
				{
					text6 = text6 + ((object)item2)?.ToString() + " ";
				}
				if (text4 == growid || text5 == growid)
				{
					lstOwned.Items.Add(text3 + ".json");
					num2++;
				}
				if (text6.Contains(growid))
				{
					lstAccessed.Items.Add(text3 + ".json");
				}
				text3 = "";
				text4 = "";
				text5 = "";
				text6 = "";
				val3 = null;
				val4 = null;
			}
			catch
			{
				MessageBox.Show("An error occurred while getting information from the world's JSON file.\nThis could be because the file " + fileInfo.Name + " was corrupted.\n" + fileInfo.Name + " was not added to list.", "An error occurred", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
			lblTotalOwned.Text = "Total owned: " + num2;
		}
	}

	private void btnDrestroyEverything_Click(object sender, EventArgs e)
	{
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Expected O, but got Unknown
		//IL_02d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02df: Expected O, but got Unknown
		Process[] processesByName = Process.GetProcessesByName("enet");
		if (processesByName.Length != 0)
		{
			MessageBox.Show("Enet server is running! Please close it.", "enet server is running!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			return;
		}
		if (!lstInventory.Items[0].ToString().Contains("[INFO]"))
		{
			try
			{
				File.Delete("inventory/" + lstInventory.Items[0].ToString());
			}
			catch (Exception)
			{
				MessageBox.Show("An error occurred while deleting inventory/" + lstInventory.Items[0].ToString() + " Is it exists?", "An error occurred", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
		if (!lstAccount.Items[0].ToString().Contains("[INFO]"))
		{
			db db = new db();
			MySqlCommand val = new MySqlCommand("DELETE from PlayerDatabase where username=@name;", db.Connection);
			try
			{
				val.get_Parameters().AddWithValue("@name", (object)lstAccount.Items[0].ToString());
				((DbConnection)(object)db.Connection).Open();
				((DbCommand)(object)val).ExecuteNonQuery();
				((DbConnection)(object)db.Connection).Close();
				db = null;
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
		}
		if (!lstGems.Items[0].ToString().Contains("[INFO]"))
		{
			try
			{
				File.Delete("gemdb/" + lstGems.Items[0].ToString());
			}
			catch (Exception)
			{
				MessageBox.Show("An error occurred while deleting gemdb/" + lstGems.Items[0].ToString() + " Is it exists?", "An error occurred", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
		foreach (object item in lstOwned.Items)
		{
			try
			{
				File.Delete("worlds/" + item);
			}
			catch (Exception)
			{
				MessageBox.Show("An error occurred while deleting worlds/" + item?.ToString() + " Is it exists?", "An error occurred", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
		foreach (object item2 in lstAccessed.Items)
		{
			try
			{
				List<string> list = new List<string>();
				string text = File.ReadAllText("worlds/" + item2);
				JObject val2 = JObject.Parse(text);
				JArray val3 = (JArray)val2.get_Item("access");
				foreach (JToken item3 in val3)
				{
					list.Add(((object)item3).ToString());
				}
				if (list.Contains(growid))
				{
					list.Remove(growid);
				}
				val3.Clear();
				foreach (string item4 in list)
				{
					val3.Add(JToken.op_Implicit(item4));
				}
				val2.set_Item("access", (JToken)(object)val3);
				File.WriteAllText("worlds/" + item2, ((object)val2).ToString());
			}
			catch (Exception)
			{
				MessageBox.Show("An error occurred while deleting accessed player (" + growid + ") from worlds/" + item2?.ToString() + " Is it exists?", "An error occurred", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
		MessageBox.Show("Successfully.", "Done", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		Close();
	}

	private void label7_Click(object sender, EventArgs e)
	{
	}

	private void label6_Click(object sender, EventArgs e)
	{
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
		lstOwned = new System.Windows.Forms.ListBox();
		label1 = new System.Windows.Forms.Label();
		lstGems = new System.Windows.Forms.ListBox();
		label2 = new System.Windows.Forms.Label();
		label3 = new System.Windows.Forms.Label();
		label4 = new System.Windows.Forms.Label();
		lstInventory = new System.Windows.Forms.ListBox();
		label5 = new System.Windows.Forms.Label();
		lstAccessed = new System.Windows.Forms.ListBox();
		label6 = new System.Windows.Forms.Label();
		label7 = new System.Windows.Forms.Label();
		btnDrestroyEverything = new System.Windows.Forms.Button();
		lblTotalOwned = new System.Windows.Forms.Label();
		label8 = new System.Windows.Forms.Label();
		lblGrowID = new System.Windows.Forms.Label();
		lstAccount = new System.Windows.Forms.ListBox();
		SuspendLayout();
		lstOwned.FormattingEnabled = true;
		lstOwned.ItemHeight = 16;
		lstOwned.Location = new System.Drawing.Point(9, 104);
		lstOwned.Name = "lstOwned";
		lstOwned.Size = new System.Drawing.Size(305, 164);
		lstOwned.TabIndex = 0;
		label1.AutoSize = true;
		label1.Location = new System.Drawing.Point(9, 83);
		label1.Name = "label1";
		label1.Size = new System.Drawing.Size(100, 17);
		label1.TabIndex = 1;
		label1.Text = "Owned worlds:";
		lstGems.FormattingEnabled = true;
		lstGems.ItemHeight = 16;
		lstGems.Location = new System.Drawing.Point(12, 320);
		lstGems.Name = "lstGems";
		lstGems.Size = new System.Drawing.Size(305, 68);
		lstGems.TabIndex = 2;
		label2.AutoSize = true;
		label2.Location = new System.Drawing.Point(9, 300);
		label2.Name = "label2";
		label2.Size = new System.Drawing.Size(64, 17);
		label2.TabIndex = 3;
		label2.Text = "gems file";
		label3.AutoSize = true;
		label3.Location = new System.Drawing.Point(9, 409);
		label3.Name = "label3";
		label3.Size = new System.Drawing.Size(98, 17);
		label3.TabIndex = 5;
		label3.Text = "account mysql";
		label4.AutoSize = true;
		label4.Location = new System.Drawing.Point(376, 300);
		label4.Name = "label4";
		label4.Size = new System.Drawing.Size(118, 17);
		label4.TabIndex = 7;
		label4.Text = "inventory json file";
		lstInventory.FormattingEnabled = true;
		lstInventory.ItemHeight = 16;
		lstInventory.Location = new System.Drawing.Point(379, 320);
		lstInventory.Name = "lstInventory";
		lstInventory.Size = new System.Drawing.Size(305, 68);
		lstInventory.TabIndex = 6;
		label5.AutoSize = true;
		label5.Location = new System.Drawing.Point(376, 83);
		label5.Name = "label5";
		label5.Size = new System.Drawing.Size(117, 17);
		label5.TabIndex = 9;
		label5.Text = "Accessed worlds:";
		lstAccessed.FormattingEnabled = true;
		lstAccessed.ItemHeight = 16;
		lstAccessed.Location = new System.Drawing.Point(376, 104);
		lstAccessed.Name = "lstAccessed";
		lstAccessed.Size = new System.Drawing.Size(305, 164);
		lstAccessed.TabIndex = 8;
		label6.AutoSize = true;
		label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8f, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, 204);
		label6.Location = new System.Drawing.Point(52, 9);
		label6.Name = "label6";
		label6.Size = new System.Drawing.Size(340, 17);
		label6.TabIndex = 11;
		label6.Text = "double click on file if you want to view it in notepad++";
		label6.Click += new System.EventHandler(label6_Click);
		label7.AutoSize = true;
		label7.Font = new System.Drawing.Font("Microsoft YaHei", 7.8f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
		label7.ForeColor = System.Drawing.SystemColors.Desktop;
		label7.Location = new System.Drawing.Point(8, 8);
		label7.Name = "label7";
		label7.Size = new System.Drawing.Size(48, 19);
		label7.TabIndex = 10;
		label7.Text = "Note:";
		label7.Click += new System.EventHandler(label7_Click);
		btnDrestroyEverything.Location = new System.Drawing.Point(379, 429);
		btnDrestroyEverything.Name = "btnDrestroyEverything";
		btnDrestroyEverything.Size = new System.Drawing.Size(225, 69);
		btnDrestroyEverything.TabIndex = 12;
		btnDrestroyEverything.Text = "Destroy everything associated with this growid";
		btnDrestroyEverything.UseVisualStyleBackColor = true;
		btnDrestroyEverything.Click += new System.EventHandler(btnDrestroyEverything_Click);
		lblTotalOwned.AutoSize = true;
		lblTotalOwned.Location = new System.Drawing.Point(11, 271);
		lblTotalOwned.Name = "lblTotalOwned";
		lblTotalOwned.Size = new System.Drawing.Size(89, 17);
		lblTotalOwned.TabIndex = 13;
		lblTotalOwned.Text = "Total owned:";
		label8.AutoSize = true;
		label8.Font = new System.Drawing.Font("Microsoft Uighur", 10.2f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		label8.Location = new System.Drawing.Point(9, 48);
		label8.Name = "label8";
		label8.Size = new System.Drawing.Size(52, 24);
		label8.TabIndex = 14;
		label8.Text = "GrowID:";
		lblGrowID.AutoSize = true;
		lblGrowID.Font = new System.Drawing.Font("Microsoft YaHei", 7.8f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		lblGrowID.Location = new System.Drawing.Point(59, 49);
		lblGrowID.Name = "lblGrowID";
		lblGrowID.Size = new System.Drawing.Size(91, 19);
		lblGrowID.TabIndex = 15;
		lblGrowID.Text = "Growidhere";
		lstAccount.FormattingEnabled = true;
		lstAccount.ItemHeight = 16;
		lstAccount.Location = new System.Drawing.Point(12, 429);
		lstAccount.Name = "lstAccount";
		lstAccount.Size = new System.Drawing.Size(305, 180);
		lstAccount.TabIndex = 4;
		base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(725, 628);
		base.Controls.Add(lblGrowID);
		base.Controls.Add(label8);
		base.Controls.Add(lblTotalOwned);
		base.Controls.Add(btnDrestroyEverything);
		base.Controls.Add(label6);
		base.Controls.Add(label7);
		base.Controls.Add(label5);
		base.Controls.Add(lstAccessed);
		base.Controls.Add(label4);
		base.Controls.Add(lstInventory);
		base.Controls.Add(label3);
		base.Controls.Add(lstAccount);
		base.Controls.Add(label2);
		base.Controls.Add(lstGems);
		base.Controls.Add(label1);
		base.Controls.Add(lstOwned);
		base.Name = "ShowEverythingAssociatedWithGrowID";
		Text = "ShowEverythingAssociatedWithGrowID";
		base.Load += new System.EventHandler(ShowEverythingAssociatedWithGrowID_Load);
		ResumeLayout(false);
		PerformLayout();
	}
}