using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

public class RemoveAllFromInventoriesExceptStoreItems : Form
{
	private IContainer components = null;

	private Label label1;

	public RemoveAllFromInventoriesExceptStoreItems()
	{
		InitializeComponent();
	}

	private void RemoveAllFromInventoriesExceptStoreItems_Load(object sender, EventArgs e)
	{
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Expected O, but got Unknown
		int num = 0;
		int num2 = Directory.GetFiles("inventory", "*", SearchOption.TopDirectoryOnly).Length;
		DirectoryInfo directoryInfo = new DirectoryInfo("inventory");
		for (int i = 0; i < num2; i++)
		{
			FileInfo fileInfo = directoryInfo.GetFiles()[i];
			string text = File.ReadAllText("inventory/" + fileInfo.Name);
			try
			{
				JObject val = JObject.Parse(text);
				JArray val2 = (JArray)val.get_Item("items");
				string s;
				try
				{
					s = File.ReadAllText("usersinventorysize/" + Path.GetFileNameWithoutExtension(fileInfo.Name) + ".txt");
				}
				catch (Exception)
				{
					DialogResult dialogResult = MessageBox.Show("An error occurred while getting information from the user's usersinventorysize txt file. Does usersinventorysize/" + Path.GetFileNameWithoutExtension(fileInfo.Name) + ".txt exists?\n\nIf you want to create this .txt with default '30' value - press 'YES'", "An error occurred", MessageBoxButtons.YesNo, MessageBoxIcon.Hand);
					if (dialogResult != DialogResult.Yes)
					{
						continue;
					}
					File.WriteAllText("usersinventorysize/" + Path.GetFileNameWithoutExtension(fileInfo.Name) + ".txt", "30");
					s = "30";
				}
				int result = 0;
				if (!int.TryParse(s, out result))
				{
					MessageBox.Show("An error occurred while convertint string to number from usersinventorysize/ " + Path.GetFileNameWithoutExtension(fileInfo.Name) + ".txt", "An error occurred", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					continue;
				}
				for (int j = 0; j < result; j++)
				{
					short num3 = (short)val2.get_Item(j).get_Item((object)"itemid");
					short num4 = (short)val2.get_Item(j).get_Item((object)"quantity");
					short num5 = (short)val2.get_Item(j).get_Item((object)"aposition");
					if (num3 != 242 && num3 != 764 && num3 != 782 && num3 != 1796 && num3 != 2408 && num3 != 9468 && num3 != 3764 && num3 != 4428 && num3 != 9460 && num3 != 9470 && num3 != 5086 && num3 != 9240 && num3 != 5480 && num3 != 9306 && num3 != 9290 && num3 != 7328 && num3 != 9416 && num3 != 9410 && num3 != 1458 && num3 != 9408 && num3 != 9360 && num3 != 6866 && num3 != 6868 && num3 != 6870 && num3 != 6872 && num3 != 6874 && num3 != 6876 && num3 != 4762 && num3 != 7382 && num3 != 6878 && num3 != 2480 && num3 != 8452 && num3 != 5132 && num3 != 7166 && num3 != 5078 && num3 != 5080 && num3 != 5082 && num3 != 5084 && num3 != 5126 && num3 != 5128 && num3 != 5130 && num3 != 5144 && num3 != 5146 && num3 != 5148 && num3 != 5150 && num3 != 5162 && num3 != 5164 && num3 != 5166 && num3 != 5168 && num3 != 5180 && num3 != 5182 && num3 != 5184 && num3 != 5186 && num3 != 7168 && num3 != 7170 && num3 != 7172 && num3 != 7174 && num3 != 8834 && num3 != 7912 && num3 != 9212 && num3 != 5134 && num3 != 5152 && num3 != 5170 && num3 != 5188 && num3 != 9432 && num3 != 1874 && num3 != 1876 && num3 != 1904 && num3 != 1932 && num3 != 1900 && num3 != 1986 && num3 != 1996 && num3 != 2970 && num3 != 3140 && num3 != 3174 && num3 != 6028 && num3 != 6846 && num3 != 8962 && num3 != 980 && num3 != 9448 && num3 != 9310 && num3 != 6 && num3 != 9492 && num3 != 1782 && num3 != 1780 && num3 != 8306 && num3 != 202 && num3 != 204 && num3 != 206 && num3 != 2950 && num3 != 4802 && num3 != 4994 && num3 != 5260 && num3 != 5814 && num3 != 5980 && num3 != 7734 && num3 != 2592 && num3 != 2242 && num3 != 1794 && num3 != 1792 && num3 != 778 && num3 != 9510 && num3 != 1790 && num3 != 8774 && num3 != 2568 && num3 != 9512 && num3 != 9502 && num3 != 9482 && num3 != 2250 && num3 != 2248 && num3 != 2244 && num3 != 2246 && num3 != 2286 && num3 != 9508 && num3 != 9504 && num3 != 9506 && num3 != 274 && num3 != 276 && num3 != 9476 && num3 != 1486 && num3 != 9498 && num3 != 4426 && num3 != 9496 && num3 != 278 && num3 != 9494 && num3 != 9490 && num3 != 2410 && num3 != 9488 && num3 != 9452 && num3 != 9454 && num3 != 9472 && num3 != 9456 && num3 != 732 && num3 != 9458 && num3 != 6336 && num3 != 112 && num3 != 8 && num3 != 3760 && num3 != 7372 && num3 != 9438 && num3 != 9462 && num3 != 9440 && num3 != 9442 && num3 != 9444 && num3 != 7960 && num3 != 7628 && num3 != 8552 && num3 != 8286 && num3 != 1970 && num3 != 1784 && num3 != 7188 && num3 != 9308 && num3 != 9426 && num3 != 9500 && num3 != 9474 && num3 != 4992 && num3 != 9484 && num3 != 2204 && num3 != 9486 && num3 != 9494 && num3 != 8428 && num3 != 9428 && num3 != 9434 && num3 != 5136 && num3 != 9478 && num3 != 9430 && num3 != 9422 && num3 != 9170 && num3 != 9432 && num3 != 1008 && num3 != 9466 && num3 != 1636 && num3 != 9418 && num3 != 3402 && num3 != 9414 && num3 != 6204 && num3 != 6202 && num3 != 6200 && num3 != 7484 && num3 != 7954 && num3 != 8470 && num3 != 9424 && num3 != 2952 && num3 != 9356)
					{
						val2.get_Item(j).set_Item((object)"itemid", JToken.op_Implicit(0));
						val2.get_Item(j).set_Item((object)"quantity", JToken.op_Implicit(0));
						num += num4;
					}
				}
				File.WriteAllText("inventory/" + fileInfo.Name, ((object)val).ToString());
			}
			catch
			{
				MessageBox.Show("An error occurred while getting information from the user's inventory JSON file.\nThis could be because the file " + fileInfo.Name + " was corrupted.\n" + fileInfo.Name + " was not added to list.", "An error occurred", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
		label1.Text = "Removed " + num + " items of quantity from all inventories";
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
		label1.Location = new System.Drawing.Point(12, 28);
		label1.Name = "label1";
		label1.Size = new System.Drawing.Size(485, 26);
		label1.TabIndex = 0;
		label1.Text = "Removed x items of quantity from all inventories";
		base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(586, 92);
		base.Controls.Add(label1);
		base.Name = "RemoveAllFromInventoriesExceptStoreItems";
		Text = "RemoveAllFromInventoriesExceptStoreItems";
		base.Load += new System.EventHandler(RemoveAllFromInventoriesExceptStoreItems_Load);
		ResumeLayout(false);
		PerformLayout();
	}
}