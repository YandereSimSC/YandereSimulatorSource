using System;
using UnityEngine;

// Token: 0x020002A6 RID: 678
public class FootprintScript : MonoBehaviour
{
	// Token: 0x06001412 RID: 5138 RVA: 0x000AFB90 File Offset: 0x000ADD90
	private void Start()
	{
		if (this.Yandere.Schoolwear == 0 || this.Yandere.Schoolwear == 2 || (this.Yandere.ClubAttire && ClubGlobals.Club == ClubType.MartialArts) || this.Yandere.Hungry || this.Yandere.LucyHelmet.activeInHierarchy)
		{
			base.GetComponent<Renderer>().material.mainTexture = this.Footprint;
		}
		if (GameGlobals.CensorBlood)
		{
			base.GetComponent<Renderer>().material.mainTexture = this.Flower;
			base.transform.localScale = new Vector3(0.2f, 0.2f, 1f);
		}
		UnityEngine.Object.Destroy(this);
	}

	// Token: 0x04001C47 RID: 7239
	public YandereScript Yandere;

	// Token: 0x04001C48 RID: 7240
	public Texture Footprint;

	// Token: 0x04001C49 RID: 7241
	public Texture Flower;
}
