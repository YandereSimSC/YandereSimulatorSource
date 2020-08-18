using System;
using UnityEngine;

// Token: 0x02000466 RID: 1126
public class WitnessCameraScript : MonoBehaviour
{
	// Token: 0x06001D20 RID: 7456 RVA: 0x0015BE68 File Offset: 0x0015A068
	private void Start()
	{
		this.MyCamera.enabled = false;
		this.MyCamera.rect = new Rect(0f, 0f, 0f, 0f);
	}

	// Token: 0x06001D21 RID: 7457 RVA: 0x0015BE9C File Offset: 0x0015A09C
	private void Update()
	{
		if (this.Show)
		{
			this.MyCamera.rect = new Rect(this.MyCamera.rect.x, this.MyCamera.rect.y, Mathf.Lerp(this.MyCamera.rect.width, 0.25f, Time.deltaTime * 10f), Mathf.Lerp(this.MyCamera.rect.height, 0.44444445f, Time.deltaTime * 10f));
			base.transform.localPosition = new Vector3(base.transform.localPosition.x, base.transform.localPosition.y, base.transform.localPosition.z + Time.deltaTime * 0.09f);
			this.WitnessTimer += Time.deltaTime;
			if (this.WitnessTimer > 5f)
			{
				this.WitnessTimer = 0f;
				this.Show = false;
			}
			if (this.Yandere.Struggling)
			{
				this.WitnessTimer = 0f;
				this.Show = false;
				return;
			}
		}
		else
		{
			this.MyCamera.rect = new Rect(this.MyCamera.rect.x, this.MyCamera.rect.y, Mathf.Lerp(this.MyCamera.rect.width, 0f, Time.deltaTime * 10f), Mathf.Lerp(this.MyCamera.rect.height, 0f, Time.deltaTime * 10f));
			if (this.MyCamera.enabled && this.MyCamera.rect.width < 0.1f)
			{
				this.MyCamera.enabled = false;
				base.transform.parent = null;
			}
		}
	}

	// Token: 0x040036E9 RID: 14057
	public YandereScript Yandere;

	// Token: 0x040036EA RID: 14058
	public Transform WitnessPOV;

	// Token: 0x040036EB RID: 14059
	public float WitnessTimer;

	// Token: 0x040036EC RID: 14060
	public Camera MyCamera;

	// Token: 0x040036ED RID: 14061
	public bool Show;
}
