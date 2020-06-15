using System;
using System.IO;
using UnityEngine;

namespace Pathfinding.Serialization
{
	// Token: 0x020005CE RID: 1486
	public class GraphSerializationContext
	{
		// Token: 0x0600282F RID: 10287 RVA: 0x001BAEFF File Offset: 0x001B90FF
		public GraphSerializationContext(BinaryReader reader, GraphNode[] id2NodeMapping, uint graphIndex, GraphMeta meta)
		{
			this.reader = reader;
			this.id2NodeMapping = id2NodeMapping;
			this.graphIndex = graphIndex;
			this.meta = meta;
		}

		// Token: 0x06002830 RID: 10288 RVA: 0x001BAF24 File Offset: 0x001B9124
		public GraphSerializationContext(BinaryWriter writer)
		{
			this.writer = writer;
		}

		// Token: 0x06002831 RID: 10289 RVA: 0x001BAF33 File Offset: 0x001B9133
		public void SerializeNodeReference(GraphNode node)
		{
			this.writer.Write((node == null) ? -1 : node.NodeIndex);
		}

		// Token: 0x06002832 RID: 10290 RVA: 0x001BAF4C File Offset: 0x001B914C
		public GraphNode DeserializeNodeReference()
		{
			int num = this.reader.ReadInt32();
			if (this.id2NodeMapping == null)
			{
				throw new Exception("Calling DeserializeNodeReference when not deserializing node references");
			}
			if (num == -1)
			{
				return null;
			}
			GraphNode graphNode = this.id2NodeMapping[num];
			if (graphNode == null)
			{
				throw new Exception("Invalid id (" + num + ")");
			}
			return graphNode;
		}

		// Token: 0x06002833 RID: 10291 RVA: 0x001BAFA4 File Offset: 0x001B91A4
		public void SerializeVector3(Vector3 v)
		{
			this.writer.Write(v.x);
			this.writer.Write(v.y);
			this.writer.Write(v.z);
		}

		// Token: 0x06002834 RID: 10292 RVA: 0x001BAFD9 File Offset: 0x001B91D9
		public Vector3 DeserializeVector3()
		{
			return new Vector3(this.reader.ReadSingle(), this.reader.ReadSingle(), this.reader.ReadSingle());
		}

		// Token: 0x06002835 RID: 10293 RVA: 0x001BB001 File Offset: 0x001B9201
		public void SerializeInt3(Int3 v)
		{
			this.writer.Write(v.x);
			this.writer.Write(v.y);
			this.writer.Write(v.z);
		}

		// Token: 0x06002836 RID: 10294 RVA: 0x001BB036 File Offset: 0x001B9236
		public Int3 DeserializeInt3()
		{
			return new Int3(this.reader.ReadInt32(), this.reader.ReadInt32(), this.reader.ReadInt32());
		}

		// Token: 0x06002837 RID: 10295 RVA: 0x001BB05E File Offset: 0x001B925E
		public int DeserializeInt(int defaultValue)
		{
			if (this.reader.BaseStream.Position <= this.reader.BaseStream.Length - 4L)
			{
				return this.reader.ReadInt32();
			}
			return defaultValue;
		}

		// Token: 0x06002838 RID: 10296 RVA: 0x001BB092 File Offset: 0x001B9292
		public float DeserializeFloat(float defaultValue)
		{
			if (this.reader.BaseStream.Position <= this.reader.BaseStream.Length - 4L)
			{
				return this.reader.ReadSingle();
			}
			return defaultValue;
		}

		// Token: 0x06002839 RID: 10297 RVA: 0x001BB0C8 File Offset: 0x001B92C8
		public UnityEngine.Object DeserializeUnityObject()
		{
			if (this.reader.ReadInt32() == 2147483647)
			{
				return null;
			}
			string text = this.reader.ReadString();
			string text2 = this.reader.ReadString();
			string text3 = this.reader.ReadString();
			Type type = Type.GetType(text2);
			if (type == null)
			{
				Debug.LogError("Could not find type '" + text2 + "'. Cannot deserialize Unity reference");
				return null;
			}
			if (!string.IsNullOrEmpty(text3))
			{
				UnityReferenceHelper[] array = UnityEngine.Object.FindObjectsOfType(typeof(UnityReferenceHelper)) as UnityReferenceHelper[];
				int i = 0;
				while (i < array.Length)
				{
					if (array[i].GetGUID() == text3)
					{
						if (type == typeof(GameObject))
						{
							return array[i].gameObject;
						}
						return array[i].GetComponent(type);
					}
					else
					{
						i++;
					}
				}
			}
			UnityEngine.Object[] array2 = Resources.LoadAll(text, type);
			for (int j = 0; j < array2.Length; j++)
			{
				if (array2[j].name == text || array2.Length == 1)
				{
					return array2[j];
				}
			}
			return null;
		}

		// Token: 0x0400431B RID: 17179
		private readonly GraphNode[] id2NodeMapping;

		// Token: 0x0400431C RID: 17180
		public readonly BinaryReader reader;

		// Token: 0x0400431D RID: 17181
		public readonly BinaryWriter writer;

		// Token: 0x0400431E RID: 17182
		public readonly uint graphIndex;

		// Token: 0x0400431F RID: 17183
		public readonly GraphMeta meta;
	}
}
