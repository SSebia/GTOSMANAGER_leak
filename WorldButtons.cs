using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using GTOSmanagement;
using Microsoft.VisualBasic;

public class WorldButtons : Form
{
	private IContainer components = null;

	private Button btn_all_worlds;

	private Button btnFindItemByItemIdInWorlds;

	private Button btn_clear_world_woOwner;

	private Button btnFindItemsInWorld;

	private Panel panel1;

	private Label label1;

	private TextBox txtsearchAt;

	private CheckBox cboxDonationGaia;

	private CheckBox cboxUnstable;

	private CheckBox cboxMagPlants;

	private CheckBox cboxSafeVaultsBoxes;

	private CheckBox cboxStorageBoxes;

	private CheckBox cboxDonationBoxes;

	private Button btnsearchbyitemidcheckboxes;

	private CheckBox cboxAll;

	private Label label2;

	private TextBox txtItemID;

	private ToolTip SearchAtTip;

	public WorldButtons()
	{
		InitializeComponent();
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

	private void btnFindItemByItemIdInWorlds_Click(object sender, EventArgs e)
	{
		string text = Interaction.InputBox("Enter the itemID by which we will search in all worlds.", "Sorting by itemID in all worlds", "1008");
		if (text.Length > 0)
		{
			bool flag = false;
			if (IsDigitsOnly(text))
			{
				int getitemid = int.Parse(text);
				FindItemInAllWorlds findItemInAllWorlds = new FindItemInAllWorlds(getitemid);
				findItemInAllWorlds.ShowDialog();
			}
			else
			{
				MessageBox.Show("Only digitals are available.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
	}

	private void btn_clear_world_woOwner_Click(object sender, EventArgs e)
	{
		Process[] processesByName = Process.GetProcessesByName("enet");
		if (processesByName.Length != 0)
		{
			MessageBox.Show("Enet server is running! Please close it.", "enet server is running!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			return;
		}
		ClearWorldsWOowner clearWorldsWOowner = new ClearWorldsWOowner();
		clearWorldsWOowner.ShowDialog();
	}

	private void btn_all_worlds_Click(object sender, EventArgs e)
	{
		Process[] processesByName = Process.GetProcessesByName("enet");
		if (processesByName.Length != 0)
		{
			MessageBox.Show("Enet server is running! Please close it.", "enet server is running!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			return;
		}
		DialogResult dialogResult = MessageBox.Show("Are you sure you want to change noclip to false for all worlds?", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
		if (dialogResult == DialogResult.Yes)
		{
			SetAllWorldsNoclipToFalse setAllWorldsNoclipToFalse = new SetAllWorldsNoclipToFalse();
			setAllWorldsNoclipToFalse.ShowDialog();
		}
	}

	private void btnFindItemsInWorld_Click(object sender, EventArgs e)
	{
		string text = Interaction.InputBox("Enter the name of the world where search.", "Find items in the world", "start");
		if (text.Length > 0)
		{
		}
	}

	private void btnsearchbyitemidcheckboxes_Click(object sender, EventArgs e)
	{
		int num = Convert.ToInt32(txtItemID.Text);
		if (num < 1 || num > 60000)
		{
			MessageBox.Show("incorrect itemd id.");
			return;
		}
		int num2 = -1;
		if (txtsearchAt.Text != "")
		{
			num2 = Convert.ToInt32(txtsearchAt.Text);
			if (num2 < 1 || num2 > 60000)
			{
				MessageBox.Show("incorrect search at.");
				return;
			}
		}
		bool @checked = cboxDonationBoxes.Checked;
		bool checked2 = cboxStorageBoxes.Checked;
		bool checked3 = cboxSafeVaultsBoxes.Checked;
		bool checked4 = cboxMagPlants.Checked;
		bool checked5 = cboxUnstable.Checked;
		bool checked6 = cboxDonationGaia.Checked;
		if (!@checked && !checked2 && !checked3 && !checked4 && !checked5 && !checked6)
		{
			MessageBox.Show("Please check at least 1 checkbox.");
			return;
		}
		CheckInItemsByItemId checkInItemsByItemId = new CheckInItemsByItemId(num2, num, @checked, checked2, checked3, checked4, checked5, checked6);
		checkInItemsByItemId.ShowDialog();
	}

	private void cboxAll_CheckedChanged(object sender, EventArgs e)
	{
		if (cboxAll.Checked)
		{
			cboxDonationBoxes.Checked = true;
			cboxStorageBoxes.Checked = true;
			cboxSafeVaultsBoxes.Checked = true;
			cboxMagPlants.Checked = true;
			cboxUnstable.Checked = true;
			cboxDonationGaia.Checked = true;
		}
		else
		{
			cboxDonationBoxes.Checked = false;
			cboxStorageBoxes.Checked = false;
			cboxSafeVaultsBoxes.Checked = false;
			cboxMagPlants.Checked = false;
			cboxUnstable.Checked = false;
			cboxDonationGaia.Checked = false;
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
		components = new System.ComponentModel.Container();
		btn_all_worlds = new System.Windows.Forms.Button();
		btnFindItemByItemIdInWorlds = new System.Windows.Forms.Button();
		btn_clear_world_woOwner = new System.Windows.Forms.Button();
		btnFindItemsInWorld = new System.Windows.Forms.Button();
		panel1 = new System.Windows.Forms.Panel();
		label1 = new System.Windows.Forms.Label();
		txtsearchAt = new System.Windows.Forms.TextBox();
		cboxDonationGaia = new System.Windows.Forms.CheckBox();
		cboxUnstable = new System.Windows.Forms.CheckBox();
		cboxMagPlants = new System.Windows.Forms.CheckBox();
		cboxSafeVaultsBoxes = new System.Windows.Forms.CheckBox();
		cboxStorageBoxes = new System.Windows.Forms.CheckBox();
		cboxDonationBoxes = new System.Windows.Forms.CheckBox();
		btnsearchbyitemidcheckboxes = new System.Windows.Forms.Button();
		cboxAll = new System.Windows.Forms.CheckBox();
		label2 = new System.Windows.Forms.Label();
		txtItemID = new System.Windows.Forms.TextBox();
		SearchAtTip = new System.Windows.Forms.ToolTip(components);
		panel1.SuspendLayout();
		SuspendLayout();
		btn_all_worlds.Location = new System.Drawing.Point(12, 153);
		btn_all_worlds.Name = "btn_all_worlds";
		btn_all_worlds.Size = new System.Drawing.Size(140, 56);
		btn_all_worlds.TabIndex = 7;
		btn_all_worlds.Text = "Set all worlds noclip to false";
		btn_all_worlds.UseVisualStyleBackColor = true;
		btn_all_worlds.Click += new System.EventHandler(btn_all_worlds_Click);
		btnFindItemByItemIdInWorlds.Location = new System.Drawing.Point(12, 12);
		btnFindItemByItemIdInWorlds.Name = "btnFindItemByItemIdInWorlds";
		btnFindItemByItemIdInWorlds.Size = new System.Drawing.Size(140, 52);
		btnFindItemByItemIdInWorlds.TabIndex = 8;
		btnFindItemByItemIdInWorlds.Text = "Find item in all worlds by itemID";
		btnFindItemByItemIdInWorlds.UseVisualStyleBackColor = true;
		btnFindItemByItemIdInWorlds.Click += new System.EventHandler(btnFindItemByItemIdInWorlds_Click);
		btn_clear_world_woOwner.Location = new System.Drawing.Point(12, 81);
		btn_clear_world_woOwner.Name = "btn_clear_world_woOwner";
		btn_clear_world_woOwner.Size = new System.Drawing.Size(140, 56);
		btn_clear_world_woOwner.TabIndex = 9;
		btn_clear_world_woOwner.Text = "Clear worlds without owner";
		btn_clear_world_woOwner.UseVisualStyleBackColor = true;
		btn_clear_world_woOwner.Click += new System.EventHandler(btn_clear_world_woOwner_Click);
		btnFindItemsInWorld.Location = new System.Drawing.Point(12, 228);
		btnFindItemsInWorld.Name = "btnFindItemsInWorld";
		btnFindItemsInWorld.Size = new System.Drawing.Size(140, 52);
		btnFindItemsInWorld.TabIndex = 10;
		btnFindItemsInWorld.Text = "Find items in the world";
		btnFindItemsInWorld.UseVisualStyleBackColor = true;
		btnFindItemsInWorld.Click += new System.EventHandler(btnFindItemsInWorld_Click);
		panel1.BackColor = System.Drawing.SystemColors.Info;
		panel1.Controls.Add(label2);
		panel1.Controls.Add(txtItemID);
		panel1.Controls.Add(cboxAll);
		panel1.Controls.Add(label1);
		panel1.Controls.Add(txtsearchAt);
		panel1.Controls.Add(cboxDonationGaia);
		panel1.Controls.Add(cboxUnstable);
		panel1.Controls.Add(cboxMagPlants);
		panel1.Controls.Add(cboxSafeVaultsBoxes);
		panel1.Controls.Add(cboxStorageBoxes);
		panel1.Controls.Add(cboxDonationBoxes);
		panel1.Controls.Add(btnsearchbyitemidcheckboxes);
		panel1.Location = new System.Drawing.Point(225, 12);
		panel1.Name = "panel1";
		panel1.Size = new System.Drawing.Size(259, 290);
		panel1.TabIndex = 11;
		label1.AutoSize = true;
		label1.Location = new System.Drawing.Point(74, 207);
		label1.Name = "label1";
		label1.Size = new System.Drawing.Size(73, 17);
		label1.TabIndex = 8;
		label1.Text = "Search at:";
		txtsearchAt.Location = new System.Drawing.Point(153, 204);
		txtsearchAt.Name = "txtsearchAt";
		txtsearchAt.Size = new System.Drawing.Size(100, 22);
		txtsearchAt.TabIndex = 7;
		SearchAtTip.SetToolTip(txtsearchAt, "Leave textbox empty if you won't use it");
		cboxDonationGaia.AutoSize = true;
		cboxDonationGaia.Location = new System.Drawing.Point(14, 148);
		cboxDonationGaia.Name = "cboxDonationGaia";
		cboxDonationGaia.Size = new System.Drawing.Size(124, 21);
		cboxDonationGaia.TabIndex = 6;
		cboxDonationGaia.Text = "Search in Gaia";
		cboxDonationGaia.UseVisualStyleBackColor = true;
		cboxUnstable.AutoSize = true;
		cboxUnstable.Location = new System.Drawing.Point(14, 120);
		cboxUnstable.Name = "cboxUnstable";
		cboxUnstable.Size = new System.Drawing.Size(167, 21);
		cboxUnstable.TabIndex = 5;
		cboxUnstable.Text = "Search in Unstable T.";
		cboxUnstable.UseVisualStyleBackColor = true;
		cboxMagPlants.AutoSize = true;
		cboxMagPlants.Location = new System.Drawing.Point(14, 92);
		cboxMagPlants.Name = "cboxMagPlants";
		cboxMagPlants.Size = new System.Drawing.Size(159, 21);
		cboxMagPlants.TabIndex = 4;
		cboxMagPlants.Text = "Search in Magplants";
		cboxMagPlants.UseVisualStyleBackColor = true;
		cboxSafeVaultsBoxes.AutoSize = true;
		cboxSafeVaultsBoxes.Location = new System.Drawing.Point(14, 64);
		cboxSafeVaultsBoxes.Name = "cboxSafeVaultsBoxes";
		cboxSafeVaultsBoxes.Size = new System.Drawing.Size(166, 21);
		cboxSafeVaultsBoxes.TabIndex = 3;
		cboxSafeVaultsBoxes.Text = "Search in Safe Vaults";
		cboxSafeVaultsBoxes.UseVisualStyleBackColor = true;
		cboxStorageBoxes.AutoSize = true;
		cboxStorageBoxes.Location = new System.Drawing.Point(14, 36);
		cboxStorageBoxes.Name = "cboxStorageBoxes";
		cboxStorageBoxes.Size = new System.Drawing.Size(239, 21);
		cboxStorageBoxes.TabIndex = 2;
		cboxStorageBoxes.Text = "Search in Storage Boxes 1,2,3 lvl";
		cboxStorageBoxes.UseVisualStyleBackColor = true;
		cboxDonationBoxes.AutoSize = true;
		cboxDonationBoxes.Location = new System.Drawing.Point(14, 8);
		cboxDonationBoxes.Name = "cboxDonationBoxes";
		cboxDonationBoxes.Size = new System.Drawing.Size(192, 21);
		cboxDonationBoxes.TabIndex = 1;
		cboxDonationBoxes.Text = "Search in Donation boxes";
		cboxDonationBoxes.UseVisualStyleBackColor = true;
		btnsearchbyitemidcheckboxes.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
		btnsearchbyitemidcheckboxes.Location = new System.Drawing.Point(98, 232);
		btnsearchbyitemidcheckboxes.Name = "btnsearchbyitemidcheckboxes";
		btnsearchbyitemidcheckboxes.Size = new System.Drawing.Size(156, 49);
		btnsearchbyitemidcheckboxes.TabIndex = 0;
		btnsearchbyitemidcheckboxes.Text = "Search by Item ID in these items";
		btnsearchbyitemidcheckboxes.UseVisualStyleBackColor = false;
		btnsearchbyitemidcheckboxes.Click += new System.EventHandler(btnsearchbyitemidcheckboxes_Click);
		cboxAll.AutoSize = true;
		cboxAll.Location = new System.Drawing.Point(5, 260);
		cboxAll.Name = "cboxAll";
		cboxAll.Size = new System.Drawing.Size(88, 21);
		cboxAll.TabIndex = 10;
		cboxAll.Text = "Check All";
		cboxAll.UseVisualStyleBackColor = true;
		cboxAll.CheckedChanged += new System.EventHandler(cboxAll_CheckedChanged);
		label2.AutoSize = true;
		label2.Location = new System.Drawing.Point(96, 179);
		label2.Name = "label2";
		label2.Size = new System.Drawing.Size(51, 17);
		label2.TabIndex = 12;
		label2.Text = "ItemID:";
		txtItemID.Location = new System.Drawing.Point(153, 176);
		txtItemID.Name = "txtItemID";
		txtItemID.Size = new System.Drawing.Size(100, 22);
		txtItemID.TabIndex = 11;
		SearchAtTip.AutomaticDelay = 1;
		SearchAtTip.AutoPopDelay = 6000;
		SearchAtTip.InitialDelay = 1;
		SearchAtTip.ReshowDelay = 0;
		base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(520, 309);
		base.Controls.Add(panel1);
		base.Controls.Add(btnFindItemsInWorld);
		base.Controls.Add(btn_clear_world_woOwner);
		base.Controls.Add(btnFindItemByItemIdInWorlds);
		base.Controls.Add(btn_all_worlds);
		base.Name = "WorldButtons";
		Text = "WorldButtons";
		panel1.ResumeLayout(false);
		panel1.PerformLayout();
		ResumeLayout(false);
	}
}