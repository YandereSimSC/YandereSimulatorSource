using System;
using UnityEngine;

// Token: 0x0200045B RID: 1115
public class VibrateScript : MonoBehaviour
{
	// Token: 0x06001CEB RID: 7403 RVA: 0x00155FAE File Offset: 0x001541AE
	private void Start()
	{
		this.Origin = base.transform.localPosition;
	}

	// Token: 0x06001CEC RID: 7404 RVA: 0x00155FC4 File Offset: 0x001541C4
	private void Update()
	{
		base.transform.localPosition = new Vector3(this.Origin.x + UnityEngine.Random.Range(-5f, 5f), this.Origin.y + UnityEngine.Random.Range(-5f, 5f), base.transform.localPosition.z);
	}

	// Token: 0x04003604 RID: 13828
	public Vector3 Origin;
}
