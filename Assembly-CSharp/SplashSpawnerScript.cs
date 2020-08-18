using System;
using UnityEngine;

// Token: 0x020003EC RID: 1004
public class SplashSpawnerScript : MonoBehaviour
{
	// Token: 0x06001AC7 RID: 6855 RVA: 0x0010C694 File Offset: 0x0010A894
	private void Update()
	{
		if (!this.FootUp)
		{
			if (base.transform.position.y > this.Yandere.transform.position.y + this.UpThreshold)
			{
				this.FootUp = true;
				return;
			}
		}
		else if (base.transform.position.y < this.Yandere.transform.position.y + this.DownThreshold)
		{
			this.FootUp = false;
			if (this.Bloody)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.BloodSplash, new Vector3(base.transform.position.x, this.Yandere.position.y, base.transform.position.z), Quaternion.identity);
				gameObject.transform.eulerAngles = new Vector3(-90f, gameObject.transform.eulerAngles.y, gameObject.transform.eulerAngles.z);
				this.Bloody = false;
			}
		}
	}

	// Token: 0x06001AC8 RID: 6856 RVA: 0x0010C7A7 File Offset: 0x0010A9A7
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name == "BloodPool(Clone)")
		{
			this.Bloody = true;
		}
	}

	// Token: 0x04002B48 RID: 11080
	public GameObject BloodSplash;

	// Token: 0x04002B49 RID: 11081
	public Transform Yandere;

	// Token: 0x04002B4A RID: 11082
	public bool Bloody;

	// Token: 0x04002B4B RID: 11083
	public bool FootUp;

	// Token: 0x04002B4C RID: 11084
	public float DownThreshold;

	// Token: 0x04002B4D RID: 11085
	public float UpThreshold;

	// Token: 0x04002B4E RID: 11086
	public float Height;
}
