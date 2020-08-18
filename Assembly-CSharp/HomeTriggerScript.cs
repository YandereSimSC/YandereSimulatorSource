using System;
using UnityEngine;

// Token: 0x020002F6 RID: 758
public class HomeTriggerScript : MonoBehaviour
{
	// Token: 0x06001744 RID: 5956 RVA: 0x000C8384 File Offset: 0x000C6584
	private void Start()
	{
		this.Label.color = new Color(this.Label.color.r, this.Label.color.g, this.Label.color.b, 0f);
	}

	// Token: 0x06001745 RID: 5957 RVA: 0x000C83D6 File Offset: 0x000C65D6
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			this.HomeCamera.ID = this.ID;
			this.FadeIn = true;
		}
	}

	// Token: 0x06001746 RID: 5958 RVA: 0x000C8402 File Offset: 0x000C6602
	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player")
		{
			this.HomeCamera.ID = 0;
			this.FadeIn = false;
		}
	}

	// Token: 0x06001747 RID: 5959 RVA: 0x000C842C File Offset: 0x000C662C
	private void Update()
	{
		this.Label.color = new Color(this.Label.color.r, this.Label.color.g, this.Label.color.b, Mathf.MoveTowards(this.Label.color.a, this.FadeIn ? 1f : 0f, Time.deltaTime * 10f));
	}

	// Token: 0x06001748 RID: 5960 RVA: 0x000C84B0 File Offset: 0x000C66B0
	public void Disable()
	{
		this.Label.color = new Color(this.Label.color.r, this.Label.color.g, this.Label.color.b, 0f);
		base.gameObject.SetActive(false);
	}

	// Token: 0x0400202C RID: 8236
	public HomeCameraScript HomeCamera;

	// Token: 0x0400202D RID: 8237
	public UILabel Label;

	// Token: 0x0400202E RID: 8238
	public bool FadeIn;

	// Token: 0x0400202F RID: 8239
	public int ID;
}
