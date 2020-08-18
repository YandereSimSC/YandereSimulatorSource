using System;
using UnityEngine;

// Token: 0x020003F0 RID: 1008
public class StalkerPromptScript : MonoBehaviour
{
	// Token: 0x06001AD4 RID: 6868 RVA: 0x0010D0DC File Offset: 0x0010B2DC
	private void Update()
	{
		base.transform.LookAt(this.Yandere.MainCamera.transform);
		if (Vector3.Distance(base.transform.position, this.Yandere.transform.position) < 5f)
		{
			this.Alpha = Mathf.MoveTowards(this.Alpha, 1f, Time.deltaTime);
			if (Vector3.Distance(base.transform.position, this.Yandere.transform.position) < 2f && Input.GetButtonDown("A") && this.ID == 1)
			{
				this.Yandere.MyAnimation.CrossFade("f02_climbTrellis_00");
				this.Yandere.Climbing = true;
				this.Yandere.CanMove = false;
				UnityEngine.Object.Destroy(base.gameObject);
				UnityEngine.Object.Destroy(this.MySprite);
			}
		}
		else
		{
			this.Alpha = Mathf.MoveTowards(this.Alpha, 0f, Time.deltaTime);
		}
		this.MySprite.color = new Color(1f, 1f, 1f, this.Alpha);
	}

	// Token: 0x04002B69 RID: 11113
	public StalkerYandereScript Yandere;

	// Token: 0x04002B6A RID: 11114
	public UISprite MySprite;

	// Token: 0x04002B6B RID: 11115
	public float Alpha;

	// Token: 0x04002B6C RID: 11116
	public int ID;
}
