using System;
using UnityEngine;

// Token: 0x0200037B RID: 891
public class PromptBarScript : MonoBehaviour
{
	// Token: 0x06001948 RID: 6472 RVA: 0x000F2FA8 File Offset: 0x000F11A8
	private void Awake()
	{
		base.transform.localPosition = new Vector3(base.transform.localPosition.x, -627f, base.transform.localPosition.z);
		this.ID = 0;
		while (this.ID < this.Label.Length)
		{
			this.Label[this.ID].text = string.Empty;
			this.ID++;
		}
	}

	// Token: 0x06001949 RID: 6473 RVA: 0x000F3028 File Offset: 0x000F1228
	private void Start()
	{
		this.UpdateButtons();
	}

	// Token: 0x0600194A RID: 6474 RVA: 0x000F3030 File Offset: 0x000F1230
	private void Update()
	{
		float t = Time.unscaledDeltaTime * 10f;
		if (!this.Show)
		{
			if (this.Panel.enabled)
			{
				base.transform.localPosition = new Vector3(base.transform.localPosition.x, Mathf.Lerp(base.transform.localPosition.y, -628f, t), base.transform.localPosition.z);
				if (base.transform.localPosition.y < -627f)
				{
					base.transform.localPosition = new Vector3(base.transform.localPosition.x, -628f, base.transform.localPosition.z);
					if (this.Panel != null)
					{
						this.Panel.enabled = false;
						return;
					}
				}
			}
		}
		else
		{
			base.transform.localPosition = new Vector3(base.transform.localPosition.x, Mathf.Lerp(base.transform.localPosition.y, -528.5f, t), base.transform.localPosition.z);
		}
	}

	// Token: 0x0600194B RID: 6475 RVA: 0x000F3164 File Offset: 0x000F1364
	public void UpdateButtons()
	{
		if (this.Panel != null)
		{
			this.Panel.enabled = true;
		}
		this.ID = 0;
		while (this.ID < this.Label.Length)
		{
			this.Button[this.ID].enabled = (this.Label[this.ID].text.Length > 0);
			this.ID++;
		}
	}

	// Token: 0x0600194C RID: 6476 RVA: 0x000F31E0 File Offset: 0x000F13E0
	public void ClearButtons()
	{
		this.ID = 0;
		while (this.ID < this.Label.Length)
		{
			this.Label[this.ID].text = string.Empty;
			this.Button[this.ID].enabled = false;
			this.ID++;
		}
	}

	// Token: 0x04002687 RID: 9863
	public UISprite[] Button;

	// Token: 0x04002688 RID: 9864
	public UILabel[] Label;

	// Token: 0x04002689 RID: 9865
	public UIPanel Panel;

	// Token: 0x0400268A RID: 9866
	public bool Show;

	// Token: 0x0400268B RID: 9867
	public int ID;
}
