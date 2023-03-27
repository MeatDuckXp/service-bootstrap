using System;
using System.Text.Json; 

namespace Test.Sample.Service.Services.NotificationSender
{
    public class NotificationSenderService : INotificationSenderService
    {
        public void Send(string receiver, Notification notification)
        {
            ArgumentNullException.ThrowIfNull(notification);
            ArgumentNullException.ThrowIfNullOrEmpty(receiver);
            
            Console.WriteLine($"Service=NotificationSender, Action=Sent, Receiver={receiver}, Message={JsonSerializer.Serialize(notification)}");
        }
    }
}