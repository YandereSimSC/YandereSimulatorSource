using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000033 RID: 51
[AddComponentMenu("NGUI/Examples/Load Level On Click")]
public class LoadLevelOnClick : MonoBehaviour
{
	// Token: 0x06000127 RID: 295 RVA: 0x00012DCE File Offset: 0x00010FCE
	private void OnClick()
	{
		if (!string.IsNullOrEmpty(this.levelName))
		{
			SceneManager.LoadScene(this.levelName);
		}
	}

	// Token: 0x040002B8 RID: 696
	public string levelName;
}
