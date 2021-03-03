﻿using System;
using System.Collections.Generic;
using System.Linq;
using SmartCity.Common.Enums;
using SmartCity.Data.Entities.UserAccount;
using SmartCity.Data.Interfaces;
using SmartCity.Services.Interfaces;

namespace SmartCity.Services.Services
{
    public class TaskService : ITaskService
    {
        private IUserTaskRepository userTaskRepository;

        public TaskService(IUserTaskRepository userTaskRepository)
        {
            this.userTaskRepository = userTaskRepository;
        }

        public List<UserTask> GetUserPlannedTasks(string userLogin)
        {
            var userTasks = userTaskRepository.GetTasksByUserLogin(userLogin)
                .Where(task => task.Status == TaskStatus.Planned).OrderBy(task => task.StartDate);

            return userTasks.ToList();
        }

        public List<UserTask> GetUserTasksPlannedForToday(string userLogin)
        {
            var userTasks = userTaskRepository.GetTasksByUserLogin(userLogin).Where(task =>
                task.Status == TaskStatus.Planned && task.StartDate.Date == DateTime.Today)
                .OrderBy(task => task.StartDate);

            return userTasks.ToList();
        }

        public List<UserTask> GetUserTasksPlannedForTomorrow(string userLogin)
        {
            var tomorrowDate = DateTime.Today + TimeSpan.FromDays(1);
            var userTasks = userTaskRepository.GetTasksByUserLogin(userLogin).Where(task =>
                task.Status == TaskStatus.Planned && task.StartDate.Date == tomorrowDate.Date)
                .OrderBy(task => task.StartDate);

            return userTasks.ToList();
        }

        public List<UserTask> GetUserTasksPlannedForNextDays(string userLogin, int daysFromToday)
        {
            var lastDayDate = DateTime.Today + TimeSpan.FromDays(daysFromToday);
            var userTasks = userTaskRepository.GetTasksByUserLogin(userLogin)
                .Where(task => task.Status == TaskStatus.Planned && task.StartDate >= DateTime.Now &&
                               task.StartDate.Date <= lastDayDate).OrderBy(task => task.StartDate);

            return userTasks.ToList();
        }

        public List<UserTask> GetUserCompletedTasks(string userLogin)
        {
            var userTasks = userTaskRepository.GetTasksByUserLogin(userLogin)
                .Where(task => task.Status == TaskStatus.Complete).OrderBy(task => task.StartDate);

            return userTasks.ToList();
        }

        public List<UserTask> GetUserOverdueTasks(string userLogin)
        {
            var userTasks = userTaskRepository.GetTasksByUserLogin(userLogin)
                .Where(task => task.Status == TaskStatus.Planned && task.StartDate <= DateTime.Now)
                .OrderBy(task => task.StartDate);

            return userTasks.ToList();
        }

        public UserTask GetTaskById(long taskId)
        {
            return userTaskRepository.Get(taskId);
        }

        public void Save(UserTask task)
        {
            userTaskRepository.Save(task);
        }

        public void Delete(long taskId)
        {
            userTaskRepository.Delete(taskId);
        }

        public bool Exists(long taskId)
        {
            return userTaskRepository.TaskExists(taskId);
        }
    }
}
