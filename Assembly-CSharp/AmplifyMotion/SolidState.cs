using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace AmplifyMotion
{
	// Token: 0x020004DD RID: 1245
	internal class SolidState : MotionState
	{
		// Token: 0x06001F84 RID: 8068 RVA: 0x001827AB File Offset: 0x001809AB
		public SolidState(AmplifyMotionCamera owner, AmplifyMotionObjectBase obj) : base(owner, obj)
		{
			this.m_meshRenderer = this.m_obj.GetComponent<MeshRenderer>();
		}

		// Token: 0x06001F85 RID: 8069 RVA: 0x001827C6 File Offset: 0x001809C6
		private void IssueError(string message)
		{
			if (!SolidState.m_uniqueWarnings.Contains(this.m_obj))
			{
				Debug.LogWarning(message);
				SolidState.m_uniqueWarnings.Add(this.m_obj);
			}
			this.m_error = true;
		}

		// Token: 0x06001F86 RID: 8070 RVA: 0x001827F8 File Offset: 0x001809F8
		internal override void Initialize()
		{
			MeshFilter component = this.m_obj.GetComponent<MeshFilter>();
			if (component == null || component.sharedMesh == null)
			{
				this.IssueError("[AmplifyMotion] Invalid MeshFilter/Mesh in object " + this.m_obj.name + ". Skipping.");
				return;
			}
			base.Initialize();
			this.m_mesh = component.sharedMesh;
			this.m_sharedMaterials = base.ProcessSharedMaterials(this.m_meshRenderer.sharedMaterials);
			this.m_wasVisible = false;
		}

		// Token: 0x06001F87 RID: 8071 RVA: 0x0018287C File Offset: 0x00180A7C
		internal override void UpdateTransform(CommandBuffer updateCB, bool starting)
		{
			if (!this.m_initialized)
			{
				this.Initialize();
				return;
			}
			if (!starting && this.m_wasVisible)
			{
				this.m_prevLocalToWorld = this.m_currLocalToWorld;
			}
			this.m_currLocalToWorld = this.m_transform.localToWorldMatrix;
			this.m_moved = true;
			if (!this.m_owner.Overlay)
			{
				this.m_moved = (starting || MotionState.MatrixChanged(this.m_currLocalToWorld, this.m_prevLocalToWorld));
			}
			if (starting || !this.m_wasVisible)
			{
				this.m_prevLocalToWorld = this.m_currLocalToWorld;
			}
			this.m_wasVisible = this.m_meshRenderer.isVisible;
		}

		// Token: 0x06001F88 RID: 8072 RVA: 0x00182920 File Offset: 0x00180B20
		internal override void RenderVectors(Camera camera, CommandBuffer renderCB, float scale, Quality quality)
		{
			if (this.m_initialized && !this.m_error && this.m_meshRenderer.isVisible)
			{
				bool flag = (this.m_owner.Instance.CullingMask & 1 << this.m_obj.gameObject.layer) != 0;
				if (!flag || (flag && this.m_moved))
				{
					int num = flag ? this.m_owner.Instance.GenerateObjectId(this.m_obj.gameObject) : 255;
					Matrix4x4 value;
					if (this.m_obj.FixedStep)
					{
						value = this.m_owner.PrevViewProjMatrixRT * this.m_currLocalToWorld;
					}
					else
					{
						value = this.m_owner.PrevViewProjMatrixRT * this.m_prevLocalToWorld;
					}
					renderCB.SetGlobalMatrix("_AM_MATRIX_PREV_MVP", value);
					renderCB.SetGlobalFloat("_AM_OBJECT_ID", (float)num * 0.003921569f);
					renderCB.SetGlobalFloat("_AM_MOTION_SCALE", flag ? scale : 0f);
					int num2 = (quality == Quality.Mobile) ? 0 : 2;
					for (int i = 0; i < this.m_sharedMaterials.Length; i++)
					{
						MotionState.MaterialDesc materialDesc = this.m_sharedMaterials[i];
						int shaderPass = num2 + (materialDesc.coverage ? 1 : 0);
						if (materialDesc.coverage)
						{
							Texture mainTexture = materialDesc.material.mainTexture;
							if (mainTexture != null)
							{
								materialDesc.propertyBlock.SetTexture("_MainTex", mainTexture);
							}
							if (materialDesc.cutoff)
							{
								materialDesc.propertyBlock.SetFloat("_Cutoff", materialDesc.material.GetFloat("_Cutoff"));
							}
						}
						renderCB.DrawMesh(this.m_mesh, this.m_transform.localToWorldMatrix, this.m_owner.Instance.SolidVectorsMaterial, i, shaderPass, materialDesc.propertyBlock);
					}
				}
			}
		}

		// Token: 0x04003D1D RID: 15645
		private MeshRenderer m_meshRenderer;

		// Token: 0x04003D1E RID: 15646
		private MotionState.Matrix3x4 m_prevLocalToWorld;

		// Token: 0x04003D1F RID: 15647
		private MotionState.Matrix3x4 m_currLocalToWorld;

		// Token: 0x04003D20 RID: 15648
		private Mesh m_mesh;

		// Token: 0x04003D21 RID: 15649
		private MotionState.MaterialDesc[] m_sharedMaterials;

		// Token: 0x04003D22 RID: 15650
		public bool m_moved;

		// Token: 0x04003D23 RID: 15651
		private bool m_wasVisible;

		// Token: 0x04003D24 RID: 15652
		private static HashSet<AmplifyMotionObjectBase> m_uniqueWarnings = new HashSet<AmplifyMotionObjectBase>();
	}
}
