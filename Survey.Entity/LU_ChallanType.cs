using System;
using System.Text;

namespace SurveyEntity
{
	public class LU_ChallanType
	{
		public Int32 Id { get; set; }
		public string Name { get; set; }
		public Int32 CreatorId { get; set; }
		public string CreationDate { get; set; }
		public Int32 ModifierId { get; set; }
		public string ModificationDate { get; set; }
		public bool IsActive { get; set; }
	}
}
