using System;
using UnityEngine;

// Token: 0x0200049C RID: 1180
public class FoldingChairScript : MonoBehaviour
{
	// Token: 0x06001E1F RID: 7711 RVA: 0x00178E30 File Offset: 0x00177030
	private void Start()
	{
		int num = UnityEngine.Random.Range(0, this.Student.Length);
		UnityEngine.Object.Instantiate<GameObject>(this.Student[num], base.transform.position - new Vector3(0f, 0.4f, 0f), base.transform.rotation);
	}

	// Token: 0x04003BF9 RID: 15353
	public GameObject[] Student;
}
