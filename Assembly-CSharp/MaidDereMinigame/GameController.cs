using System;
using System.Collections;
using System.Collections.Generic;
using MaidDereMinigame.Malee;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MaidDereMinigame
{
	// Token: 0x020004F3 RID: 1267
	public class GameController : MonoBehaviour
	{
		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x06001FEC RID: 8172 RVA: 0x00184D9E File Offset: 0x00182F9E
		public static GameController Instance
		{
			get
			{
				if (GameController.instance == null)
				{
					GameController.instance = UnityEngine.Object.FindObjectOfType<GameController>();
				}
				return GameController.instance;
			}
		}

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x06001FED RID: 8173 RVA: 0x00184DBC File Offset: 0x00182FBC
		public static SceneWrapper Scenes
		{
			get
			{
				return GameController.Instance.scenes;
			}
		}

		// Token: 0x06001FEE RID: 8174 RVA: 0x00184DC8 File Offset: 0x00182FC8
		public static void GoToExitScene(bool fadeOut = true)
		{
			GameController.Instance.StartCoroutine(GameController.Instance.FadeWithAction(delegate
			{
				PlayerGlobals.Money += GameController.Instance.totalPayout;
				if (SceneManager.GetActiveScene().name == "MaidMenuScene")
				{
					SceneManager.LoadScene("StreetScene");
					return;
				}
				SceneManager.LoadScene("CalendarScene");
			}, fadeOut, true));
		}

		// Token: 0x06001FEF RID: 8175 RVA: 0x00184E00 File Offset: 0x00183000
		private void Awake()
		{
			if (GameController.Instance != this)
			{
				UnityEngine.Object.DestroyImmediate(base.gameObject);
				return;
			}
			this.spriteRenderer = base.GetComponent<SpriteRenderer>();
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}

		// Token: 0x06001FF0 RID: 8176 RVA: 0x00184E32 File Offset: 0x00183032
		public static void SetPause(bool toPause)
		{
			if (GameController.PauseGame != null)
			{
				GameController.PauseGame(toPause);
			}
		}

		// Token: 0x06001FF1 RID: 8177 RVA: 0x00184E46 File Offset: 0x00183046
		public void LoadScene(SceneObject scene)
		{
			base.StartCoroutine(this.FadeWithAction(delegate
			{
				SceneManager.LoadScene("MaidGameScene");
			}, true, false));
		}

		// Token: 0x06001FF2 RID: 8178 RVA: 0x00184E76 File Offset: 0x00183076
		private IEnumerator FadeWithAction(Action PostFadeAction, bool doFadeOut = true, bool destroyGameController = false)
		{
			float timeToFade = 0f;
			if (doFadeOut)
			{
				while (timeToFade <= this.activeDifficultyVariables.transitionTime)
				{
					this.spriteRenderer.color = new Color(1f, 1f, 1f, Mathf.Lerp(0f, 1f, timeToFade / this.activeDifficultyVariables.transitionTime));
					timeToFade += Time.deltaTime;
					yield return null;
				}
				this.spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
			}
			else
			{
				timeToFade = this.activeDifficultyVariables.transitionTime;
			}
			PostFadeAction();
			if (destroyGameController)
			{
				if (GameController.Instance.whiteFadeOutPost != null && doFadeOut)
				{
					GameController.Instance.whiteFadeOutPost.color = Color.white;
				}
				UnityEngine.Object.Destroy(GameController.Instance.gameObject);
				Camera.main.farClipPlane = 0f;
				GameController.instance = null;
			}
			else
			{
				while (timeToFade >= 0f)
				{
					this.spriteRenderer.color = new Color(1f, 1f, 1f, Mathf.Lerp(0f, 1f, timeToFade / this.activeDifficultyVariables.transitionTime));
					timeToFade -= Time.deltaTime;
					yield return null;
				}
				this.spriteRenderer.color = new Color(1f, 1f, 1f, 0f);
			}
			yield break;
		}

		// Token: 0x06001FF3 RID: 8179 RVA: 0x00184E9A File Offset: 0x0018309A
		public static void TimeUp()
		{
			GameController.SetPause(true);
			GameController.Instance.tipPage.Init();
			GameController.Instance.tipPage.DisplayTips(GameController.Instance.tips);
			UnityEngine.Object.FindObjectOfType<GameStarter>().GetComponent<AudioSource>().Stop();
		}

		// Token: 0x06001FF4 RID: 8180 RVA: 0x00184EDC File Offset: 0x001830DC
		public static void AddTip(float tip)
		{
			if (GameController.Instance.tips == null)
			{
				GameController.Instance.tips = new List<float>();
			}
			tip = Mathf.Floor(tip * 100f) / 100f;
			GameController.Instance.tips.Add(tip);
		}

		// Token: 0x06001FF5 RID: 8181 RVA: 0x00184F28 File Offset: 0x00183128
		public static float GetTotalDollars()
		{
			float num = 0f;
			foreach (float num2 in GameController.Instance.tips)
			{
				num += Mathf.Floor(num2 * 100f) / 100f;
			}
			return num + GameController.Instance.activeDifficultyVariables.basePay;
		}

		// Token: 0x06001FF6 RID: 8182 RVA: 0x00184FA4 File Offset: 0x001831A4
		public static void AddAngryCustomer()
		{
			GameController.Instance.angryCustomers++;
			if (GameController.Instance.angryCustomers >= GameController.Instance.activeDifficultyVariables.failQuantity)
			{
				FailGame.GameFailed();
				GameController.SetPause(true);
			}
		}

		// Token: 0x04003DA3 RID: 15779
		private static GameController instance;

		// Token: 0x04003DA4 RID: 15780
		[Reorderable]
		public Sprites numbers;

		// Token: 0x04003DA5 RID: 15781
		public SceneWrapper scenes;

		// Token: 0x04003DA6 RID: 15782
		[Tooltip("Scene Object Reference to return to when the game ends.")]
		public SceneObject returnScene;

		// Token: 0x04003DA7 RID: 15783
		public SetupVariables activeDifficultyVariables;

		// Token: 0x04003DA8 RID: 15784
		public SetupVariables easyVariables;

		// Token: 0x04003DA9 RID: 15785
		public SetupVariables mediumVariables;

		// Token: 0x04003DAA RID: 15786
		public SetupVariables hardVariables;

		// Token: 0x04003DAB RID: 15787
		private List<float> tips;

		// Token: 0x04003DAC RID: 15788
		private SpriteRenderer spriteRenderer;

		// Token: 0x04003DAD RID: 15789
		private int angryCustomers;

		// Token: 0x04003DAE RID: 15790
		[HideInInspector]
		public TipPage tipPage;

		// Token: 0x04003DAF RID: 15791
		[HideInInspector]
		public float totalPayout;

		// Token: 0x04003DB0 RID: 15792
		[HideInInspector]
		public SpriteRenderer whiteFadeOutPost;

		// Token: 0x04003DB1 RID: 15793
		public static BoolParameterEvent PauseGame;
	}
}
