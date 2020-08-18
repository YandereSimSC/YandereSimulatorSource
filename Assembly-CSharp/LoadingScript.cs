using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000320 RID: 800
public class LoadingScript : MonoBehaviour
{
	// Token: 0x060017F3 RID: 6131 RVA: 0x000D3DD9 File Offset: 0x000D1FD9
	private void Start()
	{
		SceneManager.LoadScene("SchoolScene");
	}
}
