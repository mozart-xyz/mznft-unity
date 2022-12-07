namespace Mozart
{
using System;
using System.Collections;

[Serializable]
public class V1FtsBalances
{

		/*[string] The id of the Balance.*/
		public string id = "";

		/*[string] The id of the FT.*/
		public string ftId = "";

		/*[string] The id of the User.*/
		public string userId = "";

		/*[string] The balance of this FT that this User has.*/
		public string balance = "";

		public string name = "";

		public int GetBalance()
		{
			return int.Parse(balance);
		}


	}
}