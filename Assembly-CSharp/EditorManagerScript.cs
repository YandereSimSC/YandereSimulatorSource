using System;
using System.Collections.Generic;
using System.IO;
using JsonFx.Json;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000278 RID: 632
public class EditorManagerScript : MonoBehaviour
{
	// Token: 0x06001379 RID: 4985 RVA: 0x000A7A6A File Offset: 0x000A5C6A
	private void Awake()
	{
		this.buttonIndex = 0;
		this.inputManager = UnityEngine.Object.FindObjectOfType<InputManagerScript>();
	}

	// Token: 0x0600137A RID: 4986 RVA: 0x000A7A80 File Offset: 0x000A5C80
	private void Start()
	{
		this.promptBar.Label[0].text = "Select";
		this.promptBar.Label[1].text = "Exit";
		this.promptBar.Label[4].text = "Choose";
		this.promptBar.UpdateButtons();
	}

	// Token: 0x0600137B RID: 4987 RVA: 0x000A7AE0 File Offset: 0x000A5CE0
	private void OnEnable()
	{
		this.promptBar.Label[0].text = "Select";
		this.promptBar.Label[1].text = "Exit";
		this.promptBar.Label[4].text = "Choose";
		this.promptBar.UpdateButtons();
	}

	// Token: 0x0600137C RID: 4988 RVA: 0x000A7B3D File Offset: 0x000A5D3D
	public static Dictionary<string, object>[] DeserializeJson(string filename)
	{
		return JsonReader.Deserialize<Dictionary<string, object>[]>(File.ReadAllText(Path.Combine(Application.streamingAssetsPath, Path.Combine("JSON", filename))));
	}

	// Token: 0x0600137D RID: 4989 RVA: 0x000A7B60 File Offset: 0x000A5D60
	private void HandleInput()
	{
		if (Input.GetButtonDown("B"))
		{
			SceneManager.LoadScene("TitleScene");
		}
		bool tappedUp = this.inputManager.TappedUp;
		bool tappedDown = this.inputManager.TappedDown;
		if (tappedUp)
		{
			this.buttonIndex = ((this.buttonIndex > 0) ? (this.buttonIndex - 1) : 2);
		}
		else if (tappedDown)
		{
			this.buttonIndex = ((this.buttonIndex < 2) ? (this.buttonIndex + 1) : 0);
		}
		if (tappedUp || tappedDown)
		{
			Transform transform = this.cursorLabel.transform;
			transform.localPosition = new Vector3(transform.localPosition.x, 100f - (float)this.buttonIndex * 100f, transform.localPosition.z);
		}
		if (Input.GetButtonDown("A"))
		{
			this.editorPanels[this.buttonIndex].gameObject.SetActive(true);
			this.mainPanel.gameObject.SetActive(false);
		}
	}

	// Token: 0x0600137E RID: 4990 RVA: 0x000A7C4F File Offset: 0x000A5E4F
	private void Update()
	{
		this.HandleInput();
	}

	// Token: 0x04001AA8 RID: 6824
	[SerializeField]
	private UIPanel mainPanel;

	// Token: 0x04001AA9 RID: 6825
	[SerializeField]
	private UIPanel[] editorPanels;

	// Token: 0x04001AAA RID: 6826
	[SerializeField]
	private UILabel cursorLabel;

	// Token: 0x04001AAB RID: 6827
	[SerializeField]
	private PromptBarScript promptBar;

	// Token: 0x04001AAC RID: 6828
	private int buttonIndex;

	// Token: 0x04001AAD RID: 6829
	private const int ButtonCount = 3;

	// Token: 0x04001AAE RID: 6830
	private InputManagerScript inputManager;
}
