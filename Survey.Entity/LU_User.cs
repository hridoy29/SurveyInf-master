using System;
using System.Text;

namespace SurveyEntity
{
	public class LU_User
	{
		public Int32 Id { get; set; }
		public Int32 UserGroupId { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string MobileNo { get; set; }
		public string Password { get; set; }
		public Int32 CreatorId { get; set; }
		public DateTime CreationDate { get; set; }
		public Int32 ModifierId { get; set; }
		public DateTime ModificationDate { get; set; }
		public bool IsActive { get; set; }
		public string GroupName { get; set; }
	}
}
