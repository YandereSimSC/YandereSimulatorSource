﻿using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

// Token: 0x02000042 RID: 66
[RequireComponent(typeof(UILabel))]
[AddComponentMenu("NGUI/Interaction/Typewriter Effect")]
public class TypewriterEffect : MonoBehaviour
{
	// Token: 0x1700001B RID: 27
	// (get) Token: 0x06000156 RID: 342 RVA: 0x0001399C File Offset: 0x00011B9C
	public bool isActive
	{
		get
		{
			return this.mActive;
		}
	}

	// Token: 0x06000157 RID: 343 RVA: 0x000139A4 File Offset: 0x00011BA4
	public void ResetToBeginning()
	{
		this.Finish();
		this.mReset = true;
		this.mActive = true;
		this.mNextChar = 0f;
		this.mCurrentOffset = 0;
		this.Update();
	}

	// Token: 0x06000158 RID: 344 RVA: 0x000139D4 File Offset: 0x00011BD4
	public void Finish()
	{
		if (this.mActive)
		{
			this.mActive = false;
			if (!this.mReset)
			{
				this.mCurrentOffset = this.mFullText.Length;
				this.mFade.Clear();
				this.mLabel.text = this.mFullText;
			}
			if (this.keepFullDimensions && this.scrollView != null)
			{
				this.scrollView.UpdatePosition();
			}
			TypewriterEffect.current = this;
			EventDelegate.Execute(this.onFinished);
			TypewriterEffect.current = null;
		}
	}

	// Token: 0x06000159 RID: 345 RVA: 0x00013A5D File Offset: 0x00011C5D
	private void OnEnable()
	{
		this.mReset = true;
		this.mActive = true;
	}

	// Token: 0x0600015A RID: 346 RVA: 0x00013A6D File Offset: 0x00011C6D
	private void OnDisable()
	{
		this.Finish();
	}

	// Token: 0x0600015B RID: 347 RVA: 0x00013A78 File Offset: 0x00011C78
	private void Update()
	{
		if (!this.mActive)
		{
			return;
		}
		if (this.mReset)
		{
			this.mCurrentOffset = 0;
			this.mReset = false;
			this.mLabel = base.GetComponent<UILabel>();
			this.mFullText = this.mLabel.processedText;
			this.mFade.Clear();
			if (this.keepFullDimensions && this.scrollView != null)
			{
				this.scrollView.UpdatePosition();
			}
		}
		if (string.IsNullOrEmpty(this.mFullText))
		{
			return;
		}
		int length = this.mFullText.Length;
		while (this.mCurrentOffset < length && this.mNextChar <= RealTime.time)
		{
			int num = this.mCurrentOffset;
			this.charsPerSecond = Mathf.Max(1, this.charsPerSecond);
			if (this.mLabel.supportEncoding)
			{
				while (NGUIText.ParseSymbol(this.mFullText, ref this.mCurrentOffset))
				{
				}
			}
			this.mCurrentOffset++;
			if (this.mCurrentOffset > length)
			{
				break;
			}
			float num2 = 1f / (float)this.charsPerSecond;
			char c = (num < length) ? this.mFullText[num] : '\n';
			if (c == '\n')
			{
				num2 += this.delayOnNewLine;
			}
			else if (num + 1 == length || this.mFullText[num + 1] <= ' ')
			{
				if (c == '.')
				{
					if (num + 2 < length && this.mFullText[num + 1] == '.' && this.mFullText[num + 2] == '.')
					{
						num2 += this.delayOnPeriod * 3f;
						num += 2;
					}
					else
					{
						num2 += this.delayOnPeriod;
					}
				}
				else if (c == '!' || c == '?')
				{
					num2 += this.delayOnPeriod;
				}
			}
			if (this.mNextChar == 0f)
			{
				this.mNextChar = RealTime.time + num2;
			}
			else
			{
				this.mNextChar += num2;
			}
			if (this.fadeInTime != 0f)
			{
				TypewriterEffect.FadeEntry item = default(TypewriterEffect.FadeEntry);
				item.index = num;
				item.alpha = 0f;
				item.text = this.mFullText.Substring(num, this.mCurrentOffset - num);
				this.mFade.Add(item);
			}
			else
			{
				this.mLabel.text = (this.keepFullDimensions ? (this.mFullText.Substring(0, this.mCurrentOffset) + "[00]" + this.mFullText.Substring(this.mCurrentOffset)) : this.mFullText.Substring(0, this.mCurrentOffset));
				if (!this.keepFullDimensions && this.scrollView != null)
				{
					this.scrollView.UpdatePosition();
				}
			}
		}
		if (this.mCurrentOffset >= length && this.mFade.size == 0)
		{
			this.mLabel.text = this.mFullText;
			TypewriterEffect.current = this;
			EventDelegate.Execute(this.onFinished);
			TypewriterEffect.current = null;
			this.mActive = false;
			return;
		}
		if (this.mFade.size != 0)
		{
			int i = 0;
			while (i < this.mFade.size)
			{
				TypewriterEffect.FadeEntry fadeEntry = this.mFade.buffer[i];
				fadeEntry.alpha += RealTime.deltaTime / this.fadeInTime;
				if (fadeEntry.alpha < 1f)
				{
					this.mFade.buffer[i] = fadeEntry;
					i++;
				}
				else
				{
					this.mFade.RemoveAt(i);
				}
			}
			if (this.mFade.size == 0)
			{
				if (this.keepFullDimensions)
				{
					this.mLabel.text = this.mFullText.Substring(0, this.mCurrentOffset) + "[00]" + this.mFullText.Substring(this.mCurrentOffset);
					return;
				}
				this.mLabel.text = this.mFullText.Substring(0, this.mCurrentOffset);
				return;
			}
			else
			{
				StringBuilder stringBuilder = new StringBuilder();
				for (int j = 0; j < this.mFade.size; j++)
				{
					TypewriterEffect.FadeEntry fadeEntry2 = this.mFade.buffer[j];
					if (j == 0)
					{
						stringBuilder.Append(this.mFullText.Substring(0, fadeEntry2.index));
					}
					stringBuilder.Append('[');
					stringBuilder.Append(NGUIText.EncodeAlpha(fadeEntry2.alpha));
					stringBuilder.Append(']');
					stringBuilder.Append(fadeEntry2.text);
				}
				if (this.keepFullDimensions)
				{
					stringBuilder.Append("[00]");
					stringBuilder.Append(this.mFullText.Substring(this.mCurrentOffset));
				}
				this.mLabel.text = stringBuilder.ToString();
			}
		}
	}

	// Token: 0x040002E6 RID: 742
	public static TypewriterEffect current;

	// Token: 0x040002E7 RID: 743
	public int charsPerSecond = 20;

	// Token: 0x040002E8 RID: 744
	public float fadeInTime;

	// Token: 0x040002E9 RID: 745
	public float delayOnPeriod;

	// Token: 0x040002EA RID: 746
	public float delayOnNewLine;

	// Token: 0x040002EB RID: 747
	public UIScrollView scrollView;

	// Token: 0x040002EC RID: 748
	public bool keepFullDimensions;

	// Token: 0x040002ED RID: 749
	public List<EventDelegate> onFinished = new List<EventDelegate>();

	// Token: 0x040002EE RID: 750
	public UILabel mLabel;

	// Token: 0x040002EF RID: 751
	public string mFullText = "";

	// Token: 0x040002F0 RID: 752
	public int mCurrentOffset;

	// Token: 0x040002F1 RID: 753
	private float mNextChar;

	// Token: 0x040002F2 RID: 754
	private bool mReset = true;

	// Token: 0x040002F3 RID: 755
	public bool mActive;

	// Token: 0x040002F4 RID: 756
	public bool delayOnComma;

	// Token: 0x040002F5 RID: 757
	private BetterList<TypewriterEffect.FadeEntry> mFade = new BetterList<TypewriterEffect.FadeEntry>();

	// Token: 0x0200060F RID: 1551
	private struct FadeEntry
	{
		// Token: 0x0400447B RID: 17531
		public int index;

		// Token: 0x0400447C RID: 17532
		public string text;

		// Token: 0x0400447D RID: 17533
		public float alpha;
	}
}
