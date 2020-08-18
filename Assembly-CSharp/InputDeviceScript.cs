using System;
using UnityEngine;

// Token: 0x02000303 RID: 771
public class InputDeviceScript : MonoBehaviour
{
	// Token: 0x06001770 RID: 6000 RVA: 0x000CA8F4 File Offset: 0x000C8AF4
	private void Update()
	{
		this.MouseDelta = Input.mousePosition - this.MousePrevious;
		this.MousePrevious = Input.mousePosition;
		InputDeviceType type = this.Type;
		if ((Input.GetJoystickNames().Length == 0 && Input.anyKey) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetMouseButton(2) || this.MouseDelta != Vector3.zero)
		{
			this.Type = InputDeviceType.MouseAndKeyboard;
		}
		else
		{
			bool flag = false;
			for (int i = 0; i < 20; i++)
			{
				if (Input.GetKey("joystick 1 button " + i))
				{
					flag = true;
					break;
				}
			}
			bool flag2 = Math.Abs(Input.GetAxis("DpadX")) > 0.5f || Math.Abs(Input.GetAxis("DpadY")) > 0.5f;
			bool flag3 = Mathf.Abs(Input.GetAxis("Vertical")) == 1f || Mathf.Abs(Input.GetAxis("Horizontal")) == 1f;
			if (flag || flag2 || flag3)
			{
				this.Type = InputDeviceType.Gamepad;
			}
		}
		if (this.Type != type)
		{
			PromptSwapScript[] array = Resources.FindObjectsOfTypeAll<PromptSwapScript>();
			for (int j = 0; j < array.Length; j++)
			{
				array[j].UpdateSpriteType(this.Type);
			}
		}
		this.Horizontal = Input.GetAxis("Horizontal");
		this.Vertical = Input.GetAxis("Vertical");
	}

	// Token: 0x040020A4 RID: 8356
	public InputDeviceType Type = InputDeviceType.Gamepad;

	// Token: 0x040020A5 RID: 8357
	public Vector3 MousePrevious;

	// Token: 0x040020A6 RID: 8358
	public Vector3 MouseDelta;

	// Token: 0x040020A7 RID: 8359
	public float Horizontal;

	// Token: 0x040020A8 RID: 8360
	public float Vertical;
}
