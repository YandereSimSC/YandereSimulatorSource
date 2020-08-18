using System;
using UnityEngine;

// Token: 0x0200004D RID: 77
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Interaction/Drag Camera")]
public class UIDragCamera : MonoBehaviour
{
	// Token: 0x060001B0 RID: 432 RVA: 0x000159FC File Offset: 0x00013BFC
	private void Awake()
	{
		if (this.draggableCamera == null)
		{
			this.draggableCamera = NGUITools.FindInParents<UIDraggableCamera>(base.gameObject);
		}
	}

	// Token: 0x060001B1 RID: 433 RVA: 0x00015A1D File Offset: 0x00013C1D
	private void OnPress(bool isPressed)
	{
		if (base.enabled && NGUITools.GetActive(base.gameObject) && this.draggableCamera != null && this.draggableCamera.enabled)
		{
			this.draggableCamera.Press(isPressed);
		}
	}

	// Token: 0x060001B2 RID: 434 RVA: 0x00015A5B File Offset: 0x00013C5B
	private void OnDrag(Vector2 delta)
	{
		if (base.enabled && NGUITools.GetActive(base.gameObject) && this.draggableCamera != null && this.draggableCamera.enabled)
		{
			this.draggableCamera.Drag(delta);
		}
	}

	// Token: 0x060001B3 RID: 435 RVA: 0x00015A99 File Offset: 0x00013C99
	private void OnScroll(float delta)
	{
		if (base.enabled && NGUITools.GetActive(base.gameObject) && this.draggableCamera != null && this.draggableCamera.enabled)
		{
			this.draggableCamera.Scroll(delta);
		}
	}

	// Token: 0x04000333 RID: 819
	public UIDraggableCamera draggableCamera;
}
