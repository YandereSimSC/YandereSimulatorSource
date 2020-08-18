using System;
using UnityEngine;

// Token: 0x020002DA RID: 730
public class GrowShrinkScript : MonoBehaviour
{
	// Token: 0x060016D7 RID: 5847 RVA: 0x000BCDF8 File Offset: 0x000BAFF8
	private void Start()
	{
		this.OriginalPosition = base.transform.localPosition;
		base.transform.localScale = Vector3.zero;
	}

	// Token: 0x060016D8 RID: 5848 RVA: 0x000BCE1C File Offset: 0x000BB01C
	private void Update()
	{
		this.Timer += Time.deltaTime;
		this.Scale += Time.deltaTime * (this.Strength * this.Speed);
		if (!this.Shrink)
		{
			this.Strength += Time.deltaTime * this.Speed;
			if (this.Strength > this.Threshold)
			{
				this.Strength = this.Threshold;
			}
			if (this.Scale > this.Target)
			{
				this.Threshold *= this.Slowdown;
				this.Shrink = true;
			}
		}
		else
		{
			this.Strength -= Time.deltaTime * this.Speed;
			float num = this.Threshold * -1f;
			if (this.Strength < num)
			{
				this.Strength = num;
			}
			if (this.Scale < this.Target)
			{
				this.Threshold *= this.Slowdown;
				this.Shrink = false;
			}
		}
		if (this.Timer > 3.33333f)
		{
			this.FallSpeed += Time.deltaTime * 10f;
			base.transform.localPosition = new Vector3(base.transform.localPosition.x, base.transform.localPosition.y - this.FallSpeed * this.FallSpeed, base.transform.localPosition.z);
		}
		base.transform.localScale = new Vector3(this.Scale, this.Scale, this.Scale);
	}

	// Token: 0x060016D9 RID: 5849 RVA: 0x000BCFB4 File Offset: 0x000BB1B4
	public void Return()
	{
		base.transform.localPosition = this.OriginalPosition;
		base.transform.localScale = Vector3.zero;
		this.FallSpeed = 0f;
		this.Threshold = 1f;
		this.Slowdown = 0.5f;
		this.Strength = 1f;
		this.Target = 1f;
		this.Scale = 0f;
		this.Speed = 5f;
		this.Timer = 0f;
		base.gameObject.SetActive(false);
	}

	// Token: 0x04001E20 RID: 7712
	public float FallSpeed;

	// Token: 0x04001E21 RID: 7713
	public float Threshold = 1f;

	// Token: 0x04001E22 RID: 7714
	public float Slowdown = 0.5f;

	// Token: 0x04001E23 RID: 7715
	public float Strength = 1f;

	// Token: 0x04001E24 RID: 7716
	public float Target = 1f;

	// Token: 0x04001E25 RID: 7717
	public float Scale;

	// Token: 0x04001E26 RID: 7718
	public float Speed = 5f;

	// Token: 0x04001E27 RID: 7719
	public float Timer;

	// Token: 0x04001E28 RID: 7720
	public bool Shrink;

	// Token: 0x04001E29 RID: 7721
	public Vector3 OriginalPosition;
}
