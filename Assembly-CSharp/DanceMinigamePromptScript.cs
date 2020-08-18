using System;
using UnityEngine;

// Token: 0x02000256 RID: 598
public class DanceMinigamePromptScript : MonoBehaviour
{
	// Token: 0x060012ED RID: 4845 RVA: 0x00098A90 File Offset: 0x00096C90
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Yandere.transform.position = this.PlayerLocation.position;
			this.Prompt.Yandere.transform.rotation = this.PlayerLocation.rotation;
			this.Prompt.Yandere.CharacterAnimation.Play("f02_danceMachineIdle_00");
			this.Prompt.Yandere.StudentManager.Clock.StopTime = true;
			this.Prompt.Yandere.MyController.enabled = false;
			this.Prompt.Yandere.HeartCamera.enabled = false;
			this.Prompt.Yandere.HUD.enabled = false;
			this.Prompt.Yandere.CanMove = false;
			this.Prompt.Yandere.enabled = false;
			this.Prompt.Yandere.Jukebox.LastVolume = this.Prompt.Yandere.Jukebox.Volume;
			this.Prompt.Yandere.Jukebox.Volume = 0f;
			this.Prompt.Yandere.HUD.transform.parent.gameObject.SetActive(false);
			this.Prompt.Yandere.MainCamera.gameObject.SetActive(false);
			this.OriginalRenderer.enabled = false;
			Physics.SyncTransforms();
			this.DanceMinigame.SetActive(true);
			this.DanceManager.BeginMinigame();
			this.StudentManager.DisableEveryone();
		}
	}

	// Token: 0x040018B9 RID: 6329
	public StudentManagerScript StudentManager;

	// Token: 0x040018BA RID: 6330
	public Renderer OriginalRenderer;

	// Token: 0x040018BB RID: 6331
	public DDRManager DanceManager;

	// Token: 0x040018BC RID: 6332
	public PromptScript Prompt;

	// Token: 0x040018BD RID: 6333
	public ClockScript Clock;

	// Token: 0x040018BE RID: 6334
	public GameObject DanceMinigame;

	// Token: 0x040018BF RID: 6335
	public Transform PlayerLocation;
}
