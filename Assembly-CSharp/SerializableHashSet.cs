using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using UnityEngine;

// Token: 0x02000457 RID: 1111
public class SerializableHashSet<T> : HashSet<T>, ISerializationCallbackReceiver, IXmlSerializable
{
	// Token: 0x06001CF7 RID: 7415 RVA: 0x00159054 File Offset: 0x00157254
	public SerializableHashSet()
	{
		this.elements = new List<T>();
	}

	// Token: 0x06001CF8 RID: 7416 RVA: 0x00159068 File Offset: 0x00157268
	public void OnBeforeSerialize()
	{
		this.elements.Clear();
		foreach (T item in this)
		{
			this.elements.Add(item);
		}
	}

	// Token: 0x06001CF9 RID: 7417 RVA: 0x001590C8 File Offset: 0x001572C8
	public void OnAfterDeserialize()
	{
		base.Clear();
		for (int i = 0; i < this.elements.Count; i++)
		{
			base.Add(this.elements[i]);
		}
	}

	// Token: 0x06001CFA RID: 7418 RVA: 0x00158E73 File Offset: 0x00157073
	public XmlSchema GetSchema()
	{
		return null;
	}

	// Token: 0x06001CFB RID: 7419 RVA: 0x00159104 File Offset: 0x00157304
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

	// Token: 0x06001CFC RID: 7420 RVA: 0x00159168 File Offset: 0x00157368
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

	// Token: 0x0400366D RID: 13933
	[SerializeField]
	private List<T> elements;

	// Token: 0x0400366E RID: 13934
	private const string XML_Element = "Element";
}
