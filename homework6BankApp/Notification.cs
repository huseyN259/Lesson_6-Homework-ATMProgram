class Notification
{
	public string? NotMessage { get; set; }
	public DateTime Time { get; set; }

	public Notification() { }

	public Notification(string? notMessage, DateTime time)
	{
		NotMessage = notMessage;
		Time = time;
	}

	public override string ToString() => $"{NotMessage} -> {Time.ToString()}";
}