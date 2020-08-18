using System;
using UnityEngine;

// Token: 0x020000E2 RID: 226
public class BlowtorchScript : MonoBehaviour
{
	// Token: 0x06000A61 RID: 2657 RVA: 0x0005562B File Offset: 0x0005382B
	private void Start()
	{
		this.Flame.localScale = Vector3.zero;
		base.enabled = false;
	}

	// Token: 0x06000A62 RID: 2658 RVA: 0x00055644 File Offset: 0x00053844
	private void Update()
	{
		this.Timer = Mathf.MoveTowards(this.Timer, 5f, Time.deltaTime);
		float num = UnityEngine.Random.Range(0.9f, 1f);
		this.Flame.localScale = new Vector3(num, num, num);
		if (this.Timer == 5f)
		{
			this.Flame.localScale = Vector3.zero;
			this.Yandere.Cauterizing = false;
			this.Yandere.CanMove = true;
			base.enabled = false;
			base.GetComponent<AudioSource>().Stop();
			this.Timer = 0f;
		}
	}

	// Token: 0x04000AAC RID: 2732
	public YandereScript Yandere;

	// Token: 0x04000AAD RID: 2733
	public RagdollScript Corpse;

	// Token: 0x04000AAE RID: 2734
	public PickUpScript PickUp;

	// Token: 0x04000AAF RID: 2735
	public PromptScript Prompt;

	// Token: 0x04000AB0 RID: 2736
	public Transform Flame;

	// Token: 0x04000AB1 RID: 2737
	public float Timer;
}
