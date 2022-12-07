namespace Mozart
{
using System;
using System.Collections;
    using System.Collections.Generic;

    [Serializable]
	public class Nft
{

	/*[string] The id of the Nft.*/
	public string id;

	/*[string] The base10 id of the Nft.*/
	public string nftIdBase10;

	/*[string] NO DESCRIPTION, PLEASE ADD TO API.YAML*/
	public string userId;

	/*[string] NO DESCRIPTION, PLEASE ADD TO API.YAML*/
	public string collectionId;

	/*[string] The name of the NFT. This will be shown on third party marketplaces such as OpenSea.
*/
	public string name;

	/*[string] The image url associated with the NFT. This will be shown on third party marketplaces such as OpenSea.
*/
	public string imageUrl;

	/*[string] The description of the NFT. This will be shown on third party marketplaces such as OpenSea
*/
	public string description;

	/*[string] Address of the deployed contract for the NFT.*/
	public string contractAddress;

	/*[object] This includes a variable number of key/value pairs as metadata of the NFT.
*/
	public Dictionary<string, string> metadata;

	
}
}