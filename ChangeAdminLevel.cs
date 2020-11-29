using System;
using System.ComponentModel;
using System.Data.Common;
using System.Drawing;
using System.Windows.Forms;
using GTOSmanagement;
using MySql.Data.MySqlClient;

public class ChangeAdminLevel : Form
{
	private string username;

	private int adminLevel;

	private IContainer components = null;

	private Label label1;

	private Label label2;

	private Label lblGrowid;

	private Label lblCurrentAdmLvl;

	private TextBox txtNewAdminLvl;

	private Button btnConfirm;

	private Label label3;

	private Button btnSuspend;

	public ChangeAdminLevel(string usrName, int admLevel)
	{
		InitializeComponent();
		username = usrName;
		adminLevel = admLevel;
	}

	private void ChangeAdminLevel_Load(object sender, EventArgs e)
	{
		lblGrowid.Text = username;
		lblCurrentAdmLvl.Text = adminLevel.ToString();
	}

	private void btnConfirm_Click(object sender, EventArgs e)
	{
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Expected O, but got Unknown
		string text = txtNewAdminLvl.Text;
		if (text == "0" || text == "1" || text == "2")
		{
			db db = new db();
			MySqlCommand val = new MySqlCommand("UPDATE PlayerDatabase SET adminLevel=@ans where username=@name;", db.Connection);
			try
			{
				val.get_Parameters().AddWithValue("@ans", (object)text);
				val.get_Parameters().AddWithValue("@name", (object)username);
				((DbConnection)(object)db.Connection).Open();
				((DbCommand)(object)val).ExecuteNonQuery();
				((DbConnection)(object)db.Connection).Close();
				db = null;
				MessageBox.Show("User's Admin-level updated.");
				Close();
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
		}
		else
		{
			MessageBox.Show("Wrong level number.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void btnSuspend_Click(object sender, EventArgs e)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		db db = new db();
		MySqlCommand val = new MySqlCommand("UPDATE PlayerDatabase SET isBanned=1 where username=@name;", db.Connection);
		try
		{
			val.get_Parameters().AddWithValue("@name", (object)username);
			((DbConnection)(object)db.Connection).Open();
			((DbCommand)(object)val).ExecuteNonQuery();
			((DbConnection)(object)db.Connection).Close();
			db = null;
			MessageBox.Show("User suspended");
		}
		finally
		{
			((IDisposable)val)?.Dispose();
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
		label1 = new System.Windows.Forms.Label();
		label2 = new System.Windows.Forms.Label();
		lblGrowid = new System.Windows.Forms.Label();
		lblCurrentAdmLvl = new System.Windows.Forms.Label();
		txtNewAdminLvl = new System.Windows.Forms.TextBox();
		btnConfirm = new System.Windows.Forms.Button();
		label3 = new System.Windows.Forms.Label();
		btnSuspend = new System.Windows.Forms.Button();
		SuspendLayout();
		label1.AutoSize = true;
		label1.Font = new System.Drawing.Font("Microsoft Tai Le", 7.8f, System.Drawing.FontStyle.Italic | System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, 0);
		label1.Location = new System.Drawing.Point(12, 49);
		label1.Name = "label1";
		label1.Size = new System.Drawing.Size(133, 18);
		label1.TabIndex = 0;
		label1.Text = "Current AdminLevel:";
		label2.AutoSize = true;
		label2.Font = new System.Drawing.Font("Microsoft Tai Le", 7.8f, System.Drawing.FontStyle.Italic | System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, 0);
		label2.Location = new System.Drawing.Point(12, 9);
		label2.Name = "label2";
		label2.Size = new System.Drawing.Size(58, 18);
		label2.TabIndex = 1;
		label2.Text = "GrowID:";
		lblGrowid.AutoSize = true;
		lblGrowid.Font = new System.Drawing.Font("Microsoft YaHei", 7.8f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
		lblGrowid.Location = new System.Drawing.Point(76, 9);
		lblGrowid.Name = "lblGrowid";
		lblGrowid.Size = new System.Drawing.Size(17, 19);
		lblGrowid.TabIndex = 2;
		lblGrowid.Text = "x";
		lblCurrentAdmLvl.AutoSize = true;
		lblCurrentAdmLvl.Font = new System.Drawing.Font("Microsoft YaHei", 7.8f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
		lblCurrentAdmLvl.Location = new System.Drawing.Point(154, 49);
		lblCurrentAdmLvl.Name = "lblCurrentAdmLvl";
		lblCurrentAdmLvl.Size = new System.Drawing.Size(17, 19);
		lblCurrentAdmLvl.TabIndex = 3;
		lblCurrentAdmLvl.Text = "x";
		txtNewAdminLvl.Location = new System.Drawing.Point(287, 113);
		txtNewAdminLvl.Name = "txtNewAdminLvl";
		txtNewAdminLvl.Size = new System.Drawing.Size(100, 22);
		txtNewAdminLvl.TabIndex = 4;
		btnConfirm.Location = new System.Drawing.Point(312, 156);
		btnConfirm.Name = "btnConfirm";
		btnConfirm.Size = new System.Drawing.Size(75, 31);
		btnConfirm.TabIndex = 5;
		btnConfirm.Text = "Confirm";
		btnConfirm.UseVisualStyleBackColor = true;
		btnConfirm.Click += new System.EventHandler(btnConfirm_Click);
		label3.AutoSize = true;
		label3.Font = new System.Drawing.Font("Microsoft Tai Le", 7.8f, System.Drawing.FontStyle.Italic | System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, 0);
		label3.Location = new System.Drawing.Point(12, 117);
		label3.Name = "label3";
		label3.Size = new System.Drawing.Size(229, 18);
		label3.TabIndex = 6;
		label3.Text = "Type here a new user's Admin Level:";
		btnSuspend.Location = new System.Drawing.Point(312, 12);
		btnSuspend.Name = "btnSuspend";
		btnSuspend.Size = new System.Drawing.Size(89, 32);
		btnSuspend.TabIndex = 7;
		btnSuspend.Text = "Suspend";
		btnSuspend.UseVisualStyleBackColor = true;
		btnSuspend.Click += new System.EventHandler(btnSuspend_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(417, 199);
		base.Controls.Add(btnSuspend);
		base.Controls.Add(label3);
		base.Controls.Add(btnConfirm);
		base.Controls.Add(txtNewAdminLvl);
		base.Controls.Add(lblCurrentAdmLvl);
		base.Controls.Add(lblGrowid);
		base.Controls.Add(label2);
		base.Controls.Add(label1);
		base.Name = "ChangeAdminLevel";
		Text = "ChangeAdminLevel";
		base.Load += new System.EventHandler(ChangeAdminLevel_Load);
		ResumeLayout(false);
		PerformLayout();
	}
}