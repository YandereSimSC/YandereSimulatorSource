using System;
using UnityEngine;

// Token: 0x020000B1 RID: 177
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
[AddComponentMenu("NGUI/UI/Viewport Camera")]
public class UIViewport : MonoBehaviour
{
	// Token: 0x06000930 RID: 2352 RVA: 0x00048F72 File Offset: 0x00047172
	private void Start()
	{
		this.mCam = base.GetComponent<Camera>();
		if (this.sourceCamera == null)
		{
			this.sourceCamera = Camera.main;
		}
	}

	// Token: 0x06000931 RID: 2353 RVA: 0x00048F9C File Offset: 0x0004719C
	private void LateUpdate()
	{
		if (this.topLeft != null && this.bottomRight != null)
		{
			if (this.topLeft.gameObject.activeInHierarchy)
			{
				Vector3 vector = this.sourceCamera.WorldToScreenPoint(this.topLeft.position);
				Vector3 vector2 = this.sourceCamera.WorldToScreenPoint(this.bottomRight.position);
				Rect rect = new Rect(vector.x / (float)Screen.width, vector2.y / (float)Screen.height, (vector2.x - vector.x) / (float)Screen.width, (vector.y - vector2.y) / (float)Screen.height);
				float num = this.fullSize * rect.height;
				if (rect != this.mCam.rect)
				{
					this.mCam.rect = rect;
				}
				if (this.mCam.orthographicSize != num)
				{
					this.mCam.orthographicSize = num;
				}
				this.mCam.enabled = true;
				return;
			}
			this.mCam.enabled = false;
		}
	}

	// Token: 0x040007C0 RID: 1984
	public Camera sourceCamera;

	// Token: 0x040007C1 RID: 1985
	public Transform topLeft;

	// Token: 0x040007C2 RID: 1986
	public Transform bottomRight;

	// Token: 0x040007C3 RID: 1987
	public float fullSize = 1f;

	// Token: 0x040007C4 RID: 1988
	private Camera mCam;
}
