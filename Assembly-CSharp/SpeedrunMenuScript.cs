using System;
using UnityEngine;

// Token: 0x020003E8 RID: 1000
public class SpeedrunMenuScript : MonoBehaviour
{
	// Token: 0x06001ABB RID: 6843 RVA: 0x0010C371 File Offset: 0x0010A571
	private void Start()
	{
		this.YandereAnim["f02_nierRun_00"].speed = 1.5f;
	}

	// Token: 0x06001ABC RID: 6844 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x04002B3B RID: 11067
	public Animation YandereAnim;
}
