using System;
using UnityEngine.Rendering;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020004CD RID: 1229
	public abstract class PostProcessingComponentCommandBuffer<T> : PostProcessingComponent<T> where T : PostProcessingModel
	{
		// Token: 0x06001F1B RID: 7963
		public abstract CameraEvent GetCameraEvent();

		// Token: 0x06001F1C RID: 7964
		public abstract string GetName();

		// Token: 0x06001F1D RID: 7965
		public abstract void PopulateCommandBuffer(CommandBuffer cb);
	}
}
