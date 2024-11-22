using System;

public static class CubeNumber
{
	public enum Index
	{
		_2 = 1,
		_4,
		_8,
		_16,
		_32,
		_64,
		_128,
		_256,
		_512,
		_1024,
		_2048,
		_4096,
		_8192,
		_16K,
		_32K,
		_65K,
		_131K,
		_262K,
		_524K,
		_1M,
		_2M,
		_4M,
		_8M,
		_16M,
		_33M,
		_67M,
		_134M,
		_268M,
		_536M,
		_1B
	}

	public const Index MIN = Index._2;
	public const Index MAX = Index._1B;
	public const Index MAX_SHOOTABLE = Index._64;

	private static readonly Random _random = new();

	public static Index GetNextIndex(Index number)
	{
		number++;

		if (number > MAX)
		{
			number = MIN;
		}

		return number;
	}

	public static Index GetRandomShootableIndex()
	{
		return (Index)_random.Next((int)MIN, (int)MAX_SHOOTABLE + 1);
	}

	public static string GetDisplayText(Index number)
	{
		return number.ToString().Replace("_", "");
	}
}