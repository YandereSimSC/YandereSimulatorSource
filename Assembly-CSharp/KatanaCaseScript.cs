using System;
using UnityEngine;

// Token: 0x02000314 RID: 788
public class KatanaCaseScript : MonoBehaviour
{
	// Token: 0x060017CC RID: 6092 RVA: 0x000D10ED File Offset: 0x000CF2ED
	private void Start()
	{
		this.CasePrompt.enabled = false;
	}

	// Token: 0x060017CD RID: 6093 RVA: 0x000D10FC File Offset: 0x000CF2FC
	private void Update()
	{
		if (this.Key.activeInHierarchy && this.KeyPrompt.Circle[0].fillAmount == 0f)
		{
			this.KeyPrompt.Yandere.Inventory.CaseKey = true;
			this.CasePrompt.HideButton[0] = false;
			this.CasePrompt.enabled = true;
			this.Key.SetActive(false);
		}
		if (this.CasePrompt.Circle[0].fillAmount == 0f)
		{
			this.KeyPrompt.Yandere.Inventory.CaseKey = false;
			this.Open = true;
			this.CasePrompt.Hide();
			this.CasePrompt.enabled = false;
		}
		if (this.CasePrompt.Yandere.Inventory.LockPick)
		{
			this.CasePrompt.HideButton[2] = false;
			this.CasePrompt.enabled = true;
			if (this.CasePrompt.Circle[2].fillAmount == 0f)
			{
				this.KeyPrompt.Hide();
				this.KeyPrompt.enabled = false;
				this.CasePrompt.Yandere.Inventory.LockPick = false;
				this.CasePrompt.Label[0].text = "     Open";
				this.CasePrompt.HideButton[2] = true;
				this.CasePrompt.HideButton[0] = true;
				this.Open = true;
			}
		}
		else if (!this.CasePrompt.HideButton[2])
		{
			this.CasePrompt.HideButton[2] = true;
		}
		if (this.Open)
		{
			this.Rotation = Mathf.Lerp(this.Rotation, -180f, Time.deltaTime * 10f);
			this.Door.eulerAngles = new Vector3(this.Door.eulerAngles.x, this.Door.eulerAngles.y, this.Rotation);
			if (this.Rotation < -179.9f)
			{
				base.enabled = false;
			}
		}
	}

	// Token: 0x040021F9 RID: 8697
	public PromptScript CasePrompt;

	// Token: 0x040021FA RID: 8698
	public PromptScript KeyPrompt;

	// Token: 0x040021FB RID: 8699
	public Transform Door;

	// Token: 0x040021FC RID: 8700
	public GameObject Key;

	// Token: 0x040021FD RID: 8701
	public float Rotation;

	// Token: 0x040021FE RID: 8702
	public bool Open;
}
