using System;
using UnityEngine;

// Token: 0x020002B1 RID: 689
public class GazerHairScript : MonoBehaviour
{
	// Token: 0x0600143B RID: 5179 RVA: 0x000B2C80 File Offset: 0x000B0E80
	private void Update()
	{
		this.ID = 0;
		while (this.ID < this.Weight.Length)
		{
			this.Weight[this.ID] = Mathf.MoveTowards(this.Weight[this.ID], this.TargetWeight[this.ID], Time.deltaTime * this.Strength);
			if (this.Weight[this.ID] == this.TargetWeight[this.ID])
			{
				this.TargetWeight[this.ID] = UnityEngine.Random.Range(0f, 100f);
			}
			this.MyMesh.SetBlendShapeWeight(this.ID, this.Weight[this.ID]);
			this.ID++;
		}
	}

	// Token: 0x04001CD4 RID: 7380
	public SkinnedMeshRenderer MyMesh;

	// Token: 0x04001CD5 RID: 7381
	public float[] TargetWeight;

	// Token: 0x04001CD6 RID: 7382
	public float[] Weight;

	// Token: 0x04001CD7 RID: 7383
	public float Strength = 100f;

	// Token: 0x04001CD8 RID: 7384
	public int ID;
}
