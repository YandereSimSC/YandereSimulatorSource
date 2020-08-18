using System;
using UnityEngine;

// Token: 0x02000366 RID: 870
public class PlaceholderStudentScript : MonoBehaviour
{
	// Token: 0x060018FA RID: 6394 RVA: 0x000E9E30 File Offset: 0x000E8030
	private void Start()
	{
		this.Target = UnityEngine.Object.Instantiate<GameObject>(this.EmptyGameObject).transform;
		this.ChooseNewDestination();
	}

	// Token: 0x060018FB RID: 6395 RVA: 0x000E9E50 File Offset: 0x000E8050
	private void Update()
	{
		base.transform.LookAt(this.Target.position);
		base.transform.position = Vector3.MoveTowards(base.transform.position, this.Target.position, Time.deltaTime);
		if (Vector3.Distance(base.transform.position, this.Target.position) < 1f)
		{
			this.ChooseNewDestination();
		}
		if (Input.GetKeyDown("space"))
		{
			if (!this.ShootRaycasts)
			{
				this.ShootRaycasts = true;
			}
			else
			{
				this.Phase++;
			}
		}
		if (base.transform.position.y < 1f && this.ShootRaycasts)
		{
			if (this.Phase == 0)
			{
				Debug.DrawLine(this.Eyes.position, this.FakeStudentSpawner.SuspiciousObjects[0].transform.position, Color.red);
				Debug.DrawLine(this.Eyes.position, this.FakeStudentSpawner.SuspiciousObjects[1].transform.position, Color.red);
				Debug.DrawLine(this.Eyes.position, this.FakeStudentSpawner.SuspiciousObjects[2].transform.position, Color.red);
				return;
			}
			if (this.StudentID < this.FakeStudentSpawner.StudentID + 5 && this.StudentID > this.FakeStudentSpawner.StudentID - 5)
			{
				if (Vector3.Distance(base.transform.position, this.FakeStudentSpawner.SuspiciousObjects[0].transform.position) < 5f)
				{
					Debug.DrawLine(this.Eyes.position, this.FakeStudentSpawner.SuspiciousObjects[0].transform.position, Color.red);
				}
				if (Vector3.Distance(base.transform.position, this.FakeStudentSpawner.SuspiciousObjects[1].transform.position) < 5f)
				{
					Debug.DrawLine(this.Eyes.position, this.FakeStudentSpawner.SuspiciousObjects[1].transform.position, Color.red);
				}
				if (Vector3.Distance(base.transform.position, this.FakeStudentSpawner.SuspiciousObjects[2].transform.position) < 5f)
				{
					Debug.DrawLine(this.Eyes.position, this.FakeStudentSpawner.SuspiciousObjects[2].transform.position, Color.red);
				}
			}
		}
	}

	// Token: 0x060018FC RID: 6396 RVA: 0x000EA0E4 File Offset: 0x000E82E4
	private void ChooseNewDestination()
	{
		if (this.NESW == 1)
		{
			this.Target.position = new Vector3(UnityEngine.Random.Range(-21f, 21f), base.transform.position.y, UnityEngine.Random.Range(21f, 19f));
			return;
		}
		if (this.NESW == 2)
		{
			this.Target.position = new Vector3(UnityEngine.Random.Range(19f, 21f), base.transform.position.y, UnityEngine.Random.Range(29f, -37f));
			return;
		}
		if (this.NESW == 3)
		{
			this.Target.position = new Vector3(UnityEngine.Random.Range(-21f, 21f), base.transform.position.y, UnityEngine.Random.Range(-21f, -19f));
			return;
		}
		if (this.NESW == 4)
		{
			this.Target.position = new Vector3(UnityEngine.Random.Range(-19f, -21f), base.transform.position.y, UnityEngine.Random.Range(29f, -37f));
		}
	}

	// Token: 0x04002582 RID: 9602
	public FakeStudentSpawnerScript FakeStudentSpawner;

	// Token: 0x04002583 RID: 9603
	public GameObject EmptyGameObject;

	// Token: 0x04002584 RID: 9604
	public bool ShootRaycasts;

	// Token: 0x04002585 RID: 9605
	public Transform Target;

	// Token: 0x04002586 RID: 9606
	public Transform Eyes;

	// Token: 0x04002587 RID: 9607
	public int StudentID;

	// Token: 0x04002588 RID: 9608
	public int Phase;

	// Token: 0x04002589 RID: 9609
	public int NESW;
}
