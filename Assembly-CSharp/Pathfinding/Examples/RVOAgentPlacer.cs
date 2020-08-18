using System;
using System.Collections;
using UnityEngine;

namespace Pathfinding.Examples
{
	// Token: 0x020005E6 RID: 1510
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_examples_1_1_r_v_o_agent_placer.php")]
	public class RVOAgentPlacer : MonoBehaviour
	{
		// Token: 0x060029AD RID: 10669 RVA: 0x001C0843 File Offset: 0x001BEA43
		private IEnumerator Start()
		{
			yield return null;
			for (int i = 0; i < this.agents; i++)
			{
				float num = (float)i / (float)this.agents * 3.1415927f * 2f;
				Vector3 vector = new Vector3((float)Math.Cos((double)num), 0f, (float)Math.Sin((double)num)) * this.ringSize;
				Vector3 target = -vector + this.goalOffset;
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.prefab, Vector3.zero, Quaternion.Euler(0f, num + 180f, 0f));
				RVOExampleAgent component = gameObject.GetComponent<RVOExampleAgent>();
				if (component == null)
				{
					Debug.LogError("Prefab does not have an RVOExampleAgent component attached");
					yield break;
				}
				gameObject.transform.parent = base.transform;
				gameObject.transform.position = vector;
				component.repathRate = this.repathRate;
				component.SetTarget(target);
				component.SetColor(this.GetColor(num));
			}
			yield break;
		}

		// Token: 0x060029AE RID: 10670 RVA: 0x001BF971 File Offset: 0x001BDB71
		public Color GetColor(float angle)
		{
			return AstarMath.HSVToRGB(angle * 57.295776f, 0.8f, 0.6f);
		}

		// Token: 0x0400436C RID: 17260
		public int agents = 100;

		// Token: 0x0400436D RID: 17261
		public float ringSize = 100f;

		// Token: 0x0400436E RID: 17262
		public LayerMask mask;

		// Token: 0x0400436F RID: 17263
		public GameObject prefab;

		// Token: 0x04004370 RID: 17264
		public Vector3 goalOffset;

		// Token: 0x04004371 RID: 17265
		public float repathRate = 1f;

		// Token: 0x04004372 RID: 17266
		private const float rad2Deg = 57.295776f;
	}
}
