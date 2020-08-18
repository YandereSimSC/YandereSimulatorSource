using System;
using UnityEngine;

// Token: 0x0200030C RID: 780
public class InventoryTestScript : MonoBehaviour
{
	// Token: 0x06001789 RID: 6025 RVA: 0x000CEC74 File Offset: 0x000CCE74
	private void Start()
	{
		this.RightGrid.localScale = new Vector3(0f, 0f, 0f);
		this.LeftGrid.localScale = new Vector3(0f, 0f, 0f);
		Time.timeScale = 1f;
	}

	// Token: 0x0600178A RID: 6026 RVA: 0x000CECCC File Offset: 0x000CCECC
	private void Update()
	{
		if (Input.GetKeyDown("space"))
		{
			this.Open = !this.Open;
		}
		AnimationState animationState = this.SkirtAnimation["InverseSkirtOpen"];
		AnimationState animationState2 = this.GirlAnimation["f02_inventory_00"];
		if (this.Open)
		{
			this.RightGrid.localScale = Vector3.MoveTowards(this.RightGrid.localScale, new Vector3(0.9f, 0.9f, 0.9f), Time.deltaTime * 10f);
			this.LeftGrid.localScale = Vector3.MoveTowards(this.LeftGrid.localScale, new Vector3(0.9f, 0.9f, 0.9f), Time.deltaTime * 10f);
			base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y, Mathf.Lerp(base.transform.position.z, 0.37f, Time.deltaTime * 10f));
			animationState.time = Mathf.Lerp(animationState2.time, 1f, Time.deltaTime * 10f);
			animationState2.time = animationState.time;
			this.Alpha = Mathf.Lerp(this.Alpha, 1f, Time.deltaTime * 10f);
			this.SkirtRenderer.material.color = new Color(1f, 1f, 1f, this.Alpha);
			this.GirlRenderer.materials[0].color = new Color(0f, 0f, 0f, this.Alpha);
			this.GirlRenderer.materials[1].color = new Color(0f, 0f, 0f, this.Alpha);
			if (Input.GetKeyDown("right"))
			{
				this.Column++;
				this.UpdateHighlight();
			}
			if (Input.GetKeyDown("left"))
			{
				this.Column--;
				this.UpdateHighlight();
			}
			if (Input.GetKeyDown("up"))
			{
				this.Row--;
				this.UpdateHighlight();
			}
			if (Input.GetKeyDown("down"))
			{
				this.Row++;
				this.UpdateHighlight();
			}
		}
		else
		{
			this.RightGrid.localScale = Vector3.MoveTowards(this.RightGrid.localScale, new Vector3(0f, 0f, 0f), Time.deltaTime * 10f);
			this.LeftGrid.localScale = Vector3.MoveTowards(this.LeftGrid.localScale, new Vector3(0f, 0f, 0f), Time.deltaTime * 10f);
			base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y, Mathf.Lerp(base.transform.position.z, 1f, Time.deltaTime * 10f));
			animationState.time = Mathf.Lerp(animationState2.time, 0f, Time.deltaTime * 10f);
			animationState2.time = animationState.time;
			this.Alpha = Mathf.Lerp(this.Alpha, 0f, Time.deltaTime * 10f);
			this.SkirtRenderer.material.color = new Color(1f, 1f, 1f, this.Alpha);
			this.GirlRenderer.materials[0].color = new Color(0f, 0f, 0f, this.Alpha);
			this.GirlRenderer.materials[1].color = new Color(0f, 0f, 0f, this.Alpha);
		}
		for (int i = 0; i < this.Items.Length; i++)
		{
			if (this.Items[i].Clicked)
			{
				Debug.Log(string.Concat(new object[]
				{
					"Item width is ",
					this.Items[i].InventoryItem.Width,
					" and item height is ",
					this.Items[i].InventoryItem.Height,
					". Open space is: ",
					this.OpenSpace
				}));
				if (this.Items[i].InventoryItem.Height * this.Items[i].InventoryItem.Width < this.OpenSpace)
				{
					Debug.Log("We might have enough open space to add the item to the inventory.");
					this.CheckOpenSpace();
					if (this.UseGrid == 1)
					{
						this.Items[i].transform.parent = this.LeftGridItemParent;
						float inventorySize = this.Items[i].InventoryItem.InventorySize;
						this.Items[i].transform.localScale = new Vector3(inventorySize, inventorySize, inventorySize);
						this.Items[i].transform.localEulerAngles = new Vector3(90f, 180f, 0f);
						this.Items[i].transform.localPosition = this.Items[i].InventoryItem.InventoryPosition;
						int j = 1;
						if (this.UseColumn == 1)
						{
							while (j < this.Items[i].InventoryItem.Height + 1)
							{
								this.LeftSpaces1[j] = true;
								j++;
							}
						}
						else if (this.UseColumn == 2)
						{
							while (j < this.Items[i].InventoryItem.Height + 1)
							{
								this.LeftSpaces2[j] = true;
								j++;
							}
						}
						if (this.UseColumn > 1)
						{
							this.Items[i].transform.localPosition -= new Vector3(0.05f * (float)(this.UseColumn - 1), 0f, 0f);
						}
					}
				}
				this.Items[i].Clicked = false;
			}
		}
	}

	// Token: 0x0600178B RID: 6027 RVA: 0x000CF308 File Offset: 0x000CD508
	private void CheckOpenSpace()
	{
		this.UseColumn = 0;
		this.UseGrid = 0;
		int i;
		for (i = 1; i < this.LeftSpaces1.Length; i++)
		{
			if (this.UseGrid == 0 && !this.LeftSpaces1[i])
			{
				this.UseColumn = 1;
				this.UseGrid = 1;
			}
		}
		i = 1;
		if (this.UseGrid == 0)
		{
			while (i < this.LeftSpaces2.Length)
			{
				if (this.UseGrid == 0 && !this.LeftSpaces2[i])
				{
					this.UseColumn = 2;
					this.UseGrid = 1;
				}
				i++;
			}
		}
	}

	// Token: 0x0600178C RID: 6028 RVA: 0x000CF394 File Offset: 0x000CD594
	private void UpdateHighlight()
	{
		if (this.Column == 5)
		{
			if (this.Grid == 1)
			{
				this.Grid = 2;
			}
			else
			{
				this.Grid = 1;
			}
			this.Column = 1;
		}
		else if (this.Column == 0)
		{
			if (this.Grid == 1)
			{
				this.Grid = 2;
			}
			else
			{
				this.Grid = 1;
			}
			this.Column = 4;
		}
		if (this.Row == 6)
		{
			this.Row = 1;
		}
		else if (this.Row == 0)
		{
			this.Row = 5;
		}
		if (this.Grid == 1)
		{
			this.Highlight.transform.parent = this.LeftGridHighlightParent;
		}
		else
		{
			this.Highlight.transform.parent = this.RightGridHighlightParent;
		}
		this.Highlight.localPosition = new Vector3((float)this.Column, (float)(this.Row * -1), 0f);
	}

	// Token: 0x0400216D RID: 8557
	public SimpleDetectClickScript[] Items;

	// Token: 0x0400216E RID: 8558
	public Animation SkirtAnimation;

	// Token: 0x0400216F RID: 8559
	public Animation GirlAnimation;

	// Token: 0x04002170 RID: 8560
	public GameObject Skirt;

	// Token: 0x04002171 RID: 8561
	public GameObject Girl;

	// Token: 0x04002172 RID: 8562
	public Renderer SkirtRenderer;

	// Token: 0x04002173 RID: 8563
	public Renderer GirlRenderer;

	// Token: 0x04002174 RID: 8564
	public Transform RightGridHighlightParent;

	// Token: 0x04002175 RID: 8565
	public Transform LeftGridHighlightParent;

	// Token: 0x04002176 RID: 8566
	public Transform RightGridItemParent;

	// Token: 0x04002177 RID: 8567
	public Transform LeftGridItemParent;

	// Token: 0x04002178 RID: 8568
	public Transform Highlight;

	// Token: 0x04002179 RID: 8569
	public Transform RightGrid;

	// Token: 0x0400217A RID: 8570
	public Transform LeftGrid;

	// Token: 0x0400217B RID: 8571
	public float Alpha;

	// Token: 0x0400217C RID: 8572
	public bool Open = true;

	// Token: 0x0400217D RID: 8573
	public int OpenSpace = 1;

	// Token: 0x0400217E RID: 8574
	public int UseColumn;

	// Token: 0x0400217F RID: 8575
	public int UseGrid;

	// Token: 0x04002180 RID: 8576
	public int Column = 1;

	// Token: 0x04002181 RID: 8577
	public int Grid = 1;

	// Token: 0x04002182 RID: 8578
	public int Row = 1;

	// Token: 0x04002183 RID: 8579
	public bool[] LeftSpaces1;

	// Token: 0x04002184 RID: 8580
	public bool[] LeftSpaces2;

	// Token: 0x04002185 RID: 8581
	public bool[] LeftSpaces3;

	// Token: 0x04002186 RID: 8582
	public bool[] LeftSpaces4;

	// Token: 0x04002187 RID: 8583
	public bool[] RightSpaces1;

	// Token: 0x04002188 RID: 8584
	public bool[] RightSpaces2;

	// Token: 0x04002189 RID: 8585
	public bool[] RightSpaces3;

	// Token: 0x0400218A RID: 8586
	public bool[] RightSpaces4;
}
