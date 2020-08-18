using System;
using System.Reflection;
using UnityEngine;

// Token: 0x02000499 RID: 1177
public class ScriptSetter : MonoBehaviour
{
	// Token: 0x06001E15 RID: 7701 RVA: 0x0017848C File Offset: 0x0017668C
	private void Start()
	{
		foreach (Component component in base.GetComponents(typeof(Component)))
		{
			Debug.Log(string.Concat(new object[]
			{
				"name ",
				component.name,
				" type ",
				component.GetType(),
				" basetype ",
				component.GetType().BaseType
			}));
			foreach (FieldInfo fieldInfo in component.GetType().GetFields())
			{
				object obj = component;
				Debug.Log(fieldInfo.Name + " value is: " + fieldInfo.GetValue(obj));
			}
		}
	}

	// Token: 0x04003BE8 RID: 15336
	public StudentScript OldStudent;

	// Token: 0x04003BE9 RID: 15337
	public StudentScript NewStudent;
}
