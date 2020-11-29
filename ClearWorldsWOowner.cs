using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

public class ClearWorldsWOowner : Form
{
	private IContainer components = null;

	private Label lblTotal;

	private Label label3;

	private ListBox lstdeletesLog;

	private Button btnRemoveAll;

	public ClearWorldsWOowner()
	{
		InitializeComponent();
	}

	private void ClearWorldsWOowner_Load(object sender, EventArgs e)
	{
		int num = Directory.GetFiles("worlds", "*", SearchOption.TopDirectoryOnly).Length;
		DirectoryInfo directoryInfo = new DirectoryInfo("worlds");
		int num2 = 0;
		for (int i = 0; i < num; i++)
		{
			FileInfo fileInfo = directoryInfo.GetFiles()[i];
			try
			{
				string text = File.ReadAllText("worlds/" + fileInfo.Name);
				string[] array = text.Split(new string[1]
				{
					"\"owner\":\""
				}, StringSplitOptions.None);
				string value = array[1].Split('"')[0];
				if (string.IsNullOrEmpty(value))
				{
					num2++;
					lstdeletesLog.Items.Add(fileInfo.Name);
				}
			}
			catch
			{
				MessageBox.Show("An error occurred while getting information from the world's JSON file.\nThis could be because the file " + fileInfo.Name + " was corrupted.\n" + fileInfo.Name + " was not added to list.", "An error occurred", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
		lblTotal.Text = "Total need to remove worlds: " + num2;
	}

	private void btnRemoveAll_Click(object sender, EventArgs e)
	{
		foreach (object item in lstdeletesLog.Items)
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
		MessageBox.Show("Successfully.", "Done", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
		lblTotal = new System.Windows.Forms.Label();
		label3 = new System.Windows.Forms.Label();
		lstdeletesLog = new System.Windows.Forms.ListBox();
		btnRemoveAll = new System.Windows.Forms.Button();
		SuspendLayout();
		lblTotal.AutoSize = true;
		lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8f, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, 204);
		lblTotal.Location = new System.Drawing.Point(12, 272);
		lblTotal.Name = "lblTotal";
		lblTotal.Size = new System.Drawing.Size(44, 17);
		lblTotal.TabIndex = 20;
		lblTotal.Text = "Total:";
		label3.AutoSize = true;
		label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8f, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, 204);
		label3.Location = new System.Drawing.Point(12, 14);
		label3.Name = "label3";
		label3.Size = new System.Drawing.Size(97, 17);
		label3.TabIndex = 19;
		label3.Text = "Need remove:";
		lstdeletesLog.FormattingEnabled = true;
		lstdeletesLog.ItemHeight = 16;
		lstdeletesLog.Location = new System.Drawing.Point(6, 34);
		lstdeletesLog.Name = "lstdeletesLog";
		lstdeletesLog.Size = new System.Drawing.Size(782, 228);
		lstdeletesLog.TabIndex = 18;
		btnRemoveAll.Location = new System.Drawing.Point(6, 305);
		btnRemoveAll.Name = "btnRemoveAll";
		btnRemoveAll.Size = new System.Drawing.Size(140, 56);
		btnRemoveAll.TabIndex = 21;
		btnRemoveAll.Text = "Remove all";
		btnRemoveAll.UseVisualStyleBackColor = true;
		btnRemoveAll.Click += new System.EventHandler(btnRemoveAll_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(800, 384);
		base.Controls.Add(btnRemoveAll);
		base.Controls.Add(lblTotal);
		base.Controls.Add(label3);
		base.Controls.Add(lstdeletesLog);
		base.Name = "ClearWorldsWOowner";
		Text = "ClearWorldsWOowner";
		base.Load += new System.EventHandler(ClearWorldsWOowner_Load);
		ResumeLayout(false);
		PerformLayout();
	}
}