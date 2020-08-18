using System;
using UnityEngine;

// Token: 0x020003EB RID: 1003
public class SplashCameraScript : MonoBehaviour
{
	// Token: 0x06001AC4 RID: 6852 RVA: 0x0010C4D6 File Offset: 0x0010A6D6
	private void Start()
	{
		this.MyCamera.enabled = false;
		this.MyCamera.rect = new Rect(0f, 0.219f, 0f, 0f);
	}

	// Token: 0x06001AC5 RID: 6853 RVA: 0x0010C508 File Offset: 0x0010A708
	private void Update()
	{
		if (this.Show)
		{
			this.MyCamera.rect = new Rect(this.MyCamera.rect.x, this.MyCamera.rect.y, Mathf.Lerp(this.MyCamera.rect.width, 0.4f, Time.deltaTime * 10f), Mathf.Lerp(this.MyCamera.rect.height, 0.71104f, Time.deltaTime * 10f));
			this.Timer += Time.deltaTime;
			if (this.Timer > 15f)
			{
				this.Show = false;
				this.Timer = 0f;
				return;
			}
		}
		else
		{
			this.MyCamera.rect = new Rect(this.MyCamera.rect.x, this.MyCamera.rect.y, Mathf.Lerp(this.MyCamera.rect.width, 0f, Time.deltaTime * 10f), Mathf.Lerp(this.MyCamera.rect.height, 0f, Time.deltaTime * 10f));
			if (this.MyCamera.enabled && this.MyCamera.rect.width < 0.1f)
			{
				this.MyCamera.enabled = false;
			}
		}
	}

	// Token: 0x04002B45 RID: 11077
	public Camera MyCamera;

	// Token: 0x04002B46 RID: 11078
	public bool Show;

	// Token: 0x04002B47 RID: 11079
	public float Timer;
}
