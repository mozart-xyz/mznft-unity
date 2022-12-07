namespace Mozart
{
using System;
using System.Collections;
    using System.Collections.Generic;

    [Serializable]
	public class User
	{

		/*[string] NO DESCRIPTION, PLEASE ADD TO API.YAML*/
		public string id;

		/*[string] NO DESCRIPTION, PLEASE ADD TO API.YAML*/
		public string name;

		/*[string] NO DESCRIPTION, PLEASE ADD TO API.YAML*/
		public string email;

		/*[string] The wallet address associated with the user.*/
		public string walletAddress;

		/*[array] An array of gameIds that this item is associated with*/
		public List<string> gameIds;

	
	}
}