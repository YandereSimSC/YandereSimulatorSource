using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020002DF RID: 735
public class HeartbrokenCursorScript : MonoBehaviour
{
	// Token: 0x060016ED RID: 5869 RVA: 0x000BE65C File Offset: 0x000BC85C
	private void Start()
	{
		this.Darkness.transform.localPosition = new Vector3(this.Darkness.transform.localPosition.x, this.Darkness.transform.localPosition.y, -989f);
		this.Continue.color = new Color(this.Continue.color.r, this.Continue.color.g, this.Continue.color.b, 0f);
		this.StudentManager.Yandere.Jukebox.gameObject.SetActive(false);
		if (this.StudentManager.Yandere.Weapon[1] != null && this.StudentManager.Yandere.Weapon[1].Type == WeaponType.Knife)
		{
			this.StudentManager.Yandere.Weapon[1].Drop();
		}
		if (this.StudentManager.Yandere.Weapon[2] != null && this.StudentManager.Yandere.Weapon[2].Type == WeaponType.Knife)
		{
			this.StudentManager.Yandere.Weapon[2].Drop();
		}
	}

	// Token: 0x060016EE RID: 5870 RVA: 0x000BE7A4 File Offset: 0x000BC9A4
	private void Update()
	{
		base.transform.localPosition = new Vector3(base.transform.localPosition.x, Mathf.Lerp(base.transform.localPosition.y, 255f - (float)this.Selected * 50f, Time.deltaTime * 10f), base.transform.localPosition.z);
		if (this.Selected == 4)
		{
			this.GameOverMusic.volume = Mathf.MoveTowards(this.GameOverMusic.volume, 0f, Time.deltaTime * 0.5f);
		}
		else
		{
			this.GameOverMusic.volume = Mathf.MoveTowards(this.GameOverMusic.volume, 1f, Time.deltaTime * 0.5f);
		}
		if (!this.FadeOut)
		{
			if (this.MyLabel.color.a >= 1f)
			{
				if (this.InputManager.TappedDown)
				{
					this.Selected++;
					if (this.Selected > this.Options)
					{
						this.Selected = 1;
					}
					this.MyAudio.clip = this.MoveSound;
					this.MyAudio.Play();
				}
				if (this.InputManager.TappedUp)
				{
					this.Selected--;
					if (this.Selected < 1)
					{
						this.Selected = this.Options;
					}
					this.MyAudio.clip = this.MoveSound;
					this.MyAudio.Play();
				}
				this.Continue.color = new Color(this.Continue.color.r, this.Continue.color.g, this.Continue.color.b, (this.Selected != 4) ? 1f : 0f);
				if (Input.GetButtonDown("A"))
				{
					this.Nudge = true;
					if (this.Selected != 4)
					{
						this.MyAudio.clip = this.SelectSound;
						this.MyAudio.Play();
						this.FadeOut = true;
					}
					else
					{
						this.StudentManager.Yandere.ShoulderCamera.enabled = false;
						if (this.CracksSpawned == 0)
						{
							this.Cracks[1].transform.parent.position = this.StudentManager.Yandere.Head.position;
							this.Cracks[1].transform.parent.position = Vector3.MoveTowards(this.Cracks[1].transform.parent.position, this.Heartbroken.transform.parent.position, -1f);
							VibrateScript[] vibrations = this.Vibrations;
							for (int i = 0; i < vibrations.Length; i++)
							{
								vibrations[i].enabled = false;
							}
							this.Heartbroken.Freeze = true;
						}
						if (this.CracksSpawned < 17)
						{
							this.Heartbroken.Darken();
							while (this.RandomCrack == this.LastRandomCrack)
							{
								this.RandomCrack = UnityEngine.Random.Range(0, 3);
							}
							this.LastRandomCrack = this.RandomCrack;
							this.MyAudio.clip = this.CrackSound[this.RandomCrack];
							this.MyAudio.Play();
							this.TwitchID++;
							if (this.TwitchID > 5)
							{
								this.TwitchID = 0;
							}
							this.StudentManager.Yandere.CharacterAnimation["f02_snapTwitch_0" + this.TwitchID].time = 0.1f;
							this.StudentManager.Yandere.CharacterAnimation.Play("f02_snapTwitch_0" + this.TwitchID);
							this.StudentManager.MainCamera.Translate(this.StudentManager.MainCamera.forward * 0.1f, Space.World);
							this.StudentManager.MainCamera.position = new Vector3(this.StudentManager.MainCamera.position.x, this.StudentManager.MainCamera.position.y - 0.01f, this.StudentManager.MainCamera.position.z);
							int cracksSpawned = this.CracksSpawned;
							while (cracksSpawned == this.CracksSpawned)
							{
								int num = UnityEngine.Random.Range(1, this.Cracks.Length);
								if (!this.Cracks[num].activeInHierarchy)
								{
									this.Cracks[num].SetActive(true);
									this.CracksSpawned++;
								}
							}
							if (this.NeverSnap && this.CracksSpawned == 16)
							{
								while (this.CracksSpawned > 0)
								{
									this.Cracks[this.CracksSpawned].SetActive(false);
									this.CracksSpawned--;
								}
							}
							this.StudentManager.SnapSomeStudents();
							this.StudentManager.OpenSomeDoors();
						}
						else
						{
							for (int j = 1; j < this.Cracks.Length; j++)
							{
								this.Cracks[j].GetComponent<UITexture>().fillAmount = 0.425f;
								this.Cracks[j].SetActive(false);
							}
							this.CracksSpawned = 0;
							this.StudentManager.Yandere.CameraEffects.AlarmBloom.enabled = false;
							this.StudentManager.Yandere.CameraEffects.QualityBloom.enabled = false;
							this.StudentManager.Yandere.CameraEffects.QualityVignetting.enabled = false;
							this.StudentManager.Yandere.CameraEffects.Vignette.enabled = false;
							this.StudentManager.Yandere.CameraEffects.QualityAntialiasingAsPostEffect.enabled = false;
							this.StudentManager.Yandere.ColorCorrection.enabled = false;
							this.StudentManager.Yandere.YandereColorCorrection.enabled = false;
							this.StudentManager.Yandere.Vignette.enabled = false;
							this.StudentManager.Yandere.DepthOfField.enabled = false;
							this.StudentManager.Yandere.Obscurance.enabled = false;
							this.StudentManager.SelectiveGreyscale.enabled = false;
							this.StudentManager.Vignettes[2].enabled = false;
							this.StudentManager.QualityManager.ExperimentalBloomAndLensFlares.enabled = false;
							this.StudentManager.Yandere.RPGCamera.mouseSpeed = 8f;
							this.StudentManager.Yandere.RPGCamera.distance = 0.566666f;
							this.StudentManager.Yandere.RPGCamera.distanceMax = 0.666666f;
							this.StudentManager.Yandere.RPGCamera.distanceMin = 0.666666f;
							this.StudentManager.Yandere.RPGCamera.desiredDistance = 0.666666f;
							this.StudentManager.Yandere.RPGCamera.mouseX = this.StudentManager.Yandere.transform.eulerAngles.y;
							this.StudentManager.Yandere.RPGCamera.mouseXSmooth = this.StudentManager.Yandere.transform.eulerAngles.y;
							this.StudentManager.Yandere.RPGCamera.mouseY = 15f;
							this.StudentManager.Yandere.RPGCamera.mouseY = 15f;
							this.StudentManager.Yandere.Zoom.OverShoulder = true;
							this.StudentManager.Yandere.Zoom.TargetZoom = 0.4f;
							this.StudentManager.Yandere.Zoom.Zoom = 0.4f;
							this.StudentManager.Yandere.Zoom.enabled = false;
							this.StudentManager.Yandere.RightYandereEye.material.color = new Color(1f, 1f, 1f, 1f);
							this.StudentManager.Yandere.LeftYandereEye.material.color = new Color(1f, 1f, 1f, 1f);
							this.SnapPOV.localPosition = new Vector3(1.25f, 1.546664f, -0.5473595f);
							this.SnapFocus.parent = null;
							this.StudentManager.Yandere.MainCamera.enabled = true;
							this.Continue.color = new Color(0f, 0f, 0f, 0f);
							this.MyLabel.color = new Color(0f, 0f, 0f, 0f);
							this.CursorSprite.enabled = false;
							this.MainFilter.enabled = true;
							this.FPS.SetActive(false);
							this.SnapSequence = true;
							this.MyAudio.clip = this.GlassShatter;
							this.MyAudio.volume = 1f;
							this.MyAudio.Play();
							this.Background[0].SetActive(false);
							this.Background[1].SetActive(false);
							this.SNAPLetters.SetActive(false);
							Time.timeScale = 0.5f;
							ShatterSpawner component = UnityEngine.Object.Instantiate<GameObject>(this.ShatterPrefab).GetComponent<ShatterSpawner>();
							component.ScreenMaterial.mainTexture = this.BlackTexture;
							component.ShatterOrigin = new Vector2((float)Screen.width * 0.5f, (float)Screen.height * 0.5f);
							this.StudentManager.Yandere.CharacterAnimation["f02_snapRise_00"].speed = 2f;
							this.StudentManager.Yandere.CharacterAnimation.CrossFade("f02_snapRise_00");
							this.StudentManager.Yandere.enabled = false;
							this.StudentManager.Students[1].Character.SetActive(true);
							this.SnapUICamera.SetActive(true);
							this.StudentManager.SnapSomeStudents();
							this.StudentManager.OpenSomeDoors();
							this.StudentManager.DarkenAllStudents();
							this.StudentManager.Headmaster.gameObject.SetActive(false);
						}
					}
				}
			}
			if (this.SnapSequence)
			{
				this.SnapTimer += Time.deltaTime;
				if (this.SnapTimer > 10f)
				{
					this.StudentManager.Yandere.CharacterAnimation["f02_sadEyebrows_00"].weight = 0f;
					this.HeartbrokenCamera.cullingMask = this.StudentManager.Yandere.MainCamera.cullingMask;
					this.HeartbrokenCamera.clearFlags = this.StudentManager.Yandere.MainCamera.clearFlags;
					this.HeartbrokenCamera.farClipPlane = this.StudentManager.Yandere.MainCamera.farClipPlane;
					this.Heartbroken.MainCamera.enabled = false;
					this.StudentManager.Yandere.RPGCamera.transform.parent = this.StudentManager.Yandere.transform;
					this.SnappedYandere.enabled = true;
					this.SnappedYandere.CanMove = true;
					this.SnapStatic.Play();
					this.SnapMusic.Play();
					base.enabled = false;
					this.MyAudio.Stop();
					Debug.Log("The player now has control over Yandere-chan again.");
				}
				else if (this.SnapTimer > 3f)
				{
					if (this.MyAudio.clip != this.ReverseHit)
					{
						this.MyAudio.clip = this.ReverseHit;
						this.MyAudio.time = 1f;
						this.MyAudio.Play();
					}
					Time.timeScale = 1f;
					this.Speed += Time.deltaTime * 0.5f;
					this.SnapPOV.localPosition = Vector3.Lerp(this.SnapPOV.localPosition, new Vector3(0.25f, 1.546664f, -0.5473595f), Time.deltaTime * this.Speed);
					this.StudentManager.MainCamera.position = Vector3.Lerp(this.StudentManager.MainCamera.position, this.SnapPOV.position, Time.deltaTime * this.Speed);
					this.SnapFocus.position = Vector3.Lerp(this.SnapFocus.position, this.SnapDestination.position, Time.deltaTime * this.Speed);
					this.StudentManager.MainCamera.LookAt(this.SnapFocus);
					this.MainFilter.Fade = Mathf.MoveTowards(this.MainFilter.Fade, 0f, Time.deltaTime * 0.14285715f);
					this.HeartbrokenFilter.Fade = Mathf.MoveTowards(this.HeartbrokenFilter.Fade, 1f, Time.deltaTime * 0.14285715f);
					this.SnappedYandere.CompressionFX.Parasite = Mathf.MoveTowards(this.SnappedYandere.CompressionFX.Parasite, 1f, Time.deltaTime * 0.14285715f);
					this.SnappedYandere.TiltShift.Size = Mathf.MoveTowards(this.SnappedYandere.TiltShift.Size, 0.75f, Time.deltaTime * 0.14285715f);
					this.SnappedYandere.TiltShiftV.Size = Mathf.MoveTowards(this.SnappedYandere.TiltShiftV.Size, 0.75f, Time.deltaTime * 0.14285715f);
				}
			}
		}
		else
		{
			this.Heartbroken.GetComponent<AudioSource>().volume -= Time.deltaTime;
			this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, this.Darkness.color.a + Time.deltaTime);
			if (this.Darkness.color.a >= 1f)
			{
				if (this.Selected == 1)
				{
					for (int k = 0; k < this.StudentManager.NPCsTotal; k++)
					{
						if (StudentGlobals.GetStudentDying(k))
						{
							StudentGlobals.SetStudentDying(k, false);
						}
					}
					SceneManager.LoadScene("LoadingScene");
				}
				else if (this.Selected == 2)
				{
					this.LoveSick = GameGlobals.LoveSick;
					Globals.DeleteAll();
					GameGlobals.LoveSick = this.LoveSick;
					SceneManager.LoadScene("CalendarScene");
				}
				else if (this.Selected == 3)
				{
					SceneManager.LoadScene("TitleScene");
				}
			}
		}
		if (this.Nudge)
		{
			base.transform.localPosition = new Vector3(base.transform.localPosition.x + Time.deltaTime * 250f, base.transform.localPosition.y, base.transform.localPosition.z);
			if (base.transform.localPosition.x > -225f)
			{
				base.transform.localPosition = new Vector3(-225f, base.transform.localPosition.y, base.transform.localPosition.z);
				this.Nudge = false;
				return;
			}
		}
		else
		{
			base.transform.localPosition = new Vector3(base.transform.localPosition.x - Time.deltaTime * 250f, base.transform.localPosition.y, base.transform.localPosition.z);
			if (base.transform.localPosition.x < -250f)
			{
				base.transform.localPosition = new Vector3(-250f, base.transform.localPosition.y, base.transform.localPosition.z);
			}
		}
	}

	// Token: 0x04001E78 RID: 7800
	public SnappedYandereScript SnappedYandere;

	// Token: 0x04001E79 RID: 7801
	public StudentManagerScript StudentManager;

	// Token: 0x04001E7A RID: 7802
	public InputManagerScript InputManager;

	// Token: 0x04001E7B RID: 7803
	public HeartbrokenScript Heartbroken;

	// Token: 0x04001E7C RID: 7804
	public VibrateScript[] Vibrations;

	// Token: 0x04001E7D RID: 7805
	public UISprite CursorSprite;

	// Token: 0x04001E7E RID: 7806
	public UISprite Darkness;

	// Token: 0x04001E7F RID: 7807
	public AudioClip SelectSound;

	// Token: 0x04001E80 RID: 7808
	public AudioClip MoveSound;

	// Token: 0x04001E81 RID: 7809
	public AudioSource MyAudio;

	// Token: 0x04001E82 RID: 7810
	public UILabel Continue;

	// Token: 0x04001E83 RID: 7811
	public UILabel MyLabel;

	// Token: 0x04001E84 RID: 7812
	public GameObject FPS;

	// Token: 0x04001E85 RID: 7813
	public bool LoveSick;

	// Token: 0x04001E86 RID: 7814
	public bool FadeOut;

	// Token: 0x04001E87 RID: 7815
	public bool Nudge;

	// Token: 0x04001E88 RID: 7816
	public int CracksSpawned;

	// Token: 0x04001E89 RID: 7817
	public int Selected = 1;

	// Token: 0x04001E8A RID: 7818
	public int Options = 4;

	// Token: 0x04001E8B RID: 7819
	public int LastRandomCrack;

	// Token: 0x04001E8C RID: 7820
	public int RandomCrack;

	// Token: 0x04001E8D RID: 7821
	public CameraFilterPack_Gradients_FireGradient HeartbrokenFilter;

	// Token: 0x04001E8E RID: 7822
	public CameraFilterPack_Gradients_FireGradient MainFilter;

	// Token: 0x04001E8F RID: 7823
	public Camera HeartbrokenCamera;

	// Token: 0x04001E90 RID: 7824
	public AudioSource GameOverMusic;

	// Token: 0x04001E91 RID: 7825
	public AudioSource SnapStatic;

	// Token: 0x04001E92 RID: 7826
	public AudioSource SnapMusic;

	// Token: 0x04001E93 RID: 7827
	public AudioClip GlassShatter;

	// Token: 0x04001E94 RID: 7828
	public AudioClip ReverseHit;

	// Token: 0x04001E95 RID: 7829
	public AudioClip[] CrackSound;

	// Token: 0x04001E96 RID: 7830
	public GameObject ShatterPrefab;

	// Token: 0x04001E97 RID: 7831
	public GameObject SNAPLetters;

	// Token: 0x04001E98 RID: 7832
	public GameObject SnapUICamera;

	// Token: 0x04001E99 RID: 7833
	public UIPanel SNAPPanel;

	// Token: 0x04001E9A RID: 7834
	public GameObject[] Background;

	// Token: 0x04001E9B RID: 7835
	public GameObject[] Cracks;

	// Token: 0x04001E9C RID: 7836
	public AudioClip[] CracksTier1;

	// Token: 0x04001E9D RID: 7837
	public AudioClip[] CracksTier2;

	// Token: 0x04001E9E RID: 7838
	public AudioClip[] CracksTier3;

	// Token: 0x04001E9F RID: 7839
	public AudioClip[] CracksTier4;

	// Token: 0x04001EA0 RID: 7840
	public Texture BlackTexture;

	// Token: 0x04001EA1 RID: 7841
	public Transform SnapDestination;

	// Token: 0x04001EA2 RID: 7842
	public Transform SnapFocus;

	// Token: 0x04001EA3 RID: 7843
	public Transform SnapPOV;

	// Token: 0x04001EA4 RID: 7844
	public bool SnapSequence;

	// Token: 0x04001EA5 RID: 7845
	public bool NeverSnap;

	// Token: 0x04001EA6 RID: 7846
	public float SnapTimer;

	// Token: 0x04001EA7 RID: 7847
	public float Speed;

	// Token: 0x04001EA8 RID: 7848
	public int TwitchID;
}
