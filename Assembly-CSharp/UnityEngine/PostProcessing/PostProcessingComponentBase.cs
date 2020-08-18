using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020004CB RID: 1227
	public abstract class PostProcessingComponentBase
	{
		// Token: 0x06001F10 RID: 7952 RVA: 0x0002D171 File Offset: 0x0002B371
		public virtual DepthTextureMode GetCameraFlags()
		{
			return DepthTextureMode.None;
		}

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06001F11 RID: 7953
		public abstract bool active { get; }

		// Token: 0x06001F12 RID: 7954 RVA: 0x00002ACE File Offset: 0x00000CCE
		public virtual void OnEnable()
		{
		}

		// Token: 0x06001F13 RID: 7955 RVA: 0x00002ACE File Offset: 0x00000CCE
		public virtual void OnDisable()
		{
		}

		// Token: 0x06001F14 RID: 7956
		public abstract PostProcessingModel GetModel();

		// Token: 0x04003CA6 RID: 15526
		public PostProcessingContext context;
	}
}
