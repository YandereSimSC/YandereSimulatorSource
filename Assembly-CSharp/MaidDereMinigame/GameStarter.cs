using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaidDereMinigame
{
	// Token: 0x020004F6 RID: 1270
	public class GameStarter : MonoBehaviour
	{
		// Token: 0x06001FFA RID: 8186 RVA: 0x00185088 File Offset: 0x00183288
		private void Awake()
		{
			this.spriteRenderer = base.GetComponent<SpriteRenderer>();
			this.audioSource = base.GetComponent<AudioSource>();
			base.StartCoroutine(this.CountdownToStart());
			GameController.Instance.tipPage = this.tipPage;
			GameController.Instance.whiteFadeOutPost = this.whiteFadeOutPost;
		}

		// Token: 0x06001FFB RID: 8187 RVA: 0x001850DC File Offset: 0x001832DC
		public void SetGameTime(float gameTime)
		{
			int num = Mathf.CeilToInt(gameTime);
			if ((float)num == 10f)
			{
				SFXController.PlaySound(SFXController.Sounds.ClockTick);
			}
			if (gameTime > 3f)
			{
				return;
			}
			if (num - 1 <= 2)
			{
				this.spriteRenderer.sprite = this.numbers[num];
				return;
			}
			this.EndGame();
		}

		// Token: 0x06001FFC RID: 8188 RVA: 0x0018512C File Offset: 0x0018332C
		public void EndGame()
		{
			base.StartCoroutine(this.EndGameRoutine());
			SFXController.PlaySound(SFXController.Sounds.GameSuccess);
		}

		// Token: 0x06001FFD RID: 8189 RVA: 0x00185141 File Offset: 0x00183341
		private IEnumerator CountdownToStart()
		{
			yield return new WaitForSeconds(GameController.Instance.activeDifficultyVariables.transitionTime);
			SFXController.PlaySound(SFXController.Sounds.Countdown);
			while (this.timeToStart > 0)
			{
				yield return new WaitForSeconds(1f);
				this.timeToStart--;
				this.spriteRenderer.sprite = this.numbers[this.timeToStart];
			}
			yield return new WaitForSeconds(1f);
			GameController.SetPause(false);
			this.spriteRenderer.sprite = null;
			yield break;
		}

		// Token: 0x06001FFE RID: 8190 RVA: 0x00185150 File Offset: 0x00183350
		private IEnumerator EndGameRoutine()
		{
			GameController.SetPause(true);
			this.spriteRenderer.sprite = this.timeUp;
			yield return new WaitForSeconds(1f);
			UnityEngine.Object.FindObjectOfType<InteractionMenu>().gameObject.SetActive(false);
			GameController.TimeUp();
			yield break;
		}

		// Token: 0x06001FFF RID: 8191 RVA: 0x0018515F File Offset: 0x0018335F
		public void SetAudioPitch(float value)
		{
			this.audioSource.pitch = value;
		}

		// Token: 0x04003DBF RID: 15807
		public List<Sprite> numbers;

		// Token: 0x04003DC0 RID: 15808
		public SpriteRenderer whiteFadeOutPost;

		// Token: 0x04003DC1 RID: 15809
		public Sprite timeUp;

		// Token: 0x04003DC2 RID: 15810
		public TipPage tipPage;

		// Token: 0x04003DC3 RID: 15811
		private AudioSource audioSource;

		// Token: 0x04003DC4 RID: 15812
		private SpriteRenderer spriteRenderer;

		// Token: 0x04003DC5 RID: 15813
		private int timeToStart = 3;
	}
}
