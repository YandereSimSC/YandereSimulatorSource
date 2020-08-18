using System;
using UnityEngine;

namespace MaidDereMinigame
{
	// Token: 0x020004EE RID: 1262
	[CreateAssetMenu(fileName = "New Food Item", menuName = "Food")]
	public class Food : ScriptableObject
	{
		// Token: 0x04003D99 RID: 15769
		public Sprite largeSprite;

		// Token: 0x04003D9A RID: 15770
		public Sprite smallSprite;

		// Token: 0x04003D9B RID: 15771
		public float cookTimeMultiplier = 1f;
	}
}
