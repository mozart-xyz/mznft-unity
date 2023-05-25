namespace Mozart
{ 
    using System.Collections.Generic;

    [System.Serializable]
    public class MeResponse
    {
        public User user;
        public List<Nft> nfts= new List<Nft>();
        public List<V1FtsBalances> balances= new List<V1FtsBalances>();

        public MeResponse(User user, List<Nft> nfts, List<V1FtsBalances> balances)
        {
            this.user = user;
            this.nfts = nfts;
            this.balances = balances;
        }
        public MeResponse(User user, List<Nft> nfts, List<Balance> balances)
        {
            this.user = user;
            this.nfts = nfts;

            foreach (var item in balances)
            {
                this.balances.Add(new V1FtsBalances( item));
            }
           
        }
    }
}