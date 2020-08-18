using System;
using UnityEngine;

// Token: 0x02000298 RID: 664
public class ExpressionMaskScript : MonoBehaviour
{
	// Token: 0x060013EE RID: 5102 RVA: 0x000AD770 File Offset: 0x000AB970
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.LeftAlt))
		{
			if (this.ID < 3)
			{
				this.ID++;
			}
			else
			{
				this.ID = 0;
			}
			switch (this.ID)
			{
			case 0:
				this.Mask.material.mainTextureOffset = Vector2.zero;
				return;
			case 1:
				this.Mask.material.mainTextureOffset = new Vector2(0f, 0.5f);
				return;
			case 2:
				this.Mask.material.mainTextureOffset = new Vector2(0.5f, 0f);
				return;
			case 3:
				this.Mask.material.mainTextureOffset = new Vector2(0.5f, 0.5f);
				break;
			default:
				return;
			}
		}
	}

	// Token: 0x04001BC7 RID: 7111
	public Renderer Mask;

	// Token: 0x04001BC8 RID: 7112
	public int ID;
}
