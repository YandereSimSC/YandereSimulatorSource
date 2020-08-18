using System;
using UnityEngine;

// Token: 0x020000DC RID: 220
public class BlendshapeScript : MonoBehaviour
{
	// Token: 0x06000A4B RID: 2635 RVA: 0x00054A10 File Offset: 0x00052C10
	private void LateUpdate()
	{
		this.Happiness += Time.deltaTime * 10f;
		this.MyMesh.SetBlendShapeWeight(0, this.Happiness);
		this.Blink += Time.deltaTime * 10f;
		this.MyMesh.SetBlendShapeWeight(8, 100f);
	}

	// Token: 0x04000A87 RID: 2695
	public SkinnedMeshRenderer MyMesh;

	// Token: 0x04000A88 RID: 2696
	public float Happiness;

	// Token: 0x04000A89 RID: 2697
	public float Blink;
}
