using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding.Examples
{
	// Token: 0x020005F1 RID: 1521
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_examples_1_1_path_types_demo.php")]
	public class PathTypesDemo : MonoBehaviour
	{
		// Token: 0x060029E7 RID: 10727 RVA: 0x001C1CCC File Offset: 0x001BFECC
		private void Update()
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			Vector3 vector = ray.origin + ray.direction * (ray.origin.y / -ray.direction.y);
			this.end.position = vector;
			if (Input.GetMouseButtonUp(0))
			{
				if (Input.GetKey(KeyCode.LeftShift))
				{
					this.multipoints.Add(vector);
				}
				if (Input.GetKey(KeyCode.LeftControl))
				{
					this.multipoints.Clear();
				}
				if (Input.mousePosition.x > 225f)
				{
					this.DemoPath();
				}
			}
			if (Input.GetMouseButton(0) && Input.GetKey(KeyCode.LeftAlt) && (this.lastPath == null || this.lastPath.IsDone()))
			{
				this.DemoPath();
			}
		}

		// Token: 0x060029E8 RID: 10728 RVA: 0x001C1DA8 File Offset: 0x001BFFA8
		public void OnGUI()
		{
			GUILayout.BeginArea(new Rect(5f, 5f, 220f, (float)(Screen.height - 10)), "", "Box");
			switch (this.activeDemo)
			{
			case PathTypesDemo.DemoMode.ABPath:
				GUILayout.Label("Basic path. Finds a path from point A to point B.", Array.Empty<GUILayoutOption>());
				break;
			case PathTypesDemo.DemoMode.MultiTargetPath:
				GUILayout.Label("Multi Target Path. Finds a path quickly from one point to many others in a single search.", Array.Empty<GUILayoutOption>());
				break;
			case PathTypesDemo.DemoMode.RandomPath:
				GUILayout.Label("Randomized Path. Finds a path with a specified length in a random direction or biased towards some point when using a larger aim strenggth.", Array.Empty<GUILayoutOption>());
				break;
			case PathTypesDemo.DemoMode.FleePath:
				GUILayout.Label("Flee Path. Tries to flee from a specified point. Remember to set Flee Strength!", Array.Empty<GUILayoutOption>());
				break;
			case PathTypesDemo.DemoMode.ConstantPath:
				GUILayout.Label("Finds all nodes which it costs less than some value to reach.", Array.Empty<GUILayoutOption>());
				break;
			case PathTypesDemo.DemoMode.FloodPath:
				GUILayout.Label("Searches the whole graph from a specific point. FloodPathTracer can then be used to quickly find a path to that point", Array.Empty<GUILayoutOption>());
				break;
			case PathTypesDemo.DemoMode.FloodPathTracer:
				GUILayout.Label("Traces a path to where the FloodPath started. Compare the claculation times for this path with ABPath!\nGreat for TD games", Array.Empty<GUILayoutOption>());
				break;
			}
			GUILayout.Space(5f);
			GUILayout.Label("Note that the paths are rendered without ANY post-processing applied, so they might look a bit edgy", Array.Empty<GUILayoutOption>());
			GUILayout.Space(5f);
			GUILayout.Label("Click anywhere to recalculate the path. Hold Alt to continuously recalculate the path while the mouse is pressed.", Array.Empty<GUILayoutOption>());
			if (this.activeDemo == PathTypesDemo.DemoMode.ConstantPath || this.activeDemo == PathTypesDemo.DemoMode.RandomPath || this.activeDemo == PathTypesDemo.DemoMode.FleePath)
			{
				GUILayout.Label("Search Distance (" + this.searchLength + ")", Array.Empty<GUILayoutOption>());
				this.searchLength = Mathf.RoundToInt(GUILayout.HorizontalSlider((float)this.searchLength, 0f, 100000f, Array.Empty<GUILayoutOption>()));
			}
			if (this.activeDemo == PathTypesDemo.DemoMode.RandomPath || this.activeDemo == PathTypesDemo.DemoMode.FleePath)
			{
				GUILayout.Label("Spread (" + this.spread + ")", Array.Empty<GUILayoutOption>());
				this.spread = Mathf.RoundToInt(GUILayout.HorizontalSlider((float)this.spread, 0f, 40000f, Array.Empty<GUILayoutOption>()));
				GUILayout.Label(string.Concat(new object[]
				{
					(this.activeDemo == PathTypesDemo.DemoMode.RandomPath) ? "Aim strength" : "Flee strength",
					" (",
					this.aimStrength,
					")"
				}), Array.Empty<GUILayoutOption>());
				this.aimStrength = GUILayout.HorizontalSlider(this.aimStrength, 0f, 1f, Array.Empty<GUILayoutOption>());
			}
			if (this.activeDemo == PathTypesDemo.DemoMode.MultiTargetPath)
			{
				GUILayout.Label("Hold shift and click to add new target points. Hold ctr and click to remove all target points", Array.Empty<GUILayoutOption>());
			}
			if (GUILayout.Button("A to B path", Array.Empty<GUILayoutOption>()))
			{
				this.activeDemo = PathTypesDemo.DemoMode.ABPath;
			}
			if (GUILayout.Button("Multi Target Path", Array.Empty<GUILayoutOption>()))
			{
				this.activeDemo = PathTypesDemo.DemoMode.MultiTargetPath;
			}
			if (GUILayout.Button("Random Path", Array.Empty<GUILayoutOption>()))
			{
				this.activeDemo = PathTypesDemo.DemoMode.RandomPath;
			}
			if (GUILayout.Button("Flee path", Array.Empty<GUILayoutOption>()))
			{
				this.activeDemo = PathTypesDemo.DemoMode.FleePath;
			}
			if (GUILayout.Button("Constant Path", Array.Empty<GUILayoutOption>()))
			{
				this.activeDemo = PathTypesDemo.DemoMode.ConstantPath;
			}
			if (GUILayout.Button("Flood Path", Array.Empty<GUILayoutOption>()))
			{
				this.activeDemo = PathTypesDemo.DemoMode.FloodPath;
			}
			if (GUILayout.Button("Flood Path Tracer", Array.Empty<GUILayoutOption>()))
			{
				this.activeDemo = PathTypesDemo.DemoMode.FloodPathTracer;
			}
			GUILayout.EndArea();
		}

		// Token: 0x060029E9 RID: 10729 RVA: 0x001C20AC File Offset: 0x001C02AC
		public void OnPathComplete(Path p)
		{
			if (this.lastRender == null)
			{
				return;
			}
			this.ClearPrevious();
			if (p.error)
			{
				return;
			}
			GameObject gameObject = new GameObject("LineRenderer", new Type[]
			{
				typeof(LineRenderer)
			});
			LineRenderer component = gameObject.GetComponent<LineRenderer>();
			component.sharedMaterial = this.lineMat;
			component.startWidth = this.lineWidth;
			component.endWidth = this.lineWidth;
			component.positionCount = p.vectorPath.Count;
			for (int i = 0; i < p.vectorPath.Count; i++)
			{
				component.SetPosition(i, p.vectorPath[i] + this.pathOffset);
			}
			this.lastRender.Add(gameObject);
		}

		// Token: 0x060029EA RID: 10730 RVA: 0x001C216C File Offset: 0x001C036C
		private void ClearPrevious()
		{
			for (int i = 0; i < this.lastRender.Count; i++)
			{
				UnityEngine.Object.Destroy(this.lastRender[i]);
			}
			this.lastRender.Clear();
		}

		// Token: 0x060029EB RID: 10731 RVA: 0x001C21AB File Offset: 0x001C03AB
		private void OnDestroy()
		{
			this.ClearPrevious();
			this.lastRender = null;
		}

		// Token: 0x060029EC RID: 10732 RVA: 0x001C21BC File Offset: 0x001C03BC
		private void DemoPath()
		{
			Path path = null;
			switch (this.activeDemo)
			{
			case PathTypesDemo.DemoMode.ABPath:
				path = ABPath.Construct(this.start.position, this.end.position, new OnPathDelegate(this.OnPathComplete));
				break;
			case PathTypesDemo.DemoMode.MultiTargetPath:
				base.StartCoroutine(this.DemoMultiTargetPath());
				break;
			case PathTypesDemo.DemoMode.RandomPath:
			{
				RandomPath randomPath = RandomPath.Construct(this.start.position, this.searchLength, new OnPathDelegate(this.OnPathComplete));
				randomPath.spread = this.spread;
				randomPath.aimStrength = this.aimStrength;
				randomPath.aim = this.end.position;
				path = randomPath;
				break;
			}
			case PathTypesDemo.DemoMode.FleePath:
			{
				FleePath fleePath = FleePath.Construct(this.start.position, this.end.position, this.searchLength, new OnPathDelegate(this.OnPathComplete));
				fleePath.aimStrength = this.aimStrength;
				fleePath.spread = this.spread;
				path = fleePath;
				break;
			}
			case PathTypesDemo.DemoMode.ConstantPath:
				base.StartCoroutine(this.DemoConstantPath());
				break;
			case PathTypesDemo.DemoMode.FloodPath:
				path = (this.lastFloodPath = FloodPath.Construct(this.end.position, null));
				break;
			case PathTypesDemo.DemoMode.FloodPathTracer:
				if (this.lastFloodPath != null)
				{
					path = FloodPathTracer.Construct(this.end.position, this.lastFloodPath, new OnPathDelegate(this.OnPathComplete));
				}
				break;
			}
			if (path != null)
			{
				AstarPath.StartPath(path, false);
				this.lastPath = path;
			}
		}

		// Token: 0x060029ED RID: 10733 RVA: 0x001C233C File Offset: 0x001C053C
		private IEnumerator DemoMultiTargetPath()
		{
			MultiTargetPath mp = MultiTargetPath.Construct(this.multipoints.ToArray(), this.end.position, null, null);
			this.lastPath = mp;
			AstarPath.StartPath(mp, false);
			yield return base.StartCoroutine(mp.WaitForPath());
			List<GameObject> list = new List<GameObject>(this.lastRender);
			this.lastRender.Clear();
			for (int i = 0; i < mp.vectorPaths.Length; i++)
			{
				if (mp.vectorPaths[i] != null)
				{
					List<Vector3> list2 = mp.vectorPaths[i];
					GameObject gameObject;
					if (list.Count > i && list[i].GetComponent<LineRenderer>() != null)
					{
						gameObject = list[i];
						list.RemoveAt(i);
					}
					else
					{
						gameObject = new GameObject("LineRenderer_" + i, new Type[]
						{
							typeof(LineRenderer)
						});
					}
					LineRenderer component = gameObject.GetComponent<LineRenderer>();
					component.sharedMaterial = this.lineMat;
					component.startWidth = this.lineWidth;
					component.endWidth = this.lineWidth;
					component.positionCount = list2.Count;
					for (int j = 0; j < list2.Count; j++)
					{
						component.SetPosition(j, list2[j] + this.pathOffset);
					}
					this.lastRender.Add(gameObject);
				}
			}
			for (int k = 0; k < list.Count; k++)
			{
				UnityEngine.Object.Destroy(list[k]);
			}
			yield break;
		}

		// Token: 0x060029EE RID: 10734 RVA: 0x001C234B File Offset: 0x001C054B
		public IEnumerator DemoConstantPath()
		{
			ConstantPath constPath = ConstantPath.Construct(this.end.position, this.searchLength, null);
			AstarPath.StartPath(constPath, false);
			this.lastPath = constPath;
			yield return base.StartCoroutine(constPath.WaitForPath());
			this.ClearPrevious();
			List<GraphNode> allNodes = constPath.allNodes;
			Mesh mesh = new Mesh();
			List<Vector3> list = new List<Vector3>();
			bool flag = false;
			for (int i = allNodes.Count - 1; i >= 0; i--)
			{
				Vector3 a = (Vector3)allNodes[i].position + this.pathOffset;
				if (list.Count == 65000 && !flag)
				{
					Debug.LogError("Too many nodes, rendering a mesh would throw 65K vertex error. Using Debug.DrawRay instead for the rest of the nodes");
					flag = true;
				}
				if (flag)
				{
					Debug.DrawRay(a, Vector3.up, Color.blue);
				}
				else
				{
					GridGraph gridGraph = AstarData.GetGraph(allNodes[i]) as GridGraph;
					float d = 1f;
					if (gridGraph != null)
					{
						d = gridGraph.nodeSize;
					}
					list.Add(a + new Vector3(-0.5f, 0f, -0.5f) * d);
					list.Add(a + new Vector3(0.5f, 0f, -0.5f) * d);
					list.Add(a + new Vector3(-0.5f, 0f, 0.5f) * d);
					list.Add(a + new Vector3(0.5f, 0f, 0.5f) * d);
				}
			}
			Vector3[] array = list.ToArray();
			int[] array2 = new int[3 * array.Length / 2];
			int j = 0;
			int num = 0;
			while (j < array.Length)
			{
				array2[num] = j;
				array2[num + 1] = j + 1;
				array2[num + 2] = j + 2;
				array2[num + 3] = j + 1;
				array2[num + 4] = j + 3;
				array2[num + 5] = j + 2;
				num += 6;
				j += 4;
			}
			Vector2[] array3 = new Vector2[array.Length];
			for (int k = 0; k < array3.Length; k += 4)
			{
				array3[k] = new Vector2(0f, 0f);
				array3[k + 1] = new Vector2(1f, 0f);
				array3[k + 2] = new Vector2(0f, 1f);
				array3[k + 3] = new Vector2(1f, 1f);
			}
			mesh.vertices = array;
			mesh.triangles = array2;
			mesh.uv = array3;
			mesh.RecalculateNormals();
			GameObject gameObject = new GameObject("Mesh", new Type[]
			{
				typeof(MeshRenderer),
				typeof(MeshFilter)
			});
			gameObject.GetComponent<MeshFilter>().mesh = mesh;
			gameObject.GetComponent<MeshRenderer>().material = this.squareMat;
			this.lastRender.Add(gameObject);
			yield break;
		}

		// Token: 0x040043A5 RID: 17317
		public PathTypesDemo.DemoMode activeDemo;

		// Token: 0x040043A6 RID: 17318
		public Transform start;

		// Token: 0x040043A7 RID: 17319
		public Transform end;

		// Token: 0x040043A8 RID: 17320
		public Vector3 pathOffset;

		// Token: 0x040043A9 RID: 17321
		public Material lineMat;

		// Token: 0x040043AA RID: 17322
		public Material squareMat;

		// Token: 0x040043AB RID: 17323
		public float lineWidth;

		// Token: 0x040043AC RID: 17324
		public int searchLength = 1000;

		// Token: 0x040043AD RID: 17325
		public int spread = 100;

		// Token: 0x040043AE RID: 17326
		public float aimStrength;

		// Token: 0x040043AF RID: 17327
		private Path lastPath;

		// Token: 0x040043B0 RID: 17328
		private FloodPath lastFloodPath;

		// Token: 0x040043B1 RID: 17329
		private List<GameObject> lastRender = new List<GameObject>();

		// Token: 0x040043B2 RID: 17330
		private List<Vector3> multipoints = new List<Vector3>();

		// Token: 0x02000787 RID: 1927
		public enum DemoMode
		{
			// Token: 0x04004ADC RID: 19164
			ABPath,
			// Token: 0x04004ADD RID: 19165
			MultiTargetPath,
			// Token: 0x04004ADE RID: 19166
			RandomPath,
			// Token: 0x04004ADF RID: 19167
			FleePath,
			// Token: 0x04004AE0 RID: 19168
			ConstantPath,
			// Token: 0x04004AE1 RID: 19169
			FloodPath,
			// Token: 0x04004AE2 RID: 19170
			FloodPathTracer
		}
	}
}
