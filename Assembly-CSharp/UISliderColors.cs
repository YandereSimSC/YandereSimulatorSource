using System;
using UnityEngine;

// Token: 0x0200003D RID: 61
[AddComponentMenu("NGUI/Examples/Slider Colors")]
public class UISliderColors : MonoBehaviour
{
	// Token: 0x06000142 RID: 322 RVA: 0x000134DF File Offset: 0x000116DF
	private void Start()
	{
		this.mBar = base.GetComponent<UIProgressBar>();
		this.mSprite = base.GetComponent<UIBasicSprite>();
		this.Update();
	}

	// Token: 0x06000143 RID: 323 RVA: 0x00013500 File Offset: 0x00011700
	private void Update()
	{
		if (this.sprite == null || this.colors.Length == 0)
		{
			return;
		}
		float num = (this.mBar != null) ? this.mBar.value : this.mSprite.fillAmount;
		num *= (float)(this.colors.Length - 1);
		int num2 = Mathf.FloorToInt(num);
		Color color = this.colors[0];
		if (num2 >= 0)
		{
			if (num2 + 1 < this.colors.Length)
			{
				float t = num - (float)num2;
				color = Color.Lerp(this.colors[num2], this.colors[num2 + 1], t);
			}
			else if (num2 < this.colors.Length)
			{
				color = this.colors[num2];
			}
			else
			{
				color = this.colors[this.colors.Length - 1];
			}
		}
		color.a = this.sprite.color.a;
		this.sprite.color = color;
	}

	// Token: 0x040002D0 RID: 720
	public UISprite sprite;

	// Token: 0x040002D1 RID: 721
	public Color[] colors = new Color[]
	{
		Color.red,
		Color.yellow,
		Color.green
	};

	// Token: 0x040002D2 RID: 722
	private UIProgressBar mBar;

	// Token: 0x040002D3 RID: 723
	private UIBasicSprite mSprite;
}
