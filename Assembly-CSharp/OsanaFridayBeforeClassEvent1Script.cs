using System;
using UnityEngine;

// Token: 0x02000396 RID: 918
public class OsanaFridayBeforeClassEvent1Script : MonoBehaviour
{
	// Token: 0x060019B5 RID: 6581 RVA: 0x000FB9E7 File Offset: 0x000F9BE7
	private void Start()
	{
		this.EventSubtitle.transform.localScale = Vector3.zero;
		if (DateGlobals.Weekday != this.EventDay)
		{
			base.enabled = false;
		}
		this.Yoogle.SetActive(false);
	}

	// Token: 0x040027B2 RID: 10162
	public OsanaFridayBeforeClassEvent2Script OtherEvent;

	// Token: 0x040027B3 RID: 10163
	public StudentManagerScript StudentManager;

	// Token: 0x040027B4 RID: 10164
	public JukeboxScript Jukebox;

	// Token: 0x040027B5 RID: 10165
	public UILabel EventSubtitle;

	// Token: 0x040027B6 RID: 10166
	public YandereScript Yandere;

	// Token: 0x040027B7 RID: 10167
	public ClockScript Clock;

	// Token: 0x040027B8 RID: 10168
	public StudentScript Rival;

	// Token: 0x040027B9 RID: 10169
	public Transform Location;

	// Token: 0x040027BA RID: 10170
	public AudioClip[] SpeechClip;

	// Token: 0x040027BB RID: 10171
	public string[] SpeechText;

	// Token: 0x040027BC RID: 10172
	public string EventAnim;

	// Token: 0x040027BD RID: 10173
	public GameObject AlarmDisc;

	// Token: 0x040027BE RID: 10174
	public GameObject VoiceClip;

	// Token: 0x040027BF RID: 10175
	public GameObject Yoogle;

	// Token: 0x040027C0 RID: 10176
	public float Distance;

	// Token: 0x040027C1 RID: 10177
	public float Scale;

	// Token: 0x040027C2 RID: 10178
	public float Timer;

	// Token: 0x040027C3 RID: 10179
	public DayOfWeek EventDay;

	// Token: 0x040027C4 RID: 10180
	public int RivalID = 11;

	// Token: 0x040027C5 RID: 10181
	public int Phase;

	// Token: 0x040027C6 RID: 10182
	public int Frame;

	// Token: 0x040027C7 RID: 10183
	public Vector3 OriginalPosition;

	// Token: 0x040027C8 RID: 10184
	public Vector3 OriginalRotation;
}
