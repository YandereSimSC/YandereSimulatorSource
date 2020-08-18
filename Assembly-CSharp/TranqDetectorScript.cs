using System;
using UnityEngine;

// Token: 0x0200042D RID: 1069
public class TranqDetectorScript : MonoBehaviour
{
	// Token: 0x06001C5F RID: 7263 RVA: 0x00153156 File Offset: 0x00151356
	private void Start()
	{
		this.Checklist.alpha = 0f;
	}

	// Token: 0x06001C60 RID: 7264 RVA: 0x00153168 File Offset: 0x00151368
	private void Update()
	{
		if (this.StopChecking)
		{
			this.Checklist.alpha = Mathf.MoveTowards(this.Checklist.alpha, 0f, Time.deltaTime);
			if (this.Checklist.alpha == 0f)
			{
				base.enabled = false;
			}
			return;
		}
		if (this.MyCollider.bounds.Contains(this.Yandere.transform.position))
		{
			if (SchoolGlobals.KidnapVictim > 0)
			{
				this.KidnappingLabel.text = "There is no room for another prisoner in your basement.";
			}
			else
			{
				if (this.Yandere.Inventory.Tranquilizer || this.Yandere.Inventory.Sedative)
				{
					this.TranquilizerIcon.spriteName = "Yes";
				}
				else
				{
					this.TranquilizerIcon.spriteName = "No";
				}
				if (this.Yandere.Followers != 1)
				{
					this.FollowerIcon.spriteName = "No";
				}
				else if (this.Yandere.Follower.Male)
				{
					this.KidnappingLabel.text = "You cannot kidnap male students at this point in time.";
					this.FollowerIcon.spriteName = "No";
				}
				else
				{
					this.KidnappingLabel.text = "Kidnapping Checklist";
					this.FollowerIcon.spriteName = "Yes";
				}
				this.BiologyIcon.spriteName = ((ClassGlobals.BiologyGrade + ClassGlobals.BiologyBonus != 0) ? "Yes" : "No");
				if (!this.Yandere.Armed)
				{
					this.SyringeIcon.spriteName = "No";
				}
				else if (this.Yandere.EquippedWeapon.WeaponID != 3)
				{
					this.SyringeIcon.spriteName = "No";
				}
				else
				{
					this.SyringeIcon.spriteName = "Yes";
				}
				if (this.Door.Open || this.Door.Timer < 1f)
				{
					this.DoorIcon.spriteName = "No";
				}
				else
				{
					this.DoorIcon.spriteName = "Yes";
				}
			}
			this.Checklist.alpha = Mathf.MoveTowards(this.Checklist.alpha, 1f, Time.deltaTime);
			return;
		}
		this.Checklist.alpha = Mathf.MoveTowards(this.Checklist.alpha, 0f, Time.deltaTime);
	}

	// Token: 0x06001C61 RID: 7265 RVA: 0x001533C4 File Offset: 0x001515C4
	public void TranqCheck()
	{
		if (!this.StopChecking && this.KidnappingLabel.text == "Kidnapping Checklist" && this.TranquilizerIcon.spriteName == "Yes" && this.FollowerIcon.spriteName == "Yes" && this.BiologyIcon.spriteName == "Yes" && this.SyringeIcon.spriteName == "Yes" && this.DoorIcon.spriteName == "Yes")
		{
			AudioSource component = base.GetComponent<AudioSource>();
			component.clip = this.TranqClips[UnityEngine.Random.Range(0, this.TranqClips.Length)];
			component.Play();
			this.Door.Prompt.Hide();
			this.Door.Prompt.enabled = false;
			this.Door.enabled = false;
			this.Yandere.Inventory.Tranquilizer = false;
			if (!this.Yandere.Follower.Male)
			{
				this.Yandere.CanTranq = true;
			}
			this.Yandere.EquippedWeapon.Type = WeaponType.Syringe;
			this.Yandere.AttackManager.Stealth = true;
			this.StopChecking = true;
		}
	}

	// Token: 0x04003540 RID: 13632
	public YandereScript Yandere;

	// Token: 0x04003541 RID: 13633
	public DoorScript Door;

	// Token: 0x04003542 RID: 13634
	public UIPanel Checklist;

	// Token: 0x04003543 RID: 13635
	public Collider MyCollider;

	// Token: 0x04003544 RID: 13636
	public UILabel KidnappingLabel;

	// Token: 0x04003545 RID: 13637
	public UISprite TranquilizerIcon;

	// Token: 0x04003546 RID: 13638
	public UISprite FollowerIcon;

	// Token: 0x04003547 RID: 13639
	public UISprite BiologyIcon;

	// Token: 0x04003548 RID: 13640
	public UISprite SyringeIcon;

	// Token: 0x04003549 RID: 13641
	public UISprite DoorIcon;

	// Token: 0x0400354A RID: 13642
	public bool StopChecking;

	// Token: 0x0400354B RID: 13643
	public AudioClip[] TranqClips;
}
