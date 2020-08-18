using System;
using UnityEngine;

// Token: 0x020003AA RID: 938
public class SafeScript : MonoBehaviour
{
	// Token: 0x060019DD RID: 6621 RVA: 0x000FCB11 File Offset: 0x000FAD11
	private void Start()
	{
		this.ContentsPrompt.MyCollider.enabled = false;
		this.SafePrompt.enabled = false;
	}

	// Token: 0x060019DE RID: 6622 RVA: 0x000FCB30 File Offset: 0x000FAD30
	private void Update()
	{
		if (this.Key.activeInHierarchy && this.KeyPrompt.Circle[0].fillAmount == 0f)
		{
			this.KeyPrompt.Yandere.Inventory.SafeKey = true;
			this.SafePrompt.HideButton[0] = false;
			this.SafePrompt.enabled = true;
			this.Key.SetActive(false);
		}
		if (this.SafePrompt.Circle[0].fillAmount == 0f)
		{
			this.KeyPrompt.Yandere.Inventory.SafeKey = false;
			this.ContentsPrompt.MyCollider.enabled = true;
			this.Open = true;
			this.SafePrompt.Hide();
			this.SafePrompt.enabled = false;
		}
		if (this.ContentsPrompt.Circle[0].fillAmount == 0f)
		{
			this.MissionMode.DocumentsStolen = true;
			base.enabled = false;
			this.ContentsPrompt.Hide();
			this.ContentsPrompt.enabled = false;
			this.ContentsPrompt.gameObject.SetActive(false);
		}
		if (this.Open)
		{
			this.Rotation = Mathf.Lerp(this.Rotation, 0f, Time.deltaTime * 10f);
			this.Door.localEulerAngles = new Vector3(this.Door.localEulerAngles.x, this.Rotation, this.Door.localEulerAngles.z);
			if (this.Rotation < 1f)
			{
				this.Open = false;
				return;
			}
		}
		else if (this.SafePrompt.Yandere.Inventory.LockPick)
		{
			this.SafePrompt.HideButton[2] = false;
			this.SafePrompt.enabled = true;
			if (this.SafePrompt.Circle[2].fillAmount == 0f)
			{
				this.KeyPrompt.Hide();
				this.KeyPrompt.enabled = false;
				this.SafePrompt.Yandere.Inventory.LockPick = false;
				this.SafePrompt.HideButton[2] = true;
				this.ContentsPrompt.MyCollider.enabled = true;
				this.Open = true;
				return;
			}
		}
		else if (!this.SafePrompt.HideButton[2])
		{
			this.SafePrompt.HideButton[2] = true;
		}
	}

	// Token: 0x04002876 RID: 10358
	public MissionModeScript MissionMode;

	// Token: 0x04002877 RID: 10359
	public PromptScript ContentsPrompt;

	// Token: 0x04002878 RID: 10360
	public PromptScript SafePrompt;

	// Token: 0x04002879 RID: 10361
	public PromptScript KeyPrompt;

	// Token: 0x0400287A RID: 10362
	public Transform Door;

	// Token: 0x0400287B RID: 10363
	public GameObject Key;

	// Token: 0x0400287C RID: 10364
	public float Rotation;

	// Token: 0x0400287D RID: 10365
	public bool Open;
}
