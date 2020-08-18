using System;
using UnityEngine;

// Token: 0x02000083 RID: 131
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Internal/Snapshot Point")]
public class UISnapshotPoint : MonoBehaviour
{
	// Token: 0x0600054D RID: 1357 RVA: 0x0003211C File Offset: 0x0003031C
	private void Start()
	{
		if (base.tag != "EditorOnly")
		{
			base.tag = "EditorOnly";
		}
	}

	// Token: 0x04000580 RID: 1408
	public bool isOrthographic = true;

	// Token: 0x04000581 RID: 1409
	public float nearClip = -100f;

	// Token: 0x04000582 RID: 1410
	public float farClip = 100f;

	// Token: 0x04000583 RID: 1411
	[Range(10f, 80f)]
	public int fieldOfView = 35;

	// Token: 0x04000584 RID: 1412
	public float orthoSize = 30f;

	// Token: 0x04000585 RID: 1413
	public Texture2D thumbnail;
}
