using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Pathfinding;
using Pathfinding.Util;
using UnityEngine;
using UnityEngine.Serialization;

// Token: 0x02000002 RID: 2
[ExecuteInEditMode]
[AddComponentMenu("Pathfinding/Pathfinder")]
[HelpURL("http://arongranberg.com/astar/docs/class_astar_path.php")]
public class AstarPath : VersionedMonoBehaviour
{
	// Token: 0x17000001 RID: 1
	// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
	[Obsolete]
	public Type[] graphTypes
	{
		get
		{
			return this.data.graphTypes;
		}
	}

	// Token: 0x17000002 RID: 2
	// (get) Token: 0x06000002 RID: 2 RVA: 0x0000205D File Offset: 0x0000025D
	[Obsolete("The 'astarData' field has been renamed to 'data'")]
	public AstarData astarData
	{
		get
		{
			return this.data;
		}
	}

	// Token: 0x17000003 RID: 3
	// (get) Token: 0x06000003 RID: 3 RVA: 0x00002065 File Offset: 0x00000265
	public NavGraph[] graphs
	{
		get
		{
			if (this.data == null)
			{
				this.data = new AstarData();
			}
			return this.data.graphs;
		}
	}

	// Token: 0x17000004 RID: 4
	// (get) Token: 0x06000004 RID: 4 RVA: 0x00002085 File Offset: 0x00000285
	public float maxNearestNodeDistanceSqr
	{
		get
		{
			return this.maxNearestNodeDistance * this.maxNearestNodeDistance;
		}
	}

	// Token: 0x17000005 RID: 5
	// (get) Token: 0x06000005 RID: 5 RVA: 0x00002094 File Offset: 0x00000294
	// (set) Token: 0x06000006 RID: 6 RVA: 0x0000209C File Offset: 0x0000029C
	[Obsolete("This field has been renamed to 'batchGraphUpdates'")]
	public bool limitGraphUpdates
	{
		get
		{
			return this.batchGraphUpdates;
		}
		set
		{
			this.batchGraphUpdates = value;
		}
	}

	// Token: 0x17000006 RID: 6
	// (get) Token: 0x06000007 RID: 7 RVA: 0x000020A5 File Offset: 0x000002A5
	// (set) Token: 0x06000008 RID: 8 RVA: 0x000020AD File Offset: 0x000002AD
	[Obsolete("This field has been renamed to 'graphUpdateBatchingInterval'")]
	public float maxGraphUpdateFreq
	{
		get
		{
			return this.graphUpdateBatchingInterval;
		}
		set
		{
			this.graphUpdateBatchingInterval = value;
		}
	}

	// Token: 0x17000007 RID: 7
	// (get) Token: 0x06000009 RID: 9 RVA: 0x000020B6 File Offset: 0x000002B6
	// (set) Token: 0x0600000A RID: 10 RVA: 0x000020BE File Offset: 0x000002BE
	public float lastScanTime { get; private set; }

	// Token: 0x17000008 RID: 8
	// (get) Token: 0x0600000B RID: 11 RVA: 0x000020C7 File Offset: 0x000002C7
	// (set) Token: 0x0600000C RID: 12 RVA: 0x000020CF File Offset: 0x000002CF
	public bool isScanning
	{
		get
		{
			return this.isScanningBacking;
		}
		private set
		{
			this.isScanningBacking = value;
		}
	}

	// Token: 0x17000009 RID: 9
	// (get) Token: 0x0600000D RID: 13 RVA: 0x000020D8 File Offset: 0x000002D8
	public int NumParallelThreads
	{
		get
		{
			return this.pathProcessor.NumThreads;
		}
	}

	// Token: 0x1700000A RID: 10
	// (get) Token: 0x0600000E RID: 14 RVA: 0x000020E5 File Offset: 0x000002E5
	public bool IsUsingMultithreading
	{
		get
		{
			return this.pathProcessor.IsUsingMultithreading;
		}
	}

	// Token: 0x1700000B RID: 11
	// (get) Token: 0x0600000F RID: 15 RVA: 0x000020F2 File Offset: 0x000002F2
	[Obsolete("Fixed grammar, use IsAnyGraphUpdateQueued instead")]
	public bool IsAnyGraphUpdatesQueued
	{
		get
		{
			return this.IsAnyGraphUpdateQueued;
		}
	}

	// Token: 0x1700000C RID: 12
	// (get) Token: 0x06000010 RID: 16 RVA: 0x000020FA File Offset: 0x000002FA
	public bool IsAnyGraphUpdateQueued
	{
		get
		{
			return this.graphUpdates.IsAnyGraphUpdateQueued;
		}
	}

	// Token: 0x1700000D RID: 13
	// (get) Token: 0x06000011 RID: 17 RVA: 0x00002107 File Offset: 0x00000307
	public bool IsAnyGraphUpdateInProgress
	{
		get
		{
			return this.graphUpdates.IsAnyGraphUpdateInProgress;
		}
	}

	// Token: 0x1700000E RID: 14
	// (get) Token: 0x06000012 RID: 18 RVA: 0x00002114 File Offset: 0x00000314
	public bool IsAnyWorkItemInProgress
	{
		get
		{
			return this.workItems.workItemsInProgress;
		}
	}

	// Token: 0x1700000F RID: 15
	// (get) Token: 0x06000013 RID: 19 RVA: 0x00002121 File Offset: 0x00000321
	internal bool IsInsideWorkItem
	{
		get
		{
			return this.workItems.workItemsInProgressRightNow;
		}
	}

	// Token: 0x06000014 RID: 20 RVA: 0x00002130 File Offset: 0x00000330
	private AstarPath()
	{
		this.pathReturnQueue = new PathReturnQueue(this);
		this.pathProcessor = new PathProcessor(this, this.pathReturnQueue, 1, false);
		this.workItems = new WorkItemProcessor(this);
		this.graphUpdates = new GraphUpdateProcessor(this);
		this.graphUpdates.OnGraphsUpdated += delegate()
		{
			if (AstarPath.OnGraphsUpdated != null)
			{
				AstarPath.OnGraphsUpdated(this);
			}
		};
	}

	// Token: 0x06000015 RID: 21 RVA: 0x00002234 File Offset: 0x00000434
	public string[] GetTagNames()
	{
		if (this.tagNames == null || this.tagNames.Length != 32)
		{
			this.tagNames = new string[32];
			for (int i = 0; i < this.tagNames.Length; i++)
			{
				this.tagNames[i] = string.Concat(i);
			}
			this.tagNames[0] = "Basic Ground";
		}
		return this.tagNames;
	}

	// Token: 0x06000016 RID: 22 RVA: 0x0000229C File Offset: 0x0000049C
	public static void FindAstarPath()
	{
		if (Application.isPlaying)
		{
			return;
		}
		if (AstarPath.active == null)
		{
			AstarPath.active = UnityEngine.Object.FindObjectOfType<AstarPath>();
		}
		if (AstarPath.active != null && (AstarPath.active.data.graphs == null || AstarPath.active.data.graphs.Length == 0))
		{
			AstarPath.active.data.DeserializeGraphs();
		}
	}

	// Token: 0x06000017 RID: 23 RVA: 0x00002307 File Offset: 0x00000507
	public static string[] FindTagNames()
	{
		AstarPath.FindAstarPath();
		if (!(AstarPath.active != null))
		{
			return new string[]
			{
				"There is no AstarPath component in the scene"
			};
		}
		return AstarPath.active.GetTagNames();
	}

	// Token: 0x06000018 RID: 24 RVA: 0x00002334 File Offset: 0x00000534
	internal ushort GetNextPathID()
	{
		if (this.nextFreePathID == 0)
		{
			this.nextFreePathID += 1;
			if (AstarPath.On65KOverflow != null)
			{
				Action on65KOverflow = AstarPath.On65KOverflow;
				AstarPath.On65KOverflow = null;
				on65KOverflow();
			}
		}
		ushort num = this.nextFreePathID;
		this.nextFreePathID = num + 1;
		return num;
	}

	// Token: 0x06000019 RID: 25 RVA: 0x00002384 File Offset: 0x00000584
	private void RecalculateDebugLimits()
	{
		this.debugFloor = float.PositiveInfinity;
		this.debugRoof = float.NegativeInfinity;
		bool ignoreSearchTree = !this.showSearchTree || this.debugPathData == null;
		Action<GraphNode> <>9__0;
		for (int i = 0; i < this.graphs.Length; i++)
		{
			if (this.graphs[i] != null && this.graphs[i].drawGizmos)
			{
				NavGraph navGraph = this.graphs[i];
				Action<GraphNode> action;
				if ((action = <>9__0) == null)
				{
					action = (<>9__0 = delegate(GraphNode node)
					{
						if (ignoreSearchTree || GraphGizmoHelper.InSearchTree(node, this.debugPathData, this.debugPathID))
						{
							if (this.debugMode == GraphDebugMode.Penalty)
							{
								this.debugFloor = Mathf.Min(this.debugFloor, node.Penalty);
								this.debugRoof = Mathf.Max(this.debugRoof, node.Penalty);
								return;
							}
							if (this.debugPathData != null)
							{
								PathNode pathNode = this.debugPathData.GetPathNode(node);
								switch (this.debugMode)
								{
								case GraphDebugMode.G:
									this.debugFloor = Mathf.Min(this.debugFloor, pathNode.G);
									this.debugRoof = Mathf.Max(this.debugRoof, pathNode.G);
									return;
								case GraphDebugMode.H:
									this.debugFloor = Mathf.Min(this.debugFloor, pathNode.H);
									this.debugRoof = Mathf.Max(this.debugRoof, pathNode.H);
									break;
								case GraphDebugMode.F:
									this.debugFloor = Mathf.Min(this.debugFloor, pathNode.F);
									this.debugRoof = Mathf.Max(this.debugRoof, pathNode.F);
									return;
								default:
									return;
								}
							}
						}
					});
				}
				navGraph.GetNodes(action);
			}
		}
		if (float.IsInfinity(this.debugFloor))
		{
			this.debugFloor = 0f;
			this.debugRoof = 1f;
		}
		if (this.debugRoof - this.debugFloor < 1f)
		{
			this.debugRoof += 1f;
		}
	}

	// Token: 0x0600001A RID: 26 RVA: 0x00002470 File Offset: 0x00000670
	private void OnDrawGizmos()
	{
		if (AstarPath.active == null)
		{
			AstarPath.active = this;
		}
		if (AstarPath.active != this || this.graphs == null)
		{
			return;
		}
		if (Event.current.type != EventType.Repaint)
		{
			return;
		}
		if (this.workItems.workItemsInProgress || this.isScanning)
		{
			this.gizmos.DrawExisting();
		}
		else
		{
			if (this.showNavGraphs && !this.manualDebugFloorRoof)
			{
				this.RecalculateDebugLimits();
			}
			for (int i = 0; i < this.graphs.Length; i++)
			{
				if (this.graphs[i] != null && this.graphs[i].drawGizmos)
				{
					this.graphs[i].OnDrawGizmos(this.gizmos, this.showNavGraphs);
				}
			}
			if (this.showNavGraphs)
			{
				this.euclideanEmbedding.OnDrawGizmos();
			}
		}
		this.gizmos.FinalizeDraw();
	}

	// Token: 0x0600001B RID: 27 RVA: 0x00002550 File Offset: 0x00000750
	private void OnGUI()
	{
		if (this.logPathResults == PathLog.InGame && this.inGameDebugPath != "")
		{
			GUI.Label(new Rect(5f, 5f, 400f, 600f), this.inGameDebugPath);
		}
	}

	// Token: 0x0600001C RID: 28 RVA: 0x0000259C File Offset: 0x0000079C
	private void LogPathResults(Path path)
	{
		if (this.logPathResults != PathLog.None && (path.error || this.logPathResults != PathLog.OnlyErrors))
		{
			string message = path.DebugString(this.logPathResults);
			if (this.logPathResults == PathLog.InGame)
			{
				this.inGameDebugPath = message;
				return;
			}
			if (path.error)
			{
				UnityEngine.Debug.LogWarning(message);
				return;
			}
			UnityEngine.Debug.Log(message);
		}
	}

	// Token: 0x0600001D RID: 29 RVA: 0x000025F5 File Offset: 0x000007F5
	private void Update()
	{
		if (!Application.isPlaying)
		{
			return;
		}
		if (!this.isScanning)
		{
			this.PerformBlockingActions(false);
		}
		this.pathProcessor.TickNonMultithreaded();
		this.pathReturnQueue.ReturnPaths(true);
	}

	// Token: 0x0600001E RID: 30 RVA: 0x00002628 File Offset: 0x00000828
	private void PerformBlockingActions(bool force = false)
	{
		if (this.workItemLock.Held && this.pathProcessor.queue.AllReceiversBlocked)
		{
			this.pathReturnQueue.ReturnPaths(false);
			if (this.workItems.ProcessWorkItems(force))
			{
				this.workItemLock.Release();
			}
		}
	}

	// Token: 0x0600001F RID: 31 RVA: 0x00002679 File Offset: 0x00000879
	[Obsolete("This method has been moved. Use the method on the context object that can be sent with work item delegates instead")]
	public void QueueWorkItemFloodFill()
	{
		throw new Exception("This method has been moved. Use the method on the context object that can be sent with work item delegates instead");
	}

	// Token: 0x06000020 RID: 32 RVA: 0x00002679 File Offset: 0x00000879
	[Obsolete("This method has been moved. Use the method on the context object that can be sent with work item delegates instead")]
	public void EnsureValidFloodFill()
	{
		throw new Exception("This method has been moved. Use the method on the context object that can be sent with work item delegates instead");
	}

	// Token: 0x06000021 RID: 33 RVA: 0x00002685 File Offset: 0x00000885
	public void AddWorkItem(Action callback)
	{
		this.AddWorkItem(new AstarWorkItem(callback, null));
	}

	// Token: 0x06000022 RID: 34 RVA: 0x00002694 File Offset: 0x00000894
	public void AddWorkItem(Action<IWorkItemContext> callback)
	{
		this.AddWorkItem(new AstarWorkItem(callback, null));
	}

	// Token: 0x06000023 RID: 35 RVA: 0x000026A3 File Offset: 0x000008A3
	public void AddWorkItem(AstarWorkItem item)
	{
		this.workItems.AddWorkItem(item);
		if (!this.workItemLock.Held)
		{
			this.workItemLock = this.PausePathfindingSoon();
		}
	}

	// Token: 0x06000024 RID: 36 RVA: 0x000026CC File Offset: 0x000008CC
	public void QueueGraphUpdates()
	{
		if (!this.graphUpdatesWorkItemAdded)
		{
			this.graphUpdatesWorkItemAdded = true;
			AstarWorkItem workItem = this.graphUpdates.GetWorkItem();
			this.AddWorkItem(new AstarWorkItem(delegate()
			{
				this.graphUpdatesWorkItemAdded = false;
				this.lastGraphUpdate = Time.realtimeSinceStartup;
				workItem.init();
			}, workItem.update));
		}
	}

	// Token: 0x06000025 RID: 37 RVA: 0x00002728 File Offset: 0x00000928
	private IEnumerator DelayedGraphUpdate()
	{
		this.graphUpdateRoutineRunning = true;
		yield return new WaitForSeconds(this.graphUpdateBatchingInterval - (Time.realtimeSinceStartup - this.lastGraphUpdate));
		this.QueueGraphUpdates();
		this.graphUpdateRoutineRunning = false;
		yield break;
	}

	// Token: 0x06000026 RID: 38 RVA: 0x00002737 File Offset: 0x00000937
	public void UpdateGraphs(Bounds bounds, float delay)
	{
		this.UpdateGraphs(new GraphUpdateObject(bounds), delay);
	}

	// Token: 0x06000027 RID: 39 RVA: 0x00002746 File Offset: 0x00000946
	public void UpdateGraphs(GraphUpdateObject ob, float delay)
	{
		base.StartCoroutine(this.UpdateGraphsInternal(ob, delay));
	}

	// Token: 0x06000028 RID: 40 RVA: 0x00002757 File Offset: 0x00000957
	private IEnumerator UpdateGraphsInternal(GraphUpdateObject ob, float delay)
	{
		yield return new WaitForSeconds(delay);
		this.UpdateGraphs(ob);
		yield break;
	}

	// Token: 0x06000029 RID: 41 RVA: 0x00002774 File Offset: 0x00000974
	public void UpdateGraphs(Bounds bounds)
	{
		this.UpdateGraphs(new GraphUpdateObject(bounds));
	}

	// Token: 0x0600002A RID: 42 RVA: 0x00002784 File Offset: 0x00000984
	public void UpdateGraphs(GraphUpdateObject ob)
	{
		this.graphUpdates.AddToQueue(ob);
		if (this.batchGraphUpdates && Time.realtimeSinceStartup - this.lastGraphUpdate < this.graphUpdateBatchingInterval)
		{
			if (!this.graphUpdateRoutineRunning)
			{
				base.StartCoroutine(this.DelayedGraphUpdate());
				return;
			}
		}
		else
		{
			this.QueueGraphUpdates();
		}
	}

	// Token: 0x0600002B RID: 43 RVA: 0x000027D5 File Offset: 0x000009D5
	public void FlushGraphUpdates()
	{
		if (this.IsAnyGraphUpdateQueued)
		{
			this.QueueGraphUpdates();
			this.FlushWorkItems();
		}
	}

	// Token: 0x0600002C RID: 44 RVA: 0x000027EC File Offset: 0x000009EC
	public void FlushWorkItems()
	{
		PathProcessor.GraphUpdateLock graphUpdateLock = this.PausePathfinding();
		this.PerformBlockingActions(true);
		graphUpdateLock.Release();
	}

	// Token: 0x0600002D RID: 45 RVA: 0x00002810 File Offset: 0x00000A10
	[Obsolete("Use FlushWorkItems() instead")]
	public void FlushWorkItems(bool unblockOnComplete, bool block)
	{
		PathProcessor.GraphUpdateLock graphUpdateLock = this.PausePathfinding();
		this.PerformBlockingActions(block);
		graphUpdateLock.Release();
	}

	// Token: 0x0600002E RID: 46 RVA: 0x00002832 File Offset: 0x00000A32
	[Obsolete("Use FlushWorkItems instead")]
	public void FlushThreadSafeCallbacks()
	{
		this.FlushWorkItems();
	}

	// Token: 0x0600002F RID: 47 RVA: 0x0000283C File Offset: 0x00000A3C
	public static int CalculateThreadCount(ThreadCount count)
	{
		if (count != ThreadCount.AutomaticLowLoad && count != ThreadCount.AutomaticHighLoad)
		{
			return (int)count;
		}
		int num = Mathf.Max(1, SystemInfo.processorCount);
		int num2 = SystemInfo.systemMemorySize;
		if (num2 <= 0)
		{
			UnityEngine.Debug.LogError("Machine reporting that is has <= 0 bytes of RAM. This is definitely not true, assuming 1 GiB");
			num2 = 1024;
		}
		if (num <= 1)
		{
			return 0;
		}
		if (num2 <= 512)
		{
			return 0;
		}
		if (count == ThreadCount.AutomaticHighLoad)
		{
			if (num2 <= 1024)
			{
				num = Math.Min(num, 2);
			}
		}
		else
		{
			num /= 2;
			num = Mathf.Max(1, num);
			if (num2 <= 1024)
			{
				num = Math.Min(num, 2);
			}
			num = Math.Min(num, 6);
		}
		return num;
	}

	// Token: 0x06000030 RID: 48 RVA: 0x000028C8 File Offset: 0x00000AC8
	protected override void Awake()
	{
		base.Awake();
		AstarPath.active = this;
		if (UnityEngine.Object.FindObjectsOfType(typeof(AstarPath)).Length > 1)
		{
			UnityEngine.Debug.LogError("You should NOT have more than one AstarPath component in the scene at any time.\nThis can cause serious errors since the AstarPath component builds around a singleton pattern.");
		}
		base.useGUILayout = false;
		if (!Application.isPlaying)
		{
			return;
		}
		if (AstarPath.OnAwakeSettings != null)
		{
			AstarPath.OnAwakeSettings();
		}
		GraphModifier.FindAllModifiers();
		RelevantGraphSurface.FindAllGraphSurfaces();
		this.InitializePathProcessor();
		this.InitializeProfiler();
		this.ConfigureReferencesInternal();
		this.InitializeAstarData();
		this.FlushWorkItems();
		this.euclideanEmbedding.dirty = true;
		if (this.scanOnStartup && (!this.data.cacheStartup || this.data.file_cachedStartup == null))
		{
			this.Scan(null);
		}
	}

	// Token: 0x06000031 RID: 49 RVA: 0x00002984 File Offset: 0x00000B84
	private void InitializePathProcessor()
	{
		int num = AstarPath.CalculateThreadCount(this.threadCount);
		if (!Application.isPlaying)
		{
			num = 0;
		}
		int processors = Mathf.Max(num, 1);
		bool flag = num > 0;
		this.pathProcessor = new PathProcessor(this, this.pathReturnQueue, processors, flag);
		this.pathProcessor.OnPathPreSearch += delegate(Path path)
		{
			OnPathDelegate onPathPreSearch = AstarPath.OnPathPreSearch;
			if (onPathPreSearch != null)
			{
				onPathPreSearch(path);
			}
		};
		this.pathProcessor.OnPathPostSearch += delegate(Path path)
		{
			this.LogPathResults(path);
			OnPathDelegate onPathPostSearch = AstarPath.OnPathPostSearch;
			if (onPathPostSearch != null)
			{
				onPathPostSearch(path);
			}
		};
		this.pathProcessor.OnQueueUnblocked += delegate()
		{
			if (this.euclideanEmbedding.dirty)
			{
				this.euclideanEmbedding.RecalculateCosts();
			}
		};
		if (flag)
		{
			this.graphUpdates.EnableMultithreading();
		}
	}

	// Token: 0x06000032 RID: 50 RVA: 0x00002A30 File Offset: 0x00000C30
	internal void VerifyIntegrity()
	{
		if (AstarPath.active != this)
		{
			throw new Exception("Singleton pattern broken. Make sure you only have one AstarPath object in the scene");
		}
		if (this.data == null)
		{
			throw new NullReferenceException("data is null... A* not set up correctly?");
		}
		if (this.data.graphs == null)
		{
			this.data.graphs = new NavGraph[0];
			this.data.UpdateShortcuts();
		}
	}

	// Token: 0x06000033 RID: 51 RVA: 0x00002A91 File Offset: 0x00000C91
	public void ConfigureReferencesInternal()
	{
		AstarPath.active = this;
		this.data = (this.data ?? new AstarData());
		this.colorSettings = (this.colorSettings ?? new AstarColor());
		this.colorSettings.OnEnable();
	}

	// Token: 0x06000034 RID: 52 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void InitializeProfiler()
	{
	}

	// Token: 0x06000035 RID: 53 RVA: 0x00002AD0 File Offset: 0x00000CD0
	private void InitializeAstarData()
	{
		this.data.FindGraphTypes();
		this.data.Awake();
		this.data.UpdateShortcuts();
	}

	// Token: 0x06000036 RID: 54 RVA: 0x00002AF3 File Offset: 0x00000CF3
	private void OnDisable()
	{
		this.gizmos.ClearCache();
	}

	// Token: 0x06000037 RID: 55 RVA: 0x00002B00 File Offset: 0x00000D00
	private void OnDestroy()
	{
		if (!Application.isPlaying)
		{
			return;
		}
		if (this.logPathResults == PathLog.Heavy)
		{
			UnityEngine.Debug.Log("+++ AstarPath Component Destroyed - Cleaning Up Pathfinding Data +++");
		}
		if (AstarPath.active != this)
		{
			return;
		}
		this.PausePathfinding();
		this.euclideanEmbedding.dirty = false;
		this.FlushWorkItems();
		this.pathProcessor.queue.TerminateReceivers();
		if (this.logPathResults == PathLog.Heavy)
		{
			UnityEngine.Debug.Log("Processing Possible Work Items");
		}
		this.graphUpdates.DisableMultithreading();
		this.pathProcessor.JoinThreads();
		if (this.logPathResults == PathLog.Heavy)
		{
			UnityEngine.Debug.Log("Returning Paths");
		}
		this.pathReturnQueue.ReturnPaths(false);
		if (this.logPathResults == PathLog.Heavy)
		{
			UnityEngine.Debug.Log("Destroying Graphs");
		}
		this.data.OnDestroy();
		if (this.logPathResults == PathLog.Heavy)
		{
			UnityEngine.Debug.Log("Cleaning up variables");
		}
		AstarPath.OnAwakeSettings = null;
		AstarPath.OnGraphPreScan = null;
		AstarPath.OnGraphPostScan = null;
		AstarPath.OnPathPreSearch = null;
		AstarPath.OnPathPostSearch = null;
		AstarPath.OnPreScan = null;
		AstarPath.OnPostScan = null;
		AstarPath.OnLatePostScan = null;
		AstarPath.On65KOverflow = null;
		AstarPath.OnGraphsUpdated = null;
		AstarPath.active = null;
	}

	// Token: 0x06000038 RID: 56 RVA: 0x00002C1A File Offset: 0x00000E1A
	public void FloodFill(GraphNode seed)
	{
		this.graphUpdates.FloodFill(seed);
	}

	// Token: 0x06000039 RID: 57 RVA: 0x00002C28 File Offset: 0x00000E28
	public void FloodFill(GraphNode seed, uint area)
	{
		this.graphUpdates.FloodFill(seed, area);
	}

	// Token: 0x0600003A RID: 58 RVA: 0x00002C37 File Offset: 0x00000E37
	[ContextMenu("Flood Fill Graphs")]
	public void FloodFill()
	{
		this.graphUpdates.FloodFill();
		this.workItems.OnFloodFill();
	}

	// Token: 0x0600003B RID: 59 RVA: 0x00002C4F File Offset: 0x00000E4F
	internal int GetNewNodeIndex()
	{
		return this.pathProcessor.GetNewNodeIndex();
	}

	// Token: 0x0600003C RID: 60 RVA: 0x00002C5C File Offset: 0x00000E5C
	internal void InitializeNode(GraphNode node)
	{
		this.pathProcessor.InitializeNode(node);
	}

	// Token: 0x0600003D RID: 61 RVA: 0x00002C6A File Offset: 0x00000E6A
	internal void DestroyNode(GraphNode node)
	{
		this.pathProcessor.DestroyNode(node);
	}

	// Token: 0x0600003E RID: 62 RVA: 0x00002ACE File Offset: 0x00000CCE
	[Obsolete("Use PausePathfinding instead. Make sure to call Release on the returned lock.", true)]
	public void BlockUntilPathQueueBlocked()
	{
	}

	// Token: 0x0600003F RID: 63 RVA: 0x00002C78 File Offset: 0x00000E78
	public PathProcessor.GraphUpdateLock PausePathfinding()
	{
		return this.pathProcessor.PausePathfinding(true);
	}

	// Token: 0x06000040 RID: 64 RVA: 0x00002C86 File Offset: 0x00000E86
	private PathProcessor.GraphUpdateLock PausePathfindingSoon()
	{
		return this.pathProcessor.PausePathfinding(false);
	}

	// Token: 0x06000041 RID: 65 RVA: 0x00002C94 File Offset: 0x00000E94
	public void Scan(NavGraph graphToScan)
	{
		if (graphToScan == null)
		{
			throw new ArgumentNullException();
		}
		this.Scan(new NavGraph[]
		{
			graphToScan
		});
	}

	// Token: 0x06000042 RID: 66 RVA: 0x00002CB0 File Offset: 0x00000EB0
	public void Scan(NavGraph[] graphsToScan = null)
	{
		Progress progress = default(Progress);
		foreach (Progress progress2 in this.ScanAsync(graphsToScan))
		{
			progress.description != progress2.description;
		}
	}

	// Token: 0x06000043 RID: 67 RVA: 0x00002D14 File Offset: 0x00000F14
	[Obsolete("ScanLoop is now named ScanAsync and is an IEnumerable<Progress>. Use foreach to iterate over the progress insead")]
	public void ScanLoop(OnScanStatus statusCallback)
	{
		foreach (Progress progress in this.ScanAsync(null))
		{
			statusCallback(progress);
		}
	}

	// Token: 0x06000044 RID: 68 RVA: 0x00002D64 File Offset: 0x00000F64
	public IEnumerable<Progress> ScanAsync(NavGraph graphToScan)
	{
		if (graphToScan == null)
		{
			throw new ArgumentNullException();
		}
		return this.ScanAsync(new NavGraph[]
		{
			graphToScan
		});
	}

	// Token: 0x06000045 RID: 69 RVA: 0x00002D7F File Offset: 0x00000F7F
	public IEnumerable<Progress> ScanAsync(NavGraph[] graphsToScan = null)
	{
		if (graphsToScan == null)
		{
			graphsToScan = this.graphs;
		}
		if (graphsToScan == null)
		{
			yield break;
		}
		if (this.isScanning)
		{
			throw new InvalidOperationException("Another async scan is already running");
		}
		this.isScanning = true;
		this.VerifyIntegrity();
		PathProcessor.GraphUpdateLock graphUpdateLock = this.PausePathfinding();
		this.pathReturnQueue.ReturnPaths(false);
		if (!Application.isPlaying)
		{
			this.data.FindGraphTypes();
			GraphModifier.FindAllModifiers();
		}
		yield return new Progress(0.05f, "Pre processing graphs");
		if (AstarPath.OnPreScan != null)
		{
			AstarPath.OnPreScan(this);
		}
		GraphModifier.TriggerEvent(GraphModifier.EventType.PreScan);
		this.data.LockGraphStructure(false);
		Stopwatch watch = Stopwatch.StartNew();
		for (int j = 0; j < graphsToScan.Length; j++)
		{
			if (graphsToScan[j] != null)
			{
				((IGraphInternals)graphsToScan[j]).DestroyAllNodes();
			}
		}
		int num;
		for (int i = 0; i < graphsToScan.Length; i = num + 1)
		{
			if (graphsToScan[i] != null)
			{
				float minp = Mathf.Lerp(0.1f, 0.8f, (float)i / (float)graphsToScan.Length);
				float maxp = Mathf.Lerp(0.1f, 0.8f, ((float)i + 0.95f) / (float)graphsToScan.Length);
				string progressDescriptionPrefix = string.Concat(new object[]
				{
					"Scanning graph ",
					i + 1,
					" of ",
					graphsToScan.Length,
					" - "
				});
				IEnumerator<Progress> coroutine = this.ScanGraph(graphsToScan[i]).GetEnumerator();
				for (;;)
				{
					try
					{
						if (!coroutine.MoveNext())
						{
							break;
						}
					}
					catch
					{
						this.isScanning = false;
						this.data.UnlockGraphStructure();
						graphUpdateLock.Release();
						throw;
					}
					Progress progress = coroutine.Current;
					yield return progress.MapTo(minp, maxp, progressDescriptionPrefix);
				}
				progressDescriptionPrefix = null;
				coroutine = null;
			}
			num = i;
		}
		this.data.UnlockGraphStructure();
		yield return new Progress(0.8f, "Post processing graphs");
		if (AstarPath.OnPostScan != null)
		{
			AstarPath.OnPostScan(this);
		}
		GraphModifier.TriggerEvent(GraphModifier.EventType.PostScan);
		this.FlushWorkItems();
		yield return new Progress(0.9f, "Computing areas");
		this.FloodFill();
		yield return new Progress(0.95f, "Late post processing");
		this.isScanning = false;
		if (AstarPath.OnLatePostScan != null)
		{
			AstarPath.OnLatePostScan(this);
		}
		GraphModifier.TriggerEvent(GraphModifier.EventType.LatePostScan);
		this.euclideanEmbedding.dirty = true;
		this.euclideanEmbedding.RecalculatePivots();
		this.FlushWorkItems();
		graphUpdateLock.Release();
		watch.Stop();
		this.lastScanTime = (float)watch.Elapsed.TotalSeconds;
		GC.Collect();
		if (this.logPathResults != PathLog.None && this.logPathResults != PathLog.OnlyErrors)
		{
			UnityEngine.Debug.Log("Scanning - Process took " + (this.lastScanTime * 1000f).ToString("0") + " ms to complete");
		}
		yield break;
	}

	// Token: 0x06000046 RID: 70 RVA: 0x00002D96 File Offset: 0x00000F96
	private IEnumerable<Progress> ScanGraph(NavGraph graph)
	{
		if (AstarPath.OnGraphPreScan != null)
		{
			yield return new Progress(0f, "Pre processing");
			AstarPath.OnGraphPreScan(graph);
		}
		yield return new Progress(0f, "");
		foreach (Progress progress in ((IGraphInternals)graph).ScanInternal())
		{
			yield return progress.MapTo(0f, 0.95f, null);
		}
		IEnumerator<Progress> enumerator = null;
		yield return new Progress(0.95f, "Assigning graph indices");
		graph.GetNodes(delegate(GraphNode node)
		{
			node.GraphIndex = graph.graphIndex;
		});
		if (AstarPath.OnGraphPostScan != null)
		{
			yield return new Progress(0.99f, "Post processing");
			AstarPath.OnGraphPostScan(graph);
		}
		yield break;
		yield break;
	}

	// Token: 0x06000047 RID: 71 RVA: 0x00002DA6 File Offset: 0x00000FA6
	[Obsolete("This method has been renamed to BlockUntilCalculated")]
	public static void WaitForPath(Path path)
	{
		AstarPath.BlockUntilCalculated(path);
	}

	// Token: 0x06000048 RID: 72 RVA: 0x00002DB0 File Offset: 0x00000FB0
	public static void BlockUntilCalculated(Path path)
	{
		if (AstarPath.active == null)
		{
			throw new Exception("Pathfinding is not correctly initialized in this scene (yet?). AstarPath.active is null.\nDo not call this function in Awake");
		}
		if (path == null)
		{
			throw new ArgumentNullException("Path must not be null");
		}
		if (AstarPath.active.pathProcessor.queue.IsTerminating)
		{
			return;
		}
		if (path.PipelineState == PathState.Created)
		{
			throw new Exception("The specified path has not been started yet.");
		}
		AstarPath.waitForPathDepth++;
		if (AstarPath.waitForPathDepth == 5)
		{
			UnityEngine.Debug.LogError("You are calling the BlockUntilCalculated function recursively (maybe from a path callback). Please don't do this.");
		}
		if (path.PipelineState < PathState.ReturnQueue)
		{
			if (AstarPath.active.IsUsingMultithreading)
			{
				while (path.PipelineState < PathState.ReturnQueue)
				{
					if (AstarPath.active.pathProcessor.queue.IsTerminating)
					{
						AstarPath.waitForPathDepth--;
						throw new Exception("Pathfinding Threads seem to have crashed.");
					}
					Thread.Sleep(1);
					AstarPath.active.PerformBlockingActions(true);
				}
			}
			else
			{
				while (path.PipelineState < PathState.ReturnQueue)
				{
					if (AstarPath.active.pathProcessor.queue.IsEmpty && path.PipelineState != PathState.Processing)
					{
						AstarPath.waitForPathDepth--;
						throw new Exception("Critical error. Path Queue is empty but the path state is '" + path.PipelineState + "'");
					}
					AstarPath.active.pathProcessor.TickNonMultithreaded();
					AstarPath.active.PerformBlockingActions(true);
				}
			}
		}
		AstarPath.active.pathReturnQueue.ReturnPaths(false);
		AstarPath.waitForPathDepth--;
	}

	// Token: 0x06000049 RID: 73 RVA: 0x00002F1B File Offset: 0x0000111B
	[Obsolete("The threadSafe parameter has been deprecated")]
	public static void RegisterSafeUpdate(Action callback, bool threadSafe)
	{
		AstarPath.RegisterSafeUpdate(callback);
	}

	// Token: 0x0600004A RID: 74 RVA: 0x00002F23 File Offset: 0x00001123
	[Obsolete("Use AddWorkItem(System.Action) instead. Note the slight change in behavior (mentioned in the documentation).")]
	public static void RegisterSafeUpdate(Action callback)
	{
		AstarPath.active.AddWorkItem(new AstarWorkItem(callback, null));
	}

	// Token: 0x0600004B RID: 75 RVA: 0x00002F38 File Offset: 0x00001138
	public static void StartPath(Path path, bool pushToFront = false)
	{
		AstarPath astarPath = AstarPath.active;
		if (astarPath == null)
		{
			UnityEngine.Debug.LogError("There is no AstarPath object in the scene or it has not been initialized yet");
			return;
		}
		if (path.PipelineState != PathState.Created)
		{
			throw new Exception(string.Concat(new object[]
			{
				"The path has an invalid state. Expected ",
				PathState.Created,
				" found ",
				path.PipelineState,
				"\nMake sure you are not requesting the same path twice"
			}));
		}
		if (astarPath.pathProcessor.queue.IsTerminating)
		{
			path.FailWithError("No new paths are accepted");
			return;
		}
		if (astarPath.graphs == null || astarPath.graphs.Length == 0)
		{
			UnityEngine.Debug.LogError("There are no graphs in the scene");
			path.FailWithError("There are no graphs in the scene");
			UnityEngine.Debug.LogError(path.errorLog);
			return;
		}
		path.Claim(astarPath);
		((IPathInternals)path).AdvanceState(PathState.PathQueue);
		if (pushToFront)
		{
			astarPath.pathProcessor.queue.PushFront(path);
		}
		else
		{
			astarPath.pathProcessor.queue.Push(path);
		}
		if (!Application.isPlaying)
		{
			AstarPath.BlockUntilCalculated(path);
		}
	}

	// Token: 0x0600004C RID: 76 RVA: 0x00003033 File Offset: 0x00001233
	private void OnApplicationQuit()
	{
		this.OnDestroy();
		this.pathProcessor.AbortThreads();
	}

	// Token: 0x0600004D RID: 77 RVA: 0x00003046 File Offset: 0x00001246
	public NNInfo GetNearest(Vector3 position)
	{
		return this.GetNearest(position, NNConstraint.None);
	}

	// Token: 0x0600004E RID: 78 RVA: 0x00003054 File Offset: 0x00001254
	public NNInfo GetNearest(Vector3 position, NNConstraint constraint)
	{
		return this.GetNearest(position, constraint, null);
	}

	// Token: 0x0600004F RID: 79 RVA: 0x00003060 File Offset: 0x00001260
	public NNInfo GetNearest(Vector3 position, NNConstraint constraint, GraphNode hint)
	{
		NavGraph[] graphs = this.graphs;
		float num = float.PositiveInfinity;
		NNInfoInternal nninfoInternal = default(NNInfoInternal);
		int num2 = -1;
		if (graphs != null)
		{
			for (int i = 0; i < graphs.Length; i++)
			{
				NavGraph navGraph = graphs[i];
				if (navGraph != null && constraint.SuitableGraph(i, navGraph))
				{
					NNInfoInternal nninfoInternal2;
					if (this.fullGetNearestSearch)
					{
						nninfoInternal2 = navGraph.GetNearestForce(position, constraint);
					}
					else
					{
						nninfoInternal2 = navGraph.GetNearest(position, constraint);
					}
					if (nninfoInternal2.node != null)
					{
						float magnitude = (nninfoInternal2.clampedPosition - position).magnitude;
						if (this.prioritizeGraphs && magnitude < this.prioritizeGraphsLimit)
						{
							nninfoInternal = nninfoInternal2;
							num2 = i;
							break;
						}
						if (magnitude < num)
						{
							num = magnitude;
							nninfoInternal = nninfoInternal2;
							num2 = i;
						}
					}
				}
			}
		}
		if (num2 == -1)
		{
			return default(NNInfo);
		}
		if (nninfoInternal.constrainedNode != null)
		{
			nninfoInternal.node = nninfoInternal.constrainedNode;
			nninfoInternal.clampedPosition = nninfoInternal.constClampedPosition;
		}
		if (!this.fullGetNearestSearch && nninfoInternal.node != null && !constraint.Suitable(nninfoInternal.node))
		{
			NNInfoInternal nearestForce = graphs[num2].GetNearestForce(position, constraint);
			if (nearestForce.node != null)
			{
				nninfoInternal = nearestForce;
			}
		}
		if (!constraint.Suitable(nninfoInternal.node) || (constraint.constrainDistance && (nninfoInternal.clampedPosition - position).sqrMagnitude > this.maxNearestNodeDistanceSqr))
		{
			return default(NNInfo);
		}
		return new NNInfo(nninfoInternal);
	}

	// Token: 0x06000050 RID: 80 RVA: 0x000031D0 File Offset: 0x000013D0
	public GraphNode GetNearest(Ray ray)
	{
		if (this.graphs == null)
		{
			return null;
		}
		float minDist = float.PositiveInfinity;
		GraphNode nearestNode = null;
		Vector3 lineDirection = ray.direction;
		Vector3 lineOrigin = ray.origin;
		Action<GraphNode> <>9__0;
		for (int i = 0; i < this.graphs.Length; i++)
		{
			NavGraph navGraph = this.graphs[i];
			Action<GraphNode> action;
			if ((action = <>9__0) == null)
			{
				action = (<>9__0 = delegate(GraphNode node)
				{
					Vector3 vector = (Vector3)node.position;
					Vector3 vector2 = lineOrigin + Vector3.Dot(vector - lineOrigin, lineDirection) * lineDirection;
					float num = Mathf.Abs(vector2.x - vector.x);
					if (num * num > minDist)
					{
						return;
					}
					float num2 = Mathf.Abs(vector2.z - vector.z);
					if (num2 * num2 > minDist)
					{
						return;
					}
					float sqrMagnitude = (vector2 - vector).sqrMagnitude;
					if (sqrMagnitude < minDist)
					{
						minDist = sqrMagnitude;
						nearestNode = node;
					}
				});
			}
			navGraph.GetNodes(action);
		}
		return nearestNode;
	}

	// Token: 0x04000001 RID: 1
	public static readonly Version Version = new Version(4, 1, 16);

	// Token: 0x04000002 RID: 2
	public static readonly AstarPath.AstarDistribution Distribution = AstarPath.AstarDistribution.WebsiteDownload;

	// Token: 0x04000003 RID: 3
	public static readonly string Branch = "master_Pro";

	// Token: 0x04000004 RID: 4
	[FormerlySerializedAs("astarData")]
	public AstarData data;

	// Token: 0x04000005 RID: 5
	public static AstarPath active;

	// Token: 0x04000006 RID: 6
	public bool showNavGraphs = true;

	// Token: 0x04000007 RID: 7
	public bool showUnwalkableNodes = true;

	// Token: 0x04000008 RID: 8
	public GraphDebugMode debugMode;

	// Token: 0x04000009 RID: 9
	public float debugFloor;

	// Token: 0x0400000A RID: 10
	public float debugRoof = 20000f;

	// Token: 0x0400000B RID: 11
	public bool manualDebugFloorRoof;

	// Token: 0x0400000C RID: 12
	public bool showSearchTree;

	// Token: 0x0400000D RID: 13
	public float unwalkableNodeDebugSize = 0.3f;

	// Token: 0x0400000E RID: 14
	public PathLog logPathResults = PathLog.Normal;

	// Token: 0x0400000F RID: 15
	public float maxNearestNodeDistance = 100f;

	// Token: 0x04000010 RID: 16
	public bool scanOnStartup = true;

	// Token: 0x04000011 RID: 17
	public bool fullGetNearestSearch;

	// Token: 0x04000012 RID: 18
	public bool prioritizeGraphs;

	// Token: 0x04000013 RID: 19
	public float prioritizeGraphsLimit = 1f;

	// Token: 0x04000014 RID: 20
	public AstarColor colorSettings;

	// Token: 0x04000015 RID: 21
	[SerializeField]
	protected string[] tagNames;

	// Token: 0x04000016 RID: 22
	public Heuristic heuristic = Heuristic.Euclidean;

	// Token: 0x04000017 RID: 23
	public float heuristicScale = 1f;

	// Token: 0x04000018 RID: 24
	public ThreadCount threadCount = ThreadCount.One;

	// Token: 0x04000019 RID: 25
	public float maxFrameTime = 1f;

	// Token: 0x0400001A RID: 26
	[Obsolete("Minimum area size is mostly obsolete since the limit has been raised significantly, and the edge cases are handled automatically")]
	public int minAreaSize;

	// Token: 0x0400001B RID: 27
	public bool batchGraphUpdates;

	// Token: 0x0400001C RID: 28
	public float graphUpdateBatchingInterval = 0.2f;

	// Token: 0x0400001E RID: 30
	[NonSerialized]
	public PathHandler debugPathData;

	// Token: 0x0400001F RID: 31
	[NonSerialized]
	public ushort debugPathID;

	// Token: 0x04000020 RID: 32
	private string inGameDebugPath;

	// Token: 0x04000021 RID: 33
	[NonSerialized]
	private bool isScanningBacking;

	// Token: 0x04000022 RID: 34
	public static Action OnAwakeSettings;

	// Token: 0x04000023 RID: 35
	public static OnGraphDelegate OnGraphPreScan;

	// Token: 0x04000024 RID: 36
	public static OnGraphDelegate OnGraphPostScan;

	// Token: 0x04000025 RID: 37
	public static OnPathDelegate OnPathPreSearch;

	// Token: 0x04000026 RID: 38
	public static OnPathDelegate OnPathPostSearch;

	// Token: 0x04000027 RID: 39
	public static OnScanDelegate OnPreScan;

	// Token: 0x04000028 RID: 40
	public static OnScanDelegate OnPostScan;

	// Token: 0x04000029 RID: 41
	public static OnScanDelegate OnLatePostScan;

	// Token: 0x0400002A RID: 42
	public static OnScanDelegate OnGraphsUpdated;

	// Token: 0x0400002B RID: 43
	public static Action On65KOverflow;

	// Token: 0x0400002C RID: 44
	[Obsolete]
	public Action OnGraphsWillBeUpdated;

	// Token: 0x0400002D RID: 45
	[Obsolete]
	public Action OnGraphsWillBeUpdated2;

	// Token: 0x0400002E RID: 46
	private readonly GraphUpdateProcessor graphUpdates;

	// Token: 0x0400002F RID: 47
	private readonly WorkItemProcessor workItems;

	// Token: 0x04000030 RID: 48
	private PathProcessor pathProcessor;

	// Token: 0x04000031 RID: 49
	private bool graphUpdateRoutineRunning;

	// Token: 0x04000032 RID: 50
	private bool graphUpdatesWorkItemAdded;

	// Token: 0x04000033 RID: 51
	private float lastGraphUpdate = -9999f;

	// Token: 0x04000034 RID: 52
	private PathProcessor.GraphUpdateLock workItemLock;

	// Token: 0x04000035 RID: 53
	internal readonly PathReturnQueue pathReturnQueue;

	// Token: 0x04000036 RID: 54
	public EuclideanEmbedding euclideanEmbedding = new EuclideanEmbedding();

	// Token: 0x04000037 RID: 55
	public bool showGraphs;

	// Token: 0x04000038 RID: 56
	private ushort nextFreePathID = 1;

	// Token: 0x04000039 RID: 57
	private RetainedGizmos gizmos = new RetainedGizmos();

	// Token: 0x0400003A RID: 58
	private static int waitForPathDepth = 0;

	// Token: 0x020005FA RID: 1530
	public enum AstarDistribution
	{
		// Token: 0x0400440D RID: 17421
		WebsiteDownload,
		// Token: 0x0400440E RID: 17422
		AssetStore
	}
}
