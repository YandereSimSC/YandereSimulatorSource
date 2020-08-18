using System;
using UnityEngine;

// Token: 0x0200023C RID: 572
public class ClothingScript : MonoBehaviour
{
	// Token: 0x0600125A RID: 4698 RVA: 0x00083E48 File Offset: 0x00082048
	private void Start()
	{
		this.Yandere = GameObject.Find("YandereChan").GetComponent<YandereScript>();
	}

	// Token: 0x0600125B RID: 4699 RVA: 0x00083E60 File Offset: 0x00082060
	private void Update()
	{
		if (this.CanPickUp)
		{
			if (this.Yandere.Bloodiness == 0f)
			{
				this.CanPickUp = false;
				this.Prompt.Hide();
				this.Prompt.enabled = false;
			}
		}
		else if (this.Yandere.Bloodiness > 0f)
		{
			this.CanPickUp = true;
			this.Prompt.enabled = true;
		}
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Yandere.Bloodiness = 0f;
			UnityEngine.Object.Instantiate<GameObject>(this.FoldedUniform, base.transform.position + Vector3.up, Quaternion.identity);
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x040015E9 RID: 5609
	public YandereScript Yandere;

	// Token: 0x040015EA RID: 5610
	public PromptScript Prompt;

	// Token: 0x040015EB RID: 5611
	public GameObject FoldedUniform;

	// Token: 0x040015EC RID: 5612
	public bool CanPickUp;
}
