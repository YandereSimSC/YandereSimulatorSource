using System;
using UnityEngine;

// Token: 0x02000369 RID: 873
public class PoisonScript : MonoBehaviour
{
	// Token: 0x06001903 RID: 6403 RVA: 0x000EA468 File Offset: 0x000E8668
	public void Start()
	{
		base.gameObject.SetActive(ClassGlobals.ChemistryGrade + ClassGlobals.ChemistryBonus >= 1);
	}

	// Token: 0x06001904 RID: 6404 RVA: 0x000EA488 File Offset: 0x000E8688
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Yandere.Inventory.ChemicalPoison = true;
			this.Yandere.StudentManager.UpdateAllBentos();
			UnityEngine.Object.Destroy(base.gameObject);
			UnityEngine.Object.Destroy(this.Bottle);
		}
	}

	// Token: 0x04002593 RID: 9619
	public YandereScript Yandere;

	// Token: 0x04002594 RID: 9620
	public PromptScript Prompt;

	// Token: 0x04002595 RID: 9621
	public GameObject Bottle;
}
