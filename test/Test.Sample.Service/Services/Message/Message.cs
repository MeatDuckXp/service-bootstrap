namespace Test.Sample.Service.Services.Message
{
    public class Message
    {
        public Message(string title, string body)
        {
            Title = title;
            Body = body;
        }

        public string Title { get; }

        public string Body { get; }
    }
}
