namespace MyHomeWork.Common
{
    using System.Collections.Generic;
    using System;
    using System.Web.Mvc;
    using System.Data.Entity;
    using System.Linq;
    using System.Text;
    public static class baseUnityty
    {
        #region 1.0 數值檢驗
        /// <summary>
        /// 1.1 是否為整數的數字
        /// </summary>
        /// <param name="sNum"></param>
        /// <returns></returns>
        public static bool IsNumuric(this object sNum)
        {
            try
            {
                Int32.Parse(sNum.ToString());
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 1.2 是否為有小數的數字
        /// </summary>
        /// <param name="sNum"></param>
        /// <returns></returns>
        public static bool IsDecimal(this object sNum)
        {
            try
            {
                decimal.Parse(sNum.ToString());
            }
            catch
            {
                return false;
            }
            return true;
        }
        #endregion 
        #region 2.0日期檢驗
        /// <summary>
        /// 2.1 是否為日期格式
        /// </summary>
        /// <param name="Date"></param>
        /// <returns></returns>
        public static bool IsDate(this object Date)
        {
            try
            {
                string date = Date.ToString().DateFieldSaving();
                DateTime dt = new DateTime(int.Parse(date.Substring(0, 4)), int.Parse(date.Substring(4, 2)), int.Parse(date.Substring(6, 2)));
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 2.2 日期顯示格式
        /// </summary>
        /// <param name="Date"></param>
        /// <returns></returns>
        public static string DateFieldDisplaying(this object Date)
        {
            if (Date.ToString().Trim().Equals("")) return "";
            string date = Date.ToString().DateFieldSaving();
            return date.Substring(0, 4) + "/" + date.Substring(4, 2) + "/" + date.Substring(6, 2);
        }

        /// <summary>
        /// 2.3 日期存檔格式
        /// </summary>
        /// <param name="Date"></param>
        /// <returns></returns>
        static string DateFieldSaving(this object Date)
        {
            return Date.ToString().Replace("/", "").Replace("-", "");
        }
        #endregion
        #region Html Unility
        public static MvcHtmlString Button(this HtmlHelper htmlHelper, string id, string value, string className, string eventArg, string eventContent)
        {
            string rtnValue = "<input type='button' id='" + id + "' name='" + id + "' value='" + value + "' class='" + className + "' " + eventArg + "='" + eventContent + "' />";
            return new MvcHtmlString(rtnValue);
        }
        #endregion
    }
}