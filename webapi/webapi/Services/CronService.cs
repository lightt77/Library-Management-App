using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using webapi.Models;

namespace webapi.Services
{
    public class CronService
    {
        public static void ScheduleJobs()
        {
            //TODO: Change the intervals
            //Thread cronThread = new Thread(Foo);

            //cronThread.Start();
         
            ISchedulerFactory schedulerFactory = new StdSchedulerFactory();
            IScheduler scheduler = schedulerFactory.GetScheduler();

            IJobDetail job = JobBuilder.Create<NotificationGenerator>()
                .WithIdentity("name", "group")
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithSimpleSchedule(s => s.WithIntervalInSeconds(2).RepeatForever())
                .StartNow()
                .Build();

            scheduler.ScheduleJob(job, trigger);
            scheduler.Start();
            //Thread.Sleep(TimeSpan.FromMinutes(10));
            //scheduler.Shutdown();
        }
    }

    public class NotificationGenerator : IJob
    {
        private readonly NotificationService notificationService = new NotificationService();

        public void Execute(IJobExecutionContext context)
        {
            GenerateBookReturnDateDueNotifications();

            GenerateBookInWishlistAvailableNotifications();

            GenerateBookIssueRequestNotifications();
        }

        private void GenerateBookReturnDateDueNotifications()
        {
            List<Rental> recordList = notificationService.GetAllBookReturnDateDueRecords();

            foreach (var i in recordList)
                notificationService.GenerateBookReturnDateDueNotifications(i.BookName, i.UserName, i.ReturnDate);   
        }

        private void GenerateBookInWishlistAvailableNotifications()
        {
            List<Rental> recordList = notificationService.GetBooksInWishlistAvailableRecords();

            foreach (var i in recordList)
                notificationService.GenerateBookInWishlistAvailableNotifications(i.BookName, i.UserName);
        }

        private void GenerateBookIssueRequestNotifications()
        {
            List<Rental> recordList = notificationService.GetNewRentalRequests();

            foreach (var record in recordList)
                notificationService.GenerateNewBookIssueRequestNotifications(record.UserName, record.BookName);
        }
    }


}