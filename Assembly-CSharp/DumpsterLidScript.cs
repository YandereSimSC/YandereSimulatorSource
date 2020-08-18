using System;
using UnityEngine;

// Token: 0x02000277 RID: 631
public class DumpsterLidScript : MonoBehaviour
{
	// Token: 0x06001375 RID: 4981 RVA: 0x000A7595 File Offset: 0x000A5795
	private void Start()
	{
		this.FallChecker.SetActive(false);
		this.Prompt.HideButton[3] = true;
	}

	// Token: 0x06001376 RID: 4982 RVA: 0x000A75B4 File Offset: 0x000A57B4
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Circle[0].fillAmount = 1f;
			if (!this.Open)
			{
				this.Prompt.Label[0].text = "     Close";
				this.Open = true;
			}
			else
			{
				this.Prompt.Label[0].text = "     Open";
				this.Open = false;
			}
		}
		if (!this.Open)
		{
			this.Rotation = Mathf.Lerp(this.Rotation, 0f, Time.deltaTime * 10f);
			this.Prompt.HideButton[3] = true;
		}
		else
		{
			this.Rotation = Mathf.Lerp(this.Rotation, -115f, Time.deltaTime * 10f);
			if (this.Corpse != null)
			{
				if (this.Prompt.Yandere.PickUp != null)
				{
					this.Prompt.HideButton[3] = !this.Prompt.Yandere.PickUp.Garbage;
				}
				else
				{
					this.Prompt.HideButton[3] = true;
				}
			}
			else
			{
				this.Prompt.HideButton[3] = true;
			}
			if (this.Prompt.Circle[3].fillAmount == 0f)
			{
				UnityEngine.Object.Destroy(this.Prompt.Yandere.PickUp.gameObject);
				this.Prompt.Circle[3].fillAmount = 1f;
				this.Prompt.HideButton[3] = false;
				this.Fill = true;
			}
			if (base.transform.position.z > this.DisposalSpot - 0.05f && base.transform.position.z < this.DisposalSpot + 0.05f)
			{
				this.FallChecker.SetActive(this.Prompt.Yandere.RoofPush);
			}
			else
			{
				this.FallChecker.SetActive(false);
			}
			if (this.Slide)
			{
				base.transform.eulerAngles = Vector3.Lerp(base.transform.eulerAngles, this.SlideLocation.eulerAngles, Time.deltaTime * 10f);
				base.transform.position = Vector3.Lerp(base.transform.position, this.SlideLocation.position, Time.deltaTime * 10f);
				this.Corpse.GetComponent<RagdollScript>().Student.Hips.position = base.transform.position + new Vector3(0f, 1f, 0f);
				if (Vector3.Distance(base.transform.position, this.SlideLocation.position) < 0.01f)
				{
					this.DragPrompts[0].enabled = false;
					this.DragPrompts[1].enabled = false;
					this.FallChecker.SetActive(false);
					this.Slide = false;
				}
			}
		}
		this.Hinge.localEulerAngles = new Vector3(this.Rotation, 0f, 0f);
		if (this.Fill)
		{
			this.GarbageDebris.localPosition = new Vector3(this.GarbageDebris.localPosition.x, Mathf.Lerp(this.GarbageDebris.localPosition.y, 1f, Time.deltaTime * 10f), this.GarbageDebris.localPosition.z);
			if (this.GarbageDebris.localPosition.y > 0.99f)
			{
				this.Prompt.Yandere.Police.SuicideScene = false;
				this.Prompt.Yandere.Police.Suicide = false;
				this.Prompt.Yandere.Police.HiddenCorpses--;
				this.Prompt.Yandere.Police.Corpses--;
				if (this.Corpse.GetComponent<RagdollScript>().AddingToCount)
				{
					this.Prompt.Yandere.NearBodies--;
				}
				this.GarbageDebris.localPosition = new Vector3(this.GarbageDebris.localPosition.x, 1f, this.GarbageDebris.localPosition.z);
				this.StudentToGoMissing = this.Corpse.GetComponent<StudentScript>().StudentID;
				UnityEngine.Object.Destroy(this.Corpse);
				this.Fill = false;
				this.Prompt.Yandere.StudentManager.UpdateStudents(0);
			}
		}
	}

	// Token: 0x06001377 RID: 4983 RVA: 0x000A7A5C File Offset: 0x000A5C5C
	public void SetVictimMissing()
	{
		StudentGlobals.SetStudentMissing(this.StudentToGoMissing, true);
	}

	// Token: 0x04001A9A RID: 6810
	public StudentScript Victim;

	// Token: 0x04001A9B RID: 6811
	public Transform SlideLocation;

	// Token: 0x04001A9C RID: 6812
	public Transform GarbageDebris;

	// Token: 0x04001A9D RID: 6813
	public Transform Hinge;

	// Token: 0x04001A9E RID: 6814
	public GameObject FallChecker;

	// Token: 0x04001A9F RID: 6815
	public GameObject Corpse;

	// Token: 0x04001AA0 RID: 6816
	public PromptScript[] DragPrompts;

	// Token: 0x04001AA1 RID: 6817
	public PromptScript Prompt;

	// Token: 0x04001AA2 RID: 6818
	public float DisposalSpot;

	// Token: 0x04001AA3 RID: 6819
	public float Rotation;

	// Token: 0x04001AA4 RID: 6820
	public bool Slide;

	// Token: 0x04001AA5 RID: 6821
	public bool Fill;

	// Token: 0x04001AA6 RID: 6822
	public bool Open;

	// Token: 0x04001AA7 RID: 6823
	public int StudentToGoMissing;
}
