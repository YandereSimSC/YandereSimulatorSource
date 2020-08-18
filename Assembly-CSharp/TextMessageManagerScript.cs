using System;
using UnityEngine;

// Token: 0x0200041C RID: 1052
public class TextMessageManagerScript : MonoBehaviour
{
	// Token: 0x06001C20 RID: 7200 RVA: 0x0014F968 File Offset: 0x0014DB68
	private void Update()
	{
		if (Input.GetButtonDown("B"))
		{
			UnityEngine.Object.Destroy(this.NewMessage);
			this.PromptBar.ClearButtons();
			this.PromptBar.Label[0].text = "Accept";
			this.PromptBar.Label[1].text = "Exit";
			this.PromptBar.Label[5].text = "Choose";
			this.PromptBar.UpdateButtons();
			this.PauseScreen.Sideways = true;
			this.ServicesMenu.SetActive(true);
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x06001C21 RID: 7201 RVA: 0x0014FA10 File Offset: 0x0014DC10
	public void SpawnMessage(int ServiceID)
	{
		this.PromptBar.ClearButtons();
		this.PromptBar.Label[1].text = "Exit";
		this.PromptBar.UpdateButtons();
		this.PauseScreen.Sideways = false;
		this.ServicesMenu.SetActive(false);
		base.gameObject.SetActive(true);
		if (this.NewMessage != null)
		{
			UnityEngine.Object.Destroy(this.NewMessage);
		}
		this.NewMessage = UnityEngine.Object.Instantiate<GameObject>(this.Message);
		this.NewMessage.transform.parent = base.transform;
		this.NewMessage.transform.localPosition = new Vector3(-225f, -275f, 0f);
		this.NewMessage.transform.localEulerAngles = Vector3.zero;
		this.NewMessage.transform.localScale = new Vector3(1f, 1f, 1f);
		this.MessageText = this.Messages[ServiceID];
		if (ServiceID == 7 || ServiceID == 4)
		{
			this.MessageHeight = 11;
		}
		else
		{
			this.MessageHeight = 5;
		}
		this.NewMessage.GetComponent<UISprite>().height = 36 + 36 * this.MessageHeight;
		this.NewMessage.GetComponent<TextMessageScript>().Label.text = this.MessageText;
	}

	// Token: 0x04003488 RID: 13448
	public PauseScreenScript PauseScreen;

	// Token: 0x04003489 RID: 13449
	public PromptBarScript PromptBar;

	// Token: 0x0400348A RID: 13450
	public GameObject ServicesMenu;

	// Token: 0x0400348B RID: 13451
	public string[] Messages;

	// Token: 0x0400348C RID: 13452
	private GameObject NewMessage;

	// Token: 0x0400348D RID: 13453
	public GameObject Message;

	// Token: 0x0400348E RID: 13454
	public int MessageHeight;

	// Token: 0x0400348F RID: 13455
	public string MessageText = string.Empty;
}
