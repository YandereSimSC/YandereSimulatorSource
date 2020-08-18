using System;
using UnityEngine;

// Token: 0x020000BA RID: 186
public class RPG_Animation_CharacterFadeOnly : MonoBehaviour
{
	// Token: 0x060009C4 RID: 2500 RVA: 0x0004BB06 File Offset: 0x00049D06
	private void Awake()
	{
		RPG_Animation_CharacterFadeOnly.instance = this;
	}

	// Token: 0x0400081F RID: 2079
	public static RPG_Animation_CharacterFadeOnly instance;
}
