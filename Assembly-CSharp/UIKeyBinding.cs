using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000059 RID: 89
[AddComponentMenu("NGUI/Interaction/Key Binding")]
public class UIKeyBinding : MonoBehaviour
{
	// Token: 0x17000028 RID: 40
	// (get) Token: 0x0600022A RID: 554 RVA: 0x0001847C File Offset: 0x0001667C
	public string captionText
	{
		get
		{
			string text = NGUITools.KeyToCaption(this.keyCode);
			if (this.modifier == UIKeyBinding.Modifier.None || this.modifier == UIKeyBinding.Modifier.Any)
			{
				return text;
			}
			return this.modifier + "+" + text;
		}
	}

	// Token: 0x0600022B RID: 555 RVA: 0x000184C0 File Offset: 0x000166C0
	public static bool IsBound(KeyCode key)
	{
		int i = 0;
		int count = UIKeyBinding.list.Count;
		while (i < count)
		{
			UIKeyBinding uikeyBinding = UIKeyBinding.list[i];
			if (uikeyBinding != null && uikeyBinding.keyCode == key)
			{
				return true;
			}
			i++;
		}
		return false;
	}

	// Token: 0x0600022C RID: 556 RVA: 0x00018508 File Offset: 0x00016708
	public static UIKeyBinding Find(string name)
	{
		int i = 0;
		int count = UIKeyBinding.list.Count;
		while (i < count)
		{
			if (UIKeyBinding.list[i].name == name)
			{
				return UIKeyBinding.list[i];
			}
			i++;
		}
		return null;
	}

	// Token: 0x0600022D RID: 557 RVA: 0x00018551 File Offset: 0x00016751
	protected virtual void OnEnable()
	{
		UIKeyBinding.list.Add(this);
	}

	// Token: 0x0600022E RID: 558 RVA: 0x0001855E File Offset: 0x0001675E
	protected virtual void OnDisable()
	{
		UIKeyBinding.list.Remove(this);
	}

	// Token: 0x0600022F RID: 559 RVA: 0x0001856C File Offset: 0x0001676C
	protected virtual void Start()
	{
		UIInput component = base.GetComponent<UIInput>();
		this.mIsInput = (component != null);
		if (component != null)
		{
			EventDelegate.Add(component.onSubmit, new EventDelegate.Callback(this.OnSubmit));
		}
	}

	// Token: 0x06000230 RID: 560 RVA: 0x000185AF File Offset: 0x000167AF
	protected virtual void OnSubmit()
	{
		if (UICamera.currentKey == this.keyCode && this.IsModifierActive())
		{
			this.mIgnoreUp = true;
		}
	}

	// Token: 0x06000231 RID: 561 RVA: 0x000185CD File Offset: 0x000167CD
	protected virtual bool IsModifierActive()
	{
		return UIKeyBinding.IsModifierActive(this.modifier);
	}

	// Token: 0x06000232 RID: 562 RVA: 0x000185DC File Offset: 0x000167DC
	public static bool IsModifierActive(UIKeyBinding.Modifier modifier)
	{
		if (modifier == UIKeyBinding.Modifier.Any)
		{
			return true;
		}
		if (modifier == UIKeyBinding.Modifier.Alt)
		{
			if (UICamera.GetKey(KeyCode.LeftAlt) || UICamera.GetKey(KeyCode.RightAlt))
			{
				return true;
			}
		}
		else if (modifier == UIKeyBinding.Modifier.Ctrl)
		{
			if (UICamera.GetKey(KeyCode.LeftControl) || UICamera.GetKey(KeyCode.RightControl))
			{
				return true;
			}
		}
		else if (modifier == UIKeyBinding.Modifier.Shift)
		{
			if (UICamera.GetKey(KeyCode.LeftShift) || UICamera.GetKey(KeyCode.RightShift))
			{
				return true;
			}
		}
		else if (modifier == UIKeyBinding.Modifier.None)
		{
			return !UICamera.GetKey(KeyCode.LeftAlt) && !UICamera.GetKey(KeyCode.RightAlt) && !UICamera.GetKey(KeyCode.LeftControl) && !UICamera.GetKey(KeyCode.RightControl) && !UICamera.GetKey(KeyCode.LeftShift) && !UICamera.GetKey(KeyCode.RightShift);
		}
		return false;
	}

	// Token: 0x06000233 RID: 563 RVA: 0x000186DC File Offset: 0x000168DC
	protected virtual void Update()
	{
		if (this.keyCode != KeyCode.Numlock && UICamera.inputHasFocus)
		{
			return;
		}
		if (this.keyCode == KeyCode.None || !this.IsModifierActive())
		{
			return;
		}
		bool flag = UICamera.GetKeyDown(this.keyCode);
		bool flag2 = UICamera.GetKeyUp(this.keyCode);
		if (flag)
		{
			this.mPress = true;
		}
		if (this.action == UIKeyBinding.Action.PressAndClick || this.action == UIKeyBinding.Action.All)
		{
			if (flag)
			{
				UICamera.currentTouchID = -1;
				UICamera.currentKey = this.keyCode;
				this.OnBindingPress(true);
			}
			if (this.mPress && flag2)
			{
				UICamera.currentTouchID = -1;
				UICamera.currentKey = this.keyCode;
				this.OnBindingPress(false);
				this.OnBindingClick();
			}
		}
		if ((this.action == UIKeyBinding.Action.Select || this.action == UIKeyBinding.Action.All) && flag2)
		{
			if (this.mIsInput)
			{
				if (!this.mIgnoreUp && (this.keyCode == KeyCode.Numlock || !UICamera.inputHasFocus) && this.mPress)
				{
					UICamera.selectedObject = base.gameObject;
				}
				this.mIgnoreUp = false;
			}
			else if (this.mPress)
			{
				UICamera.hoveredObject = base.gameObject;
			}
		}
		if (flag2)
		{
			this.mPress = false;
		}
	}

	// Token: 0x06000234 RID: 564 RVA: 0x00018801 File Offset: 0x00016A01
	protected virtual void OnBindingPress(bool pressed)
	{
		UICamera.Notify(base.gameObject, "OnPress", pressed);
	}

	// Token: 0x06000235 RID: 565 RVA: 0x00018819 File Offset: 0x00016A19
	protected virtual void OnBindingClick()
	{
		UICamera.Notify(base.gameObject, "OnClick", null);
	}

	// Token: 0x06000236 RID: 566 RVA: 0x0001882C File Offset: 0x00016A2C
	public override string ToString()
	{
		return UIKeyBinding.GetString(this.keyCode, this.modifier);
	}

	// Token: 0x06000237 RID: 567 RVA: 0x0001883F File Offset: 0x00016A3F
	public static string GetString(KeyCode keyCode, UIKeyBinding.Modifier modifier)
	{
		if (modifier == UIKeyBinding.Modifier.None)
		{
			return NGUITools.KeyToCaption(keyCode);
		}
		return modifier + "+" + NGUITools.KeyToCaption(keyCode);
	}

	// Token: 0x06000238 RID: 568 RVA: 0x00018864 File Offset: 0x00016A64
	public static bool GetKeyCode(string text, out KeyCode key, out UIKeyBinding.Modifier modifier)
	{
		key = KeyCode.None;
		modifier = UIKeyBinding.Modifier.None;
		if (string.IsNullOrEmpty(text))
		{
			return true;
		}
		if (text.Length > 2 && text.Contains("+") && text[text.Length - 1] != '+')
		{
			string[] array = text.Split(new char[]
			{
				'+'
			}, 2);
			key = NGUITools.CaptionToKey(array[1]);
			try
			{
				modifier = (UIKeyBinding.Modifier)Enum.Parse(typeof(UIKeyBinding.Modifier), array[0]);
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
		modifier = UIKeyBinding.Modifier.None;
		key = NGUITools.CaptionToKey(text);
		return true;
	}

	// Token: 0x06000239 RID: 569 RVA: 0x00018904 File Offset: 0x00016B04
	public static UIKeyBinding.Modifier GetActiveModifier()
	{
		UIKeyBinding.Modifier result = UIKeyBinding.Modifier.None;
		if (UICamera.GetKey(KeyCode.LeftAlt) || UICamera.GetKey(KeyCode.RightAlt))
		{
			result = UIKeyBinding.Modifier.Alt;
		}
		else if (UICamera.GetKey(KeyCode.LeftShift) || UICamera.GetKey(KeyCode.RightShift))
		{
			result = UIKeyBinding.Modifier.Shift;
		}
		else if (UICamera.GetKey(KeyCode.LeftControl) || UICamera.GetKey(KeyCode.RightControl))
		{
			result = UIKeyBinding.Modifier.Ctrl;
		}
		return result;
	}

	// Token: 0x040003AB RID: 939
	public static List<UIKeyBinding> list = new List<UIKeyBinding>();

	// Token: 0x040003AC RID: 940
	public KeyCode keyCode;

	// Token: 0x040003AD RID: 941
	public UIKeyBinding.Modifier modifier;

	// Token: 0x040003AE RID: 942
	public UIKeyBinding.Action action;

	// Token: 0x040003AF RID: 943
	[NonSerialized]
	private bool mIgnoreUp;

	// Token: 0x040003B0 RID: 944
	[NonSerialized]
	private bool mIsInput;

	// Token: 0x040003B1 RID: 945
	[NonSerialized]
	private bool mPress;

	// Token: 0x02000618 RID: 1560
	[DoNotObfuscateNGUI]
	public enum Action
	{
		// Token: 0x0400449E RID: 17566
		PressAndClick,
		// Token: 0x0400449F RID: 17567
		Select,
		// Token: 0x040044A0 RID: 17568
		All
	}

	// Token: 0x02000619 RID: 1561
	[DoNotObfuscateNGUI]
	public enum Modifier
	{
		// Token: 0x040044A2 RID: 17570
		Any,
		// Token: 0x040044A3 RID: 17571
		Shift,
		// Token: 0x040044A4 RID: 17572
		Ctrl,
		// Token: 0x040044A5 RID: 17573
		Alt,
		// Token: 0x040044A6 RID: 17574
		None
	}
}
