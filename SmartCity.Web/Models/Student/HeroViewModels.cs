﻿namespace SmartCity.Web.Models.Student
{
    public class HeroViewModels
    {
        public string Name { get; set; }
        public Rangs Rang { get; set; }
        public int Number { get; set; }
        public string UrlPhoto { get; set; }
        public enum Rangs
        { 
            S,
            A,
            B,
            C
        }
    }
}
