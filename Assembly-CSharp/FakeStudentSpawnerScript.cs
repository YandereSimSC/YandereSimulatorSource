using System;
using UnityEngine;

// Token: 0x0200029B RID: 667
public class FakeStudentSpawnerScript : MonoBehaviour
{
	// Token: 0x060013F6 RID: 5110 RVA: 0x000AE114 File Offset: 0x000AC314
	public void Spawn()
	{
		if (!this.AlreadySpawned)
		{
			this.Student = this.FakeFemale;
			this.NESW = 1;
			while (this.Spawned < 100)
			{
				if (this.NESW == 1)
				{
					this.NewStudent = UnityEngine.Object.Instantiate<GameObject>(this.Student, new Vector3(UnityEngine.Random.Range(-21f, 21f), (float)this.Height, UnityEngine.Random.Range(21f, 19f)), Quaternion.identity);
				}
				else if (this.NESW == 2)
				{
					this.NewStudent = UnityEngine.Object.Instantiate<GameObject>(this.Student, new Vector3(UnityEngine.Random.Range(19f, 21f), (float)this.Height, UnityEngine.Random.Range(29f, -37f)), Quaternion.identity);
				}
				else if (this.NESW == 3)
				{
					this.NewStudent = UnityEngine.Object.Instantiate<GameObject>(this.Student, new Vector3(UnityEngine.Random.Range(-21f, 21f), (float)this.Height, UnityEngine.Random.Range(-21f, -19f)), Quaternion.identity);
				}
				else if (this.NESW == 4)
				{
					this.NewStudent = UnityEngine.Object.Instantiate<GameObject>(this.Student, new Vector3(UnityEngine.Random.Range(-19f, -21f), (float)this.Height, UnityEngine.Random.Range(29f, -37f)), Quaternion.identity);
				}
				this.StudentID++;
				this.NewStudent.GetComponent<PlaceholderStudentScript>().FakeStudentSpawner = this;
				this.NewStudent.GetComponent<PlaceholderStudentScript>().StudentID = this.StudentID;
				this.NewStudent.GetComponent<PlaceholderStudentScript>().NESW = this.NESW;
				this.NewStudent.transform.parent = this.FakeStudentParent;
				this.CurrentFloor++;
				this.CurrentRow++;
				this.Spawned++;
				if (this.CurrentFloor == this.FloorLimit)
				{
					this.CurrentFloor = 0;
					this.Height += 4;
				}
				if (this.CurrentRow == this.RowLimit)
				{
					this.CurrentRow = 0;
					this.NESW++;
					if (this.NESW > 4)
					{
						this.NESW = 1;
					}
				}
				this.Student = ((this.Student == this.FakeFemale) ? this.FakeMale : this.FakeFemale);
			}
			this.StudentIDLimit = this.StudentID;
			this.StudentID = 1;
			this.AlreadySpawned = true;
			return;
		}
		this.FakeStudentParent.gameObject.SetActive(!this.FakeStudentParent.gameObject.activeInHierarchy);
	}

	// Token: 0x04001BEA RID: 7146
	public Transform FakeStudentParent;

	// Token: 0x04001BEB RID: 7147
	public GameObject NewStudent;

	// Token: 0x04001BEC RID: 7148
	public GameObject FakeFemale;

	// Token: 0x04001BED RID: 7149
	public GameObject FakeMale;

	// Token: 0x04001BEE RID: 7150
	public GameObject Student;

	// Token: 0x04001BEF RID: 7151
	public bool AlreadySpawned;

	// Token: 0x04001BF0 RID: 7152
	public int CurrentFloor;

	// Token: 0x04001BF1 RID: 7153
	public int CurrentRow;

	// Token: 0x04001BF2 RID: 7154
	public int FloorLimit;

	// Token: 0x04001BF3 RID: 7155
	public int RowLimit;

	// Token: 0x04001BF4 RID: 7156
	public int StudentIDLimit;

	// Token: 0x04001BF5 RID: 7157
	public int StudentID;

	// Token: 0x04001BF6 RID: 7158
	public int Spawned;

	// Token: 0x04001BF7 RID: 7159
	public int Height;

	// Token: 0x04001BF8 RID: 7160
	public int NESW;

	// Token: 0x04001BF9 RID: 7161
	public int ID;

	// Token: 0x04001BFA RID: 7162
	public GameObject[] SuspiciousObjects;
}
