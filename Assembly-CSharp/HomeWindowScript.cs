using System;
using UnityEngine;

// Token: 0x020002F9 RID: 761
public class HomeWindowScript : MonoBehaviour
{
	// Token: 0x06001750 RID: 5968 RVA: 0x000C8B94 File Offset: 0x000C6D94
	private void Start()
	{
		this.Sprite.color = new Color(this.Sprite.color.r, this.Sprite.color.g, this.Sprite.color.b, 0f);
	}

	// Token: 0x06001751 RID: 5969 RVA: 0x000C8BE8 File Offset: 0x000C6DE8
	private void Update()
	{
		this.Sprite.color = new Color(this.Sprite.color.r, this.Sprite.color.g, this.Sprite.color.b, Mathf.Lerp(this.Sprite.color.a, this.Show ? 1f : 0f, Time.deltaTime * 10f));
	}

	// Token: 0x04002047 RID: 8263
	public UISprite Sprite;

	// Token: 0x04002048 RID: 8264
	public bool Show;
}
