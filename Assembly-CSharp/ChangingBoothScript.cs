using System;
using UnityEngine;

// Token: 0x0200022E RID: 558
public class ChangingBoothScript : MonoBehaviour
{
	// Token: 0x06001229 RID: 4649 RVA: 0x000804FA File Offset: 0x0007E6FA
	private void Start()
	{
		this.CheckYandereClub();
	}

	// Token: 0x0600122A RID: 4650 RVA: 0x00080504 File Offset: 0x0007E704
	private void Update()
	{
		if (!this.Occupied && this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Yandere.EmptyHands();
			this.Yandere.CanMove = false;
			this.YandereChanging = true;
			this.Occupied = true;
			this.Prompt.Hide();
			this.Prompt.enabled = false;
		}
		if (this.Occupied)
		{
			if (this.OccupyTimer == 0f)
			{
				if (this.Yandere.transform.position.y > base.transform.position.y - 1f && this.Yandere.transform.position.y < base.transform.position.y + 1f)
				{
					this.MyAudioSource.clip = this.CurtainSound;
					this.MyAudioSource.Play();
				}
			}
			else if (this.OccupyTimer > 1f && this.Phase == 0)
			{
				if (this.Yandere.transform.position.y > base.transform.position.y - 1f && this.Yandere.transform.position.y < base.transform.position.y + 1f)
				{
					this.MyAudioSource.clip = this.ClothSound;
					this.MyAudioSource.Play();
				}
				this.Phase++;
			}
			this.OccupyTimer += Time.deltaTime;
			if (this.YandereChanging)
			{
				if (this.OccupyTimer < 2f)
				{
					this.Yandere.CharacterAnimation.CrossFade(this.Yandere.IdleAnim);
					this.Weight = Mathf.Lerp(this.Weight, 0f, Time.deltaTime * 10f);
					this.Curtains.SetBlendShapeWeight(0, this.Weight);
					this.Yandere.MoveTowardsTarget(base.transform.position);
					return;
				}
				if (this.OccupyTimer < 3f)
				{
					this.Weight = Mathf.Lerp(this.Weight, 100f, Time.deltaTime * 10f);
					this.Curtains.SetBlendShapeWeight(0, this.Weight);
					if (this.Phase < 2)
					{
						this.MyAudioSource.clip = this.CurtainSound;
						this.MyAudioSource.Play();
						if (!this.Yandere.ClubAttire)
						{
							this.Yandere.PreviousSchoolwear = this.Yandere.Schoolwear;
						}
						this.Yandere.ChangeClubwear();
						this.Phase++;
					}
					this.Yandere.transform.rotation = Quaternion.Slerp(this.Yandere.transform.rotation, base.transform.rotation, 10f * Time.deltaTime);
					this.Yandere.MoveTowardsTarget(this.ExitSpot.position);
					return;
				}
				this.YandereChanging = false;
				this.Yandere.CanMove = true;
				this.Prompt.enabled = true;
				this.Occupied = false;
				this.OccupyTimer = 0f;
				this.Phase = 0;
				return;
			}
			else
			{
				if (this.OccupyTimer < 2f)
				{
					this.Weight = Mathf.Lerp(this.Weight, 0f, Time.deltaTime * 10f);
					this.Curtains.SetBlendShapeWeight(0, this.Weight);
					return;
				}
				if (this.OccupyTimer < 3f)
				{
					this.Weight = Mathf.Lerp(this.Weight, 100f, Time.deltaTime * 10f);
					this.Curtains.SetBlendShapeWeight(0, this.Weight);
					if (this.Phase < 2)
					{
						if (this.Yandere.transform.position.y > base.transform.position.y - 1f && this.Yandere.transform.position.y < base.transform.position.y + 1f)
						{
							this.MyAudioSource.clip = this.CurtainSound;
							this.MyAudioSource.Play();
						}
						this.Student.ChangeClubwear();
						this.Phase++;
						return;
					}
				}
				else
				{
					this.Student.WalkAnim = this.Student.OriginalWalkAnim;
					this.Occupied = false;
					this.OccupyTimer = 0f;
					this.Student = null;
					this.Phase = 0;
					this.CheckYandereClub();
				}
			}
		}
	}

	// Token: 0x0600122B RID: 4651 RVA: 0x000809C4 File Offset: 0x0007EBC4
	public void CheckYandereClub()
	{
		if (ClubGlobals.Club != this.ClubID)
		{
			this.Prompt.Hide();
			this.Prompt.enabled = false;
			return;
		}
		if (this.Yandere.Bloodiness != 0f || this.CannotChange || this.Yandere.Schoolwear <= 0)
		{
			this.Prompt.Hide();
			this.Prompt.enabled = false;
			return;
		}
		if (!this.Occupied)
		{
			this.Prompt.enabled = true;
			return;
		}
		this.Prompt.Hide();
		this.Prompt.enabled = false;
	}

	// Token: 0x04001555 RID: 5461
	public YandereScript Yandere;

	// Token: 0x04001556 RID: 5462
	public StudentScript Student;

	// Token: 0x04001557 RID: 5463
	public PromptScript Prompt;

	// Token: 0x04001558 RID: 5464
	public SkinnedMeshRenderer Curtains;

	// Token: 0x04001559 RID: 5465
	public Transform ExitSpot;

	// Token: 0x0400155A RID: 5466
	public Transform[] WaitSpots;

	// Token: 0x0400155B RID: 5467
	public bool YandereChanging;

	// Token: 0x0400155C RID: 5468
	public bool CannotChange;

	// Token: 0x0400155D RID: 5469
	public bool Occupied;

	// Token: 0x0400155E RID: 5470
	public AudioSource MyAudioSource;

	// Token: 0x0400155F RID: 5471
	public AudioClip CurtainSound;

	// Token: 0x04001560 RID: 5472
	public AudioClip ClothSound;

	// Token: 0x04001561 RID: 5473
	public float OccupyTimer;

	// Token: 0x04001562 RID: 5474
	public float Weight;

	// Token: 0x04001563 RID: 5475
	public ClubType ClubID;

	// Token: 0x04001564 RID: 5476
	public int Phase;
}
