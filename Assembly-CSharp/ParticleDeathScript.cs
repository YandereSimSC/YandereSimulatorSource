using System;
using UnityEngine;

// Token: 0x02000353 RID: 851
public class ParticleDeathScript : MonoBehaviour
{
	// Token: 0x0600189F RID: 6303 RVA: 0x000E1296 File Offset: 0x000DF496
	private void LateUpdate()
	{
		if (this.Particles.isPlaying && this.Particles.particleCount == 0)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04002448 RID: 9288
	public ParticleSystem Particles;
}
