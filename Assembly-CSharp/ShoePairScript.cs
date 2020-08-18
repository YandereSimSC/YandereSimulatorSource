using System;
using UnityEngine;

// Token: 0x020003D7 RID: 983
public class ShoePairScript : MonoBehaviour
{
	// Token: 0x06001A71 RID: 6769 RVA: 0x001038BF File Offset: 0x00101ABF
	private void Start()
	{
		this.Police = GameObject.Find("Police").GetComponent<PoliceScript>();
		if (ClassGlobals.LanguageGrade + ClassGlobals.LanguageBonus < 1)
		{
			this.Prompt.enabled = false;
		}
		this.Note.SetActive(false);
	}

	// Token: 0x06001A72 RID: 6770 RVA: 0x001038FC File Offset: 0x00101AFC
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Hide();
			this.Prompt.enabled = false;
			this.Police.Suicide = true;
			this.Note.SetActive(true);
		}
	}

	// Token: 0x04002A08 RID: 10760
	public PoliceScript Police;

	// Token: 0x04002A09 RID: 10761
	public PromptScript Prompt;

	// Token: 0x04002A0A RID: 10762
	public GameObject Note;
}
