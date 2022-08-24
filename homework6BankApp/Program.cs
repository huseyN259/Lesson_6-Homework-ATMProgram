using Bogus;

partial class Program
{
	class BankException : Exception
	{
		public BankException(string message = "Balance is not enough !") : base(message) { }
	}
	
	public static BankCard FakeCardGenerator()
	{
		BankCard card = new Faker<BankCard>()
			.RuleFor(card => card.PAN, bogus => bogus.Random.String(16, '0', '9'))
			.RuleFor(card => card.PIN, bogus => bogus.Random.String(4, '0', '9'))
			.RuleFor(card => card.CVC, bogus => bogus.Random.Int(100, 999))
			.RuleFor(card => card.ExpireDate, bogus => $"{bogus.Random.Int(1, 12)} / {DateTime.Now.Year} / {DateTime.Now.Year}")
			.RuleFor(card => card.Balance, bogus => bogus.Random.Double(0, 10000));

		return card;
	}

	public static Client FakeClientGenerator()
	{
		Client client = new Faker<Client>()
			.RuleFor(client => client.Name, bogus => bogus.Name.FirstName())
			.RuleFor(client => client.Surname, bogus => bogus.Name.LastName());
		client.Card = FakeCardGenerator();

		return client;
	}
	
	static void Main()
	{
		string enterProgram = "* WELCOME TO BANK PROGRAM *";
		Console.WriteLine(enterProgram.PadLeft(70));
		Console.WriteLine("\nLoading...");
		System.Threading.Thread.Sleep(2000);
		Console.Clear();
		
		Client? client = null;
		Client[] clients =
		{
			FakeClientGenerator(),
			FakeClientGenerator(),
			FakeClientGenerator()
		};

		while (true)
		{
			while (true)
			{
				Console.Clear();

				foreach (var c in clients)
					Console.WriteLine(c);

				string start = "Please Select Any Account and Enter PIN Code : ";
				Console.WriteLine();
				Console.Write(start.PadLeft(70));

				string? inPIN = Console.ReadLine();
				if (inPIN?.Length != 4)
					continue;

				for (int i = 0; i < clients.Length; i++)
					if (clients[i].Card.PIN == inPIN)
					{
						client = clients[i];
						break;
					}
				if (client != null)
					break;
			}

			Console.Clear();
			client.AddMessage("You Entered System !");
			int choice = Convert.ToInt32(GetSelect("\n~ Please Enter Your Selection\n", new string[] { "Balance", "Get Cash", "Show Notification", "Card To Card", "Exit" }) + 1);
			Console.WriteLine('\n');

			try
			{
				if (choice == 1)
				{
					Console.WriteLine($"Balance : {client.Card.Balance}");
					client.AddMessage("Balance Shown !");
				}
				else if (choice == 2)
				{
					int moneyChoice = Convert.ToInt32(GetSelect("Enter Amount of Money :", new string[] { "10 AZN", "20 AZN", "50 AZN", "100 AZN", "Other" }) + 1);

					int moneyAmount = default;
					switch (moneyChoice)
					{
						case 1:
							moneyAmount = 10;
							break;
						case 2:
							moneyAmount = 20;
							break;
						case 3:
							moneyAmount = 50;
							break;
						case 4:
							moneyAmount = 100;
							break;
						case 5:
							do
							{
								Console.Write("Enter Money Amount : ");
								int.TryParse(Console.ReadLine(), out moneyAmount);
								Console.Clear();
							} while (moneyAmount <= 0);
							break;
					}

					if (client.Card.Balance < moneyAmount)
					{
						client.AddMessage("Tried to Withdraw More Money Than The Balance !");
						throw new BankException();
					}
					else
					{
						Console.WriteLine("Successfully Withdrawn !");
						client.AddMessage($"Withdrew {moneyAmount} AZN Money !");
						client.Card.Balance -= moneyAmount;
					}
				}
				else if (choice == 3)
				{
					foreach (var log in client.Not)
						Console.WriteLine(log);
					Console.ReadKey();
				}
				else if (choice == 4)
				{
					BankCard? targetCard = null;

					while (true)
					{
						string? inPIN = string.Empty;
						Console.Write("Enter PIN Code : ");
						inPIN = Console.ReadLine();

						foreach (var user in clients)
							if (user.Card.PIN == inPIN && !BankCard.ReferenceEquals(user.Card, client.Card))
							{
								targetCard = user.Card;
								break;
							}
						if (targetCard != null)
							break;
						Console.Clear();
					}

					double moneyAmount = default;
					do
					{
						Console.Write("Enter Money Amount : ");
						moneyAmount = Convert.ToDouble(Console.ReadLine());
						Console.Clear();
					} while (moneyAmount <= 0);

					if (client.Card.Balance < moneyAmount)
						throw new BankException();
					client.Card.Balance -= moneyAmount;
					targetCard.Balance += moneyAmount;
				}
				else if (choice == 5)
					Environment.Exit(0);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				client.AddMessage($"Exception : {ex.Message}");
			}
			finally
			{
				Console.Write("\nPress any key to continue...");
				Console.ReadKey();
			}
		}
	}
}