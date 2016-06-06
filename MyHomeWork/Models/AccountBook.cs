namespace MyHomeWork.Models
{
    using System;  
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
  

    [Table("AccountBook")]///指定連結資料庫的資料表名稱
    public partial class AccountBook
    { 
        public Guid Id { get; set; }

        public int Categoryyy { get; set; }

        public int Amounttt { get; set; }

        public DateTime Dateee { get; set; }

        [Required]
        public string Remarkkk { get; set; }
    }
}
