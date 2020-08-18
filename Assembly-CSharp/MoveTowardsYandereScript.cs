using System;
using UnityEngine;

// Token: 0x02000337 RID: 823
public class MoveTowardsYandereScript : MonoBehaviour
{
	// Token: 0x0600183C RID: 6204 RVA: 0x000D8D15 File Offset: 0x000D6F15
	private void Start()
	{
		this.Yandere = GameObject.Find("YandereChan").GetComponent<YandereScript>().Spine[3];
		this.Distance = Vector3.Distance(base.transform.position, this.Yandere.position);
	}

	// Token: 0x0600183D RID: 6205 RVA: 0x000D8D54 File Offset: 0x000D6F54
	private void Update()
	{
		if (Vector3.Distance(base.transform.position, this.Yandere.position) > this.Distance * 0.5f && base.transform.position.y < this.Yandere.position.y + 0.5f)
		{
			base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y + Time.deltaTime, base.transform.position.z);
		}
		this.Speed += Time.deltaTime;
		base.transform.position = Vector3.MoveTowards(base.transform.position, this.Yandere.position, this.Speed * Time.deltaTime);
		if (Vector3.Distance(base.transform.position, this.Yandere.position) == 0f)
		{
			this.Smoke.emission.enabled = false;
		}
	}

	// Token: 0x04002325 RID: 8997
	public ParticleSystem Smoke;

	// Token: 0x04002326 RID: 8998
	public Transform Yandere;

	// Token: 0x04002327 RID: 8999
	public float Distance;

	// Token: 0x04002328 RID: 9000
	public float Speed;

	// Token: 0x04002329 RID: 9001
	public bool Fall;
}
