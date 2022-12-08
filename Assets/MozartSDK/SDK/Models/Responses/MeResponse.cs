namespace Mozart
{ 
    using System.Collections.Generic;

    public class MeResponse
    {
        public User user;
        public List<Nft> nfts;
        public List<V1FtsBalances> balances;
    }
}