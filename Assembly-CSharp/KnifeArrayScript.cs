using System;
using UnityEngine;

// Token: 0x02000316 RID: 790
public class KnifeArrayScript : MonoBehaviour
{
	// Token: 0x060017D4 RID: 6100 RVA: 0x000D1460 File Offset: 0x000CF660
	private void Update()
	{
		this.Timer += Time.deltaTime;
		if (this.ID < 10)
		{
			if (this.Timer > this.SpawnTimes[this.ID] && this.GlobalKnifeArray.ID < 1000)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.Knife, base.transform.position, Quaternion.identity);
				gameObject.transform.parent = base.transform;
				gameObject.transform.localPosition = new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(0.5f, 2f), UnityEngine.Random.Range(-0.75f, -1.75f));
				gameObject.transform.parent = null;
				gameObject.transform.LookAt(this.KnifeTarget);
				this.GlobalKnifeArray.Knives[this.GlobalKnifeArray.ID] = gameObject.GetComponent<TimeStopKnifeScript>();
				this.GlobalKnifeArray.ID++;
				this.ID++;
				return;
			}
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04002208 RID: 8712
	public GlobalKnifeArrayScript GlobalKnifeArray;

	// Token: 0x04002209 RID: 8713
	public Transform KnifeTarget;

	// Token: 0x0400220A RID: 8714
	public float[] SpawnTimes;

	// Token: 0x0400220B RID: 8715
	public GameObject Knife;

	// Token: 0x0400220C RID: 8716
	public float Timer;

	// Token: 0x0400220D RID: 8717
	public int ID;
}
