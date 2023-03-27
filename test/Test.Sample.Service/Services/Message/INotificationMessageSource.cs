using System.Collections.Generic;

namespace Test.Sample.Service.Services.Message
{
    public interface INotificationMessageSource
    {
        List<Message> GetMessages();
    }
}