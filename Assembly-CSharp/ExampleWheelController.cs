using System;
using UnityEngine;

// Token: 0x020000B8 RID: 184
public class ExampleWheelController : MonoBehaviour
{
	// Token: 0x060009B1 RID: 2481 RVA: 0x0004B757 File Offset: 0x00049957
	private void Start()
	{
		this.m_Rigidbody = base.GetComponent<Rigidbody>();
		this.m_Rigidbody.maxAngularVelocity = 100f;
	}

	// Token: 0x060009B2 RID: 2482 RVA: 0x0004B778 File Offset: 0x00049978
	private void Update()
	{
		if (Input.GetKey(KeyCode.UpArrow))
		{
			this.m_Rigidbody.AddRelativeTorque(new Vector3(-1f * this.acceleration, 0f, 0f), ForceMode.Acceleration);
		}
		else if (Input.GetKey(KeyCode.DownArrow))
		{
			this.m_Rigidbody.AddRelativeTorque(new Vector3(1f * this.acceleration, 0f, 0f), ForceMode.Acceleration);
		}
		float value = -this.m_Rigidbody.angularVelocity.x / 100f;
		if (this.motionVectorRenderer)
		{
			this.motionVectorRenderer.material.SetFloat(ExampleWheelController.Uniforms._MotionAmount, Mathf.Clamp(value, -0.25f, 0.25f));
		}
	}

	// Token: 0x04000819 RID: 2073
	public float acceleration;

	// Token: 0x0400081A RID: 2074
	public Renderer motionVectorRenderer;

	// Token: 0x0400081B RID: 2075
	private Rigidbody m_Rigidbody;

	// Token: 0x02000692 RID: 1682
	private static class Uniforms
	{
		// Token: 0x04004600 RID: 17920
		internal static readonly int _MotionAmount = Shader.PropertyToID("_MotionAmount");
	}
}
