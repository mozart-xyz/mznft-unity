namespace Mozart
{
using System;
using System.Collections;

public class Collection
{

	/*[string] The id of the Collection*/
	public string id;

	/*[integer] The total number of NFTs in circulation that are associated with this collection.*/
	public int currentSupply;

	/*[string] The name of the collection. This will be shown on third party marketplaces such as OpenSea.*/
	public string name;

	/*[integer] The max number of NFTs that can be created and associated with this collection. If not included, there is an unlimited supply of NFTs that can be created for this collection.*/
	public int maxSupply;

	/*[string] The image associated with the collection. This will be shown on third party marketplaces such as OpenSea.*/
	public string imageUrl;

	/*[string] The description of the collection. This will be shown on third party marketplaces such as OpenSea.*/
	public string description;

	/*[string] The address of the smart contract associated with the Collection.*/
	public string contractAddress;

	/*[string] NO DESCRIPTION, PLEASE ADD TO API.YAML*/
	public string gameId;

	
}
}