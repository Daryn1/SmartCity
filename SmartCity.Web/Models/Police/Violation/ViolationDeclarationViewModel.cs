﻿using System;
using SmartCity.Common.Enums;

namespace SmartCity.Web.Models.Police.Violation
{
    public class ViolationDeclarationViewModel
    {
        public string UserLogin { get; set; }
        public string BlamedUserLogin { get; set; }
        public DateTime Date { get; set; }
        public string Explanation { get; set; }
        public TypeOfOffense OffenseType { get; set; }

        public string RedirectLink { get; set; }
    }
}
