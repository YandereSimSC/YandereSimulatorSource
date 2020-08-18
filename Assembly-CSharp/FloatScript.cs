using System;
using UnityEngine;

// Token: 0x020002A2 RID: 674
public class FloatScript : MonoBehaviour
{
	// Token: 0x06001409 RID: 5129 RVA: 0x000AF608 File Offset: 0x000AD808
	private void Update()
	{
		if (!this.Down)
		{
			this.Float += Time.deltaTime * this.Speed;
			if (this.Float > this.Limit)
			{
				this.Down = true;
			}
		}
		else
		{
			this.Float -= Time.deltaTime * this.Speed;
			if (this.Float < -1f * this.Limit)
			{
				this.Down = false;
			}
		}
		base.transform.localPosition += new Vector3(0f, this.Float * Time.deltaTime, 0f);
		if (base.transform.localPosition.y > this.UpLimit)
		{
			base.transform.localPosition = new Vector3(base.transform.localPosition.x, this.UpLimit, base.transform.localPosition.z);
		}
		if (base.transform.localPosition.y < this.DownLimit)
		{
			base.transform.localPosition = new Vector3(base.transform.localPosition.x, this.DownLimit, base.transform.localPosition.z);
		}
	}

	// Token: 0x04001C2E RID: 7214
	public bool Down;

	// Token: 0x04001C2F RID: 7215
	public float Float;

	// Token: 0x04001C30 RID: 7216
	public float Speed;

	// Token: 0x04001C31 RID: 7217
	public float Limit;

	// Token: 0x04001C32 RID: 7218
	public float DownLimit;

	// Token: 0x04001C33 RID: 7219
	public float UpLimit;
}
