using System;
using UnityEngine;

// Token: 0x02000491 RID: 1169
public class NormalBufferView : MonoBehaviour
{
	// Token: 0x06001DFE RID: 7678 RVA: 0x00178101 File Offset: 0x00176301
	public void ApplyNormalView()
	{
		this.camera.SetReplacementShader(this.normalShader, "RenderType");
	}

	// Token: 0x06001DFF RID: 7679 RVA: 0x00178119 File Offset: 0x00176319
	public void DisableNormalView()
	{
		this.camera.ResetReplacementShader();
	}

	// Token: 0x04003BD9 RID: 15321
	[SerializeField]
	private Camera camera;

	// Token: 0x04003BDA RID: 15322
	[SerializeField]
	private Shader normalShader;
}
