namespace Mozart
{
using System;
using System.Collections;

	[Serializable]
	public class V1Fts
{

	/*[string] The id of the FT.*/
	public string id;

	/*[string] The ticker symbol for your FT. This must be 4 letters long.*/
	public string symbol;

	/*[string] The name of the FT.
*/
	public string name;

	/*[string] The image url associated with the FT.
*/
	public string imageUrl;

	/*[string] The description of the FT.
*/
	public string description;

	/*[object] This includes a variable number of key/value pairs as metadata of the FT.
*/
	public DictionaryBase metadata;

	/*[string] The number of this FT that has been created. FTs automatically belong to your org default user when FTs
are created or when the supply of FTs increases.
*/
	public string supply;

	
}
}