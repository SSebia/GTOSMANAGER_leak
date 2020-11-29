using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using GTOSmanagement;
using Newtonsoft.Json.Linq;

public class CheckInItemsByItemId : Form
{
	private int searchat;

	private int itemid;

	private bool donationbox;

	private bool storagebox;

	private bool safevault;

	private bool magplants;

	private bool unstable;

	private bool gaia;

	private IContainer components = null;

	private ListBox lstSafeVault;

	private ListBox lstGaia;

	private ListBox lstUnstableT;

	private ListBox lstMagPlants;

	private ListBox lstStorageBox;

	private Label label1;

	private Label label2;

	private ListBox lstDonationBoxes;

	private Label label3;

	private Label label4;

	private Label label5;

	private Label label6;

	private Label label7;

	private Label label8;

	private void lstUnstableT_MouseDoubleClick(object sender, MouseEventArgs e)
	{
		if (lstUnstableT.SelectedItem != null)
		{
			string text = lstUnstableT.SelectedItem.ToString();
			int quantity = Convert.ToInt32(text.Split(' ')[2]);
			string delete = text.Split(' ')[0];
			string suspend = text.Split(' ')[7];
			SuspendAndDeleteFromCheckInItemsByItemId suspendAndDeleteFromCheckInItemsByItemId = new SuspendAndDeleteFromCheckInItemsByItemId(suspend, delete, itemid, quantity);
			suspendAndDeleteFromCheckInItemsByItemId.ShowDialog();
		}
	}

	private void lstGaia_MouseDoubleClick(object sender, MouseEventArgs e)
	{
		if (lstGaia.SelectedItem != null)
		{
			string text = lstGaia.SelectedItem.ToString();
			int quantity = Convert.ToInt32(text.Split(' ')[2]);
			string delete = text.Split(' ')[0];
			string suspend = text.Split(' ')[7];
			SuspendAndDeleteFromCheckInItemsByItemId suspendAndDeleteFromCheckInItemsByItemId = new SuspendAndDeleteFromCheckInItemsByItemId(suspend, delete, itemid, quantity);
			suspendAndDeleteFromCheckInItemsByItemId.ShowDialog();
		}
	}

	private void lstMagPlants_MouseDoubleClick(object sender, MouseEventArgs e)
	{
		if (lstMagPlants.SelectedItem != null)
		{
			string text = lstMagPlants.SelectedItem.ToString();
			int quantity = Convert.ToInt32(text.Split(' ')[2]);
			string delete = text.Split(' ')[0];
			string suspend = text.Split(' ')[7];
			SuspendAndDeleteFromCheckInItemsByItemId suspendAndDeleteFromCheckInItemsByItemId = new SuspendAndDeleteFromCheckInItemsByItemId(suspend, delete, itemid, quantity);
			suspendAndDeleteFromCheckInItemsByItemId.ShowDialog();
		}
	}

	private void lstSafeVault_MouseDoubleClick(object sender, MouseEventArgs e)
	{
		if (lstSafeVault.SelectedItem != null)
		{
			string text = lstSafeVault.SelectedItem.ToString();
			int quantity = Convert.ToInt32(text.Split(' ')[2]);
			string delete = text.Split(' ')[0];
			string suspend = text.Split(' ')[7];
			SuspendAndDeleteFromCheckInItemsByItemId suspendAndDeleteFromCheckInItemsByItemId = new SuspendAndDeleteFromCheckInItemsByItemId(suspend, delete, itemid, quantity);
			suspendAndDeleteFromCheckInItemsByItemId.ShowDialog();
		}
	}

	private void lstStorageBox_MouseDoubleClick(object sender, MouseEventArgs e)
	{
		if (lstStorageBox.SelectedItem != null)
		{
			string text = lstStorageBox.SelectedItem.ToString();
			int quantity = Convert.ToInt32(text.Split(' ')[2]);
			string delete = text.Split(' ')[0];
			string suspend = text.Split(' ')[7];
			SuspendAndDeleteFromCheckInItemsByItemId suspendAndDeleteFromCheckInItemsByItemId = new SuspendAndDeleteFromCheckInItemsByItemId(suspend, delete, itemid, quantity);
			suspendAndDeleteFromCheckInItemsByItemId.ShowDialog();
		}
	}

	private void lstDonationBoxes_MouseDoubleClick(object sender, MouseEventArgs e)
	{
		if (lstDonationBoxes.SelectedItem != null)
		{
			string text = lstDonationBoxes.SelectedItem.ToString();
			int quantity = Convert.ToInt32(text.Split(' ')[2]);
			string delete = text.Split(' ')[0];
			string suspend = text.Split(' ')[7];
			SuspendAndDeleteFromCheckInItemsByItemId suspendAndDeleteFromCheckInItemsByItemId = new SuspendAndDeleteFromCheckInItemsByItemId(suspend, delete, itemid, quantity);
			suspendAndDeleteFromCheckInItemsByItemId.ShowDialog();
		}
	}

	public CheckInItemsByItemId(int _searchat, int _itemid, bool _donationbox, bool _storagebox, bool _safevault, bool _magplants, bool _unstable, bool _gaia)
	{
		InitializeComponent();
		searchat = _searchat;
		itemid = _itemid;
		donationbox = _donationbox;
		storagebox = _storagebox;
		safevault = _safevault;
		magplants = _magplants;
		unstable = _unstable;
		gaia = _gaia;
	}

	private void CheckInItemsByItemId_Load(object sender, EventArgs e)
	{
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Expected O, but got Unknown
		//IL_038f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0396: Expected O, but got Unknown
		//IL_0674: Unknown result type (might be due to invalid IL or missing references)
		//IL_067b: Expected O, but got Unknown
		//IL_0943: Unknown result type (might be due to invalid IL or missing references)
		//IL_094a: Expected O, but got Unknown
		//IL_0c12: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c19: Expected O, but got Unknown
		if (donationbox)
		{
			List<string> list = new List<string>();
			DirectoryInfo directoryInfo = new DirectoryInfo("donationboxes");
			DirectoryInfo[] directories = directoryInfo.GetDirectories();
			DirectoryInfo[] array = directories;
			foreach (DirectoryInfo directoryInfo2 in array)
			{
				DirectoryInfo directoryInfo3 = new DirectoryInfo("donationboxes/" + directoryInfo2.Name);
				FileInfo[] files = directoryInfo3.GetFiles();
				FileInfo[] array2 = files;
				foreach (FileInfo fileInfo in array2)
				{
					string text = File.ReadAllText("donationboxes/" + directoryInfo2.Name + "/" + fileInfo.Name);
					JObject val = JObject.Parse(text);
					JArray val2 = (JArray)val.get_Item("donatedItems");
					for (int k = 0; k < 20; k++)
					{
						if ((int)val2.get_Item(k).get_Item((object)"itemid") != itemid)
						{
							continue;
						}
						string text2 = "not_detected";
						if (File.Exists("worlds/_" + directoryInfo2.Name + ".json"))
						{
							string text3 = File.ReadAllText("worlds/_" + directoryInfo2.Name + ".json");
							string[] array3 = text3.Split(new string[1]
							{
								"\"owner\":\""
							}, StringSplitOptions.None);
							text2 = array3[1].Split('"')[0];
							if (text2 == "")
							{
								text2 = "without_owner";
							}
						}
						if ((int)val2.get_Item(k).get_Item((object)"itemcount") >= searchat && searchat != -1)
						{
							list.Add(string.Format("{0} has {1} of {2} items. Owner: {3}", "donationboxes/" + directoryInfo2.Name + "/" + fileInfo.Name, (int)val2.get_Item(k).get_Item((object)"itemcount"), itemid, text2));
						}
						else if (searchat == -1)
						{
							list.Add(string.Format("{0} has {1} of {2} items. Owner: {3}", "donationboxes/" + directoryInfo2.Name + "/" + fileInfo.Name, (int)val2.get_Item(k).get_Item((object)"itemcount"), itemid, text2));
						}
					}
				}
			}
			lstDonationBoxes.DataSource = list;
		}
		if (safevault)
		{
			List<string> list2 = new List<string>();
			DirectoryInfo directoryInfo4 = new DirectoryInfo("safevault");
			DirectoryInfo[] directories2 = directoryInfo4.GetDirectories();
			DirectoryInfo[] array4 = directories2;
			foreach (DirectoryInfo directoryInfo5 in array4)
			{
				DirectoryInfo directoryInfo6 = new DirectoryInfo("safevault/" + directoryInfo5.Name);
				FileInfo[] files2 = directoryInfo6.GetFiles();
				FileInfo[] array5 = files2;
				foreach (FileInfo fileInfo2 in array5)
				{
					string text4 = File.ReadAllText("safevault/" + directoryInfo5.Name + "/" + fileInfo2.Name);
					JObject val3 = JObject.Parse(text4);
					JArray val4 = (JArray)val3.get_Item("safe");
					for (int n = 0; n < 20; n++)
					{
						if ((int)val4.get_Item(n).get_Item((object)"itemid") != itemid)
						{
							continue;
						}
						string text5 = "not_detected";
						if (File.Exists("worlds/_" + directoryInfo5.Name + ".json"))
						{
							string text6 = File.ReadAllText("worlds/_" + directoryInfo5.Name + ".json");
							string[] array6 = text6.Split(new string[1]
							{
								"\"owner\":\""
							}, StringSplitOptions.None);
							text5 = array6[1].Split('"')[0];
							if (text5 == "")
							{
								text5 = "without_owner";
							}
						}
						if ((int)val4.get_Item(n).get_Item((object)"itemcount") >= searchat && searchat != -1)
						{
							list2.Add(string.Format("{0} has {1} of {2} items. Owner: {3}", "safevault/" + directoryInfo5.Name + "/" + fileInfo2.Name, (int)val4.get_Item(n).get_Item((object)"itemcount"), itemid, text5));
						}
						else if (searchat == -1)
						{
							list2.Add(string.Format("{0} has {1} of {2} items. Owner: {3}", "safevault/" + directoryInfo5.Name + "/" + fileInfo2.Name, (int)val4.get_Item(n).get_Item((object)"itemcount"), itemid, text5));
						}
					}
				}
			}
			lstSafeVault.DataSource = list2;
		}
		List<string> list3 = new List<string>();
		if (storagebox)
		{
			DirectoryInfo directoryInfo7 = new DirectoryInfo("storageboxlvl1");
			DirectoryInfo[] directories3 = directoryInfo7.GetDirectories();
			DirectoryInfo[] array7 = directories3;
			foreach (DirectoryInfo directoryInfo8 in array7)
			{
				DirectoryInfo directoryInfo9 = new DirectoryInfo("storageboxlvl1/" + directoryInfo8.Name);
				FileInfo[] files3 = directoryInfo9.GetFiles();
				FileInfo[] array8 = files3;
				foreach (FileInfo fileInfo3 in array8)
				{
					string text7 = File.ReadAllText("storageboxlvl1/" + directoryInfo8.Name + "/" + fileInfo3.Name);
					JObject val5 = JObject.Parse(text7);
					JArray val6 = (JArray)val5.get_Item("storage");
					for (int num3 = 0; num3 < 20; num3++)
					{
						if ((int)val6.get_Item(num3).get_Item((object)"itemid") != itemid)
						{
							continue;
						}
						string text8 = "not_detected";
						if (File.Exists("worlds/_" + directoryInfo8.Name + ".json"))
						{
							string text9 = File.ReadAllText("worlds/_" + directoryInfo8.Name + ".json");
							string[] array9 = text9.Split(new string[1]
							{
								"\"owner\":\""
							}, StringSplitOptions.None);
							text8 = array9[1].Split('"')[0];
							if (text8 == "")
							{
								text8 = "without_owner";
							}
						}
						if ((int)val6.get_Item(num3).get_Item((object)"itemcount") >= searchat && searchat != -1)
						{
							list3.Add(string.Format("{0} has {1} of {2} items. Owner: {3}", "storageboxlvl1/" + directoryInfo8.Name + "/" + fileInfo3.Name, (int)val6.get_Item(num3).get_Item((object)"itemcount"), itemid, text8));
						}
						else if (searchat == -1)
						{
							list3.Add(string.Format("{0} has {1} of {2} items. Owner: {3}", "storageboxlvl1/" + directoryInfo8.Name + "/" + fileInfo3.Name, (int)val6.get_Item(num3).get_Item((object)"itemcount"), itemid, text8));
						}
					}
				}
			}
		}
		if (storagebox)
		{
			DirectoryInfo directoryInfo10 = new DirectoryInfo("storageboxlvl2");
			DirectoryInfo[] directories4 = directoryInfo10.GetDirectories();
			DirectoryInfo[] array10 = directories4;
			foreach (DirectoryInfo directoryInfo11 in array10)
			{
				DirectoryInfo directoryInfo12 = new DirectoryInfo("storageboxlvl2/" + directoryInfo11.Name);
				FileInfo[] files4 = directoryInfo12.GetFiles();
				FileInfo[] array11 = files4;
				foreach (FileInfo fileInfo4 in array11)
				{
					string text10 = File.ReadAllText("storageboxlvl2/" + directoryInfo11.Name + "/" + fileInfo4.Name);
					JObject val7 = JObject.Parse(text10);
					JArray val8 = (JArray)val7.get_Item("storage");
					for (int num6 = 0; num6 < 40; num6++)
					{
						if ((int)val8.get_Item(num6).get_Item((object)"itemid") != itemid)
						{
							continue;
						}
						string text11 = "not_detected";
						if (File.Exists("worlds/_" + directoryInfo11.Name + ".json"))
						{
							string text12 = File.ReadAllText("worlds/_" + directoryInfo11.Name + ".json");
							string[] array12 = text12.Split(new string[1]
							{
								"\"owner\":\""
							}, StringSplitOptions.None);
							text11 = array12[1].Split('"')[0];
							if (text11 == "")
							{
								text11 = "without_owner";
							}
						}
						if ((int)val8.get_Item(num6).get_Item((object)"itemcount") >= searchat && searchat != -1)
						{
							list3.Add(string.Format("{0} has {1} of {2} items. Owner: {3}", "storageboxlvl2/" + directoryInfo11.Name + "/" + fileInfo4.Name, (int)val8.get_Item(num6).get_Item((object)"itemcount"), itemid, text11));
						}
						else if (searchat == -1)
						{
							list3.Add(string.Format("{0} has {1} of {2} items. Owner: {3}", "storageboxlvl2/" + directoryInfo11.Name + "/" + fileInfo4.Name, (int)val8.get_Item(num6).get_Item((object)"itemcount"), itemid, text11));
						}
					}
				}
			}
		}
		if (storagebox)
		{
			DirectoryInfo directoryInfo13 = new DirectoryInfo("storageboxlvl3");
			DirectoryInfo[] directories5 = directoryInfo13.GetDirectories();
			DirectoryInfo[] array13 = directories5;
			foreach (DirectoryInfo directoryInfo14 in array13)
			{
				DirectoryInfo directoryInfo15 = new DirectoryInfo("storageboxlvl3/" + directoryInfo14.Name);
				FileInfo[] files5 = directoryInfo15.GetFiles();
				FileInfo[] array14 = files5;
				foreach (FileInfo fileInfo5 in array14)
				{
					string text13 = File.ReadAllText("storageboxlvl3/" + directoryInfo14.Name + "/" + fileInfo5.Name);
					JObject val9 = JObject.Parse(text13);
					JArray val10 = (JArray)val9.get_Item("storage");
					for (int num9 = 0; num9 < 20; num9++)
					{
						if ((int)val10.get_Item(num9).get_Item((object)"itemid") != itemid)
						{
							continue;
						}
						string text14 = "not_detected";
						if (File.Exists("worlds/_" + directoryInfo14.Name + ".json"))
						{
							string text15 = File.ReadAllText("worlds/_" + directoryInfo14.Name + ".json");
							string[] array15 = text15.Split(new string[1]
							{
								"\"owner\":\""
							}, StringSplitOptions.None);
							text14 = array15[1].Split('"')[0];
							if (text14 == "")
							{
								text14 = "without_owner";
							}
						}
						if ((int)val10.get_Item(num9).get_Item((object)"itemcount") >= searchat && searchat != -1)
						{
							list3.Add(string.Format("{0} has {1} of {2} items. Owner: {3}", "storageboxlvl3/" + directoryInfo14.Name + "/" + fileInfo5.Name, (int)val10.get_Item(num9).get_Item((object)"itemcount"), itemid, text14));
						}
						else if (searchat == -1)
						{
							list3.Add(string.Format("{0} has {1} of {2} items. Owner: {3}", "storageboxlvl3/" + directoryInfo14.Name + "/" + fileInfo5.Name, (int)val10.get_Item(num9).get_Item((object)"itemcount"), itemid, text14));
						}
					}
				}
			}
			lstStorageBox.DataSource = list3;
		}
		if (magplants)
		{
			List<string> list4 = new List<string>();
			DirectoryInfo directoryInfo16 = new DirectoryInfo("magplant/storeditem");
			FileInfo[] files6 = directoryInfo16.GetFiles();
			FileInfo[] array16 = files6;
			foreach (FileInfo fileInfo6 in array16)
			{
				string a = File.ReadAllText("magplant/storeditem/" + fileInfo6.Name);
				if (!(a == itemid.ToString()))
				{
					continue;
				}
				string text16 = "not_detected";
				if (File.Exists("worlds/_" + fileInfo6.Name.Split('X')[0] + ".json"))
				{
					string text17 = File.ReadAllText("worlds/_" + fileInfo6.Name.Split('X')[0] + ".json");
					string[] array17 = text17.Split(new string[1]
					{
						"\"owner\":\""
					}, StringSplitOptions.None);
					text16 = array17[1].Split('"')[0];
					if (text16 == "")
					{
						text16 = "without_owner";
					}
				}
				string text18 = File.ReadAllText("magplant/count/" + fileInfo6.Name);
				if (Convert.ToInt32(text18) >= searchat && searchat != -1)
				{
					list4.Add(string.Format("{0} has {1} of {2} items. Owner: {3}", "magplant/storeditem/" + fileInfo6.Name, text18, itemid, text16));
				}
				else if (searchat == -1)
				{
					list4.Add(string.Format("{0} has {1} of {2} items. Owner: {3}", "magplant/storeditem/" + fileInfo6.Name, text18, itemid, text16));
				}
			}
			lstMagPlants.DataSource = list4;
		}
		if (unstable)
		{
			List<string> list5 = new List<string>();
			DirectoryInfo directoryInfo17 = new DirectoryInfo("unstabletesseract/storeditem");
			FileInfo[] files7 = directoryInfo17.GetFiles();
			FileInfo[] array18 = files7;
			foreach (FileInfo fileInfo7 in array18)
			{
				string a2 = File.ReadAllText("unstabletesseract/storeditem/" + fileInfo7.Name);
				if (!(a2 == itemid.ToString()))
				{
					continue;
				}
				string text19 = "not_detected";
				if (File.Exists("worlds/_" + fileInfo7.Name.Split('X')[0] + ".json"))
				{
					string text20 = File.ReadAllText("worlds/_" + fileInfo7.Name.Split('X')[0] + ".json");
					string[] array19 = text20.Split(new string[1]
					{
						"\"owner\":\""
					}, StringSplitOptions.None);
					text19 = array19[1].Split('"')[0];
					if (text19 == "")
					{
						text19 = "without_owner";
					}
				}
				string text21 = File.ReadAllText("unstabletesseract/count/" + fileInfo7.Name);
				if (Convert.ToInt32(text21) >= searchat && searchat != -1)
				{
					list5.Add(string.Format("{0} has {1} of {2} items. Owner: {3}", "unstabletesseract/storeditem/" + fileInfo7.Name, text21, itemid, text19));
				}
				else if (searchat == -1)
				{
					list5.Add(string.Format("{0} has {1} of {2} items. Owner: {3}", "unstabletesseract/storeditem/" + fileInfo7.Name, text21, itemid, text19));
				}
			}
			lstUnstableT.DataSource = list5;
		}
		if (gaia)
		{
			List<string> list6 = new List<string>();
			DirectoryInfo directoryInfo18 = new DirectoryInfo("gaiabeacon/storeditem");
			FileInfo[] files8 = directoryInfo18.GetFiles();
			FileInfo[] array20 = files8;
			foreach (FileInfo fileInfo8 in array20)
			{
				string a3 = File.ReadAllText("gaiabeacon/storeditem/" + fileInfo8.Name);
				if (!(a3 == itemid.ToString()))
				{
					continue;
				}
				string text22 = "not_detected";
				if (File.Exists("worlds/_" + fileInfo8.Name.Split('X')[0] + ".json"))
				{
					string text23 = File.ReadAllText("worlds/_" + fileInfo8.Name.Split('X')[0] + ".json");
					string[] array21 = text23.Split(new string[1]
					{
						"\"owner\":\""
					}, StringSplitOptions.None);
					text22 = array21[1].Split('"')[0];
					if (text22 == "")
					{
						text22 = "without_owner";
					}
				}
				string text24 = File.ReadAllText("gaiabeacon/count/" + fileInfo8.Name);
				if (Convert.ToInt32(text24) >= searchat && searchat != -1)
				{
					list6.Add(string.Format("{0} has {1} of {2} items. Owner: {3}", "gaiabeacon/storeditem/" + fileInfo8.Name, text24, itemid, text22));
				}
				else if (searchat == -1)
				{
					list6.Add(string.Format("{0} has {1} of {2} items. Owner: {3}", "gaiabeacon/storeditem/" + fileInfo8.Name, text24, itemid, text22));
				}
			}
			lstGaia.DataSource = list6;
		}
		GC.Collect();
		GC.WaitForPendingFinalizers();
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
		lstSafeVault = new System.Windows.Forms.ListBox();
		lstGaia = new System.Windows.Forms.ListBox();
		lstUnstableT = new System.Windows.Forms.ListBox();
		lstMagPlants = new System.Windows.Forms.ListBox();
		lstStorageBox = new System.Windows.Forms.ListBox();
		label1 = new System.Windows.Forms.Label();
		label2 = new System.Windows.Forms.Label();
		lstDonationBoxes = new System.Windows.Forms.ListBox();
		label3 = new System.Windows.Forms.Label();
		label4 = new System.Windows.Forms.Label();
		label5 = new System.Windows.Forms.Label();
		label6 = new System.Windows.Forms.Label();
		label7 = new System.Windows.Forms.Label();
		label8 = new System.Windows.Forms.Label();
		SuspendLayout();
		lstSafeVault.FormattingEnabled = true;
		lstSafeVault.ItemHeight = 16;
		lstSafeVault.Location = new System.Drawing.Point(15, 364);
		lstSafeVault.Name = "lstSafeVault";
		lstSafeVault.Size = new System.Drawing.Size(576, 132);
		lstSafeVault.TabIndex = 2;
		lstSafeVault.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(lstSafeVault_MouseDoubleClick);
		lstGaia.FormattingEnabled = true;
		lstGaia.ItemHeight = 16;
		lstGaia.Location = new System.Drawing.Point(623, 364);
		lstGaia.Name = "lstGaia";
		lstGaia.Size = new System.Drawing.Size(660, 132);
		lstGaia.TabIndex = 5;
		lstGaia.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(lstGaia_MouseDoubleClick);
		lstUnstableT.FormattingEnabled = true;
		lstUnstableT.ItemHeight = 16;
		lstUnstableT.Location = new System.Drawing.Point(623, 202);
		lstUnstableT.Name = "lstUnstableT";
		lstUnstableT.Size = new System.Drawing.Size(660, 132);
		lstUnstableT.TabIndex = 4;
		lstUnstableT.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(lstUnstableT_MouseDoubleClick);
		lstMagPlants.FormattingEnabled = true;
		lstMagPlants.ItemHeight = 16;
		lstMagPlants.Location = new System.Drawing.Point(623, 29);
		lstMagPlants.Name = "lstMagPlants";
		lstMagPlants.Size = new System.Drawing.Size(660, 132);
		lstMagPlants.TabIndex = 3;
		lstMagPlants.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(lstMagPlants_MouseDoubleClick);
		lstStorageBox.FormattingEnabled = true;
		lstStorageBox.ItemHeight = 16;
		lstStorageBox.Location = new System.Drawing.Point(15, 202);
		lstStorageBox.Name = "lstStorageBox";
		lstStorageBox.Size = new System.Drawing.Size(576, 132);
		lstStorageBox.TabIndex = 6;
		lstStorageBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(lstStorageBox_MouseDoubleClick);
		label1.AutoSize = true;
		label1.Location = new System.Drawing.Point(12, 9);
		label1.Name = "label1";
		label1.Size = new System.Drawing.Size(126, 17);
		label1.TabIndex = 7;
		label1.Text = "In Donation Boxes:";
		label2.AutoSize = true;
		label2.Location = new System.Drawing.Point(621, 9);
		label2.Name = "label2";
		label2.Size = new System.Drawing.Size(92, 17);
		label2.TabIndex = 8;
		label2.Text = "In Magplants:";
		lstDonationBoxes.FormattingEnabled = true;
		lstDonationBoxes.ItemHeight = 16;
		lstDonationBoxes.Location = new System.Drawing.Point(15, 29);
		lstDonationBoxes.Name = "lstDonationBoxes";
		lstDonationBoxes.Size = new System.Drawing.Size(576, 132);
		lstDonationBoxes.TabIndex = 9;
		lstDonationBoxes.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(lstDonationBoxes_MouseDoubleClick);
		label3.AutoSize = true;
		label3.Location = new System.Drawing.Point(12, 182);
		label3.Name = "label3";
		label3.Size = new System.Drawing.Size(119, 17);
		label3.TabIndex = 10;
		label3.Text = "In Storage Boxes:";
		label4.AutoSize = true;
		label4.Location = new System.Drawing.Point(620, 182);
		label4.Name = "label4";
		label4.Size = new System.Drawing.Size(100, 17);
		label4.TabIndex = 11;
		label4.Text = "In Unstable T.:";
		label5.AutoSize = true;
		label5.Location = new System.Drawing.Point(12, 344);
		label5.Name = "label5";
		label5.Size = new System.Drawing.Size(99, 17);
		label5.TabIndex = 12;
		label5.Text = "In Safe Vaults:";
		label6.AutoSize = true;
		label6.Location = new System.Drawing.Point(621, 344);
		label6.Name = "label6";
		label6.Size = new System.Drawing.Size(57, 17);
		label6.TabIndex = 13;
		label6.Text = "In Gaia:";
		label7.AutoSize = true;
		label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8f, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, 204);
		label7.Location = new System.Drawing.Point(58, 508);
		label7.Name = "label7";
		label7.Size = new System.Drawing.Size(410, 17);
		label7.TabIndex = 17;
		label7.Text = "double click on item if you want to take any actions with this item";
		label8.AutoSize = true;
		label8.Font = new System.Drawing.Font("Microsoft YaHei", 7.8f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
		label8.ForeColor = System.Drawing.SystemColors.Desktop;
		label8.Location = new System.Drawing.Point(14, 507);
		label8.Name = "label8";
		label8.Size = new System.Drawing.Size(48, 19);
		label8.TabIndex = 16;
		label8.Text = "Note:";
		base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1299, 530);
		base.Controls.Add(label7);
		base.Controls.Add(label8);
		base.Controls.Add(label6);
		base.Controls.Add(label5);
		base.Controls.Add(label4);
		base.Controls.Add(label3);
		base.Controls.Add(lstDonationBoxes);
		base.Controls.Add(label2);
		base.Controls.Add(label1);
		base.Controls.Add(lstStorageBox);
		base.Controls.Add(lstGaia);
		base.Controls.Add(lstUnstableT);
		base.Controls.Add(lstMagPlants);
		base.Controls.Add(lstSafeVault);
		base.Name = "CheckInItemsByItemId";
		Text = "CheckInItemsByItemId";
		base.Load += new System.EventHandler(CheckInItemsByItemId_Load);
		ResumeLayout(false);
		PerformLayout();
	}
}