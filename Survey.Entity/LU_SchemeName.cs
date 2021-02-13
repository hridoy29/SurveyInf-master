using System;
using System.Text;

namespace SurveyEntity
{
	public class LU_SchemeName
	{
		public Int32 Id { get; set; }
		public string Name { get; set; }
		public string ActivateFromDate { get; set; }
		public string ActivateToDate { get; set; }
		public Int32 CreatorId { get; set; }
		public string CreationDate { get; set; }
		public Int32 ModifierId { get; set; }
		public string ModificationDate { get; set; }
		public bool IsActive { get; set; }
	}
}
