using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020004C9 RID: 1225
	[Serializable]
	public class VignetteModel : PostProcessingModel
	{
		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06001EFB RID: 7931 RVA: 0x0017E4DC File Offset: 0x0017C6DC
		// (set) Token: 0x06001EFC RID: 7932 RVA: 0x0017E4E4 File Offset: 0x0017C6E4
		public VignetteModel.Settings settings
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

		// Token: 0x06001EFD RID: 7933 RVA: 0x0017E4ED File Offset: 0x0017C6ED
		public override void Reset()
		{
			this.m_Settings = VignetteModel.Settings.defaultSettings;
		}

		// Token: 0x04003C88 RID: 15496
		[SerializeField]
		private VignetteModel.Settings m_Settings = VignetteModel.Settings.defaultSettings;

		// Token: 0x020006F6 RID: 1782
		public enum Mode
		{
			// Token: 0x04004841 RID: 18497
			Classic,
			// Token: 0x04004842 RID: 18498
			Masked
		}

		// Token: 0x020006F7 RID: 1783
		[Serializable]
		public struct Settings
		{
			// Token: 0x17000659 RID: 1625
			// (get) Token: 0x06002C1C RID: 11292 RVA: 0x001C8BC0 File Offset: 0x001C6DC0
			public static VignetteModel.Settings defaultSettings
			{
				get
				{
					return new VignetteModel.Settings
					{
						mode = VignetteModel.Mode.Classic,
						color = new Color(0f, 0f, 0f, 1f),
						center = new Vector2(0.5f, 0.5f),
						intensity = 0.45f,
						smoothness = 0.2f,
						roundness = 1f,
						mask = null,
						opacity = 1f,
						rounded = false
					};
				}
			}

			// Token: 0x04004843 RID: 18499
			[Tooltip("Use the \"Classic\" mode for parametric controls. Use the \"Masked\" mode to use your own texture mask.")]
			public VignetteModel.Mode mode;

			// Token: 0x04004844 RID: 18500
			[ColorUsage(false)]
			[Tooltip("Vignette color. Use the alpha channel for transparency.")]
			public Color color;

			// Token: 0x04004845 RID: 18501
			[Tooltip("Sets the vignette center point (screen center is [0.5,0.5]).")]
			public Vector2 center;

			// Token: 0x04004846 RID: 18502
			[Range(0f, 1f)]
			[Tooltip("Amount of vignetting on screen.")]
			public float intensity;

			// Token: 0x04004847 RID: 18503
			[Range(0.01f, 1f)]
			[Tooltip("Smoothness of the vignette borders.")]
			public float smoothness;

			// Token: 0x04004848 RID: 18504
			[Range(0f, 1f)]
			[Tooltip("Lower values will make a square-ish vignette.")]
			public float roundness;

			// Token: 0x04004849 RID: 18505
			[Tooltip("A black and white mask to use as a vignette.")]
			public Texture mask;

			// Token: 0x0400484A RID: 18506
			[Range(0f, 1f)]
			[Tooltip("Mask opacity.")]
			public float opacity;

			// Token: 0x0400484B RID: 18507
			[Tooltip("Should the vignette be perfectly round or be dependent on the current aspect ratio?")]
			public bool rounded;
		}
	}
}
