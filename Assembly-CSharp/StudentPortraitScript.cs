using System;
using UnityEngine;

// Token: 0x02000405 RID: 1029
public class StudentPortraitScript : MonoBehaviour
{
	// Token: 0x06001B68 RID: 7016 RVA: 0x0011ACD4 File Offset: 0x00118ED4
	private void Start()
	{
		this.DeathShadow.SetActive(false);
		this.PrisonBars.SetActive(false);
		this.Panties.SetActive(false);
		this.Friend.SetActive(false);
	}

	// Token: 0x04002DCF RID: 11727
	public GameObject DeathShadow;

	// Token: 0x04002DD0 RID: 11728
	public GameObject PrisonBars;

	// Token: 0x04002DD1 RID: 11729
	public GameObject Panties;

	// Token: 0x04002DD2 RID: 11730
	public GameObject Friend;

	// Token: 0x04002DD3 RID: 11731
	public UITexture Portrait;
}
