using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020004BE RID: 1214
	[Serializable]
	public class BuiltinDebugViewsModel : PostProcessingModel
	{
		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x06001EC8 RID: 7880 RVA: 0x0017E247 File Offset: 0x0017C447
		// (set) Token: 0x06001EC9 RID: 7881 RVA: 0x0017E24F File Offset: 0x0017C44F
		public BuiltinDebugViewsModel.Settings settings
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

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x06001ECA RID: 7882 RVA: 0x0017E258 File Offset: 0x0017C458
		public bool willInterrupt
		{
			get
			{
				return !this.IsModeActive(BuiltinDebugViewsModel.Mode.None) && !this.IsModeActive(BuiltinDebugViewsModel.Mode.EyeAdaptation) && !this.IsModeActive(BuiltinDebugViewsModel.Mode.PreGradingLog) && !this.IsModeActive(BuiltinDebugViewsModel.Mode.LogLut) && !this.IsModeActive(BuiltinDebugViewsModel.Mode.UserLut);
			}
		}

		// Token: 0x06001ECB RID: 7883 RVA: 0x0017E28B File Offset: 0x0017C48B
		public override void Reset()
		{
			this.settings = BuiltinDebugViewsModel.Settings.defaultSettings;
		}

		// Token: 0x06001ECC RID: 7884 RVA: 0x0017E298 File Offset: 0x0017C498
		public bool IsModeActive(BuiltinDebugViewsModel.Mode mode)
		{
			return this.m_Settings.mode == mode;
		}

		// Token: 0x04003C7B RID: 15483
		[SerializeField]
		private BuiltinDebugViewsModel.Settings m_Settings = BuiltinDebugViewsModel.Settings.defaultSettings;

		// Token: 0x020006D8 RID: 1752
		[Serializable]
		public struct DepthSettings
		{
			// Token: 0x17000645 RID: 1605
			// (get) Token: 0x06002C08 RID: 11272 RVA: 0x001C838C File Offset: 0x001C658C
			public static BuiltinDebugViewsModel.DepthSettings defaultSettings
			{
				get
				{
					return new BuiltinDebugViewsModel.DepthSettings
					{
						scale = 1f
					};
				}
			}

			// Token: 0x040047BE RID: 18366
			[Range(0f, 1f)]
			[Tooltip("Scales the camera far plane before displaying the depth map.")]
			public float scale;
		}

		// Token: 0x020006D9 RID: 1753
		[Serializable]
		public struct MotionVectorsSettings
		{
			// Token: 0x17000646 RID: 1606
			// (get) Token: 0x06002C09 RID: 11273 RVA: 0x001C83B0 File Offset: 0x001C65B0
			public static BuiltinDebugViewsModel.MotionVectorsSettings defaultSettings
			{
				get
				{
					return new BuiltinDebugViewsModel.MotionVectorsSettings
					{
						sourceOpacity = 1f,
						motionImageOpacity = 0f,
						motionImageAmplitude = 16f,
						motionVectorsOpacity = 1f,
						motionVectorsResolution = 24,
						motionVectorsAmplitude = 64f
					};
				}
			}

			// Token: 0x040047BF RID: 18367
			[Range(0f, 1f)]
			[Tooltip("Opacity of the source render.")]
			public float sourceOpacity;

			// Token: 0x040047C0 RID: 18368
			[Range(0f, 1f)]
			[Tooltip("Opacity of the per-pixel motion vector colors.")]
			public float motionImageOpacity;

			// Token: 0x040047C1 RID: 18369
			[Min(0f)]
			[Tooltip("Because motion vectors are mainly very small vectors, you can use this setting to make them more visible.")]
			public float motionImageAmplitude;

			// Token: 0x040047C2 RID: 18370
			[Range(0f, 1f)]
			[Tooltip("Opacity for the motion vector arrows.")]
			public float motionVectorsOpacity;

			// Token: 0x040047C3 RID: 18371
			[Range(8f, 64f)]
			[Tooltip("The arrow density on screen.")]
			public int motionVectorsResolution;

			// Token: 0x040047C4 RID: 18372
			[Min(0f)]
			[Tooltip("Tweaks the arrows length.")]
			public float motionVectorsAmplitude;
		}

		// Token: 0x020006DA RID: 1754
		public enum Mode
		{
			// Token: 0x040047C6 RID: 18374
			None,
			// Token: 0x040047C7 RID: 18375
			Depth,
			// Token: 0x040047C8 RID: 18376
			Normals,
			// Token: 0x040047C9 RID: 18377
			MotionVectors,
			// Token: 0x040047CA RID: 18378
			AmbientOcclusion,
			// Token: 0x040047CB RID: 18379
			EyeAdaptation,
			// Token: 0x040047CC RID: 18380
			FocusPlane,
			// Token: 0x040047CD RID: 18381
			PreGradingLog,
			// Token: 0x040047CE RID: 18382
			LogLut,
			// Token: 0x040047CF RID: 18383
			UserLut
		}

		// Token: 0x020006DB RID: 1755
		[Serializable]
		public struct Settings
		{
			// Token: 0x17000647 RID: 1607
			// (get) Token: 0x06002C0A RID: 11274 RVA: 0x001C840C File Offset: 0x001C660C
			public static BuiltinDebugViewsModel.Settings defaultSettings
			{
				get
				{
					return new BuiltinDebugViewsModel.Settings
					{
						mode = BuiltinDebugViewsModel.Mode.None,
						depth = BuiltinDebugViewsModel.DepthSettings.defaultSettings,
						motionVectors = BuiltinDebugViewsModel.MotionVectorsSettings.defaultSettings
					};
				}
			}

			// Token: 0x040047D0 RID: 18384
			public BuiltinDebugViewsModel.Mode mode;

			// Token: 0x040047D1 RID: 18385
			public BuiltinDebugViewsModel.DepthSettings depth;

			// Token: 0x040047D2 RID: 18386
			public BuiltinDebugViewsModel.MotionVectorsSettings motionVectors;
		}
	}
}
