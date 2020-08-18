using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020004CC RID: 1228
	public abstract class PostProcessingComponent<T> : PostProcessingComponentBase where T : PostProcessingModel
	{
		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x06001F16 RID: 7958 RVA: 0x0017F12A File Offset: 0x0017D32A
		// (set) Token: 0x06001F17 RID: 7959 RVA: 0x0017F132 File Offset: 0x0017D332
		public T model { get; internal set; }

		// Token: 0x06001F18 RID: 7960 RVA: 0x0017F13B File Offset: 0x0017D33B
		public virtual void Init(PostProcessingContext pcontext, T pmodel)
		{
			this.context = pcontext;
			this.model = pmodel;
		}

		// Token: 0x06001F19 RID: 7961 RVA: 0x0017F14B File Offset: 0x0017D34B
		public override PostProcessingModel GetModel()
		{
			return this.model;
		}
	}
}
