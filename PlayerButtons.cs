using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using GTOSmanagement;
using Microsoft.VisualBasic;

public class PlayerButtons : Form
{
	private IContainer components = null;

	private Button btnFindItemByItemIdInInventory;

	private Button btnShowGems;

	private Button btnShowUsersByAdminLevel;

	private Button btnAssociatedWithIp;

	private Button removeAllFromInventoriesExceptStoreItems;

	private Button btn_give_unequip;

	public PlayerButtons()
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

	private void btnShowUsersByAdminLevel_Click(object sender, EventArgs e)
	{
		string str = "\r\n                            0 - newbie\r\n                            1 - moderator\r\n                            2 - developer\r\n\r\n                            ";
		string text = Interaction.InputBox("Enter the level of administration by which we will sort. Available levels:\n " + str, "Sorting by admin level", "1000");
		if (text.Length > 0)
		{
			if (text == "0" || text == "1" || text == "2")
			{
				int admLevel = int.Parse(text);
				AdminLevelSorting adminLevelSorting = new AdminLevelSorting(admLevel);
				adminLevelSorting.ShowDialog();
			}
			else
			{
				MessageBox.Show("Wrong level number.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
	}

	private void btnFindItemByItemIdInInventory_Click(object sender, EventArgs e)
	{
		string text = Interaction.InputBox("Enter the itemID by which we will search in all users inventories.", "Sorting by itemID", "7188");
		if (text.Length > 0)
		{
			bool flag = false;
			if (IsDigitsOnly(text))
			{
				int srcItemId = int.Parse(text);
				FindItemInUserInventoryByItemId findItemInUserInventoryByItemId = new FindItemInUserInventoryByItemId(srcItemId);
				findItemInUserInventoryByItemId.ShowDialog();
			}
			else
			{
				MessageBox.Show("Only digitals are available.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
	}

	private void btnShowGems_Click(object sender, EventArgs e)
	{
		string text = Interaction.InputBox("Enter quantity of gems.", "Sorting by quantity of gems", "1000000");
		if (text.Length > 0)
		{
			bool flag = false;
			if (IsDigitsOnly(text))
			{
				int gems = int.Parse(text);
				ShowUsersGems showUsersGems = new ShowUsersGems(gems);
				showUsersGems.ShowDialog();
			}
			else
			{
				MessageBox.Show("Only digitals are available.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
	}

	private void btnAssociatedWithIp_Click(object sender, EventArgs e)
	{
		string text = Interaction.InputBox("Enter Ip address.", "Search by IP.", "127.0.0.1");
		if (text.Length > 16)
		{
			MessageBox.Show("Ip address length should be lower than 16", "Invalid Ip address!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			return;
		}
		if (text.Length < 6)
		{
			MessageBox.Show("Ip address length should be higher than 6", "Invalid Ip address!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			return;
		}
		if (Regex.IsMatch(text, "[a-zA-Z]"))
		{
			MessageBox.Show("Ip address shouldn't contain letters.", "Invalid Ip address!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			return;
		}
		if (text.Count((char f) => f == '.') != 3)
		{
			MessageBox.Show("Ip address should have 3 dots.", "Invalid Ip address!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			return;
		}
		FindAllAccountsAssociatedWithIP findAllAccountsAssociatedWithIP = new FindAllAccountsAssociatedWithIP(text);
		findAllAccountsAssociatedWithIP.ShowDialog();
	}

	private void removeAllFromInventoriesExceptStoreItems_Click(object sender, EventArgs e)
	{
		MessageBox.Show("Items will be deleted during the search process. Close the server.", "Note!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		Process[] processesByName = Process.GetProcessesByName("enet");
		if (processesByName.Length != 0)
		{
			MessageBox.Show("Enet server is running! Please close it.", "enet server is running!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			return;
		}
		RemoveAllFromInventoriesExceptStoreItems removeAllFromInventoriesExceptStoreItems = new RemoveAllFromInventoriesExceptStoreItems();
		removeAllFromInventoriesExceptStoreItems.ShowDialog();
	}

	private void btn_give_unequip_Click(object sender, EventArgs e)
	{
		MessageBox.Show("Unequip will be changed during the search process. Close the server.", "Note!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		Process[] processesByName = Process.GetProcessesByName("enet");
		if (processesByName.Length != 0)
		{
			MessageBox.Show("Enet server is running! Please close it.", "enet server is running!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			return;
		}
		GiveUnequipToAllPlayers giveUnequipToAllPlayers = new GiveUnequipToAllPlayers();
		giveUnequipToAllPlayers.ShowDialog();
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
		btnFindItemByItemIdInInventory = new System.Windows.Forms.Button();
		btnShowGems = new System.Windows.Forms.Button();
		btnShowUsersByAdminLevel = new System.Windows.Forms.Button();
		btnAssociatedWithIp = new System.Windows.Forms.Button();
		removeAllFromInventoriesExceptStoreItems = new System.Windows.Forms.Button();
		btn_give_unequip = new System.Windows.Forms.Button();
		SuspendLayout();
		btnFindItemByItemIdInInventory.Location = new System.Drawing.Point(12, 96);
		btnFindItemByItemIdInInventory.Name = "btnFindItemByItemIdInInventory";
		btnFindItemByItemIdInInventory.Size = new System.Drawing.Size(140, 78);
		btnFindItemByItemIdInInventory.TabIndex = 2;
		btnFindItemByItemIdInInventory.Text = "Find item's count in users inventories by itemID";
		btnFindItemByItemIdInInventory.UseVisualStyleBackColor = true;
		btnFindItemByItemIdInInventory.Click += new System.EventHandler(btnFindItemByItemIdInInventory_Click);
		btnShowGems.Location = new System.Drawing.Point(12, 180);
		btnShowGems.Name = "btnShowGems";
		btnShowGems.Size = new System.Drawing.Size(140, 78);
		btnShowGems.TabIndex = 3;
		btnShowGems.Text = "Show the number of user gems by sorting by quantity";
		btnShowGems.UseVisualStyleBackColor = true;
		btnShowGems.Click += new System.EventHandler(btnShowGems_Click);
		btnShowUsersByAdminLevel.Location = new System.Drawing.Point(12, 12);
		btnShowUsersByAdminLevel.Name = "btnShowUsersByAdminLevel";
		btnShowUsersByAdminLevel.Size = new System.Drawing.Size(140, 78);
		btnShowUsersByAdminLevel.TabIndex = 4;
		btnShowUsersByAdminLevel.Text = "Show users by admin level";
		btnShowUsersByAdminLevel.UseVisualStyleBackColor = true;
		btnShowUsersByAdminLevel.Click += new System.EventHandler(btnShowUsersByAdminLevel_Click);
		btnAssociatedWithIp.Location = new System.Drawing.Point(12, 264);
		btnAssociatedWithIp.Name = "btnAssociatedWithIp";
		btnAssociatedWithIp.Size = new System.Drawing.Size(140, 78);
		btnAssociatedWithIp.TabIndex = 5;
		btnAssociatedWithIp.Text = "Find all accounts associated with IP";
		btnAssociatedWithIp.UseVisualStyleBackColor = true;
		btnAssociatedWithIp.Click += new System.EventHandler(btnAssociatedWithIp_Click);
		removeAllFromInventoriesExceptStoreItems.Location = new System.Drawing.Point(186, 12);
		removeAllFromInventoriesExceptStoreItems.Name = "removeAllFromInventoriesExceptStoreItems";
		removeAllFromInventoriesExceptStoreItems.Size = new System.Drawing.Size(176, 78);
		removeAllFromInventoriesExceptStoreItems.TabIndex = 6;
		removeAllFromInventoriesExceptStoreItems.Text = "Remove all items from inventories except items from the store";
		removeAllFromInventoriesExceptStoreItems.UseVisualStyleBackColor = true;
		removeAllFromInventoriesExceptStoreItems.Click += new System.EventHandler(removeAllFromInventoriesExceptStoreItems_Click);
		btn_give_unequip.Location = new System.Drawing.Point(186, 96);
		btn_give_unequip.Name = "btn_give_unequip";
		btn_give_unequip.Size = new System.Drawing.Size(176, 78);
		btn_give_unequip.TabIndex = 7;
		btn_give_unequip.Text = "Give unequip to all players";
		btn_give_unequip.UseVisualStyleBackColor = true;
		btn_give_unequip.Click += new System.EventHandler(btn_give_unequip_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(800, 359);
		base.Controls.Add(btn_give_unequip);
		base.Controls.Add(removeAllFromInventoriesExceptStoreItems);
		base.Controls.Add(btnAssociatedWithIp);
		base.Controls.Add(btnShowUsersByAdminLevel);
		base.Controls.Add(btnShowGems);
		base.Controls.Add(btnFindItemByItemIdInInventory);
		base.Name = "PlayerButtons";
		Text = "PlayerButtons";
		ResumeLayout(false);
	}
}