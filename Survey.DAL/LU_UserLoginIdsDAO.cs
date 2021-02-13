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
	public class LU_UserLoginIdsDAO : IDisposable
	{
		private static volatile LU_UserLoginIdsDAO instance;
		private static readonly object lockObj = new object();
		public static LU_UserLoginIdsDAO GetInstance()
		{
			if (instance == null)
			{
				instance = new LU_UserLoginIdsDAO();
			}
			return instance;
		}
		public static LU_UserLoginIdsDAO GetInstanceThreadSafe
		{
			get
			{
				if (instance == null)
				{
					lock (lockObj)
					{
						if (instance == null)
						{
							instance = new LU_UserLoginIdsDAO();
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

		public LU_UserLoginIdsDAO()
		{
			//dbExecutor = DBExecutor.GetInstanceThreadSafe;
			dbExecutor = new DBExecutor();
		}

		public List<LU_UserLoginIds> Get(Int32? id = null)
		{
			try
			{
				List<LU_UserLoginIds> LU_UserLoginIdsLst = new List<LU_UserLoginIds>();
				Parameters[] colparameters = new Parameters[1]{
				new Parameters("@paramId", id, DbType.Int32, ParameterDirection.Input)
				};
				LU_UserLoginIdsLst = dbExecutor.FetchData<LU_UserLoginIds>(CommandType.StoredProcedure, "wsp_LU_UserLoginIds_Get", colparameters);
				return LU_UserLoginIdsLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public List<LU_UserLoginIds> GetDynamic(string whereCondition,string orderByExpression)
		{
			try
			{
				List<LU_UserLoginIds> LU_UserLoginIdsLst = new List<LU_UserLoginIds>();
				Parameters[] colparameters = new Parameters[2]{
				new Parameters("@paramWhereCondition", whereCondition, DbType.String, ParameterDirection.Input),
				new Parameters("@paramOrderByExpression", orderByExpression, DbType.String, ParameterDirection.Input),
				};
				LU_UserLoginIdsLst = dbExecutor.FetchData<LU_UserLoginIds>(CommandType.StoredProcedure, "wsp_LU_UserLoginIds_GetDynamic", colparameters);
				return LU_UserLoginIdsLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public List<LU_UserLoginIds> GetPaged(int startRecordNo, int rowPerPage, string whereClause, string sortColumn, string sortOrder, ref int rows)
		{
			try
			{
				List<LU_UserLoginIds> LU_UserLoginIdsLst = new List<LU_UserLoginIds>();
				Parameters[] colparameters = new Parameters[5]{
				new Parameters("@StartRecordNo", startRecordNo, DbType.Int32, ParameterDirection.Input),
				new Parameters("@RowPerPage", rowPerPage, DbType.Int32, ParameterDirection.Input),
				new Parameters("@WhereClause", whereClause, DbType.String, ParameterDirection.Input),
				new Parameters("@SortColumn", sortColumn, DbType.String, ParameterDirection.Input),
				new Parameters("@SortOrder", sortOrder, DbType.String, ParameterDirection.Input),
				};
				LU_UserLoginIdsLst = dbExecutor.FetchDataRef<LU_UserLoginIds>(CommandType.StoredProcedure, "LU_UserLoginIds_GetPaged", colparameters, ref rows);
				return LU_UserLoginIdsLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public string Post(LU_UserLoginIds _LU_UserLoginIds, string transactionType)
		{
			string ret = string.Empty;
			try
			{
				Parameters[] colparameters = new Parameters[4]{
				new Parameters("@paramId", _LU_UserLoginIds.Id, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramLogInID", _LU_UserLoginIds.LogInID, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramIsDefault", _LU_UserLoginIds.IsDefault, DbType.Boolean, ParameterDirection.Input),
				new Parameters("@paramTransactionType", transactionType, DbType.String, ParameterDirection.Input)
				};
				dbExecutor.ManageTransaction(TransactionType.Open);
				ret = dbExecutor.ExecuteScalarString(true, CommandType.StoredProcedure, "wsp_LU_UserLoginIds_Post", colparameters, true);
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
