using CMS.Web.Areas.User_management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Web.Areas.User_management.Constants
{
    public class Permissions
    {
        public static List<PermissionModel> getPermissions()
        {
            return new List<PermissionModel>()
            {
                   new PermissionModel()
                {
                    permission_name=getBillingPermissionName(),
                    is_checked=false
                },
                 new PermissionModel()
                {
                    permission_name=getAccountPermissionName(),
                    is_checked=false
                }, new PermissionModel()
                {
                    permission_name=getUserManagementPermissionName(),
                    is_checked=false
                }, new PermissionModel()
                {
                    permission_name=getTableManipulationPermissionName(),
                    is_checked=false
                }
            };
        }

        public static string getUserManagementPermissionName() => "User management";
        public static string getTableManipulationPermissionName() => "Table Management";
        public static string getBillingPermissionName() => "Billing";
        public static string getAccountPermissionName() => "Account";
    }
}
