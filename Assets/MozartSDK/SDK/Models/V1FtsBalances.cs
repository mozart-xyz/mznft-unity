namespace Mozart
{
using System;
using System.Collections;

	[System.Serializable]
	public class V1FtsBalances
{

		public string balance;
		public string ftId;
		public string ftKey;
		public string name;

        public V1FtsBalances(Balance balance)
        {
            this.balance = balance.balance;
            this.ftId = balance.ftId;
            this.ftKey = balance. ftKey;
            this.name = balance.name;
        }

        public int GetBalance()
		{
			return int.Parse(balance);
		}


	}
}