using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding.Util
{
	// Token: 0x020005E2 RID: 1506
	public class RetainedGizmos
	{
		// Token: 0x06002991 RID: 10641 RVA: 0x001BF0E4 File Offset: 0x001BD2E4
		public GraphGizmoHelper GetSingleFrameGizmoHelper(AstarPath active)
		{
			RetainedGizmos.Hasher hasher = default(RetainedGizmos.Hasher);
			hasher.AddHash(Time.realtimeSinceStartup.GetHashCode());
			this.Draw(hasher);
			return this.GetGizmoHelper(active, hasher);
		}

		// Token: 0x06002992 RID: 10642 RVA: 0x001BF11D File Offset: 0x001BD31D
		public GraphGizmoHelper GetGizmoHelper(AstarPath active, RetainedGizmos.Hasher hasher)
		{
			GraphGizmoHelper graphGizmoHelper = ObjectPool<GraphGizmoHelper>.Claim();
			graphGizmoHelper.Init(active, hasher, this);
			return graphGizmoHelper;
		}

		// Token: 0x06002993 RID: 10643 RVA: 0x001BF12D File Offset: 0x001BD32D
		private void PoolMesh(Mesh mesh)
		{
			mesh.Clear();
			this.cachedMeshes.Push(mesh);
		}

		// Token: 0x06002994 RID: 10644 RVA: 0x001BF141 File Offset: 0x001BD341
		private Mesh GetMesh()
		{
			if (this.cachedMeshes.Count > 0)
			{
				return this.cachedMeshes.Pop();
			}
			return new Mesh
			{
				hideFlags = HideFlags.DontSave
			};
		}

		// Token: 0x06002995 RID: 10645 RVA: 0x001BF16A File Offset: 0x001BD36A
		public bool HasCachedMesh(RetainedGizmos.Hasher hasher)
		{
			return this.existingHashes.Contains(hasher.Hash);
		}

		// Token: 0x06002996 RID: 10646 RVA: 0x001BF17E File Offset: 0x001BD37E
		public bool Draw(RetainedGizmos.Hasher hasher)
		{
			this.usedHashes.Add(hasher.Hash);
			return this.HasCachedMesh(hasher);
		}

		// Token: 0x06002997 RID: 10647 RVA: 0x001BF19C File Offset: 0x001BD39C
		public void DrawExisting()
		{
			for (int i = 0; i < this.meshes.Count; i++)
			{
				this.usedHashes.Add(this.meshes[i].hash);
			}
		}

		// Token: 0x06002998 RID: 10648 RVA: 0x001BF1DC File Offset: 0x001BD3DC
		public void FinalizeDraw()
		{
			this.RemoveUnusedMeshes(this.meshes);
			Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.current);
			if (this.surfaceMaterial == null || this.lineMaterial == null)
			{
				return;
			}
			for (int i = 0; i <= 1; i++)
			{
				Material material = (i == 0) ? this.surfaceMaterial : this.lineMaterial;
				for (int j = 0; j < material.passCount; j++)
				{
					material.SetPass(j);
					for (int k = 0; k < this.meshes.Count; k++)
					{
						if (this.meshes[k].lines == (material == this.lineMaterial) && GeometryUtility.TestPlanesAABB(planes, this.meshes[k].mesh.bounds))
						{
							Graphics.DrawMeshNow(this.meshes[k].mesh, Matrix4x4.identity);
						}
					}
				}
			}
			this.usedHashes.Clear();
		}

		// Token: 0x06002999 RID: 10649 RVA: 0x001BF2E0 File Offset: 0x001BD4E0
		public void ClearCache()
		{
			this.usedHashes.Clear();
			this.RemoveUnusedMeshes(this.meshes);
			while (this.cachedMeshes.Count > 0)
			{
				UnityEngine.Object.DestroyImmediate(this.cachedMeshes.Pop());
			}
		}

		// Token: 0x0600299A RID: 10650 RVA: 0x001BF31C File Offset: 0x001BD51C
		private void RemoveUnusedMeshes(List<RetainedGizmos.MeshWithHash> meshList)
		{
			int i = 0;
			int num = 0;
			while (i < meshList.Count)
			{
				if (num == meshList.Count)
				{
					num--;
					meshList.RemoveAt(num);
				}
				else if (this.usedHashes.Contains(meshList[num].hash))
				{
					meshList[i] = meshList[num];
					i++;
					num++;
				}
				else
				{
					this.PoolMesh(meshList[num].mesh);
					this.existingHashes.Remove(meshList[num].hash);
					num++;
				}
			}
		}

		// Token: 0x04004343 RID: 17219
		private List<RetainedGizmos.MeshWithHash> meshes = new List<RetainedGizmos.MeshWithHash>();

		// Token: 0x04004344 RID: 17220
		private HashSet<ulong> usedHashes = new HashSet<ulong>();

		// Token: 0x04004345 RID: 17221
		private HashSet<ulong> existingHashes = new HashSet<ulong>();

		// Token: 0x04004346 RID: 17222
		private Stack<Mesh> cachedMeshes = new Stack<Mesh>();

		// Token: 0x04004347 RID: 17223
		public Material surfaceMaterial;

		// Token: 0x04004348 RID: 17224
		public Material lineMaterial;

		// Token: 0x0200077B RID: 1915
		public struct Hasher
		{
			// Token: 0x06002DAD RID: 11693 RVA: 0x001D0784 File Offset: 0x001CE984
			public Hasher(AstarPath active)
			{
				this.hash = 0UL;
				this.debugData = active.debugPathData;
				this.includePathSearchInfo = (this.debugData != null && (active.debugMode == GraphDebugMode.F || active.debugMode == GraphDebugMode.G || active.debugMode == GraphDebugMode.H || active.showSearchTree));
				this.AddHash((int)active.debugMode);
				this.AddHash(active.debugFloor.GetHashCode());
				this.AddHash(active.debugRoof.GetHashCode());
			}

			// Token: 0x06002DAE RID: 11694 RVA: 0x001D0808 File Offset: 0x001CEA08
			public void AddHash(int hash)
			{
				this.hash = (1572869UL * this.hash ^ (ulong)((long)hash));
			}

			// Token: 0x06002DAF RID: 11695 RVA: 0x001D0820 File Offset: 0x001CEA20
			public void HashNode(GraphNode node)
			{
				this.AddHash(node.GetGizmoHashCode());
				if (this.includePathSearchInfo)
				{
					PathNode pathNode = this.debugData.GetPathNode(node.NodeIndex);
					this.AddHash((int)pathNode.pathID);
					this.AddHash((pathNode.pathID == this.debugData.PathID) ? 1 : 0);
					this.AddHash((int)pathNode.F);
				}
			}

			// Token: 0x17000691 RID: 1681
			// (get) Token: 0x06002DB0 RID: 11696 RVA: 0x001D0888 File Offset: 0x001CEA88
			public ulong Hash
			{
				get
				{
					return this.hash;
				}
			}

			// Token: 0x04004A9C RID: 19100
			private ulong hash;

			// Token: 0x04004A9D RID: 19101
			private bool includePathSearchInfo;

			// Token: 0x04004A9E RID: 19102
			private PathHandler debugData;
		}

		// Token: 0x0200077C RID: 1916
		public class Builder : IAstarPooledObject
		{
			// Token: 0x06002DB1 RID: 11697 RVA: 0x001D0890 File Offset: 0x001CEA90
			public void DrawMesh(RetainedGizmos gizmos, Vector3[] vertices, List<int> triangles, Color[] colors)
			{
				Mesh mesh = gizmos.GetMesh();
				mesh.vertices = vertices;
				mesh.SetTriangles(triangles, 0);
				mesh.colors = colors;
				mesh.UploadMeshData(true);
				this.meshes.Add(mesh);
			}

			// Token: 0x06002DB2 RID: 11698 RVA: 0x001D08D0 File Offset: 0x001CEAD0
			public void DrawWireCube(GraphTransform tr, Bounds bounds, Color color)
			{
				Vector3 min = bounds.min;
				Vector3 max = bounds.max;
				this.DrawLine(tr.Transform(new Vector3(min.x, min.y, min.z)), tr.Transform(new Vector3(max.x, min.y, min.z)), color);
				this.DrawLine(tr.Transform(new Vector3(max.x, min.y, min.z)), tr.Transform(new Vector3(max.x, min.y, max.z)), color);
				this.DrawLine(tr.Transform(new Vector3(max.x, min.y, max.z)), tr.Transform(new Vector3(min.x, min.y, max.z)), color);
				this.DrawLine(tr.Transform(new Vector3(min.x, min.y, max.z)), tr.Transform(new Vector3(min.x, min.y, min.z)), color);
				this.DrawLine(tr.Transform(new Vector3(min.x, max.y, min.z)), tr.Transform(new Vector3(max.x, max.y, min.z)), color);
				this.DrawLine(tr.Transform(new Vector3(max.x, max.y, min.z)), tr.Transform(new Vector3(max.x, max.y, max.z)), color);
				this.DrawLine(tr.Transform(new Vector3(max.x, max.y, max.z)), tr.Transform(new Vector3(min.x, max.y, max.z)), color);
				this.DrawLine(tr.Transform(new Vector3(min.x, max.y, max.z)), tr.Transform(new Vector3(min.x, max.y, min.z)), color);
				this.DrawLine(tr.Transform(new Vector3(min.x, min.y, min.z)), tr.Transform(new Vector3(min.x, max.y, min.z)), color);
				this.DrawLine(tr.Transform(new Vector3(max.x, min.y, min.z)), tr.Transform(new Vector3(max.x, max.y, min.z)), color);
				this.DrawLine(tr.Transform(new Vector3(max.x, min.y, max.z)), tr.Transform(new Vector3(max.x, max.y, max.z)), color);
				this.DrawLine(tr.Transform(new Vector3(min.x, min.y, max.z)), tr.Transform(new Vector3(min.x, max.y, max.z)), color);
			}

			// Token: 0x06002DB3 RID: 11699 RVA: 0x001D0BFC File Offset: 0x001CEDFC
			public void DrawLine(Vector3 start, Vector3 end, Color color)
			{
				this.lines.Add(start);
				this.lines.Add(end);
				Color32 item = color;
				this.lineColors.Add(item);
				this.lineColors.Add(item);
			}

			// Token: 0x06002DB4 RID: 11700 RVA: 0x001D0C40 File Offset: 0x001CEE40
			public void Submit(RetainedGizmos gizmos, RetainedGizmos.Hasher hasher)
			{
				this.SubmitLines(gizmos, hasher.Hash);
				this.SubmitMeshes(gizmos, hasher.Hash);
			}

			// Token: 0x06002DB5 RID: 11701 RVA: 0x001D0C60 File Offset: 0x001CEE60
			private void SubmitMeshes(RetainedGizmos gizmos, ulong hash)
			{
				for (int i = 0; i < this.meshes.Count; i++)
				{
					gizmos.meshes.Add(new RetainedGizmos.MeshWithHash
					{
						hash = hash,
						mesh = this.meshes[i],
						lines = false
					});
					gizmos.existingHashes.Add(hash);
				}
			}

			// Token: 0x06002DB6 RID: 11702 RVA: 0x001D0CC8 File Offset: 0x001CEEC8
			private void SubmitLines(RetainedGizmos gizmos, ulong hash)
			{
				int num = (this.lines.Count + 32766 - 1) / 32766;
				for (int i = 0; i < num; i++)
				{
					int num2 = 32766 * i;
					int num3 = Mathf.Min(num2 + 32766, this.lines.Count);
					int num4 = num3 - num2;
					List<Vector3> list = ListPool<Vector3>.Claim(num4 * 2);
					List<Color32> list2 = ListPool<Color32>.Claim(num4 * 2);
					List<Vector3> list3 = ListPool<Vector3>.Claim(num4 * 2);
					List<Vector2> list4 = ListPool<Vector2>.Claim(num4 * 2);
					List<int> list5 = ListPool<int>.Claim(num4 * 3);
					for (int j = num2; j < num3; j++)
					{
						Vector3 item = this.lines[j];
						list.Add(item);
						list.Add(item);
						Color32 item2 = this.lineColors[j];
						list2.Add(item2);
						list2.Add(item2);
						list4.Add(new Vector2(0f, 0f));
						list4.Add(new Vector2(1f, 0f));
					}
					for (int k = num2; k < num3; k += 2)
					{
						Vector3 item3 = this.lines[k + 1] - this.lines[k];
						list3.Add(item3);
						list3.Add(item3);
						list3.Add(item3);
						list3.Add(item3);
					}
					int l = 0;
					int num5 = 0;
					while (l < num4 * 3)
					{
						list5.Add(num5);
						list5.Add(num5 + 1);
						list5.Add(num5 + 2);
						list5.Add(num5 + 1);
						list5.Add(num5 + 3);
						list5.Add(num5 + 2);
						l += 6;
						num5 += 4;
					}
					Mesh mesh = gizmos.GetMesh();
					mesh.SetVertices(list);
					mesh.SetTriangles(list5, 0);
					mesh.SetColors(list2);
					mesh.SetNormals(list3);
					mesh.SetUVs(0, list4);
					mesh.UploadMeshData(true);
					ListPool<Vector3>.Release(ref list);
					ListPool<Color32>.Release(ref list2);
					ListPool<Vector3>.Release(ref list3);
					ListPool<Vector2>.Release(ref list4);
					ListPool<int>.Release(ref list5);
					gizmos.meshes.Add(new RetainedGizmos.MeshWithHash
					{
						hash = hash,
						mesh = mesh,
						lines = true
					});
					gizmos.existingHashes.Add(hash);
				}
			}

			// Token: 0x06002DB7 RID: 11703 RVA: 0x001D0F2B File Offset: 0x001CF12B
			void IAstarPooledObject.OnEnterPool()
			{
				this.lines.Clear();
				this.lineColors.Clear();
				this.meshes.Clear();
			}

			// Token: 0x04004A9F RID: 19103
			private List<Vector3> lines = new List<Vector3>();

			// Token: 0x04004AA0 RID: 19104
			private List<Color32> lineColors = new List<Color32>();

			// Token: 0x04004AA1 RID: 19105
			private List<Mesh> meshes = new List<Mesh>();
		}

		// Token: 0x0200077D RID: 1917
		private struct MeshWithHash
		{
			// Token: 0x04004AA2 RID: 19106
			public ulong hash;

			// Token: 0x04004AA3 RID: 19107
			public Mesh mesh;

			// Token: 0x04004AA4 RID: 19108
			public bool lines;
		}
	}
}
