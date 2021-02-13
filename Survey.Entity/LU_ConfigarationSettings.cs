using System;
using System.Text;

namespace SurveyEntity
{
	public class LU_ConfigarationSettings
	{
		public Int32 Id { get; set; }
		public string Config1 { get; set; }
		public Int32 CreatorId { get; set; }
		public string CreationDate { get; set; }
		public Int32 ModifierId { get; set; }
		public Int32 EmployeeTypeId { get; set; }
		public string ModificationDate { get; set; }
		public bool IsActive { get; set; }
	}
}
