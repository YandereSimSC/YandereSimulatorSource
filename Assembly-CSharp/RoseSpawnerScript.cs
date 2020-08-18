using System;
using UnityEngine;

// Token: 0x0200049F RID: 1183
public class RoseSpawnerScript : MonoBehaviour
{
	// Token: 0x06001E27 RID: 7719 RVA: 0x00178F52 File Offset: 0x00177152
	private void Start()
	{
		this.SpawnRose();
	}

	// Token: 0x06001E28 RID: 7720 RVA: 0x00178F5A File Offset: 0x0017715A
	private void Update()
	{
		this.Timer += Time.deltaTime;
		if (this.Timer > 0.1f)
		{
			this.SpawnRose();
		}
	}

	// Token: 0x06001E29 RID: 7721 RVA: 0x00178F84 File Offset: 0x00177184
	private void SpawnRose()
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.Rose, base.transform.position, Quaternion.identity);
		gameObject.GetComponent<Rigidbody>().AddForce(base.transform.forward * this.ForwardForce);
		gameObject.GetComponent<Rigidbody>().AddForce(base.transform.up * this.UpwardForce);
		gameObject.transform.localEulerAngles = new Vector3(UnityEngine.Random.Range(0f, 360f), UnityEngine.Random.Range(0f, 360f), UnityEngine.Random.Range(0f, 360f));
		base.transform.localPosition = new Vector3(UnityEngine.Random.Range(-5f, 5f), base.transform.localPosition.y, base.transform.localPosition.z);
		base.transform.LookAt(this.DramaGirl);
		this.Timer = 0f;
	}

	// Token: 0x04003BFC RID: 15356
	public Transform DramaGirl;

	// Token: 0x04003BFD RID: 15357
	public Transform Target;

	// Token: 0x04003BFE RID: 15358
	public GameObject Rose;

	// Token: 0x04003BFF RID: 15359
	public float Timer;

	// Token: 0x04003C00 RID: 15360
	public float ForwardForce;

	// Token: 0x04003C01 RID: 15361
	public float UpwardForce;
}
