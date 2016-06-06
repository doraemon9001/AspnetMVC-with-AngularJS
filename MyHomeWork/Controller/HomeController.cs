namespace MyHomeWork.Controllers
{
    using Models;
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using System.Data.Entity.Validation;
    using System.Runtime.Caching;
    using System.Collections.Generic;
    public class HomeController : MyController
    {
        public ActionResult Login(RegisterModel model)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string txtName, string txtPwd)
        {
            if(txtName.ToLower() == "admin")
            {
                // 登入時清空所有 Session 資料
                Session.RemoveAll();
                Session.Add("LoginedUser", txtName);
                return RedirectToAction("Index","Admin");
            }
            return View("Index");
        }
        #region 1.0 帳本列表 + ActionResult Index(int id)
        /// <summary>
        /// 1.0 帳本列表
        /// </summary>
        /// <returns>View(list)</returns>
        [HttpGet]
        //[MyAuth]
        public ActionResult Index(int id = 1)
        {
            //var uName = Session["LoginedUser"].ToString();
            return View();
        }
        #endregion
        #region 2.0 獲取分頁Bar +  ActionResult PageList(string id)
        /// <summary>
        /// 2.0 獲取分頁Bar
        /// </summary>
        /// <param name="pageIndex">當前頁碼</param>
        /// <returns>分頁Bar</returns>
        [HttpPost]
        public ActionResult PageList(int pageIndex,int pageSize = 10)
        {  
            var totalCount = this.GetListCount() / pageSize;
            var prevIndex = pageIndex == 1 ? 1 : pageIndex - 1;
            var nextIndex = pageIndex == totalCount ? totalCount : pageIndex + 1;
            return Json(new PageModel() {
                Models = MemoryCache.Default.Get("listBooks") as List<AccountBookViewModel> ?? this.GetList(pageIndex, pageSize),
                PageBarSize = 5,
                PrevIndex = prevIndex,
                NextIndex = nextIndex,
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalCount = totalCount
            });
        }
        #endregion
        #region 3.0 刪除單筆AccountBook + ActionResult Del(string id)
        /// <summary>
        /// 3.0 刪除單筆AccountBook
        /// </summary>
        /// <param name="id">guid + pageIndex</param>
        /// <returns>RedirectToAction("Index")</returns>
        [HttpPost]
        public ActionResult Delete(string id, int pageIndex)
        {
            Guid rGuid;
            if (!Guid.TryParse(id,out rGuid)) return Json(new {
                success = false,
                errors = ModelState.Keys.SelectMany(k => ModelState[k].Errors).Select(m => m.ErrorMessage).ToArray()}); 
            try
            {
                Instance.AccountBooks.RemoveRange(listBooks.Where(s => s.Id == rGuid));
                Instance.SaveChanges();
                MemoryCache.Default.Remove("listBooks");
                return Json(new { success = true, pageIndex = pageIndex });
            }
            catch (DbEntityValidationException ex)
            {
                Helpers.GetDbError(ex);
                return Json(new { success = false });
            }
        }
        #endregion
        #region 4.0 取得單筆AccountBoo資料
        /// <summary>
        /// 4.0 取得單筆AccountBoo資料
        /// </summary>
        /// <param name="id">guid + pageIndex</param>
        /// <returns>AccountBook</returns>
        [HttpGet]
        public ActionResult Edit(string id,int pageIndex)
        {
            Guid rGuid;
            if (!Guid.TryParse(id,out rGuid)) return Content("<script>alert('Id Error');</script>");
            var editModel = listBooks.Where(s => s.Id == rGuid).Select(s => s.ToViewModel(pageIndex)).FirstOrDefault();
            return View(editModel);
        }
        #endregion
        #region 5.0 更新單筆資料 + ActionResult Edit(Models.AccountBookViewModel model)
        /// <summary>
        /// 5.0 更新單筆資料
        /// </summary>
        /// <param name="model">要修改的實體</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Models.AccountBookViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    success = false,
                    errors = ModelState.Keys.SelectMany(k => ModelState[k].Errors).Select(m => m.ErrorMessage).ToArray()
                });
            }
            var rGuid = Guid.Parse(model.Id);
            try
            {
                var eModel = listBooks.FirstOrDefault(s => s.Id == rGuid);
                eModel.Categoryyy = (int)model.Categoryyy;
                eModel.Amounttt = (int)model.Amounttt;
                eModel.Dateee = (DateTime)model.Dateee;
                eModel.Remarkkk = model.Remarkkk;
                Instance.SaveChanges();
                MemoryCache.Default.Remove("listBooks");
                return Json(new { success = true, pageIndex = model.PageIndex });
            }
            catch (DbEntityValidationException ex)
            {
                Helpers.GetDbError(ex);
                return Json(new { success = false });
            }
        }
        #endregion
        #region 6.0 新增頁面Get + ActionResult Add(string id)
        /// <summary>
        /// 6.0 新增頁面Get
        /// </summary>
        /// <param name="id">pageIndex</param>
        /// <returns>View(AccountBookViewModel)</returns>
        [HttpGet]
        public ActionResult Add(int pageIndex)
        {
            return View(new AccountBookViewModel() { PageIndex = pageIndex });
        }
        #endregion
        #region 7.0 新增頁面Post + ActionResult Add(AccountBookViewModel model)
        /// <summary>
        /// 7.0 新增頁面Post
        /// </summary>
        /// <param name="model">要新增的實體</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(AccountBookViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new
                    {
                        success = false,
                        errors = ModelState.Keys.SelectMany(k => ModelState[k].Errors)
                                    .Select(m => m.ErrorMessage).ToArray()
                    });
                }
                var addModel = new AccountBook() { Amounttt = (int)model.Amounttt, Categoryyy = (int)model.Categoryyy, Dateee = (DateTime)model.Dateee, Remarkkk = model.Remarkkk, Id = Guid.NewGuid() };
                Instance.AccountBooks.Add(addModel);
                Instance.SaveChanges();
                MemoryCache.Default.Remove("listBooks");
                return Json(new { success = true,pageIndex = model.PageIndex });
            }
            catch (DbEntityValidationException ex)
            {
                Helpers.GetDbError(ex);
                return Json(new { success = false });
            }
        }
        #endregion
    }
}
