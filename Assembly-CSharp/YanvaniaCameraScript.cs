using System;
using UnityEngine;

// Token: 0x02000477 RID: 1143
public class YanvaniaCameraScript : MonoBehaviour
{
	// Token: 0x06001DA3 RID: 7587 RVA: 0x001706AE File Offset: 0x0016E8AE
	private void Start()
	{
		base.transform.position = this.Yanmont.transform.position + new Vector3(0f, 1.5f, -5.85f);
	}

	// Token: 0x06001DA4 RID: 7588 RVA: 0x001706E4 File Offset: 0x0016E8E4
	private void FixedUpdate()
	{
		this.TargetZoom += Input.GetAxis("Mouse ScrollWheel");
		if (this.TargetZoom < 0f)
		{
			this.TargetZoom = 0f;
		}
		if (this.TargetZoom > 3.85f)
		{
			this.TargetZoom = 3.85f;
		}
		this.Zoom = Mathf.Lerp(this.Zoom, this.TargetZoom, Time.deltaTime);
		if (!this.Cutscene)
		{
			base.transform.position = this.Yanmont.transform.position + new Vector3(0f, 1.5f, -5.85f + this.Zoom);
			if (base.transform.position.x > 47.9f)
			{
				base.transform.position = new Vector3(47.9f, base.transform.position.y, base.transform.position.z);
				return;
			}
		}
		else
		{
			if (this.StopMusic)
			{
				AudioSource component = this.Jukebox.GetComponent<AudioSource>();
				component.volume -= Time.deltaTime * ((this.Yanmont.Health > 0f) ? 0.2f : 0.025f);
				if (component.volume <= 0f)
				{
					this.StopMusic = false;
				}
			}
			base.transform.position = new Vector3(Mathf.MoveTowards(base.transform.position.x, -34.675f, Time.deltaTime * this.Yanmont.walkSpeed), 8f, -5.85f + this.Zoom);
		}
	}

	// Token: 0x04003A87 RID: 14983
	public YanvaniaYanmontScript Yanmont;

	// Token: 0x04003A88 RID: 14984
	public GameObject Jukebox;

	// Token: 0x04003A89 RID: 14985
	public bool Cutscene;

	// Token: 0x04003A8A RID: 14986
	public bool StopMusic = true;

	// Token: 0x04003A8B RID: 14987
	public float TargetZoom;

	// Token: 0x04003A8C RID: 14988
	public float Zoom;
}
