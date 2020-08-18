using System;
using UnityEngine;

// Token: 0x0200048F RID: 1167
[Serializable]
public struct HighlightTarget
{
	// Token: 0x04003BD0 RID: 15312
	public Color TargetColor;

	// Token: 0x04003BD1 RID: 15313
	[ColorUsage(true, true, 0f, 3f, 0f, 3f)]
	public Color ReplacementColor;

	// Token: 0x04003BD2 RID: 15314
	[Range(0f, 1f)]
	public float Threshold;
}
