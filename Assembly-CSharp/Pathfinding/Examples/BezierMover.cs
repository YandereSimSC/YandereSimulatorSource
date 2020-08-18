using System;
using UnityEngine;

namespace Pathfinding.Examples
{
	// Token: 0x020005E9 RID: 1513
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_examples_1_1_bezier_mover.php")]
	public class BezierMover : MonoBehaviour
	{
		// Token: 0x060029BB RID: 10683 RVA: 0x001C11D4 File Offset: 0x001BF3D4
		private Vector3 Position(float t)
		{
			int num = this.points.Length;
			int num2 = Mathf.FloorToInt(t) % num;
			return AstarSplines.CatmullRom(this.points[(num2 - 1 + num) % num].position, this.points[num2].position, this.points[(num2 + 1) % num].position, this.points[(num2 + 2) % num].position, t - (float)Mathf.FloorToInt(t));
		}

		// Token: 0x060029BC RID: 10684 RVA: 0x001C1244 File Offset: 0x001BF444
		private void Update()
		{
			float num = this.time;
			float num2 = this.time + 1f;
			while (num2 - num > 0.0001f)
			{
				float num3 = (num + num2) / 2f;
				if ((this.Position(num3) - base.transform.position).sqrMagnitude > this.speed * Time.deltaTime * (this.speed * Time.deltaTime))
				{
					num2 = num3;
				}
				else
				{
					num = num3;
				}
			}
			this.time = (num + num2) / 2f;
			Vector3 vector = this.Position(this.time);
			Vector3 a = this.Position(this.time + 0.001f);
			base.transform.position = vector;
			Vector3 vector2 = this.Position(this.time + 0.15f);
			Vector3 b = ((this.Position(this.time + 0.15f + 0.001f) - vector2).normalized - (a - vector).normalized) / (vector2 - vector).magnitude;
			Vector3 upwards = new Vector3(0f, 1f / (this.tiltAmount + 1E-05f), 0f) + b;
			base.transform.rotation = Quaternion.LookRotation(a - vector, upwards);
		}

		// Token: 0x060029BD RID: 10685 RVA: 0x001C13A8 File Offset: 0x001BF5A8
		private void OnDrawGizmos()
		{
			if (this.points.Length >= 3)
			{
				for (int i = 0; i < this.points.Length; i++)
				{
					if (this.points[i] == null)
					{
						return;
					}
				}
				Gizmos.color = Color.white;
				Vector3 from = this.Position(0f);
				for (int j = 0; j < this.points.Length; j++)
				{
					for (int k = 1; k <= 100; k++)
					{
						Vector3 vector = this.Position((float)j + (float)k / 100f);
						Gizmos.DrawLine(from, vector);
						from = vector;
					}
				}
			}
		}

		// Token: 0x04004389 RID: 17289
		public Transform[] points;

		// Token: 0x0400438A RID: 17290
		public float speed = 1f;

		// Token: 0x0400438B RID: 17291
		public float tiltAmount = 1f;

		// Token: 0x0400438C RID: 17292
		private float time;
	}
}
