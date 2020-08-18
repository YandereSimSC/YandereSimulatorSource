using System;
using UnityEngine;

// Token: 0x02000487 RID: 1159
public class YanvaniaWineScript : MonoBehaviour
{
	// Token: 0x06001DD6 RID: 7638 RVA: 0x00174194 File Offset: 0x00172394
	private void Update()
	{
		if (base.transform.parent == null)
		{
			this.Rotation += Time.deltaTime * 360f;
			base.transform.localEulerAngles = new Vector3(this.Rotation, this.Rotation, this.Rotation);
			if (base.transform.position.y < 6.5f)
			{
				UnityEngine.Object.Instantiate<GameObject>(this.Shards, new Vector3(base.transform.position.x, 6.5f, base.transform.position.z), Quaternion.identity).transform.localEulerAngles = new Vector3(-90f, 0f, 0f);
				AudioSource.PlayClipAtPoint(base.GetComponent<AudioSource>().clip, base.transform.position);
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}
	}

	// Token: 0x04003B1F RID: 15135
	public GameObject Shards;

	// Token: 0x04003B20 RID: 15136
	public float Rotation;
}
