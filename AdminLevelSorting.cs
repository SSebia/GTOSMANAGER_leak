using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Drawing;
using System.Windows.Forms;
using GTOSmanagement;
using MySql.Data.MySqlClient;

public class AdminLevelSorting : Form
{
	private int adminLevel;

	private IContainer components = null;

	private ListBox lstUserNames;

	private Label label1;

	private Label lblTotal;

	private Label label2;

	private Label label3;

	public AdminLevelSorting(int admLevel)
	{
		InitializeComponent();
		adminLevel = admLevel;
	}

	private void AdminLevelSorting_Load(object sender, EventArgs e)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Expected O, but got Unknown
		List<string> list = new List<string>();
		db db = new db();
		MySqlCommand val = new MySqlCommand("select username from PlayerDatabase where adminLevel=@admlvl;", db.Connection);
		try
		{
			((DbConnection)(object)db.Connection).Open();
			val.get_Parameters().AddWithValue("@admlvl", (object)adminLevel.ToString());
			MySqlDataReader val2 = val.ExecuteReader();
			try
			{
				while (((DbDataReader)(object)val2).Read())
				{
					list.Add($"User {((DbDataReader)(object)val2).GetString(0)} is {adminLevel.ToString()} Admin Level.");
				}
			}
			finally
			{
				((IDisposable)val2)?.Dispose();
			}
		}
		finally
		{
			((IDisposable)val)?.Dispose();
		}
		lstUserNames.DataSource = list;
		lblTotal.Text = lstUserNames.Items.Count.ToString();
		((DbConnection)(object)db.Connection).Close();
		db = null;
	}

	private void lstUserNames_DoubleClick(object sender, EventArgs e)
	{
		if (lstUserNames.SelectedItem != null)
		{
			string text = lstUserNames.SelectedItem.ToString();
			string usrName = text.Split(' ')[1];
			int admLevel = int.Parse(text.Split(' ')[3].ToString());
			ChangeAdminLevel changeAdminLevel = new ChangeAdminLevel(usrName, admLevel);
			changeAdminLevel.ShowDialog();
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
		lstUserNames = new System.Windows.Forms.ListBox();
		label1 = new System.Windows.Forms.Label();
		lblTotal = new System.Windows.Forms.Label();
		label2 = new System.Windows.Forms.Label();
		label3 = new System.Windows.Forms.Label();
		SuspendLayout();
		lstUserNames.FormattingEnabled = true;
		lstUserNames.ItemHeight = 16;
		lstUserNames.Location = new System.Drawing.Point(25, 48);
		lstUserNames.Name = "lstUserNames";
		lstUserNames.Size = new System.Drawing.Size(496, 196);
		lstUserNames.TabIndex = 0;
		lstUserNames.DoubleClick += new System.EventHandler(lstUserNames_DoubleClick);
		label1.AutoSize = true;
		label1.Location = new System.Drawing.Point(22, 260);
		label1.Name = "label1";
		label1.Size = new System.Drawing.Size(83, 17);
		label1.TabIndex = 1;
		label1.Text = "Total users:\n";
		lblTotal.AutoSize = true;
		lblTotal.Location = new System.Drawing.Point(102, 260);
		lblTotal.Name = "lblTotal";
		lblTotal.Size = new System.Drawing.Size(16, 17);
		lblTotal.TabIndex = 2;
		lblTotal.Text = "0";
		label2.AutoSize = true;
		label2.Font = new System.Drawing.Font("Microsoft YaHei", 7.8f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
		label2.ForeColor = System.Drawing.SystemColors.Desktop;
		label2.Location = new System.Drawing.Point(22, 19);
		label2.Name = "label2";
		label2.Size = new System.Drawing.Size(48, 19);
		label2.TabIndex = 3;
		label2.Text = "Note:";
		label3.AutoSize = true;
		label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8f, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, 204);
		label3.Location = new System.Drawing.Point(66, 20);
		label3.Name = "label3";
		label3.Size = new System.Drawing.Size(426, 17);
		label3.TabIndex = 4;
		label3.Text = "double click on user if you want to change AdminLevel or Suspend";
		base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(546, 292);
		base.Controls.Add(label3);
		base.Controls.Add(label2);
		base.Controls.Add(lblTotal);
		base.Controls.Add(label1);
		base.Controls.Add(lstUserNames);
		base.Name = "AdminLevelSorting";
		Text = "AdminLevelSorting";
		base.Load += new System.EventHandler(AdminLevelSorting_Load);
		ResumeLayout(false);
		PerformLayout();
	}
}