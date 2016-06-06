namespace MyHomeWork
{
    using System;
    using Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using System.Runtime.Caching;
    public class MyController:Controller
    { 
        private static readonly Lazy<SkillTree> lazy = 
            new Lazy<SkillTree>(() => new SkillTree()); 

        public static SkillTree Instance { get { return lazy.Value; } }
        protected readonly List<AccountBook> listBooks;
        public MyController()
        {
            if (MemoryCache.Default.Get("listBooks") != null) listBooks = MemoryCache.Default.Get("listBooks") as List<AccountBook>;
            else
            {
                var cachePolicty = new CacheItemPolicy();
                cachePolicty.AbsoluteExpiration = DateTime.Now.AddSeconds(60);
                listBooks = Instance.AccountBooks.Where(s => true).ToList();
                MemoryCache.Default.Add("listBooks", listBooks, cachePolicty);
            }
        }
        #region 1.0 獲取頁面資訊 - List<AccountBook> GetList(int pageIndex, int pageSize)
        /// <summary>
        /// 1.0 獲取頁面資訊
        /// </summary>
        /// <param name="pageIndex">當前頁碼</param>
        /// <param name="pageSize">當前頁面總數</param>
        /// <returns>List<AccountBook></returns>
        protected List<AccountBookViewModel> GetList(int pageIndex, int pageSize)
        {
           // var list= listBooks.Select(s => s.ToViewModel(pageIndex)).ToList(); ///塞PageIndex值
           return listBooks.Select(s => s.ToViewModel(pageIndex))
                .OrderByDescending(s => s.Dateee)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).ToList();
        }
        #endregion
        #region 2.0 獲取AccountBook總數 - List<AccountBook> GetListCount()
        /// <summary>
        /// 2.0 獲取頁面資訊
        /// </summary>
        /// <returns>listBooks總數</returns>
        protected int GetListCount()
        {
            return listBooks.Count;
        }
        #endregion
    }
}