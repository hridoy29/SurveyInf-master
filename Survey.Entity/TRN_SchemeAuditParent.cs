using System;
using System.Text;

namespace SurveyEntity
{
	public class TRN_SchemeAuditParent
	{
		public Int32 Id { get; set; }
		public string Number { get; set; }
		public Int32 UserId { get; set; }
		public string OutlateName { get; set; }
		public string Date { get; set; }
		public Int32 GccCode { get; set; }
		public string RetailSellerName { get; set; }
		public string MobileNumber { get; set; }
		public Int32 OutlateTypeId { get; set; }
		public string VisitedDate { get; set; }
		public string DistributorName { get; set; }
		public Int32 AsmId { get; set; }
		public Int32 AicId { get; set; }
		public string OutlateAddress { get; set; }
		public Int32 IsKnowenAboutScheme { get; set; }
		public string SchemeDetails { get; set; }
		public Int32 SchemeMediaTypeId { get; set; }
		public Int32 IsFacilitatedByScheme { get; set; }
		public string DateOfScheme { get; set; }
		public Int32? IsWrittenRecordAvailable { get; set; }
		public string LatestChallanDate { get; set; }
		public string ChallanAmount { get; set; }
		public Int32? DoesGotAnyChallan { get; set; }
		public Int32? ChallanTypeId { get; set; }
		public Int32? DoesExpiredProductAvailable { get; set; }
		public Int32? DoesSatisfiedWithSallesOfficer { get; set; }
		public Int32? DoesSatisfiedWithProductOrderAndService { get; set; }
		public Int32? SallesOfficerVisitingDay { get; set; }
		public Int32? DoesGotLatestDiscountOffer { get; set; }
		public Int32? WillGetAnyDiscountOfferFromDistributor { get; set; }
		public Int32? DoesCocaColaLabelAvailable { get; set; }
		public Int32? IsGccCodeAvailable { get; set; }
		public Int32? CommentsType { get; set; }
		public Int32? Comments { get; set; }
		public string CommentDetails { get; set; }
		public SByte CreatorName { get; set; }
		public string CreationDate { get; set; }
		public SByte ModifierName { get; set; }
		public string ModificationDate { get; set; }
	}
}
