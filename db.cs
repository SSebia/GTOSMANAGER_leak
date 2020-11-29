using MySql.Data.MySqlClient;

internal class db
{
	public MySqlConnection Connection
	{
		get;
		set;
	}

	private string ip
	{
		get;
		set;
	} = "remotemysql.com";


	private string user
	{
		get;
		set;
	} = "MLn24BtQMq";


	private string pass
	{
		get;
		set;
	} = "3jzstnFbx9";


	private string database
	{
		get;
		set;
	} = "MLn24BtQMq";


	private string ConnString => $"server={ip};database={database};uid={user};pwd={pass};";

	public db()
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Expected O, but got Unknown
		Connection = new MySqlConnection(ConnString);
	}
}