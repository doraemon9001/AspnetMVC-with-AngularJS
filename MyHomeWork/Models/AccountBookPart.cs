using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyHomeWork.Models
{
    public partial class AccountBook
    {
        public AccountBookViewModel ToViewModel(int pageIndex)
        {
            return new AccountBookViewModel()
            {
                Id = this.Id.ToString(),
                Amounttt = this.Amounttt, 
                Categoryyy = this.Categoryyy,
                Dateee = this.Dateee.Date,
                Remarkkk = this.Remarkkk,
                PageIndex = pageIndex
            };
        }
    }
}