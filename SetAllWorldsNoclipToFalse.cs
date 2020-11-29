using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

public class SetAllWorldsNoclipToFalse : Form
{
	private IContainer components = null;

	private ListBox lstChangesLog;

	private Label label3;

	private Label lblTotal;

	public SetAllWorldsNoclipToFalse()
	{
		InitializeComponent();
	}

	private void SetAllWorldsNoclipToFalse_Load(object sender, EventArgs e)
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
				JObject val = JObject.Parse(text);
				if ((bool)val.get_Item("allowMod"))
				{
					num2++;
					bool flag = false;
					val.set_Item("allowMod", JToken.op_Implicit(flag));
					lstChangesLog.Items.Add(fileInfo.Name);
					File.WriteAllText("worlds/" + fileInfo.Name, ((object)val).ToString());
				}
				val = null;
			}
			catch
			{
				MessageBox.Show("An error occurred while getting information from the world's JSON file.\nThis could be because the file " + fileInfo.Name + " was corrupted.\n" + fileInfo.Name + " was not added to list.", "An error occurred", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
		lblTotal.Text = "Total changed noclip to false: " + num2;
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
		lstChangesLog = new System.Windows.Forms.ListBox();
		label3 = new System.Windows.Forms.Label();
		lblTotal = new System.Windows.Forms.Label();
		SuspendLayout();
		lstChangesLog.FormattingEnabled = true;
		lstChangesLog.ItemHeight = 16;
		lstChangesLog.Location = new System.Drawing.Point(6, 43);
		lstChangesLog.Name = "lstChangesLog";
		lstChangesLog.Size = new System.Drawing.Size(782, 228);
		lstChangesLog.TabIndex = 14;
		label3.AutoSize = true;
		label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8f, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, 204);
		label3.Location = new System.Drawing.Point(12, 23);
		label3.Name = "label3";
		label3.Size = new System.Drawing.Size(91, 17);
		label3.TabIndex = 16;
		label3.Text = "Changes log:";
		lblTotal.AutoSize = true;
		lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8f, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, 204);
		lblTotal.Location = new System.Drawing.Point(12, 281);
		lblTotal.Name = "lblTotal";
		lblTotal.Size = new System.Drawing.Size(44, 17);
		lblTotal.TabIndex = 17;
		lblTotal.Text = "Total:";
		base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(800, 308);
		base.Controls.Add(lblTotal);
		base.Controls.Add(label3);
		base.Controls.Add(lstChangesLog);
		base.Name = "SetAllWorldsNoclipToFalse";
		Text = "SetAllWorldsNoclipToFalse";
		base.Load += new System.EventHandler(SetAllWorldsNoclipToFalse_Load);
		ResumeLayout(false);
		PerformLayout();
	}
}