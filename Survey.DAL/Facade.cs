using Survey.DAL;

namespace SurveyDAL
{
    public static class Facade
    {
        public static TRN_PermissionDetailDAO TRN_PermissionDetailDAO { get { return new TRN_PermissionDetailDAO(); } }
        public static LU_EmployeeDAO LU_EmployeeDAO { get { return new LU_EmployeeDAO(); } }
        public static LU_CommentsTypeDAO LU_CommentsTypeDAO { get { return new LU_CommentsTypeDAO(); } }
        public static LU_AICDAO LU_AICDAO { get { return new LU_AICDAO(); } }
        public static LU_ASMDAO LU_ASMDAO { get { return new LU_ASMDAO(); } }
        public static LU_DistributorlistDAO LU_DistributorlistDAO { get { return new LU_DistributorlistDAO(); } }

        public static LU_ScreenDetailDAO LU_ScreenDetailDAO { get { return new LU_ScreenDetailDAO(); } }
        public static TRN_PermissionDAO TRN_PermissionDAO { get { return new TRN_PermissionDAO(); } }
        public static LU_ScreenDAO LU_ScreenDAO { get { return new LU_ScreenDAO(); } }

        public static LU_UserGroupDAO LU_UserGroupDAO { get { return new LU_UserGroupDAO(); } }
        public static LU_UserDAO LU_UserDAO { get { return new LU_UserDAO(); } }
        public static LU_CommnetsDAO LU_CommnetsDAO { get { return new LU_CommnetsDAO(); } }
        
    }
}
