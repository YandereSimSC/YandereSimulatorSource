using System;
using UnityEngine;

// Token: 0x0200031A RID: 794
[RequireComponent(typeof(Camera))]
public class Letterboxing : MonoBehaviour
{
	// Token: 0x060017E1 RID: 6113 RVA: 0x000D1E70 File Offset: 0x000D0070
	private void Start()
	{
		float num = (float)Screen.width / (float)Screen.height;
		float num2 = 1f - num / 1.7777778f;
		base.GetComponent<Camera>().rect = new Rect(0f, num2 / 2f, 1f, 1f - num2);
	}

	// Token: 0x04002228 RID: 8744
	private const float KEEP_ASPECT = 1.7777778f;
}
