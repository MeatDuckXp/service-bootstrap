using System.Collections.Generic;

namespace Test.Sample.Service.Services.Message
{
    public class NotificationMessageSource : INotificationMessageSource
    {
        public List<Message> GetMessages()
        {
            return new List<Message>()
            {
                new Message("Hello world", "This is a hello world message, message's body."),
                new Message("Bon Voyage", "Bon Voyage and get there safe.")
            };

        }
    }
} 