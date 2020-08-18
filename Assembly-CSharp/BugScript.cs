using System;
using UnityEngine;

// Token: 0x020000F1 RID: 241
public class BugScript : MonoBehaviour
{
	// Token: 0x06000A96 RID: 2710 RVA: 0x00057B22 File Offset: 0x00055D22
	private void Start()
	{
		this.MyRenderer.enabled = false;
	}

	// Token: 0x06000A97 RID: 2711 RVA: 0x00057B30 File Offset: 0x00055D30
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.MyAudio.clip = this.Praise[UnityEngine.Random.Range(0, this.Praise.Length)];
			this.MyAudio.Play();
			this.MyRenderer.enabled = true;
			this.Prompt.Yandere.Inventory.PantyShots += 5;
			base.enabled = false;
			this.Prompt.enabled = false;
			this.Prompt.Hide();
		}
	}

	// Token: 0x04000B2F RID: 2863
	public PromptScript Prompt;

	// Token: 0x04000B30 RID: 2864
	public Renderer MyRenderer;

	// Token: 0x04000B31 RID: 2865
	public AudioSource MyAudio;

	// Token: 0x04000B32 RID: 2866
	public AudioClip[] Praise;
}
