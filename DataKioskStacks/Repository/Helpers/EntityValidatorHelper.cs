﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataKioskStacks.Repository.Helpers
{
    public class EntityValidatorHelper
    {
        public static bool Validate(dynamic obj, out List<ValidationResult> results)
        {
            results = new List<ValidationResult>();
            var context = new ValidationContext(obj, null, null);
            return Validator.TryValidateObject(obj, context, results, true);
        }
    }
}
