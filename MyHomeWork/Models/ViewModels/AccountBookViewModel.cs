namespace MyHomeWork.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class AccountBookViewModel : IValidatableObject
    {
        [DisplayName("次序")]
        public string Id { get; set; }

        [Required(ErrorMessage = "類別必填")]
        [DisplayName("類別")]
        public int? Categoryyy { get; set; }

        [Required(ErrorMessage = "金額必填,只能輸入正整數")]
        [RegularExpression("^[0-9]*[1-9][0-9]*$", ErrorMessage = "Invalid Expression!")]
        [DisplayName("金額")]
        public int? Amounttt { get; set; }

        [DisplayName("日期")]
        public DateTime? Dateee { get; set; }

        [Required(ErrorMessage = "備註必填")]
        [DisplayName("備註")]
        [StringLength(100)]
        public string Remarkkk { get; set; } 

        public int PageIndex { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Dateee.HasValue && Dateee.DateDiff(Convert.ToDateTime(Dateee)))
               yield return new ValidationResult("日期不得大於今天", new[] { "Dateee" });
        }
    }
}