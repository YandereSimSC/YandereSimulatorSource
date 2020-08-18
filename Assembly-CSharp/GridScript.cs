using System;
using UnityEngine;

// Token: 0x020002D9 RID: 729
public class GridScript : MonoBehaviour
{
	// Token: 0x060016D5 RID: 5845 RVA: 0x000BCD04 File Offset: 0x000BAF04
	private void Start()
	{
		while (this.ID < this.Rows * this.Columns)
		{
			UnityEngine.Object.Instantiate<GameObject>(this.Tile, new Vector3((float)this.Row, 0f, (float)this.Column), Quaternion.identity).transform.parent = base.transform;
			this.Row++;
			if (this.Row > this.Rows)
			{
				this.Row = 1;
				this.Column++;
			}
			this.ID++;
		}
		base.transform.localScale = new Vector3(4f, 4f, 4f);
		base.transform.position = new Vector3(-52f, 0f, -52f);
	}

	// Token: 0x04001E1A RID: 7706
	public GameObject Tile;

	// Token: 0x04001E1B RID: 7707
	public int Row;

	// Token: 0x04001E1C RID: 7708
	public int Column;

	// Token: 0x04001E1D RID: 7709
	public int Rows = 25;

	// Token: 0x04001E1E RID: 7710
	public int Columns = 25;

	// Token: 0x04001E1F RID: 7711
	public int ID;
}
