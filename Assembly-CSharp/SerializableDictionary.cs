using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using UnityEngine;

// Token: 0x02000443 RID: 1091
public class SerializableDictionary<K, V> : Dictionary<K, V>, ISerializationCallbackReceiver, IXmlSerializable
{
	// Token: 0x06001CBE RID: 7358 RVA: 0x00155883 File Offset: 0x00153A83
	public SerializableDictionary()
	{
		this.keys = new List<K>();
		this.values = new List<V>();
	}

	// Token: 0x06001CBF RID: 7359 RVA: 0x001558A4 File Offset: 0x00153AA4
	public void OnBeforeSerialize()
	{
		this.keys.Clear();
		this.values.Clear();
		foreach (KeyValuePair<K, V> keyValuePair in this)
		{
			this.keys.Add(keyValuePair.Key);
			this.values.Add(keyValuePair.Value);
		}
	}

	// Token: 0x06001CC0 RID: 7360 RVA: 0x00155928 File Offset: 0x00153B28
	public void OnAfterDeserialize()
	{
		base.Clear();
		for (int i = 0; i < this.keys.Count; i++)
		{
			base.Add(this.keys[i], this.values[i]);
		}
	}

	// Token: 0x06001CC1 RID: 7361 RVA: 0x0015596F File Offset: 0x00153B6F
	public XmlSchema GetSchema()
	{
		return null;
	}

	// Token: 0x06001CC2 RID: 7362 RVA: 0x00155974 File Offset: 0x00153B74
	public void ReadXml(XmlReader reader)
	{
		XmlSerializer xmlSerializer = new XmlSerializer(typeof(K));
		XmlSerializer xmlSerializer2 = new XmlSerializer(typeof(V));
		bool isEmptyElement = reader.IsEmptyElement;
		reader.Read();
		if (isEmptyElement)
		{
			return;
		}
		while (reader.NodeType != XmlNodeType.EndElement)
		{
			reader.ReadStartElement("Item");
			reader.ReadStartElement("Key");
			K key = (K)((object)xmlSerializer.Deserialize(reader));
			reader.ReadEndElement();
			reader.ReadStartElement("Value");
			V value = (V)((object)xmlSerializer2.Deserialize(reader));
			reader.ReadEndElement();
			base.Add(key, value);
			reader.ReadEndElement();
			reader.MoveToContent();
		}
		reader.ReadEndElement();
	}

	// Token: 0x06001CC3 RID: 7363 RVA: 0x00155A20 File Offset: 0x00153C20
	public void WriteXml(XmlWriter writer)
	{
		XmlSerializer xmlSerializer = new XmlSerializer(typeof(K));
		XmlSerializer xmlSerializer2 = new XmlSerializer(typeof(V));
		foreach (KeyValuePair<K, V> keyValuePair in this)
		{
			writer.WriteStartElement("Item");
			writer.WriteStartElement("Key");
			xmlSerializer.Serialize(writer, keyValuePair.Key);
			writer.WriteEndElement();
			writer.WriteStartElement("Value");
			xmlSerializer2.Serialize(writer, keyValuePair.Value);
			writer.WriteEndElement();
			writer.WriteEndElement();
		}
	}

	// Token: 0x040035F3 RID: 13811
	[SerializeField]
	private List<K> keys;

	// Token: 0x040035F4 RID: 13812
	[SerializeField]
	private List<V> values;

	// Token: 0x040035F5 RID: 13813
	private const string XML_Item = "Item";

	// Token: 0x040035F6 RID: 13814
	private const string XML_Key = "Key";

	// Token: 0x040035F7 RID: 13815
	private const string XML_Value = "Value";
}
