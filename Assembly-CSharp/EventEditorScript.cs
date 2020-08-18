using System;
using UnityEngine;

// Token: 0x02000279 RID: 633
public class EventEditorScript : MonoBehaviour
{
	// Token: 0x06001380 RID: 4992 RVA: 0x000A7C57 File Offset: 0x000A5E57
	private void Awake()
	{
		this.inputManager = UnityEngine.Object.FindObjectOfType<InputManagerScript>();
	}

	// Token: 0x06001381 RID: 4993 RVA: 0x000A7C64 File Offset: 0x000A5E64
	private void OnEnable()
	{
		this.promptBar.Label[0].text = string.Empty;
		this.promptBar.Label[1].text = "Back";
		this.promptBar.Label[4].text = string.Empty;
		this.promptBar.UpdateButtons();
	}

	// Token: 0x06001382 RID: 4994 RVA: 0x000A7CC1 File Offset: 0x000A5EC1
	private void HandleInput()
	{
		if (Input.GetButtonDown("B"))
		{
			this.mainPanel.gameObject.SetActive(true);
			this.eventPanel.gameObject.SetActive(false);
		}
	}

	// Token: 0x06001383 RID: 4995 RVA: 0x000A7CF1 File Offset: 0x000A5EF1
	private void Update()
	{
		this.HandleInput();
	}

	// Token: 0x04001AAF RID: 6831
	[SerializeField]
	private UIPanel mainPanel;

	// Token: 0x04001AB0 RID: 6832
	[SerializeField]
	private UIPanel eventPanel;

	// Token: 0x04001AB1 RID: 6833
	[SerializeField]
	private UILabel titleLabel;

	// Token: 0x04001AB2 RID: 6834
	[SerializeField]
	private PromptBarScript promptBar;

	// Token: 0x04001AB3 RID: 6835
	private InputManagerScript inputManager;
}
