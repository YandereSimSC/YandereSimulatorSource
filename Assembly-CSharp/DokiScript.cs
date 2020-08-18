using System;
using UnityEngine;

// Token: 0x0200026C RID: 620
public class DokiScript : MonoBehaviour
{
	// Token: 0x0600134E RID: 4942 RVA: 0x000A4274 File Offset: 0x000A2474
	private void Update()
	{
		if (!this.Yandere.Egg)
		{
			if (this.OtherPrompt.Circle[0].fillAmount == 0f)
			{
				this.Prompt.Hide();
				this.Prompt.enabled = false;
				base.enabled = false;
			}
			if (this.Prompt.Circle[0].fillAmount == 0f)
			{
				this.Yandere.PantyAttacher.newRenderer.enabled = false;
				this.Prompt.Circle[0].fillAmount = 1f;
				UnityEngine.Object.Instantiate<GameObject>(this.TransformEffect, this.Yandere.Hips.position, Quaternion.identity);
				this.Yandere.MyRenderer.sharedMesh = this.Yandere.Uniforms[4];
				this.Yandere.MyRenderer.materials[0].mainTexture = this.DokiTexture;
				this.Yandere.MyRenderer.materials[1].mainTexture = this.DokiTexture;
				this.ID++;
				if (this.ID > 4)
				{
					this.ID = 1;
				}
				this.Credits.SongLabel.text = this.DokiName[this.ID] + " from Doki Doki Literature Club";
				this.Credits.BandLabel.text = "by Team Salvato";
				this.Credits.Panel.enabled = true;
				this.Credits.Slide = true;
				this.Credits.Timer = 0f;
				if (this.ID == 1)
				{
					this.Yandere.MyRenderer.materials[0].SetTexture("_OverlayTex", this.DokiSocks[0]);
					this.Yandere.MyRenderer.materials[1].SetTexture("_OverlayTex", this.DokiSocks[0]);
				}
				else
				{
					this.Yandere.MyRenderer.materials[0].SetTexture("_OverlayTex", this.DokiSocks[1]);
					this.Yandere.MyRenderer.materials[1].SetTexture("_OverlayTex", this.DokiSocks[1]);
				}
				Debug.Log("Activating shadows on Yandere-chan.");
				this.Yandere.MyRenderer.materials[0].SetFloat("_BlendAmount", 1f);
				this.Yandere.MyRenderer.materials[1].SetFloat("_BlendAmount", 1f);
				this.Yandere.MyRenderer.materials[2].mainTexture = this.DokiHair[this.ID];
				this.Yandere.Hairstyle = 136 + this.ID;
				this.Yandere.UpdateHair();
				return;
			}
		}
		else
		{
			this.Prompt.Hide();
			this.Prompt.enabled = false;
			base.enabled = false;
		}
	}

	// Token: 0x04001A31 RID: 6705
	public MusicCreditScript Credits;

	// Token: 0x04001A32 RID: 6706
	public YandereScript Yandere;

	// Token: 0x04001A33 RID: 6707
	public PromptScript OtherPrompt;

	// Token: 0x04001A34 RID: 6708
	public PromptScript Prompt;

	// Token: 0x04001A35 RID: 6709
	public GameObject TransformEffect;

	// Token: 0x04001A36 RID: 6710
	public Texture DokiTexture;

	// Token: 0x04001A37 RID: 6711
	public Texture[] DokiSocks;

	// Token: 0x04001A38 RID: 6712
	public Texture[] DokiHair;

	// Token: 0x04001A39 RID: 6713
	public string[] DokiName;

	// Token: 0x04001A3A RID: 6714
	public int ID;
}
