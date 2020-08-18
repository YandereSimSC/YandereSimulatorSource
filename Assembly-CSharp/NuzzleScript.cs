using System;
using UnityEngine;

// Token: 0x02000346 RID: 838
public class NuzzleScript : MonoBehaviour
{
	// Token: 0x0600187C RID: 6268 RVA: 0x000DFCC9 File Offset: 0x000DDEC9
	private void Start()
	{
		this.OriginalRotation = base.transform.localEulerAngles;
	}

	// Token: 0x0600187D RID: 6269 RVA: 0x000DFCDC File Offset: 0x000DDEDC
	private void Update()
	{
		if (!this.Down)
		{
			this.Rotate += Time.deltaTime * this.Speed;
			if (this.Rotate > this.Limit)
			{
				this.Down = true;
			}
		}
		else
		{
			this.Rotate -= Time.deltaTime * this.Speed;
			if (this.Rotate < -1f * this.Limit)
			{
				this.Down = false;
			}
		}
		base.transform.localEulerAngles = this.OriginalRotation + new Vector3(this.Rotate, 0f, 0f);
	}

	// Token: 0x040023F2 RID: 9202
	public Vector3 OriginalRotation;

	// Token: 0x040023F3 RID: 9203
	public float Rotate;

	// Token: 0x040023F4 RID: 9204
	public float Limit;

	// Token: 0x040023F5 RID: 9205
	public float Speed;

	// Token: 0x040023F6 RID: 9206
	private bool Down;
}
