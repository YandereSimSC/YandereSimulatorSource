using System;
using UnityEngine;

// Token: 0x02000386 RID: 902
public class RandomAnimScript : MonoBehaviour
{
	// Token: 0x06001982 RID: 6530 RVA: 0x000F88DD File Offset: 0x000F6ADD
	private void Start()
	{
		this.PickRandomAnim();
		base.GetComponent<Animation>().CrossFade(this.CurrentAnim);
	}

	// Token: 0x06001983 RID: 6531 RVA: 0x000F88F8 File Offset: 0x000F6AF8
	private void Update()
	{
		AnimationState animationState = base.GetComponent<Animation>()[this.CurrentAnim];
		if (animationState.time >= animationState.length)
		{
			this.PickRandomAnim();
		}
	}

	// Token: 0x06001984 RID: 6532 RVA: 0x000F892B File Offset: 0x000F6B2B
	private void PickRandomAnim()
	{
		this.CurrentAnim = this.AnimationNames[UnityEngine.Random.Range(0, this.AnimationNames.Length)];
		base.GetComponent<Animation>().CrossFade(this.CurrentAnim);
	}

	// Token: 0x04002756 RID: 10070
	public string[] AnimationNames;

	// Token: 0x04002757 RID: 10071
	public string CurrentAnim = string.Empty;
}
