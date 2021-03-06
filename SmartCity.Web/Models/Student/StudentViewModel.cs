﻿namespace SmartCity.Web.Models.Student
{
    public class StudentViewModel
    {
        public string CurentUserName { get; set; }

        public int Second { get; set; }

        public string DayOfWeek { get; set; }

        public Sex Sex { get; set; }
    }

    public enum Sex
    {
        Male = 1,
        Female = 2,
        Null = 3
    }
}
