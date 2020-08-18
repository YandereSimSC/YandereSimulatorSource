using System;
using UnityEngine;

// Token: 0x0200039B RID: 923
public class OsanaMorningFriendEventScript : MonoBehaviour
{
	// Token: 0x040027FE RID: 10238
	public RivalMorningEventManagerScript OtherEvent;

	// Token: 0x040027FF RID: 10239
	public StudentManagerScript StudentManager;

	// Token: 0x04002800 RID: 10240
	public EndOfDayScript EndOfDay;

	// Token: 0x04002801 RID: 10241
	public JukeboxScript Jukebox;

	// Token: 0x04002802 RID: 10242
	public UILabel EventSubtitle;

	// Token: 0x04002803 RID: 10243
	public YandereScript Yandere;

	// Token: 0x04002804 RID: 10244
	public ClockScript Clock;

	// Token: 0x04002805 RID: 10245
	public SpyScript Spy;

	// Token: 0x04002806 RID: 10246
	public StudentScript CurrentSpeaker;

	// Token: 0x04002807 RID: 10247
	public StudentScript Friend;

	// Token: 0x04002808 RID: 10248
	public StudentScript Rival;

	// Token: 0x04002809 RID: 10249
	public Transform Epicenter;

	// Token: 0x0400280A RID: 10250
	public Transform[] Location;

	// Token: 0x0400280B RID: 10251
	public AudioClip SpeechClip;

	// Token: 0x0400280C RID: 10252
	public string[] SpeechText;

	// Token: 0x0400280D RID: 10253
	public float[] SpeechTime;

	// Token: 0x0400280E RID: 10254
	public string[] EventAnim;

	// Token: 0x0400280F RID: 10255
	public int[] Speaker;

	// Token: 0x04002810 RID: 10256
	public AudioClip InterruptedClip;

	// Token: 0x04002811 RID: 10257
	public string[] InterruptedSpeech;

	// Token: 0x04002812 RID: 10258
	public float[] InterruptedTime;

	// Token: 0x04002813 RID: 10259
	public string[] InterruptedAnim;

	// Token: 0x04002814 RID: 10260
	public int[] InterruptedSpeaker;

	// Token: 0x04002815 RID: 10261
	public AudioClip AltSpeechClip;

	// Token: 0x04002816 RID: 10262
	public string[] AltSpeechText;

	// Token: 0x04002817 RID: 10263
	public float[] AltSpeechTime;

	// Token: 0x04002818 RID: 10264
	public string[] AltEventAnim;

	// Token: 0x04002819 RID: 10265
	public int[] AltSpeaker;

	// Token: 0x0400281A RID: 10266
	public GameObject AlarmDisc;

	// Token: 0x0400281B RID: 10267
	public GameObject VoiceClip;

	// Token: 0x0400281C RID: 10268
	public Quaternion targetRotation;

	// Token: 0x0400281D RID: 10269
	public float Distance;

	// Token: 0x0400281E RID: 10270
	public float Scale;

	// Token: 0x0400281F RID: 10271
	public float Timer;

	// Token: 0x04002820 RID: 10272
	public DayOfWeek EventDay;

	// Token: 0x04002821 RID: 10273
	public int SpeechPhase = 1;

	// Token: 0x04002822 RID: 10274
	public int FriendID = 6;

	// Token: 0x04002823 RID: 10275
	public int RivalID = 11;

	// Token: 0x04002824 RID: 10276
	public int Phase;

	// Token: 0x04002825 RID: 10277
	public int Frame;

	// Token: 0x04002826 RID: 10278
	public Vector3 OriginalPosition;

	// Token: 0x04002827 RID: 10279
	public Vector3 OriginalRotation;

	// Token: 0x04002828 RID: 10280
	public bool LosingFriend;
}
