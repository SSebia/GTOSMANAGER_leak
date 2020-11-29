using System;
using System.ComponentModel;
using System.Data.Common;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using GTOSmanagement;
using Microsoft.VisualBasic;

public class Form1 : Form
{
	private static string current_version = "1.4";

	private static bool isSaw = true;

	private IContainer components = null;

	private Button btn_associated;

	private Button btn_all_worlds;

	private Label lbl_CodedBy;

	private MenuStrip menuStrip1;

	private ToolStripMenuItem updatesToolStripMenuItem;

	private ToolStripMenuItem aboutToolStripMenuItem;

	public Timer timerUpdatesStrip;

	private Button button1;

	private Button btn_make_worlds_backup;

	private Button Checkmemoryaddress;

	public Form1()
	{
		InitializeComponent();
	}

	private void btn_associated_Click(object sender, EventArgs e)
	{
		string text = Interaction.InputBox("Enter growid.", "Enter growid", "sebia");
		if (text.Length > 0)
		{
			ShowEverythingAssociatedWithGrowID showEverythingAssociatedWithGrowID = new ShowEverythingAssociatedWithGrowID(text);
			showEverythingAssociatedWithGrowID.ShowDialog();
		}
	}

	private void btn_all_worlds_Click(object sender, EventArgs e)
	{
		WorldButtons worldButtons = new WorldButtons();
		worldButtons.ShowDialog();
	}

	private void lbl_CodedBy_Click(object sender, EventArgs e)
	{
		MessageBox.Show("Discord tag was copied to ClipBoard.", "Discord tag", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		Clipboard.SetText("RobertasLTU#8643");
		try
		{
			Process.Start(Environment.GetEnvironmentVariable("LocalAppData") + "\\Discord\\app-0.0.306\\Discord.exe");
		}
		catch (Exception)
		{
		}
	}

	private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
	{
		MessageBox.Show("App version: " + current_version + "\n\nThis management app was created for GTOS server.\n\nGTOS server owners: RobertasLTU#8643 and Sebia#1337\n\nManagement app developer: RobertasLTU#8643", "Information about app.", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
	}

	private void Form1_Load(object sender, EventArgs e)
	{
		try
		{
			db db = new db();
			((DbConnection)(object)db.Connection).Open();
			((DbConnection)(object)db.Connection).Close();
			db = null;
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString(), "Mysql Database connection error.", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			Application.Exit();
		}
		if (!Directory.Exists("management_config"))
		{
			Directory.CreateDirectory("management_config");
		}
		if (!File.Exists("management_config/version.txt"))
		{
			File.WriteAllText("management_config/version.txt", "new");
		}
		string a = File.ReadAllText("management_config/version.txt");
		if (a != current_version)
		{
			isSaw = false;
			timerUpdatesStrip.Start();
			timerUpdatesStrip.Interval = 500;
			updatesToolStripMenuItem.Text = "Updates (new)";
		}
	}

	private void timerUpdatesStrip_Tick(object sender, EventArgs e)
	{
		if (updatesToolStripMenuItem.BackColor == SystemColors.Control)
		{
			updatesToolStripMenuItem.BackColor = Color.Red;
		}
		else
		{
			updatesToolStripMenuItem.BackColor = SystemColors.Control;
		}
	}

	private void updatesToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (!isSaw)
		{
			isSaw = true;
			File.WriteAllText("management_config/version.txt", current_version);
			timerUpdatesStrip.Stop();
			updatesToolStripMenuItem.BackColor = SystemColors.Control;
			updatesToolStripMenuItem.Text = "Updates";
		}
		string text = "Update 1.0\n\n → Added world buttons button\n → In finding items in inventories now fully works double click on the user, where you can remove his items with properties, curse, ban, open his account or inventory in notepad++.\n → In finding items in inventories added to remove all button\n → Added show everything associated with growid button.\n → Remade show users by admin level + added when double click, you can change his admin level.\n → Added menu strip with 'Updates' and 'About' buttons.\n → Added players buttons button. \n → Added button show player by ip.\n\nUpdate 1.1\n\n → Added worlds backup button.\n\nUpdate 1.2\n\n → Added remove all items from inventories except store items.\n\nUpdate 1.3\n\n → Added unequip all players button.\n\nUpdate 1.4\n\n → Added search in magplants, gaia, unstable, storage boxes, safe vault, donation boxes by Item ID.";
		MessageBox.Show(text, "Updates!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
	}

	private void button1_Click(object sender, EventArgs e)
	{
		PlayerButtons playerButtons = new PlayerButtons();
		playerButtons.ShowDialog();
	}

	private void btn_make_worlds_backup_Click(object sender, EventArgs e)
	{
		if (!Directory.Exists("worlds"))
		{
			MessageBox.Show("'Worlds' folder doesn't exists!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			return;
		}
		if (!Directory.Exists("backups"))
		{
			DialogResult dialogResult = MessageBox.Show("'backups' folder doesn't exists! Do you want to create it?", "Error!", MessageBoxButtons.YesNo, MessageBoxIcon.Hand);
			if (dialogResult != DialogResult.Yes)
			{
				return;
			}
			Directory.CreateDirectory("backups");
			MessageBox.Show("Successfully created. Starting making backup. Close this dialog.", "Done!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
		string str = DateTime.Now.ToString("yyyy-MM-dd hh.mm.ss.");
		string sourceDirectoryName = "c:worlds\\";
		string destinationArchiveFileName = "backups\\" + str + ".zip";
		ZipFile.CreateFromDirectory(sourceDirectoryName, destinationArchiveFileName, CompressionLevel.Fastest, includeBaseDirectory: true);
		MessageBox.Show("Successfully.", "Done!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
	}

	private void Checkmemoryaddress_Click(object sender, EventArgs e)
	{
		string text = Interaction.InputBox("Enter address.", "Enter address", "like: 0x0018F36C");
		if (text.Length > 4)
		{
			IntPtr ptr = new IntPtr(Convert.ToInt32(text, 16));
			bool flag = false;
			MessageBox.Show("Value is: " + Marshal.ReadInt64(ptr));
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
		btn_associated = new System.Windows.Forms.Button();
		btn_all_worlds = new System.Windows.Forms.Button();
		lbl_CodedBy = new System.Windows.Forms.Label();
		menuStrip1 = new System.Windows.Forms.MenuStrip();
		updatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		timerUpdatesStrip = new System.Windows.Forms.Timer(components);
		button1 = new System.Windows.Forms.Button();
		btn_make_worlds_backup = new System.Windows.Forms.Button();
		Checkmemoryaddress = new System.Windows.Forms.Button();
		menuStrip1.SuspendLayout();
		SuspendLayout();
		btn_associated.Location = new System.Drawing.Point(168, 126);
		btn_associated.Name = "btn_associated";
		btn_associated.Size = new System.Drawing.Size(140, 78);
		btn_associated.TabIndex = 5;
		btn_associated.Text = "Show everything associated with growid";
		btn_associated.UseVisualStyleBackColor = true;
		btn_associated.Click += new System.EventHandler(btn_associated_Click);
		btn_all_worlds.Location = new System.Drawing.Point(12, 126);
		btn_all_worlds.Name = "btn_all_worlds";
		btn_all_worlds.Size = new System.Drawing.Size(140, 78);
		btn_all_worlds.TabIndex = 6;
		btn_all_worlds.Text = "World buttons";
		btn_all_worlds.UseVisualStyleBackColor = true;
		btn_all_worlds.Click += new System.EventHandler(btn_all_worlds_Click);
		lbl_CodedBy.AutoSize = true;
		lbl_CodedBy.BackColor = System.Drawing.Color.FromArgb(255, 224, 192);
		lbl_CodedBy.Cursor = System.Windows.Forms.Cursors.Hand;
		lbl_CodedBy.Font = new System.Drawing.Font("Palatino Linotype", 10.2f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic | System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, 204);
		lbl_CodedBy.ForeColor = System.Drawing.Color.Green;
		lbl_CodedBy.Location = new System.Drawing.Point(12, 302);
		lbl_CodedBy.Name = "lbl_CodedBy";
		lbl_CodedBy.Size = new System.Drawing.Size(239, 23);
		lbl_CodedBy.TabIndex = 7;
		lbl_CodedBy.Text = "Coded by - RobertasLTU#9999";
		lbl_CodedBy.Click += new System.EventHandler(lbl_CodedBy_Click);
		menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
		menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[2]
		{
			updatesToolStripMenuItem,
			aboutToolStripMenuItem
		});
		menuStrip1.Location = new System.Drawing.Point(0, 0);
		menuStrip1.Name = "menuStrip1";
		menuStrip1.Size = new System.Drawing.Size(563, 28);
		menuStrip1.TabIndex = 8;
		menuStrip1.Text = "menuStrip1";
		updatesToolStripMenuItem.Name = "updatesToolStripMenuItem";
		updatesToolStripMenuItem.Size = new System.Drawing.Size(76, 24);
		updatesToolStripMenuItem.Text = "Updates";
		updatesToolStripMenuItem.Click += new System.EventHandler(updatesToolStripMenuItem_Click);
		aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
		aboutToolStripMenuItem.Size = new System.Drawing.Size(62, 24);
		aboutToolStripMenuItem.Text = "About";
		aboutToolStripMenuItem.Click += new System.EventHandler(aboutToolStripMenuItem_Click);
		timerUpdatesStrip.Tick += new System.EventHandler(timerUpdatesStrip_Tick);
		button1.Location = new System.Drawing.Point(12, 42);
		button1.Name = "button1";
		button1.Size = new System.Drawing.Size(140, 78);
		button1.TabIndex = 9;
		button1.Text = "Player buttons";
		button1.UseVisualStyleBackColor = true;
		button1.Click += new System.EventHandler(button1_Click);
		btn_make_worlds_backup.Location = new System.Drawing.Point(168, 42);
		btn_make_worlds_backup.Name = "btn_make_worlds_backup";
		btn_make_worlds_backup.Size = new System.Drawing.Size(140, 78);
		btn_make_worlds_backup.TabIndex = 10;
		btn_make_worlds_backup.Text = "Make worlds backup";
		btn_make_worlds_backup.UseVisualStyleBackColor = true;
		btn_make_worlds_backup.Click += new System.EventHandler(btn_make_worlds_backup_Click);
		Checkmemoryaddress.Location = new System.Drawing.Point(324, 42);
		Checkmemoryaddress.Name = "Checkmemoryaddress";
		Checkmemoryaddress.Size = new System.Drawing.Size(140, 78);
		Checkmemoryaddress.TabIndex = 11;
		Checkmemoryaddress.Text = "Check memory address";
		Checkmemoryaddress.UseVisualStyleBackColor = true;
		Checkmemoryaddress.Click += new System.EventHandler(Checkmemoryaddress_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(563, 348);
		base.Controls.Add(Checkmemoryaddress);
		base.Controls.Add(btn_make_worlds_backup);
		base.Controls.Add(button1);
		base.Controls.Add(lbl_CodedBy);
		base.Controls.Add(btn_all_worlds);
		base.Controls.Add(btn_associated);
		base.Controls.Add(menuStrip1);
		base.Name = "Form1";
		Text = "GTOS MANAGEMENT";
		base.Load += new System.EventHandler(Form1_Load);
		menuStrip1.ResumeLayout(false);
		menuStrip1.PerformLayout();
		ResumeLayout(false);
		PerformLayout();
	}
}