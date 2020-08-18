using System;
using UnityEngine;

// Token: 0x02000478 RID: 1144
public class YanvaniaCandlestickHeadScript : MonoBehaviour
{
	// Token: 0x06001DA6 RID: 7590 RVA: 0x0017089C File Offset: 0x0016EA9C
	private void Start()
	{
		Rigidbody component = base.GetComponent<Rigidbody>();
		component.AddForce(base.transform.up * 100f);
		component.AddForce(base.transform.right * 100f);
		this.Value = UnityEngine.Random.Range(-1f, 1f);
	}

	// Token: 0x06001DA7 RID: 7591 RVA: 0x001708FC File Offset: 0x0016EAFC
	private void Update()
	{
		this.Rotation += new Vector3(this.Value, this.Value, this.Value);
		base.transform.localEulerAngles = this.Rotation;
		if (base.transform.localPosition.y < 0.23f)
		{
			UnityEngine.Object.Instantiate<GameObject>(this.Fire, base.transform.position, Quaternion.identity);
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04003A8D RID: 14989
	public GameObject Fire;

	// Token: 0x04003A8E RID: 14990
	public Vector3 Rotation;

	// Token: 0x04003A8F RID: 14991
	public float Value;
}
