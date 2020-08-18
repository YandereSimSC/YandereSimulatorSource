using System;
using UnityEngine;

// Token: 0x02000301 RID: 769
public class InfoChanWindowScript : MonoBehaviour
{
	// Token: 0x0600176D RID: 5997 RVA: 0x000CA760 File Offset: 0x000C8960
	private void Update()
	{
		if (this.Drop)
		{
			this.Rotation = Mathf.Lerp(this.Rotation, this.Drop ? -90f : 0f, Time.deltaTime * 10f);
			base.transform.localEulerAngles = new Vector3(base.transform.localEulerAngles.x, this.Rotation, base.transform.localEulerAngles.z);
			this.Timer += Time.deltaTime;
			if (this.Timer > 1f)
			{
				if ((float)this.Orders > 0f)
				{
					UnityEngine.Object.Instantiate<GameObject>(this.Drops[this.ItemsToDrop[this.Orders]], this.DropPoint.position, Quaternion.identity);
					this.Timer = 0f;
					this.Orders--;
				}
				else
				{
					this.Open = false;
					if (this.Timer > 3f)
					{
						base.transform.localEulerAngles = new Vector3(base.transform.localEulerAngles.x, 0f, base.transform.localEulerAngles.z);
						this.Drop = false;
					}
				}
			}
		}
		if (this.Test)
		{
			this.DropObject();
		}
	}

	// Token: 0x0600176E RID: 5998 RVA: 0x000CA8B1 File Offset: 0x000C8AB1
	public void DropObject()
	{
		this.Rotation = 0f;
		this.Timer = 0f;
		this.Dropped = false;
		this.Test = false;
		this.Drop = true;
		this.Open = true;
	}

	// Token: 0x04002096 RID: 8342
	public Transform DropPoint;

	// Token: 0x04002097 RID: 8343
	public GameObject[] Drops;

	// Token: 0x04002098 RID: 8344
	public int[] ItemsToDrop;

	// Token: 0x04002099 RID: 8345
	public int Orders;

	// Token: 0x0400209A RID: 8346
	public int ID;

	// Token: 0x0400209B RID: 8347
	public float Rotation;

	// Token: 0x0400209C RID: 8348
	public float Timer;

	// Token: 0x0400209D RID: 8349
	public bool Dropped;

	// Token: 0x0400209E RID: 8350
	public bool Drop;

	// Token: 0x0400209F RID: 8351
	public bool Open = true;

	// Token: 0x040020A0 RID: 8352
	public bool Test;
}
