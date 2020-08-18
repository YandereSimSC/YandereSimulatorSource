using System;
using UnityEngine;

// Token: 0x0200039C RID: 924
public class OsanaThursdayAfterClassEventScript : MonoBehaviour
{
	// Token: 0x04002829 RID: 10281
	public StudentManagerScript StudentManager;

	// Token: 0x0400282A RID: 10282
	public PhoneMinigameScript PhoneMinigame;

	// Token: 0x0400282B RID: 10283
	public JukeboxScript Jukebox;

	// Token: 0x0400282C RID: 10284
	public UILabel EventSubtitle;

	// Token: 0x0400282D RID: 10285
	public YandereScript Yandere;

	// Token: 0x0400282E RID: 10286
	public ClockScript Clock;

	// Token: 0x0400282F RID: 10287
	public StudentScript Friend;

	// Token: 0x04002830 RID: 10288
	public StudentScript Rival;

	// Token: 0x04002831 RID: 10289
	public Transform FriendLocation;

	// Token: 0x04002832 RID: 10290
	public Transform Location;

	// Token: 0x04002833 RID: 10291
	public AudioClip[] SpeechClip;

	// Token: 0x04002834 RID: 10292
	public string[] SpeechText;

	// Token: 0x04002835 RID: 10293
	public string[] EventAnim;

	// Token: 0x04002836 RID: 10294
	public GameObject AlarmDisc;

	// Token: 0x04002837 RID: 10295
	public GameObject VoiceClip;

	// Token: 0x04002838 RID: 10296
	public float FriendWarningTimer;

	// Token: 0x04002839 RID: 10297
	public float Distance;

	// Token: 0x0400283A RID: 10298
	public float Scale;

	// Token: 0x0400283B RID: 10299
	public float Timer;

	// Token: 0x0400283C RID: 10300
	public DayOfWeek EventDay;

	// Token: 0x0400283D RID: 10301
	public int FriendID = 10;

	// Token: 0x0400283E RID: 10302
	public int RivalID = 11;

	// Token: 0x0400283F RID: 10303
	public int Phase;

	// Token: 0x04002840 RID: 10304
	public int Frame;

	// Token: 0x04002841 RID: 10305
	public bool FriendWarned;

	// Token: 0x04002842 RID: 10306
	public bool Sabotaged;

	// Token: 0x04002843 RID: 10307
	public Vector3 OriginalPosition;

	// Token: 0x04002844 RID: 10308
	public Vector3 OriginalRotation;
}
