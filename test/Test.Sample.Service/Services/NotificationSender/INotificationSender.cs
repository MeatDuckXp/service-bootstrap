namespace Test.Sample.Service.Services.NotificationSender
{
    public interface INotificationSenderService
    {
        void Send(string receiver, Notification notification);
    }
}
