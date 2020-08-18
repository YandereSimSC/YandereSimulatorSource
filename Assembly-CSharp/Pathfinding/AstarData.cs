using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding.Serialization;
using Pathfinding.Util;
using Pathfinding.WindowsStore;
using UnityEngine;
using UnityEngine.Serialization;

namespace Pathfinding
{
	// Token: 0x0200051A RID: 1306
	[Serializable]
	public class AstarData
	{
		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x06002176 RID: 8566 RVA: 0x0018AA54 File Offset: 0x00188C54
		public static AstarPath active
		{
			get
			{
				return AstarPath.active;
			}
		}

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x06002177 RID: 8567 RVA: 0x0018AA5B File Offset: 0x00188C5B
		// (set) Token: 0x06002178 RID: 8568 RVA: 0x0018AA63 File Offset: 0x00188C63
		public NavMeshGraph navmesh { get; private set; }

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x06002179 RID: 8569 RVA: 0x0018AA6C File Offset: 0x00188C6C
		// (set) Token: 0x0600217A RID: 8570 RVA: 0x0018AA74 File Offset: 0x00188C74
		public GridGraph gridGraph { get; private set; }

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x0600217B RID: 8571 RVA: 0x0018AA7D File Offset: 0x00188C7D
		// (set) Token: 0x0600217C RID: 8572 RVA: 0x0018AA85 File Offset: 0x00188C85
		public LayerGridGraph layerGridGraph { get; private set; }

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x0600217D RID: 8573 RVA: 0x0018AA8E File Offset: 0x00188C8E
		// (set) Token: 0x0600217E RID: 8574 RVA: 0x0018AA96 File Offset: 0x00188C96
		public PointGraph pointGraph { get; private set; }

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x0600217F RID: 8575 RVA: 0x0018AA9F File Offset: 0x00188C9F
		// (set) Token: 0x06002180 RID: 8576 RVA: 0x0018AAA7 File Offset: 0x00188CA7
		public RecastGraph recastGraph { get; private set; }

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x06002181 RID: 8577 RVA: 0x0018AAB0 File Offset: 0x00188CB0
		// (set) Token: 0x06002182 RID: 8578 RVA: 0x0018AAB8 File Offset: 0x00188CB8
		public Type[] graphTypes { get; private set; }

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x06002183 RID: 8579 RVA: 0x0018AAC1 File Offset: 0x00188CC1
		// (set) Token: 0x06002184 RID: 8580 RVA: 0x0018AAFC File Offset: 0x00188CFC
		private byte[] data
		{
			get
			{
				if (this.upgradeData != null && this.upgradeData.Length != 0)
				{
					this.data = this.upgradeData;
					this.upgradeData = null;
				}
				if (this.dataString == null)
				{
					return null;
				}
				return Convert.FromBase64String(this.dataString);
			}
			set
			{
				this.dataString = ((value != null) ? Convert.ToBase64String(value) : null);
			}
		}

		// Token: 0x06002185 RID: 8581 RVA: 0x0018AB10 File Offset: 0x00188D10
		public byte[] GetData()
		{
			return this.data;
		}

		// Token: 0x06002186 RID: 8582 RVA: 0x0018AB18 File Offset: 0x00188D18
		public void SetData(byte[] data)
		{
			this.data = data;
		}

		// Token: 0x06002187 RID: 8583 RVA: 0x0018AB21 File Offset: 0x00188D21
		public void Awake()
		{
			this.graphs = new NavGraph[0];
			if (this.cacheStartup && this.file_cachedStartup != null)
			{
				this.LoadFromCache();
				return;
			}
			this.DeserializeGraphs();
		}

		// Token: 0x06002188 RID: 8584 RVA: 0x0018AB52 File Offset: 0x00188D52
		internal void LockGraphStructure(bool allowAddingGraphs = false)
		{
			this.graphStructureLocked.Add(allowAddingGraphs);
		}

		// Token: 0x06002189 RID: 8585 RVA: 0x0018AB60 File Offset: 0x00188D60
		internal void UnlockGraphStructure()
		{
			if (this.graphStructureLocked.Count == 0)
			{
				throw new InvalidOperationException();
			}
			this.graphStructureLocked.RemoveAt(this.graphStructureLocked.Count - 1);
		}

		// Token: 0x0600218A RID: 8586 RVA: 0x0018AB90 File Offset: 0x00188D90
		private PathProcessor.GraphUpdateLock AssertSafe(bool onlyAddingGraph = false)
		{
			if (this.graphStructureLocked.Count > 0)
			{
				bool flag = true;
				for (int i = 0; i < this.graphStructureLocked.Count; i++)
				{
					flag &= this.graphStructureLocked[i];
				}
				if (!onlyAddingGraph || !flag)
				{
					throw new InvalidOperationException("Graphs cannot be added, removed or serialized while the graph structure is locked. This is the case when a graph is currently being scanned and when executing graph updates and work items.\nHowever as a special case, graphs can be added inside work items.");
				}
			}
			PathProcessor.GraphUpdateLock result = AstarData.active.PausePathfinding();
			if (!AstarData.active.IsInsideWorkItem)
			{
				AstarData.active.FlushWorkItems();
				AstarData.active.pathReturnQueue.ReturnPaths(false);
			}
			return result;
		}

		// Token: 0x0600218B RID: 8587 RVA: 0x0018AC14 File Offset: 0x00188E14
		public void UpdateShortcuts()
		{
			this.navmesh = (NavMeshGraph)this.FindGraphOfType(typeof(NavMeshGraph));
			this.gridGraph = (GridGraph)this.FindGraphOfType(typeof(GridGraph));
			this.layerGridGraph = (LayerGridGraph)this.FindGraphOfType(typeof(LayerGridGraph));
			this.pointGraph = (PointGraph)this.FindGraphOfType(typeof(PointGraph));
			this.recastGraph = (RecastGraph)this.FindGraphOfType(typeof(RecastGraph));
		}

		// Token: 0x0600218C RID: 8588 RVA: 0x0018ACA8 File Offset: 0x00188EA8
		public void LoadFromCache()
		{
			PathProcessor.GraphUpdateLock graphUpdateLock = this.AssertSafe(false);
			if (this.file_cachedStartup != null)
			{
				byte[] bytes = this.file_cachedStartup.bytes;
				this.DeserializeGraphs(bytes);
				GraphModifier.TriggerEvent(GraphModifier.EventType.PostCacheLoad);
			}
			else
			{
				Debug.LogError("Can't load from cache since the cache is empty");
			}
			graphUpdateLock.Release();
		}

		// Token: 0x0600218D RID: 8589 RVA: 0x0018ACF8 File Offset: 0x00188EF8
		public byte[] SerializeGraphs()
		{
			return this.SerializeGraphs(SerializeSettings.Settings);
		}

		// Token: 0x0600218E RID: 8590 RVA: 0x0018AD08 File Offset: 0x00188F08
		public byte[] SerializeGraphs(SerializeSettings settings)
		{
			uint num;
			return this.SerializeGraphs(settings, out num);
		}

		// Token: 0x0600218F RID: 8591 RVA: 0x0018AD20 File Offset: 0x00188F20
		public byte[] SerializeGraphs(SerializeSettings settings, out uint checksum)
		{
			PathProcessor.GraphUpdateLock graphUpdateLock = this.AssertSafe(false);
			AstarSerializer astarSerializer = new AstarSerializer(this, settings);
			astarSerializer.OpenSerialize();
			astarSerializer.SerializeGraphs(this.graphs);
			astarSerializer.SerializeExtraInfo();
			byte[] result = astarSerializer.CloseSerialize();
			checksum = astarSerializer.GetChecksum();
			graphUpdateLock.Release();
			return result;
		}

		// Token: 0x06002190 RID: 8592 RVA: 0x0018AD6A File Offset: 0x00188F6A
		public void DeserializeGraphs()
		{
			if (this.data != null)
			{
				this.DeserializeGraphs(this.data);
			}
		}

		// Token: 0x06002191 RID: 8593 RVA: 0x0018AD80 File Offset: 0x00188F80
		private void ClearGraphs()
		{
			if (this.graphs == null)
			{
				return;
			}
			for (int i = 0; i < this.graphs.Length; i++)
			{
				if (this.graphs[i] != null)
				{
					((IGraphInternals)this.graphs[i]).OnDestroy();
					this.graphs[i].active = null;
				}
			}
			this.graphs = null;
			this.UpdateShortcuts();
		}

		// Token: 0x06002192 RID: 8594 RVA: 0x0018ADDB File Offset: 0x00188FDB
		public void OnDestroy()
		{
			this.ClearGraphs();
		}

		// Token: 0x06002193 RID: 8595 RVA: 0x0018ADE4 File Offset: 0x00188FE4
		public void DeserializeGraphs(byte[] bytes)
		{
			PathProcessor.GraphUpdateLock graphUpdateLock = this.AssertSafe(false);
			this.ClearGraphs();
			this.DeserializeGraphsAdditive(bytes);
			graphUpdateLock.Release();
		}

		// Token: 0x06002194 RID: 8596 RVA: 0x0018AE10 File Offset: 0x00189010
		public void DeserializeGraphsAdditive(byte[] bytes)
		{
			PathProcessor.GraphUpdateLock graphUpdateLock = this.AssertSafe(false);
			try
			{
				if (bytes == null)
				{
					throw new ArgumentNullException("bytes");
				}
				AstarSerializer astarSerializer = new AstarSerializer(this);
				if (astarSerializer.OpenDeserialize(bytes))
				{
					this.DeserializeGraphsPartAdditive(astarSerializer);
					astarSerializer.CloseDeserialize();
				}
				else
				{
					Debug.Log("Invalid data file (cannot read zip).\nThe data is either corrupt or it was saved using a 3.0.x or earlier version of the system");
				}
				AstarData.active.VerifyIntegrity();
			}
			catch (Exception arg)
			{
				Debug.LogError("Caught exception while deserializing data.\n" + arg);
				this.graphs = new NavGraph[0];
			}
			this.UpdateShortcuts();
			graphUpdateLock.Release();
		}

		// Token: 0x06002195 RID: 8597 RVA: 0x0018AEA8 File Offset: 0x001890A8
		private void DeserializeGraphsPartAdditive(AstarSerializer sr)
		{
			if (this.graphs == null)
			{
				this.graphs = new NavGraph[0];
			}
			List<NavGraph> list = new List<NavGraph>(this.graphs);
			sr.SetGraphIndexOffset(list.Count);
			list.AddRange(sr.DeserializeGraphs());
			this.graphs = list.ToArray();
			sr.DeserializeEditorSettingsCompatibility();
			sr.DeserializeExtraInfo();
			int i;
			int l;
			for (i = 0; i < this.graphs.Length; i = l + 1)
			{
				if (this.graphs[i] != null)
				{
					this.graphs[i].GetNodes(delegate(GraphNode node)
					{
						node.GraphIndex = (uint)i;
					});
				}
				l = i;
			}
			for (int j = 0; j < this.graphs.Length; j++)
			{
				for (int k = j + 1; k < this.graphs.Length; k++)
				{
					if (this.graphs[j] != null && this.graphs[k] != null && this.graphs[j].guid == this.graphs[k].guid)
					{
						Debug.LogWarning("Guid Conflict when importing graphs additively. Imported graph will get a new Guid.\nThis message is (relatively) harmless.");
						this.graphs[j].guid = Pathfinding.Util.Guid.NewGuid();
						break;
					}
				}
			}
			sr.PostDeserialization();
		}

		// Token: 0x06002196 RID: 8598 RVA: 0x0018AFE8 File Offset: 0x001891E8
		public void FindGraphTypes()
		{
			Type[] types = WindowsStoreCompatibility.GetTypeInfo(typeof(AstarPath)).Assembly.GetTypes();
			List<Type> list = new List<Type>();
			foreach (Type type in types)
			{
				Type baseType = type.BaseType;
				while (baseType != null)
				{
					if (object.Equals(baseType, typeof(NavGraph)))
					{
						list.Add(type);
						break;
					}
					baseType = baseType.BaseType;
				}
			}
			this.graphTypes = list.ToArray();
		}

		// Token: 0x06002197 RID: 8599 RVA: 0x0018B06C File Offset: 0x0018926C
		[Obsolete("If really necessary. Use System.Type.GetType instead.")]
		public Type GetGraphType(string type)
		{
			for (int i = 0; i < this.graphTypes.Length; i++)
			{
				if (this.graphTypes[i].Name == type)
				{
					return this.graphTypes[i];
				}
			}
			return null;
		}

		// Token: 0x06002198 RID: 8600 RVA: 0x0018B0AC File Offset: 0x001892AC
		[Obsolete("Use CreateGraph(System.Type) instead")]
		public NavGraph CreateGraph(string type)
		{
			Debug.Log("Creating Graph of type '" + type + "'");
			for (int i = 0; i < this.graphTypes.Length; i++)
			{
				if (this.graphTypes[i].Name == type)
				{
					return this.CreateGraph(this.graphTypes[i]);
				}
			}
			Debug.LogError("Graph type (" + type + ") wasn't found");
			return null;
		}

		// Token: 0x06002199 RID: 8601 RVA: 0x0018B11B File Offset: 0x0018931B
		internal NavGraph CreateGraph(Type type)
		{
			NavGraph navGraph = Activator.CreateInstance(type) as NavGraph;
			navGraph.active = AstarData.active;
			return navGraph;
		}

		// Token: 0x0600219A RID: 8602 RVA: 0x0018B134 File Offset: 0x00189334
		[Obsolete("Use AddGraph(System.Type) instead")]
		public NavGraph AddGraph(string type)
		{
			NavGraph navGraph = null;
			for (int i = 0; i < this.graphTypes.Length; i++)
			{
				if (this.graphTypes[i].Name == type)
				{
					navGraph = this.CreateGraph(this.graphTypes[i]);
				}
			}
			if (navGraph == null)
			{
				Debug.LogError("No NavGraph of type '" + type + "' could be found");
				return null;
			}
			this.AddGraph(navGraph);
			return navGraph;
		}

		// Token: 0x0600219B RID: 8603 RVA: 0x0018B19C File Offset: 0x0018939C
		public NavGraph AddGraph(Type type)
		{
			NavGraph navGraph = null;
			for (int i = 0; i < this.graphTypes.Length; i++)
			{
				if (object.Equals(this.graphTypes[i], type))
				{
					navGraph = this.CreateGraph(this.graphTypes[i]);
				}
			}
			if (navGraph == null)
			{
				Debug.LogError(string.Concat(new object[]
				{
					"No NavGraph of type '",
					type,
					"' could be found, ",
					this.graphTypes.Length,
					" graph types are avaliable"
				}));
				return null;
			}
			this.AddGraph(navGraph);
			return navGraph;
		}

		// Token: 0x0600219C RID: 8604 RVA: 0x0018B228 File Offset: 0x00189428
		private void AddGraph(NavGraph graph)
		{
			PathProcessor.GraphUpdateLock graphUpdateLock = this.AssertSafe(true);
			bool flag = false;
			for (int i = 0; i < this.graphs.Length; i++)
			{
				if (this.graphs[i] == null)
				{
					this.graphs[i] = graph;
					graph.graphIndex = (uint)i;
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				if (this.graphs != null && (long)this.graphs.Length >= 255L)
				{
					throw new Exception("Graph Count Limit Reached. You cannot have more than " + 255u + " graphs.");
				}
				this.graphs = new List<NavGraph>(this.graphs ?? new NavGraph[0])
				{
					graph
				}.ToArray();
				graph.graphIndex = (uint)(this.graphs.Length - 1);
			}
			this.UpdateShortcuts();
			graph.active = AstarData.active;
			graphUpdateLock.Release();
		}

		// Token: 0x0600219D RID: 8605 RVA: 0x0018B2FC File Offset: 0x001894FC
		public bool RemoveGraph(NavGraph graph)
		{
			PathProcessor.GraphUpdateLock graphUpdateLock = this.AssertSafe(false);
			((IGraphInternals)graph).OnDestroy();
			graph.active = null;
			int num = Array.IndexOf<NavGraph>(this.graphs, graph);
			if (num != -1)
			{
				this.graphs[num] = null;
			}
			this.UpdateShortcuts();
			graphUpdateLock.Release();
			return num != -1;
		}

		// Token: 0x0600219E RID: 8606 RVA: 0x0018B34C File Offset: 0x0018954C
		public static NavGraph GetGraph(GraphNode node)
		{
			if (node == null)
			{
				return null;
			}
			AstarPath active = AstarPath.active;
			if (active == null)
			{
				return null;
			}
			AstarData data = active.data;
			if (data == null || data.graphs == null)
			{
				return null;
			}
			uint graphIndex = node.GraphIndex;
			if ((ulong)graphIndex >= (ulong)((long)data.graphs.Length))
			{
				return null;
			}
			return data.graphs[(int)graphIndex];
		}

		// Token: 0x0600219F RID: 8607 RVA: 0x0018B3A4 File Offset: 0x001895A4
		public NavGraph FindGraph(Func<NavGraph, bool> predicate)
		{
			if (this.graphs != null)
			{
				for (int i = 0; i < this.graphs.Length; i++)
				{
					if (this.graphs[i] != null && predicate(this.graphs[i]))
					{
						return this.graphs[i];
					}
				}
			}
			return null;
		}

		// Token: 0x060021A0 RID: 8608 RVA: 0x0018B3F0 File Offset: 0x001895F0
		public NavGraph FindGraphOfType(Type type)
		{
			return this.FindGraph((NavGraph graph) => object.Equals(graph.GetType(), type));
		}

		// Token: 0x060021A1 RID: 8609 RVA: 0x0018B41C File Offset: 0x0018961C
		public NavGraph FindGraphWhichInheritsFrom(Type type)
		{
			return this.FindGraph((NavGraph graph) => WindowsStoreCompatibility.GetTypeInfo(type).IsAssignableFrom(WindowsStoreCompatibility.GetTypeInfo(graph.GetType())));
		}

		// Token: 0x060021A2 RID: 8610 RVA: 0x0018B448 File Offset: 0x00189648
		public IEnumerable FindGraphsOfType(Type type)
		{
			if (this.graphs == null)
			{
				yield break;
			}
			int num;
			for (int i = 0; i < this.graphs.Length; i = num + 1)
			{
				if (this.graphs[i] != null && object.Equals(this.graphs[i].GetType(), type))
				{
					yield return this.graphs[i];
				}
				num = i;
			}
			yield break;
		}

		// Token: 0x060021A3 RID: 8611 RVA: 0x0018B45F File Offset: 0x0018965F
		public IEnumerable GetUpdateableGraphs()
		{
			if (this.graphs == null)
			{
				yield break;
			}
			int num;
			for (int i = 0; i < this.graphs.Length; i = num + 1)
			{
				if (this.graphs[i] is IUpdatableGraph)
				{
					yield return this.graphs[i];
				}
				num = i;
			}
			yield break;
		}

		// Token: 0x060021A4 RID: 8612 RVA: 0x0018B46F File Offset: 0x0018966F
		[Obsolete("Obsolete because it is not used by the package internally and the use cases are few. Iterate through the graphs array instead.")]
		public IEnumerable GetRaycastableGraphs()
		{
			if (this.graphs == null)
			{
				yield break;
			}
			int num;
			for (int i = 0; i < this.graphs.Length; i = num + 1)
			{
				if (this.graphs[i] is IRaycastableGraph)
				{
					yield return this.graphs[i];
				}
				num = i;
			}
			yield break;
		}

		// Token: 0x060021A5 RID: 8613 RVA: 0x0018B480 File Offset: 0x00189680
		public int GetGraphIndex(NavGraph graph)
		{
			if (graph == null)
			{
				throw new ArgumentNullException("graph");
			}
			int num = -1;
			if (this.graphs != null)
			{
				num = Array.IndexOf<NavGraph>(this.graphs, graph);
				if (num == -1)
				{
					Debug.LogError("Graph doesn't exist");
				}
			}
			return num;
		}

		// Token: 0x04003EAA RID: 16042
		[NonSerialized]
		public NavGraph[] graphs = new NavGraph[0];

		// Token: 0x04003EAB RID: 16043
		[SerializeField]
		private string dataString;

		// Token: 0x04003EAC RID: 16044
		[SerializeField]
		[FormerlySerializedAs("data")]
		private byte[] upgradeData;

		// Token: 0x04003EAD RID: 16045
		public TextAsset file_cachedStartup;

		// Token: 0x04003EAE RID: 16046
		public byte[] data_cachedStartup;

		// Token: 0x04003EAF RID: 16047
		[SerializeField]
		public bool cacheStartup;

		// Token: 0x04003EB0 RID: 16048
		private List<bool> graphStructureLocked = new List<bool>();
	}
}
