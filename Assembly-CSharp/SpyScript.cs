using System;
using UnityEngine;

// Token: 0x020003EE RID: 1006
public class SpyScript : MonoBehaviour
{
	// Token: 0x06001ACD RID: 6861 RVA: 0x0010C94C File Offset: 0x0010AB4C
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Yandere.Character.GetComponent<Animation>().CrossFade("f02_spying_00");
			this.Yandere.CanMove = false;
			this.Phase++;
		}
		if (this.Phase == 1)
		{
			Quaternion b = Quaternion.LookRotation(this.SpyTarget.transform.position - this.Yandere.transform.position);
			this.Yandere.transform.rotation = Quaternion.Slerp(this.Yandere.transform.rotation, b, Time.deltaTime * 10f);
			this.Yandere.MoveTowardsTarget(this.SpySpot.position);
			this.Timer += Time.deltaTime;
			if (this.Timer > 1f)
			{
				if (this.Yandere.Inventory.DirectionalMic)
				{
					this.PromptBar.Label[0].text = "Record";
					this.CanRecord = true;
				}
				this.PromptBar.Label[1].text = "Stop";
				this.PromptBar.UpdateButtons();
				this.PromptBar.Show = true;
				this.Yandere.MainCamera.enabled = false;
				this.SpyCamera.SetActive(true);
				this.Phase++;
				return;
			}
		}
		else if (this.Phase == 2)
		{
			if (this.CanRecord && Input.GetButtonDown("A"))
			{
				this.Yandere.Character.GetComponent<Animation>().CrossFade("f02_spyRecord_00");
				this.Yandere.Microphone.SetActive(true);
				this.Recording = true;
			}
			if (Input.GetButtonDown("B"))
			{
				this.End();
			}
		}
	}

	// Token: 0x06001ACE RID: 6862 RVA: 0x0010CB34 File Offset: 0x0010AD34
	public void End()
	{
		this.PromptBar.ClearButtons();
		this.PromptBar.Show = false;
		this.Yandere.Microphone.SetActive(false);
		this.Yandere.MainCamera.enabled = true;
		this.Yandere.CanMove = true;
		this.SpyCamera.SetActive(false);
		this.Timer = 0f;
		this.Phase = 0;
	}

	// Token: 0x04002B53 RID: 11091
	public PromptBarScript PromptBar;

	// Token: 0x04002B54 RID: 11092
	public YandereScript Yandere;

	// Token: 0x04002B55 RID: 11093
	public PromptScript Prompt;

	// Token: 0x04002B56 RID: 11094
	public GameObject SpyCamera;

	// Token: 0x04002B57 RID: 11095
	public Transform SpyTarget;

	// Token: 0x04002B58 RID: 11096
	public Transform SpySpot;

	// Token: 0x04002B59 RID: 11097
	public float Timer;

	// Token: 0x04002B5A RID: 11098
	public bool CanRecord;

	// Token: 0x04002B5B RID: 11099
	public bool Recording;

	// Token: 0x04002B5C RID: 11100
	public int Phase;
}
