using System;
using UnityEngine;

// Token: 0x020000B7 RID: 183
[AddComponentMenu("")]
[RequireComponent(typeof(Camera))]
public sealed class AmplifyMotionPostProcess : MonoBehaviour
{
	// Token: 0x17000201 RID: 513
	// (get) Token: 0x060009AD RID: 2477 RVA: 0x0004B729 File Offset: 0x00049929
	// (set) Token: 0x060009AE RID: 2478 RVA: 0x0004B731 File Offset: 0x00049931
	public AmplifyMotionEffectBase Instance
	{
		get
		{
			return this.m_instance;
		}
		set
		{
			this.m_instance = value;
		}
	}

	// Token: 0x060009AF RID: 2479 RVA: 0x0004B73A File Offset: 0x0004993A
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (this.m_instance != null)
		{
			this.m_instance.PostProcess(source, destination);
		}
	}

	// Token: 0x04000818 RID: 2072
	private AmplifyMotionEffectBase m_instance;
}
