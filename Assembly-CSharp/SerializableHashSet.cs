using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using UnityEngine;

// Token: 0x02000452 RID: 1106
public class SerializableHashSet<T> : HashSet<T>, ISerializationCallbackReceiver, IXmlSerializable
{
	// Token: 0x06001CD2 RID: 7378 RVA: 0x00155B50 File Offset: 0x00153D50
	public SerializableHashSet()
	{
		this.elements = new List<T>();
	}

	// Token: 0x06001CD3 RID: 7379 RVA: 0x00155B64 File Offset: 0x00153D64
	public void OnBeforeSerialize()
	{
		this.elements.Clear();
		foreach (T item in this)
		{
			this.elements.Add(item);
		}
	}

	// Token: 0x06001CD4 RID: 7380 RVA: 0x00155BC4 File Offset: 0x00153DC4
	public void OnAfterDeserialize()
	{
		base.Clear();
		for (int i = 0; i < this.elements.Count; i++)
		{
			base.Add(this.elements[i]);
		}
	}

	// Token: 0x06001CD5 RID: 7381 RVA: 0x0015596F File Offset: 0x00153B6F
	public XmlSchema GetSchema()
	{
		return null;
	}

	// Token: 0x06001CD6 RID: 7382 RVA: 0x00155C00 File Offset: 0x00153E00
	public void ReadXml(XmlReader reader)
	{
		XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
		bool isEmptyElement = reader.IsEmptyElement;
		reader.Read();
		if (isEmptyElement)
		{
			return;
		}
		while (reader.NodeType != XmlNodeType.EndElement)
		{
			reader.ReadStartElement("Element");
			T item = (T)((object)xmlSerializer.Deserialize(reader));
			reader.ReadEndElement();
			base.Add(item);
			reader.MoveToContent();
		}
	}

	// Token: 0x06001CD7 RID: 7383 RVA: 0x00155C64 File Offset: 0x00153E64
	public void WriteXml(XmlWriter writer)
	{
		XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
		foreach (T t in this)
		{
			writer.WriteStartElement("Element");
			xmlSerializer.Serialize(writer, t);
			writer.WriteEndElement();
		}
	}

	// Token: 0x040035F8 RID: 13816
	[SerializeField]
	private List<T> elements;

	// Token: 0x040035F9 RID: 13817
	private const string XML_Element = "Element";
}
