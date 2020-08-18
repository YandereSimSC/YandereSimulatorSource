using System;
using UnityEngine;

// Token: 0x020003E3 RID: 995
public class SmokeBombScript : MonoBehaviour
{
	// Token: 0x06001AA5 RID: 6821 RVA: 0x00109CBC File Offset: 0x00107EBC
	private void Update()
	{
		this.Timer += Time.deltaTime;
		if (this.Timer > 15f)
		{
			if (!this.Stink)
			{
				foreach (StudentScript studentScript in this.Students)
				{
					if (studentScript != null)
					{
						studentScript.Blind = false;
					}
				}
			}
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06001AA6 RID: 6822 RVA: 0x00109D24 File Offset: 0x00107F24
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == 9)
		{
			StudentScript component = other.gameObject.GetComponent<StudentScript>();
			if (component != null)
			{
				if (this.Stink)
				{
					this.GoAway(component);
					return;
				}
				if (this.Amnesia && !component.Chasing)
				{
					component.ReturnToNormal();
				}
				this.Students[this.ID] = component;
				component.Blind = true;
				this.ID++;
			}
		}
	}

	// Token: 0x06001AA7 RID: 6823 RVA: 0x00109DA0 File Offset: 0x00107FA0
	private void OnTriggerStay(Collider other)
	{
		if (this.Stink)
		{
			if (other.gameObject.layer == 9)
			{
				StudentScript component = other.gameObject.GetComponent<StudentScript>();
				if (component != null)
				{
					this.GoAway(component);
					return;
				}
			}
		}
		else if (this.Amnesia && other.gameObject.layer == 9)
		{
			StudentScript component2 = other.gameObject.GetComponent<StudentScript>();
			if (component2 != null && component2.Alarmed && !component2.Chasing)
			{
				component2.ReturnToNormal();
			}
		}
	}

	// Token: 0x06001AA8 RID: 6824 RVA: 0x00109E24 File Offset: 0x00108024
	private void OnTriggerExit(Collider other)
	{
		if (!this.Stink && !this.Amnesia && other.gameObject.layer == 9)
		{
			StudentScript component = other.gameObject.GetComponent<StudentScript>();
			if (component != null)
			{
				Debug.Log(component.Name + " left a smoke cloud and stopped being blind.");
				component.Blind = false;
			}
		}
	}

	// Token: 0x06001AA9 RID: 6825 RVA: 0x00109E84 File Offset: 0x00108084
	private void GoAway(StudentScript Student)
	{
		if (!Student.Chasing)
		{
			Student.BecomeAlarmed();
			Student.CurrentDestination = Student.StudentManager.GoAwaySpots.List[Student.StudentID];
			Student.Pathfinding.target = Student.StudentManager.GoAwaySpots.List[Student.StudentID];
			Student.Pathfinding.canSearch = true;
			Student.Pathfinding.canMove = true;
			Student.CharacterAnimation.CrossFade(Student.SprintAnim);
			Student.DistanceToDestination = 100f;
			Student.Pathfinding.speed = 4f;
			Student.AmnesiaTimer = 10f;
			Student.Distracted = true;
			Student.Alarmed = false;
			Student.Routine = false;
			Student.GoAway = true;
		}
	}

	// Token: 0x04002ADA RID: 10970
	public StudentScript[] Students;

	// Token: 0x04002ADB RID: 10971
	public float Timer;

	// Token: 0x04002ADC RID: 10972
	public bool Amnesia;

	// Token: 0x04002ADD RID: 10973
	public bool Stink;

	// Token: 0x04002ADE RID: 10974
	public int ID;
}
