using System;
using UnityEngine;

// Token: 0x02000052 RID: 82
[AddComponentMenu("NGUI/Interaction/Drag-Resize Widget")]
public class UIDragResize : MonoBehaviour
{
	// Token: 0x060001E1 RID: 481 RVA: 0x00016E60 File Offset: 0x00015060
	private void OnDragStart()
	{
		if (this.target != null)
		{
			Vector3[] worldCorners = this.target.worldCorners;
			this.mPlane = new Plane(worldCorners[0], worldCorners[1], worldCorners[3]);
			Ray currentRay = UICamera.currentRay;
			float distance;
			if (this.mPlane.Raycast(currentRay, out distance))
			{
				this.mRayPos = currentRay.GetPoint(distance);
				this.mLocalPos = this.target.cachedTransform.localPosition;
				this.mWidth = this.target.width;
				this.mHeight = this.target.height;
				this.mDragging = true;
			}
		}
	}

	// Token: 0x060001E2 RID: 482 RVA: 0x00016F10 File Offset: 0x00015110
	private void OnDrag(Vector2 delta)
	{
		if (this.mDragging && this.target != null)
		{
			Ray currentRay = UICamera.currentRay;
			float distance;
			if (this.mPlane.Raycast(currentRay, out distance))
			{
				Transform cachedTransform = this.target.cachedTransform;
				cachedTransform.localPosition = this.mLocalPos;
				this.target.width = this.mWidth;
				this.target.height = this.mHeight;
				Vector3 b = currentRay.GetPoint(distance) - this.mRayPos;
				cachedTransform.position += b;
				Vector3 vector = Quaternion.Inverse(cachedTransform.localRotation) * (cachedTransform.localPosition - this.mLocalPos);
				cachedTransform.localPosition = this.mLocalPos;
				NGUIMath.ResizeWidget(this.target, this.pivot, vector.x, vector.y, this.minWidth, this.minHeight, this.maxWidth, this.maxHeight);
				if (this.updateAnchors)
				{
					this.target.BroadcastMessage("UpdateAnchors");
				}
			}
		}
	}

	// Token: 0x060001E3 RID: 483 RVA: 0x00017030 File Offset: 0x00015230
	private void OnDragEnd()
	{
		this.mDragging = false;
	}

	// Token: 0x0400035C RID: 860
	public UIWidget target;

	// Token: 0x0400035D RID: 861
	public UIWidget.Pivot pivot = UIWidget.Pivot.BottomRight;

	// Token: 0x0400035E RID: 862
	public int minWidth = 100;

	// Token: 0x0400035F RID: 863
	public int minHeight = 100;

	// Token: 0x04000360 RID: 864
	public int maxWidth = 100000;

	// Token: 0x04000361 RID: 865
	public int maxHeight = 100000;

	// Token: 0x04000362 RID: 866
	public bool updateAnchors;

	// Token: 0x04000363 RID: 867
	private Plane mPlane;

	// Token: 0x04000364 RID: 868
	private Vector3 mRayPos;

	// Token: 0x04000365 RID: 869
	private Vector3 mLocalPos;

	// Token: 0x04000366 RID: 870
	private int mWidth;

	// Token: 0x04000367 RID: 871
	private int mHeight;

	// Token: 0x04000368 RID: 872
	private bool mDragging;
}
