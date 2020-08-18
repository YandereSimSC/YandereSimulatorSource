using System;

// Token: 0x0200043D RID: 1085
public static class CardinalDirection
{
	// Token: 0x06001C95 RID: 7317 RVA: 0x001553A0 File Offset: 0x001535A0
	public static Direction Reversed(Direction direction)
	{
		if (direction == Direction.North)
		{
			return Direction.South;
		}
		if (direction == Direction.East)
		{
			return Direction.West;
		}
		if (direction == Direction.South)
		{
			return Direction.North;
		}
		return Direction.East;
	}

	// Token: 0x06001C96 RID: 7318 RVA: 0x001553B4 File Offset: 0x001535B4
	public static Direction LeftPerp(Direction direction)
	{
		if (direction == Direction.North)
		{
			return Direction.West;
		}
		if (direction == Direction.East)
		{
			return Direction.North;
		}
		if (direction == Direction.South)
		{
			return Direction.East;
		}
		return Direction.South;
	}

	// Token: 0x06001C97 RID: 7319 RVA: 0x001553C8 File Offset: 0x001535C8
	public static Direction RightPerp(Direction direction)
	{
		if (direction == Direction.North)
		{
			return Direction.East;
		}
		if (direction == Direction.East)
		{
			return Direction.South;
		}
		if (direction == Direction.South)
		{
			return Direction.West;
		}
		return Direction.North;
	}
}
