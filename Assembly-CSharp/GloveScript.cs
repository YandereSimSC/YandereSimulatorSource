using System;
using UnityEngine;

// Token: 0x020002D4 RID: 724
public class GloveScript : MonoBehaviour
{
	// Token: 0x060016C7 RID: 5831 RVA: 0x000BC554 File Offset: 0x000BA754
	private void Start()
	{
		Physics.IgnoreCollision(GameObject.Find("YandereChan").GetComponent<YandereScript>().GetComponent<Collider>(), this.MyCollider);
		if (base.transform.position.y > 1000f)
		{
			base.transform.position = new Vector3(12f, 0f, 28f);
		}
	}

	// Token: 0x060016C8 RID: 5832 RVA: 0x000BC5B8 File Offset: 0x000BA7B8
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			base.transform.parent = this.Prompt.Yandere.transform;
			base.transform.localPosition = new Vector3(0f, 1f, 0.25f);
			this.Prompt.Yandere.Gloves = this;
			this.Prompt.Yandere.WearGloves();
			base.gameObject.SetActive(false);
		}
		this.Prompt.HideButton[0] = (this.Prompt.Yandere.Schoolwear != 1 || this.Prompt.Yandere.ClubAttire);
	}

	// Token: 0x04001DF5 RID: 7669
	public PromptScript Prompt;

	// Token: 0x04001DF6 RID: 7670
	public PickUpScript PickUp;

	// Token: 0x04001DF7 RID: 7671
	public Collider MyCollider;

	// Token: 0x04001DF8 RID: 7672
	public Projector Blood;
}
