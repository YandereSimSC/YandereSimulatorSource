using System;
using UnityEngine;

// Token: 0x02000363 RID: 867
public class PickUpScript : MonoBehaviour
{
	// Token: 0x060018EB RID: 6379 RVA: 0x000E8484 File Offset: 0x000E6684
	private void Start()
	{
		this.Yandere = GameObject.Find("YandereChan").GetComponent<YandereScript>();
		this.Clock = GameObject.Find("Clock").GetComponent<ClockScript>();
		if (!this.CanCollide)
		{
			Physics.IgnoreCollision(this.Yandere.GetComponent<Collider>(), this.MyCollider);
		}
		if (this.Outline.Length != 0)
		{
			this.OriginalColor = this.Outline[0].color;
		}
		this.OriginalScale = base.transform.localScale;
		if (this.MyRigidbody == null)
		{
			this.MyRigidbody = base.GetComponent<Rigidbody>();
		}
		if (this.DisableAtStart)
		{
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x060018EC RID: 6380 RVA: 0x000E8534 File Offset: 0x000E6734
	private void LateUpdate()
	{
		if (this.CleaningProduct)
		{
			if (this.Clock.Period == 5)
			{
				this.Suspicious = false;
			}
			else
			{
				this.Suspicious = true;
			}
		}
		if (this.Weight)
		{
			if (this.Period < this.Clock.Period)
			{
				this.Strength = ClassGlobals.PhysicalGrade + ClassGlobals.PhysicalBonus;
				this.Period++;
			}
			if (this.Strength == 0)
			{
				this.Prompt.Label[3].text = "     Physical Stat Too Low";
				this.Prompt.Circle[3].fillAmount = 1f;
			}
			else
			{
				this.Prompt.Label[3].text = "     Carry";
			}
		}
		if (this.Prompt.Circle[3].fillAmount == 0f)
		{
			this.Prompt.Circle[3].fillAmount = 1f;
			if (this.Weight)
			{
				if (!this.Yandere.Chased && this.Yandere.Chasers == 0)
				{
					if (this.Yandere.PickUp != null)
					{
						this.Yandere.CharacterAnimation[this.Yandere.CarryAnims[this.Yandere.PickUp.CarryAnimID]].weight = 0f;
					}
					if (this.Yandere.Armed)
					{
						this.Yandere.CharacterAnimation[this.Yandere.ArmedAnims[this.Yandere.EquippedWeapon.AnimID]].weight = 0f;
					}
					this.Yandere.targetRotation = Quaternion.LookRotation(new Vector3(base.transform.position.x, this.Yandere.transform.position.y, base.transform.position.z) - this.Yandere.transform.position);
					this.Yandere.transform.rotation = this.Yandere.targetRotation;
					this.Yandere.EmptyHands();
					base.transform.parent = this.Yandere.transform;
					base.transform.localPosition = new Vector3(0f, 0f, 0.79184f);
					base.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
					this.Yandere.Character.GetComponent<Animation>().Play("f02_heavyWeightLift_00");
					this.Yandere.HeavyWeight = true;
					this.Yandere.CanMove = false;
					this.Yandere.Lifting = true;
					this.MyAnimation.Play("Weight_liftUp_00");
					this.MyRigidbody.isKinematic = true;
					this.BeingLifted = true;
				}
			}
			else
			{
				this.BePickedUp();
			}
		}
		if (this.Yandere.PickUp == this)
		{
			base.transform.localPosition = this.HoldPosition;
			base.transform.localEulerAngles = this.HoldRotation;
			if (this.Garbage && !this.Yandere.StudentManager.IncineratorArea.bounds.Contains(this.Yandere.transform.position))
			{
				this.Drop();
				base.transform.position = new Vector3(-40f, 0f, 24f);
			}
		}
		if (this.Dumped)
		{
			this.DumpTimer += Time.deltaTime;
			if (this.DumpTimer > 1f)
			{
				if (this.Clothing)
				{
					this.Yandere.Incinerator.BloodyClothing++;
				}
				else if (this.BodyPart)
				{
					this.Yandere.Incinerator.BodyParts++;
				}
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}
		if (this.Yandere.PickUp != this && !this.MyRigidbody.isKinematic)
		{
			this.KinematicTimer = Mathf.MoveTowards(this.KinematicTimer, 5f, Time.deltaTime);
			if (this.KinematicTimer == 5f)
			{
				this.MyRigidbody.isKinematic = true;
				this.KinematicTimer = 0f;
			}
			if (base.transform.position.x > -71f && base.transform.position.x < -61f && base.transform.position.z > -37.5f && base.transform.position.z < -27.5f)
			{
				base.transform.position = new Vector3(-63f, 1f, -26.5f);
				this.KinematicTimer = 0f;
			}
			if (base.transform.position.x > -46f && base.transform.position.x < -18f && base.transform.position.z > 66f && base.transform.position.z < 78f)
			{
				base.transform.position = new Vector3(-16f, 5f, 72f);
				this.KinematicTimer = 0f;
			}
		}
		if (this.Weight && this.BeingLifted)
		{
			if (this.Yandere.Lifting)
			{
				if (this.Yandere.StudentManager.Stop)
				{
					this.Drop();
					return;
				}
			}
			else
			{
				this.BePickedUp();
			}
		}
	}

	// Token: 0x060018ED RID: 6381 RVA: 0x000E8AD8 File Offset: 0x000E6CD8
	public void BePickedUp()
	{
		if (this.Radio && SchemeGlobals.GetSchemeStage(5) == 2)
		{
			SchemeGlobals.SetSchemeStage(5, 3);
			this.Yandere.PauseScreen.Schemes.UpdateInstructions();
		}
		if (this.Salty && SchemeGlobals.GetSchemeStage(4) == 4)
		{
			SchemeGlobals.SetSchemeStage(4, 5);
			this.Yandere.PauseScreen.Schemes.UpdateInstructions();
		}
		if (this.CarryAnimID == 10)
		{
			this.MyRenderer.mesh = this.OpenBook;
			this.Yandere.LifeNotePen.SetActive(true);
		}
		if (this.MyAnimation != null)
		{
			this.MyAnimation.Stop();
		}
		this.Prompt.Circle[3].fillAmount = 1f;
		this.BeingLifted = false;
		if (this.Yandere.PickUp != null)
		{
			this.Yandere.PickUp.Drop();
		}
		if (this.Yandere.Equipped == 3)
		{
			this.Yandere.Weapon[3].Drop();
		}
		else if (this.Yandere.Equipped > 0)
		{
			this.Yandere.Unequip();
		}
		if (this.Yandere.Dragging)
		{
			this.Yandere.Ragdoll.GetComponent<RagdollScript>().StopDragging();
		}
		if (this.Yandere.Carrying)
		{
			this.Yandere.StopCarrying();
		}
		if (!this.LeftHand)
		{
			base.transform.parent = this.Yandere.ItemParent;
		}
		else
		{
			base.transform.parent = this.Yandere.LeftItemParent;
		}
		if (base.GetComponent<RadioScript>() != null && base.GetComponent<RadioScript>().On)
		{
			base.GetComponent<RadioScript>().TurnOff();
		}
		this.MyCollider.enabled = false;
		if (this.MyRigidbody != null)
		{
			this.MyRigidbody.constraints = RigidbodyConstraints.FreezeAll;
		}
		if (!this.Usable)
		{
			this.Prompt.Hide();
			this.Prompt.enabled = false;
			this.Yandere.NearestPrompt = null;
		}
		else
		{
			this.Prompt.Carried = true;
		}
		this.Yandere.PickUp = this;
		this.Yandere.CarryAnimID = this.CarryAnimID;
		OutlineScript[] outline = this.Outline;
		for (int i = 0; i < outline.Length; i++)
		{
			outline[i].color = new Color(0f, 0f, 0f, 1f);
		}
		if (this.BodyPart)
		{
			this.Yandere.NearBodies++;
		}
		this.Yandere.StudentManager.UpdateStudents(0);
		this.MyRigidbody.isKinematic = true;
		this.KinematicTimer = 0f;
	}

	// Token: 0x060018EE RID: 6382 RVA: 0x000E8D94 File Offset: 0x000E6F94
	public void Drop()
	{
		if (this.Salty && SchemeGlobals.GetSchemeStage(4) == 5)
		{
			SchemeGlobals.SetSchemeStage(4, 4);
			this.Yandere.PauseScreen.Schemes.UpdateInstructions();
		}
		if (this.TrashCan)
		{
			this.Yandere.MyController.radius = 0.2f;
		}
		if (this.CarryAnimID == 10)
		{
			this.MyRenderer.mesh = this.ClosedBook;
			this.Yandere.LifeNotePen.SetActive(false);
		}
		if (this.Weight)
		{
			this.Yandere.IdleAnim = this.Yandere.OriginalIdleAnim;
			this.Yandere.WalkAnim = this.Yandere.OriginalWalkAnim;
			this.Yandere.RunAnim = this.Yandere.OriginalRunAnim;
		}
		if (this.BloodCleaner != null)
		{
			this.BloodCleaner.enabled = true;
			this.BloodCleaner.Pathfinding.enabled = true;
		}
		this.Yandere.PickUp = null;
		if (this.BodyPart)
		{
			base.transform.parent = this.Yandere.LimbParent;
		}
		else
		{
			base.transform.parent = null;
		}
		if (this.LockRotation)
		{
			base.transform.localEulerAngles = new Vector3(0f, base.transform.localEulerAngles.y, 0f);
		}
		if (this.MyRigidbody != null)
		{
			this.MyRigidbody.constraints = this.OriginalConstraints;
			this.MyRigidbody.isKinematic = false;
			this.MyRigidbody.useGravity = true;
		}
		if (this.Dumped)
		{
			base.transform.position = this.Incinerator.DumpPoint.position;
		}
		else
		{
			this.Prompt.enabled = true;
			this.MyCollider.enabled = true;
			this.MyCollider.isTrigger = false;
			if (!this.CanCollide)
			{
				Physics.IgnoreCollision(this.Yandere.GetComponent<Collider>(), this.MyCollider);
			}
		}
		this.Prompt.Carried = false;
		OutlineScript[] outline = this.Outline;
		for (int i = 0; i < outline.Length; i++)
		{
			outline[i].color = (this.Evidence ? this.EvidenceColor : this.OriginalColor);
		}
		base.transform.localScale = this.OriginalScale;
		if (this.BodyPart)
		{
			this.Yandere.NearBodies--;
		}
		this.Yandere.StudentManager.UpdateStudents(0);
		if (this.Clothing && this.Evidence)
		{
			base.transform.parent = this.Yandere.Police.BloodParent;
		}
	}

	// Token: 0x0400252F RID: 9519
	public RigidbodyConstraints OriginalConstraints;

	// Token: 0x04002530 RID: 9520
	public BloodCleanerScript BloodCleaner;

	// Token: 0x04002531 RID: 9521
	public IncineratorScript Incinerator;

	// Token: 0x04002532 RID: 9522
	public BodyPartScript BodyPart;

	// Token: 0x04002533 RID: 9523
	public TrashCanScript TrashCan;

	// Token: 0x04002534 RID: 9524
	public OutlineScript[] Outline;

	// Token: 0x04002535 RID: 9525
	public YandereScript Yandere;

	// Token: 0x04002536 RID: 9526
	public Animation MyAnimation;

	// Token: 0x04002537 RID: 9527
	public BucketScript Bucket;

	// Token: 0x04002538 RID: 9528
	public PromptScript Prompt;

	// Token: 0x04002539 RID: 9529
	public ClockScript Clock;

	// Token: 0x0400253A RID: 9530
	public MopScript Mop;

	// Token: 0x0400253B RID: 9531
	public Mesh ClosedBook;

	// Token: 0x0400253C RID: 9532
	public Mesh OpenBook;

	// Token: 0x0400253D RID: 9533
	public Rigidbody MyRigidbody;

	// Token: 0x0400253E RID: 9534
	public Collider MyCollider;

	// Token: 0x0400253F RID: 9535
	public MeshFilter MyRenderer;

	// Token: 0x04002540 RID: 9536
	public Vector3 TrashPosition;

	// Token: 0x04002541 RID: 9537
	public Vector3 TrashRotation;

	// Token: 0x04002542 RID: 9538
	public Vector3 OriginalScale;

	// Token: 0x04002543 RID: 9539
	public Vector3 HoldPosition;

	// Token: 0x04002544 RID: 9540
	public Vector3 HoldRotation;

	// Token: 0x04002545 RID: 9541
	public Color EvidenceColor;

	// Token: 0x04002546 RID: 9542
	public Color OriginalColor;

	// Token: 0x04002547 RID: 9543
	public bool CleaningProduct;

	// Token: 0x04002548 RID: 9544
	public bool DisableAtStart;

	// Token: 0x04002549 RID: 9545
	public bool LockRotation;

	// Token: 0x0400254A RID: 9546
	public bool BeingLifted;

	// Token: 0x0400254B RID: 9547
	public bool CanCollide;

	// Token: 0x0400254C RID: 9548
	public bool Electronic;

	// Token: 0x0400254D RID: 9549
	public bool Flashlight;

	// Token: 0x0400254E RID: 9550
	public bool PuzzleCube;

	// Token: 0x0400254F RID: 9551
	public bool Suspicious;

	// Token: 0x04002550 RID: 9552
	public bool Blowtorch;

	// Token: 0x04002551 RID: 9553
	public bool Clothing;

	// Token: 0x04002552 RID: 9554
	public bool Evidence;

	// Token: 0x04002553 RID: 9555
	public bool JerryCan;

	// Token: 0x04002554 RID: 9556
	public bool LeftHand;

	// Token: 0x04002555 RID: 9557
	public bool RedPaint;

	// Token: 0x04002556 RID: 9558
	public bool Garbage;

	// Token: 0x04002557 RID: 9559
	public bool Bleach;

	// Token: 0x04002558 RID: 9560
	public bool Dumped;

	// Token: 0x04002559 RID: 9561
	public bool Usable;

	// Token: 0x0400255A RID: 9562
	public bool Weight;

	// Token: 0x0400255B RID: 9563
	public bool Radio;

	// Token: 0x0400255C RID: 9564
	public bool Salty;

	// Token: 0x0400255D RID: 9565
	public int CarryAnimID;

	// Token: 0x0400255E RID: 9566
	public int Strength;

	// Token: 0x0400255F RID: 9567
	public int Period;

	// Token: 0x04002560 RID: 9568
	public int Food;

	// Token: 0x04002561 RID: 9569
	public float KinematicTimer;

	// Token: 0x04002562 RID: 9570
	public float DumpTimer;

	// Token: 0x04002563 RID: 9571
	public bool Empty = true;

	// Token: 0x04002564 RID: 9572
	public GameObject[] FoodPieces;

	// Token: 0x04002565 RID: 9573
	public WeaponScript StuckBoxCutter;
}
