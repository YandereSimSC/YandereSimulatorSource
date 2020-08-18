using System;
using UnityEngine;

// Token: 0x020002D5 RID: 725
public class GradingPaperScript : MonoBehaviour
{
	// Token: 0x060016CA RID: 5834 RVA: 0x000BC678 File Offset: 0x000BA878
	private void Start()
	{
		this.OriginalPosition = this.Chair.position;
	}

	// Token: 0x060016CB RID: 5835 RVA: 0x000BC68C File Offset: 0x000BA88C
	private void Update()
	{
		if (!this.Writing)
		{
			if (Vector3.Distance(this.Chair.position, this.OriginalPosition) > 0.01f)
			{
				this.Chair.position = Vector3.Lerp(this.Chair.position, this.OriginalPosition, Time.deltaTime * 10f);
				return;
			}
		}
		else if (this.Character != null)
		{
			if (Vector3.Distance(this.Chair.position, this.Character.transform.position + this.Character.transform.forward * 0.1f) > 0.01f)
			{
				this.Chair.position = Vector3.Lerp(this.Chair.position, this.Character.transform.position + this.Character.transform.forward * 0.1f, Time.deltaTime * 10f);
			}
			if (this.Phase == 1)
			{
				if (this.Teacher.CharacterAnimation["f02_deskWrite"].time > this.PickUpTime1)
				{
					this.Teacher.CharacterAnimation["f02_deskWrite"].speed = this.Speed;
					this.Paper.parent = this.LeftHand;
					this.Paper.localPosition = this.PickUpPosition1;
					this.Paper.localEulerAngles = this.PickUpRotation1;
					this.Paper.localScale = new Vector3(0.9090909f, 0.9090909f, 0.9090909f);
					this.Phase++;
				}
			}
			else if (this.Phase == 2)
			{
				if (this.Teacher.CharacterAnimation["f02_deskWrite"].time > this.SetDownTime1)
				{
					this.Paper.parent = this.Character.transform;
					this.Paper.localPosition = this.SetDownPosition1;
					this.Paper.localEulerAngles = this.SetDownRotation1;
					this.Phase++;
				}
			}
			else if (this.Phase == 3)
			{
				if (this.Teacher.CharacterAnimation["f02_deskWrite"].time > this.PickUpTime2)
				{
					this.Paper.parent = this.LeftHand;
					this.Paper.localPosition = this.PickUpPosition2;
					this.Paper.localEulerAngles = this.PickUpRotation2;
					this.Phase++;
				}
			}
			else if (this.Phase == 4)
			{
				if (this.Teacher.CharacterAnimation["f02_deskWrite"].time > this.SetDownTime2)
				{
					this.Paper.parent = this.Character.transform;
					this.Paper.localScale = Vector3.zero;
					this.Phase++;
				}
			}
			else if (this.Phase == 5 && this.Teacher.CharacterAnimation["f02_deskWrite"].time >= this.Teacher.CharacterAnimation["f02_deskWrite"].length)
			{
				this.Teacher.CharacterAnimation["f02_deskWrite"].time = 0f;
				this.Teacher.CharacterAnimation.Play("f02_deskWrite");
				this.Phase = 1;
			}
			if (this.Teacher.Actions[this.Teacher.Phase] != StudentActionType.GradePapers || !this.Teacher.Routine || this.Teacher.Stop)
			{
				this.Paper.localScale = Vector3.zero;
				this.Teacher.Obstacle.enabled = false;
				this.Teacher.Pen.SetActive(false);
				this.Writing = false;
				this.Phase = 1;
			}
		}
	}

	// Token: 0x04001DF9 RID: 7673
	public StudentScript Teacher;

	// Token: 0x04001DFA RID: 7674
	public GameObject Character;

	// Token: 0x04001DFB RID: 7675
	public Transform LeftHand;

	// Token: 0x04001DFC RID: 7676
	public Transform Chair;

	// Token: 0x04001DFD RID: 7677
	public Transform Paper;

	// Token: 0x04001DFE RID: 7678
	public float PickUpTime1;

	// Token: 0x04001DFF RID: 7679
	public float SetDownTime1;

	// Token: 0x04001E00 RID: 7680
	public float PickUpTime2;

	// Token: 0x04001E01 RID: 7681
	public float SetDownTime2;

	// Token: 0x04001E02 RID: 7682
	public Vector3 OriginalPosition;

	// Token: 0x04001E03 RID: 7683
	public Vector3 PickUpPosition1;

	// Token: 0x04001E04 RID: 7684
	public Vector3 SetDownPosition1;

	// Token: 0x04001E05 RID: 7685
	public Vector3 PickUpPosition2;

	// Token: 0x04001E06 RID: 7686
	public Vector3 PickUpRotation1;

	// Token: 0x04001E07 RID: 7687
	public Vector3 SetDownRotation1;

	// Token: 0x04001E08 RID: 7688
	public Vector3 PickUpRotation2;

	// Token: 0x04001E09 RID: 7689
	public int Phase = 1;

	// Token: 0x04001E0A RID: 7690
	public float Speed = 1f;

	// Token: 0x04001E0B RID: 7691
	public bool Writing;
}
