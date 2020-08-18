using System;
using UnityEngine;

// Token: 0x020002E9 RID: 745
public class HomeCursorScript : MonoBehaviour
{
	// Token: 0x0600170F RID: 5903 RVA: 0x000C2CD4 File Offset: 0x000C0ED4
	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject == this.Photograph)
		{
			this.PhotographNull();
		}
		if (other.gameObject == this.Tack)
		{
			this.CircleHighlight.position = new Vector3(this.CircleHighlight.position.x, 100f, this.Highlight.position.z);
			this.Tack = null;
			this.PhotoGallery.UpdateButtonPrompts();
		}
	}

	// Token: 0x06001710 RID: 5904 RVA: 0x000C2D54 File Offset: 0x000C0F54
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == 16)
		{
			if (this.Tack == null)
			{
				this.Photograph = other.gameObject;
				this.Highlight.localEulerAngles = this.Photograph.transform.localEulerAngles;
				this.Highlight.localPosition = this.Photograph.transform.localPosition;
				this.Highlight.localScale = new Vector3(this.Photograph.transform.localScale.x * 1.12f, this.Photograph.transform.localScale.y * 1.2f, 1f);
				this.PhotoGallery.UpdateButtonPrompts();
				return;
			}
		}
		else if (other.gameObject.name != "SouthWall")
		{
			this.Tack = other.gameObject;
			this.CircleHighlight.position = this.Tack.transform.position;
			this.PhotoGallery.UpdateButtonPrompts();
			this.PhotographNull();
		}
	}

	// Token: 0x06001711 RID: 5905 RVA: 0x000C2E6C File Offset: 0x000C106C
	private void PhotographNull()
	{
		this.Highlight.position = new Vector3(this.Highlight.position.x, 100f, this.Highlight.position.z);
		this.Photograph = null;
		this.PhotoGallery.UpdateButtonPrompts();
	}

	// Token: 0x04001F34 RID: 7988
	public PhotoGalleryScript PhotoGallery;

	// Token: 0x04001F35 RID: 7989
	public GameObject Photograph;

	// Token: 0x04001F36 RID: 7990
	public Transform Highlight;

	// Token: 0x04001F37 RID: 7991
	public GameObject Tack;

	// Token: 0x04001F38 RID: 7992
	public Transform CircleHighlight;
}
