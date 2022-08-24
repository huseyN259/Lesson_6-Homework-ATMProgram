class Client
{
	public string? Name { get; set; }
	public string? Surname { get; set; }
	public BankCard Card { get; set; }
	public Notification[] Not { get; set; }

	public Client() { }

	public Client(string? name, string? surname, BankCard card, Notification[] not)
	{
		Name = name;
		Surname = surname;
		Card = card;
		Not = not;
	}

	public override string ToString() => $"\nUSER\n{Name} {Surname} -> {Card.PAN} | {Card.PIN} | {Card.ExpireDate}";

	public void AddMessage(string message)
	{
		Notification notification = new Notification(message, DateTime.Now);

		if (Not == null)
			Not = new Notification[] { notification };
		else
			Not = Not.Append<Notification>(notification).ToArray();
	}
}