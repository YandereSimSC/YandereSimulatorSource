using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020004D2 RID: 1234
	[Serializable]
	public sealed class ColorGradingCurve
	{
		// Token: 0x06001F31 RID: 7985 RVA: 0x0017F2C8 File Offset: 0x0017D4C8
		public ColorGradingCurve(AnimationCurve curve, float zeroValue, bool loop, Vector2 bounds)
		{
			this.curve = curve;
			this.m_ZeroValue = zeroValue;
			this.m_Loop = loop;
			this.m_Range = bounds.magnitude;
		}

		// Token: 0x06001F32 RID: 7986 RVA: 0x0017F2F4 File Offset: 0x0017D4F4
		public void Cache()
		{
			if (!this.m_Loop)
			{
				return;
			}
			int length = this.curve.length;
			if (length < 2)
			{
				return;
			}
			if (this.m_InternalLoopingCurve == null)
			{
				this.m_InternalLoopingCurve = new AnimationCurve();
			}
			Keyframe key = this.curve[length - 1];
			key.time -= this.m_Range;
			Keyframe key2 = this.curve[0];
			key2.time += this.m_Range;
			this.m_InternalLoopingCurve.keys = this.curve.keys;
			this.m_InternalLoopingCurve.AddKey(key);
			this.m_InternalLoopingCurve.AddKey(key2);
		}

		// Token: 0x06001F33 RID: 7987 RVA: 0x0017F3A4 File Offset: 0x0017D5A4
		public float Evaluate(float t)
		{
			if (this.curve.length == 0)
			{
				return this.m_ZeroValue;
			}
			if (!this.m_Loop || this.curve.length == 1)
			{
				return this.curve.Evaluate(t);
			}
			return this.m_InternalLoopingCurve.Evaluate(t);
		}

		// Token: 0x04003CBD RID: 15549
		public AnimationCurve curve;

		// Token: 0x04003CBE RID: 15550
		[SerializeField]
		private bool m_Loop;

		// Token: 0x04003CBF RID: 15551
		[SerializeField]
		private float m_ZeroValue;

		// Token: 0x04003CC0 RID: 15552
		[SerializeField]
		private float m_Range;

		// Token: 0x04003CC1 RID: 15553
		private AnimationCurve m_InternalLoopingCurve;
	}
}
