namespace Mozart
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    /* From GO code schema
     * field.Uint64("id").DefaultFunc(entutils.GenUint64Id),
		field.Time("created").Default(time.Now),
		field.Time("modified").Default(time.Now).UpdateDefault(time.Now),

		field.String("name").Default(""),
		field.String("image").Default(""),
		field.String("description").Default(""),
		field.JSON("metadata", map[string]interface{}{}).Default(map[string]interface{}{}),

		// from-edges
		field.Uint64("userId"),
		field.Uint64("collectionId"),*/

    public class NFTItem
    {
        public string id;
        public DateTime created;
        public DateTime modified;
        public string name;
        public string image;
        public string description;
        public Dictionary<String, DictionaryBase> metadata = new Dictionary<string, DictionaryBase>();
        public string userId;
        public string collectionId;
    }

}