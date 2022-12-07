namespace Mozart
{
using System;
using System.Collections;

public class Media
{

	/*[string] The display name you would like to set for the file.*/
	public string fileName;

	/*[string] Contents of a file, represented in base64 format. Get this by loading the bytes of a file into memory,
then converting them into a base64 string. Then using that base64 string as the value for this field.
*/
	public string data;

	
}
}