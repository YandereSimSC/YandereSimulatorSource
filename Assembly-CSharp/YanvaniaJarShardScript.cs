using System;
using UnityEngine;

// Token: 0x0200047F RID: 1151
public class YanvaniaJarShardScript : MonoBehaviour
{
	// Token: 0x06001DBD RID: 7613 RVA: 0x001722F8 File Offset: 0x001704F8
	private void Start()
	{
		this.Rotation = UnityEngine.Random.Range(-360f, 360f);
		base.GetComponent<Rigidbody>().AddForce(UnityEngine.Random.Range(-100f, 100f), UnityEngine.Random.Range(0f, 100f), UnityEngine.Random.Range(-100f, 100f));
	}

	// Token: 0x06001DBE RID: 7614 RVA: 0x00172354 File Offset: 0x00170554
	private void Update()
	{
		this.MyRotation += this.Rotation;
		base.transform.eulerAngles = new Vector3(this.MyRotation, this.MyRotation, this.MyRotation);
		if (base.transform.position.y < 6.5f)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04003AD5 RID: 15061
	public float MyRotation;

	// Token: 0x04003AD6 RID: 15062
	public float Rotation;
}
