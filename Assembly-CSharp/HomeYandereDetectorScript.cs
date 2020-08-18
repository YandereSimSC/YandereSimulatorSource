using System;
using UnityEngine;

// Token: 0x020002FA RID: 762
public class HomeYandereDetectorScript : MonoBehaviour
{
	// Token: 0x06001753 RID: 5971 RVA: 0x000C8C69 File Offset: 0x000C6E69
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			this.YandereDetected = true;
		}
	}

	// Token: 0x06001754 RID: 5972 RVA: 0x000C8C84 File Offset: 0x000C6E84
	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player")
		{
			this.YandereDetected = false;
		}
	}

	// Token: 0x04002049 RID: 8265
	public bool YandereDetected;
}
