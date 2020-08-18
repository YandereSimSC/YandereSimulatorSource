using System;
using UnityEngine;

namespace MaidDereMinigame
{
	// Token: 0x02000508 RID: 1288
	public class Timer : Meter
	{
		// Token: 0x06002046 RID: 8262 RVA: 0x00185E4B File Offset: 0x0018404B
		private void Awake()
		{
			this.gameTime = GameController.Instance.activeDifficultyVariables.gameTime;
			this.starter = UnityEngine.Object.FindObjectOfType<GameStarter>();
			this.isPaused = true;
		}

		// Token: 0x06002047 RID: 8263 RVA: 0x00185E74 File Offset: 0x00184074
		private void OnEnable()
		{
			GameController.PauseGame = (BoolParameterEvent)Delegate.Combine(GameController.PauseGame, new BoolParameterEvent(this.SetPause));
		}

		// Token: 0x06002048 RID: 8264 RVA: 0x00185E96 File Offset: 0x00184096
		private void OnDisable()
		{
			GameController.PauseGame = (BoolParameterEvent)Delegate.Remove(GameController.PauseGame, new BoolParameterEvent(this.SetPause));
		}

		// Token: 0x06002049 RID: 8265 RVA: 0x00185EB8 File Offset: 0x001840B8
		public void SetPause(bool toPause)
		{
			this.isPaused = toPause;
		}

		// Token: 0x0600204A RID: 8266 RVA: 0x00185EC4 File Offset: 0x001840C4
		private void Update()
		{
			if (this.isPaused)
			{
				return;
			}
			this.gameTime -= Time.deltaTime;
			base.SetFill(this.gameTime / GameController.Instance.activeDifficultyVariables.gameTime);
			this.starter.SetGameTime(this.gameTime);
		}

		// Token: 0x04003DFD RID: 15869
		private GameStarter starter;

		// Token: 0x04003DFE RID: 15870
		private float gameTime;

		// Token: 0x04003DFF RID: 15871
		private bool isPaused;
	}
}
