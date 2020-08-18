using System;
using MaidDereMinigame.Malee;
using UnityEngine;

namespace MaidDereMinigame
{
	// Token: 0x020004F7 RID: 1271
	public class SFXController : MonoBehaviour
	{
		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x06002001 RID: 8193 RVA: 0x0018517C File Offset: 0x0018337C
		public static SFXController Instance
		{
			get
			{
				if (SFXController.instance == null)
				{
					SFXController.instance = UnityEngine.Object.FindObjectOfType<SFXController>();
				}
				return SFXController.instance;
			}
		}

		// Token: 0x06002002 RID: 8194 RVA: 0x0018519A File Offset: 0x0018339A
		private void Awake()
		{
			if (SFXController.Instance != this)
			{
				UnityEngine.Object.Destroy(base.gameObject);
				return;
			}
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}

		// Token: 0x06002003 RID: 8195 RVA: 0x001851C0 File Offset: 0x001833C0
		public static void PlaySound(SFXController.Sounds sound)
		{
			SoundEmitter emitter = SFXController.Instance.GetEmitter(sound);
			AudioSource source = emitter.GetSource();
			if (!source.isPlaying || emitter.interupt)
			{
				source.clip = SFXController.Instance.GetRandomClip(emitter);
				source.Play();
			}
		}

		// Token: 0x06002004 RID: 8196 RVA: 0x00185208 File Offset: 0x00183408
		private SoundEmitter GetEmitter(SFXController.Sounds sound)
		{
			foreach (SoundEmitter soundEmitter in this.emitters)
			{
				if (soundEmitter.sound == sound)
				{
					return soundEmitter;
				}
			}
			Debug.Log(string.Format("There is no sound emitter created for {0}", sound), this);
			return null;
		}

		// Token: 0x06002005 RID: 8197 RVA: 0x00185274 File Offset: 0x00183474
		private AudioClip GetRandomClip(SoundEmitter emitter)
		{
			int index = UnityEngine.Random.Range(0, emitter.clips.Count);
			return emitter.clips[index];
		}

		// Token: 0x04003DC6 RID: 15814
		private static SFXController instance;

		// Token: 0x04003DC7 RID: 15815
		[Reorderable]
		public SoundEmitters emitters;

		// Token: 0x02000702 RID: 1794
		public enum Sounds
		{
			// Token: 0x04004881 RID: 18561
			Countdown,
			// Token: 0x04004882 RID: 18562
			MenuBack,
			// Token: 0x04004883 RID: 18563
			MenuConfirm,
			// Token: 0x04004884 RID: 18564
			ClockTick,
			// Token: 0x04004885 RID: 18565
			DoorBell,
			// Token: 0x04004886 RID: 18566
			GameFail,
			// Token: 0x04004887 RID: 18567
			GameSuccess,
			// Token: 0x04004888 RID: 18568
			Plate,
			// Token: 0x04004889 RID: 18569
			PageTurn,
			// Token: 0x0400488A RID: 18570
			MenuSelect,
			// Token: 0x0400488B RID: 18571
			MaleCustomerGreet,
			// Token: 0x0400488C RID: 18572
			MaleCustomerThank,
			// Token: 0x0400488D RID: 18573
			MaleCustomerLeave,
			// Token: 0x0400488E RID: 18574
			FemaleCustomerGreet,
			// Token: 0x0400488F RID: 18575
			FemaleCustomerThank,
			// Token: 0x04004890 RID: 18576
			FemaleCustomerLeave,
			// Token: 0x04004891 RID: 18577
			MenuOpen
		}
	}
}
