using System;
using UnityEngine;

// Token: 0x02000313 RID: 787
public class JukeboxScript : MonoBehaviour
{
	// Token: 0x060017C2 RID: 6082 RVA: 0x000CFFF0 File Offset: 0x000CE1F0
	public void Start()
	{
		if (this.BGM == 0)
		{
			this.BGM = UnityEngine.Random.Range(0, 8);
		}
		else
		{
			this.BGM++;
			if (this.BGM > 8)
			{
				this.BGM = 1;
			}
		}
		if (this.BGM == 1)
		{
			this.FullSanities = this.OriginalFull;
			this.HalfSanities = this.OriginalHalf;
			this.NoSanities = this.OriginalNo;
		}
		else if (this.BGM == 2)
		{
			this.FullSanities = this.AlternateFull;
			this.HalfSanities = this.AlternateHalf;
			this.NoSanities = this.AlternateNo;
		}
		else if (this.BGM == 3)
		{
			this.FullSanities = this.ThirdFull;
			this.HalfSanities = this.ThirdHalf;
			this.NoSanities = this.ThirdNo;
		}
		else if (this.BGM == 4)
		{
			this.FullSanities = this.FourthFull;
			this.HalfSanities = this.FourthHalf;
			this.NoSanities = this.FourthNo;
		}
		else if (this.BGM == 5)
		{
			this.FullSanities = this.FifthFull;
			this.HalfSanities = this.FifthHalf;
			this.NoSanities = this.FifthNo;
		}
		else if (this.BGM == 6)
		{
			this.FullSanities = this.SixthFull;
			this.HalfSanities = this.SixthHalf;
			this.NoSanities = this.SixthNo;
		}
		else if (this.BGM == 7)
		{
			this.FullSanities = this.SeventhFull;
			this.HalfSanities = this.SeventhHalf;
			this.NoSanities = this.SeventhNo;
		}
		else if (this.BGM == 8)
		{
			this.FullSanities = this.EighthFull;
			this.HalfSanities = this.EighthHalf;
			this.NoSanities = this.EighthNo;
		}
		if (!SchoolGlobals.SchoolAtmosphereSet)
		{
			SchoolGlobals.SchoolAtmosphereSet = true;
			SchoolGlobals.SchoolAtmosphere = 1f;
		}
		int num;
		if (SchoolAtmosphere.Type == SchoolAtmosphereType.High)
		{
			num = 3;
		}
		else if (SchoolAtmosphere.Type == SchoolAtmosphereType.Medium)
		{
			num = 2;
		}
		else
		{
			num = 1;
		}
		this.FullSanity.clip = this.FullSanities[num];
		this.HalfSanity.clip = this.HalfSanities[num];
		this.NoSanity.clip = this.NoSanities[num];
		this.Volume = 0.25f;
		this.FullSanity.volume = 0f;
		this.Hitman.time = 26f;
	}

	// Token: 0x060017C3 RID: 6083 RVA: 0x000D0250 File Offset: 0x000CE450
	private void Update()
	{
		if (!this.Yandere.PauseScreen.Show && !this.Yandere.EasterEggMenu.activeInHierarchy && Input.GetKeyDown(KeyCode.M))
		{
			this.StartStopMusic();
		}
		if (!this.Egg)
		{
			if (!this.Yandere.Police.Clock.SchoolBell.isPlaying && !this.Yandere.StudentManager.MemorialScene.enabled)
			{
				if (!this.StartMusic)
				{
					this.FullSanity.Play();
					this.HalfSanity.Play();
					this.NoSanity.Play();
					this.StartMusic = true;
				}
				if (this.Yandere.Sanity >= 66.666664f)
				{
					this.FullSanity.volume = Mathf.MoveTowards(this.FullSanity.volume, this.Volume * this.Dip - this.ClubDip, 0.016666668f * this.FadeSpeed);
					this.HalfSanity.volume = Mathf.MoveTowards(this.HalfSanity.volume, 0f, 0.016666668f * this.FadeSpeed);
					this.NoSanity.volume = Mathf.MoveTowards(this.NoSanity.volume, 0f, 0.016666668f * this.FadeSpeed);
				}
				else if (this.Yandere.Sanity >= 33.333332f)
				{
					this.FullSanity.volume = Mathf.MoveTowards(this.FullSanity.volume, 0f, 0.016666668f * this.FadeSpeed);
					this.HalfSanity.volume = Mathf.MoveTowards(this.HalfSanity.volume, this.Volume * this.Dip - this.ClubDip, 0.016666668f * this.FadeSpeed);
					this.NoSanity.volume = Mathf.MoveTowards(this.NoSanity.volume, 0f, 0.016666668f * this.FadeSpeed);
				}
				else
				{
					this.FullSanity.volume = Mathf.MoveTowards(this.FullSanity.volume, 0f, 0.016666668f * this.FadeSpeed);
					this.HalfSanity.volume = Mathf.MoveTowards(this.HalfSanity.volume, 0f, 0.016666668f * this.FadeSpeed);
					this.NoSanity.volume = Mathf.MoveTowards(this.NoSanity.volume, this.Volume * this.Dip - this.ClubDip, 0.016666668f * this.FadeSpeed);
				}
			}
		}
		else
		{
			this.AttackOnTitan.volume = Mathf.MoveTowards(this.AttackOnTitan.volume, this.Volume * this.Dip, 0.16666667f);
			this.Megalovania.volume = Mathf.MoveTowards(this.Megalovania.volume, this.Volume * this.Dip, 0.16666667f);
			this.MissionMode.volume = Mathf.MoveTowards(this.MissionMode.volume, this.Volume * this.Dip, 0.16666667f);
			this.Skeletons.volume = Mathf.MoveTowards(this.Skeletons.volume, this.Volume * this.Dip, 0.16666667f);
			this.Vaporwave.volume = Mathf.MoveTowards(this.Vaporwave.volume, this.Volume * this.Dip, 0.16666667f);
			this.AzurLane.volume = Mathf.MoveTowards(this.AzurLane.volume, this.Volume * this.Dip, 0.16666667f);
			this.LifeNote.volume = Mathf.MoveTowards(this.LifeNote.volume, this.Volume * this.Dip, 0.16666667f);
			this.Berserk.volume = Mathf.MoveTowards(this.Berserk.volume, this.Volume * this.Dip, 0.16666667f);
			this.Metroid.volume = Mathf.MoveTowards(this.Metroid.volume, this.Volume * this.Dip, 0.16666667f);
			this.Nuclear.volume = Mathf.MoveTowards(this.Nuclear.volume, this.Volume * this.Dip, 0.16666667f);
			this.Slender.volume = Mathf.MoveTowards(this.Slender.volume, this.Volume * this.Dip, 0.16666667f);
			this.Sukeban.volume = Mathf.MoveTowards(this.Sukeban.volume, this.Volume * this.Dip, 0.16666667f);
			this.Custom.volume = Mathf.MoveTowards(this.Custom.volume, this.Volume * this.Dip, 0.16666667f);
			this.Hatred.volume = Mathf.MoveTowards(this.Hatred.volume, this.Volume * this.Dip, 0.16666667f);
			this.Hitman.volume = Mathf.MoveTowards(this.Hitman.volume, this.Volume * this.Dip, 0.16666667f);
			this.Touhou.volume = Mathf.MoveTowards(this.Touhou.volume, this.Volume * this.Dip, 0.16666667f);
			this.Falcon.volume = Mathf.MoveTowards(this.Falcon.volume, this.Volume * this.Dip, 0.16666667f);
			this.Miyuki.volume = Mathf.MoveTowards(this.Miyuki.volume, this.Volume * this.Dip, 0.16666667f);
			this.Demon.volume = Mathf.MoveTowards(this.Demon.volume, this.Volume * this.Dip, 0.16666667f);
			this.Ebola.volume = Mathf.MoveTowards(this.Ebola.volume, this.Volume * this.Dip, 0.16666667f);
			this.Ninja.volume = Mathf.MoveTowards(this.Ninja.volume, this.Volume * this.Dip, 0.16666667f);
			this.Punch.volume = Mathf.MoveTowards(this.Punch.volume, this.Volume * this.Dip, 0.16666667f);
			this.Galo.volume = Mathf.MoveTowards(this.Galo.volume, this.Volume * this.Dip, 0.16666667f);
			this.Jojo.volume = Mathf.MoveTowards(this.Jojo.volume, this.Volume * this.Dip, 0.16666667f);
			this.Lied.volume = Mathf.MoveTowards(this.Lied.volume, this.Volume * this.Dip, 0.16666667f);
			this.Nier.volume = Mathf.MoveTowards(this.Nier.volume, this.Volume * this.Dip, 0.16666667f);
			this.Sith.volume = Mathf.MoveTowards(this.Sith.volume, this.Volume * this.Dip, 0.16666667f);
			this.DK.volume = Mathf.MoveTowards(this.DK.volume, this.Volume * this.Dip, 0.16666667f);
			this.Horror.volume = Mathf.MoveTowards(this.Horror.volume, this.Volume * this.Dip, 0.16666667f);
		}
		if (!this.Yandere.PauseScreen.Show && !this.Yandere.Noticed && this.Yandere.CanMove && this.Yandere.EasterEggMenu.activeInHierarchy && !this.Egg)
		{
			if (Input.GetKeyDown(KeyCode.T) || Input.GetKeyDown(KeyCode.Alpha4))
			{
				this.Egg = true;
				this.KillVolume();
				this.AttackOnTitan.enabled = true;
				return;
			}
			if (Input.GetKeyDown(KeyCode.P))
			{
				this.Egg = true;
				this.KillVolume();
				this.Nuclear.enabled = true;
				return;
			}
			if (Input.GetKeyDown(KeyCode.H))
			{
				this.Egg = true;
				this.KillVolume();
				this.Hatred.enabled = true;
				return;
			}
			if (Input.GetKeyDown(KeyCode.B))
			{
				this.Egg = true;
				this.KillVolume();
				this.Sukeban.enabled = true;
				return;
			}
			if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Z))
			{
				this.Egg = true;
				this.KillVolume();
				this.Slender.enabled = true;
				return;
			}
			if (Input.GetKeyDown(KeyCode.G))
			{
				this.Egg = true;
				this.KillVolume();
				this.Galo.enabled = true;
				return;
			}
			if (Input.GetKeyDown(KeyCode.L))
			{
				this.Egg = true;
				this.KillVolume();
				this.Hitman.enabled = true;
				return;
			}
			if (Input.GetKeyDown(KeyCode.S))
			{
				this.Egg = true;
				this.KillVolume();
				this.Skeletons.enabled = true;
				return;
			}
			if (Input.GetKeyDown(KeyCode.K))
			{
				this.Egg = true;
				this.KillVolume();
				this.DK.enabled = true;
				return;
			}
			if (Input.GetKeyDown(KeyCode.C))
			{
				this.Egg = true;
				this.KillVolume();
				this.Touhou.enabled = true;
				return;
			}
			if (Input.GetKeyDown(KeyCode.F))
			{
				this.Egg = true;
				this.KillVolume();
				this.Falcon.enabled = true;
				return;
			}
			if (Input.GetKeyDown(KeyCode.O))
			{
				this.Egg = true;
				this.KillVolume();
				this.Punch.enabled = true;
				return;
			}
			if (Input.GetKeyDown(KeyCode.U))
			{
				this.Egg = true;
				this.KillVolume();
				this.Megalovania.enabled = true;
				return;
			}
			if (Input.GetKeyDown(KeyCode.Q))
			{
				this.Egg = true;
				this.KillVolume();
				this.Metroid.enabled = true;
				return;
			}
			if (Input.GetKeyDown(KeyCode.Y))
			{
				this.Egg = true;
				this.KillVolume();
				this.Ninja.enabled = true;
				return;
			}
			if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.M) || Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Alpha7) || Input.GetKeyDown(KeyCode.F5) || Input.GetKeyDown(KeyCode.W))
			{
				this.Egg = true;
				this.KillVolume();
				this.Ebola.enabled = true;
				return;
			}
			if (Input.GetKeyDown(KeyCode.Alpha6))
			{
				this.Egg = true;
				this.KillVolume();
				this.Demon.enabled = true;
				return;
			}
			if (Input.GetKeyDown(KeyCode.D))
			{
				this.Egg = true;
				this.KillVolume();
				this.Sith.enabled = true;
				return;
			}
			if (Input.GetKeyDown(KeyCode.F2))
			{
				this.Egg = true;
				this.KillVolume();
				this.Horror.enabled = true;
				return;
			}
			if (Input.GetKeyDown(KeyCode.F3))
			{
				this.Egg = true;
				this.KillVolume();
				this.LifeNote.enabled = true;
				return;
			}
			if (Input.GetKeyDown(KeyCode.F6) || Input.GetKeyDown(KeyCode.F9))
			{
				this.Egg = true;
				this.KillVolume();
				this.Lied.enabled = true;
				return;
			}
			if (Input.GetKeyDown(KeyCode.F7))
			{
				this.Egg = true;
				this.KillVolume();
				this.Berserk.enabled = true;
				return;
			}
			if (Input.GetKeyDown(KeyCode.F8))
			{
				this.Egg = true;
				this.KillVolume();
				this.Nier.enabled = true;
				return;
			}
			if (Input.GetKeyDown(KeyCode.V))
			{
				this.Egg = true;
				this.KillVolume();
				this.Vaporwave.enabled = true;
			}
		}
	}

	// Token: 0x060017C4 RID: 6084 RVA: 0x000D0E14 File Offset: 0x000CF014
	public void StartStopMusic()
	{
		if (this.Custom.isPlaying)
		{
			this.Egg = false;
			this.Custom.Stop();
			this.FadeSpeed = 1f;
			this.StartMusic = false;
			this.Volume = this.LastVolume;
			this.Start();
			return;
		}
		if (this.Volume == 0f)
		{
			this.FadeSpeed = 1f;
			this.StartMusic = false;
			this.Volume = this.LastVolume;
			this.Start();
			return;
		}
		this.LastVolume = this.Volume;
		this.FadeSpeed = 10f;
		this.Volume = 0f;
	}

	// Token: 0x060017C5 RID: 6085 RVA: 0x000D0EB9 File Offset: 0x000CF0B9
	public void Shipgirl()
	{
		this.Egg = true;
		this.KillVolume();
		this.AzurLane.enabled = true;
	}

	// Token: 0x060017C6 RID: 6086 RVA: 0x000D0ED4 File Offset: 0x000CF0D4
	public void MiyukiMusic()
	{
		this.Egg = true;
		this.KillVolume();
		this.Miyuki.enabled = true;
	}

	// Token: 0x060017C7 RID: 6087 RVA: 0x000D0EEF File Offset: 0x000CF0EF
	public void KillVolume()
	{
		this.FullSanity.volume = 0f;
		this.HalfSanity.volume = 0f;
		this.NoSanity.volume = 0f;
		this.Volume = 0.5f;
	}

	// Token: 0x060017C8 RID: 6088 RVA: 0x000D0F2C File Offset: 0x000CF12C
	public void GameOver()
	{
		this.AttackOnTitan.Stop();
		this.Megalovania.Stop();
		this.MissionMode.Stop();
		this.Skeletons.Stop();
		this.Vaporwave.Stop();
		this.AzurLane.Stop();
		this.LifeNote.Stop();
		this.Berserk.Stop();
		this.Metroid.Stop();
		this.Nuclear.Stop();
		this.Sukeban.Stop();
		this.Custom.Stop();
		this.Slender.Stop();
		this.Hatred.Stop();
		this.Hitman.Stop();
		this.Horror.Stop();
		this.Touhou.Stop();
		this.Falcon.Stop();
		this.Miyuki.Stop();
		this.Ebola.Stop();
		this.Punch.Stop();
		this.Ninja.Stop();
		this.Jojo.Stop();
		this.Galo.Stop();
		this.Lied.Stop();
		this.Nier.Stop();
		this.Sith.Stop();
		this.DK.Stop();
		this.Confession.Stop();
		this.FullSanity.Stop();
		this.HalfSanity.Stop();
		this.NoSanity.Stop();
	}

	// Token: 0x060017C9 RID: 6089 RVA: 0x000D1099 File Offset: 0x000CF299
	public void PlayJojo()
	{
		this.Egg = true;
		this.KillVolume();
		this.Jojo.enabled = true;
	}

	// Token: 0x060017CA RID: 6090 RVA: 0x000D10B4 File Offset: 0x000CF2B4
	public void PlayCustom()
	{
		this.Egg = true;
		this.KillVolume();
		this.Custom.enabled = true;
		this.Custom.Play();
	}

	// Token: 0x040021B1 RID: 8625
	public YandereScript Yandere;

	// Token: 0x040021B2 RID: 8626
	public AudioSource SFX;

	// Token: 0x040021B3 RID: 8627
	public AudioSource AttackOnTitan;

	// Token: 0x040021B4 RID: 8628
	public AudioSource Megalovania;

	// Token: 0x040021B5 RID: 8629
	public AudioSource MissionMode;

	// Token: 0x040021B6 RID: 8630
	public AudioSource Skeletons;

	// Token: 0x040021B7 RID: 8631
	public AudioSource Vaporwave;

	// Token: 0x040021B8 RID: 8632
	public AudioSource AzurLane;

	// Token: 0x040021B9 RID: 8633
	public AudioSource LifeNote;

	// Token: 0x040021BA RID: 8634
	public AudioSource Berserk;

	// Token: 0x040021BB RID: 8635
	public AudioSource Metroid;

	// Token: 0x040021BC RID: 8636
	public AudioSource Nuclear;

	// Token: 0x040021BD RID: 8637
	public AudioSource Slender;

	// Token: 0x040021BE RID: 8638
	public AudioSource Sukeban;

	// Token: 0x040021BF RID: 8639
	public AudioSource Custom;

	// Token: 0x040021C0 RID: 8640
	public AudioSource Hatred;

	// Token: 0x040021C1 RID: 8641
	public AudioSource Hitman;

	// Token: 0x040021C2 RID: 8642
	public AudioSource Horror;

	// Token: 0x040021C3 RID: 8643
	public AudioSource Touhou;

	// Token: 0x040021C4 RID: 8644
	public AudioSource Falcon;

	// Token: 0x040021C5 RID: 8645
	public AudioSource Miyuki;

	// Token: 0x040021C6 RID: 8646
	public AudioSource Ebola;

	// Token: 0x040021C7 RID: 8647
	public AudioSource Demon;

	// Token: 0x040021C8 RID: 8648
	public AudioSource Ninja;

	// Token: 0x040021C9 RID: 8649
	public AudioSource Punch;

	// Token: 0x040021CA RID: 8650
	public AudioSource Galo;

	// Token: 0x040021CB RID: 8651
	public AudioSource Jojo;

	// Token: 0x040021CC RID: 8652
	public AudioSource Lied;

	// Token: 0x040021CD RID: 8653
	public AudioSource Nier;

	// Token: 0x040021CE RID: 8654
	public AudioSource Sith;

	// Token: 0x040021CF RID: 8655
	public AudioSource DK;

	// Token: 0x040021D0 RID: 8656
	public AudioSource Confession;

	// Token: 0x040021D1 RID: 8657
	public AudioSource FullSanity;

	// Token: 0x040021D2 RID: 8658
	public AudioSource HalfSanity;

	// Token: 0x040021D3 RID: 8659
	public AudioSource NoSanity;

	// Token: 0x040021D4 RID: 8660
	public AudioSource Chase;

	// Token: 0x040021D5 RID: 8661
	public float LastVolume;

	// Token: 0x040021D6 RID: 8662
	public float FadeSpeed;

	// Token: 0x040021D7 RID: 8663
	public float ClubDip;

	// Token: 0x040021D8 RID: 8664
	public float Volume;

	// Token: 0x040021D9 RID: 8665
	public int Track;

	// Token: 0x040021DA RID: 8666
	public int BGM;

	// Token: 0x040021DB RID: 8667
	public float Dip = 1f;

	// Token: 0x040021DC RID: 8668
	public bool StartMusic;

	// Token: 0x040021DD RID: 8669
	public bool Egg;

	// Token: 0x040021DE RID: 8670
	public AudioClip[] FullSanities;

	// Token: 0x040021DF RID: 8671
	public AudioClip[] HalfSanities;

	// Token: 0x040021E0 RID: 8672
	public AudioClip[] NoSanities;

	// Token: 0x040021E1 RID: 8673
	public AudioClip[] OriginalFull;

	// Token: 0x040021E2 RID: 8674
	public AudioClip[] OriginalHalf;

	// Token: 0x040021E3 RID: 8675
	public AudioClip[] OriginalNo;

	// Token: 0x040021E4 RID: 8676
	public AudioClip[] AlternateFull;

	// Token: 0x040021E5 RID: 8677
	public AudioClip[] AlternateHalf;

	// Token: 0x040021E6 RID: 8678
	public AudioClip[] AlternateNo;

	// Token: 0x040021E7 RID: 8679
	public AudioClip[] ThirdFull;

	// Token: 0x040021E8 RID: 8680
	public AudioClip[] ThirdHalf;

	// Token: 0x040021E9 RID: 8681
	public AudioClip[] ThirdNo;

	// Token: 0x040021EA RID: 8682
	public AudioClip[] FourthFull;

	// Token: 0x040021EB RID: 8683
	public AudioClip[] FourthHalf;

	// Token: 0x040021EC RID: 8684
	public AudioClip[] FourthNo;

	// Token: 0x040021ED RID: 8685
	public AudioClip[] FifthFull;

	// Token: 0x040021EE RID: 8686
	public AudioClip[] FifthHalf;

	// Token: 0x040021EF RID: 8687
	public AudioClip[] FifthNo;

	// Token: 0x040021F0 RID: 8688
	public AudioClip[] SixthFull;

	// Token: 0x040021F1 RID: 8689
	public AudioClip[] SixthHalf;

	// Token: 0x040021F2 RID: 8690
	public AudioClip[] SixthNo;

	// Token: 0x040021F3 RID: 8691
	public AudioClip[] SeventhFull;

	// Token: 0x040021F4 RID: 8692
	public AudioClip[] SeventhHalf;

	// Token: 0x040021F5 RID: 8693
	public AudioClip[] SeventhNo;

	// Token: 0x040021F6 RID: 8694
	public AudioClip[] EighthFull;

	// Token: 0x040021F7 RID: 8695
	public AudioClip[] EighthHalf;

	// Token: 0x040021F8 RID: 8696
	public AudioClip[] EighthNo;
}
