using System;
using UnityEngine;

// Token: 0x0200027A RID: 634
public class RivalEditorScript : MonoBehaviour
{
	// Token: 0x06001385 RID: 4997 RVA: 0x000A7CF9 File Offset: 0x000A5EF9
	private void Awake()
	{
		this.inputManager = UnityEngine.Object.FindObjectOfType<InputManagerScript>();
	}

	// Token: 0x06001386 RID: 4998 RVA: 0x000A7D08 File Offset: 0x000A5F08
	private void OnEnable()
	{
		this.promptBar.Label[0].text = string.Empty;
		this.promptBar.Label[1].text = "Back";
		this.promptBar.Label[4].text = string.Empty;
		this.promptBar.UpdateButtons();
	}

	// Token: 0x06001387 RID: 4999 RVA: 0x000A7D65 File Offset: 0x000A5F65
	private void HandleInput()
	{
		if (Input.GetButtonDown("B"))
		{
			this.mainPanel.gameObject.SetActive(true);
			this.rivalPanel.gameObject.SetActive(false);
		}
	}

	// Token: 0x06001388 RID: 5000 RVA: 0x000A7D95 File Offset: 0x000A5F95
	private void Update()
	{
		this.HandleInput();
	}

	// Token: 0x04001AB4 RID: 6836
	[SerializeField]
	private UIPanel mainPanel;

	// Token: 0x04001AB5 RID: 6837
	[SerializeField]
	private UIPanel rivalPanel;

	// Token: 0x04001AB6 RID: 6838
	[SerializeField]
	private UILabel titleLabel;

	// Token: 0x04001AB7 RID: 6839
	[SerializeField]
	private PromptBarScript promptBar;

	// Token: 0x04001AB8 RID: 6840
	private InputManagerScript inputManager;
}
