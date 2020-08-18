using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaidDereMinigame
{
	// Token: 0x020004FC RID: 1276
	public class FlipBook : MonoBehaviour
	{
		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x0600200C RID: 8204 RVA: 0x00185307 File Offset: 0x00183507
		public static FlipBook Instance
		{
			get
			{
				if (FlipBook.instance == null)
				{
					FlipBook.instance = UnityEngine.Object.FindObjectOfType<FlipBook>();
				}
				return FlipBook.instance;
			}
		}

		// Token: 0x0600200D RID: 8205 RVA: 0x00185325 File Offset: 0x00183525
		private void Awake()
		{
			base.StartCoroutine(this.OpenBook());
		}

		// Token: 0x0600200E RID: 8206 RVA: 0x00185334 File Offset: 0x00183534
		private IEnumerator OpenBook()
		{
			yield return new WaitForSeconds(1f);
			this.FlipToPage(1);
			yield break;
		}

		// Token: 0x0600200F RID: 8207 RVA: 0x00185343 File Offset: 0x00183543
		private void Update()
		{
			if (this.stopInputs)
			{
				return;
			}
			if (this.curPage > 1 && Input.GetButtonDown("B") && this.canGoBack)
			{
				this.FlipToPage(1);
			}
		}

		// Token: 0x06002010 RID: 8208 RVA: 0x00185372 File Offset: 0x00183572
		public void StopInputs()
		{
			this.stopInputs = true;
		}

		// Token: 0x06002011 RID: 8209 RVA: 0x0018537B File Offset: 0x0018357B
		public void FlipToPage(int page)
		{
			SFXController.PlaySound(SFXController.Sounds.PageTurn);
			base.StartCoroutine(this.FlipToPageRoutine(page));
		}

		// Token: 0x06002012 RID: 8210 RVA: 0x00185391 File Offset: 0x00183591
		private IEnumerator FlipToPageRoutine(int page)
		{
			bool flag = this.curPage < page;
			this.canGoBack = false;
			if (flag)
			{
				while (this.curPage < page)
				{
					List<FlipBookPage> list = this.flipBookPages;
					int num = this.curPage;
					this.curPage = num + 1;
					list[num].Transition(flag);
				}
				yield return new WaitForSeconds(0.4f);
				this.flipBookPages[this.curPage].ObjectActive(true);
			}
			else
			{
				this.flipBookPages[this.curPage].ObjectActive(false);
				while (this.curPage > page)
				{
					List<FlipBookPage> list2 = this.flipBookPages;
					int num = this.curPage - 1;
					this.curPage = num;
					list2[num].Transition(flag);
				}
				yield return new WaitForSeconds(0.6f);
				this.flipBookPages[this.curPage].ObjectActive(true);
			}
			this.canGoBack = true;
			yield break;
		}

		// Token: 0x04003DCC RID: 15820
		private static FlipBook instance;

		// Token: 0x04003DCD RID: 15821
		public List<FlipBookPage> flipBookPages;

		// Token: 0x04003DCE RID: 15822
		private int curPage;

		// Token: 0x04003DCF RID: 15823
		private bool canGoBack;

		// Token: 0x04003DD0 RID: 15824
		private bool stopInputs;
	}
}
