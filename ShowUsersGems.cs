using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

public class ShowUsersGems : Form
{
	private int quantityGems = 0;

	private IContainer components = null;

	private Label label4;

	private Label label5;

	private Button btnSort;

	private TextBox txtSort;

	private Label lblTotal;

	private Label label1;

	private Label label3;

	private Label label2;

	private ListBox lstGems;

	public ShowUsersGems(int _gems)
	{
		InitializeComponent();
		quantityGems = _gems;
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

	private void ShowUsersGems_Load(object sender, EventArgs e)
	{
		lstGems.DataSource = null;
		lstGems.Items.Clear();
		List<string> list = new List<string>();
		int num = 0;
		int num2 = Directory.GetFiles("gemdb", "*", SearchOption.TopDirectoryOnly).Length;
		DirectoryInfo directoryInfo = new DirectoryInfo("gemdb");
		for (int i = 0; i < num2; i++)
		{
			FileInfo fileInfo = directoryInfo.GetFiles()[i];
			string text;
			using (StreamReader streamReader = new StreamReader("gemdb/" + fileInfo.Name))
			{
				text = streamReader.ReadToEnd();
			}
			try
			{
				string str = null;
				if (IsDigitsOnly(text) && Convert.ToInt32(text) >= quantityGems)
				{
					str += $"User {fileInfo.Name} has {text} gems.";
					list.Add(str);
				}
			}
			catch
			{
				MessageBox.Show("An error occurred while getting information from the user's gemdb TXT file.\nThis could be because the file " + fileInfo.Name + " was corrupted.\n" + fileInfo.Name + " was not added to list.", "An error occurred", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
		lstGems.DataSource = list;
		lblTotal.Text = lstGems.Items.Count.ToString();
		if (num > 0)
		{
			MessageBox.Show(num + " files were deleted because these users are not exist in mysql database", "Scan completed.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
	}

	private void lstGems_DoubleClick(object sender, EventArgs e)
	{
		if (lstGems.SelectedItem != null)
		{
			string text = lstGems.SelectedItem.ToString();
			string str = text.Split(' ')[1];
			try
			{
				Process.Start("notepad++.exe", "gemdb/" + str);
			}
			catch
			{
				MessageBox.Show("An error occurred while opening the user's txt file.", "An error occurred", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
	}

	private void btnSort_Click(object sender, EventArgs e)
	{
		lstGems.DataSource = null;
		lstGems.Items.Clear();
		List<string> list = new List<string>();
		int num = Directory.GetFiles("gemdb", "*", SearchOption.TopDirectoryOnly).Length;
		DirectoryInfo directoryInfo = new DirectoryInfo("gemdb");
		for (int i = 0; i < num; i++)
		{
			FileInfo fileInfo = directoryInfo.GetFiles()[i];
			string text;
			using (StreamReader streamReader = new StreamReader("gemdb/" + fileInfo.Name))
			{
				text = streamReader.ReadLine();
			}
			try
			{
				string str = null;
				if (string.IsNullOrEmpty(text))
				{
					text = "NOTHING(check this file)";
				}
				else if (int.Parse(text) > int.Parse(txtSort.Text))
				{
					str += $"User {fileInfo.Name} has {text} gems.";
					list.Add(str);
				}
			}
			catch
			{
				MessageBox.Show("An error occurred while getting information from the user's gemdb TXT file.\nThis could be because the file " + fileInfo.Name + " was corrupted.\n" + fileInfo.Name + " was not added to list.", "An error occurred", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
		lstGems.DataSource = list;
		lblTotal.Text = lstGems.Items.Count.ToString();
	}

	private void txtSort_Leave_1(object sender, EventArgs e)
	{
		if (txtSort.Text == "")
		{
			txtSort.Text = "sort by quantity higher than...";
			txtSort.ForeColor = Color.Silver;
		}
	}

	private void txtSort_Enter_1(object sender, EventArgs e)
	{
		if (txtSort.Text == "sort by quantity higher than...")
		{
			txtSort.Text = "";
			txtSort.ForeColor = Color.Black;
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
		lstGems = new System.Windows.Forms.ListBox();
		SuspendLayout();
		label4.AutoSize = true;
		label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8f, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, 204);
		label4.Location = new System.Drawing.Point(408, 22);
		label4.Name = "label4";
		label4.Size = new System.Drawing.Size(200, 17);
		label4.TabIndex = 30;
		label4.Text = "you can use sort several times";
		label5.AutoSize = true;
		label5.Font = new System.Drawing.Font("Microsoft YaHei", 7.8f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
		label5.ForeColor = System.Drawing.SystemColors.Desktop;
		label5.Location = new System.Drawing.Point(364, 21);
		label5.Name = "label5";
		label5.Size = new System.Drawing.Size(48, 19);
		label5.TabIndex = 29;
		label5.Text = "Note:";
		btnSort.Cursor = System.Windows.Forms.Cursors.Hand;
		btnSort.Location = new System.Drawing.Point(316, 19);
		btnSort.Name = "btnSort";
		btnSort.Size = new System.Drawing.Size(45, 23);
		btnSort.TabIndex = 28;
		btnSort.Text = "Sort";
		btnSort.UseVisualStyleBackColor = true;
		btnSort.Click += new System.EventHandler(btnSort_Click);
		txtSort.Font = new System.Drawing.Font("Microsoft Sans Serif", 13f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
		txtSort.ForeColor = System.Drawing.SystemColors.InactiveCaption;
		txtSort.Location = new System.Drawing.Point(11, 14);
		txtSort.Name = "txtSort";
		txtSort.Size = new System.Drawing.Size(300, 32);
		txtSort.TabIndex = 27;
		txtSort.Text = "sort by quantity higher than...";
		txtSort.Enter += new System.EventHandler(txtSort_Enter_1);
		txtSort.Leave += new System.EventHandler(txtSort_Leave_1);
		lblTotal.AutoSize = true;
		lblTotal.Location = new System.Drawing.Point(100, 328);
		lblTotal.Name = "lblTotal";
		lblTotal.Size = new System.Drawing.Size(16, 17);
		lblTotal.TabIndex = 26;
		lblTotal.Text = "0";
		label1.AutoSize = true;
		label1.Location = new System.Drawing.Point(8, 328);
		label1.Name = "label1";
		label1.Size = new System.Drawing.Size(94, 17);
		label1.TabIndex = 25;
		label1.Text = "Total players:\n";
		label3.AutoSize = true;
		label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8f, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, 204);
		label3.Location = new System.Drawing.Point(51, 64);
		label3.Name = "label3";
		label3.Size = new System.Drawing.Size(405, 17);
		label3.TabIndex = 24;
		label3.Text = "double click on user if you want to open this file with notepad++";
		label2.AutoSize = true;
		label2.Font = new System.Drawing.Font("Microsoft YaHei", 7.8f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
		label2.ForeColor = System.Drawing.SystemColors.Desktop;
		label2.Location = new System.Drawing.Point(7, 63);
		label2.Name = "label2";
		label2.Size = new System.Drawing.Size(48, 19);
		label2.TabIndex = 23;
		label2.Text = "Note:";
		lstGems.FormattingEnabled = true;
		lstGems.ItemHeight = 16;
		lstGems.Location = new System.Drawing.Point(11, 85);
		lstGems.Name = "lstGems";
		lstGems.Size = new System.Drawing.Size(782, 228);
		lstGems.TabIndex = 22;
		lstGems.DoubleClick += new System.EventHandler(lstGems_DoubleClick);
		base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(805, 359);
		base.Controls.Add(label4);
		base.Controls.Add(label5);
		base.Controls.Add(btnSort);
		base.Controls.Add(txtSort);
		base.Controls.Add(lblTotal);
		base.Controls.Add(label1);
		base.Controls.Add(label3);
		base.Controls.Add(label2);
		base.Controls.Add(lstGems);
		base.Name = "ShowUsersGems";
		Text = "ShowUsersGems";
		base.Load += new System.EventHandler(ShowUsersGems_Load);
		ResumeLayout(false);
		PerformLayout();
	}
}