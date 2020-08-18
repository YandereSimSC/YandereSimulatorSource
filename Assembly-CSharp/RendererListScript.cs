using System;
using UnityEngine;

// Token: 0x0200038C RID: 908
public class RendererListScript : MonoBehaviour
{
	// Token: 0x06001990 RID: 6544 RVA: 0x000F98E4 File Offset: 0x000F7AE4
	private void Start()
	{
		Transform[] componentsInChildren = base.gameObject.GetComponentsInChildren<Transform>();
		int num = 0;
		foreach (Transform transform in componentsInChildren)
		{
			if (transform.gameObject.GetComponent<Renderer>() != null)
			{
				this.Renderers[num] = transform.gameObject.GetComponent<Renderer>();
				num++;
			}
		}
	}

	// Token: 0x06001991 RID: 6545 RVA: 0x000F993C File Offset: 0x000F7B3C
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.LeftControl))
		{
			foreach (Renderer renderer in this.Renderers)
			{
				renderer.enabled = !renderer.enabled;
			}
		}
	}

	// Token: 0x04002771 RID: 10097
	public Renderer[] Renderers;
}
