using System;
using UnityEngine;

// Token: 0x0200032D RID: 813
public class MemeManagerScript : MonoBehaviour
{
	// Token: 0x06001819 RID: 6169 RVA: 0x000D6FF0 File Offset: 0x000D51F0
	private void Start()
	{
		if (GameGlobals.LoveSick)
		{
			GameObject[] memes = this.Memes;
			for (int i = 0; i < memes.Length; i++)
			{
				memes[i].SetActive(false);
			}
		}
	}

	// Token: 0x040022E6 RID: 8934
	[SerializeField]
	private GameObject[] Memes;
}
