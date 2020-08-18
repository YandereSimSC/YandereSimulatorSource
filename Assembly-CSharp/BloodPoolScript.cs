using System;
using UnityEngine;

// Token: 0x020000DE RID: 222
public class BloodPoolScript : MonoBehaviour
{
	// Token: 0x06000A50 RID: 2640 RVA: 0x00054CD8 File Offset: 0x00052ED8
	private void Start()
	{
		if (PlayerGlobals.PantiesEquipped == 7 && this.Blood)
		{
			this.TargetSize *= 0.5f;
		}
		if (GameGlobals.CensorBlood)
		{
			this.MyRenderer.material.color = new Color(1f, 1f, 1f, 1f);
			this.MyRenderer.material.mainTexture = this.Flower;
		}
		base.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
		Vector3 position = base.transform.position;
		if (position.x > 125f || position.x < -125f || position.z > 200f || position.z < -100f)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
		if (Application.loadedLevelName == "IntroScene" || Application.loadedLevelName == "NewIntroScene")
		{
			this.MyRenderer.material.SetColor("_TintColor", new Color(0.1f, 0.1f, 0.1f));
		}
	}

	// Token: 0x06000A51 RID: 2641 RVA: 0x00054E04 File Offset: 0x00053004
	private void Update()
	{
		if (this.Grow)
		{
			base.transform.localScale = Vector3.Lerp(base.transform.localScale, new Vector3(this.TargetSize, this.TargetSize, this.TargetSize), Time.deltaTime);
			if (base.transform.localScale.x > this.TargetSize * 0.99f)
			{
				this.Grow = false;
			}
		}
	}

	// Token: 0x06000A52 RID: 2642 RVA: 0x00054E75 File Offset: 0x00053075
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name == "BloodSpawner")
		{
			this.Grow = true;
		}
	}

	// Token: 0x04000A92 RID: 2706
	public float TargetSize;

	// Token: 0x04000A93 RID: 2707
	public bool Blood = true;

	// Token: 0x04000A94 RID: 2708
	public bool Grow;

	// Token: 0x04000A95 RID: 2709
	public Renderer MyRenderer;

	// Token: 0x04000A96 RID: 2710
	public Texture Flower;
}
