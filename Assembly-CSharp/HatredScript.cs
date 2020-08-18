using System;
using UnityEngine;

// Token: 0x020000CC RID: 204
public class HatredScript : MonoBehaviour
{
	// Token: 0x06000A0B RID: 2571 RVA: 0x0004F8FE File Offset: 0x0004DAFE
	private void Start()
	{
		this.Character.SetActive(false);
	}

	// Token: 0x040009F0 RID: 2544
	public DepthOfFieldScatter DepthOfField;

	// Token: 0x040009F1 RID: 2545
	public HomeDarknessScript HomeDarkness;

	// Token: 0x040009F2 RID: 2546
	public HomeCameraScript HomeCamera;

	// Token: 0x040009F3 RID: 2547
	public GrayscaleEffect Grayscale;

	// Token: 0x040009F4 RID: 2548
	public Bloom Bloom;

	// Token: 0x040009F5 RID: 2549
	public GameObject CrackPanel;

	// Token: 0x040009F6 RID: 2550
	public AudioSource Voiceover;

	// Token: 0x040009F7 RID: 2551
	public GameObject SenpaiPhoto;

	// Token: 0x040009F8 RID: 2552
	public GameObject RivalPhotos;

	// Token: 0x040009F9 RID: 2553
	public GameObject Character;

	// Token: 0x040009FA RID: 2554
	public GameObject Panties;

	// Token: 0x040009FB RID: 2555
	public GameObject Yandere;

	// Token: 0x040009FC RID: 2556
	public GameObject Shrine;

	// Token: 0x040009FD RID: 2557
	public Transform AntennaeR;

	// Token: 0x040009FE RID: 2558
	public Transform AntennaeL;

	// Token: 0x040009FF RID: 2559
	public Transform Corkboard;

	// Token: 0x04000A00 RID: 2560
	public UISprite CrackDarkness;

	// Token: 0x04000A01 RID: 2561
	public UISprite Darkness;

	// Token: 0x04000A02 RID: 2562
	public UITexture Crack;

	// Token: 0x04000A03 RID: 2563
	public UITexture Logo;

	// Token: 0x04000A04 RID: 2564
	public bool Begin;

	// Token: 0x04000A05 RID: 2565
	public float Timer;

	// Token: 0x04000A06 RID: 2566
	public int Phase;

	// Token: 0x04000A07 RID: 2567
	public Texture[] CrackTexture;
}
