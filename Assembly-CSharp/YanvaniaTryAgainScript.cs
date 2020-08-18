using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000486 RID: 1158
public class YanvaniaTryAgainScript : MonoBehaviour
{
	// Token: 0x06001DD3 RID: 7635 RVA: 0x00173EB4 File Offset: 0x001720B4
	private void Start()
	{
		base.transform.localScale = Vector3.zero;
	}

	// Token: 0x06001DD4 RID: 7636 RVA: 0x00173EC8 File Offset: 0x001720C8
	private void Update()
	{
		if (!this.FadeOut)
		{
			if (base.transform.localScale.x > 0.9f)
			{
				if (this.InputManager.TappedLeft)
				{
					this.Selected = 1;
				}
				if (this.InputManager.TappedRight)
				{
					this.Selected = 2;
				}
				if (this.Selected == 1)
				{
					this.Highlight.localPosition = new Vector3(Mathf.Lerp(this.Highlight.localPosition.x, -100f, Time.deltaTime * 10f), this.Highlight.localPosition.y, this.Highlight.localPosition.z);
					this.Highlight.localScale = new Vector3(Mathf.Lerp(this.Highlight.localScale.x, -1f, Time.deltaTime * 10f), this.Highlight.localScale.y, this.Highlight.localScale.z);
				}
				else
				{
					this.Highlight.localPosition = new Vector3(Mathf.Lerp(this.Highlight.localPosition.x, 100f, Time.deltaTime * 10f), this.Highlight.localPosition.y, this.Highlight.localPosition.z);
					this.Highlight.localScale = new Vector3(Mathf.Lerp(this.Highlight.localScale.x, 1f, Time.deltaTime * 10f), this.Highlight.localScale.y, this.Highlight.localScale.z);
				}
				if (Input.GetButtonDown("A") || Input.GetKeyDown("z") || Input.GetKeyDown("x"))
				{
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ButtonEffect, this.Highlight.position, Quaternion.identity);
					gameObject.transform.parent = this.Highlight;
					gameObject.transform.localPosition = Vector3.zero;
					this.FadeOut = true;
					return;
				}
			}
		}
		else
		{
			this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, this.Darkness.color.a + Time.deltaTime);
			if (this.Darkness.color.a >= 1f)
			{
				if (this.Selected == 1)
				{
					SceneManager.LoadScene(SceneManager.GetActiveScene().name);
					return;
				}
				SceneManager.LoadScene("YanvaniaTitleScene");
			}
		}
	}

	// Token: 0x04003B19 RID: 15129
	public InputManagerScript InputManager;

	// Token: 0x04003B1A RID: 15130
	public GameObject ButtonEffect;

	// Token: 0x04003B1B RID: 15131
	public Transform Highlight;

	// Token: 0x04003B1C RID: 15132
	public UISprite Darkness;

	// Token: 0x04003B1D RID: 15133
	public bool FadeOut;

	// Token: 0x04003B1E RID: 15134
	public int Selected = 1;
}
