using System;
using UnityEngine;

// Token: 0x02000297 RID: 663
public class ExclamationScript : MonoBehaviour
{
	// Token: 0x060013EB RID: 5099 RVA: 0x000AD594 File Offset: 0x000AB794
	private void Start()
	{
		base.transform.localScale = Vector3.zero;
		this.Graphic.material.SetColor("_TintColor", new Color(0.5f, 0.5f, 0.5f, 0f));
		this.MainCamera = Camera.main;
	}

	// Token: 0x060013EC RID: 5100 RVA: 0x000AD5EC File Offset: 0x000AB7EC
	private void Update()
	{
		this.Timer -= Time.deltaTime;
		if (this.Timer > 0f)
		{
			base.transform.LookAt(this.MainCamera.transform);
			if (this.Timer > 1.5f)
			{
				base.transform.localScale = Vector3.Lerp(base.transform.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * 10f);
				this.Alpha = Mathf.Lerp(this.Alpha, 0.5f, Time.deltaTime * 10f);
				this.Graphic.material.SetColor("_TintColor", new Color(0.5f, 0.5f, 0.5f, this.Alpha));
				return;
			}
			if (base.transform.localScale.x > 0.1f)
			{
				base.transform.localScale = Vector3.Lerp(base.transform.localScale, Vector3.zero, Time.deltaTime * 10f);
			}
			else
			{
				base.transform.localScale = Vector3.zero;
			}
			this.Alpha = Mathf.Lerp(this.Alpha, 0f, Time.deltaTime * 10f);
			this.Graphic.material.SetColor("_TintColor", new Color(0.5f, 0.5f, 0.5f, this.Alpha));
		}
	}

	// Token: 0x04001BC3 RID: 7107
	public Renderer Graphic;

	// Token: 0x04001BC4 RID: 7108
	public float Alpha;

	// Token: 0x04001BC5 RID: 7109
	public float Timer;

	// Token: 0x04001BC6 RID: 7110
	public Camera MainCamera;
}
