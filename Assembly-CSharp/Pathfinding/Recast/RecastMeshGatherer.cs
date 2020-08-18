using System;
using System.Collections.Generic;
using Pathfinding.Util;
using Pathfinding.Voxels;
using UnityEngine;

namespace Pathfinding.Recast
{
	// Token: 0x020005B7 RID: 1463
	internal class RecastMeshGatherer
	{
		// Token: 0x060027CA RID: 10186 RVA: 0x001B437C File Offset: 0x001B257C
		public RecastMeshGatherer(Bounds bounds, int terrainSampleSize, LayerMask mask, List<string> tagMask, float colliderRasterizeDetail)
		{
			terrainSampleSize = Math.Max(terrainSampleSize, 1);
			this.bounds = bounds;
			this.terrainSampleSize = terrainSampleSize;
			this.mask = mask;
			this.tagMask = (tagMask ?? new List<string>());
			this.colliderRasterizeDetail = colliderRasterizeDetail;
		}

		// Token: 0x060027CB RID: 10187 RVA: 0x001B43D4 File Offset: 0x001B25D4
		private static List<MeshFilter> FilterMeshes(MeshFilter[] meshFilters, List<string> tagMask, LayerMask layerMask)
		{
			List<MeshFilter> list = new List<MeshFilter>(meshFilters.Length / 3);
			foreach (MeshFilter meshFilter in meshFilters)
			{
				Renderer component = meshFilter.GetComponent<Renderer>();
				if (component != null && meshFilter.sharedMesh != null && component.enabled && ((1 << meshFilter.gameObject.layer & layerMask) != 0 || tagMask.Contains(meshFilter.tag)) && meshFilter.GetComponent<RecastMeshObj>() == null)
				{
					list.Add(meshFilter);
				}
			}
			return list;
		}

		// Token: 0x060027CC RID: 10188 RVA: 0x001B4464 File Offset: 0x001B2664
		public void CollectSceneMeshes(List<RasterizationMesh> meshes)
		{
			if (this.tagMask.Count > 0 || this.mask != 0)
			{
				List<MeshFilter> list = RecastMeshGatherer.FilterMeshes(UnityEngine.Object.FindObjectsOfType<MeshFilter>(), this.tagMask, this.mask);
				Dictionary<Mesh, Vector3[]> dictionary = new Dictionary<Mesh, Vector3[]>();
				Dictionary<Mesh, int[]> dictionary2 = new Dictionary<Mesh, int[]>();
				bool flag = false;
				for (int i = 0; i < list.Count; i++)
				{
					MeshFilter meshFilter = list[i];
					Renderer component = meshFilter.GetComponent<Renderer>();
					if (component.isPartOfStaticBatch)
					{
						flag = true;
					}
					else if (component.bounds.Intersects(this.bounds))
					{
						Mesh sharedMesh = meshFilter.sharedMesh;
						RasterizationMesh rasterizationMesh;
						if (dictionary.ContainsKey(sharedMesh))
						{
							rasterizationMesh = new RasterizationMesh(dictionary[sharedMesh], dictionary2[sharedMesh], component.bounds);
						}
						else
						{
							rasterizationMesh = new RasterizationMesh(sharedMesh.vertices, sharedMesh.triangles, component.bounds);
							dictionary[sharedMesh] = rasterizationMesh.vertices;
							dictionary2[sharedMesh] = rasterizationMesh.triangles;
						}
						rasterizationMesh.matrix = component.localToWorldMatrix;
						rasterizationMesh.original = meshFilter;
						meshes.Add(rasterizationMesh);
					}
					if (flag)
					{
						Debug.LogWarning("Some meshes were statically batched. These meshes can not be used for navmesh calculation due to technical constraints.\nDuring runtime scripts cannot access the data of meshes which have been statically batched.\nOne way to solve this problem is to use cached startup (Save & Load tab in the inspector) to only calculate the graph when the game is not playing.");
					}
				}
			}
		}

		// Token: 0x060027CD RID: 10189 RVA: 0x001B45A8 File Offset: 0x001B27A8
		public void CollectRecastMeshObjs(List<RasterizationMesh> buffer)
		{
			List<RecastMeshObj> list = ListPool<RecastMeshObj>.Claim();
			RecastMeshObj.GetAllInBounds(list, this.bounds);
			Dictionary<Mesh, Vector3[]> dictionary = new Dictionary<Mesh, Vector3[]>();
			Dictionary<Mesh, int[]> dictionary2 = new Dictionary<Mesh, int[]>();
			for (int i = 0; i < list.Count; i++)
			{
				MeshFilter meshFilter = list[i].GetMeshFilter();
				Renderer renderer = (meshFilter != null) ? meshFilter.GetComponent<Renderer>() : null;
				if (meshFilter != null && renderer != null)
				{
					Mesh sharedMesh = meshFilter.sharedMesh;
					RasterizationMesh rasterizationMesh;
					if (dictionary.ContainsKey(sharedMesh))
					{
						rasterizationMesh = new RasterizationMesh(dictionary[sharedMesh], dictionary2[sharedMesh], renderer.bounds);
					}
					else
					{
						rasterizationMesh = new RasterizationMesh(sharedMesh.vertices, sharedMesh.triangles, renderer.bounds);
						dictionary[sharedMesh] = rasterizationMesh.vertices;
						dictionary2[sharedMesh] = rasterizationMesh.triangles;
					}
					rasterizationMesh.matrix = renderer.localToWorldMatrix;
					rasterizationMesh.original = meshFilter;
					rasterizationMesh.area = list[i].area;
					buffer.Add(rasterizationMesh);
				}
				else
				{
					Collider collider = list[i].GetCollider();
					if (collider == null)
					{
						Debug.LogError("RecastMeshObject (" + list[i].gameObject.name + ") didn't have a collider or MeshFilter+Renderer attached", list[i].gameObject);
					}
					else
					{
						RasterizationMesh rasterizationMesh2 = this.RasterizeCollider(collider);
						if (rasterizationMesh2 != null)
						{
							rasterizationMesh2.area = list[i].area;
							buffer.Add(rasterizationMesh2);
						}
					}
				}
			}
			this.capsuleCache.Clear();
			ListPool<RecastMeshObj>.Release(ref list);
		}

		// Token: 0x060027CE RID: 10190 RVA: 0x001B4750 File Offset: 0x001B2950
		public void CollectTerrainMeshes(bool rasterizeTrees, float desiredChunkSize, List<RasterizationMesh> result)
		{
			Terrain[] activeTerrains = Terrain.activeTerrains;
			if (activeTerrains.Length != 0)
			{
				for (int i = 0; i < activeTerrains.Length; i++)
				{
					if (!(activeTerrains[i].terrainData == null))
					{
						this.GenerateTerrainChunks(activeTerrains[i], this.bounds, desiredChunkSize, result);
						if (rasterizeTrees)
						{
							this.CollectTreeMeshes(activeTerrains[i], result);
						}
					}
				}
			}
		}

		// Token: 0x060027CF RID: 10191 RVA: 0x001B47A4 File Offset: 0x001B29A4
		private void GenerateTerrainChunks(Terrain terrain, Bounds bounds, float desiredChunkSize, List<RasterizationMesh> result)
		{
			TerrainData terrainData = terrain.terrainData;
			if (terrainData == null)
			{
				throw new ArgumentException("Terrain contains no terrain data");
			}
			Vector3 position = terrain.GetPosition();
			Vector3 center = position + terrainData.size * 0.5f;
			Bounds bounds2 = new Bounds(center, terrainData.size);
			if (!bounds2.Intersects(bounds))
			{
				return;
			}
			int heightmapResolution = terrainData.heightmapResolution;
			int heightmapResolution2 = terrainData.heightmapResolution;
			float[,] heights = terrainData.GetHeights(0, 0, heightmapResolution, heightmapResolution2);
			Vector3 heightmapScale = terrainData.heightmapScale;
			heightmapScale.y = terrainData.size.y;
			int num = Mathf.CeilToInt(Mathf.Max(desiredChunkSize / (heightmapScale.x * (float)this.terrainSampleSize), 12f)) * this.terrainSampleSize;
			int num2 = Mathf.CeilToInt(Mathf.Max(desiredChunkSize / (heightmapScale.z * (float)this.terrainSampleSize), 12f)) * this.terrainSampleSize;
			for (int i = 0; i < heightmapResolution2; i += num2)
			{
				for (int j = 0; j < heightmapResolution; j += num)
				{
					int num3 = Mathf.Min(num, heightmapResolution - j);
					int num4 = Mathf.Min(num2, heightmapResolution2 - i);
					Vector3 min = position + new Vector3((float)i * heightmapScale.x, 0f, (float)j * heightmapScale.z);
					Vector3 max = position + new Vector3((float)(i + num4) * heightmapScale.x, heightmapScale.y, (float)(j + num3) * heightmapScale.z);
					Bounds bounds3 = default(Bounds);
					bounds3.SetMinMax(min, max);
					if (bounds3.Intersects(bounds))
					{
						RasterizationMesh item = this.GenerateHeightmapChunk(heights, heightmapScale, position, j, i, num3, num4, this.terrainSampleSize);
						result.Add(item);
					}
				}
			}
		}

		// Token: 0x060027D0 RID: 10192 RVA: 0x001B4972 File Offset: 0x001B2B72
		private static int CeilDivision(int lhs, int rhs)
		{
			return (lhs + rhs - 1) / rhs;
		}

		// Token: 0x060027D1 RID: 10193 RVA: 0x001B497C File Offset: 0x001B2B7C
		private RasterizationMesh GenerateHeightmapChunk(float[,] heights, Vector3 sampleSize, Vector3 offset, int x0, int z0, int width, int depth, int stride)
		{
			int num = RecastMeshGatherer.CeilDivision(width, this.terrainSampleSize) + 1;
			int num2 = RecastMeshGatherer.CeilDivision(depth, this.terrainSampleSize) + 1;
			int length = heights.GetLength(0);
			int length2 = heights.GetLength(1);
			int num3 = num * num2;
			Vector3[] array = ArrayPool<Vector3>.Claim(num3);
			for (int i = 0; i < num2; i++)
			{
				for (int j = 0; j < num; j++)
				{
					int num4 = Math.Min(x0 + j * stride, length - 1);
					int num5 = Math.Min(z0 + i * stride, length2 - 1);
					array[i * num + j] = new Vector3((float)num5 * sampleSize.x, heights[num4, num5] * sampleSize.y, (float)num4 * sampleSize.z) + offset;
				}
			}
			int num6 = (num - 1) * (num2 - 1) * 2 * 3;
			int[] array2 = ArrayPool<int>.Claim(num6);
			int num7 = 0;
			for (int k = 0; k < num2 - 1; k++)
			{
				for (int l = 0; l < num - 1; l++)
				{
					array2[num7] = k * num + l;
					array2[num7 + 1] = k * num + l + 1;
					array2[num7 + 2] = (k + 1) * num + l + 1;
					num7 += 3;
					array2[num7] = k * num + l;
					array2[num7 + 1] = (k + 1) * num + l + 1;
					array2[num7 + 2] = (k + 1) * num + l;
					num7 += 3;
				}
			}
			RasterizationMesh rasterizationMesh = new RasterizationMesh(array, array2, default(Bounds));
			rasterizationMesh.numVertices = num3;
			rasterizationMesh.numTriangles = num6;
			rasterizationMesh.pool = true;
			rasterizationMesh.RecalculateBounds();
			return rasterizationMesh;
		}

		// Token: 0x060027D2 RID: 10194 RVA: 0x001B4B24 File Offset: 0x001B2D24
		private void CollectTreeMeshes(Terrain terrain, List<RasterizationMesh> result)
		{
			TerrainData terrainData = terrain.terrainData;
			for (int i = 0; i < terrainData.treeInstances.Length; i++)
			{
				TreeInstance treeInstance = terrainData.treeInstances[i];
				TreePrototype treePrototype = terrainData.treePrototypes[treeInstance.prototypeIndex];
				if (!(treePrototype.prefab == null))
				{
					Collider component = treePrototype.prefab.GetComponent<Collider>();
					Vector3 pos = terrain.transform.position + Vector3.Scale(treeInstance.position, terrainData.size);
					if (component == null)
					{
						Bounds bounds = new Bounds(terrain.transform.position + Vector3.Scale(treeInstance.position, terrainData.size), new Vector3(treeInstance.widthScale, treeInstance.heightScale, treeInstance.widthScale));
						Matrix4x4 matrix = Matrix4x4.TRS(pos, Quaternion.identity, new Vector3(treeInstance.widthScale, treeInstance.heightScale, treeInstance.widthScale) * 0.5f);
						RasterizationMesh item = new RasterizationMesh(RecastMeshGatherer.BoxColliderVerts, RecastMeshGatherer.BoxColliderTris, bounds, matrix);
						result.Add(item);
					}
					else
					{
						Vector3 s = new Vector3(treeInstance.widthScale, treeInstance.heightScale, treeInstance.widthScale);
						RasterizationMesh rasterizationMesh = this.RasterizeCollider(component, Matrix4x4.TRS(pos, Quaternion.identity, s));
						if (rasterizationMesh != null)
						{
							rasterizationMesh.RecalculateBounds();
							result.Add(rasterizationMesh);
						}
					}
				}
			}
		}

		// Token: 0x060027D3 RID: 10195 RVA: 0x001B4C8C File Offset: 0x001B2E8C
		public void CollectColliderMeshes(List<RasterizationMesh> result)
		{
			Collider[] array = Physics.OverlapSphere(this.bounds.center, this.bounds.size.magnitude, -1, QueryTriggerInteraction.Ignore);
			if (this.tagMask.Count > 0 || this.mask != 0)
			{
				foreach (Collider collider in array)
				{
					if (((this.mask >> collider.gameObject.layer & 1) != 0 || this.tagMask.Contains(collider.tag)) && collider.enabled && !collider.isTrigger && collider.bounds.Intersects(this.bounds) && collider.GetComponent<RecastMeshObj>() == null)
					{
						RasterizationMesh rasterizationMesh = this.RasterizeCollider(collider);
						if (rasterizationMesh != null)
						{
							result.Add(rasterizationMesh);
						}
					}
				}
			}
			this.capsuleCache.Clear();
		}

		// Token: 0x060027D4 RID: 10196 RVA: 0x001B4D88 File Offset: 0x001B2F88
		private RasterizationMesh RasterizeCollider(Collider col)
		{
			return this.RasterizeCollider(col, col.transform.localToWorldMatrix);
		}

		// Token: 0x060027D5 RID: 10197 RVA: 0x001B4D9C File Offset: 0x001B2F9C
		private RasterizationMesh RasterizeCollider(Collider col, Matrix4x4 localToWorldMatrix)
		{
			RasterizationMesh result = null;
			if (col is BoxCollider)
			{
				result = this.RasterizeBoxCollider(col as BoxCollider, localToWorldMatrix);
			}
			else if (col is SphereCollider || col is CapsuleCollider)
			{
				SphereCollider sphereCollider = col as SphereCollider;
				CapsuleCollider capsuleCollider = col as CapsuleCollider;
				float num = (sphereCollider != null) ? sphereCollider.radius : capsuleCollider.radius;
				float height = (sphereCollider != null) ? 0f : (capsuleCollider.height * 0.5f / num - 1f);
				Quaternion q = Quaternion.identity;
				if (capsuleCollider != null)
				{
					q = Quaternion.Euler((float)((capsuleCollider.direction == 2) ? 90 : 0), 0f, (float)((capsuleCollider.direction == 0) ? 90 : 0));
				}
				Matrix4x4 matrix4x = Matrix4x4.TRS((sphereCollider != null) ? sphereCollider.center : capsuleCollider.center, q, Vector3.one * num);
				matrix4x = localToWorldMatrix * matrix4x;
				result = this.RasterizeCapsuleCollider(num, height, col.bounds, matrix4x);
			}
			else if (col is MeshCollider)
			{
				MeshCollider meshCollider = col as MeshCollider;
				if (meshCollider.sharedMesh != null)
				{
					result = new RasterizationMesh(meshCollider.sharedMesh.vertices, meshCollider.sharedMesh.triangles, meshCollider.bounds, localToWorldMatrix);
				}
			}
			return result;
		}

		// Token: 0x060027D6 RID: 10198 RVA: 0x001B4EF0 File Offset: 0x001B30F0
		private RasterizationMesh RasterizeBoxCollider(BoxCollider collider, Matrix4x4 localToWorldMatrix)
		{
			Matrix4x4 matrix4x = Matrix4x4.TRS(collider.center, Quaternion.identity, collider.size * 0.5f);
			matrix4x = localToWorldMatrix * matrix4x;
			return new RasterizationMesh(RecastMeshGatherer.BoxColliderVerts, RecastMeshGatherer.BoxColliderTris, collider.bounds, matrix4x);
		}

		// Token: 0x060027D7 RID: 10199 RVA: 0x001B4F3C File Offset: 0x001B313C
		private RasterizationMesh RasterizeCapsuleCollider(float radius, float height, Bounds bounds, Matrix4x4 localToWorldMatrix)
		{
			int num = Mathf.Max(4, Mathf.RoundToInt(this.colliderRasterizeDetail * Mathf.Sqrt(localToWorldMatrix.MultiplyVector(Vector3.one).magnitude)));
			if (num > 100)
			{
				Debug.LogWarning("Very large detail for some collider meshes. Consider decreasing Collider Rasterize Detail (RecastGraph)");
			}
			int num2 = num;
			RecastMeshGatherer.CapsuleCache capsuleCache = null;
			for (int i = 0; i < this.capsuleCache.Count; i++)
			{
				RecastMeshGatherer.CapsuleCache capsuleCache2 = this.capsuleCache[i];
				if (capsuleCache2.rows == num && Mathf.Approximately(capsuleCache2.height, height))
				{
					capsuleCache = capsuleCache2;
				}
			}
			Vector3[] array;
			if (capsuleCache == null)
			{
				array = new Vector3[num * num2 + 2];
				List<int> list = new List<int>();
				array[array.Length - 1] = Vector3.up;
				for (int j = 0; j < num; j++)
				{
					for (int k = 0; k < num2; k++)
					{
						array[k + j * num2] = new Vector3(Mathf.Cos((float)k * 3.1415927f * 2f / (float)num2) * Mathf.Sin((float)j * 3.1415927f / (float)(num - 1)), Mathf.Cos((float)j * 3.1415927f / (float)(num - 1)) + ((j < num / 2) ? height : (-height)), Mathf.Sin((float)k * 3.1415927f * 2f / (float)num2) * Mathf.Sin((float)j * 3.1415927f / (float)(num - 1)));
					}
				}
				array[array.Length - 2] = Vector3.down;
				int l = 0;
				int item = num2 - 1;
				while (l < num2)
				{
					list.Add(array.Length - 1);
					list.Add(item);
					list.Add(l);
					item = l++;
				}
				for (int m = 1; m < num; m++)
				{
					int n = 0;
					int num3 = num2 - 1;
					while (n < num2)
					{
						list.Add(m * num2 + n);
						list.Add(m * num2 + num3);
						list.Add((m - 1) * num2 + n);
						list.Add((m - 1) * num2 + num3);
						list.Add((m - 1) * num2 + n);
						list.Add(m * num2 + num3);
						num3 = n++;
					}
				}
				int num4 = 0;
				int num5 = num2 - 1;
				while (num4 < num2)
				{
					list.Add(array.Length - 2);
					list.Add((num - 1) * num2 + num5);
					list.Add((num - 1) * num2 + num4);
					num5 = num4++;
				}
				capsuleCache = new RecastMeshGatherer.CapsuleCache();
				capsuleCache.rows = num;
				capsuleCache.height = height;
				capsuleCache.verts = array;
				capsuleCache.tris = list.ToArray();
				this.capsuleCache.Add(capsuleCache);
			}
			array = capsuleCache.verts;
			int[] tris = capsuleCache.tris;
			return new RasterizationMesh(array, tris, bounds, localToWorldMatrix);
		}

		// Token: 0x04004235 RID: 16949
		private readonly int terrainSampleSize;

		// Token: 0x04004236 RID: 16950
		private readonly LayerMask mask;

		// Token: 0x04004237 RID: 16951
		private readonly List<string> tagMask;

		// Token: 0x04004238 RID: 16952
		private readonly float colliderRasterizeDetail;

		// Token: 0x04004239 RID: 16953
		private readonly Bounds bounds;

		// Token: 0x0400423A RID: 16954
		private static readonly int[] BoxColliderTris = new int[]
		{
			0,
			1,
			2,
			0,
			2,
			3,
			6,
			5,
			4,
			7,
			6,
			4,
			0,
			5,
			1,
			0,
			4,
			5,
			1,
			6,
			2,
			1,
			5,
			6,
			2,
			7,
			3,
			2,
			6,
			7,
			3,
			4,
			0,
			3,
			7,
			4
		};

		// Token: 0x0400423B RID: 16955
		private static readonly Vector3[] BoxColliderVerts = new Vector3[]
		{
			new Vector3(-1f, -1f, -1f),
			new Vector3(1f, -1f, -1f),
			new Vector3(1f, -1f, 1f),
			new Vector3(-1f, -1f, 1f),
			new Vector3(-1f, 1f, -1f),
			new Vector3(1f, 1f, -1f),
			new Vector3(1f, 1f, 1f),
			new Vector3(-1f, 1f, 1f)
		};

		// Token: 0x0400423C RID: 16956
		private List<RecastMeshGatherer.CapsuleCache> capsuleCache = new List<RecastMeshGatherer.CapsuleCache>();

		// Token: 0x02000762 RID: 1890
		private class CapsuleCache
		{
			// Token: 0x04004A2B RID: 18987
			public int rows;

			// Token: 0x04004A2C RID: 18988
			public float height;

			// Token: 0x04004A2D RID: 18989
			public Vector3[] verts;

			// Token: 0x04004A2E RID: 18990
			public int[] tris;
		}
	}
}
