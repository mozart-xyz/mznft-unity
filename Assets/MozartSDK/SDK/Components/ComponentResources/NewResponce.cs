using Mozart;
using System.Collections.Generic;

// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
[System.Serializable]
public class Attribute
{
    public string trait_type;
    public string value;
}

[System.Serializable]
public class Balance
{
    public string balance;
    public string ftId;
    public string ftKey;
    public string name;
}

[System.Serializable]
public class Metadata
{
    public List<Attribute> attributes;
}

[System.Serializable]
public class Org
{
    public string apiKey;
    public string id;
    public string name;
    public string sysuserId;
}

[System.Serializable]
public class Root
{
    public List<Balance> balances;
    public List<object> games;
    public List<Nft> nfts;
    public List<Org> orgs;
    public User user;
}