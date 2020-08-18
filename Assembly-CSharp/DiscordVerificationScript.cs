using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x0200026A RID: 618
public class DiscordVerificationScript : MonoBehaviour
{
	// Token: 0x06001349 RID: 4937 RVA: 0x000A419F File Offset: 0x000A239F
	private void Update()
	{
		if (Input.GetKeyDown("q"))
		{
			SceneManager.LoadScene("MissionModeScene");
		}
	}
}
