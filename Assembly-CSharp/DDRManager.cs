using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

// Token: 0x02000004 RID: 4
public class DDRManager : MonoBehaviour
{
	// Token: 0x06000056 RID: 86 RVA: 0x000032DE File Offset: 0x000014DE
	private void Start()
	{
		this.minigameCamera.position = this.startPoint.position;
		if (this.DebugMode)
		{
			this.BeginMinigame();
		}
	}

	// Token: 0x06000057 RID: 87 RVA: 0x00003304 File Offset: 0x00001504
	public void Update()
	{
		this.minigameCamera.position = Vector3.Slerp(this.minigameCamera.position, this.target.position, this.transitionSpeed * Time.deltaTime);
		this.minigameCamera.rotation = Quaternion.Slerp(this.minigameCamera.rotation, this.target.rotation, this.transitionSpeed * Time.deltaTime);
		if (this.target == null)
		{
			return;
		}
		Vector3 position = this.standPoint.position;
		if (this.LoadedLevel != null)
		{
			this.ddrMinigame.UpdateGame(this.audioSource.time);
			this.GameState.Health -= Time.deltaTime;
			this.GameState.Health = Mathf.Clamp(this.GameState.Health, 0f, 100f);
			if (this.inputManager.TappedLeft)
			{
				this.yandereAnim["f02_danceLeft_00"].time = 0f;
				this.yandereAnim.Play("f02_danceLeft_00");
			}
			else if (this.inputManager.TappedDown)
			{
				this.yandereAnim["f02_danceDown_00"].time = 0f;
				this.yandereAnim.Play("f02_danceDown_00");
			}
			if (this.inputManager.TappedRight)
			{
				this.yandereAnim["f02_danceRight_00"].time = 0f;
				this.yandereAnim.Play("f02_danceRight_00");
			}
			else if (this.inputManager.TappedUp)
			{
				this.yandereAnim["f02_danceUp_00"].time = 0f;
				this.yandereAnim.Play("f02_danceUp_00");
			}
		}
		this.yandereAnim.transform.position = Vector3.Lerp(this.yandereAnim.transform.position, position, 10f * Time.deltaTime);
		if (this.CheckingForEnd && !this.audioSource.isPlaying)
		{
			this.OverlayCanvas.SetActive(false);
			this.GameUI.SetActive(false);
			this.CheckingForEnd = false;
			Debug.Log("End() was called because song ended.");
			base.StartCoroutine(this.End());
		}
		if (this.GameState.Health <= 0f && this.audioSource.pitch < 0.01f)
		{
			this.OverlayCanvas.SetActive(false);
			this.GameUI.SetActive(false);
			if (this.audioSource.isPlaying)
			{
				Debug.Log("End() was called because we ranout of health.");
				base.StartCoroutine(this.End());
			}
		}
	}

	// Token: 0x06000058 RID: 88 RVA: 0x000035B4 File Offset: 0x000017B4
	public void BeginMinigame()
	{
		Debug.Log("BeginMinigame() was called.");
		this.yandereAnim["f02_danceMachineIdle_00"].layer = 0;
		this.yandereAnim["f02_danceRight_00"].layer = 1;
		this.yandereAnim["f02_danceLeft_00"].layer = 2;
		this.yandereAnim["f02_danceUp_00"].layer = 1;
		this.yandereAnim["f02_danceDown_00"].layer = 2;
		this.yandereAnim["f02_danceMachineIdle_00"].weight = 1f;
		this.yandereAnim["f02_danceRight_00"].weight = 1f;
		this.yandereAnim["f02_danceLeft_00"].weight = 1f;
		this.yandereAnim["f02_danceUp_00"].weight = 1f;
		this.yandereAnim["f02_danceDown_00"].weight = 1f;
		this.OverlayCanvas.SetActive(true);
		this.GameUI.SetActive(true);
		this.ddrMinigame.LoadLevelSelect(this.levels);
		base.StartCoroutine(this.minigameFlow());
	}

	// Token: 0x06000059 RID: 89 RVA: 0x000036F4 File Offset: 0x000018F4
	public void BootOut()
	{
		this.minigameCamera.position = this.startPoint.position;
		base.StartCoroutine(this.fade(true, this.fadeImage, 5f));
		this.target = this.startPoint;
		this.ddrMinigame.UnloadLevelSelect();
		this.ReturnToNormalGameplay();
	}

	// Token: 0x0600005A RID: 90 RVA: 0x0000374D File Offset: 0x0000194D
	private IEnumerator minigameFlow()
	{
		this.levelSelect.gameObject.SetActive(true);
		this.defeatScreen.gameObject.SetActive(false);
		this.endScreen.gameObject.SetActive(false);
		this.audioSource.pitch = 1f;
		this.target = this.screenPoint;
		if (!this.booted)
		{
			yield return new WaitForSecondsRealtime(0.2f);
			base.StartCoroutine(this.fade(false, this.fadeImage, 1f));
			while (this.fadeImage.color.a > 0.4f)
			{
				yield return null;
			}
			this.machineScreenAnimation.Play();
			this.booted = true;
		}
		yield return new WaitForEndOfFrame();
		while (Input.GetAxis("A") != 0f)
		{
			yield return null;
		}
		while (this.LoadedLevel == null)
		{
			this.ddrMinigame.UpdateLevelSelect();
			yield return null;
		}
		this.ddrMinigame.LoadLevel(this.LoadedLevel);
		this.GameState = new GameState();
		yield return new WaitForSecondsRealtime(0.2f);
		this.transitionSpeed *= 2f;
		this.target = this.watchPoint;
		this.backgroundVideo.Play();
		this.backgroundVideo.playbackSpeed = 0f;
		base.StartCoroutine(this.fadeGameUI(true));
		this.backgroundVideo.playbackSpeed = 1f;
		this.audioSource.clip = this.LoadedLevel.Song;
		this.audioSource.Play();
		this.CheckingForEnd = true;
		while (this.audioSource.time < this.audioSource.clip.length)
		{
			if (this.GameState.Health <= 0f)
			{
				this.GameState.FinishStatus = DDRFinishStatus.Failed;
				while (this.audioSource.pitch > 0f)
				{
					this.audioSource.pitch = Mathf.MoveTowards(this.audioSource.pitch, 0f, 0.2f * Time.deltaTime);
					if (this.audioSource.pitch == 0f)
					{
						Debug.Log("Pitch reached zero.");
						this.audioSource.time = this.audioSource.clip.length;
						this.OverlayCanvas.SetActive(false);
						this.GameUI.SetActive(false);
					}
					yield return null;
				}
				break;
			}
			yield return null;
		}
		yield break;
	}

	// Token: 0x0600005B RID: 91 RVA: 0x0000375C File Offset: 0x0000195C
	private IEnumerator End()
	{
		this.audioSource.Stop();
		this.levelSelect.gameObject.SetActive(false);
		base.StopCoroutine(this.fadeGameUI(true));
		base.StartCoroutine(this.fadeGameUI(false));
		if (this.GameState.FinishStatus == DDRFinishStatus.Complete)
		{
			this.endScreen.gameObject.SetActive(true);
			this.ddrMinigame.UpdateEndcard(this.GameState);
		}
		else
		{
			this.defeatScreen.SetActive(true);
		}
		this.target = this.screenPoint;
		this.LoadedLevel = null;
		this.ddrMinigame.UnloadLevelSelect();
		yield return new WaitForSecondsRealtime(2f);
		base.StartCoroutine(this.fade(true, this.continueText, 1f));
		while (!Input.anyKeyDown || Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetMouseButton(2))
		{
			yield return null;
		}
		this.ddrMinigame.Unload();
		this.onLevelFinish(this.GameState.FinishStatus);
		yield break;
	}

	// Token: 0x0600005C RID: 92 RVA: 0x0000376B File Offset: 0x0000196B
	private IEnumerator fadeGameUI(bool fadein)
	{
		float destination = (float)(fadein ? 1 : 0);
		float amount = (float)(fadein ? 0 : 1);
		while (amount != destination)
		{
			amount = Mathf.Lerp(amount, destination, 10f * Time.deltaTime);
			foreach (RawImage rawImage in this.overlayImages)
			{
				Color color = rawImage.color;
				color.a = amount;
				rawImage.color = color;
			}
			yield return null;
		}
		yield break;
	}

	// Token: 0x0600005D RID: 93 RVA: 0x00003781 File Offset: 0x00001981
	private IEnumerator fade(bool fadein, MaskableGraphic graphic, float speed = 1f)
	{
		float destination = (float)(fadein ? 1 : 0);
		float amount = (float)(fadein ? 0 : 1);
		while (amount != destination)
		{
			amount = Mathf.Lerp(amount, destination, speed * Time.deltaTime);
			Color color = graphic.color;
			color.a = amount;
			graphic.color = color;
			yield return null;
		}
		yield break;
	}

	// Token: 0x0600005E RID: 94 RVA: 0x0000379E File Offset: 0x0000199E
	private void onLevelFinish(DDRFinishStatus status)
	{
		this.BootOut();
	}

	// Token: 0x0600005F RID: 95 RVA: 0x000037A8 File Offset: 0x000019A8
	public void ReturnToNormalGameplay()
	{
		Debug.Log("ReturnToNormalGameplay() was called.");
		this.yandereAnim["f02_danceMachineIdle_00"].weight = 0f;
		this.yandereAnim["f02_danceRight_00"].weight = 0f;
		this.yandereAnim["f02_danceLeft_00"].weight = 0f;
		this.yandereAnim["f02_danceUp_00"].weight = 0f;
		this.yandereAnim["f02_danceDown_00"].weight = 0f;
		this.Yandere.transform.position = this.FinishLocation.position;
		this.Yandere.transform.rotation = this.FinishLocation.rotation;
		this.Yandere.StudentManager.Clock.StopTime = false;
		this.Yandere.MyController.enabled = true;
		this.Yandere.StudentManager.ComeBack();
		this.Yandere.CanMove = true;
		this.Yandere.enabled = true;
		this.Yandere.HeartCamera.enabled = true;
		this.Yandere.HUD.enabled = true;
		this.Yandere.HUD.transform.parent.gameObject.SetActive(true);
		this.Yandere.MainCamera.gameObject.SetActive(true);
		this.Yandere.Jukebox.Volume = this.Yandere.Jukebox.LastVolume;
		this.OriginalRenderer.enabled = true;
		Physics.SyncTransforms();
		base.transform.parent.gameObject.SetActive(false);
	}

	// Token: 0x04000045 RID: 69
	public GameState GameState;

	// Token: 0x04000046 RID: 70
	public YandereScript Yandere;

	// Token: 0x04000047 RID: 71
	public Transform FinishLocation;

	// Token: 0x04000048 RID: 72
	public Renderer OriginalRenderer;

	// Token: 0x04000049 RID: 73
	public GameObject OverlayCanvas;

	// Token: 0x0400004A RID: 74
	public GameObject GameUI;

	// Token: 0x0400004B RID: 75
	[Header("General")]
	public DDRLevel LoadedLevel;

	// Token: 0x0400004C RID: 76
	[SerializeField]
	private DDRLevel[] levels;

	// Token: 0x0400004D RID: 77
	[SerializeField]
	private InputManagerScript inputManager;

	// Token: 0x0400004E RID: 78
	[SerializeField]
	private DDRMinigame ddrMinigame;

	// Token: 0x0400004F RID: 79
	[SerializeField]
	private AudioSource audioSource;

	// Token: 0x04000050 RID: 80
	[SerializeField]
	private Transform standPoint;

	// Token: 0x04000051 RID: 81
	[SerializeField]
	private float transitionSpeed = 2f;

	// Token: 0x04000052 RID: 82
	[Header("Camera")]
	[SerializeField]
	private Transform minigameCamera;

	// Token: 0x04000053 RID: 83
	[SerializeField]
	private Transform startPoint;

	// Token: 0x04000054 RID: 84
	[SerializeField]
	private Transform screenPoint;

	// Token: 0x04000055 RID: 85
	[SerializeField]
	private Transform watchPoint;

	// Token: 0x04000056 RID: 86
	[Header("Animation")]
	[SerializeField]
	private Animation machineScreenAnimation;

	// Token: 0x04000057 RID: 87
	[SerializeField]
	private Animation yandereAnim;

	// Token: 0x04000058 RID: 88
	[Header("UI")]
	[SerializeField]
	private Image fadeImage;

	// Token: 0x04000059 RID: 89
	[SerializeField]
	private RawImage[] overlayImages;

	// Token: 0x0400005A RID: 90
	[SerializeField]
	private VideoPlayer backgroundVideo;

	// Token: 0x0400005B RID: 91
	[SerializeField]
	private Transform levelSelect;

	// Token: 0x0400005C RID: 92
	[SerializeField]
	private GameObject endScreen;

	// Token: 0x0400005D RID: 93
	[SerializeField]
	private GameObject defeatScreen;

	// Token: 0x0400005E RID: 94
	[SerializeField]
	private Text continueText;

	// Token: 0x0400005F RID: 95
	[SerializeField]
	private ColorCorrectionCurves gameplayColorCorrection;

	// Token: 0x04000060 RID: 96
	private Transform target;

	// Token: 0x04000061 RID: 97
	private bool booted;

	// Token: 0x04000062 RID: 98
	public bool DebugMode;

	// Token: 0x04000063 RID: 99
	public bool CheckingForEnd;
}
