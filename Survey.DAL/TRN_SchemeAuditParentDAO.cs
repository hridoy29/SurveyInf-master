using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using System.Data.Common;
using DbExecutor;
using SurveyEntity;

namespace SurveyDAL
{
	public class TRN_SchemeAuditParentDAO : IDisposable
	{
		private static volatile TRN_SchemeAuditParentDAO instance;
		private static readonly object lockObj = new object();
		public static TRN_SchemeAuditParentDAO GetInstance()
		{
			if (instance == null)
			{
				instance = new TRN_SchemeAuditParentDAO();
			}
			return instance;
		}
		public static TRN_SchemeAuditParentDAO GetInstanceThreadSafe
		{
			get
			{
				if (instance == null)
				{
					lock (lockObj)
					{
						if (instance == null)
						{
							instance = new TRN_SchemeAuditParentDAO();
						}
					}
				}
				return instance;
			}
		}

		public void Dispose()
		{
			((IDisposable)GetInstanceThreadSafe).Dispose();
		}

		DBExecutor dbExecutor;

		public TRN_SchemeAuditParentDAO()
		{
			//dbExecutor = DBExecutor.GetInstanceThreadSafe;
			dbExecutor = new DBExecutor();
		}

		public List<TRN_SchemeAuditParent> Get(Int32? id = null)
		{
			try
			{
				List<TRN_SchemeAuditParent> TRN_SchemeAuditParentLst = new List<TRN_SchemeAuditParent>();
				Parameters[] colparameters = new Parameters[1]{
				new Parameters("@paramId", id, DbType.Int32, ParameterDirection.Input)
				};
				TRN_SchemeAuditParentLst = dbExecutor.FetchData<TRN_SchemeAuditParent>(CommandType.StoredProcedure, "wsp_TRN_SchemeAuditParent_Get", colparameters);
				return TRN_SchemeAuditParentLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public List<TRN_SchemeAuditParent> GetDynamic(string whereCondition,string orderByExpression)
		{
			try
			{
				List<TRN_SchemeAuditParent> TRN_SchemeAuditParentLst = new List<TRN_SchemeAuditParent>();
				Parameters[] colparameters = new Parameters[2]{
				new Parameters("@paramWhereCondition", whereCondition, DbType.String, ParameterDirection.Input),
				new Parameters("@paramOrderByExpression", orderByExpression, DbType.String, ParameterDirection.Input),
				};
				TRN_SchemeAuditParentLst = dbExecutor.FetchData<TRN_SchemeAuditParent>(CommandType.StoredProcedure, "wsp_TRN_SchemeAuditParent_GetDynamic", colparameters);
				return TRN_SchemeAuditParentLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public List<TRN_SchemeAuditParent> GetPaged(int startRecordNo, int rowPerPage, string whereClause, string sortColumn, string sortOrder, ref int rows)
		{
			try
			{
				List<TRN_SchemeAuditParent> TRN_SchemeAuditParentLst = new List<TRN_SchemeAuditParent>();
				Parameters[] colparameters = new Parameters[5]{
				new Parameters("@StartRecordNo", startRecordNo, DbType.Int32, ParameterDirection.Input),
				new Parameters("@RowPerPage", rowPerPage, DbType.Int32, ParameterDirection.Input),
				new Parameters("@WhereClause", whereClause, DbType.String, ParameterDirection.Input),
				new Parameters("@SortColumn", sortColumn, DbType.String, ParameterDirection.Input),
				new Parameters("@SortOrder", sortOrder, DbType.String, ParameterDirection.Input),
				};
				TRN_SchemeAuditParentLst = dbExecutor.FetchDataRef<TRN_SchemeAuditParent>(CommandType.StoredProcedure, "TRN_SchemeAuditParent_GetPaged", colparameters, ref rows);
				return TRN_SchemeAuditParentLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public string Post(TRN_SchemeAuditParent _TRN_SchemeAuditParent, string transactionType)
		{
			string ret = string.Empty;
			try
			{
				Parameters[] colparameters = new Parameters[40]{
				new Parameters("@paramId", _TRN_SchemeAuditParent.Id, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramNumber", _TRN_SchemeAuditParent.Number, DbType.String, ParameterDirection.Input),
				new Parameters("@paramUserId", _TRN_SchemeAuditParent.UserId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramOutlateName", _TRN_SchemeAuditParent.OutlateName, DbType.String, ParameterDirection.Input),
				new Parameters("@paramDate", _TRN_SchemeAuditParent.Date, DbType.Date, ParameterDirection.Input),
				new Parameters("@paramGccCode", _TRN_SchemeAuditParent.GccCode, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramRetailSellerName", _TRN_SchemeAuditParent.RetailSellerName, DbType.String, ParameterDirection.Input),
				new Parameters("@paramMobileNumber", _TRN_SchemeAuditParent.MobileNumber, DbType.String, ParameterDirection.Input),
				new Parameters("@paramOutlateTypeId", _TRN_SchemeAuditParent.OutlateTypeId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramVisitedDate", _TRN_SchemeAuditParent.VisitedDate, DbType.Date, ParameterDirection.Input),
				new Parameters("@paramDistributorName", _TRN_SchemeAuditParent.DistributorName, DbType.String, ParameterDirection.Input),
				new Parameters("@paramAsmId", _TRN_SchemeAuditParent.AsmId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramAicId", _TRN_SchemeAuditParent.AicId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramOutlateAddress", _TRN_SchemeAuditParent.OutlateAddress, DbType.String, ParameterDirection.Input),
				new Parameters("@paramIsKnowenAboutScheme", _TRN_SchemeAuditParent.IsKnowenAboutScheme, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramSchemeDetails", _TRN_SchemeAuditParent.SchemeDetails, DbType.String, ParameterDirection.Input),
				new Parameters("@paramSchemeMediaTypeId", _TRN_SchemeAuditParent.SchemeMediaTypeId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramIsFacilitatedByScheme", _TRN_SchemeAuditParent.IsFacilitatedByScheme, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramDateOfScheme", _TRN_SchemeAuditParent.DateOfScheme, DbType.Date, ParameterDirection.Input),
				new Parameters("@paramIsWrittenRecordAvailable", _TRN_SchemeAuditParent.IsWrittenRecordAvailable, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramLatestChallanDate", _TRN_SchemeAuditParent.LatestChallanDate, DbType.Date, ParameterDirection.Input),
				new Parameters("@paramChallanAmount", _TRN_SchemeAuditParent.ChallanAmount, DbType.String, ParameterDirection.Input),
				new Parameters("@paramDoesGotAnyChallan", _TRN_SchemeAuditParent.DoesGotAnyChallan, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramChallanTypeId", _TRN_SchemeAuditParent.ChallanTypeId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramDoesExpiredProductAvailable", _TRN_SchemeAuditParent.DoesExpiredProductAvailable, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramDoesSatisfiedWithSallesOfficer", _TRN_SchemeAuditParent.DoesSatisfiedWithSallesOfficer, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramDoesSatisfiedWithProductOrderAndService", _TRN_SchemeAuditParent.DoesSatisfiedWithProductOrderAndService, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramSallesOfficerVisitingDay", _TRN_SchemeAuditParent.SallesOfficerVisitingDay, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramDoesGotLatestDiscountOffer", _TRN_SchemeAuditParent.DoesGotLatestDiscountOffer, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramWillGetAnyDiscountOfferFromDistributor", _TRN_SchemeAuditParent.WillGetAnyDiscountOfferFromDistributor, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramDoesCocaColaLabelAvailable", _TRN_SchemeAuditParent.DoesCocaColaLabelAvailable, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramIsGccCodeAvailable", _TRN_SchemeAuditParent.IsGccCodeAvailable, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramCommentsType", _TRN_SchemeAuditParent.CommentsType, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramComments", _TRN_SchemeAuditParent.Comments, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramCommentDetails", _TRN_SchemeAuditParent.CommentDetails, DbType.String, ParameterDirection.Input),
				new Parameters("@paramCreatorName", _TRN_SchemeAuditParent.CreatorName, DbType.Byte, ParameterDirection.Input),
				new Parameters("@paramCreationDate", _TRN_SchemeAuditParent.CreationDate, DbType.DateTime, ParameterDirection.Input),
				new Parameters("@paramModifierName", _TRN_SchemeAuditParent.ModifierName, DbType.Byte, ParameterDirection.Input),
				new Parameters("@paramModificationDate", _TRN_SchemeAuditParent.ModificationDate, DbType.DateTime, ParameterDirection.Input),
				new Parameters("@paramTransactionType", transactionType, DbType.String, ParameterDirection.Input)
				};
				dbExecutor.ManageTransaction(TransactionType.Open);
				ret = dbExecutor.ExecuteScalarString(true, CommandType.StoredProcedure, "wsp_TRN_SchemeAuditParent_Post", colparameters, true);
				dbExecutor.ManageTransaction(TransactionType.Commit);
			}
			catch (DBConcurrencyException except)
			{
				dbExecutor.ManageTransaction(TransactionType.Rollback);
				throw except;
			}
			catch (Exception ex)
			{
				dbExecutor.ManageTransaction(TransactionType.Rollback);
				throw ex;
			}
			return ret;
		}
	}
}
