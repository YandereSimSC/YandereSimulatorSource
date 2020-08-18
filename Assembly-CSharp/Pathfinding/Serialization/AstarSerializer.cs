using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Pathfinding.Ionic.Zip;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding.Serialization
{
	// Token: 0x020005B9 RID: 1465
	public class AstarSerializer
	{
		// Token: 0x060027E4 RID: 10212 RVA: 0x001B55F0 File Offset: 0x001B37F0
		private static StringBuilder GetStringBuilder()
		{
			AstarSerializer._stringBuilder.Length = 0;
			return AstarSerializer._stringBuilder;
		}

		// Token: 0x060027E5 RID: 10213 RVA: 0x001B5602 File Offset: 0x001B3802
		public AstarSerializer(AstarData data)
		{
			this.data = data;
			this.settings = SerializeSettings.Settings;
		}

		// Token: 0x060027E6 RID: 10214 RVA: 0x001B562E File Offset: 0x001B382E
		public AstarSerializer(AstarData data, SerializeSettings settings)
		{
			this.data = data;
			this.settings = settings;
		}

		// Token: 0x060027E7 RID: 10215 RVA: 0x001B5656 File Offset: 0x001B3856
		public void SetGraphIndexOffset(int offset)
		{
			this.graphIndexOffset = offset;
		}

		// Token: 0x060027E8 RID: 10216 RVA: 0x001B565F File Offset: 0x001B385F
		private void AddChecksum(byte[] bytes)
		{
			this.checksum = Checksum.GetChecksum(bytes, this.checksum);
		}

		// Token: 0x060027E9 RID: 10217 RVA: 0x001B5673 File Offset: 0x001B3873
		private void AddEntry(string name, byte[] bytes)
		{
			this.zip.AddEntry(name, bytes);
		}

		// Token: 0x060027EA RID: 10218 RVA: 0x001B5683 File Offset: 0x001B3883
		public uint GetChecksum()
		{
			return this.checksum;
		}

		// Token: 0x060027EB RID: 10219 RVA: 0x001B568B File Offset: 0x001B388B
		public void OpenSerialize()
		{
			this.zipStream = new MemoryStream();
			this.zip = new ZipFile();
			this.zip.AlternateEncoding = Encoding.UTF8;
			this.zip.AlternateEncodingUsage = ZipOption.Always;
			this.meta = new GraphMeta();
		}

		// Token: 0x060027EC RID: 10220 RVA: 0x001B56CC File Offset: 0x001B38CC
		public byte[] CloseSerialize()
		{
			byte[] array = this.SerializeMeta();
			this.AddChecksum(array);
			this.AddEntry("meta.json", array);
			DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
			foreach (ZipEntry zipEntry in this.zip.Entries)
			{
				zipEntry.AccessedTime = dateTime;
				zipEntry.CreationTime = dateTime;
				zipEntry.LastModified = dateTime;
				zipEntry.ModifiedTime = dateTime;
			}
			this.zip.Save(this.zipStream);
			this.zip.Dispose();
			array = this.zipStream.ToArray();
			this.zip = null;
			this.zipStream = null;
			return array;
		}

		// Token: 0x060027ED RID: 10221 RVA: 0x001B5794 File Offset: 0x001B3994
		public void SerializeGraphs(NavGraph[] _graphs)
		{
			if (this.graphs != null)
			{
				throw new InvalidOperationException("Cannot serialize graphs multiple times.");
			}
			this.graphs = _graphs;
			if (this.zip == null)
			{
				throw new NullReferenceException("You must not call CloseSerialize before a call to this function");
			}
			if (this.graphs == null)
			{
				this.graphs = new NavGraph[0];
			}
			for (int i = 0; i < this.graphs.Length; i++)
			{
				if (this.graphs[i] != null)
				{
					byte[] bytes = this.Serialize(this.graphs[i]);
					this.AddChecksum(bytes);
					this.AddEntry("graph" + i + ".json", bytes);
				}
			}
		}

		// Token: 0x060027EE RID: 10222 RVA: 0x001B5834 File Offset: 0x001B3A34
		private byte[] SerializeMeta()
		{
			if (this.graphs == null)
			{
				throw new Exception("No call to SerializeGraphs has been done");
			}
			this.meta.version = AstarPath.Version;
			this.meta.graphs = this.graphs.Length;
			this.meta.guids = new List<string>();
			this.meta.typeNames = new List<string>();
			for (int i = 0; i < this.graphs.Length; i++)
			{
				if (this.graphs[i] != null)
				{
					this.meta.guids.Add(this.graphs[i].guid.ToString());
					this.meta.typeNames.Add(this.graphs[i].GetType().FullName);
				}
				else
				{
					this.meta.guids.Add(null);
					this.meta.typeNames.Add(null);
				}
			}
			StringBuilder stringBuilder = AstarSerializer.GetStringBuilder();
			TinyJsonSerializer.Serialize(this.meta, stringBuilder);
			return this.encoding.GetBytes(stringBuilder.ToString());
		}

		// Token: 0x060027EF RID: 10223 RVA: 0x001B5948 File Offset: 0x001B3B48
		public byte[] Serialize(NavGraph graph)
		{
			StringBuilder stringBuilder = AstarSerializer.GetStringBuilder();
			TinyJsonSerializer.Serialize(graph, stringBuilder);
			return this.encoding.GetBytes(stringBuilder.ToString());
		}

		// Token: 0x060027F0 RID: 10224 RVA: 0x00002ACE File Offset: 0x00000CCE
		[Obsolete("Not used anymore. You can safely remove the call to this function.")]
		public void SerializeNodes()
		{
		}

		// Token: 0x060027F1 RID: 10225 RVA: 0x001B5974 File Offset: 0x001B3B74
		private static int GetMaxNodeIndexInAllGraphs(NavGraph[] graphs)
		{
			int maxIndex = 0;
			Action<GraphNode> <>9__0;
			for (int i = 0; i < graphs.Length; i++)
			{
				if (graphs[i] != null)
				{
					NavGraph navGraph = graphs[i];
					Action<GraphNode> action;
					if ((action = <>9__0) == null)
					{
						action = (<>9__0 = delegate(GraphNode node)
						{
							maxIndex = Math.Max(node.NodeIndex, maxIndex);
							if (node.NodeIndex == -1)
							{
								Debug.LogError("Graph contains destroyed nodes. This is a bug.");
							}
						});
					}
					navGraph.GetNodes(action);
				}
			}
			return maxIndex;
		}

		// Token: 0x060027F2 RID: 10226 RVA: 0x001B59D0 File Offset: 0x001B3BD0
		private static byte[] SerializeNodeIndices(NavGraph[] graphs)
		{
			MemoryStream memoryStream = new MemoryStream();
			BinaryWriter writer = new BinaryWriter(memoryStream);
			int maxNodeIndexInAllGraphs = AstarSerializer.GetMaxNodeIndexInAllGraphs(graphs);
			writer.Write(maxNodeIndexInAllGraphs);
			int maxNodeIndex2 = 0;
			Action<GraphNode> <>9__0;
			for (int i = 0; i < graphs.Length; i++)
			{
				if (graphs[i] != null)
				{
					NavGraph navGraph = graphs[i];
					Action<GraphNode> action;
					if ((action = <>9__0) == null)
					{
						action = (<>9__0 = delegate(GraphNode node)
						{
							maxNodeIndex2 = Math.Max(node.NodeIndex, maxNodeIndex2);
							writer.Write(node.NodeIndex);
						});
					}
					navGraph.GetNodes(action);
				}
			}
			if (maxNodeIndex2 != maxNodeIndexInAllGraphs)
			{
				throw new Exception("Some graphs are not consistent in their GetNodes calls, sequential calls give different results.");
			}
			byte[] result = memoryStream.ToArray();
			writer.Close();
			return result;
		}

		// Token: 0x060027F3 RID: 10227 RVA: 0x001B5A70 File Offset: 0x001B3C70
		private static byte[] SerializeGraphExtraInfo(NavGraph graph)
		{
			MemoryStream memoryStream = new MemoryStream();
			BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
			GraphSerializationContext ctx = new GraphSerializationContext(binaryWriter);
			((IGraphInternals)graph).SerializeExtraInfo(ctx);
			byte[] result = memoryStream.ToArray();
			binaryWriter.Close();
			return result;
		}

		// Token: 0x060027F4 RID: 10228 RVA: 0x001B5AA4 File Offset: 0x001B3CA4
		private static byte[] SerializeGraphNodeReferences(NavGraph graph)
		{
			MemoryStream memoryStream = new MemoryStream();
			BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
			GraphSerializationContext ctx = new GraphSerializationContext(binaryWriter);
			graph.GetNodes(delegate(GraphNode node)
			{
				node.SerializeReferences(ctx);
			});
			binaryWriter.Close();
			return memoryStream.ToArray();
		}

		// Token: 0x060027F5 RID: 10229 RVA: 0x001B5AEC File Offset: 0x001B3CEC
		public void SerializeExtraInfo()
		{
			if (!this.settings.nodes)
			{
				return;
			}
			if (this.graphs == null)
			{
				throw new InvalidOperationException("Cannot serialize extra info with no serialized graphs (call SerializeGraphs first)");
			}
			byte[] bytes = AstarSerializer.SerializeNodeIndices(this.graphs);
			this.AddChecksum(bytes);
			this.AddEntry("graph_references.binary", bytes);
			for (int i = 0; i < this.graphs.Length; i++)
			{
				if (this.graphs[i] != null)
				{
					bytes = AstarSerializer.SerializeGraphExtraInfo(this.graphs[i]);
					this.AddChecksum(bytes);
					this.AddEntry("graph" + i + "_extra.binary", bytes);
					bytes = AstarSerializer.SerializeGraphNodeReferences(this.graphs[i]);
					this.AddChecksum(bytes);
					this.AddEntry("graph" + i + "_references.binary", bytes);
				}
			}
			bytes = this.SerializeNodeLinks();
			this.AddChecksum(bytes);
			this.AddEntry("node_link2.binary", bytes);
		}

		// Token: 0x060027F6 RID: 10230 RVA: 0x001B5BD2 File Offset: 0x001B3DD2
		private byte[] SerializeNodeLinks()
		{
			MemoryStream memoryStream = new MemoryStream();
			NodeLink2.SerializeReferences(new GraphSerializationContext(new BinaryWriter(memoryStream)));
			return memoryStream.ToArray();
		}

		// Token: 0x060027F7 RID: 10231 RVA: 0x001B5BEE File Offset: 0x001B3DEE
		private ZipEntry GetEntry(string name)
		{
			return this.zip[name];
		}

		// Token: 0x060027F8 RID: 10232 RVA: 0x001B5BFC File Offset: 0x001B3DFC
		private bool ContainsEntry(string name)
		{
			return this.GetEntry(name) != null;
		}

		// Token: 0x060027F9 RID: 10233 RVA: 0x001B5C08 File Offset: 0x001B3E08
		public bool OpenDeserialize(byte[] bytes)
		{
			this.zipStream = new MemoryStream();
			this.zipStream.Write(bytes, 0, bytes.Length);
			this.zipStream.Position = 0L;
			try
			{
				this.zip = ZipFile.Read(this.zipStream);
			}
			catch (Exception arg)
			{
				Debug.LogError("Caught exception when loading from zip\n" + arg);
				this.zipStream.Dispose();
				return false;
			}
			if (this.ContainsEntry("meta.json"))
			{
				this.meta = this.DeserializeMeta(this.GetEntry("meta.json"));
			}
			else
			{
				if (!this.ContainsEntry("meta.binary"))
				{
					throw new Exception("No metadata found in serialized data.");
				}
				this.meta = this.DeserializeBinaryMeta(this.GetEntry("meta.binary"));
			}
			if (AstarSerializer.FullyDefinedVersion(this.meta.version) > AstarSerializer.FullyDefinedVersion(AstarPath.Version))
			{
				Debug.LogWarning(string.Concat(new object[]
				{
					"Trying to load data from a newer version of the A* Pathfinding Project\nCurrent version: ",
					AstarPath.Version,
					" Data version: ",
					this.meta.version,
					"\nThis is usually fine as the stored data is usually backwards and forwards compatible.\nHowever node data (not settings) can get corrupted between versions (even though I try my best to keep compatibility), so it is recommended to recalculate any caches (those for faster startup) and resave any files. Even if it seems to load fine, it might cause subtle bugs.\n"
				}));
			}
			else if (AstarSerializer.FullyDefinedVersion(this.meta.version) < AstarSerializer.FullyDefinedVersion(AstarPath.Version))
			{
				Debug.LogWarning(string.Concat(new object[]
				{
					"Upgrading serialized pathfinding data from version ",
					this.meta.version,
					" to ",
					AstarPath.Version,
					"\nThis is usually fine, it just means you have upgraded to a new version.\nHowever node data (not settings) can get corrupted between versions (even though I try my best to keep compatibility), so it is recommended to recalculate any caches (those for faster startup) and resave any files. Even if it seems to load fine, it might cause subtle bugs.\n"
				}));
			}
			return true;
		}

		// Token: 0x060027FA RID: 10234 RVA: 0x001B5DA0 File Offset: 0x001B3FA0
		private static Version FullyDefinedVersion(Version v)
		{
			return new Version(Mathf.Max(v.Major, 0), Mathf.Max(v.Minor, 0), Mathf.Max(v.Build, 0), Mathf.Max(v.Revision, 0));
		}

		// Token: 0x060027FB RID: 10235 RVA: 0x001B5DD7 File Offset: 0x001B3FD7
		public void CloseDeserialize()
		{
			this.zipStream.Dispose();
			this.zip.Dispose();
			this.zip = null;
			this.zipStream = null;
		}

		// Token: 0x060027FC RID: 10236 RVA: 0x001B5E00 File Offset: 0x001B4000
		private NavGraph DeserializeGraph(int zipIndex, int graphIndex)
		{
			Type graphType = this.meta.GetGraphType(zipIndex);
			if (object.Equals(graphType, null))
			{
				return null;
			}
			NavGraph navGraph = this.data.CreateGraph(graphType);
			navGraph.graphIndex = (uint)graphIndex;
			string name = "graph" + zipIndex + ".json";
			string name2 = "graph" + zipIndex + ".binary";
			if (this.ContainsEntry(name))
			{
				TinyJsonDeserializer.Deserialize(AstarSerializer.GetString(this.GetEntry(name)), graphType, navGraph);
			}
			else
			{
				if (!this.ContainsEntry(name2))
				{
					throw new FileNotFoundException(string.Concat(new object[]
					{
						"Could not find data for graph ",
						zipIndex,
						" in zip. Entry 'graph",
						zipIndex,
						".json' does not exist"
					}));
				}
				GraphSerializationContext ctx = new GraphSerializationContext(AstarSerializer.GetBinaryReader(this.GetEntry(name2)), null, navGraph.graphIndex, this.meta);
				((IGraphInternals)navGraph).DeserializeSettingsCompatibility(ctx);
			}
			if (navGraph.guid.ToString() != this.meta.guids[zipIndex])
			{
				throw new Exception(string.Concat(new object[]
				{
					"Guid in graph file not equal to guid defined in meta file. Have you edited the data manually?\n",
					navGraph.guid,
					" != ",
					this.meta.guids[zipIndex]
				}));
			}
			return navGraph;
		}

		// Token: 0x060027FD RID: 10237 RVA: 0x001B5F60 File Offset: 0x001B4160
		public NavGraph[] DeserializeGraphs()
		{
			List<NavGraph> list = new List<NavGraph>();
			this.graphIndexInZip = new Dictionary<NavGraph, int>();
			for (int i = 0; i < this.meta.graphs; i++)
			{
				int graphIndex = list.Count + this.graphIndexOffset;
				NavGraph navGraph = this.DeserializeGraph(i, graphIndex);
				if (navGraph != null)
				{
					list.Add(navGraph);
					this.graphIndexInZip[navGraph] = i;
				}
			}
			this.graphs = list.ToArray();
			return this.graphs;
		}

		// Token: 0x060027FE RID: 10238 RVA: 0x001B5FD4 File Offset: 0x001B41D4
		private bool DeserializeExtraInfo(NavGraph graph)
		{
			int num = this.graphIndexInZip[graph];
			ZipEntry entry = this.GetEntry("graph" + num + "_extra.binary");
			if (entry == null)
			{
				return false;
			}
			GraphSerializationContext ctx = new GraphSerializationContext(AstarSerializer.GetBinaryReader(entry), null, graph.graphIndex, this.meta);
			((IGraphInternals)graph).DeserializeExtraInfo(ctx);
			return true;
		}

		// Token: 0x060027FF RID: 10239 RVA: 0x001B6030 File Offset: 0x001B4230
		private bool AnyDestroyedNodesInGraphs()
		{
			bool result = false;
			Action<GraphNode> <>9__0;
			for (int i = 0; i < this.graphs.Length; i++)
			{
				NavGraph navGraph = this.graphs[i];
				Action<GraphNode> action;
				if ((action = <>9__0) == null)
				{
					action = (<>9__0 = delegate(GraphNode node)
					{
						if (node.Destroyed)
						{
							result = true;
						}
					});
				}
				navGraph.GetNodes(action);
			}
			return result;
		}

		// Token: 0x06002800 RID: 10240 RVA: 0x001B6090 File Offset: 0x001B4290
		private GraphNode[] DeserializeNodeReferenceMap()
		{
			ZipEntry entry = this.GetEntry("graph_references.binary");
			if (entry == null)
			{
				throw new Exception("Node references not found in the data. Was this loaded from an older version of the A* Pathfinding Project?");
			}
			BinaryReader reader = AstarSerializer.GetBinaryReader(entry);
			int num = reader.ReadInt32();
			GraphNode[] int2Node = new GraphNode[num + 1];
			try
			{
				Action<GraphNode> <>9__0;
				for (int i = 0; i < this.graphs.Length; i++)
				{
					NavGraph navGraph = this.graphs[i];
					Action<GraphNode> action;
					if ((action = <>9__0) == null)
					{
						action = (<>9__0 = delegate(GraphNode node)
						{
							int num2 = reader.ReadInt32();
							int2Node[num2] = node;
						});
					}
					navGraph.GetNodes(action);
				}
			}
			catch (Exception innerException)
			{
				throw new Exception("Some graph(s) has thrown an exception during GetNodes, or some graph(s) have deserialized more or fewer nodes than were serialized", innerException);
			}
			if (reader.BaseStream.Position != reader.BaseStream.Length)
			{
				throw new Exception(string.Concat(new object[]
				{
					reader.BaseStream.Length / 4L,
					" nodes were serialized, but only data for ",
					reader.BaseStream.Position / 4L,
					" nodes was found. The data looks corrupt."
				}));
			}
			reader.Close();
			return int2Node;
		}

		// Token: 0x06002801 RID: 10241 RVA: 0x001B61D0 File Offset: 0x001B43D0
		private void DeserializeNodeReferences(NavGraph graph, GraphNode[] int2Node)
		{
			int num = this.graphIndexInZip[graph];
			ZipEntry entry = this.GetEntry("graph" + num + "_references.binary");
			if (entry == null)
			{
				throw new Exception("Node references for graph " + num + " not found in the data. Was this loaded from an older version of the A* Pathfinding Project?");
			}
			BinaryReader binaryReader = AstarSerializer.GetBinaryReader(entry);
			GraphSerializationContext ctx = new GraphSerializationContext(binaryReader, int2Node, graph.graphIndex, this.meta);
			graph.GetNodes(delegate(GraphNode node)
			{
				node.DeserializeReferences(ctx);
			});
		}

		// Token: 0x06002802 RID: 10242 RVA: 0x001B625C File Offset: 0x001B445C
		public void DeserializeExtraInfo()
		{
			bool flag = false;
			for (int i = 0; i < this.graphs.Length; i++)
			{
				flag |= this.DeserializeExtraInfo(this.graphs[i]);
			}
			if (!flag)
			{
				return;
			}
			if (this.AnyDestroyedNodesInGraphs())
			{
				Debug.LogError("Graph contains destroyed nodes. This is a bug.");
			}
			GraphNode[] int2Node = this.DeserializeNodeReferenceMap();
			for (int j = 0; j < this.graphs.Length; j++)
			{
				this.DeserializeNodeReferences(this.graphs[j], int2Node);
			}
			this.DeserializeNodeLinks(int2Node);
		}

		// Token: 0x06002803 RID: 10243 RVA: 0x001B62D8 File Offset: 0x001B44D8
		private void DeserializeNodeLinks(GraphNode[] int2Node)
		{
			ZipEntry entry = this.GetEntry("node_link2.binary");
			if (entry == null)
			{
				return;
			}
			NodeLink2.DeserializeReferences(new GraphSerializationContext(AstarSerializer.GetBinaryReader(entry), int2Node, 0u, this.meta));
		}

		// Token: 0x06002804 RID: 10244 RVA: 0x001B6310 File Offset: 0x001B4510
		public void PostDeserialization()
		{
			for (int i = 0; i < this.graphs.Length; i++)
			{
				GraphSerializationContext ctx = new GraphSerializationContext(null, null, 0u, this.meta);
				((IGraphInternals)this.graphs[i]).PostDeserialization(ctx);
			}
		}

		// Token: 0x06002805 RID: 10245 RVA: 0x001B6350 File Offset: 0x001B4550
		public void DeserializeEditorSettingsCompatibility()
		{
			for (int i = 0; i < this.graphs.Length; i++)
			{
				int num = this.graphIndexInZip[this.graphs[i]];
				ZipEntry entry = this.GetEntry("graph" + num + "_editor.json");
				if (entry != null)
				{
					((IGraphInternals)this.graphs[i]).SerializedEditorSettings = AstarSerializer.GetString(entry);
				}
			}
		}

		// Token: 0x06002806 RID: 10246 RVA: 0x001B63B8 File Offset: 0x001B45B8
		private static BinaryReader GetBinaryReader(ZipEntry entry)
		{
			MemoryStream memoryStream = new MemoryStream();
			entry.Extract(memoryStream);
			memoryStream.Position = 0L;
			return new BinaryReader(memoryStream);
		}

		// Token: 0x06002807 RID: 10247 RVA: 0x001B63E0 File Offset: 0x001B45E0
		private static string GetString(ZipEntry entry)
		{
			MemoryStream memoryStream = new MemoryStream();
			entry.Extract(memoryStream);
			memoryStream.Position = 0L;
			StreamReader streamReader = new StreamReader(memoryStream);
			string result = streamReader.ReadToEnd();
			streamReader.Dispose();
			return result;
		}

		// Token: 0x06002808 RID: 10248 RVA: 0x001B6415 File Offset: 0x001B4615
		private GraphMeta DeserializeMeta(ZipEntry entry)
		{
			return TinyJsonDeserializer.Deserialize(AstarSerializer.GetString(entry), typeof(GraphMeta), null) as GraphMeta;
		}

		// Token: 0x06002809 RID: 10249 RVA: 0x001B6434 File Offset: 0x001B4634
		private GraphMeta DeserializeBinaryMeta(ZipEntry entry)
		{
			GraphMeta graphMeta = new GraphMeta();
			BinaryReader binaryReader = AstarSerializer.GetBinaryReader(entry);
			if (binaryReader.ReadString() != "A*")
			{
				throw new Exception("Invalid magic number in saved data");
			}
			int num = binaryReader.ReadInt32();
			int num2 = binaryReader.ReadInt32();
			int num3 = binaryReader.ReadInt32();
			int num4 = binaryReader.ReadInt32();
			if (num < 0)
			{
				graphMeta.version = new Version(0, 0);
			}
			else if (num2 < 0)
			{
				graphMeta.version = new Version(num, 0);
			}
			else if (num3 < 0)
			{
				graphMeta.version = new Version(num, num2);
			}
			else if (num4 < 0)
			{
				graphMeta.version = new Version(num, num2, num3);
			}
			else
			{
				graphMeta.version = new Version(num, num2, num3, num4);
			}
			graphMeta.graphs = binaryReader.ReadInt32();
			graphMeta.guids = new List<string>();
			int num5 = binaryReader.ReadInt32();
			for (int i = 0; i < num5; i++)
			{
				graphMeta.guids.Add(binaryReader.ReadString());
			}
			graphMeta.typeNames = new List<string>();
			num5 = binaryReader.ReadInt32();
			for (int j = 0; j < num5; j++)
			{
				graphMeta.typeNames.Add(binaryReader.ReadString());
			}
			return graphMeta;
		}

		// Token: 0x0600280A RID: 10250 RVA: 0x001B6564 File Offset: 0x001B4764
		public static void SaveToFile(string path, byte[] data)
		{
			using (FileStream fileStream = new FileStream(path, FileMode.Create))
			{
				fileStream.Write(data, 0, data.Length);
			}
		}

		// Token: 0x0600280B RID: 10251 RVA: 0x001B65A0 File Offset: 0x001B47A0
		public static byte[] LoadFromFile(string path)
		{
			byte[] result;
			using (FileStream fileStream = new FileStream(path, FileMode.Open))
			{
				byte[] array = new byte[(int)fileStream.Length];
				fileStream.Read(array, 0, (int)fileStream.Length);
				result = array;
			}
			return result;
		}

		// Token: 0x04004242 RID: 16962
		private AstarData data;

		// Token: 0x04004243 RID: 16963
		private ZipFile zip;

		// Token: 0x04004244 RID: 16964
		private MemoryStream zipStream;

		// Token: 0x04004245 RID: 16965
		private GraphMeta meta;

		// Token: 0x04004246 RID: 16966
		private SerializeSettings settings;

		// Token: 0x04004247 RID: 16967
		private NavGraph[] graphs;

		// Token: 0x04004248 RID: 16968
		private Dictionary<NavGraph, int> graphIndexInZip;

		// Token: 0x04004249 RID: 16969
		private int graphIndexOffset;

		// Token: 0x0400424A RID: 16970
		private const string binaryExt = ".binary";

		// Token: 0x0400424B RID: 16971
		private const string jsonExt = ".json";

		// Token: 0x0400424C RID: 16972
		private uint checksum = uint.MaxValue;

		// Token: 0x0400424D RID: 16973
		private UTF8Encoding encoding = new UTF8Encoding();

		// Token: 0x0400424E RID: 16974
		private static StringBuilder _stringBuilder = new StringBuilder();

		// Token: 0x0400424F RID: 16975
		public static readonly Version V3_8_3 = new Version(3, 8, 3);

		// Token: 0x04004250 RID: 16976
		public static readonly Version V3_9_0 = new Version(3, 9, 0);

		// Token: 0x04004251 RID: 16977
		public static readonly Version V4_1_0 = new Version(4, 1, 0);
	}
}
