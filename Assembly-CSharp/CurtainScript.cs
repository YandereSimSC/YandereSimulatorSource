using System;
using UnityEngine;

// Token: 0x02000253 RID: 595
public class CurtainScript : MonoBehaviour
{
	// Token: 0x060012C8 RID: 4808 RVA: 0x000962E8 File Offset: 0x000944E8
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Circle[0].fillAmount = 1f;
			this.MyAudio.Play();
			this.Animate = true;
			this.Open = !this.Open;
		}
		if (this.Animate)
		{
			if (!this.Open)
			{
				this.Weight = Mathf.Lerp(this.Weight, 0f, Time.deltaTime * 10f);
				if (this.Weight < 0.01f)
				{
					this.Animate = false;
					this.Weight = 0f;
				}
			}
			else
			{
				this.Weight = Mathf.Lerp(this.Weight, 100f, Time.deltaTime * 10f);
				if (this.Weight > 99.99f)
				{
					this.Animate = false;
					this.Weight = 100f;
				}
			}
			this.Curtains[0].SetBlendShapeWeight(0, this.Weight);
			this.Curtains[1].SetBlendShapeWeight(0, this.Weight);
		}
	}

	// Token: 0x060012C9 RID: 4809 RVA: 0x00096404 File Offset: 0x00094604
	private void OnTriggerEnter(Collider other)
	{
		if ((other.gameObject.layer == 13 || other.gameObject.layer == 9) && !this.Open)
		{
			this.MyAudio.Play();
			this.Animate = true;
			this.Open = true;
		}
	}

	// Token: 0x04001868 RID: 6248
	public SkinnedMeshRenderer[] Curtains;

	// Token: 0x04001869 RID: 6249
	public PromptScript Prompt;

	// Token: 0x0400186A RID: 6250
	public AudioSource MyAudio;

	// Token: 0x0400186B RID: 6251
	public bool Animate;

	// Token: 0x0400186C RID: 6252
	public bool Open;

	// Token: 0x0400186D RID: 6253
	public float Weight;
}
