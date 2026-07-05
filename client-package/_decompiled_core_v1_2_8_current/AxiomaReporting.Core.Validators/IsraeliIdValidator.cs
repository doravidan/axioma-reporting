namespace AxiomaReporting.Core.Validators;

public static class IsraeliIdValidator
{
	public static bool IsValid(string? id)
	{
		if (string.IsNullOrWhiteSpace(id))
		{
			return false;
		}
		string text = id.Trim();
		string text2 = text;
		for (int i = 0; i < text2.Length; i++)
		{
			if (!char.IsDigit(text2[i]))
			{
				return false;
			}
		}
		if (text.Length > 9)
		{
			return false;
		}
		string text3 = text.PadLeft(9, '0');
		int num = 0;
		for (int j = 0; j < 9; j++)
		{
			int num2 = (text3[j] - 48) * (j % 2 + 1);
			if (num2 > 9)
			{
				num2 = num2 / 10 + num2 % 10;
			}
			num += num2;
		}
		return num % 10 == 0;
	}
}
