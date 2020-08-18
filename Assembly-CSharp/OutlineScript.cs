using System;
using HighlightingSystem;
using UnityEngine;

// Token: 0x0200034F RID: 847
public class OutlineScript : MonoBehaviour
{
	// Token: 0x06001896 RID: 6294 RVA: 0x000E0F66 File Offset: 0x000DF166
	public void Awake()
	{
		this.h = base.GetComponent<Highlighter>();
		if (this.h == null)
		{
			this.h = base.gameObject.AddComponent<Highlighter>();
		}
	}

	// Token: 0x06001897 RID: 6295 RVA: 0x000E0F93 File Offset: 0x000DF193
	private void Update()
	{
		this.h.ConstantOnImmediate(this.color);
	}

	// Token: 0x04002438 RID: 9272
	public YandereScript Yandere;

	// Token: 0x04002439 RID: 9273
	public Highlighter h;

	// Token: 0x0400243A RID: 9274
	public Color color = new Color(1f, 1f, 1f, 1f);
}
