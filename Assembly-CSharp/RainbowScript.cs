using System;
using UnityEngine;

// Token: 0x02000385 RID: 901
public class RainbowScript : MonoBehaviour
{
	// Token: 0x0600197F RID: 6527 RVA: 0x000F8869 File Offset: 0x000F6A69
	private void Start()
	{
		this.MyRenderer.material.color = Color.red;
		this.cyclesPerSecond = 0.25f;
	}

	// Token: 0x06001980 RID: 6528 RVA: 0x000F888C File Offset: 0x000F6A8C
	private void Update()
	{
		this.percent = (this.percent + Time.deltaTime * this.cyclesPerSecond) % 1f;
		this.MyRenderer.material.color = Color.HSVToRGB(this.percent, 1f, 1f);
	}

	// Token: 0x04002753 RID: 10067
	[SerializeField]
	private Renderer MyRenderer;

	// Token: 0x04002754 RID: 10068
	[SerializeField]
	private float cyclesPerSecond;

	// Token: 0x04002755 RID: 10069
	[SerializeField]
	private float percent;
}
