class BankCard
{
	public string? PAN { get; set; }
	public string? PIN { get; set; }
	public int CVC { get; set; }
	public string? ExpireDate { get; set; }
	public double Balance { get; set; }

	public BankCard() { }

	public BankCard(string? pAN, string? pIN, int cVC, string? expireDate, double balance)
	{
		PAN = pAN;
		PIN = pIN;
		CVC = cVC;
		ExpireDate = expireDate;
		Balance = balance;
	}
}