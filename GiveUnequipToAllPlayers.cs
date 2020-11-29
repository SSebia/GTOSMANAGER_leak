using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

public class GiveUnequipToAllPlayers : Form
{
	private IContainer components = null;

	private Label label1;

	public GiveUnequipToAllPlayers()
	{
		InitializeComponent();
	}

	private void GiveUnequipToAllPlayers_Load(object sender, EventArgs e)
	{
		int num = 0;
		int num2 = Directory.GetFiles("players", "*", SearchOption.TopDirectoryOnly).Length;
		DirectoryInfo directoryInfo = new DirectoryInfo("players");
		for (int i = 0; i < num2; i++)
		{
			FileInfo fileInfo = directoryInfo.GetFiles()[i];
			string text = File.ReadAllText("players/" + fileInfo.Name);
			try
			{
				JObject val = JObject.Parse(text);
				val.set_Item("ClothAnces", JToken.op_Implicit(0));
				val.set_Item("ClothBack", JToken.op_Implicit(0));
				val.set_Item("ClothFace", JToken.op_Implicit(0));
				val.set_Item("ClothFeet", JToken.op_Implicit(0));
				val.set_Item("ClothHair", JToken.op_Implicit(0));
				val.set_Item("ClothHand", JToken.op_Implicit(0));
				val.set_Item("ClothMask", JToken.op_Implicit(0));
				val.set_Item("ClothNeck", JToken.op_Implicit(0));
				val.set_Item("ClothPants", JToken.op_Implicit(0));
				val.set_Item("ClothShirt", JToken.op_Implicit(0));
				val.set_Item("effect", JToken.op_Implicit(8421376));
				File.WriteAllText("players/" + fileInfo.Name, ((object)val).ToString());
				num++;
			}
			catch
			{
				MessageBox.Show("An error occurred while getting information from the user's JSON file.\nThis could be because the file " + fileInfo.Name + " was corrupted.\n" + fileInfo.Name + " was not added to list.", "An error occurred", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
		label1.Text = "Unequipped " + num + " players.";
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
		SuspendLayout();
		label1.AutoSize = true;
		label1.Font = new System.Drawing.Font("Microsoft YaHei", 10.8f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
		label1.Location = new System.Drawing.Point(12, 63);
		label1.Name = "label1";
		label1.Size = new System.Drawing.Size(228, 26);
		label1.TabIndex = 1;
		label1.Text = "Unequipped x players.";
		base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(596, 167);
		base.Controls.Add(label1);
		base.Name = "GiveUnequipToAllPlayers";
		Text = "GiveUnequipToAllPlayers";
		base.Load += new System.EventHandler(GiveUnequipToAllPlayers_Load);
		ResumeLayout(false);
		PerformLayout();
	}
}