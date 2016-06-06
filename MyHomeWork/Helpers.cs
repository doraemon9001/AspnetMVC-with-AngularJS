namespace MyHomeWork
{
    using System;
    using System.Data.Entity.Validation;
    using System.Linq;
    using System.Text.RegularExpressions;

    /// <summary>
    /// 專案共用工具類 
    /// </summary>
    public static class Helpers
    {
        #region 1.0 檢查字串是否可以轉為數字
        /// <summary>
        /// 1.0 檢查字串是否可以轉為數字
        /// </summary>
        /// <param name="str">要轉換的字串</param>
        /// <returns>字串轉數字</returns>
        public static int CheckIdIsInt(this string str)
        {
            if (!new Regex("^[0-9]*[1-9][0-9]*$").IsMatch(str) || string.IsNullOrEmpty(str)) return -1;
            return Convert.ToInt32(str);
        }
        #endregion
        #region 2.0 驗證日期，true 表示大於當前日期時間
        /// <summary>
        /// 驗證日期，true 表示大於當前日期時間
        /// </summary>
        /// <param name="dateTime">填寫的日期</param>
        /// <returns>boo</returns>
        public static bool DateDiff(this DateTime? dateTime, DateTime time)
        {
            return time > DateTime.Now;
        }
        #endregion
        #region 3.0 獲取Db錯誤
        /// <summary>
        /// 3.0 獲取Db錯誤
        /// </summary>
        /// <param name="ex">DbEntityValidationException</param>
        /// <returns>Db錯誤字串</returns>
        public static string GetDbError(DbEntityValidationException ex)
        {
            var entityError = ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.ErrorMessage);
            var getFullMessage = string.Join("; ", entityError);
            return string.Concat(ex.Message, "errors are: ", getFullMessage);
        }
        #endregion
    }
}