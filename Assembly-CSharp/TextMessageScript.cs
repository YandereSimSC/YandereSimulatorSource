using System;
using UnityEngine;

// Token: 0x0200035B RID: 859
public class TextMessageScript : MonoBehaviour
{
	// Token: 0x060018B8 RID: 6328 RVA: 0x000E423D File Offset: 0x000E243D
	private void Start()
	{
		if (!this.Attachment && this.Image != null)
		{
			this.Image.SetActive(false);
		}
	}

	// Token: 0x060018B9 RID: 6329 RVA: 0x000E4261 File Offset: 0x000E2461
	private void Update()
	{
		base.transform.localScale = Vector3.Lerp(base.transform.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * 10f);
	}

	// Token: 0x040024CB RID: 9419
	public UILabel Label;

	// Token: 0x040024CC RID: 9420
	public GameObject Image;

	// Token: 0x040024CD RID: 9421
	public bool Attachment;
}
