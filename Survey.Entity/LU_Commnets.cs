using System;
using System.Text;

namespace SurveyEntity
{
	public class LU_Commnets
	{
		public Int32 Id { get; set; }
		public Int32 CommentsTypeId { get; set; }
		public string CommentsType { get; set; }
		public string Comments { get; set; }
		public Int32 CreatorId { get; set; }
		public DateTime CreationDate { get; set; }
		public Int32 ModifierId { get; set; }
		public DateTime ModificationDate { get; set; }
		public bool IsActive { get; set; }
	}
}
