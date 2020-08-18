using System;
using UnityEngine;

// Token: 0x020002EE RID: 750
public class HomeMangaBookScript : MonoBehaviour
{
	// Token: 0x06001724 RID: 5924 RVA: 0x000C5719 File Offset: 0x000C3919
	private void Start()
	{
		base.transform.eulerAngles = new Vector3(90f, base.transform.eulerAngles.y, base.transform.eulerAngles.z);
	}

	// Token: 0x06001725 RID: 5925 RVA: 0x000C5750 File Offset: 0x000C3950
	private void Update()
	{
		float y = (this.Manga.Selected == this.ID) ? (base.transform.eulerAngles.y + Time.deltaTime * this.RotationSpeed) : 0f;
		base.transform.eulerAngles = new Vector3(base.transform.eulerAngles.x, y, base.transform.eulerAngles.z);
	}

	// Token: 0x04001F8B RID: 8075
	public HomeMangaScript Manga;

	// Token: 0x04001F8C RID: 8076
	public float RotationSpeed;

	// Token: 0x04001F8D RID: 8077
	public int ID;
}
