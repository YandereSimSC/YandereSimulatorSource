using System;
using UnityEngine;

// Token: 0x02000494 RID: 1172
public class EyeTestScript : MonoBehaviour
{
	// Token: 0x06001E07 RID: 7687 RVA: 0x0017828C File Offset: 0x0017648C
	private void Start()
	{
		this.MyAnimation["moodyEyes_00"].layer = 1;
		this.MyAnimation.Play("moodyEyes_00");
		this.MyAnimation["moodyEyes_00"].weight = 1f;
		this.MyAnimation.Play("moodyEyes_00");
	}

	// Token: 0x04003BDF RID: 15327
	public Animation MyAnimation;
}
