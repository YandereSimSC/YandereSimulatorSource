using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000075 RID: 117
[AddComponentMenu("NGUI/Internal/Debug")]
public class NGUIDebug : MonoBehaviour
{
	// Token: 0x17000071 RID: 113
	// (get) Token: 0x060003C2 RID: 962 RVA: 0x00022AF2 File Offset: 0x00020CF2
	// (set) Token: 0x060003C3 RID: 963 RVA: 0x00022AF9 File Offset: 0x00020CF9
	public static bool debugRaycast
	{
		get
		{
			return NGUIDebug.mRayDebug;
		}
		set
		{
			NGUIDebug.mRayDebug = value;
			if (value && Application.isPlaying)
			{
				NGUIDebug.CreateInstance();
			}
		}
	}

	// Token: 0x060003C4 RID: 964 RVA: 0x00022B10 File Offset: 0x00020D10
	public static void CreateInstance()
	{
		if (NGUIDebug.mInstance == null)
		{
			GameObject gameObject = new GameObject("_NGUI Debug");
			NGUIDebug.mInstance = gameObject.AddComponent<NGUIDebug>();
			UnityEngine.Object.DontDestroyOnLoad(gameObject);
		}
	}

	// Token: 0x060003C5 RID: 965 RVA: 0x00022B39 File Offset: 0x00020D39
	private static void LogString(string text)
	{
		if (Application.isPlaying)
		{
			if (NGUIDebug.mLines.Count > 20)
			{
				NGUIDebug.mLines.RemoveAt(0);
			}
			NGUIDebug.mLines.Add(text);
			NGUIDebug.CreateInstance();
			return;
		}
		Debug.Log(text);
	}

	// Token: 0x060003C6 RID: 966 RVA: 0x00022B74 File Offset: 0x00020D74
	public static void Log(params object[] objs)
	{
		string text = "";
		for (int i = 0; i < objs.Length; i++)
		{
			if (i == 0)
			{
				text += objs[i].ToString();
			}
			else
			{
				text = text + ", " + objs[i].ToString();
			}
		}
		NGUIDebug.LogString(text);
	}

	// Token: 0x060003C7 RID: 967 RVA: 0x00022BC4 File Offset: 0x00020DC4
	public static void Log(string s)
	{
		if (!string.IsNullOrEmpty(s))
		{
			string[] array = s.Split(new char[]
			{
				'\n'
			});
			for (int i = 0; i < array.Length; i++)
			{
				NGUIDebug.LogString(array[i]);
			}
		}
	}

	// Token: 0x060003C8 RID: 968 RVA: 0x00022C01 File Offset: 0x00020E01
	public static void Clear()
	{
		NGUIDebug.mLines.Clear();
	}

	// Token: 0x060003C9 RID: 969 RVA: 0x00022C10 File Offset: 0x00020E10
	public static void DrawBounds(Bounds b)
	{
		Vector3 center = b.center;
		Vector3 vector = b.center - b.extents;
		Vector3 vector2 = b.center + b.extents;
		Debug.DrawLine(new Vector3(vector.x, vector.y, center.z), new Vector3(vector2.x, vector.y, center.z), Color.red);
		Debug.DrawLine(new Vector3(vector.x, vector.y, center.z), new Vector3(vector.x, vector2.y, center.z), Color.red);
		Debug.DrawLine(new Vector3(vector2.x, vector.y, center.z), new Vector3(vector2.x, vector2.y, center.z), Color.red);
		Debug.DrawLine(new Vector3(vector.x, vector2.y, center.z), new Vector3(vector2.x, vector2.y, center.z), Color.red);
	}

	// Token: 0x060003CA RID: 970 RVA: 0x00022D30 File Offset: 0x00020F30
	private void OnGUI()
	{
		Rect position = new Rect(5f, 5f, 1000f, 22f);
		if (NGUIDebug.mRayDebug)
		{
			UICamera.ControlScheme currentScheme = UICamera.currentScheme;
			string text = "Scheme: " + currentScheme;
			GUI.color = Color.black;
			GUI.Label(position, text);
			position.y -= 1f;
			position.x -= 1f;
			GUI.color = Color.white;
			GUI.Label(position, text);
			position.y += 18f;
			position.x += 1f;
			text = "Hover: " + NGUITools.GetHierarchy(UICamera.hoveredObject).Replace("\"", "");
			GUI.color = Color.black;
			GUI.Label(position, text);
			position.y -= 1f;
			position.x -= 1f;
			GUI.color = Color.white;
			GUI.Label(position, text);
			position.y += 18f;
			position.x += 1f;
			text = "Selection: " + NGUITools.GetHierarchy(UICamera.selectedObject).Replace("\"", "");
			GUI.color = Color.black;
			GUI.Label(position, text);
			position.y -= 1f;
			position.x -= 1f;
			GUI.color = Color.white;
			GUI.Label(position, text);
			position.y += 18f;
			position.x += 1f;
			text = "Controller: " + NGUITools.GetHierarchy(UICamera.controllerNavigationObject).Replace("\"", "");
			GUI.color = Color.black;
			GUI.Label(position, text);
			position.y -= 1f;
			position.x -= 1f;
			GUI.color = Color.white;
			GUI.Label(position, text);
			position.y += 18f;
			position.x += 1f;
			text = "Active events: " + UICamera.CountInputSources();
			if (UICamera.disableController)
			{
				text += ", disabled controller";
			}
			if (UICamera.ignoreControllerInput)
			{
				text += ", ignore controller";
			}
			if (UICamera.inputHasFocus)
			{
				text += ", input focus";
			}
			GUI.color = Color.black;
			GUI.Label(position, text);
			position.y -= 1f;
			position.x -= 1f;
			GUI.color = Color.white;
			GUI.Label(position, text);
			position.y += 18f;
			position.x += 1f;
		}
		int i = 0;
		int count = NGUIDebug.mLines.Count;
		while (i < count)
		{
			GUI.color = Color.black;
			GUI.Label(position, NGUIDebug.mLines[i]);
			position.y -= 1f;
			position.x -= 1f;
			GUI.color = Color.white;
			GUI.Label(position, NGUIDebug.mLines[i]);
			position.y += 18f;
			position.x += 1f;
			i++;
		}
	}

	// Token: 0x040004CF RID: 1231
	private static bool mRayDebug = false;

	// Token: 0x040004D0 RID: 1232
	private static List<string> mLines = new List<string>();

	// Token: 0x040004D1 RID: 1233
	private static NGUIDebug mInstance = null;
}
