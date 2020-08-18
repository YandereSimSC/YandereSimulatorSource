using System;
using System.Collections.Generic;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000515 RID: 1301
	public class RichPath
	{
		// Token: 0x0600213E RID: 8510 RVA: 0x00189057 File Offset: 0x00187257
		public RichPath()
		{
			this.Clear();
		}

		// Token: 0x0600213F RID: 8511 RVA: 0x00189070 File Offset: 0x00187270
		public void Clear()
		{
			this.parts.Clear();
			this.currentPart = 0;
			this.Endpoint = new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
		}

		// Token: 0x06002140 RID: 8512 RVA: 0x001890A0 File Offset: 0x001872A0
		public void Initialize(Seeker seeker, Path path, bool mergePartEndpoints, bool simplificationMode)
		{
			if (path.error)
			{
				throw new ArgumentException("Path has an error");
			}
			List<GraphNode> path2 = path.path;
			if (path2.Count == 0)
			{
				throw new ArgumentException("Path traverses no nodes");
			}
			this.seeker = seeker;
			for (int i = 0; i < this.parts.Count; i++)
			{
				RichFunnel richFunnel = this.parts[i] as RichFunnel;
				RichSpecial richSpecial = this.parts[i] as RichSpecial;
				if (richFunnel != null)
				{
					ObjectPool<RichFunnel>.Release(ref richFunnel);
				}
				else if (richSpecial != null)
				{
					ObjectPool<RichSpecial>.Release(ref richSpecial);
				}
			}
			this.Clear();
			this.Endpoint = path.vectorPath[path.vectorPath.Count - 1];
			for (int j = 0; j < path2.Count; j++)
			{
				if (path2[j] is TriangleMeshNode)
				{
					NavmeshBase navmeshBase = AstarData.GetGraph(path2[j]) as NavmeshBase;
					if (navmeshBase == null)
					{
						throw new Exception("Found a TriangleMeshNode that was not in a NavmeshBase graph");
					}
					RichFunnel richFunnel2 = ObjectPool<RichFunnel>.Claim().Initialize(this, navmeshBase);
					richFunnel2.funnelSimplification = simplificationMode;
					int num = j;
					uint graphIndex = path2[num].GraphIndex;
					while (j < path2.Count && (path2[j].GraphIndex == graphIndex || path2[j] is NodeLink3Node))
					{
						j++;
					}
					j--;
					if (num == 0)
					{
						richFunnel2.exactStart = path.vectorPath[0];
					}
					else
					{
						richFunnel2.exactStart = (Vector3)path2[mergePartEndpoints ? (num - 1) : num].position;
					}
					if (j == path2.Count - 1)
					{
						richFunnel2.exactEnd = path.vectorPath[path.vectorPath.Count - 1];
					}
					else
					{
						richFunnel2.exactEnd = (Vector3)path2[mergePartEndpoints ? (j + 1) : j].position;
					}
					richFunnel2.BuildFunnelCorridor(path2, num, j);
					this.parts.Add(richFunnel2);
				}
				else if (NodeLink2.GetNodeLink(path2[j]) != null)
				{
					NodeLink2 nodeLink = NodeLink2.GetNodeLink(path2[j]);
					int num2 = j;
					uint graphIndex2 = path2[num2].GraphIndex;
					j++;
					while (j < path2.Count && path2[j].GraphIndex == graphIndex2)
					{
						j++;
					}
					j--;
					if (j - num2 > 1)
					{
						throw new Exception("NodeLink2 path length greater than two (2) nodes. " + (j - num2));
					}
					if (j - num2 != 0)
					{
						RichSpecial item = ObjectPool<RichSpecial>.Claim().Initialize(nodeLink, path2[num2]);
						this.parts.Add(item);
					}
				}
			}
		}

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x06002141 RID: 8513 RVA: 0x00189365 File Offset: 0x00187565
		// (set) Token: 0x06002142 RID: 8514 RVA: 0x0018936D File Offset: 0x0018756D
		public Vector3 Endpoint { get; private set; }

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x06002143 RID: 8515 RVA: 0x00189376 File Offset: 0x00187576
		public bool CompletedAllParts
		{
			get
			{
				return this.currentPart >= this.parts.Count;
			}
		}

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x06002144 RID: 8516 RVA: 0x0018938E File Offset: 0x0018758E
		public bool IsLastPart
		{
			get
			{
				return this.currentPart >= this.parts.Count - 1;
			}
		}

		// Token: 0x06002145 RID: 8517 RVA: 0x001893A8 File Offset: 0x001875A8
		public void NextPart()
		{
			this.currentPart = Mathf.Min(this.currentPart + 1, this.parts.Count);
		}

		// Token: 0x06002146 RID: 8518 RVA: 0x001893C8 File Offset: 0x001875C8
		public RichPathPart GetCurrentPart()
		{
			if (this.parts.Count == 0)
			{
				return null;
			}
			if (this.currentPart >= this.parts.Count)
			{
				return this.parts[this.parts.Count - 1];
			}
			return this.parts[this.currentPart];
		}

		// Token: 0x04003E7A RID: 15994
		private int currentPart;

		// Token: 0x04003E7B RID: 15995
		private readonly List<RichPathPart> parts = new List<RichPathPart>();

		// Token: 0x04003E7C RID: 15996
		public Seeker seeker;

		// Token: 0x04003E7D RID: 15997
		public ITransform transform;
	}
}
