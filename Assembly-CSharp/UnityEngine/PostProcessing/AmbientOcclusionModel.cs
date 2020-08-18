using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020004BB RID: 1211
	[Serializable]
	public class AmbientOcclusionModel : PostProcessingModel
	{
		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x06001EBC RID: 7868 RVA: 0x0017E1B4 File Offset: 0x0017C3B4
		// (set) Token: 0x06001EBD RID: 7869 RVA: 0x0017E1BC File Offset: 0x0017C3BC
		public AmbientOcclusionModel.Settings settings
		{
			get
			{
				return this.m_Settings;
			}
			set
			{
				this.m_Settings = value;
			}
		}

		// Token: 0x06001EBE RID: 7870 RVA: 0x0017E1C5 File Offset: 0x0017C3C5
		public override void Reset()
		{
			this.m_Settings = AmbientOcclusionModel.Settings.defaultSettings;
		}

		// Token: 0x04003C78 RID: 15480
		[SerializeField]
		private AmbientOcclusionModel.Settings m_Settings = AmbientOcclusionModel.Settings.defaultSettings;

		// Token: 0x020006CC RID: 1740
		public enum SampleCount
		{
			// Token: 0x04004790 RID: 18320
			Lowest = 3,
			// Token: 0x04004791 RID: 18321
			Low = 6,
			// Token: 0x04004792 RID: 18322
			Medium = 10,
			// Token: 0x04004793 RID: 18323
			High = 16
		}

		// Token: 0x020006CD RID: 1741
		[Serializable]
		public struct Settings
		{
			// Token: 0x1700063D RID: 1597
			// (get) Token: 0x06002BFD RID: 11261 RVA: 0x001C7F58 File Offset: 0x001C6158
			public static AmbientOcclusionModel.Settings defaultSettings
			{
				get
				{
					return new AmbientOcclusionModel.Settings
					{
						intensity = 1f,
						radius = 0.3f,
						sampleCount = AmbientOcclusionModel.SampleCount.Medium,
						downsampling = true,
						forceForwardCompatibility = false,
						ambientOnly = false,
						highPrecision = false
					};
				}
			}

			// Token: 0x04004794 RID: 18324
			[Range(0f, 4f)]
			[Tooltip("Degree of darkness produced by the effect.")]
			public float intensity;

			// Token: 0x04004795 RID: 18325
			[Min(0.0001f)]
			[Tooltip("Radius of sample points, which affects extent of darkened areas.")]
			public float radius;

			// Token: 0x04004796 RID: 18326
			[Tooltip("Number of sample points, which affects quality and performance.")]
			public AmbientOcclusionModel.SampleCount sampleCount;

			// Token: 0x04004797 RID: 18327
			[Tooltip("Halves the resolution of the effect to increase performance at the cost of visual quality.")]
			public bool downsampling;

			// Token: 0x04004798 RID: 18328
			[Tooltip("Forces compatibility with Forward rendered objects when working with the Deferred rendering path.")]
			public bool forceForwardCompatibility;

			// Token: 0x04004799 RID: 18329
			[Tooltip("Enables the ambient-only mode in that the effect only affects ambient lighting. This mode is only available with the Deferred rendering path and HDR rendering.")]
			public bool ambientOnly;

			// Token: 0x0400479A RID: 18330
			[Tooltip("Toggles the use of a higher precision depth texture with the forward rendering path (may impact performances). Has no effect with the deferred rendering path.")]
			public bool highPrecision;
		}
	}
}
