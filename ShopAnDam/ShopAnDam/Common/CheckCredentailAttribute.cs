using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopAnDam.Common
{
    public class CheckCredentailAttribute : AuthorizeAttribute
    {
        public string RoleID { set; get; }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            //var isAuthorized = base.AuthorizeCore(httpContext);
            //  if (!isAuthorized)
            //{
            //    return false;
            //}
            var session = (UserLogin)HttpContext.Current.Session[Common.CommonConStants.USER_SESSION];
           if(session == null)
            {
                return false;
            }
            List<string> privileLevel = this.GetCredentialByLoggedInUser(session.UserName); // gọi phương thức khác để lấy ra tên nguwofi dùng
            if (privileLevel.Contains(this.RoleID) || session.GroupID == CommonConStants.SUPPER_GROUP)
            {
                return true;
            }
            else
            {
                return false;
            }
            // return base.AuthorizeCore(httpContext);
        }
        //protected override void HandleUnauthorizeRequest(AuthorizationContext filterContext)
        //{
        //    filterContext.Result = new ViewResult
        //    {
        //        ViewName = "~/Areas/Admin/Views/Shared/401.cshtml"
        //    };
      //  }

            private List<string> GetCredentialByLoggedInUser(string UserName)
        {

            var credentails = (List<string>)HttpContext.Current.Session[Common.CommonConStants.SESSION_PERMITIONS];
            return credentails;
        }

    }
}