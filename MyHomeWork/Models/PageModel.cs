using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyHomeWork.Models
{
    public class PageModel
    {
        public List<AccountBookViewModel> Models { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int PageBarSize { get; set; }
        public int TotalCount { get; set; }
        public int PrevIndex { get; set; }
        public int NextIndex { get; set; }
    }
}