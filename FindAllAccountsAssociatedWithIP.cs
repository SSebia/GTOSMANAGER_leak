using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

public class FindAllAccountsAssociatedWithIP : Form
{
	private string searchingIP;

	private IContainer components = null;

	private Label lblTotal;

	private Label label1;

	private Label label3;

	private Label label2;

	private ListBox lstUsers;

	public FindAllAccountsAssociatedWithIP(string UserAnswer)
	{
		InitializeComponent();
		searchingIP = UserAnswer;
	}

	private void FindAllAccountsAssociatedWithIP_Load(object sender, EventArgs e)
	{
		int num = Directory.GetFiles("accountSecurity", "*", SearchOption.TopDirectoryOnly).Length;
		DirectoryInfo directoryInfo = new DirectoryInfo("accountSecurity");
		int num2 = 0;
		for (int i = 0; i < num; i++)
		{
			FileInfo fileInfo = directoryInfo.GetFiles()[i];
			string text = File.ReadLines("accountSecurity/" + fileInfo.Name).ElementAtOrDefault(8);
			if (text == searchingIP)
			{
				num2++;
				lstUsers.Items.Add("GrowID: " + Path.GetFileNameWithoutExtension(fileInfo.Name) + " has IP: " + text);
			}
		}
		lblTotal.Text = num2.ToString();
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
		lblTotal = new System.Windows.Forms.Label();
		label1 = new System.Windows.Forms.Label();
		label3 = new System.Windows.Forms.Label();
		label2 = new System.Windows.Forms.Label();
		lstUsers = new System.Windows.Forms.ListBox();
		SuspendLayout();
		lblTotal.AutoSize = true;
		lblTotal.Location = new System.Drawing.Point(83, 275);
		lblTotal.Name = "lblTotal";
		lblTotal.Size = new System.Drawing.Size(16, 17);
		lblTotal.TabIndex = 13;
		lblTotal.Text = "0";
		label1.AutoSize = true;
		label1.Location = new System.Drawing.Point(3, 275);
		label1.Name = "label1";
		label1.Size = new System.Drawing.Size(83, 17);
		label1.TabIndex = 12;
		label1.Text = "Total users:\n";
		label3.AutoSize = true;
		label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8f, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, 204);
		label3.Location = new System.Drawing.Point(46, 11);
		label3.Name = "label3";
		label3.Size = new System.Drawing.Size(414, 17);
		label3.TabIndex = 11;
		label3.Text = "double click on user if you want to take any actions with this user";
		label2.AutoSize = true;
		label2.Font = new System.Drawing.Font("Microsoft YaHei", 7.8f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
		label2.ForeColor = System.Drawing.SystemColors.Desktop;
		label2.Location = new System.Drawing.Point(2, 10);
		label2.Name = "label2";
		label2.Size = new System.Drawing.Size(48, 19);
		label2.TabIndex = 10;
		label2.Text = "Note:";
		lstUsers.FormattingEnabled = true;
		lstUsers.ItemHeight = 16;
		lstUsers.Location = new System.Drawing.Point(6, 32);
		lstUsers.Name = "lstUsers";
		lstUsers.Size = new System.Drawing.Size(782, 228);
		lstUsers.TabIndex = 9;
		base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(800, 310);
		base.Controls.Add(lblTotal);
		base.Controls.Add(label1);
		base.Controls.Add(label3);
		base.Controls.Add(label2);
		base.Controls.Add(lstUsers);
		base.Name = "FindAllAccountsAssociatedWithIP";
		Text = "FindAllAccountsAssociatedWithIP";
		base.Load += new System.EventHandler(FindAllAccountsAssociatedWithIP_Load);
		ResumeLayout(false);
		PerformLayout();
	}
}