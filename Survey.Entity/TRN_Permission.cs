using System;
using System.Text;

namespace SurveyEntity
{
	public class TRN_Permission
	{
		public Int64 PermissionId { get; set; }
		public Int32 UserGroupId { get; set; }
		public Int32 ScreenId { get; set; }
		public bool CanView { get; set; }
		public Int32 CreatorId { get; set; }
		public DateTime CreateDate { get; set; }
		public Int32 UpdatorId { get; set; }
		public DateTime UpdateDate { get; set; }
	}
}
