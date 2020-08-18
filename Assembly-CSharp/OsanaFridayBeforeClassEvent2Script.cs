using System;
using UnityEngine;

// Token: 0x02000397 RID: 919
public class OsanaFridayBeforeClassEvent2Script : MonoBehaviour
{
	// Token: 0x040027C9 RID: 10185
	public OsanaFridayBeforeClassEvent1Script OtherEvent;

	// Token: 0x040027CA RID: 10186
	public StudentManagerScript StudentManager;

	// Token: 0x040027CB RID: 10187
	public AudioSoftwareScript AudioSoftware;

	// Token: 0x040027CC RID: 10188
	public JukeboxScript Jukebox;

	// Token: 0x040027CD RID: 10189
	public UILabel EventSubtitle;

	// Token: 0x040027CE RID: 10190
	public YandereScript Yandere;

	// Token: 0x040027CF RID: 10191
	public ClockScript Clock;

	// Token: 0x040027D0 RID: 10192
	public SpyScript Spy;

	// Token: 0x040027D1 RID: 10193
	public StudentScript Ganguro;

	// Token: 0x040027D2 RID: 10194
	public StudentScript Friend;

	// Token: 0x040027D3 RID: 10195
	public StudentScript Rival;

	// Token: 0x040027D4 RID: 10196
	public Transform[] Location;

	// Token: 0x040027D5 RID: 10197
	public AudioClip[] SpeechClip;

	// Token: 0x040027D6 RID: 10198
	public string[] SpeechText;

	// Token: 0x040027D7 RID: 10199
	public float[] SpeechTime;

	// Token: 0x040027D8 RID: 10200
	public string[] EventAnim;

	// Token: 0x040027D9 RID: 10201
	public GameObject AlarmDisc;

	// Token: 0x040027DA RID: 10202
	public GameObject VoiceClip;

	// Token: 0x040027DB RID: 10203
	public Quaternion targetRotation;

	// Token: 0x040027DC RID: 10204
	public float Distance;

	// Token: 0x040027DD RID: 10205
	public float Scale;

	// Token: 0x040027DE RID: 10206
	public float Timer;

	// Token: 0x040027DF RID: 10207
	public DayOfWeek EventDay;

	// Token: 0x040027E0 RID: 10208
	public int SpeechPhase = 1;

	// Token: 0x040027E1 RID: 10209
	public int GanguroID = 81;

	// Token: 0x040027E2 RID: 10210
	public int FriendID = 10;

	// Token: 0x040027E3 RID: 10211
	public int RivalID = 11;

	// Token: 0x040027E4 RID: 10212
	public int Phase;

	// Token: 0x040027E5 RID: 10213
	public int Frame;

	// Token: 0x040027E6 RID: 10214
	public Vector3 OriginalPosition;

	// Token: 0x040027E7 RID: 10215
	public Vector3 OriginalRotation;
}
