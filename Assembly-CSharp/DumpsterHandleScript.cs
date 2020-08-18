using System;
using UnityEngine;

// Token: 0x02000276 RID: 630
public class DumpsterHandleScript : MonoBehaviour
{
	// Token: 0x06001371 RID: 4977 RVA: 0x000A6F67 File Offset: 0x000A5167
	private void Start()
	{
		this.Panel.SetActive(false);
	}

	// Token: 0x06001372 RID: 4978 RVA: 0x000A6F78 File Offset: 0x000A5178
	private void Update()
	{
		this.Prompt.HideButton[3] = (this.Prompt.Yandere.PickUp != null || this.Prompt.Yandere.Dragging || this.Prompt.Yandere.Carrying);
		if (this.Prompt.Circle[3].fillAmount == 0f)
		{
			this.Prompt.Circle[3].fillAmount = 1f;
			if (!this.Prompt.Yandere.Chased && this.Prompt.Yandere.Chasers == 0)
			{
				this.Prompt.Yandere.DumpsterGrabbing = true;
				this.Prompt.Yandere.DumpsterHandle = this;
				this.Prompt.Yandere.CanMove = false;
				this.PromptBar.ClearButtons();
				this.PromptBar.Label[1].text = "STOP";
				this.PromptBar.Label[5].text = "PUSH / PULL";
				this.PromptBar.UpdateButtons();
				this.PromptBar.Show = true;
				this.Grabbed = true;
			}
		}
		if (this.Grabbed)
		{
			this.Prompt.Yandere.transform.rotation = Quaternion.Lerp(this.Prompt.Yandere.transform.rotation, this.GrabSpot.rotation, Time.deltaTime * 10f);
			if (Vector3.Distance(this.Prompt.Yandere.transform.position, this.GrabSpot.position) > 0.1f)
			{
				this.Prompt.Yandere.MoveTowardsTarget(this.GrabSpot.position);
			}
			else
			{
				this.Prompt.Yandere.transform.position = this.GrabSpot.position;
			}
			if (Input.GetAxis("Horizontal") > 0.5f || Input.GetAxis("DpadX") > 0.5f || Input.GetKey("right"))
			{
				base.transform.parent.transform.position = new Vector3(base.transform.parent.transform.position.x, base.transform.parent.transform.position.y, base.transform.parent.transform.position.z - Time.deltaTime);
			}
			else if (Input.GetAxis("Horizontal") < -0.5f || Input.GetAxis("DpadX") < -0.5f || Input.GetKey("left"))
			{
				base.transform.parent.transform.position = new Vector3(base.transform.parent.transform.position.x, base.transform.parent.transform.position.y, base.transform.parent.transform.position.z + Time.deltaTime);
			}
			if (this.PullLimit < this.PushLimit)
			{
				if (base.transform.parent.transform.position.z < this.PullLimit)
				{
					base.transform.parent.transform.position = new Vector3(base.transform.parent.transform.position.x, base.transform.parent.transform.position.y, this.PullLimit);
				}
				else if (base.transform.parent.transform.position.z > this.PushLimit)
				{
					base.transform.parent.transform.position = new Vector3(base.transform.parent.transform.position.x, base.transform.parent.transform.position.y, this.PushLimit);
				}
			}
			else if (base.transform.parent.transform.position.z > this.PullLimit)
			{
				base.transform.parent.transform.position = new Vector3(base.transform.parent.transform.position.x, base.transform.parent.transform.position.y, this.PullLimit);
			}
			else if (base.transform.parent.transform.position.z < this.PushLimit)
			{
				base.transform.parent.transform.position = new Vector3(base.transform.parent.transform.position.x, base.transform.parent.transform.position.y, this.PushLimit);
			}
			this.Panel.SetActive(this.DumpsterLid.transform.position.z > this.DumpsterLid.DisposalSpot - 0.05f && this.DumpsterLid.transform.position.z < this.DumpsterLid.DisposalSpot + 0.05f);
			if (this.Prompt.Yandere.Chased || this.Prompt.Yandere.Chasers > 0 || Input.GetButtonDown("B"))
			{
				this.StopGrabbing();
			}
		}
	}

	// Token: 0x06001373 RID: 4979 RVA: 0x000A753C File Offset: 0x000A573C
	private void StopGrabbing()
	{
		this.Prompt.Yandere.DumpsterGrabbing = false;
		this.Prompt.Yandere.CanMove = true;
		this.PromptBar.ClearButtons();
		this.PromptBar.Show = false;
		this.Panel.SetActive(false);
		this.Grabbed = false;
	}

	// Token: 0x04001A91 RID: 6801
	public DumpsterLidScript DumpsterLid;

	// Token: 0x04001A92 RID: 6802
	public PromptBarScript PromptBar;

	// Token: 0x04001A93 RID: 6803
	public PromptScript Prompt;

	// Token: 0x04001A94 RID: 6804
	public Transform GrabSpot;

	// Token: 0x04001A95 RID: 6805
	public GameObject Panel;

	// Token: 0x04001A96 RID: 6806
	public bool Grabbed;

	// Token: 0x04001A97 RID: 6807
	public float Direction;

	// Token: 0x04001A98 RID: 6808
	public float PullLimit;

	// Token: 0x04001A99 RID: 6809
	public float PushLimit;
}
