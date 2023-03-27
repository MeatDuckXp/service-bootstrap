using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Test.Sample.Service.Core;
using Test.Sample.Service.Services.Message;
using Test.Sample.Service.Services.NotificationSender;

namespace Test.Sample.Service
{
    /// <summary>
    ///     This class represents the service worker. This worker is a simple background service, that is supposed to send static messages as notifications. 
    /// </summary>
    public class NotificationWorker : BackgroundService
    {
        private readonly INotificationMessageSource _messageSource;
        private readonly INotificationSenderService _notificationSender;
        private readonly ServiceConfiguration _configuration;

        public NotificationWorker(
            INotificationMessageSource messageSource,
            INotificationSenderService notificationSender,
            IOptions<ServiceConfiguration> configuration)
        {
            ArgumentNullException.ThrowIfNull(messageSource);
            ArgumentNullException.ThrowIfNull(notificationSender);
            ArgumentNullException.ThrowIfNull(configuration);

            _messageSource = messageSource;
            _notificationSender = notificationSender;
            _configuration = configuration.Value;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                return Task.Factory.StartNew(() =>
                {
                    while (!stoppingToken.IsCancellationRequested)
                    {
                        var messages = _messageSource.GetMessages();
                        var receiver = _configuration.Receivers.First();

                        foreach (var message in messages)
                        {
                            _notificationSender.Send(receiver, new Notification
                            {
                                Title = message.Title, 
                                Text = message.Body
                            });
                        }

                        Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
                    }
                }, stoppingToken);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            } 
        }
    }
} 