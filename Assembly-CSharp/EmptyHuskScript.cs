using System;
using UnityEngine;

// Token: 0x0200027E RID: 638
public class EmptyHuskScript : MonoBehaviour
{
	// Token: 0x06001396 RID: 5014 RVA: 0x000A8560 File Offset: 0x000A6760
	private void Update()
	{
		if (this.EatPhase < this.BloodTimes.Length && this.MyAnimation["f02_sixEat_00"].time > this.BloodTimes[this.EatPhase])
		{
			UnityEngine.Object.Instantiate<GameObject>(this.TargetStudent.StabBloodEffect, this.Mouth.position, Quaternion.identity).GetComponent<RandomStabScript>().Biting = true;
			this.EatPhase++;
		}
		if (this.MyAnimation["f02_sixEat_00"].time >= this.MyAnimation["f02_sixEat_00"].length)
		{
			if (this.DarkAura != null)
			{
				UnityEngine.Object.Instantiate<GameObject>(this.DarkAura, base.transform.position + Vector3.up * 0.81f, Quaternion.identity);
			}
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04001AC8 RID: 6856
	public StudentScript TargetStudent;

	// Token: 0x04001AC9 RID: 6857
	public Animation MyAnimation;

	// Token: 0x04001ACA RID: 6858
	public GameObject DarkAura;

	// Token: 0x04001ACB RID: 6859
	public Transform Mouth;

	// Token: 0x04001ACC RID: 6860
	public float[] BloodTimes;

	// Token: 0x04001ACD RID: 6861
	public int EatPhase;
}
