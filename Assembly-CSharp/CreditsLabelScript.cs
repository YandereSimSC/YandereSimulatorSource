using System;
using UnityEngine;

// Token: 0x0200024F RID: 591
public class CreditsLabelScript : MonoBehaviour
{
	// Token: 0x060012BC RID: 4796 RVA: 0x00095BA4 File Offset: 0x00093DA4
	private void Start()
	{
		this.Rotation = -90f;
		base.transform.localEulerAngles = new Vector3(base.transform.localEulerAngles.x, this.Rotation, base.transform.localEulerAngles.z);
	}

	// Token: 0x060012BD RID: 4797 RVA: 0x00095BF4 File Offset: 0x00093DF4
	private void Update()
	{
		this.Rotation += Time.deltaTime * this.RotationSpeed;
		base.transform.localEulerAngles = new Vector3(base.transform.localEulerAngles.x, this.Rotation, base.transform.localEulerAngles.z);
		base.transform.localPosition = new Vector3(base.transform.localPosition.x, base.transform.localPosition.y + Time.deltaTime * this.MovementSpeed, base.transform.localPosition.z);
		if (this.Rotation > 90f)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04001852 RID: 6226
	public float RotationSpeed;

	// Token: 0x04001853 RID: 6227
	public float MovementSpeed;

	// Token: 0x04001854 RID: 6228
	public float Rotation;
}
