using System;
using System.Collections;
using UnityEngine;

namespace MaidDereMinigame
{
	// Token: 0x02000504 RID: 1284
	public class FailGame : MonoBehaviour
	{
		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x0600202D RID: 8237 RVA: 0x0018592C File Offset: 0x00183B2C
		public static FailGame Instance
		{
			get
			{
				if (FailGame.instance == null)
				{
					FailGame.instance = UnityEngine.Object.FindObjectOfType<FailGame>();
				}
				return FailGame.instance;
			}
		}

		// Token: 0x0600202E RID: 8238 RVA: 0x0018594C File Offset: 0x00183B4C
		private void Awake()
		{
			this.spriteRenderer = base.GetComponent<SpriteRenderer>();
			this.textRenderer = base.transform.GetChild(0).GetComponent<SpriteRenderer>();
			this.targetTransitionTime = GameController.Instance.activeDifficultyVariables.transitionTime * this.fadeMultiplier;
		}

		// Token: 0x0600202F RID: 8239 RVA: 0x00185998 File Offset: 0x00183B98
		public static void GameFailed()
		{
			FailGame.Instance.StartCoroutine(FailGame.Instance.GameFailedRoutine());
			FailGame.Instance.StartCoroutine(FailGame.Instance.SlowPitch());
			SFXController.PlaySound(SFXController.Sounds.GameFail);
		}

		// Token: 0x06002030 RID: 8240 RVA: 0x001859CA File Offset: 0x00183BCA
		private IEnumerator GameFailedRoutine()
		{
			UnityEngine.Object.FindObjectOfType<InteractionMenu>().gameObject.SetActive(false);
			yield return null;
			this.textRenderer.color = Color.white;
			while (this.transitionTime < this.targetTransitionTime)
			{
				this.transitionTime += Time.deltaTime;
				yield return null;
			}
			base.transform.GetChild(1).gameObject.SetActive(true);
			while (!Input.anyKeyDown)
			{
				yield return null;
			}
			while (this.transitionTime < this.targetTransitionTime)
			{
				this.transitionTime += Time.deltaTime;
				float a = Mathf.Lerp(0f, 1f, this.transitionTime / this.targetTransitionTime);
				this.spriteRenderer.color = new Color(0f, 0f, 0f, a);
				yield return null;
			}
			GameController.GoToExitScene(false);
			yield break;
		}

		// Token: 0x06002031 RID: 8241 RVA: 0x001859D9 File Offset: 0x00183BD9
		private IEnumerator SlowPitch()
		{
			GameStarter starter = UnityEngine.Object.FindObjectOfType<GameStarter>();
			float timeToZero = 5f;
			while (timeToZero > 0f)
			{
				starter.SetAudioPitch(Mathf.Lerp(0f, 1f, timeToZero / 5f));
				timeToZero -= Time.deltaTime;
				yield return null;
			}
			starter.SetAudioPitch(0f);
			yield break;
		}

		// Token: 0x04003DE1 RID: 15841
		private static FailGame instance;

		// Token: 0x04003DE2 RID: 15842
		public float fadeMultiplier = 2f;

		// Token: 0x04003DE3 RID: 15843
		private SpriteRenderer spriteRenderer;

		// Token: 0x04003DE4 RID: 15844
		private SpriteRenderer textRenderer;

		// Token: 0x04003DE5 RID: 15845
		private float targetTransitionTime;

		// Token: 0x04003DE6 RID: 15846
		private float transitionTime;
	}
}
