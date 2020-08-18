using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding.Examples
{
	// Token: 0x020005E8 RID: 1512
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_examples_1_1_procedural_world.php")]
	public class ProceduralWorld : MonoBehaviour
	{
		// Token: 0x060029B7 RID: 10679 RVA: 0x001C0F39 File Offset: 0x001BF139
		private void Start()
		{
			this.Update();
			AstarPath.active.Scan(null);
			base.StartCoroutine(this.GenerateTiles());
		}

		// Token: 0x060029B8 RID: 10680 RVA: 0x001C0F5C File Offset: 0x001BF15C
		private void Update()
		{
			Int2 @int = new Int2(Mathf.RoundToInt((this.target.position.x - this.tileSize * 0.5f) / this.tileSize), Mathf.RoundToInt((this.target.position.z - this.tileSize * 0.5f) / this.tileSize));
			this.range = ((this.range < 1) ? 1 : this.range);
			bool flag = true;
			while (flag)
			{
				flag = false;
				foreach (KeyValuePair<Int2, ProceduralWorld.ProceduralTile> keyValuePair in this.tiles)
				{
					if (Mathf.Abs(keyValuePair.Key.x - @int.x) > this.range || Mathf.Abs(keyValuePair.Key.y - @int.y) > this.range)
					{
						keyValuePair.Value.Destroy();
						this.tiles.Remove(keyValuePair.Key);
						flag = true;
						break;
					}
				}
			}
			for (int i = @int.x - this.range; i <= @int.x + this.range; i++)
			{
				for (int j = @int.y - this.range; j <= @int.y + this.range; j++)
				{
					if (!this.tiles.ContainsKey(new Int2(i, j)))
					{
						ProceduralWorld.ProceduralTile proceduralTile = new ProceduralWorld.ProceduralTile(this, i, j);
						IEnumerator enumerator2 = proceduralTile.Generate();
						enumerator2.MoveNext();
						this.tileGenerationQueue.Enqueue(enumerator2);
						this.tiles.Add(new Int2(i, j), proceduralTile);
					}
				}
			}
			for (int k = @int.x - 1; k <= @int.x + 1; k++)
			{
				for (int l = @int.y - 1; l <= @int.y + 1; l++)
				{
					this.tiles[new Int2(k, l)].ForceFinish();
				}
			}
		}

		// Token: 0x060029B9 RID: 10681 RVA: 0x001C118C File Offset: 0x001BF38C
		private IEnumerator GenerateTiles()
		{
			for (;;)
			{
				if (this.tileGenerationQueue.Count > 0)
				{
					IEnumerator routine = this.tileGenerationQueue.Dequeue();
					yield return base.StartCoroutine(routine);
				}
				yield return null;
			}
			yield break;
		}

		// Token: 0x04004381 RID: 17281
		public Transform target;

		// Token: 0x04004382 RID: 17282
		public ProceduralWorld.ProceduralPrefab[] prefabs;

		// Token: 0x04004383 RID: 17283
		public int range = 1;

		// Token: 0x04004384 RID: 17284
		public float tileSize = 100f;

		// Token: 0x04004385 RID: 17285
		public int subTiles = 20;

		// Token: 0x04004386 RID: 17286
		public bool staticBatching;

		// Token: 0x04004387 RID: 17287
		private Queue<IEnumerator> tileGenerationQueue = new Queue<IEnumerator>();

		// Token: 0x04004388 RID: 17288
		private Dictionary<Int2, ProceduralWorld.ProceduralTile> tiles = new Dictionary<Int2, ProceduralWorld.ProceduralTile>();

		// Token: 0x02000780 RID: 1920
		[Serializable]
		public class ProceduralPrefab
		{
			// Token: 0x04004AAE RID: 19118
			public GameObject prefab;

			// Token: 0x04004AAF RID: 19119
			public float density;

			// Token: 0x04004AB0 RID: 19120
			public float perlin;

			// Token: 0x04004AB1 RID: 19121
			public float perlinPower = 1f;

			// Token: 0x04004AB2 RID: 19122
			public Vector2 perlinOffset = Vector2.zero;

			// Token: 0x04004AB3 RID: 19123
			public float perlinScale = 1f;

			// Token: 0x04004AB4 RID: 19124
			public float random = 1f;

			// Token: 0x04004AB5 RID: 19125
			public bool singleFixed;
		}

		// Token: 0x02000781 RID: 1921
		private class ProceduralTile
		{
			// Token: 0x17000694 RID: 1684
			// (get) Token: 0x06002DC0 RID: 11712 RVA: 0x001D10FB File Offset: 0x001CF2FB
			// (set) Token: 0x06002DC1 RID: 11713 RVA: 0x001D1103 File Offset: 0x001CF303
			public bool destroyed { get; private set; }

			// Token: 0x06002DC2 RID: 11714 RVA: 0x001D110C File Offset: 0x001CF30C
			public ProceduralTile(ProceduralWorld world, int x, int z)
			{
				this.x = x;
				this.z = z;
				this.world = world;
				this.rnd = new System.Random(x * 10007 ^ z * 36007);
			}

			// Token: 0x06002DC3 RID: 11715 RVA: 0x001D1143 File Offset: 0x001CF343
			public IEnumerator Generate()
			{
				this.ie = this.InternalGenerate();
				GameObject gameObject = new GameObject(string.Concat(new object[]
				{
					"Tile ",
					this.x,
					" ",
					this.z
				}));
				this.root = gameObject.transform;
				while (this.ie != null && this.root != null && this.ie.MoveNext())
				{
					yield return this.ie.Current;
				}
				this.ie = null;
				yield break;
			}

			// Token: 0x06002DC4 RID: 11716 RVA: 0x001D1152 File Offset: 0x001CF352
			public void ForceFinish()
			{
				while (this.ie != null && this.root != null && this.ie.MoveNext())
				{
				}
				this.ie = null;
			}

			// Token: 0x06002DC5 RID: 11717 RVA: 0x001D1180 File Offset: 0x001CF380
			private Vector3 RandomInside()
			{
				return new Vector3
				{
					x = ((float)this.x + (float)this.rnd.NextDouble()) * this.world.tileSize,
					z = ((float)this.z + (float)this.rnd.NextDouble()) * this.world.tileSize
				};
			}

			// Token: 0x06002DC6 RID: 11718 RVA: 0x001D11E4 File Offset: 0x001CF3E4
			private Vector3 RandomInside(float px, float pz)
			{
				return new Vector3
				{
					x = (px + (float)this.rnd.NextDouble() / (float)this.world.subTiles) * this.world.tileSize,
					z = (pz + (float)this.rnd.NextDouble() / (float)this.world.subTiles) * this.world.tileSize
				};
			}

			// Token: 0x06002DC7 RID: 11719 RVA: 0x001D1256 File Offset: 0x001CF456
			private Quaternion RandomYRot()
			{
				return Quaternion.Euler(360f * (float)this.rnd.NextDouble(), 0f, 360f * (float)this.rnd.NextDouble());
			}

			// Token: 0x06002DC8 RID: 11720 RVA: 0x001D1286 File Offset: 0x001CF486
			private IEnumerator InternalGenerate()
			{
				Debug.Log(string.Concat(new object[]
				{
					"Generating tile ",
					this.x,
					", ",
					this.z
				}));
				int counter = 0;
				float[,] ditherMap = new float[this.world.subTiles + 2, this.world.subTiles + 2];
				int num3;
				for (int i = 0; i < this.world.prefabs.Length; i = num3 + 1)
				{
					ProceduralWorld.ProceduralPrefab pref = this.world.prefabs[i];
					if (pref.singleFixed)
					{
						Vector3 position = new Vector3(((float)this.x + 0.5f) * this.world.tileSize, 0f, ((float)this.z + 0.5f) * this.world.tileSize);
						UnityEngine.Object.Instantiate<GameObject>(pref.prefab, position, Quaternion.identity).transform.parent = this.root;
					}
					else
					{
						float subSize = this.world.tileSize / (float)this.world.subTiles;
						for (int k = 0; k < this.world.subTiles; k++)
						{
							for (int l = 0; l < this.world.subTiles; l++)
							{
								ditherMap[k + 1, l + 1] = 0f;
							}
						}
						for (int sx = 0; sx < this.world.subTiles; sx = num3 + 1)
						{
							for (int sz = 0; sz < this.world.subTiles; sz = num3 + 1)
							{
								float px = (float)this.x + (float)sx / (float)this.world.subTiles;
								float pz = (float)this.z + (float)sz / (float)this.world.subTiles;
								float b = Mathf.Pow(Mathf.PerlinNoise((px + pref.perlinOffset.x) * pref.perlinScale, (pz + pref.perlinOffset.y) * pref.perlinScale), pref.perlinPower);
								float num = pref.density * Mathf.Lerp(1f, b, pref.perlin) * Mathf.Lerp(1f, (float)this.rnd.NextDouble(), pref.random);
								float num2 = subSize * subSize * num + ditherMap[sx + 1, sz + 1];
								int count = Mathf.RoundToInt(num2);
								ditherMap[sx + 1 + 1, sz + 1] += 0.4375f * (num2 - (float)count);
								ditherMap[sx + 1 - 1, sz + 1 + 1] += 0.1875f * (num2 - (float)count);
								ditherMap[sx + 1, sz + 1 + 1] += 0.3125f * (num2 - (float)count);
								ditherMap[sx + 1 + 1, sz + 1 + 1] += 0.0625f * (num2 - (float)count);
								for (int j = 0; j < count; j = num3 + 1)
								{
									Vector3 position2 = this.RandomInside(px, pz);
									UnityEngine.Object.Instantiate<GameObject>(pref.prefab, position2, this.RandomYRot()).transform.parent = this.root;
									num3 = counter;
									counter = num3 + 1;
									if (counter % 2 == 0)
									{
										yield return null;
									}
									num3 = j;
								}
								num3 = sz;
							}
							num3 = sx;
						}
					}
					pref = null;
					num3 = i;
				}
				ditherMap = null;
				yield return null;
				yield return null;
				if (Application.HasProLicense() && this.world.staticBatching)
				{
					StaticBatchingUtility.Combine(this.root.gameObject);
				}
				yield break;
			}

			// Token: 0x06002DC9 RID: 11721 RVA: 0x001D1298 File Offset: 0x001CF498
			public void Destroy()
			{
				if (this.root != null)
				{
					Debug.Log(string.Concat(new object[]
					{
						"Destroying tile ",
						this.x,
						", ",
						this.z
					}));
					UnityEngine.Object.Destroy(this.root.gameObject);
					this.root = null;
				}
				this.ie = null;
			}

			// Token: 0x04004AB6 RID: 19126
			private int x;

			// Token: 0x04004AB7 RID: 19127
			private int z;

			// Token: 0x04004AB8 RID: 19128
			private System.Random rnd;

			// Token: 0x04004AB9 RID: 19129
			private ProceduralWorld world;

			// Token: 0x04004ABB RID: 19131
			private Transform root;

			// Token: 0x04004ABC RID: 19132
			private IEnumerator ie;
		}
	}
}
