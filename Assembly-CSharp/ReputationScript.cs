using System;
using UnityEngine;

// Token: 0x0200038D RID: 909
public class ReputationScript : MonoBehaviour
{
	// Token: 0x06001993 RID: 6547 RVA: 0x000F997B File Offset: 0x000F7B7B
	private void Start()
	{
		if (MissionModeGlobals.MissionMode)
		{
			this.MissionMode = true;
		}
		this.Reputation = PlayerGlobals.Reputation;
		this.Bully();
	}

	// Token: 0x06001994 RID: 6548 RVA: 0x000F999C File Offset: 0x000F7B9C
	private void Update()
	{
		if (this.Phase == 1)
		{
			if (this.Clock.PresentTime / 60f > 8.5f)
			{
				this.Phase++;
			}
		}
		else if (this.Phase == 2)
		{
			if (this.Clock.PresentTime / 60f > 13.5f)
			{
				this.Phase++;
			}
		}
		else if (this.Phase == 3 && this.Clock.PresentTime / 60f > 18f)
		{
			this.Phase++;
		}
		if (this.PendingRep < 0f)
		{
			this.StudentManager.TutorialWindow.ShowRepMessage = true;
		}
		if (this.CheckedRep < this.Phase && !this.StudentManager.Yandere.Struggling && !this.StudentManager.Yandere.DelinquentFighting && !this.StudentManager.Yandere.Pickpocketing && !this.StudentManager.Yandere.Noticed && !this.ArmDetector.SummonDemon)
		{
			this.UpdateRep();
			if (this.Reputation <= -100f)
			{
				this.Portal.EndDay();
			}
		}
		if (!this.MissionMode)
		{
			this.CurrentRepMarker.localPosition = new Vector3(Mathf.Lerp(this.CurrentRepMarker.localPosition.x, -830f + this.Reputation * 1.5f, Time.deltaTime * 10f), this.CurrentRepMarker.localPosition.y, this.CurrentRepMarker.localPosition.z);
			this.PendingRepMarker.localPosition = new Vector3(Mathf.Lerp(this.PendingRepMarker.localPosition.x, this.CurrentRepMarker.transform.localPosition.x + this.PendingRep * 1.5f, Time.deltaTime * 10f), this.PendingRepMarker.localPosition.y, this.PendingRepMarker.localPosition.z);
		}
		else
		{
			this.PendingRepMarker.localPosition = new Vector3(Mathf.Lerp(this.PendingRepMarker.localPosition.x, -980f + this.PendingRep * -3f, Time.deltaTime * 10f), this.PendingRepMarker.localPosition.y, this.PendingRepMarker.localPosition.z);
		}
		if (this.CurrentRepMarker.localPosition.x < -980f)
		{
			this.CurrentRepMarker.localPosition = new Vector3(-980f, this.CurrentRepMarker.localPosition.y, this.CurrentRepMarker.localPosition.z);
		}
		if (this.PendingRepMarker.localPosition.x < -980f)
		{
			this.PendingRepMarker.localPosition = new Vector3(-980f, this.PendingRepMarker.localPosition.y, this.PendingRepMarker.localPosition.z);
		}
		if (this.CurrentRepMarker.localPosition.x > -680f)
		{
			this.CurrentRepMarker.localPosition = new Vector3(-680f, this.CurrentRepMarker.localPosition.y, this.CurrentRepMarker.localPosition.z);
		}
		if (this.PendingRepMarker.localPosition.x > -680f)
		{
			this.PendingRepMarker.localPosition = new Vector3(-680f, this.PendingRepMarker.localPosition.y, this.PendingRepMarker.localPosition.z);
		}
		if (!this.MissionMode)
		{
			if (this.PendingRep > 0f)
			{
				this.PendingRepLabel.text = "+" + this.PendingRep.ToString();
				return;
			}
			if (this.PendingRep < 0f)
			{
				this.PendingRepLabel.text = this.PendingRep.ToString();
				return;
			}
			this.PendingRepLabel.text = string.Empty;
			return;
		}
		else
		{
			if (this.PendingRep < 0f)
			{
				this.PendingRepLabel.text = (-this.PendingRep).ToString();
				return;
			}
			this.PendingRepLabel.text = string.Empty;
			return;
		}
	}

	// Token: 0x06001995 RID: 6549 RVA: 0x000F9DEF File Offset: 0x000F7FEF
	private void Bully()
	{
		this.FlowerVase.SetActive(false);
	}

	// Token: 0x06001996 RID: 6550 RVA: 0x000F9E00 File Offset: 0x000F8000
	public void UpdateRep()
	{
		this.Reputation += this.PendingRep;
		this.PendingRep = 0f;
		this.CheckedRep++;
		if (ClubGlobals.Club == ClubType.Delinquent && this.Reputation > -33.33333f)
		{
			this.Reputation = -33.33333f;
		}
		this.StudentManager.WipePendingRep();
	}

	// Token: 0x04002772 RID: 10098
	public StudentManagerScript StudentManager;

	// Token: 0x04002773 RID: 10099
	public ArmDetectorScript ArmDetector;

	// Token: 0x04002774 RID: 10100
	public PortalScript Portal;

	// Token: 0x04002775 RID: 10101
	public Transform CurrentRepMarker;

	// Token: 0x04002776 RID: 10102
	public Transform PendingRepMarker;

	// Token: 0x04002777 RID: 10103
	public UILabel PendingRepLabel;

	// Token: 0x04002778 RID: 10104
	public ClockScript Clock;

	// Token: 0x04002779 RID: 10105
	public float Reputation;

	// Token: 0x0400277A RID: 10106
	public float PendingRep;

	// Token: 0x0400277B RID: 10107
	public int CheckedRep = 1;

	// Token: 0x0400277C RID: 10108
	public int Phase;

	// Token: 0x0400277D RID: 10109
	public bool MissionMode;

	// Token: 0x0400277E RID: 10110
	public GameObject FlowerVase;

	// Token: 0x0400277F RID: 10111
	public GameObject Grafitti;
}
