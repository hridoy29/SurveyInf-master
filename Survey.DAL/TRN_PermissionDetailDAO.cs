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
	public class TRN_PermissionDetailDAO : IDisposable
	{
		private static volatile TRN_PermissionDetailDAO instance;
		private static readonly object lockObj = new object();
		public static TRN_PermissionDetailDAO GetInstance()
		{
			if (instance == null)
			{
				instance = new TRN_PermissionDetailDAO();
			}
			return instance;
		}
		public static TRN_PermissionDetailDAO GetInstanceThreadSafe
		{
			get
			{
				if (instance == null)
				{
					lock (lockObj)
					{
						if (instance == null)
						{
							instance = new TRN_PermissionDetailDAO();
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

		public TRN_PermissionDetailDAO()
		{
			//dbExecutor = DBExecutor.GetInstanceThreadSafe;
			dbExecutor = new DBExecutor();
		}

		public List<TRN_PermissionDetail> GetWebUserForLogin(string email, string passcode)
        {
			try
			{
				List<TRN_PermissionDetail> TRN_PermissionDetailLst = new List<TRN_PermissionDetail>();
				Parameters[] colparameters = new Parameters[2]{
				new Parameters("@paramEmail", email, DbType.String, ParameterDirection.Input),
				new Parameters("@paramPassword", passcode, DbType.String, ParameterDirection.Input)
                };
				TRN_PermissionDetailLst = dbExecutor.FetchData<TRN_PermissionDetail>(CommandType.StoredProcedure, "wsp_TRN_PermissionDetail_GetWebUserForLogin", colparameters);
				return TRN_PermissionDetailLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

        public List<TRN_PermissionDetail> GetByUserGroupId(int userGroupId)
        {
            try
            {
                List<TRN_PermissionDetail> TRN_PermissionDetailLst = new List<TRN_PermissionDetail>();
                Parameters[] colparameters = new Parameters[1]{
                new Parameters("@UserGroupId", userGroupId, DbType.Int32, ParameterDirection.Input)
                };
                TRN_PermissionDetailLst = dbExecutor.FetchData<TRN_PermissionDetail>(CommandType.StoredProcedure, "wsp_TRN_PermissionDetail_GetByUserGroupId", colparameters);
                return TRN_PermissionDetailLst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TRN_PermissionDetail> GetDynamic(string whereCondition,string orderByExpression)
		{
			try
			{
				List<TRN_PermissionDetail> TRN_PermissionDetailLst = new List<TRN_PermissionDetail>();
				Parameters[] colparameters = new Parameters[2]{
				new Parameters("@paramWhereCondition", whereCondition, DbType.String, ParameterDirection.Input),
				new Parameters("@paramOrderByExpression", orderByExpression, DbType.String, ParameterDirection.Input),
				};
				TRN_PermissionDetailLst = dbExecutor.FetchData<TRN_PermissionDetail>(CommandType.StoredProcedure, "wsp_TRN_PermissionDetail_GetDynamic", colparameters);
				return TRN_PermissionDetailLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public List<TRN_PermissionDetail> GetPaged(int startRecordNo, int rowPerPage, string whereClause, string sortColumn, string sortOrder, ref int rows)
		{
			try
			{
				List<TRN_PermissionDetail> TRN_PermissionDetailLst = new List<TRN_PermissionDetail>();
				Parameters[] colparameters = new Parameters[5]{
				new Parameters("@StartRecordNo", startRecordNo, DbType.Int32, ParameterDirection.Input),
				new Parameters("@RowPerPage", rowPerPage, DbType.Int32, ParameterDirection.Input),
				new Parameters("@WhereClause", whereClause, DbType.String, ParameterDirection.Input),
				new Parameters("@SortColumn", sortColumn, DbType.String, ParameterDirection.Input),
				new Parameters("@SortOrder", sortOrder, DbType.String, ParameterDirection.Input),
				};
				TRN_PermissionDetailLst = dbExecutor.FetchDataRef<TRN_PermissionDetail>(CommandType.StoredProcedure, "TRN_PermissionDetail_GetPaged", colparameters, ref rows);
				return TRN_PermissionDetailLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public string Post(TRN_PermissionDetail _TRN_PermissionDetail, string transactionType)
		{
			string ret = string.Empty;
			try
			{
				Parameters[] colparameters = new Parameters[5]{
				new Parameters("@paramPermissionDetailId", _TRN_PermissionDetail.PermissionDetailId, DbType.Int64, ParameterDirection.Input),
				new Parameters("@paramPermissionId", _TRN_PermissionDetail.PermissionId, DbType.Int64, ParameterDirection.Input),
				new Parameters("@paramScreenDetailId", _TRN_PermissionDetail.ScreenDetailId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramCanExecute", _TRN_PermissionDetail.CanExecute, DbType.Boolean, ParameterDirection.Input),
				new Parameters("@paramTransactionType", transactionType, DbType.String, ParameterDirection.Input)
				};
				dbExecutor.ManageTransaction(TransactionType.Open);
				ret = dbExecutor.ExecuteScalarString(true, CommandType.StoredProcedure, "wsp_TRN_PermissionDetail_Post", colparameters, true);
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
