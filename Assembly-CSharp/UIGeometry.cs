using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000081 RID: 129
public class UIGeometry
{
	// Token: 0x1700009A RID: 154
	// (get) Token: 0x0600051B RID: 1307 RVA: 0x000310E0 File Offset: 0x0002F2E0
	public bool hasVertices
	{
		get
		{
			return this.verts.Count > 0;
		}
	}

	// Token: 0x1700009B RID: 155
	// (get) Token: 0x0600051C RID: 1308 RVA: 0x000310F0 File Offset: 0x0002F2F0
	public bool hasTransformed
	{
		get
		{
			return this.mRtpVerts != null && this.mRtpVerts.Count > 0 && this.mRtpVerts.Count == this.verts.Count;
		}
	}

	// Token: 0x0600051D RID: 1309 RVA: 0x00031122 File Offset: 0x0002F322
	public void Clear()
	{
		this.verts.Clear();
		this.uvs.Clear();
		this.cols.Clear();
		this.mRtpVerts.Clear();
	}

	// Token: 0x0600051E RID: 1310 RVA: 0x00031150 File Offset: 0x0002F350
	public void ApplyTransform(Matrix4x4 widgetToPanel, bool generateNormals = true)
	{
		if (this.verts.Count > 0)
		{
			this.mRtpVerts.Clear();
			int i = 0;
			int count = this.verts.Count;
			while (i < count)
			{
				this.mRtpVerts.Add(widgetToPanel.MultiplyPoint3x4(this.verts[i]));
				i++;
			}
			if (generateNormals)
			{
				this.mRtpNormal = widgetToPanel.MultiplyVector(Vector3.back).normalized;
				Vector3 normalized = widgetToPanel.MultiplyVector(Vector3.right).normalized;
				this.mRtpTan = new Vector4(normalized.x, normalized.y, normalized.z, -1f);
				return;
			}
		}
		else
		{
			this.mRtpVerts.Clear();
		}
	}

	// Token: 0x0600051F RID: 1311 RVA: 0x00031210 File Offset: 0x0002F410
	public void WriteToBuffers(List<Vector3> v, List<Vector2> u, List<Color> c, List<Vector3> n, List<Vector4> t, List<Vector4> u2)
	{
		if (this.mRtpVerts != null && this.mRtpVerts.Count > 0)
		{
			if (n == null)
			{
				int i = 0;
				int count = this.mRtpVerts.Count;
				while (i < count)
				{
					v.Add(this.mRtpVerts[i]);
					u.Add(this.uvs[i]);
					c.Add(this.cols[i]);
					i++;
				}
			}
			else
			{
				int j = 0;
				int count2 = this.mRtpVerts.Count;
				while (j < count2)
				{
					v.Add(this.mRtpVerts[j]);
					u.Add(this.uvs[j]);
					c.Add(this.cols[j]);
					n.Add(this.mRtpNormal);
					t.Add(this.mRtpTan);
					j++;
				}
			}
			if (u2 != null)
			{
				Vector4 zero = Vector4.zero;
				int k = 0;
				int count3 = this.verts.Count;
				while (k < count3)
				{
					zero.x = this.verts[k].x;
					zero.y = this.verts[k].y;
					u2.Add(zero);
					k++;
				}
			}
			if (this.onCustomWrite != null)
			{
				this.onCustomWrite(v, u, c, n, t, u2);
			}
		}
	}

	// Token: 0x04000565 RID: 1381
	public List<Vector3> verts = new List<Vector3>();

	// Token: 0x04000566 RID: 1382
	public List<Vector2> uvs = new List<Vector2>();

	// Token: 0x04000567 RID: 1383
	public List<Color> cols = new List<Color>();

	// Token: 0x04000568 RID: 1384
	public UIGeometry.OnCustomWrite onCustomWrite;

	// Token: 0x04000569 RID: 1385
	private List<Vector3> mRtpVerts = new List<Vector3>();

	// Token: 0x0400056A RID: 1386
	private Vector3 mRtpNormal;

	// Token: 0x0400056B RID: 1387
	private Vector4 mRtpTan;

	// Token: 0x0200064A RID: 1610
	// (Invoke) Token: 0x06002AC8 RID: 10952
	public delegate void OnCustomWrite(List<Vector3> v, List<Vector2> u, List<Color> c, List<Vector3> n, List<Vector4> t, List<Vector4> u2);
}
