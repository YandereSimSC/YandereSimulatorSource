using System;
using System.IO;
using UnityEngine;

namespace Pathfinding.Serialization
{
	// Token: 0x020005B8 RID: 1464
	public class GraphSerializationContext
	{
		// Token: 0x060027D9 RID: 10201 RVA: 0x001B5313 File Offset: 0x001B3513
		public GraphSerializationContext(BinaryReader reader, GraphNode[] id2NodeMapping, uint graphIndex, GraphMeta meta)
		{
			this.reader = reader;
			this.id2NodeMapping = id2NodeMapping;
			this.graphIndex = graphIndex;
			this.meta = meta;
		}

		// Token: 0x060027DA RID: 10202 RVA: 0x001B5338 File Offset: 0x001B3538
		public GraphSerializationContext(BinaryWriter writer)
		{
			this.writer = writer;
		}

		// Token: 0x060027DB RID: 10203 RVA: 0x001B5347 File Offset: 0x001B3547
		public void SerializeNodeReference(GraphNode node)
		{
			this.writer.Write((node == null) ? -1 : node.NodeIndex);
		}

		// Token: 0x060027DC RID: 10204 RVA: 0x001B5360 File Offset: 0x001B3560
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

		// Token: 0x060027DD RID: 10205 RVA: 0x001B53B8 File Offset: 0x001B35B8
		public void SerializeVector3(Vector3 v)
		{
			this.writer.Write(v.x);
			this.writer.Write(v.y);
			this.writer.Write(v.z);
		}

		// Token: 0x060027DE RID: 10206 RVA: 0x001B53ED File Offset: 0x001B35ED
		public Vector3 DeserializeVector3()
		{
			return new Vector3(this.reader.ReadSingle(), this.reader.ReadSingle(), this.reader.ReadSingle());
		}

		// Token: 0x060027DF RID: 10207 RVA: 0x001B5415 File Offset: 0x001B3615
		public void SerializeInt3(Int3 v)
		{
			this.writer.Write(v.x);
			this.writer.Write(v.y);
			this.writer.Write(v.z);
		}

		// Token: 0x060027E0 RID: 10208 RVA: 0x001B544A File Offset: 0x001B364A
		public Int3 DeserializeInt3()
		{
			return new Int3(this.reader.ReadInt32(), this.reader.ReadInt32(), this.reader.ReadInt32());
		}

		// Token: 0x060027E1 RID: 10209 RVA: 0x001B5472 File Offset: 0x001B3672
		public int DeserializeInt(int defaultValue)
		{
			if (this.reader.BaseStream.Position <= this.reader.BaseStream.Length - 4L)
			{
				return this.reader.ReadInt32();
			}
			return defaultValue;
		}

		// Token: 0x060027E2 RID: 10210 RVA: 0x001B54A6 File Offset: 0x001B36A6
		public float DeserializeFloat(float defaultValue)
		{
			if (this.reader.BaseStream.Position <= this.reader.BaseStream.Length - 4L)
			{
				return this.reader.ReadSingle();
			}
			return defaultValue;
		}

		// Token: 0x060027E3 RID: 10211 RVA: 0x001B54DC File Offset: 0x001B36DC
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

		// Token: 0x0400423D RID: 16957
		private readonly GraphNode[] id2NodeMapping;

		// Token: 0x0400423E RID: 16958
		public readonly BinaryReader reader;

		// Token: 0x0400423F RID: 16959
		public readonly BinaryWriter writer;

		// Token: 0x04004240 RID: 16960
		public readonly uint graphIndex;

		// Token: 0x04004241 RID: 16961
		public readonly GraphMeta meta;
	}
}
