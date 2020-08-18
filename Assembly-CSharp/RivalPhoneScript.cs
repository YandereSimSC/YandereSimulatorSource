using System;
using UnityEngine;

// Token: 0x020003A2 RID: 930
public class RivalPhoneScript : MonoBehaviour
{
	// Token: 0x060019C4 RID: 6596 RVA: 0x000FBAF2 File Offset: 0x000F9CF2
	private void Start()
	{
		this.OriginalParent = base.transform.parent;
		this.OriginalPosition = base.transform.localPosition;
		this.OriginalRotation = base.transform.localRotation;
	}

	// Token: 0x060019C5 RID: 6597 RVA: 0x000FBB28 File Offset: 0x000F9D28
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			if (this.StudentID == this.Prompt.Yandere.StudentManager.RivalID && SchemeGlobals.GetSchemeStage(1) == 4)
			{
				SchemeGlobals.SetSchemeStage(1, 5);
				this.Prompt.Yandere.PauseScreen.Schemes.UpdateInstructions();
			}
			this.Prompt.Yandere.RivalPhoneTexture = this.MyRenderer.material.mainTexture;
			this.Prompt.Yandere.Inventory.RivalPhone = true;
			this.Prompt.Yandere.Inventory.RivalPhoneID = this.StudentID;
			this.Prompt.enabled = false;
			base.enabled = false;
			this.StolenPhoneDropoff.Prompt.enabled = true;
			this.StolenPhoneDropoff.Phase = 1;
			this.StolenPhoneDropoff.Timer = 0f;
			this.StolenPhoneDropoff.Prompt.Label[0].text = "     Provide Stolen Phone";
			base.gameObject.SetActive(false);
			this.Stolen = true;
		}
	}

	// Token: 0x060019C6 RID: 6598 RVA: 0x000FBC58 File Offset: 0x000F9E58
	public void ReturnToOrigin()
	{
		base.transform.parent = this.OriginalParent;
		base.transform.localPosition = this.OriginalPosition;
		base.transform.localRotation = this.OriginalRotation;
		base.gameObject.SetActive(false);
		this.Prompt.enabled = true;
		this.LewdPhotos = false;
		this.Stolen = false;
		base.enabled = true;
	}

	// Token: 0x04002845 RID: 10309
	public DoorGapScript StolenPhoneDropoff;

	// Token: 0x04002846 RID: 10310
	public Renderer MyRenderer;

	// Token: 0x04002847 RID: 10311
	public PromptScript Prompt;

	// Token: 0x04002848 RID: 10312
	public bool LewdPhotos;

	// Token: 0x04002849 RID: 10313
	public bool Stolen;

	// Token: 0x0400284A RID: 10314
	public int StudentID;

	// Token: 0x0400284B RID: 10315
	public Vector3 OriginalPosition;

	// Token: 0x0400284C RID: 10316
	public Quaternion OriginalRotation;

	// Token: 0x0400284D RID: 10317
	public Transform OriginalParent;
}
