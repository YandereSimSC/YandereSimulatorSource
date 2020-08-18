using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020004CE RID: 1230
	public abstract class PostProcessingComponentRenderTexture<T> : PostProcessingComponent<T> where T : PostProcessingModel
	{
		// Token: 0x06001F1F RID: 7967 RVA: 0x00002ACE File Offset: 0x00000CCE
		public virtual void Prepare(Material material)
		{
		}
	}
}
