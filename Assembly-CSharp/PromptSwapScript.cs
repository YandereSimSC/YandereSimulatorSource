using System;
using UnityEngine;

// Token: 0x02000380 RID: 896
public class PromptSwapScript : MonoBehaviour
{
	// Token: 0x0600195B RID: 6491 RVA: 0x000F4A56 File Offset: 0x000F2C56
	private void Awake()
	{
		if (this.InputDevice == null)
		{
			this.InputDevice = UnityEngine.Object.FindObjectOfType<InputDeviceScript>();
		}
	}

	// Token: 0x0600195C RID: 6492 RVA: 0x000F4A74 File Offset: 0x000F2C74
	public void UpdateSpriteType(InputDeviceType deviceType)
	{
		if (this.InputDevice == null)
		{
			this.InputDevice = UnityEngine.Object.FindObjectOfType<InputDeviceScript>();
		}
		if (deviceType == InputDeviceType.Gamepad)
		{
			this.MySprite.spriteName = this.GamepadName;
			if (this.MyLetter != null)
			{
				this.MyLetter.text = "";
				return;
			}
		}
		else
		{
			this.MySprite.spriteName = this.KeyboardName;
			if (this.MyLetter != null)
			{
				this.MyLetter.text = this.KeyboardLetter;
			}
		}
	}

	// Token: 0x040026CA RID: 9930
	public InputDeviceScript InputDevice;

	// Token: 0x040026CB RID: 9931
	public UISprite MySprite;

	// Token: 0x040026CC RID: 9932
	public UILabel MyLetter;

	// Token: 0x040026CD RID: 9933
	public string KeyboardLetter = string.Empty;

	// Token: 0x040026CE RID: 9934
	public string KeyboardName = string.Empty;

	// Token: 0x040026CF RID: 9935
	public string GamepadName = string.Empty;
}
