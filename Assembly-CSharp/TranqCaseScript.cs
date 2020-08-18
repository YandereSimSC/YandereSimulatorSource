using System;
using UnityEngine;

// Token: 0x0200042C RID: 1068
public class TranqCaseScript : MonoBehaviour
{
	// Token: 0x06001C5C RID: 7260 RVA: 0x00152DDD File Offset: 0x00150FDD
	private void Start()
	{
		this.Prompt.enabled = false;
	}

	// Token: 0x06001C5D RID: 7261 RVA: 0x00152DEC File Offset: 0x00150FEC
	private void Update()
	{
		if (this.Yandere.transform.position.x > base.transform.position.x && Vector3.Distance(base.transform.position, this.Yandere.transform.position) < 1f)
		{
			if (this.Yandere.Dragging)
			{
				if (this.Ragdoll == null)
				{
					this.Ragdoll = this.Yandere.Ragdoll.GetComponent<RagdollScript>();
				}
				if (this.Ragdoll.Tranquil)
				{
					if (!this.Prompt.enabled)
					{
						this.Prompt.enabled = true;
					}
				}
				else if (this.Prompt.enabled)
				{
					this.Prompt.Hide();
					this.Prompt.enabled = false;
				}
			}
			else if (this.Prompt.enabled)
			{
				this.Prompt.Hide();
				this.Prompt.enabled = false;
			}
		}
		else if (this.Prompt.enabled)
		{
			this.Prompt.Hide();
			this.Prompt.enabled = false;
		}
		if (this.Prompt.enabled && this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Circle[0].fillAmount = 1f;
			if (!this.Yandere.Chased && this.Yandere.Chasers == 0)
			{
				this.Yandere.TranquilHiding = true;
				this.Yandere.CanMove = false;
				this.Prompt.enabled = false;
				this.Prompt.Hide();
				this.Ragdoll.TranqCase = this;
				this.VictimClubType = this.Ragdoll.Student.Club;
				this.VictimID = this.Ragdoll.StudentID;
				this.Door.Prompt.enabled = true;
				this.Door.enabled = true;
				this.Occupied = true;
				this.Animate = true;
				this.Open = true;
			}
		}
		if (this.Animate)
		{
			if (this.Open)
			{
				this.Rotation = Mathf.Lerp(this.Rotation, 105f, Time.deltaTime * 10f);
			}
			else
			{
				this.Rotation = Mathf.Lerp(this.Rotation, 0f, Time.deltaTime * 10f);
				this.Ragdoll.Student.OsanaHairL.transform.localScale = Vector3.MoveTowards(this.Ragdoll.Student.OsanaHairL.transform.localScale, new Vector3(0f, 0f, 0f), Time.deltaTime * 10f);
				this.Ragdoll.Student.OsanaHairR.transform.localScale = Vector3.MoveTowards(this.Ragdoll.Student.OsanaHairR.transform.localScale, new Vector3(0f, 0f, 0f), Time.deltaTime * 10f);
				if (this.Rotation < 1f)
				{
					this.Animate = false;
					this.Rotation = 0f;
				}
			}
			this.Hinge.localEulerAngles = new Vector3(0f, 0f, this.Rotation);
		}
	}

	// Token: 0x04003535 RID: 13621
	public YandereScript Yandere;

	// Token: 0x04003536 RID: 13622
	public RagdollScript Ragdoll;

	// Token: 0x04003537 RID: 13623
	public PromptScript Prompt;

	// Token: 0x04003538 RID: 13624
	public DoorScript Door;

	// Token: 0x04003539 RID: 13625
	public Transform Hinge;

	// Token: 0x0400353A RID: 13626
	public bool Occupied;

	// Token: 0x0400353B RID: 13627
	public bool Open;

	// Token: 0x0400353C RID: 13628
	public int VictimID;

	// Token: 0x0400353D RID: 13629
	public ClubType VictimClubType;

	// Token: 0x0400353E RID: 13630
	public float Rotation;

	// Token: 0x0400353F RID: 13631
	public bool Animate;
}
