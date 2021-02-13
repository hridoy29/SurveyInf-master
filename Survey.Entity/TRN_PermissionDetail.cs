using System;
using System.Text;

namespace SurveyEntity
{
	public class TRN_PermissionDetail
	{
		public Int64 PermissionDetailId { get; set; }
		public Int64 PermissionId { get; set; }
		public Int32 ScreenDetailId { get; set; }
		public Int32 ScreenId { get; set; }
		
		public bool CanExecute { get; set; }
		public bool CanView { get; set; }
        public string ScreenName { get; set; }
        public string FunctionName { get; set; }
        public string ModuleName { get; set; }
        public string SQLMessage { get; set; }
    }
}
