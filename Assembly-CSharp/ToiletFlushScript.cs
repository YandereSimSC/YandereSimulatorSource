using System;
using System.Linq;
using UnityEngine;

// Token: 0x02000428 RID: 1064
internal class ToiletFlushScript : MonoBehaviour
{
	// Token: 0x06001C4E RID: 7246 RVA: 0x00152B65 File Offset: 0x00150D65
	private void Start()
	{
		this.StudentManager = UnityEngine.Object.FindObjectOfType<StudentManagerScript>();
		this.Toilet = this.StudentManager.Students[11].gameObject;
		this.toilet = this.Toilet;
	}

	// Token: 0x06001C4F RID: 7247 RVA: 0x00152B97 File Offset: 0x00150D97
	private void Update()
	{
		this.Flush(this.toilet);
	}

	// Token: 0x06001C50 RID: 7248 RVA: 0x00152BA8 File Offset: 0x00150DA8
	private void Flush(GameObject toilet)
	{
		if (this.Toilet != null)
		{
			this.Toilet = null;
		}
		if (toilet.activeInHierarchy)
		{
			int length = UnityEngine.Random.Range(1, 15);
			toilet.name = this.RandomSound(length);
			base.name = this.RandomSound(length);
			toilet.SetActive(false);
		}
	}

	// Token: 0x06001C51 RID: 7249 RVA: 0x00152BFC File Offset: 0x00150DFC
	private string RandomSound(int Length)
	{
		return new string((from s in Enumerable.Repeat<string>("ABCDEFGHIJKLMNOPQRSTUVWXYZ", Length)
		select s[ToiletFlushScript.random.Next(s.Length)]).ToArray<char>());
	}

	// Token: 0x0400352B RID: 13611
	[Header("=== Toilet Related ===")]
	public GameObject Toilet;

	// Token: 0x0400352C RID: 13612
	private GameObject toilet;

	// Token: 0x0400352D RID: 13613
	private static System.Random random = new System.Random();

	// Token: 0x0400352E RID: 13614
	private StudentManagerScript StudentManager;
}
