using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Pathfinding.Serialization;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000565 RID: 1381
	[JsonOptIn]
	public class GridGraph : NavGraph, IUpdatableGraph, ITransformedGraph, IRaycastableGraph
	{
		// Token: 0x06002485 RID: 9349 RVA: 0x00197E0A File Offset: 0x0019600A
		protected override void OnDestroy()
		{
			base.OnDestroy();
			this.RemoveGridGraphFromStatic();
		}

		// Token: 0x06002486 RID: 9350 RVA: 0x00197E18 File Offset: 0x00196018
		protected override void DestroyAllNodes()
		{
			this.GetNodes(delegate(GraphNode node)
			{
				(node as GridNodeBase).ClearCustomConnections(true);
				node.ClearConnections(false);
				node.Destroy();
			});
		}

		// Token: 0x06002487 RID: 9351 RVA: 0x00197E3F File Offset: 0x0019603F
		private void RemoveGridGraphFromStatic()
		{
			GridNode.SetGridGraph(AstarPath.active.data.GetGraphIndex(this), null);
		}

		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x06002488 RID: 9352 RVA: 0x0002291C File Offset: 0x00020B1C
		public virtual bool uniformWidthDepthGrid
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x06002489 RID: 9353 RVA: 0x0002291C File Offset: 0x00020B1C
		public virtual int LayerCount
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x0600248A RID: 9354 RVA: 0x00197E57 File Offset: 0x00196057
		public override int CountNodes()
		{
			return this.nodes.Length;
		}

		// Token: 0x0600248B RID: 9355 RVA: 0x00197E64 File Offset: 0x00196064
		public override void GetNodes(Action<GraphNode> action)
		{
			if (this.nodes == null)
			{
				return;
			}
			for (int i = 0; i < this.nodes.Length; i++)
			{
				action(this.nodes[i]);
			}
		}

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x0600248C RID: 9356 RVA: 0x00197E9B File Offset: 0x0019609B
		protected bool useRaycastNormal
		{
			get
			{
				return Math.Abs(90f - this.maxSlope) > float.Epsilon;
			}
		}

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x0600248D RID: 9357 RVA: 0x00197EB5 File Offset: 0x001960B5
		// (set) Token: 0x0600248E RID: 9358 RVA: 0x00197EBD File Offset: 0x001960BD
		public Vector2 size { get; protected set; }

		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x0600248F RID: 9359 RVA: 0x00197EC6 File Offset: 0x001960C6
		// (set) Token: 0x06002490 RID: 9360 RVA: 0x00197ECE File Offset: 0x001960CE
		public GraphTransform transform { get; private set; }

		// Token: 0x06002491 RID: 9361 RVA: 0x00197ED8 File Offset: 0x001960D8
		public GridGraph()
		{
			this.unclampedSize = new Vector2(10f, 10f);
			this.nodeSize = 1f;
			this.collision = new GraphCollision();
			this.transform = new GraphTransform(Matrix4x4.identity);
		}

		// Token: 0x06002492 RID: 9362 RVA: 0x00197FD1 File Offset: 0x001961D1
		public override void RelocateNodes(Matrix4x4 deltaMatrix)
		{
			throw new Exception("This method cannot be used for Grid Graphs. Please use the other overload of RelocateNodes instead");
		}

		// Token: 0x06002493 RID: 9363 RVA: 0x00197FE0 File Offset: 0x001961E0
		public void RelocateNodes(Vector3 center, Quaternion rotation, float nodeSize, float aspectRatio = 1f, float isometricAngle = 0f)
		{
			GraphTransform previousTransform = this.transform;
			this.center = center;
			this.rotation = rotation.eulerAngles;
			this.aspectRatio = aspectRatio;
			this.isometricAngle = isometricAngle;
			this.SetDimensions(this.width, this.depth, nodeSize);
			this.GetNodes(delegate(GraphNode node)
			{
				GridNodeBase gridNodeBase = node as GridNodeBase;
				float y = previousTransform.InverseTransform((Vector3)node.position).y;
				node.position = this.GraphPointToWorld(gridNodeBase.XCoordinateInGrid, gridNodeBase.ZCoordinateInGrid, y);
			});
		}

		// Token: 0x06002494 RID: 9364 RVA: 0x0019804F File Offset: 0x0019624F
		public Int3 GraphPointToWorld(int x, int z, float height)
		{
			return (Int3)this.transform.Transform(new Vector3((float)x + 0.5f, height, (float)z + 0.5f));
		}

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x06002495 RID: 9365 RVA: 0x00198077 File Offset: 0x00196277
		// (set) Token: 0x06002496 RID: 9366 RVA: 0x0019807F File Offset: 0x0019627F
		public int Width
		{
			get
			{
				return this.width;
			}
			set
			{
				this.width = value;
			}
		}

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x06002497 RID: 9367 RVA: 0x00198088 File Offset: 0x00196288
		// (set) Token: 0x06002498 RID: 9368 RVA: 0x00198090 File Offset: 0x00196290
		public int Depth
		{
			get
			{
				return this.depth;
			}
			set
			{
				this.depth = value;
			}
		}

		// Token: 0x06002499 RID: 9369 RVA: 0x00198099 File Offset: 0x00196299
		public uint GetConnectionCost(int dir)
		{
			return this.neighbourCosts[dir];
		}

		// Token: 0x0600249A RID: 9370 RVA: 0x001980A4 File Offset: 0x001962A4
		public GridNode GetNodeConnection(GridNode node, int dir)
		{
			if (!node.HasConnectionInDirection(dir))
			{
				return null;
			}
			if (!node.EdgeNode)
			{
				return this.nodes[node.NodeInGridIndex + this.neighbourOffsets[dir]];
			}
			int nodeInGridIndex = node.NodeInGridIndex;
			int num = nodeInGridIndex / this.Width;
			int x = nodeInGridIndex - num * this.Width;
			return this.GetNodeConnection(nodeInGridIndex, x, num, dir);
		}

		// Token: 0x0600249B RID: 9371 RVA: 0x00198100 File Offset: 0x00196300
		public bool HasNodeConnection(GridNode node, int dir)
		{
			if (!node.HasConnectionInDirection(dir))
			{
				return false;
			}
			if (!node.EdgeNode)
			{
				return true;
			}
			int nodeInGridIndex = node.NodeInGridIndex;
			int num = nodeInGridIndex / this.Width;
			int x = nodeInGridIndex - num * this.Width;
			return this.HasNodeConnection(nodeInGridIndex, x, num, dir);
		}

		// Token: 0x0600249C RID: 9372 RVA: 0x00198148 File Offset: 0x00196348
		public void SetNodeConnection(GridNode node, int dir, bool value)
		{
			int nodeInGridIndex = node.NodeInGridIndex;
			int num = nodeInGridIndex / this.Width;
			int x = nodeInGridIndex - num * this.Width;
			this.SetNodeConnection(nodeInGridIndex, x, num, dir, value);
		}

		// Token: 0x0600249D RID: 9373 RVA: 0x0019817C File Offset: 0x0019637C
		private GridNode GetNodeConnection(int index, int x, int z, int dir)
		{
			if (!this.nodes[index].HasConnectionInDirection(dir))
			{
				return null;
			}
			int num = x + this.neighbourXOffsets[dir];
			if (num < 0 || num >= this.Width)
			{
				return null;
			}
			int num2 = z + this.neighbourZOffsets[dir];
			if (num2 < 0 || num2 >= this.Depth)
			{
				return null;
			}
			int num3 = index + this.neighbourOffsets[dir];
			return this.nodes[num3];
		}

		// Token: 0x0600249E RID: 9374 RVA: 0x001981E6 File Offset: 0x001963E6
		public void SetNodeConnection(int index, int x, int z, int dir, bool value)
		{
			this.nodes[index].SetConnectionInternal(dir, value);
		}

		// Token: 0x0600249F RID: 9375 RVA: 0x001981FC File Offset: 0x001963FC
		public bool HasNodeConnection(int index, int x, int z, int dir)
		{
			if (!this.nodes[index].HasConnectionInDirection(dir))
			{
				return false;
			}
			int num = x + this.neighbourXOffsets[dir];
			if (num < 0 || num >= this.Width)
			{
				return false;
			}
			int num2 = z + this.neighbourZOffsets[dir];
			return num2 >= 0 && num2 < this.Depth;
		}

		// Token: 0x060024A0 RID: 9376 RVA: 0x00198253 File Offset: 0x00196453
		public void SetDimensions(int width, int depth, float nodeSize)
		{
			this.unclampedSize = new Vector2((float)width, (float)depth) * nodeSize;
			this.nodeSize = nodeSize;
			this.UpdateTransform();
		}

		// Token: 0x060024A1 RID: 9377 RVA: 0x00198277 File Offset: 0x00196477
		[Obsolete("Use SetDimensions instead")]
		public void UpdateSizeFromWidthDepth()
		{
			this.SetDimensions(this.width, this.depth, this.nodeSize);
		}

		// Token: 0x060024A2 RID: 9378 RVA: 0x00198291 File Offset: 0x00196491
		[Obsolete("This method has been renamed to UpdateTransform")]
		public void GenerateMatrix()
		{
			this.UpdateTransform();
		}

		// Token: 0x060024A3 RID: 9379 RVA: 0x00198299 File Offset: 0x00196499
		public void UpdateTransform()
		{
			this.CalculateDimensions(out this.width, out this.depth, out this.nodeSize);
			this.transform = this.CalculateTransform();
		}

		// Token: 0x060024A4 RID: 9380 RVA: 0x001982C0 File Offset: 0x001964C0
		public GraphTransform CalculateTransform()
		{
			int num;
			int num2;
			float num3;
			this.CalculateDimensions(out num, out num2, out num3);
			Matrix4x4 rhs = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 45f, 0f), Vector3.one);
			rhs = Matrix4x4.Scale(new Vector3(Mathf.Cos(0.017453292f * this.isometricAngle), 1f, 1f)) * rhs;
			rhs = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, -45f, 0f), Vector3.one) * rhs;
			return new GraphTransform(Matrix4x4.TRS((Matrix4x4.TRS(this.center, Quaternion.Euler(this.rotation), new Vector3(this.aspectRatio, 1f, 1f)) * rhs).MultiplyPoint3x4(-new Vector3((float)num * num3, 0f, (float)num2 * num3) * 0.5f), Quaternion.Euler(this.rotation), new Vector3(num3 * this.aspectRatio, 1f, num3)) * rhs);
		}

		// Token: 0x060024A5 RID: 9381 RVA: 0x001983DC File Offset: 0x001965DC
		private void CalculateDimensions(out int width, out int depth, out float nodeSize)
		{
			Vector2 vector = this.unclampedSize;
			vector.x *= Mathf.Sign(vector.x);
			vector.y *= Mathf.Sign(vector.y);
			nodeSize = Mathf.Max(this.nodeSize, vector.x / 1024f);
			nodeSize = Mathf.Max(this.nodeSize, vector.y / 1024f);
			vector.x = ((vector.x < nodeSize) ? nodeSize : vector.x);
			vector.y = ((vector.y < nodeSize) ? nodeSize : vector.y);
			this.size = vector;
			width = Mathf.FloorToInt(this.size.x / nodeSize);
			depth = Mathf.FloorToInt(this.size.y / nodeSize);
			if (Mathf.Approximately(this.size.x / nodeSize, (float)Mathf.CeilToInt(this.size.x / nodeSize)))
			{
				width = Mathf.CeilToInt(this.size.x / nodeSize);
			}
			if (Mathf.Approximately(this.size.y / nodeSize, (float)Mathf.CeilToInt(this.size.y / nodeSize)))
			{
				depth = Mathf.CeilToInt(this.size.y / nodeSize);
			}
		}

		// Token: 0x060024A6 RID: 9382 RVA: 0x00198534 File Offset: 0x00196734
		public override NNInfoInternal GetNearest(Vector3 position, NNConstraint constraint, GraphNode hint)
		{
			if (this.nodes == null || this.depth * this.width != this.nodes.Length)
			{
				return default(NNInfoInternal);
			}
			position = this.transform.InverseTransform(position);
			float x = position.x;
			float z = position.z;
			int num = Mathf.Clamp((int)x, 0, this.width - 1);
			int num2 = Mathf.Clamp((int)z, 0, this.depth - 1);
			NNInfoInternal result = new NNInfoInternal(this.nodes[num2 * this.width + num]);
			float y = this.transform.InverseTransform((Vector3)this.nodes[num2 * this.width + num].position).y;
			result.clampedPosition = this.transform.Transform(new Vector3(Mathf.Clamp(x, (float)num, (float)num + 1f), y, Mathf.Clamp(z, (float)num2, (float)num2 + 1f)));
			return result;
		}

		// Token: 0x060024A7 RID: 9383 RVA: 0x0019862C File Offset: 0x0019682C
		public override NNInfoInternal GetNearestForce(Vector3 position, NNConstraint constraint)
		{
			if (this.nodes == null || this.depth * this.width != this.nodes.Length)
			{
				return default(NNInfoInternal);
			}
			Vector3 b = position;
			position = this.transform.InverseTransform(position);
			float x = position.x;
			float z = position.z;
			int num = Mathf.Clamp((int)x, 0, this.width - 1);
			int num2 = Mathf.Clamp((int)z, 0, this.depth - 1);
			GridNode gridNode = this.nodes[num + num2 * this.width];
			GridNode gridNode2 = null;
			float num3 = float.PositiveInfinity;
			int num4 = 2;
			Vector3 clampedPosition = Vector3.zero;
			NNInfoInternal result = new NNInfoInternal(null);
			if (constraint == null || constraint.Suitable(gridNode))
			{
				gridNode2 = gridNode;
				num3 = ((Vector3)gridNode2.position - b).sqrMagnitude;
				float y = this.transform.InverseTransform((Vector3)gridNode.position).y;
				clampedPosition = this.transform.Transform(new Vector3(Mathf.Clamp(x, (float)num, (float)num + 1f), y, Mathf.Clamp(z, (float)num2, (float)num2 + 1f)));
			}
			if (gridNode2 != null)
			{
				result.node = gridNode2;
				result.clampedPosition = clampedPosition;
				if (num4 == 0)
				{
					return result;
				}
				num4--;
			}
			float num5 = (constraint == null || constraint.constrainDistance) ? AstarPath.active.maxNearestNodeDistance : float.PositiveInfinity;
			float num6 = num5 * num5;
			int num7 = 1;
			while (this.nodeSize * (float)num7 <= num5)
			{
				bool flag = false;
				int i = num2 + num7;
				int num8 = i * this.width;
				int j;
				for (j = num - num7; j <= num + num7; j++)
				{
					if (j >= 0 && i >= 0 && j < this.width && i < this.depth)
					{
						flag = true;
						if (constraint == null || constraint.Suitable(this.nodes[j + num8]))
						{
							float sqrMagnitude = ((Vector3)this.nodes[j + num8].position - b).sqrMagnitude;
							if (sqrMagnitude < num3 && sqrMagnitude < num6)
							{
								num3 = sqrMagnitude;
								gridNode2 = this.nodes[j + num8];
								clampedPosition = this.transform.Transform(new Vector3(Mathf.Clamp(x, (float)j, (float)j + 1f), this.transform.InverseTransform((Vector3)gridNode2.position).y, Mathf.Clamp(z, (float)i, (float)i + 1f)));
							}
						}
					}
				}
				i = num2 - num7;
				num8 = i * this.width;
				for (j = num - num7; j <= num + num7; j++)
				{
					if (j >= 0 && i >= 0 && j < this.width && i < this.depth)
					{
						flag = true;
						if (constraint == null || constraint.Suitable(this.nodes[j + num8]))
						{
							float sqrMagnitude2 = ((Vector3)this.nodes[j + num8].position - b).sqrMagnitude;
							if (sqrMagnitude2 < num3 && sqrMagnitude2 < num6)
							{
								num3 = sqrMagnitude2;
								gridNode2 = this.nodes[j + num8];
								clampedPosition = this.transform.Transform(new Vector3(Mathf.Clamp(x, (float)j, (float)j + 1f), this.transform.InverseTransform((Vector3)gridNode2.position).y, Mathf.Clamp(z, (float)i, (float)i + 1f)));
							}
						}
					}
				}
				j = num - num7;
				for (i = num2 - num7 + 1; i <= num2 + num7 - 1; i++)
				{
					if (j >= 0 && i >= 0 && j < this.width && i < this.depth)
					{
						flag = true;
						if (constraint == null || constraint.Suitable(this.nodes[j + i * this.width]))
						{
							float sqrMagnitude3 = ((Vector3)this.nodes[j + i * this.width].position - b).sqrMagnitude;
							if (sqrMagnitude3 < num3 && sqrMagnitude3 < num6)
							{
								num3 = sqrMagnitude3;
								gridNode2 = this.nodes[j + i * this.width];
								clampedPosition = this.transform.Transform(new Vector3(Mathf.Clamp(x, (float)j, (float)j + 1f), this.transform.InverseTransform((Vector3)gridNode2.position).y, Mathf.Clamp(z, (float)i, (float)i + 1f)));
							}
						}
					}
				}
				j = num + num7;
				for (i = num2 - num7 + 1; i <= num2 + num7 - 1; i++)
				{
					if (j >= 0 && i >= 0 && j < this.width && i < this.depth)
					{
						flag = true;
						if (constraint == null || constraint.Suitable(this.nodes[j + i * this.width]))
						{
							float sqrMagnitude4 = ((Vector3)this.nodes[j + i * this.width].position - b).sqrMagnitude;
							if (sqrMagnitude4 < num3 && sqrMagnitude4 < num6)
							{
								num3 = sqrMagnitude4;
								gridNode2 = this.nodes[j + i * this.width];
								clampedPosition = this.transform.Transform(new Vector3(Mathf.Clamp(x, (float)j, (float)j + 1f), this.transform.InverseTransform((Vector3)gridNode2.position).y, Mathf.Clamp(z, (float)i, (float)i + 1f)));
							}
						}
					}
				}
				if (gridNode2 != null)
				{
					if (num4 == 0)
					{
						break;
					}
					num4--;
				}
				if (!flag)
				{
					break;
				}
				num7++;
			}
			result.node = gridNode2;
			result.clampedPosition = clampedPosition;
			return result;
		}

		// Token: 0x060024A8 RID: 9384 RVA: 0x00198C34 File Offset: 0x00196E34
		public virtual void SetUpOffsetsAndCosts()
		{
			this.neighbourOffsets[0] = -this.width;
			this.neighbourOffsets[1] = 1;
			this.neighbourOffsets[2] = this.width;
			this.neighbourOffsets[3] = -1;
			this.neighbourOffsets[4] = -this.width + 1;
			this.neighbourOffsets[5] = this.width + 1;
			this.neighbourOffsets[6] = this.width - 1;
			this.neighbourOffsets[7] = -this.width - 1;
			uint num = (uint)Mathf.RoundToInt(this.nodeSize * 1000f);
			uint num2 = this.uniformEdgeCosts ? num : ((uint)Mathf.RoundToInt(this.nodeSize * Mathf.Sqrt(2f) * 1000f));
			this.neighbourCosts[0] = num;
			this.neighbourCosts[1] = num;
			this.neighbourCosts[2] = num;
			this.neighbourCosts[3] = num;
			this.neighbourCosts[4] = num2;
			this.neighbourCosts[5] = num2;
			this.neighbourCosts[6] = num2;
			this.neighbourCosts[7] = num2;
			this.neighbourXOffsets[0] = 0;
			this.neighbourXOffsets[1] = 1;
			this.neighbourXOffsets[2] = 0;
			this.neighbourXOffsets[3] = -1;
			this.neighbourXOffsets[4] = 1;
			this.neighbourXOffsets[5] = 1;
			this.neighbourXOffsets[6] = -1;
			this.neighbourXOffsets[7] = -1;
			this.neighbourZOffsets[0] = -1;
			this.neighbourZOffsets[1] = 0;
			this.neighbourZOffsets[2] = 1;
			this.neighbourZOffsets[3] = 0;
			this.neighbourZOffsets[4] = -1;
			this.neighbourZOffsets[5] = 1;
			this.neighbourZOffsets[6] = 1;
			this.neighbourZOffsets[7] = -1;
		}

		// Token: 0x060024A9 RID: 9385 RVA: 0x00198DC4 File Offset: 0x00196FC4
		protected override IEnumerable<Progress> ScanInternal()
		{
			if (this.nodeSize <= 0f)
			{
				yield break;
			}
			this.UpdateTransform();
			if (this.width > 1024 || this.depth > 1024)
			{
				Debug.LogError("One of the grid's sides is longer than 1024 nodes");
				yield break;
			}
			if (this.useJumpPointSearch)
			{
				Debug.LogError("Trying to use Jump Point Search, but support for it is not enabled. Please enable it in the inspector (Grid Graph settings).");
			}
			this.SetUpOffsetsAndCosts();
			GridNode.SetGridGraph((int)this.graphIndex, this);
			yield return new Progress(0.05f, "Creating nodes");
			this.nodes = new GridNode[this.width * this.depth];
			for (int i = 0; i < this.depth; i++)
			{
				for (int j = 0; j < this.width; j++)
				{
					int num = i * this.width + j;
					GridNode gridNode = this.nodes[num] = new GridNode(this.active);
					gridNode.GraphIndex = this.graphIndex;
					gridNode.NodeInGridIndex = num;
				}
			}
			if (this.collision == null)
			{
				this.collision = new GraphCollision();
			}
			this.collision.Initialize(this.transform, this.nodeSize);
			this.textureData.Initialize();
			int progressCounter = 0;
			int num2;
			for (int z = 0; z < this.depth; z = num2 + 1)
			{
				if (progressCounter >= 1000)
				{
					progressCounter = 0;
					yield return new Progress(Mathf.Lerp(0.1f, 0.7f, (float)z / (float)this.depth), "Calculating positions");
				}
				progressCounter += this.width;
				for (int k = 0; k < this.width; k++)
				{
					this.RecalculateCell(k, z, true, true);
					this.textureData.Apply(this.nodes[z * this.width + k], k, z);
				}
				num2 = z;
			}
			progressCounter = 0;
			for (int z = 0; z < this.depth; z = num2 + 1)
			{
				if (progressCounter >= 1000)
				{
					progressCounter = 0;
					yield return new Progress(Mathf.Lerp(0.7f, 0.9f, (float)z / (float)this.depth), "Calculating connections");
				}
				progressCounter += this.width;
				for (int l = 0; l < this.width; l++)
				{
					this.CalculateConnections(l, z);
				}
				num2 = z;
			}
			yield return new Progress(0.95f, "Calculating erosion");
			this.ErodeWalkableArea();
			yield break;
		}

		// Token: 0x060024AA RID: 9386 RVA: 0x00198DD4 File Offset: 0x00196FD4
		[Obsolete("Use RecalculateCell instead which works both for grid graphs and layered grid graphs")]
		public virtual void UpdateNodePositionCollision(GridNode node, int x, int z, bool resetPenalty = true)
		{
			this.RecalculateCell(x, z, resetPenalty, false);
		}

		// Token: 0x060024AB RID: 9387 RVA: 0x00198DE4 File Offset: 0x00196FE4
		public virtual void RecalculateCell(int x, int z, bool resetPenalties = true, bool resetTags = true)
		{
			GridNode gridNode = this.nodes[z * this.width + x];
			gridNode.position = this.GraphPointToWorld(x, z, 0f);
			RaycastHit raycastHit;
			bool flag;
			Vector3 ob = this.collision.CheckHeight((Vector3)gridNode.position, out raycastHit, out flag);
			gridNode.position = (Int3)ob;
			if (resetPenalties)
			{
				gridNode.Penalty = this.initialPenalty;
				if (this.penaltyPosition)
				{
					gridNode.Penalty += (uint)Mathf.RoundToInt(((float)gridNode.position.y - this.penaltyPositionOffset) * this.penaltyPositionFactor);
				}
			}
			if (resetTags)
			{
				gridNode.Tag = 0u;
			}
			if (flag && this.useRaycastNormal && this.collision.heightCheck && raycastHit.normal != Vector3.zero)
			{
				float num = Vector3.Dot(raycastHit.normal.normalized, this.collision.up);
				if (this.penaltyAngle && resetPenalties)
				{
					gridNode.Penalty += (uint)Mathf.RoundToInt((1f - Mathf.Pow(num, this.penaltyAnglePower)) * this.penaltyAngleFactor);
				}
				float num2 = Mathf.Cos(this.maxSlope * 0.017453292f);
				if (num < num2)
				{
					flag = false;
				}
			}
			gridNode.Walkable = (flag && this.collision.Check((Vector3)gridNode.position));
			gridNode.WalkableErosion = gridNode.Walkable;
		}

		// Token: 0x060024AC RID: 9388 RVA: 0x00198F60 File Offset: 0x00197160
		protected virtual bool ErosionAnyFalseConnections(GraphNode baseNode)
		{
			GridNode node = baseNode as GridNode;
			if (this.neighbours == NumNeighbours.Six)
			{
				for (int i = 0; i < 6; i++)
				{
					if (!this.HasNodeConnection(node, GridGraph.hexagonNeighbourIndices[i]))
					{
						return true;
					}
				}
			}
			else
			{
				for (int j = 0; j < 4; j++)
				{
					if (!this.HasNodeConnection(node, j))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060024AD RID: 9389 RVA: 0x00198FB6 File Offset: 0x001971B6
		private void ErodeNode(GraphNode node)
		{
			if (node.Walkable && this.ErosionAnyFalseConnections(node))
			{
				node.Walkable = false;
			}
		}

		// Token: 0x060024AE RID: 9390 RVA: 0x00198FD0 File Offset: 0x001971D0
		private void ErodeNodeWithTagsInit(GraphNode node)
		{
			if (node.Walkable && this.ErosionAnyFalseConnections(node))
			{
				node.Tag = (uint)this.erosionFirstTag;
				return;
			}
			node.Tag = 0u;
		}

		// Token: 0x060024AF RID: 9391 RVA: 0x00198FF8 File Offset: 0x001971F8
		private void ErodeNodeWithTags(GraphNode node, int iteration)
		{
			GridNodeBase gridNodeBase = node as GridNodeBase;
			if (gridNodeBase.Walkable && (ulong)gridNodeBase.Tag >= (ulong)((long)this.erosionFirstTag) && (ulong)gridNodeBase.Tag < (ulong)((long)(this.erosionFirstTag + iteration)))
			{
				if (this.neighbours == NumNeighbours.Six)
				{
					for (int i = 0; i < 6; i++)
					{
						GridNodeBase neighbourAlongDirection = gridNodeBase.GetNeighbourAlongDirection(GridGraph.hexagonNeighbourIndices[i]);
						if (neighbourAlongDirection != null)
						{
							uint tag = neighbourAlongDirection.Tag;
							if ((ulong)tag > (ulong)((long)(this.erosionFirstTag + iteration)) || (ulong)tag < (ulong)((long)this.erosionFirstTag))
							{
								neighbourAlongDirection.Tag = (uint)(this.erosionFirstTag + iteration);
							}
						}
					}
					return;
				}
				for (int j = 0; j < 4; j++)
				{
					GridNodeBase neighbourAlongDirection2 = gridNodeBase.GetNeighbourAlongDirection(j);
					if (neighbourAlongDirection2 != null)
					{
						uint tag2 = neighbourAlongDirection2.Tag;
						if ((ulong)tag2 > (ulong)((long)(this.erosionFirstTag + iteration)) || (ulong)tag2 < (ulong)((long)this.erosionFirstTag))
						{
							neighbourAlongDirection2.Tag = (uint)(this.erosionFirstTag + iteration);
						}
					}
				}
			}
		}

		// Token: 0x060024B0 RID: 9392 RVA: 0x001990E3 File Offset: 0x001972E3
		public virtual void ErodeWalkableArea()
		{
			this.ErodeWalkableArea(0, 0, this.Width, this.Depth);
		}

		// Token: 0x060024B1 RID: 9393 RVA: 0x001990FC File Offset: 0x001972FC
		public void ErodeWalkableArea(int xmin, int zmin, int xmax, int zmax)
		{
			if (this.erosionUseTags)
			{
				if (this.erodeIterations + this.erosionFirstTag > 31)
				{
					Debug.LogError(string.Concat(new object[]
					{
						"Too few tags available for ",
						this.erodeIterations,
						" erode iterations and starting with tag ",
						this.erosionFirstTag,
						" (erodeIterations+erosionFirstTag > 31)"
					}), this.active);
					return;
				}
				if (this.erosionFirstTag <= 0)
				{
					Debug.LogError("First erosion tag must be greater or equal to 1", this.active);
					return;
				}
			}
			if (this.erodeIterations == 0)
			{
				return;
			}
			IntRect rect = new IntRect(xmin, zmin, xmax - 1, zmax - 1);
			List<GraphNode> nodesInRegion = this.GetNodesInRegion(rect);
			int count = nodesInRegion.Count;
			for (int i = 0; i < this.erodeIterations; i++)
			{
				if (this.erosionUseTags)
				{
					if (i == 0)
					{
						for (int j = 0; j < count; j++)
						{
							this.ErodeNodeWithTagsInit(nodesInRegion[j]);
						}
					}
					else
					{
						for (int k = 0; k < count; k++)
						{
							this.ErodeNodeWithTags(nodesInRegion[k], i);
						}
					}
				}
				else
				{
					for (int l = 0; l < count; l++)
					{
						this.ErodeNode(nodesInRegion[l]);
					}
					for (int m = 0; m < count; m++)
					{
						this.CalculateConnections(nodesInRegion[m] as GridNodeBase);
					}
				}
			}
			ListPool<GraphNode>.Release(ref nodesInRegion);
		}

		// Token: 0x060024B2 RID: 9394 RVA: 0x0019925C File Offset: 0x0019745C
		public virtual bool IsValidConnection(GridNodeBase node1, GridNodeBase node2)
		{
			if (!node1.Walkable || !node2.Walkable)
			{
				return false;
			}
			if (this.maxClimb <= 0f || this.collision.use2D)
			{
				return true;
			}
			if (this.transform.onlyTranslational)
			{
				return (float)Math.Abs(node1.position.y - node2.position.y) <= this.maxClimb * 1000f;
			}
			Vector3 vector = (Vector3)node1.position;
			Vector3 rhs = (Vector3)node2.position;
			Vector3 lhs = this.transform.WorldUpAtGraphPosition(vector);
			return Math.Abs(Vector3.Dot(lhs, vector) - Vector3.Dot(lhs, rhs)) <= this.maxClimb;
		}

		// Token: 0x060024B3 RID: 9395 RVA: 0x00199318 File Offset: 0x00197518
		public void CalculateConnectionsForCellAndNeighbours(int x, int z)
		{
			this.CalculateConnections(x, z);
			for (int i = 0; i < 8; i++)
			{
				int x2 = x + this.neighbourXOffsets[i];
				int z2 = z + this.neighbourZOffsets[i];
				this.CalculateConnections(x2, z2);
			}
		}

		// Token: 0x060024B4 RID: 9396 RVA: 0x00199357 File Offset: 0x00197557
		[Obsolete("Use the instance function instead")]
		public static void CalculateConnections(GridNode node)
		{
			(AstarData.GetGraph(node) as GridGraph).CalculateConnections(node);
		}

		// Token: 0x060024B5 RID: 9397 RVA: 0x0019936C File Offset: 0x0019756C
		public virtual void CalculateConnections(GridNodeBase node)
		{
			int nodeInGridIndex = node.NodeInGridIndex;
			int x = nodeInGridIndex % this.width;
			int z = nodeInGridIndex / this.width;
			this.CalculateConnections(x, z);
		}

		// Token: 0x060024B6 RID: 9398 RVA: 0x00199398 File Offset: 0x00197598
		[Obsolete("CalculateConnections no longer takes a node array, it just uses the one on the graph")]
		public virtual void CalculateConnections(GridNode[] nodes, int x, int z, GridNode node)
		{
			this.CalculateConnections(x, z);
		}

		// Token: 0x060024B7 RID: 9399 RVA: 0x001993A2 File Offset: 0x001975A2
		[Obsolete("Use CalculateConnections(x,z) or CalculateConnections(node) instead")]
		public virtual void CalculateConnections(int x, int z, GridNode node)
		{
			this.CalculateConnections(x, z);
		}

		// Token: 0x060024B8 RID: 9400 RVA: 0x001993AC File Offset: 0x001975AC
		public virtual void CalculateConnections(int x, int z)
		{
			GridNode gridNode = this.nodes[z * this.width + x];
			if (!gridNode.Walkable)
			{
				gridNode.ResetConnectionsInternal();
				return;
			}
			int nodeInGridIndex = gridNode.NodeInGridIndex;
			if (this.neighbours == NumNeighbours.Four || this.neighbours == NumNeighbours.Eight)
			{
				int num = 0;
				for (int i = 0; i < 4; i++)
				{
					int num2 = x + this.neighbourXOffsets[i];
					int num3 = z + this.neighbourZOffsets[i];
					if (num2 >= 0 & num3 >= 0 & num2 < this.width & num3 < this.depth)
					{
						GridNode node = this.nodes[nodeInGridIndex + this.neighbourOffsets[i]];
						if (this.IsValidConnection(gridNode, node))
						{
							num |= 1 << i;
						}
					}
				}
				int num4 = 0;
				if (this.neighbours == NumNeighbours.Eight)
				{
					if (this.cutCorners)
					{
						for (int j = 0; j < 4; j++)
						{
							if (((num >> j | num >> j + 1 | num >> j + 1 - 4) & 1) != 0)
							{
								int num5 = j + 4;
								int num6 = x + this.neighbourXOffsets[num5];
								int num7 = z + this.neighbourZOffsets[num5];
								if (num6 >= 0 & num7 >= 0 & num6 < this.width & num7 < this.depth)
								{
									GridNode node2 = this.nodes[nodeInGridIndex + this.neighbourOffsets[num5]];
									if (this.IsValidConnection(gridNode, node2))
									{
										num4 |= 1 << num5;
									}
								}
							}
						}
					}
					else
					{
						for (int k = 0; k < 4; k++)
						{
							if ((num >> k & 1) != 0 && ((num >> k + 1 | num >> k + 1 - 4) & 1) != 0)
							{
								GridNode node3 = this.nodes[nodeInGridIndex + this.neighbourOffsets[k + 4]];
								if (this.IsValidConnection(gridNode, node3))
								{
									num4 |= 1 << k + 4;
								}
							}
						}
					}
				}
				gridNode.SetAllConnectionInternal(num | num4);
				return;
			}
			gridNode.ResetConnectionsInternal();
			for (int l = 0; l < GridGraph.hexagonNeighbourIndices.Length; l++)
			{
				int num8 = GridGraph.hexagonNeighbourIndices[l];
				int num9 = x + this.neighbourXOffsets[num8];
				int num10 = z + this.neighbourZOffsets[num8];
				if (num9 >= 0 & num10 >= 0 & num9 < this.width & num10 < this.depth)
				{
					GridNode node4 = this.nodes[nodeInGridIndex + this.neighbourOffsets[num8]];
					gridNode.SetConnectionInternal(num8, this.IsValidConnection(gridNode, node4));
				}
			}
		}

		// Token: 0x060024B9 RID: 9401 RVA: 0x00199634 File Offset: 0x00197834
		public override void OnDrawGizmos(RetainedGizmos gizmos, bool drawNodes)
		{
			using (GraphGizmoHelper singleFrameGizmoHelper = gizmos.GetSingleFrameGizmoHelper(this.active))
			{
				int num;
				int num2;
				float num3;
				this.CalculateDimensions(out num, out num2, out num3);
				Bounds bounds = default(Bounds);
				bounds.SetMinMax(Vector3.zero, new Vector3((float)num, 0f, (float)num2));
				GraphTransform graphTransform = this.CalculateTransform();
				singleFrameGizmoHelper.builder.DrawWireCube(graphTransform, bounds, Color.white);
				int num4 = (this.nodes != null) ? this.nodes.Length : -1;
				if (this is LayerGridGraph)
				{
					num4 = (((this as LayerGridGraph).nodes != null) ? (this as LayerGridGraph).nodes.Length : -1);
				}
				if (drawNodes && this.width * this.depth * this.LayerCount != num4)
				{
					Color color = new Color(1f, 1f, 1f, 0.2f);
					for (int i = 0; i < num2; i++)
					{
						singleFrameGizmoHelper.builder.DrawLine(graphTransform.Transform(new Vector3(0f, 0f, (float)i)), graphTransform.Transform(new Vector3((float)num, 0f, (float)i)), color);
					}
					for (int j = 0; j < num; j++)
					{
						singleFrameGizmoHelper.builder.DrawLine(graphTransform.Transform(new Vector3((float)j, 0f, 0f)), graphTransform.Transform(new Vector3((float)j, 0f, (float)num2)), color);
					}
				}
			}
			if (!drawNodes)
			{
				return;
			}
			GridNodeBase[] array = ArrayPool<GridNodeBase>.Claim(1024 * this.LayerCount);
			for (int k = this.width / 32; k >= 0; k--)
			{
				for (int l = this.depth / 32; l >= 0; l--)
				{
					int nodesInRegion = this.GetNodesInRegion(new IntRect(k * 32, l * 32, (k + 1) * 32 - 1, (l + 1) * 32 - 1), array);
					RetainedGizmos.Hasher hasher = new RetainedGizmos.Hasher(this.active);
					hasher.AddHash(this.showMeshOutline ? 1 : 0);
					hasher.AddHash(this.showMeshSurface ? 1 : 0);
					hasher.AddHash(this.showNodeConnections ? 1 : 0);
					for (int m = 0; m < nodesInRegion; m++)
					{
						hasher.HashNode(array[m]);
					}
					if (!gizmos.Draw(hasher))
					{
						using (GraphGizmoHelper gizmoHelper = gizmos.GetGizmoHelper(this.active, hasher))
						{
							if (this.showNodeConnections)
							{
								for (int n = 0; n < nodesInRegion; n++)
								{
									if (array[n].Walkable)
									{
										gizmoHelper.DrawConnections(array[n]);
									}
								}
							}
							if (this.showMeshSurface || this.showMeshOutline)
							{
								this.CreateNavmeshSurfaceVisualization(array, nodesInRegion, gizmoHelper);
							}
						}
					}
				}
			}
			ArrayPool<GridNodeBase>.Release(ref array, false);
			if (this.active.showUnwalkableNodes)
			{
				base.DrawUnwalkableNodes(this.nodeSize * 0.3f);
			}
		}

		// Token: 0x060024BA RID: 9402 RVA: 0x00199960 File Offset: 0x00197B60
		private void CreateNavmeshSurfaceVisualization(GridNodeBase[] nodes, int nodeCount, GraphGizmoHelper helper)
		{
			int num = 0;
			for (int i = 0; i < nodeCount; i++)
			{
				if (nodes[i].Walkable)
				{
					num++;
				}
			}
			int[] array;
			if (this.neighbours != NumNeighbours.Six)
			{
				RuntimeHelpers.InitializeArray(array = new int[4], fieldof(<PrivateImplementationDetails>.02E4414E7DFA0F3AA2387EE8EA7AB31431CB406A).FieldHandle);
			}
			else
			{
				array = GridGraph.hexagonNeighbourIndices;
			}
			int[] array2 = array;
			float num2 = (this.neighbours == NumNeighbours.Six) ? 0.333333f : 0.5f;
			int num3 = array2.Length - 2;
			int num4 = 3 * num3;
			Vector3[] array3 = ArrayPool<Vector3>.Claim(num * num4);
			Color[] array4 = ArrayPool<Color>.Claim(num * num4);
			int num5 = 0;
			for (int j = 0; j < nodeCount; j++)
			{
				GridNodeBase gridNodeBase = nodes[j];
				if (gridNodeBase.Walkable)
				{
					Color color = helper.NodeColor(gridNodeBase);
					if (color.a > 0.001f)
					{
						for (int k = 0; k < array2.Length; k++)
						{
							int num6 = array2[k];
							int num7 = array2[(k + 1) % array2.Length];
							GridNodeBase gridNodeBase2 = null;
							GridNodeBase neighbourAlongDirection = gridNodeBase.GetNeighbourAlongDirection(num6);
							if (neighbourAlongDirection != null && this.neighbours != NumNeighbours.Six)
							{
								gridNodeBase2 = neighbourAlongDirection.GetNeighbourAlongDirection(num7);
							}
							GridNodeBase neighbourAlongDirection2 = gridNodeBase.GetNeighbourAlongDirection(num7);
							if (neighbourAlongDirection2 != null && gridNodeBase2 == null && this.neighbours != NumNeighbours.Six)
							{
								gridNodeBase2 = neighbourAlongDirection2.GetNeighbourAlongDirection(num6);
							}
							Vector3 vector = new Vector3((float)gridNodeBase.XCoordinateInGrid + 0.5f, 0f, (float)gridNodeBase.ZCoordinateInGrid + 0.5f);
							vector.x += (float)(this.neighbourXOffsets[num6] + this.neighbourXOffsets[num7]) * num2;
							vector.z += (float)(this.neighbourZOffsets[num6] + this.neighbourZOffsets[num7]) * num2;
							vector.y += this.transform.InverseTransform((Vector3)gridNodeBase.position).y;
							if (neighbourAlongDirection != null)
							{
								vector.y += this.transform.InverseTransform((Vector3)neighbourAlongDirection.position).y;
							}
							if (neighbourAlongDirection2 != null)
							{
								vector.y += this.transform.InverseTransform((Vector3)neighbourAlongDirection2.position).y;
							}
							if (gridNodeBase2 != null)
							{
								vector.y += this.transform.InverseTransform((Vector3)gridNodeBase2.position).y;
							}
							vector.y /= 1f + ((neighbourAlongDirection != null) ? 1f : 0f) + ((neighbourAlongDirection2 != null) ? 1f : 0f) + ((gridNodeBase2 != null) ? 1f : 0f);
							vector = this.transform.Transform(vector);
							array3[num5 + k] = vector;
						}
						if (this.neighbours == NumNeighbours.Six)
						{
							array3[num5 + 6] = array3[num5];
							array3[num5 + 7] = array3[num5 + 2];
							array3[num5 + 8] = array3[num5 + 3];
							array3[num5 + 9] = array3[num5];
							array3[num5 + 10] = array3[num5 + 3];
							array3[num5 + 11] = array3[num5 + 5];
						}
						else
						{
							array3[num5 + 4] = array3[num5];
							array3[num5 + 5] = array3[num5 + 2];
						}
						for (int l = 0; l < num4; l++)
						{
							array4[num5 + l] = color;
						}
						for (int m = 0; m < array2.Length; m++)
						{
							GridNodeBase neighbourAlongDirection3 = gridNodeBase.GetNeighbourAlongDirection(array2[(m + 1) % array2.Length]);
							if (neighbourAlongDirection3 == null || (this.showMeshOutline && gridNodeBase.NodeInGridIndex < neighbourAlongDirection3.NodeInGridIndex))
							{
								helper.builder.DrawLine(array3[num5 + m], array3[num5 + (m + 1) % array2.Length], (neighbourAlongDirection3 == null) ? Color.black : color);
							}
						}
						num5 += num4;
					}
				}
			}
			if (this.showMeshSurface)
			{
				helper.DrawTriangles(array3, array4, num5 * num3 / num4);
			}
			ArrayPool<Vector3>.Release(ref array3, false);
			ArrayPool<Color>.Release(ref array4, false);
		}

		// Token: 0x060024BB RID: 9403 RVA: 0x00199D98 File Offset: 0x00197F98
		protected IntRect GetRectFromBounds(Bounds bounds)
		{
			bounds = this.transform.InverseTransform(bounds);
			Vector3 min = bounds.min;
			Vector3 max = bounds.max;
			int xmin = Mathf.RoundToInt(min.x - 0.5f);
			int xmax = Mathf.RoundToInt(max.x - 0.5f);
			int ymin = Mathf.RoundToInt(min.z - 0.5f);
			int ymax = Mathf.RoundToInt(max.z - 0.5f);
			IntRect a = new IntRect(xmin, ymin, xmax, ymax);
			IntRect b = new IntRect(0, 0, this.width - 1, this.depth - 1);
			return IntRect.Intersection(a, b);
		}

		// Token: 0x060024BC RID: 9404 RVA: 0x00199E34 File Offset: 0x00198034
		[Obsolete("This method has been renamed to GetNodesInRegion", true)]
		public List<GraphNode> GetNodesInArea(Bounds bounds)
		{
			return this.GetNodesInRegion(bounds);
		}

		// Token: 0x060024BD RID: 9405 RVA: 0x00199E3D File Offset: 0x0019803D
		[Obsolete("This method has been renamed to GetNodesInRegion", true)]
		public List<GraphNode> GetNodesInArea(GraphUpdateShape shape)
		{
			return this.GetNodesInRegion(shape);
		}

		// Token: 0x060024BE RID: 9406 RVA: 0x00199E46 File Offset: 0x00198046
		[Obsolete("This method has been renamed to GetNodesInRegion", true)]
		public List<GraphNode> GetNodesInArea(Bounds bounds, GraphUpdateShape shape)
		{
			return this.GetNodesInRegion(bounds, shape);
		}

		// Token: 0x060024BF RID: 9407 RVA: 0x00199E50 File Offset: 0x00198050
		public List<GraphNode> GetNodesInRegion(Bounds bounds)
		{
			return this.GetNodesInRegion(bounds, null);
		}

		// Token: 0x060024C0 RID: 9408 RVA: 0x00199E5A File Offset: 0x0019805A
		public List<GraphNode> GetNodesInRegion(GraphUpdateShape shape)
		{
			return this.GetNodesInRegion(shape.GetBounds(), shape);
		}

		// Token: 0x060024C1 RID: 9409 RVA: 0x00199E6C File Offset: 0x0019806C
		protected virtual List<GraphNode> GetNodesInRegion(Bounds bounds, GraphUpdateShape shape)
		{
			IntRect rectFromBounds = this.GetRectFromBounds(bounds);
			if (this.nodes == null || !rectFromBounds.IsValid() || this.nodes.Length != this.width * this.depth)
			{
				return ListPool<GraphNode>.Claim();
			}
			List<GraphNode> list = ListPool<GraphNode>.Claim(rectFromBounds.Width * rectFromBounds.Height);
			for (int i = rectFromBounds.xmin; i <= rectFromBounds.xmax; i++)
			{
				for (int j = rectFromBounds.ymin; j <= rectFromBounds.ymax; j++)
				{
					int num = j * this.width + i;
					GraphNode graphNode = this.nodes[num];
					if (bounds.Contains((Vector3)graphNode.position) && (shape == null || shape.Contains((Vector3)graphNode.position)))
					{
						list.Add(graphNode);
					}
				}
			}
			return list;
		}

		// Token: 0x060024C2 RID: 9410 RVA: 0x00199F3C File Offset: 0x0019813C
		public virtual List<GraphNode> GetNodesInRegion(IntRect rect)
		{
			IntRect b = new IntRect(0, 0, this.width - 1, this.depth - 1);
			rect = IntRect.Intersection(rect, b);
			if (this.nodes == null || !rect.IsValid() || this.nodes.Length != this.width * this.depth)
			{
				return ListPool<GraphNode>.Claim(0);
			}
			List<GraphNode> list = ListPool<GraphNode>.Claim(rect.Width * rect.Height);
			for (int i = rect.ymin; i <= rect.ymax; i++)
			{
				int num = i * this.Width;
				for (int j = rect.xmin; j <= rect.xmax; j++)
				{
					list.Add(this.nodes[num + j]);
				}
			}
			return list;
		}

		// Token: 0x060024C3 RID: 9411 RVA: 0x00199FFC File Offset: 0x001981FC
		public virtual int GetNodesInRegion(IntRect rect, GridNodeBase[] buffer)
		{
			IntRect b = new IntRect(0, 0, this.width - 1, this.depth - 1);
			rect = IntRect.Intersection(rect, b);
			if (this.nodes == null || !rect.IsValid() || this.nodes.Length != this.width * this.depth)
			{
				return 0;
			}
			if (buffer.Length < rect.Width * rect.Height)
			{
				throw new ArgumentException("Buffer is too small");
			}
			int num = 0;
			int i = rect.ymin;
			while (i <= rect.ymax)
			{
				Array.Copy(this.nodes, i * this.Width + rect.xmin, buffer, num, rect.Width);
				i++;
				num += rect.Width;
			}
			return num;
		}

		// Token: 0x060024C4 RID: 9412 RVA: 0x0019A0BA File Offset: 0x001982BA
		public virtual GridNodeBase GetNode(int x, int z)
		{
			if (x < 0 || z < 0 || x >= this.width || z >= this.depth)
			{
				return null;
			}
			return this.nodes[x + z * this.width];
		}

		// Token: 0x060024C5 RID: 9413 RVA: 0x0002D171 File Offset: 0x0002B371
		GraphUpdateThreading IUpdatableGraph.CanUpdateAsync(GraphUpdateObject o)
		{
			return GraphUpdateThreading.UnityThread;
		}

		// Token: 0x060024C6 RID: 9414 RVA: 0x00002ACE File Offset: 0x00000CCE
		void IUpdatableGraph.UpdateAreaInit(GraphUpdateObject o)
		{
		}

		// Token: 0x060024C7 RID: 9415 RVA: 0x00002ACE File Offset: 0x00000CCE
		void IUpdatableGraph.UpdateAreaPost(GraphUpdateObject o)
		{
		}

		// Token: 0x060024C8 RID: 9416 RVA: 0x0019A0EC File Offset: 0x001982EC
		protected void CalculateAffectedRegions(GraphUpdateObject o, out IntRect originalRect, out IntRect affectRect, out IntRect physicsRect, out bool willChangeWalkability, out int erosion)
		{
			Bounds bounds = this.transform.InverseTransform(o.bounds);
			Vector3 vector = bounds.min;
			Vector3 vector2 = bounds.max;
			int xmin = Mathf.RoundToInt(vector.x - 0.5f);
			int xmax = Mathf.RoundToInt(vector2.x - 0.5f);
			int ymin = Mathf.RoundToInt(vector.z - 0.5f);
			int ymax = Mathf.RoundToInt(vector2.z - 0.5f);
			originalRect = new IntRect(xmin, ymin, xmax, ymax);
			affectRect = originalRect;
			physicsRect = originalRect;
			erosion = (o.updateErosion ? this.erodeIterations : 0);
			willChangeWalkability = (o.updatePhysics || o.modifyWalkability);
			if (o.updatePhysics && !o.modifyWalkability && this.collision.collisionCheck)
			{
				Vector3 a = new Vector3(this.collision.diameter, 0f, this.collision.diameter) * 0.5f;
				vector -= a * 1.02f;
				vector2 += a * 1.02f;
				physicsRect = new IntRect(Mathf.RoundToInt(vector.x - 0.5f), Mathf.RoundToInt(vector.z - 0.5f), Mathf.RoundToInt(vector2.x - 0.5f), Mathf.RoundToInt(vector2.z - 0.5f));
				affectRect = IntRect.Union(physicsRect, affectRect);
			}
			if (willChangeWalkability || erosion > 0)
			{
				affectRect = affectRect.Expand(erosion + 1);
			}
		}

		// Token: 0x060024C9 RID: 9417 RVA: 0x0019A2B8 File Offset: 0x001984B8
		void IUpdatableGraph.UpdateArea(GraphUpdateObject o)
		{
			if (this.nodes == null || this.nodes.Length != this.width * this.depth)
			{
				Debug.LogWarning("The Grid Graph is not scanned, cannot update area");
				return;
			}
			IntRect a;
			IntRect a2;
			IntRect intRect;
			bool flag;
			int num;
			this.CalculateAffectedRegions(o, out a, out a2, out intRect, out flag, out num);
			IntRect b = new IntRect(0, 0, this.width - 1, this.depth - 1);
			IntRect intRect2 = IntRect.Intersection(a2, b);
			for (int i = intRect2.xmin; i <= intRect2.xmax; i++)
			{
				for (int j = intRect2.ymin; j <= intRect2.ymax; j++)
				{
					o.WillUpdateNode(this.nodes[j * this.width + i]);
				}
			}
			if (o.updatePhysics && !o.modifyWalkability)
			{
				this.collision.Initialize(this.transform, this.nodeSize);
				intRect2 = IntRect.Intersection(intRect, b);
				for (int k = intRect2.xmin; k <= intRect2.xmax; k++)
				{
					for (int l = intRect2.ymin; l <= intRect2.ymax; l++)
					{
						this.RecalculateCell(k, l, o.resetPenaltyOnPhysics, false);
					}
				}
			}
			intRect2 = IntRect.Intersection(a, b);
			for (int m = intRect2.xmin; m <= intRect2.xmax; m++)
			{
				for (int n = intRect2.ymin; n <= intRect2.ymax; n++)
				{
					int num2 = n * this.width + m;
					GridNode gridNode = this.nodes[num2];
					if (flag)
					{
						gridNode.Walkable = gridNode.WalkableErosion;
						if (o.bounds.Contains((Vector3)gridNode.position))
						{
							o.Apply(gridNode);
						}
						gridNode.WalkableErosion = gridNode.Walkable;
					}
					else if (o.bounds.Contains((Vector3)gridNode.position))
					{
						o.Apply(gridNode);
					}
				}
			}
			if (flag && num == 0)
			{
				intRect2 = IntRect.Intersection(a2, b);
				for (int num3 = intRect2.xmin; num3 <= intRect2.xmax; num3++)
				{
					for (int num4 = intRect2.ymin; num4 <= intRect2.ymax; num4++)
					{
						this.CalculateConnections(num3, num4);
					}
				}
				return;
			}
			if (flag && num > 0)
			{
				IntRect a3 = IntRect.Union(a, intRect).Expand(num);
				IntRect intRect3 = a3.Expand(num);
				a3 = IntRect.Intersection(a3, b);
				intRect3 = IntRect.Intersection(intRect3, b);
				for (int num5 = intRect3.xmin; num5 <= intRect3.xmax; num5++)
				{
					for (int num6 = intRect3.ymin; num6 <= intRect3.ymax; num6++)
					{
						int num7 = num6 * this.width + num5;
						GridNode gridNode2 = this.nodes[num7];
						bool walkable = gridNode2.Walkable;
						gridNode2.Walkable = gridNode2.WalkableErosion;
						if (!a3.Contains(num5, num6))
						{
							gridNode2.TmpWalkable = walkable;
						}
					}
				}
				for (int num8 = intRect3.xmin; num8 <= intRect3.xmax; num8++)
				{
					for (int num9 = intRect3.ymin; num9 <= intRect3.ymax; num9++)
					{
						this.CalculateConnections(num8, num9);
					}
				}
				this.ErodeWalkableArea(intRect3.xmin, intRect3.ymin, intRect3.xmax + 1, intRect3.ymax + 1);
				for (int num10 = intRect3.xmin; num10 <= intRect3.xmax; num10++)
				{
					for (int num11 = intRect3.ymin; num11 <= intRect3.ymax; num11++)
					{
						if (!a3.Contains(num10, num11))
						{
							int num12 = num11 * this.width + num10;
							GridNode gridNode3 = this.nodes[num12];
							gridNode3.Walkable = gridNode3.TmpWalkable;
						}
					}
				}
				for (int num13 = intRect3.xmin; num13 <= intRect3.xmax; num13++)
				{
					for (int num14 = intRect3.ymin; num14 <= intRect3.ymax; num14++)
					{
						this.CalculateConnections(num13, num14);
					}
				}
			}
		}

		// Token: 0x060024CA RID: 9418 RVA: 0x0019A6E0 File Offset: 0x001988E0
		public bool Linecast(Vector3 from, Vector3 to)
		{
			GraphHitInfo graphHitInfo;
			return this.Linecast(from, to, null, out graphHitInfo);
		}

		// Token: 0x060024CB RID: 9419 RVA: 0x0019A6F8 File Offset: 0x001988F8
		public bool Linecast(Vector3 from, Vector3 to, GraphNode hint)
		{
			GraphHitInfo graphHitInfo;
			return this.Linecast(from, to, hint, out graphHitInfo);
		}

		// Token: 0x060024CC RID: 9420 RVA: 0x0019A710 File Offset: 0x00198910
		public bool Linecast(Vector3 from, Vector3 to, GraphNode hint, out GraphHitInfo hit)
		{
			return this.Linecast(from, to, hint, out hit, null);
		}

		// Token: 0x060024CD RID: 9421 RVA: 0x0019A71E File Offset: 0x0019891E
		protected static float CrossMagnitude(Vector2 a, Vector2 b)
		{
			return a.x * b.y - b.x * a.y;
		}

		// Token: 0x060024CE RID: 9422 RVA: 0x0019A73B File Offset: 0x0019893B
		protected static long CrossMagnitude(Int2 a, Int2 b)
		{
			return (long)a.x * (long)b.y - (long)b.x * (long)a.y;
		}

		// Token: 0x060024CF RID: 9423 RVA: 0x0019A75C File Offset: 0x0019895C
		protected bool ClipLineSegmentToBounds(Vector3 a, Vector3 b, out Vector3 outA, out Vector3 outB)
		{
			if (a.x < 0f || a.z < 0f || a.x > (float)this.width || a.z > (float)this.depth || b.x < 0f || b.z < 0f || b.x > (float)this.width || b.z > (float)this.depth)
			{
				Vector3 vector = new Vector3(0f, 0f, 0f);
				Vector3 vector2 = new Vector3(0f, 0f, (float)this.depth);
				Vector3 vector3 = new Vector3((float)this.width, 0f, (float)this.depth);
				Vector3 vector4 = new Vector3((float)this.width, 0f, 0f);
				int num = 0;
				bool flag;
				Vector3 vector5 = VectorMath.SegmentIntersectionPointXZ(a, b, vector, vector2, out flag);
				if (flag)
				{
					num++;
					if (!VectorMath.RightOrColinearXZ(vector, vector2, a))
					{
						a = vector5;
					}
					else
					{
						b = vector5;
					}
				}
				vector5 = VectorMath.SegmentIntersectionPointXZ(a, b, vector2, vector3, out flag);
				if (flag)
				{
					num++;
					if (!VectorMath.RightOrColinearXZ(vector2, vector3, a))
					{
						a = vector5;
					}
					else
					{
						b = vector5;
					}
				}
				vector5 = VectorMath.SegmentIntersectionPointXZ(a, b, vector3, vector4, out flag);
				if (flag)
				{
					num++;
					if (!VectorMath.RightOrColinearXZ(vector3, vector4, a))
					{
						a = vector5;
					}
					else
					{
						b = vector5;
					}
				}
				vector5 = VectorMath.SegmentIntersectionPointXZ(a, b, vector4, vector, out flag);
				if (flag)
				{
					num++;
					if (!VectorMath.RightOrColinearXZ(vector4, vector, a))
					{
						a = vector5;
					}
					else
					{
						b = vector5;
					}
				}
				if (num == 0)
				{
					outA = Vector3.zero;
					outB = Vector3.zero;
					return false;
				}
			}
			outA = a;
			outB = b;
			return true;
		}

		// Token: 0x060024D0 RID: 9424 RVA: 0x0019A918 File Offset: 0x00198B18
		public bool Linecast(Vector3 from, Vector3 to, GraphNode hint, out GraphHitInfo hit, List<GraphNode> trace)
		{
			hit = default(GraphHitInfo);
			hit.origin = from;
			Vector3 vector = this.transform.InverseTransform(from);
			Vector3 vector2 = this.transform.InverseTransform(to);
			if (!this.ClipLineSegmentToBounds(vector, vector2, out vector, out vector2))
			{
				hit.point = to;
				return false;
			}
			GridNodeBase gridNodeBase = base.GetNearest(this.transform.Transform(vector), NNConstraint.None).node as GridNodeBase;
			GridNodeBase gridNodeBase2 = base.GetNearest(this.transform.Transform(vector2), NNConstraint.None).node as GridNodeBase;
			if (!gridNodeBase.Walkable)
			{
				hit.node = gridNodeBase;
				hit.point = this.transform.Transform(vector);
				hit.tangentOrigin = hit.point;
				return true;
			}
			Vector2 vector3 = new Vector2(vector.x - 0.5f, vector.z - 0.5f);
			Vector2 vector4 = new Vector2(vector2.x - 0.5f, vector2.z - 0.5f);
			if (gridNodeBase == null || gridNodeBase2 == null)
			{
				hit.node = null;
				hit.point = from;
				return true;
			}
			Vector2 vector5 = vector4 - vector3;
			Vector2 b = new Vector2(Mathf.Sign(vector5.x), Mathf.Sign(vector5.y));
			float num = GridGraph.CrossMagnitude(vector5, b) * 0.5f;
			int num2 = ((vector5.y >= 0f) ? 0 : 3) ^ ((vector5.x >= 0f) ? 0 : 1);
			int num3 = num2 + 1 & 3;
			int num4 = num2 + 2 & 3;
			GridNodeBase gridNodeBase3 = gridNodeBase;
			while (gridNodeBase3.NodeInGridIndex != gridNodeBase2.NodeInGridIndex)
			{
				if (trace != null)
				{
					trace.Add(gridNodeBase3);
				}
				Vector2 a = new Vector2((float)gridNodeBase3.XCoordinateInGrid, (float)gridNodeBase3.ZCoordinateInGrid);
				int num5 = (GridGraph.CrossMagnitude(vector5, a - vector3) + num < 0f) ? num4 : num3;
				GridNodeBase neighbourAlongDirection = gridNodeBase3.GetNeighbourAlongDirection(num5);
				if (neighbourAlongDirection == null)
				{
					Vector2 a2 = new Vector2((float)this.neighbourXOffsets[num5], (float)this.neighbourZOffsets[num5]);
					Vector2 b2 = new Vector2((float)this.neighbourXOffsets[num5 - 1 + 4 & 3], (float)this.neighbourZOffsets[num5 - 1 + 4 & 3]);
					Vector2 vector6 = new Vector2((float)this.neighbourXOffsets[num5 + 1 & 3], (float)this.neighbourZOffsets[num5 + 1 & 3]);
					Vector2 vector7 = a + (a2 + b2) * 0.5f;
					Vector2 vector8 = VectorMath.LineIntersectionPoint(vector7, vector7 + vector6, vector3, vector4);
					Vector3 vector9 = this.transform.InverseTransform((Vector3)gridNodeBase3.position);
					Vector3 point = new Vector3(vector8.x + 0.5f, vector9.y, vector8.y + 0.5f);
					Vector3 point2 = new Vector3(vector7.x + 0.5f, vector9.y, vector7.y + 0.5f);
					hit.point = this.transform.Transform(point);
					hit.tangentOrigin = this.transform.Transform(point2);
					hit.tangent = this.transform.TransformVector(new Vector3(vector6.x, 0f, vector6.y));
					hit.node = gridNodeBase3;
					return true;
				}
				gridNodeBase3 = neighbourAlongDirection;
			}
			if (trace != null)
			{
				trace.Add(gridNodeBase3);
			}
			if (gridNodeBase3 == gridNodeBase2)
			{
				hit.point = to;
				hit.node = gridNodeBase3;
				return false;
			}
			hit.point = (Vector3)gridNodeBase3.position;
			hit.tangentOrigin = hit.point;
			return true;
		}

		// Token: 0x060024D1 RID: 9425 RVA: 0x0019ACC0 File Offset: 0x00198EC0
		public bool SnappedLinecast(Vector3 from, Vector3 to, GraphNode hint, out GraphHitInfo hit)
		{
			return this.Linecast((Vector3)base.GetNearest(from, NNConstraint.None).node.position, (Vector3)base.GetNearest(to, NNConstraint.None).node.position, hint, out hit);
		}

		// Token: 0x060024D2 RID: 9426 RVA: 0x0019AD0C File Offset: 0x00198F0C
		public bool Linecast(GridNodeBase fromNode, GridNodeBase toNode)
		{
			Int2 @int = new Int2(toNode.XCoordinateInGrid - fromNode.XCoordinateInGrid, toNode.ZCoordinateInGrid - fromNode.ZCoordinateInGrid);
			long num = GridGraph.CrossMagnitude(@int, new Int2(Math.Sign(@int.x), Math.Sign(@int.y)));
			int num2 = 0;
			if (@int.x <= 0 && @int.y > 0)
			{
				num2 = 1;
			}
			else if (@int.x < 0 && @int.y <= 0)
			{
				num2 = 2;
			}
			else if (@int.x >= 0 && @int.y < 0)
			{
				num2 = 3;
			}
			int num3 = num2 + 1 & 3;
			int num4 = num2 + 2 & 3;
			int num5 = (@int.x != 0 && @int.y != 0) ? (4 + (num2 + 1 & 3)) : -1;
			Int2 int2 = new Int2(0, 0);
			while (fromNode != null && fromNode.NodeInGridIndex != toNode.NodeInGridIndex)
			{
				long num6 = GridGraph.CrossMagnitude(@int, int2) * 2L + num;
				int num7 = (num6 < 0L) ? num4 : num3;
				if (num6 == 0L && num5 != -1)
				{
					num7 = num5;
				}
				fromNode = fromNode.GetNeighbourAlongDirection(num7);
				int2 += new Int2(this.neighbourXOffsets[num7], this.neighbourZOffsets[num7]);
			}
			return fromNode != toNode;
		}

		// Token: 0x060024D3 RID: 9427 RVA: 0x0019AE38 File Offset: 0x00199038
		public bool CheckConnection(GridNode node, int dir)
		{
			if (this.neighbours == NumNeighbours.Eight || this.neighbours == NumNeighbours.Six || dir < 4)
			{
				return this.HasNodeConnection(node, dir);
			}
			int num = dir - 4 - 1 & 3;
			int num2 = dir - 4 + 1 & 3;
			if (!this.HasNodeConnection(node, num) || !this.HasNodeConnection(node, num2))
			{
				return false;
			}
			GridNode gridNode = this.nodes[node.NodeInGridIndex + this.neighbourOffsets[num]];
			GridNode gridNode2 = this.nodes[node.NodeInGridIndex + this.neighbourOffsets[num2]];
			return gridNode.Walkable && gridNode2.Walkable && this.HasNodeConnection(gridNode2, num) && this.HasNodeConnection(gridNode, num2);
		}

		// Token: 0x060024D4 RID: 9428 RVA: 0x0019AEE4 File Offset: 0x001990E4
		protected override void SerializeExtraInfo(GraphSerializationContext ctx)
		{
			if (this.nodes == null)
			{
				ctx.writer.Write(-1);
				return;
			}
			ctx.writer.Write(this.nodes.Length);
			for (int i = 0; i < this.nodes.Length; i++)
			{
				this.nodes[i].SerializeNode(ctx);
			}
		}

		// Token: 0x060024D5 RID: 9429 RVA: 0x0019AF3C File Offset: 0x0019913C
		protected override void DeserializeExtraInfo(GraphSerializationContext ctx)
		{
			int num = ctx.reader.ReadInt32();
			if (num == -1)
			{
				this.nodes = null;
				return;
			}
			this.nodes = new GridNode[num];
			for (int i = 0; i < this.nodes.Length; i++)
			{
				this.nodes[i] = new GridNode(this.active);
				this.nodes[i].DeserializeNode(ctx);
			}
		}

		// Token: 0x060024D6 RID: 9430 RVA: 0x0019AFA4 File Offset: 0x001991A4
		protected override void DeserializeSettingsCompatibility(GraphSerializationContext ctx)
		{
			base.DeserializeSettingsCompatibility(ctx);
			this.aspectRatio = ctx.reader.ReadSingle();
			this.rotation = ctx.DeserializeVector3();
			this.center = ctx.DeserializeVector3();
			this.unclampedSize = ctx.DeserializeVector3();
			this.nodeSize = ctx.reader.ReadSingle();
			this.collision.DeserializeSettingsCompatibility(ctx);
			this.maxClimb = ctx.reader.ReadSingle();
			ctx.reader.ReadInt32();
			this.maxSlope = ctx.reader.ReadSingle();
			this.erodeIterations = ctx.reader.ReadInt32();
			this.erosionUseTags = ctx.reader.ReadBoolean();
			this.erosionFirstTag = ctx.reader.ReadInt32();
			ctx.reader.ReadBoolean();
			this.neighbours = (NumNeighbours)ctx.reader.ReadInt32();
			this.cutCorners = ctx.reader.ReadBoolean();
			this.penaltyPosition = ctx.reader.ReadBoolean();
			this.penaltyPositionFactor = ctx.reader.ReadSingle();
			this.penaltyAngle = ctx.reader.ReadBoolean();
			this.penaltyAngleFactor = ctx.reader.ReadSingle();
			this.penaltyAnglePower = ctx.reader.ReadSingle();
			this.isometricAngle = ctx.reader.ReadSingle();
			this.uniformEdgeCosts = ctx.reader.ReadBoolean();
			this.useJumpPointSearch = ctx.reader.ReadBoolean();
		}

		// Token: 0x060024D7 RID: 9431 RVA: 0x0019B128 File Offset: 0x00199328
		protected override void PostDeserialization(GraphSerializationContext ctx)
		{
			this.UpdateTransform();
			this.SetUpOffsetsAndCosts();
			GridNode.SetGridGraph((int)this.graphIndex, this);
			if (this.nodes == null || this.nodes.Length == 0)
			{
				return;
			}
			if (this.width * this.depth != this.nodes.Length)
			{
				Debug.LogError("Node data did not match with bounds data. Probably a change to the bounds/width/depth data was made after scanning the graph just prior to saving it. Nodes will be discarded");
				this.nodes = new GridNode[0];
				return;
			}
			for (int i = 0; i < this.depth; i++)
			{
				for (int j = 0; j < this.width; j++)
				{
					GridNode gridNode = this.nodes[i * this.width + j];
					if (gridNode == null)
					{
						Debug.LogError("Deserialization Error : Couldn't cast the node to the appropriate type - GridGenerator");
						return;
					}
					gridNode.NodeInGridIndex = i * this.width + j;
				}
			}
		}

		// Token: 0x0400405A RID: 16474
		[JsonMember]
		public InspectorGridMode inspectorGridMode;

		// Token: 0x0400405B RID: 16475
		public int width;

		// Token: 0x0400405C RID: 16476
		public int depth;

		// Token: 0x0400405D RID: 16477
		[JsonMember]
		public float aspectRatio = 1f;

		// Token: 0x0400405E RID: 16478
		[JsonMember]
		public float isometricAngle;

		// Token: 0x0400405F RID: 16479
		[JsonMember]
		public bool uniformEdgeCosts;

		// Token: 0x04004060 RID: 16480
		[JsonMember]
		public Vector3 rotation;

		// Token: 0x04004061 RID: 16481
		[JsonMember]
		public Vector3 center;

		// Token: 0x04004062 RID: 16482
		[JsonMember]
		public Vector2 unclampedSize;

		// Token: 0x04004063 RID: 16483
		[JsonMember]
		public float nodeSize = 1f;

		// Token: 0x04004064 RID: 16484
		[JsonMember]
		public GraphCollision collision;

		// Token: 0x04004065 RID: 16485
		[JsonMember]
		public float maxClimb = 0.4f;

		// Token: 0x04004066 RID: 16486
		[JsonMember]
		public float maxSlope = 90f;

		// Token: 0x04004067 RID: 16487
		[JsonMember]
		public int erodeIterations;

		// Token: 0x04004068 RID: 16488
		[JsonMember]
		public bool erosionUseTags;

		// Token: 0x04004069 RID: 16489
		[JsonMember]
		public int erosionFirstTag = 1;

		// Token: 0x0400406A RID: 16490
		[JsonMember]
		public NumNeighbours neighbours = NumNeighbours.Eight;

		// Token: 0x0400406B RID: 16491
		[JsonMember]
		public bool cutCorners = true;

		// Token: 0x0400406C RID: 16492
		[JsonMember]
		public float penaltyPositionOffset;

		// Token: 0x0400406D RID: 16493
		[JsonMember]
		public bool penaltyPosition;

		// Token: 0x0400406E RID: 16494
		[JsonMember]
		public float penaltyPositionFactor = 1f;

		// Token: 0x0400406F RID: 16495
		[JsonMember]
		public bool penaltyAngle;

		// Token: 0x04004070 RID: 16496
		[JsonMember]
		public float penaltyAngleFactor = 100f;

		// Token: 0x04004071 RID: 16497
		[JsonMember]
		public float penaltyAnglePower = 1f;

		// Token: 0x04004072 RID: 16498
		[JsonMember]
		public bool useJumpPointSearch;

		// Token: 0x04004073 RID: 16499
		[JsonMember]
		public bool showMeshOutline = true;

		// Token: 0x04004074 RID: 16500
		[JsonMember]
		public bool showNodeConnections;

		// Token: 0x04004075 RID: 16501
		[JsonMember]
		public bool showMeshSurface = true;

		// Token: 0x04004076 RID: 16502
		[JsonMember]
		public GridGraph.TextureData textureData = new GridGraph.TextureData();

		// Token: 0x04004078 RID: 16504
		[NonSerialized]
		public readonly int[] neighbourOffsets = new int[8];

		// Token: 0x04004079 RID: 16505
		[NonSerialized]
		public readonly uint[] neighbourCosts = new uint[8];

		// Token: 0x0400407A RID: 16506
		[NonSerialized]
		public readonly int[] neighbourXOffsets = new int[8];

		// Token: 0x0400407B RID: 16507
		[NonSerialized]
		public readonly int[] neighbourZOffsets = new int[8];

		// Token: 0x0400407C RID: 16508
		internal static readonly int[] hexagonNeighbourIndices = new int[]
		{
			0,
			1,
			5,
			2,
			3,
			7
		};

		// Token: 0x0400407D RID: 16509
		public const int getNearestForceOverlap = 2;

		// Token: 0x0400407E RID: 16510
		public GridNode[] nodes;

		// Token: 0x02000732 RID: 1842
		public class TextureData
		{
			// Token: 0x06002CD6 RID: 11478 RVA: 0x001CB438 File Offset: 0x001C9638
			public void Initialize()
			{
				if (this.enabled && this.source != null)
				{
					for (int i = 0; i < this.channels.Length; i++)
					{
						if (this.channels[i] != GridGraph.TextureData.ChannelUse.None)
						{
							try
							{
								this.data = this.source.GetPixels32();
								break;
							}
							catch (UnityException ex)
							{
								Debug.LogWarning(ex.ToString());
								this.data = null;
								break;
							}
						}
					}
				}
			}

			// Token: 0x06002CD7 RID: 11479 RVA: 0x001CB4B0 File Offset: 0x001C96B0
			public void Apply(GridNode node, int x, int z)
			{
				if (this.enabled && this.data != null && x < this.source.width && z < this.source.height)
				{
					Color32 color = this.data[z * this.source.width + x];
					if (this.channels[0] != GridGraph.TextureData.ChannelUse.None)
					{
						this.ApplyChannel(node, x, z, (int)color.r, this.channels[0], this.factors[0]);
					}
					if (this.channels[1] != GridGraph.TextureData.ChannelUse.None)
					{
						this.ApplyChannel(node, x, z, (int)color.g, this.channels[1], this.factors[1]);
					}
					if (this.channels[2] != GridGraph.TextureData.ChannelUse.None)
					{
						this.ApplyChannel(node, x, z, (int)color.b, this.channels[2], this.factors[2]);
					}
					node.WalkableErosion = node.Walkable;
				}
			}

			// Token: 0x06002CD8 RID: 11480 RVA: 0x001CB598 File Offset: 0x001C9798
			private void ApplyChannel(GridNode node, int x, int z, int value, GridGraph.TextureData.ChannelUse channelUse, float factor)
			{
				switch (channelUse)
				{
				case GridGraph.TextureData.ChannelUse.Penalty:
					node.Penalty += (uint)Mathf.RoundToInt((float)value * factor);
					return;
				case GridGraph.TextureData.ChannelUse.Position:
					node.position = GridNode.GetGridGraph(node.GraphIndex).GraphPointToWorld(x, z, (float)value);
					return;
				case GridGraph.TextureData.ChannelUse.WalkablePenalty:
					if (value == 0)
					{
						node.Walkable = false;
						return;
					}
					node.Penalty += (uint)Mathf.RoundToInt((float)(value - 1) * factor);
					return;
				default:
					return;
				}
			}

			// Token: 0x04004943 RID: 18755
			public bool enabled;

			// Token: 0x04004944 RID: 18756
			public Texture2D source;

			// Token: 0x04004945 RID: 18757
			public float[] factors = new float[3];

			// Token: 0x04004946 RID: 18758
			public GridGraph.TextureData.ChannelUse[] channels = new GridGraph.TextureData.ChannelUse[3];

			// Token: 0x04004947 RID: 18759
			private Color32[] data;

			// Token: 0x02000796 RID: 1942
			public enum ChannelUse
			{
				// Token: 0x04004AF5 RID: 19189
				None,
				// Token: 0x04004AF6 RID: 19190
				Penalty,
				// Token: 0x04004AF7 RID: 19191
				Position,
				// Token: 0x04004AF8 RID: 19192
				WalkablePenalty
			}
		}
	}
}
