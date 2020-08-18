using System;
using UnityEngine;

// Token: 0x0200034D RID: 845
public class ActivateOsuScript : MonoBehaviour
{
	// Token: 0x06001890 RID: 6288 RVA: 0x000E0CFC File Offset: 0x000DEEFC
	private void Start()
	{
		this.OsuScripts = this.Osu.GetComponents<OsuScript>();
		this.OriginalMouseRotation = this.Mouse.transform.eulerAngles;
		this.OriginalMousePosition = this.Mouse.transform.position;
	}

	// Token: 0x06001891 RID: 6289 RVA: 0x000E0D3C File Offset: 0x000DEF3C
	private void Update()
	{
		if (this.Student == null)
		{
			this.Student = this.StudentManager.Students[this.PlayerID];
			return;
		}
		if (!this.Osu.activeInHierarchy)
		{
			if (Vector3.Distance(base.transform.position, this.Student.transform.position) < 0.1f && this.Student.Routine && this.Student.CurrentDestination == this.Student.Destinations[this.Student.Phase] && this.Student.Actions[this.Student.Phase] == StudentActionType.Gaming)
			{
				this.ActivateOsu();
				return;
			}
		}
		else
		{
			this.Mouse.transform.eulerAngles = this.OriginalMouseRotation;
			if (!this.Student.Routine || this.Student.CurrentDestination != this.Student.Destinations[this.Student.Phase] || this.Student.Actions[this.Student.Phase] != StudentActionType.Gaming)
			{
				this.DeactivateOsu();
			}
		}
	}

	// Token: 0x06001892 RID: 6290 RVA: 0x000E0E74 File Offset: 0x000DF074
	private void ActivateOsu()
	{
		this.Osu.transform.parent.gameObject.SetActive(true);
		this.Student.SmartPhone.SetActive(false);
		this.Music.SetActive(true);
		this.Mouse.parent = this.Student.RightHand;
		this.Mouse.transform.localPosition = Vector3.zero;
	}

	// Token: 0x06001893 RID: 6291 RVA: 0x000E0EE4 File Offset: 0x000DF0E4
	private void DeactivateOsu()
	{
		this.Osu.transform.parent.gameObject.SetActive(false);
		this.Music.SetActive(false);
		OsuScript[] osuScripts = this.OsuScripts;
		for (int i = 0; i < osuScripts.Length; i++)
		{
			osuScripts[i].Timer = 0f;
		}
		this.Mouse.parent = base.transform.parent;
		this.Mouse.transform.position = this.OriginalMousePosition;
	}

	// Token: 0x04002424 RID: 9252
	public StudentManagerScript StudentManager;

	// Token: 0x04002425 RID: 9253
	public OsuScript[] OsuScripts;

	// Token: 0x04002426 RID: 9254
	public StudentScript Student;

	// Token: 0x04002427 RID: 9255
	public ClockScript Clock;

	// Token: 0x04002428 RID: 9256
	public GameObject Music;

	// Token: 0x04002429 RID: 9257
	public Transform Mouse;

	// Token: 0x0400242A RID: 9258
	public GameObject Osu;

	// Token: 0x0400242B RID: 9259
	public int PlayerID;

	// Token: 0x0400242C RID: 9260
	public Vector3 OriginalMousePosition;

	// Token: 0x0400242D RID: 9261
	public Vector3 OriginalMouseRotation;
}
