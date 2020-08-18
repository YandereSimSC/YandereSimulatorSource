using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace AmplifyMotion
{
	// Token: 0x020004D8 RID: 1240
	[Serializable]
	internal abstract class MotionState
	{
		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x06001F45 RID: 8005 RVA: 0x0017F95F File Offset: 0x0017DB5F
		public AmplifyMotionCamera Owner
		{
			get
			{
				return this.m_owner;
			}
		}

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x06001F46 RID: 8006 RVA: 0x0017F967 File Offset: 0x0017DB67
		public bool Initialized
		{
			get
			{
				return this.m_initialized;
			}
		}

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x06001F47 RID: 8007 RVA: 0x0017F96F File Offset: 0x0017DB6F
		public bool Error
		{
			get
			{
				return this.m_error;
			}
		}

		// Token: 0x06001F48 RID: 8008 RVA: 0x0017F977 File Offset: 0x0017DB77
		public MotionState(AmplifyMotionCamera owner, AmplifyMotionObjectBase obj)
		{
			this.m_error = false;
			this.m_initialized = false;
			this.m_owner = owner;
			this.m_obj = obj;
			this.m_transform = obj.transform;
		}

		// Token: 0x06001F49 RID: 8009 RVA: 0x0017F9A7 File Offset: 0x0017DBA7
		internal virtual void Initialize()
		{
			this.m_initialized = true;
		}

		// Token: 0x06001F4A RID: 8010 RVA: 0x00002ACE File Offset: 0x00000CCE
		internal virtual void Shutdown()
		{
		}

		// Token: 0x06001F4B RID: 8011 RVA: 0x00002ACE File Offset: 0x00000CCE
		internal virtual void AsyncUpdate()
		{
		}

		// Token: 0x06001F4C RID: 8012
		internal abstract void UpdateTransform(CommandBuffer updateCB, bool starting);

		// Token: 0x06001F4D RID: 8013 RVA: 0x00002ACE File Offset: 0x00000CCE
		internal virtual void RenderVectors(Camera camera, CommandBuffer renderCB, float scale, Quality quality)
		{
		}

		// Token: 0x06001F4E RID: 8014 RVA: 0x00002ACE File Offset: 0x00000CCE
		internal virtual void RenderDebugHUD()
		{
		}

		// Token: 0x06001F4F RID: 8015 RVA: 0x0017F9B0 File Offset: 0x0017DBB0
		protected MotionState.MaterialDesc[] ProcessSharedMaterials(Material[] mats)
		{
			MotionState.MaterialDesc[] array = new MotionState.MaterialDesc[mats.Length];
			for (int i = 0; i < mats.Length; i++)
			{
				array[i].material = mats[i];
				bool flag = mats[i].GetTag("RenderType", false) == "TransparentCutout" || mats[i].IsKeywordEnabled("_ALPHATEST_ON");
				array[i].propertyBlock = new MaterialPropertyBlock();
				array[i].coverage = (mats[i].HasProperty("_MainTex") && flag);
				array[i].cutoff = mats[i].HasProperty("_Cutoff");
				if (flag && !array[i].coverage && !MotionState.m_materialWarnings.Contains(array[i].material))
				{
					Debug.LogWarning(string.Concat(new string[]
					{
						"[AmplifyMotion] TransparentCutout material \"",
						array[i].material.name,
						"\" {",
						array[i].material.shader.name,
						"} not using _MainTex standard property."
					}));
					MotionState.m_materialWarnings.Add(array[i].material);
				}
			}
			return array;
		}

		// Token: 0x06001F50 RID: 8016 RVA: 0x0017FAF8 File Offset: 0x0017DCF8
		protected static bool MatrixChanged(MotionState.Matrix3x4 a, MotionState.Matrix3x4 b)
		{
			return Vector4.SqrMagnitude(new Vector4(a.m00 - b.m00, a.m01 - b.m01, a.m02 - b.m02, a.m03 - b.m03)) > 0f || Vector4.SqrMagnitude(new Vector4(a.m10 - b.m10, a.m11 - b.m11, a.m12 - b.m12, a.m13 - b.m13)) > 0f || Vector4.SqrMagnitude(new Vector4(a.m20 - b.m20, a.m21 - b.m21, a.m22 - b.m22, a.m23 - b.m23)) > 0f;
		}

		// Token: 0x06001F51 RID: 8017 RVA: 0x0017FBDC File Offset: 0x0017DDDC
		protected static void MulPoint3x4_XYZ(ref Vector3 result, ref MotionState.Matrix3x4 mat, Vector4 vec)
		{
			result.x = mat.m00 * vec.x + mat.m01 * vec.y + mat.m02 * vec.z + mat.m03;
			result.y = mat.m10 * vec.x + mat.m11 * vec.y + mat.m12 * vec.z + mat.m13;
			result.z = mat.m20 * vec.x + mat.m21 * vec.y + mat.m22 * vec.z + mat.m23;
		}

		// Token: 0x06001F52 RID: 8018 RVA: 0x0017FC8C File Offset: 0x0017DE8C
		protected static void MulPoint3x4_XYZW(ref Vector3 result, ref MotionState.Matrix3x4 mat, Vector4 vec)
		{
			result.x = mat.m00 * vec.x + mat.m01 * vec.y + mat.m02 * vec.z + mat.m03 * vec.w;
			result.y = mat.m10 * vec.x + mat.m11 * vec.y + mat.m12 * vec.z + mat.m13 * vec.w;
			result.z = mat.m20 * vec.x + mat.m21 * vec.y + mat.m22 * vec.z + mat.m23 * vec.w;
		}

		// Token: 0x06001F53 RID: 8019 RVA: 0x0017FD50 File Offset: 0x0017DF50
		protected static void MulAddPoint3x4_XYZW(ref Vector3 result, ref MotionState.Matrix3x4 mat, Vector4 vec)
		{
			result.x += mat.m00 * vec.x + mat.m01 * vec.y + mat.m02 * vec.z + mat.m03 * vec.w;
			result.y += mat.m10 * vec.x + mat.m11 * vec.y + mat.m12 * vec.z + mat.m13 * vec.w;
			result.z += mat.m20 * vec.x + mat.m21 * vec.y + mat.m22 * vec.z + mat.m23 * vec.w;
		}

		// Token: 0x04003CD1 RID: 15569
		public const int AsyncUpdateTimeout = 100;

		// Token: 0x04003CD2 RID: 15570
		protected bool m_error;

		// Token: 0x04003CD3 RID: 15571
		protected bool m_initialized;

		// Token: 0x04003CD4 RID: 15572
		protected Transform m_transform;

		// Token: 0x04003CD5 RID: 15573
		protected AmplifyMotionCamera m_owner;

		// Token: 0x04003CD6 RID: 15574
		protected AmplifyMotionObjectBase m_obj;

		// Token: 0x04003CD7 RID: 15575
		private static HashSet<Material> m_materialWarnings = new HashSet<Material>();

		// Token: 0x020006F8 RID: 1784
		protected struct MaterialDesc
		{
			// Token: 0x0400484C RID: 18508
			public Material material;

			// Token: 0x0400484D RID: 18509
			public MaterialPropertyBlock propertyBlock;

			// Token: 0x0400484E RID: 18510
			public bool coverage;

			// Token: 0x0400484F RID: 18511
			public bool cutoff;
		}

		// Token: 0x020006F9 RID: 1785
		protected struct Matrix3x4
		{
			// Token: 0x06002C1D RID: 11293 RVA: 0x001C8C54 File Offset: 0x001C6E54
			public Vector4 GetRow(int i)
			{
				if (i == 0)
				{
					return new Vector4(this.m00, this.m01, this.m02, this.m03);
				}
				if (i == 1)
				{
					return new Vector4(this.m10, this.m11, this.m12, this.m13);
				}
				if (i == 2)
				{
					return new Vector4(this.m20, this.m21, this.m22, this.m23);
				}
				return new Vector4(0f, 0f, 0f, 1f);
			}

			// Token: 0x06002C1E RID: 11294 RVA: 0x001C8CE0 File Offset: 0x001C6EE0
			public static implicit operator MotionState.Matrix3x4(Matrix4x4 from)
			{
				return new MotionState.Matrix3x4
				{
					m00 = from.m00,
					m01 = from.m01,
					m02 = from.m02,
					m03 = from.m03,
					m10 = from.m10,
					m11 = from.m11,
					m12 = from.m12,
					m13 = from.m13,
					m20 = from.m20,
					m21 = from.m21,
					m22 = from.m22,
					m23 = from.m23
				};
			}

			// Token: 0x06002C1F RID: 11295 RVA: 0x001C8D94 File Offset: 0x001C6F94
			public static implicit operator Matrix4x4(MotionState.Matrix3x4 from)
			{
				Matrix4x4 result = default(Matrix4x4);
				result.m00 = from.m00;
				result.m01 = from.m01;
				result.m02 = from.m02;
				result.m03 = from.m03;
				result.m10 = from.m10;
				result.m11 = from.m11;
				result.m12 = from.m12;
				result.m13 = from.m13;
				result.m20 = from.m20;
				result.m21 = from.m21;
				result.m22 = from.m22;
				result.m23 = from.m23;
				result.m30 = (result.m31 = (result.m32 = 0f));
				result.m33 = 1f;
				return result;
			}

			// Token: 0x06002C20 RID: 11296 RVA: 0x001C8E74 File Offset: 0x001C7074
			public static MotionState.Matrix3x4 operator *(MotionState.Matrix3x4 a, MotionState.Matrix3x4 b)
			{
				return new MotionState.Matrix3x4
				{
					m00 = a.m00 * b.m00 + a.m01 * b.m10 + a.m02 * b.m20,
					m01 = a.m00 * b.m01 + a.m01 * b.m11 + a.m02 * b.m21,
					m02 = a.m00 * b.m02 + a.m01 * b.m12 + a.m02 * b.m22,
					m03 = a.m00 * b.m03 + a.m01 * b.m13 + a.m02 * b.m23 + a.m03,
					m10 = a.m10 * b.m00 + a.m11 * b.m10 + a.m12 * b.m20,
					m11 = a.m10 * b.m01 + a.m11 * b.m11 + a.m12 * b.m21,
					m12 = a.m10 * b.m02 + a.m11 * b.m12 + a.m12 * b.m22,
					m13 = a.m10 * b.m03 + a.m11 * b.m13 + a.m12 * b.m23 + a.m13,
					m20 = a.m20 * b.m00 + a.m21 * b.m10 + a.m22 * b.m20,
					m21 = a.m20 * b.m01 + a.m21 * b.m11 + a.m22 * b.m21,
					m22 = a.m20 * b.m02 + a.m21 * b.m12 + a.m22 * b.m22,
					m23 = a.m20 * b.m03 + a.m21 * b.m13 + a.m22 * b.m23 + a.m23
				};
			}

			// Token: 0x04004850 RID: 18512
			public float m00;

			// Token: 0x04004851 RID: 18513
			public float m01;

			// Token: 0x04004852 RID: 18514
			public float m02;

			// Token: 0x04004853 RID: 18515
			public float m03;

			// Token: 0x04004854 RID: 18516
			public float m10;

			// Token: 0x04004855 RID: 18517
			public float m11;

			// Token: 0x04004856 RID: 18518
			public float m12;

			// Token: 0x04004857 RID: 18519
			public float m13;

			// Token: 0x04004858 RID: 18520
			public float m20;

			// Token: 0x04004859 RID: 18521
			public float m21;

			// Token: 0x0400485A RID: 18522
			public float m22;

			// Token: 0x0400485B RID: 18523
			public float m23;
		}
	}
}
