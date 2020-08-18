using System;
using UnityEngine;

// Token: 0x020003D2 RID: 978
public class SentenceScript : MonoBehaviour
{
	// Token: 0x06001A5E RID: 6750 RVA: 0x00101F5D File Offset: 0x0010015D
	private void Update()
	{
		if (Input.GetButtonDown("A"))
		{
			this.Sentence.text = this.Words[this.ID];
			this.ID++;
		}
	}

	// Token: 0x040029C1 RID: 10689
	public UILabel Sentence;

	// Token: 0x040029C2 RID: 10690
	public string[] Words;

	// Token: 0x040029C3 RID: 10691
	public int ID;
}
