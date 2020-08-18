using System;
using UnityEngine;

// Token: 0x02000474 RID: 1140
public class YanvaniaBlackHoleAttackScript : MonoBehaviour
{
	// Token: 0x06001D9A RID: 7578 RVA: 0x0017046D File Offset: 0x0016E66D
	private void Start()
	{
		this.Yanmont = GameObject.Find("YanmontChan").GetComponent<YanvaniaYanmontScript>();
	}

	// Token: 0x06001D9B RID: 7579 RVA: 0x00170484 File Offset: 0x0016E684
	private void Update()
	{
		base.transform.position = Vector3.MoveTowards(base.transform.position, this.Yanmont.transform.position + Vector3.up, Time.deltaTime);
		if (Vector3.Distance(base.transform.position, this.Yanmont.transform.position) > 10f || this.Yanmont.EnterCutscene)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06001D9C RID: 7580 RVA: 0x0017050C File Offset: 0x0016E70C
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			UnityEngine.Object.Instantiate<GameObject>(this.BlackExplosion, base.transform.position, Quaternion.identity);
			this.Yanmont.TakeDamage(20);
		}
		if (other.gameObject.name == "Heart")
		{
			UnityEngine.Object.Instantiate<GameObject>(this.BlackExplosion, base.transform.position, Quaternion.identity);
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04003A7E RID: 14974
	public YanvaniaYanmontScript Yanmont;

	// Token: 0x04003A7F RID: 14975
	public GameObject BlackExplosion;
}
