using System;
using UnityEngine;

// Token: 0x0200024D RID: 589
public class CountdownScript : MonoBehaviour
{
	// Token: 0x060012B8 RID: 4792 RVA: 0x00095B41 File Offset: 0x00093D41
	private void Update()
	{
		this.Sprite.fillAmount = Mathf.MoveTowards(this.Sprite.fillAmount, 0f, Time.deltaTime * this.Speed);
	}

	// Token: 0x0400184E RID: 6222
	public UISprite Sprite;

	// Token: 0x0400184F RID: 6223
	public float Speed = 0.05f;

	// Token: 0x04001850 RID: 6224
	public bool MaskedPhoto;
}
