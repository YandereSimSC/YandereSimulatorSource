using System;
using UnityEngine;

// Token: 0x020003D5 RID: 981
public class SewingMachineScript : MonoBehaviour
{
	// Token: 0x06001A6C RID: 6764 RVA: 0x0010348E File Offset: 0x0010168E
	private void Start()
	{
		if (TaskGlobals.GetTaskStatus(30) == 1)
		{
			this.Check = true;
			return;
		}
		if (TaskGlobals.GetTaskStatus(30) > 2)
		{
			base.enabled = false;
		}
	}

	// Token: 0x06001A6D RID: 6765 RVA: 0x001034B4 File Offset: 0x001016B4
	private void Update()
	{
		if (this.Check)
		{
			if (this.Yandere.PickUp != null)
			{
				if (this.Yandere.PickUp.Clothing && this.Yandere.PickUp.GetComponent<FoldedUniformScript>().Clean && this.Yandere.PickUp.GetComponent<FoldedUniformScript>().Type == 1 && this.Yandere.PickUp.gameObject.GetComponent<FoldedUniformScript>().Type == 1)
				{
					this.Prompt.enabled = true;
				}
			}
			else
			{
				this.Prompt.Hide();
				this.Prompt.enabled = false;
			}
		}
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Circle[0].fillAmount = 1f;
			if (!this.Yandere.Chased && this.Yandere.Chasers == 0)
			{
				this.Yandere.Character.GetComponent<Animation>().CrossFade("f02_sewing_00");
				this.Yandere.MyController.radius = 0.1f;
				this.Yandere.CanMove = false;
				this.Chair.enabled = false;
				this.Sewing = true;
				base.GetComponent<AudioSource>().Play();
				this.Uniform = this.Yandere.PickUp;
				this.Yandere.EmptyHands();
				this.Uniform.transform.parent = this.Yandere.RightHand;
				this.Uniform.transform.localPosition = new Vector3(0f, 0f, 0.09f);
				this.Uniform.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
				this.Uniform.MyRigidbody.useGravity = false;
				this.Uniform.MyCollider.enabled = false;
			}
		}
		if (this.Sewing)
		{
			this.Timer += Time.deltaTime;
			if (this.Timer < 5f)
			{
				this.targetRotation = Quaternion.LookRotation(base.transform.parent.transform.parent.position - this.Yandere.transform.position);
				this.Yandere.transform.rotation = Quaternion.Slerp(this.Yandere.transform.rotation, this.targetRotation, Time.deltaTime * 10f);
				this.Yandere.MoveTowardsTarget(this.Chair.transform.position);
				return;
			}
			if (!this.MoveAway)
			{
				this.Yandere.Character.GetComponent<Animation>().CrossFade(this.Yandere.IdleAnim);
				this.Yandere.Inventory.ModifiedUniform = true;
				this.StudentManager.Students[30].TaskPhase = 5;
				TaskGlobals.SetTaskStatus(30, 2);
				UnityEngine.Object.Destroy(this.Uniform.gameObject);
				this.MoveAway = true;
				this.Check = false;
				return;
			}
			this.Yandere.MoveTowardsTarget(this.Chair.gameObject.transform.position + new Vector3(-0.5f, 0f, 0f));
			if (this.Timer > 6f)
			{
				this.Yandere.MyController.radius = 0.2f;
				this.Yandere.CanMove = true;
				this.Chair.enabled = true;
				base.enabled = false;
				this.Sewing = false;
				this.Prompt.Hide();
				this.Prompt.enabled = false;
			}
		}
	}

	// Token: 0x040029FD RID: 10749
	public StudentManagerScript StudentManager;

	// Token: 0x040029FE RID: 10750
	public YandereScript Yandere;

	// Token: 0x040029FF RID: 10751
	public PromptScript Prompt;

	// Token: 0x04002A00 RID: 10752
	public Quaternion targetRotation;

	// Token: 0x04002A01 RID: 10753
	public PickUpScript Uniform;

	// Token: 0x04002A02 RID: 10754
	public Collider Chair;

	// Token: 0x04002A03 RID: 10755
	public bool MoveAway;

	// Token: 0x04002A04 RID: 10756
	public bool Sewing;

	// Token: 0x04002A05 RID: 10757
	public bool Check;

	// Token: 0x04002A06 RID: 10758
	public float Timer;
}
