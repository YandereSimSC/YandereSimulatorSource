using System;
using UnityEngine;

// Token: 0x02000399 RID: 921
public class OsanaMondayBeforeClassEventScript : MonoBehaviour
{
	// Token: 0x060019B9 RID: 6585 RVA: 0x000FBA58 File Offset: 0x000F9C58
	private void Start()
	{
		this.EventSubtitle.transform.localScale = Vector3.zero;
		this.Bentos[1].SetActive(false);
		this.Bentos[2].SetActive(false);
		if (DateGlobals.Weekday != DayOfWeek.Monday)
		{
			base.enabled = false;
		}
	}

	// Token: 0x040027E8 RID: 10216
	public StudentManagerScript StudentManager;

	// Token: 0x040027E9 RID: 10217
	public EventManagerScript NextEvent;

	// Token: 0x040027EA RID: 10218
	public JukeboxScript Jukebox;

	// Token: 0x040027EB RID: 10219
	public UILabel EventSubtitle;

	// Token: 0x040027EC RID: 10220
	public YandereScript Yandere;

	// Token: 0x040027ED RID: 10221
	public ClockScript Clock;

	// Token: 0x040027EE RID: 10222
	public StudentScript Rival;

	// Token: 0x040027EF RID: 10223
	public Transform Destination;

	// Token: 0x040027F0 RID: 10224
	public AudioClip SpeechClip;

	// Token: 0x040027F1 RID: 10225
	public string[] SpeechText;

	// Token: 0x040027F2 RID: 10226
	public float[] SpeechTime;

	// Token: 0x040027F3 RID: 10227
	public GameObject AlarmDisc;

	// Token: 0x040027F4 RID: 10228
	public GameObject VoiceClip;

	// Token: 0x040027F5 RID: 10229
	public GameObject[] Bentos;

	// Token: 0x040027F6 RID: 10230
	public bool EventActive;

	// Token: 0x040027F7 RID: 10231
	public float Distance;

	// Token: 0x040027F8 RID: 10232
	public float Scale;

	// Token: 0x040027F9 RID: 10233
	public float Timer;

	// Token: 0x040027FA RID: 10234
	public int SpeechPhase = 1;

	// Token: 0x040027FB RID: 10235
	public int RivalID = 11;

	// Token: 0x040027FC RID: 10236
	public int Phase;

	// Token: 0x040027FD RID: 10237
	public int Frame;
}
