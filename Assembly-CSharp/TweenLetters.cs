using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200008E RID: 142
public class TweenLetters : UITweener
{
	// Token: 0x060005DF RID: 1503 RVA: 0x00034CC6 File Offset: 0x00032EC6
	private void OnEnable()
	{
		this.mVertexCount = -1;
		UILabel uilabel = this.mLabel;
		uilabel.onPostFill = (UIWidget.OnPostFillCallback)Delegate.Combine(uilabel.onPostFill, new UIWidget.OnPostFillCallback(this.OnPostFill));
	}

	// Token: 0x060005E0 RID: 1504 RVA: 0x00034CF6 File Offset: 0x00032EF6
	private void OnDisable()
	{
		UILabel uilabel = this.mLabel;
		uilabel.onPostFill = (UIWidget.OnPostFillCallback)Delegate.Remove(uilabel.onPostFill, new UIWidget.OnPostFillCallback(this.OnPostFill));
	}

	// Token: 0x060005E1 RID: 1505 RVA: 0x00034D1F File Offset: 0x00032F1F
	private void Awake()
	{
		this.mLabel = base.GetComponent<UILabel>();
		this.mCurrent = this.hoverOver;
	}

	// Token: 0x060005E2 RID: 1506 RVA: 0x00034D39 File Offset: 0x00032F39
	public override void Play(bool forward)
	{
		this.mCurrent = (forward ? this.hoverOver : this.hoverOut);
		base.Play(forward);
	}

	// Token: 0x060005E3 RID: 1507 RVA: 0x00034D5C File Offset: 0x00032F5C
	private void OnPostFill(UIWidget widget, int bufferOffset, List<Vector3> verts, List<Vector2> uvs, List<Color> cols)
	{
		if (verts == null)
		{
			return;
		}
		int count = verts.Count;
		if (verts == null || count == 0)
		{
			return;
		}
		if (this.mLabel == null)
		{
			return;
		}
		try
		{
			int quadsPerCharacter = this.mLabel.quadsPerCharacter;
			int num = count / quadsPerCharacter / 4;
			string printedText = this.mLabel.printedText;
			if (this.mVertexCount != count)
			{
				this.mVertexCount = count;
				this.SetLetterOrder(num);
				this.GetLetterDuration(num);
			}
			Matrix4x4 identity = Matrix4x4.identity;
			Vector3 pos = Vector3.zero;
			Quaternion q = Quaternion.identity;
			Vector3 s = Vector3.one;
			Vector3 b = Vector3.zero;
			Quaternion a = Quaternion.Euler(this.mCurrent.rot);
			Vector3 vector = Vector3.zero;
			Color value = Color.clear;
			float num2 = base.tweenFactor * this.duration;
			for (int i = 0; i < quadsPerCharacter; i++)
			{
				for (int j = 0; j < num; j++)
				{
					int num3 = this.mLetterOrder[j];
					int num4 = i * num * 4 + num3 * 4;
					if (num4 < count)
					{
						float start = this.mLetter[num3].start;
						float num5 = Mathf.Clamp(num2 - start, 0f, this.mLetter[num3].duration) / this.mLetter[num3].duration;
						num5 = this.animationCurve.Evaluate(num5);
						b = TweenLetters.GetCenter(verts, num4, 4);
						Vector2 offset = this.mLetter[num3].offset;
						pos = Vector3.LerpUnclamped(this.mCurrent.pos + new Vector3(offset.x, offset.y, 0f), Vector3.zero, num5);
						q = Quaternion.SlerpUnclamped(a, Quaternion.identity, num5);
						s = Vector3.LerpUnclamped(this.mCurrent.scale, Vector3.one, num5);
						float num6 = Mathf.LerpUnclamped(this.mCurrent.alpha, 1f, num5);
						identity.SetTRS(pos, q, s);
						for (int k = num4; k < num4 + 4; k++)
						{
							vector = verts[k];
							vector -= b;
							vector = identity.MultiplyPoint3x4(vector);
							vector += b;
							verts[k] = vector;
							value = cols[k];
							value.a *= num6;
							cols[k] = value;
						}
					}
				}
			}
		}
		catch (Exception)
		{
			base.enabled = false;
		}
	}

	// Token: 0x060005E4 RID: 1508 RVA: 0x00034FEC File Offset: 0x000331EC
	protected override void OnUpdate(float factor, bool isFinished)
	{
		this.mLabel.MarkAsChanged();
	}

	// Token: 0x060005E5 RID: 1509 RVA: 0x00034FFC File Offset: 0x000331FC
	private void SetLetterOrder(int letterCount)
	{
		if (letterCount == 0)
		{
			this.mLetter = null;
			this.mLetterOrder = null;
			return;
		}
		this.mLetterOrder = new int[letterCount];
		this.mLetter = new TweenLetters.LetterProperties[letterCount];
		for (int i = 0; i < letterCount; i++)
		{
			this.mLetterOrder[i] = ((this.mCurrent.animationOrder == TweenLetters.AnimationLetterOrder.Reverse) ? (letterCount - 1 - i) : i);
			int num = this.mLetterOrder[i];
			this.mLetter[num] = new TweenLetters.LetterProperties();
			this.mLetter[num].offset = new Vector2(UnityEngine.Random.Range(-this.mCurrent.offsetRange.x, this.mCurrent.offsetRange.x), UnityEngine.Random.Range(-this.mCurrent.offsetRange.y, this.mCurrent.offsetRange.y));
		}
		if (this.mCurrent.animationOrder == TweenLetters.AnimationLetterOrder.Random)
		{
			System.Random random = new System.Random();
			int j = letterCount;
			while (j > 1)
			{
				int num2 = random.Next(--j + 1);
				int num3 = this.mLetterOrder[num2];
				this.mLetterOrder[num2] = this.mLetterOrder[j];
				this.mLetterOrder[j] = num3;
			}
		}
	}

	// Token: 0x060005E6 RID: 1510 RVA: 0x00035128 File Offset: 0x00033328
	private void GetLetterDuration(int letterCount)
	{
		if (this.mCurrent.randomDurations)
		{
			for (int i = 0; i < this.mLetter.Length; i++)
			{
				this.mLetter[i].start = UnityEngine.Random.Range(0f, this.mCurrent.randomness.x * this.duration);
				float num = UnityEngine.Random.Range(this.mCurrent.randomness.y * this.duration, this.duration);
				this.mLetter[i].duration = num - this.mLetter[i].start;
			}
			return;
		}
		float num2 = this.duration / (float)letterCount;
		float num3 = 1f - this.mCurrent.overlap;
		float num4 = num2 * (float)letterCount * num3;
		float duration = this.ScaleRange(num2, num4 + num2 * this.mCurrent.overlap, this.duration);
		float num5 = 0f;
		for (int j = 0; j < this.mLetter.Length; j++)
		{
			int num6 = this.mLetterOrder[j];
			this.mLetter[num6].start = num5;
			this.mLetter[num6].duration = duration;
			num5 += this.mLetter[num6].duration * num3;
		}
	}

	// Token: 0x060005E7 RID: 1511 RVA: 0x00035267 File Offset: 0x00033467
	private float ScaleRange(float value, float baseMax, float limitMax)
	{
		return limitMax * value / baseMax;
	}

	// Token: 0x060005E8 RID: 1512 RVA: 0x00035270 File Offset: 0x00033470
	private static Vector3 GetCenter(List<Vector3> verts, int firstVert, int length)
	{
		Vector3 a = verts[firstVert];
		for (int i = firstVert + 1; i < firstVert + length; i++)
		{
			a += verts[i];
		}
		return a / (float)length;
	}

	// Token: 0x040005D9 RID: 1497
	public TweenLetters.AnimationProperties hoverOver;

	// Token: 0x040005DA RID: 1498
	public TweenLetters.AnimationProperties hoverOut;

	// Token: 0x040005DB RID: 1499
	private UILabel mLabel;

	// Token: 0x040005DC RID: 1500
	private int mVertexCount = -1;

	// Token: 0x040005DD RID: 1501
	private int[] mLetterOrder;

	// Token: 0x040005DE RID: 1502
	private TweenLetters.LetterProperties[] mLetter;

	// Token: 0x040005DF RID: 1503
	private TweenLetters.AnimationProperties mCurrent;

	// Token: 0x02000653 RID: 1619
	[DoNotObfuscateNGUI]
	public enum AnimationLetterOrder
	{
		// Token: 0x04004548 RID: 17736
		Forward,
		// Token: 0x04004549 RID: 17737
		Reverse,
		// Token: 0x0400454A RID: 17738
		Random
	}

	// Token: 0x02000654 RID: 1620
	private class LetterProperties
	{
		// Token: 0x0400454B RID: 17739
		public float start;

		// Token: 0x0400454C RID: 17740
		public float duration;

		// Token: 0x0400454D RID: 17741
		public Vector2 offset;
	}

	// Token: 0x02000655 RID: 1621
	[Serializable]
	public class AnimationProperties
	{
		// Token: 0x0400454E RID: 17742
		public TweenLetters.AnimationLetterOrder animationOrder = TweenLetters.AnimationLetterOrder.Random;

		// Token: 0x0400454F RID: 17743
		[Range(0f, 1f)]
		public float overlap = 0.5f;

		// Token: 0x04004550 RID: 17744
		public bool randomDurations;

		// Token: 0x04004551 RID: 17745
		[MinMaxRange(0f, 1f)]
		public Vector2 randomness = new Vector2(0.25f, 0.75f);

		// Token: 0x04004552 RID: 17746
		public Vector2 offsetRange = Vector2.zero;

		// Token: 0x04004553 RID: 17747
		public Vector3 pos = Vector3.zero;

		// Token: 0x04004554 RID: 17748
		public Vector3 rot = Vector3.zero;

		// Token: 0x04004555 RID: 17749
		public Vector3 scale = Vector3.one;

		// Token: 0x04004556 RID: 17750
		public float alpha = 1f;
	}
}
