using System;
using System.ComponentModel;
using System.Data.Common;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using GTOSmanagement;
using MySql.Data.MySqlClient;

public class SuspendAndDeleteFromCheckInItemsByItemId : Form
{
	private string suspend;

	private string delete;

	private int itemid;

	private int quantity;

	private IContainer components = null;

	private Label label1;

	private Label label2;

	private Button btnSuspend;

	private Button btnDelete;

	private Label lblOwner;

	private Label lblPath;

	private void btnSuspend_Click(object sender, EventArgs e)
	{
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Expected O, but got Unknown
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Expected O, but got Unknown
		if (suspend == "not_detected")
		{
			MessageBox.Show("You can't suspend undetected user. The world does not exists.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			return;
		}
		if (suspend == "without_owner")
		{
			MessageBox.Show("You can't suspend that user. Perhaps this world is not locked.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			return;
		}
		db db = new db();
		bool flag = false;
		bool flag2 = false;
		MySqlCommand val = new MySqlCommand("select isBanned from PlayerDatabase where username=@name;", db.Connection);
		try
		{
			((DbConnection)(object)db.Connection).Open();
			val.get_Parameters().AddWithValue("@name", (object)suspend);
			MySqlDataReader val2 = val.ExecuteReader();
			try
			{
				while (((DbDataReader)(object)val2).Read())
				{
					flag = true;
					if (((DbDataReader)(object)val2).GetString(0) == "1")
					{
						flag2 = true;
					}
				}
			}
			finally
			{
				((IDisposable)val2)?.Dispose();
			}
			((DbConnection)(object)db.Connection).Close();
		}
		finally
		{
			((IDisposable)val)?.Dispose();
		}
		if (!flag)
		{
			MessageBox.Show("User does not exists in mysql database.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			return;
		}
		if (flag2)
		{
			MessageBox.Show("User is already banned.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			return;
		}
		MySqlCommand val3 = new MySqlCommand("UPDATE PlayerDatabase SET isBanned=@give where username=@name;", db.Connection);
		try
		{
			val3.get_Parameters().AddWithValue("@give", (object)1);
			val3.get_Parameters().AddWithValue("@name", (object)suspend);
			((DbConnection)(object)db.Connection).Open();
			((DbCommand)(object)val3).ExecuteNonQuery();
			((DbConnection)(object)db.Connection).Close();
			db = null;
			MessageBox.Show("User was suspended.");
			using StreamWriter streamWriter = File.AppendText("management_config/logs.txt");
			streamWriter.WriteLine("\nSuspended user \"" + suspend + "\" for having " + quantity + " of " + itemid + " items in " + delete);
		}
		finally
		{
			((IDisposable)val3)?.Dispose();
		}
	}

	private void btnDelete_Click(object sender, EventArgs e)
	{
		try
		{
			if (delete.Split('/')[0] == "magplant")
			{
				File.Delete("magplant/count/" + delete.Split('/')[2]);
			}
			if (delete.Split('/')[0] == "unstabletesseract")
			{
				File.Delete("unstabletesseract/count/" + delete.Split('/')[2]);
			}
			if (delete.Split('/')[0] == "gaiabeacon")
			{
				File.Delete("gaiabeacon/count/" + delete.Split('/')[2]);
			}
			File.Delete(delete);
			MessageBox.Show("File was removed.");
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString(), "Error delete files.", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void SuspendAndDeleteFromCheckInItemsByItemId_Load(object sender, EventArgs e)
	{
		lblOwner.Text = suspend;
		lblPath.Text = delete;
	}

	public SuspendAndDeleteFromCheckInItemsByItemId(string _suspend, string _delete, int _itemid, int _quantity)
	{
		InitializeComponent();
		suspend = _suspend;
		delete = _delete;
		itemid = _itemid;
		quantity = _quantity;
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
		label2 = new System.Windows.Forms.Label();
		btnSuspend = new System.Windows.Forms.Button();
		btnDelete = new System.Windows.Forms.Button();
		lblOwner = new System.Windows.Forms.Label();
		lblPath = new System.Windows.Forms.Label();
		SuspendLayout();
		label1.AutoSize = true;
		label1.Location = new System.Drawing.Point(26, 22);
		label1.Name = "label1";
		label1.Size = new System.Drawing.Size(53, 17);
		label1.TabIndex = 0;
		label1.Text = "Owner:";
		label2.AutoSize = true;
		label2.Location = new System.Drawing.Point(34, 63);
		label2.Name = "label2";
		label2.Size = new System.Drawing.Size(41, 17);
		label2.TabIndex = 1;
		label2.Text = "Path:";
		btnSuspend.Location = new System.Drawing.Point(37, 121);
		btnSuspend.Name = "btnSuspend";
		btnSuspend.Size = new System.Drawing.Size(99, 49);
		btnSuspend.TabIndex = 2;
		btnSuspend.Text = "Suspend Owner";
		btnSuspend.UseVisualStyleBackColor = true;
		btnSuspend.Click += new System.EventHandler(btnSuspend_Click);
		btnDelete.Location = new System.Drawing.Point(173, 121);
		btnDelete.Name = "btnDelete";
		btnDelete.Size = new System.Drawing.Size(99, 49);
		btnDelete.TabIndex = 3;
		btnDelete.Text = "Delete File";
		btnDelete.UseVisualStyleBackColor = true;
		btnDelete.Click += new System.EventHandler(btnDelete_Click);
		lblOwner.AutoSize = true;
		lblOwner.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
		lblOwner.ForeColor = System.Drawing.Color.Maroon;
		lblOwner.Location = new System.Drawing.Point(85, 22);
		lblOwner.Name = "lblOwner";
		lblOwner.Size = new System.Drawing.Size(62, 18);
		lblOwner.TabIndex = 4;
		lblOwner.Text = "Owner:";
		lblPath.AutoSize = true;
		lblPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
		lblPath.ForeColor = System.Drawing.Color.Maroon;
		lblPath.Location = new System.Drawing.Point(85, 63);
		lblPath.Name = "lblPath";
		lblPath.Size = new System.Drawing.Size(47, 18);
		lblPath.TabIndex = 5;
		lblPath.Text = "Path:";
		base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(512, 195);
		base.Controls.Add(lblPath);
		base.Controls.Add(lblOwner);
		base.Controls.Add(btnDelete);
		base.Controls.Add(btnSuspend);
		base.Controls.Add(label2);
		base.Controls.Add(label1);
		base.Name = "SuspendAndDeleteFromCheckInItemsByItemId";
		Text = "SuspendAndDeleteFromCheckInItemsByItemId";
		base.Load += new System.EventHandler(SuspendAndDeleteFromCheckInItemsByItemId_Load);
		ResumeLayout(false);
		PerformLayout();
	}
}