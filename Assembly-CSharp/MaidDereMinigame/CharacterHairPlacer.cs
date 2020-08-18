using System;
using UnityEngine;

namespace MaidDereMinigame
{
	// Token: 0x020004E4 RID: 1252
	public class CharacterHairPlacer : MonoBehaviour
	{
		// Token: 0x06001F9B RID: 8091 RVA: 0x001830C8 File Offset: 0x001812C8
		private void Awake()
		{
			int num = UnityEngine.Random.Range(0, this.hairSprites.Length);
			this.hairInstance = new GameObject("Hair", new Type[]
			{
				typeof(SpriteRenderer)
			}).GetComponent<SpriteRenderer>();
			Transform transform = this.hairInstance.transform;
			transform.parent = base.transform;
			transform.localPosition = new Vector3(0f, 0f, -0.1f);
			this.hairInstance.sprite = this.hairSprites[num];
		}

		// Token: 0x06001F9C RID: 8092 RVA: 0x0018314F File Offset: 0x0018134F
		public void WalkPose(float height)
		{
			this.hairInstance.transform.localPosition = new Vector3(0f, height, this.hairInstance.transform.localPosition.z);
		}

		// Token: 0x06001F9D RID: 8093 RVA: 0x00183184 File Offset: 0x00181384
		public void HairPose(string point)
		{
			string[] array = point.Split(new char[]
			{
				','
			});
			float num;
			float.TryParse(array[0], out num);
			float y;
			float.TryParse(array[1], out y);
			this.hairInstance.transform.localPosition = new Vector3(this.hairInstance.flipX ? (-num) : num, y, this.hairInstance.transform.localPosition.z);
		}

		// Token: 0x04003D55 RID: 15701
		public Sprite[] hairSprites;

		// Token: 0x04003D56 RID: 15702
		[HideInInspector]
		public SpriteRenderer hairInstance;
	}
}
