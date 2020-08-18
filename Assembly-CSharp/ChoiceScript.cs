using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000236 RID: 566
public class ChoiceScript : MonoBehaviour
{
	// Token: 0x0600123C RID: 4668 RVA: 0x00081110 File Offset: 0x0007F310
	private void Start()
	{
		this.Darkness.color = new Color(1f, 1f, 1f, 1f);
	}

	// Token: 0x0600123D RID: 4669 RVA: 0x00081138 File Offset: 0x0007F338
	private void Update()
	{
		this.Highlight.transform.localPosition = Vector3.Lerp(this.Highlight.transform.localPosition, new Vector3((float)(-360 + 720 * this.Selected), this.Highlight.transform.localPosition.y, this.Highlight.transform.localPosition.z), Time.deltaTime * 10f);
		if (this.Phase == 0)
		{
			this.Darkness.color = new Color(1f, 1f, 1f, Mathf.MoveTowards(this.Darkness.color.a, 0f, Time.deltaTime * 2f));
			if (this.Darkness.color.a == 0f)
			{
				this.Phase++;
				return;
			}
		}
		else if (this.Phase == 1)
		{
			if (this.InputManager.TappedLeft)
			{
				this.Darkness.color = new Color(1f, 1f, 1f, 0f);
				this.Selected = 0;
			}
			else if (this.InputManager.TappedRight)
			{
				this.Darkness.color = new Color(0f, 0f, 0f, 0f);
				this.Selected = 1;
			}
			if (Input.GetButtonDown("A"))
			{
				this.Phase++;
				return;
			}
		}
		else if (this.Phase == 2)
		{
			this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, Mathf.MoveTowards(this.Darkness.color.a, 1f, Time.deltaTime * 2f));
			if (this.Darkness.color.a == 1f)
			{
				GameGlobals.LoveSick = (this.Selected == 1);
				SceneManager.LoadScene("TitleScene");
			}
		}
	}

	// Token: 0x04001586 RID: 5510
	public InputManagerScript InputManager;

	// Token: 0x04001587 RID: 5511
	public PromptBarScript PromptBar;

	// Token: 0x04001588 RID: 5512
	public Transform Highlight;

	// Token: 0x04001589 RID: 5513
	public UISprite Darkness;

	// Token: 0x0400158A RID: 5514
	public int Selected;

	// Token: 0x0400158B RID: 5515
	public int Phase;
}
