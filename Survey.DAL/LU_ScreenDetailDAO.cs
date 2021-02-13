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
	public class LU_ScreenDetailDAO : IDisposable
	{
		private static volatile LU_ScreenDetailDAO instance;
		private static readonly object lockObj = new object();
		public static LU_ScreenDetailDAO GetInstance()
		{
			if (instance == null)
			{
				instance = new LU_ScreenDetailDAO();
			}
			return instance;
		}
		public static LU_ScreenDetailDAO GetInstanceThreadSafe
		{
			get
			{
				if (instance == null)
				{
					lock (lockObj)
					{
						if (instance == null)
						{
							instance = new LU_ScreenDetailDAO();
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

		public LU_ScreenDetailDAO()
		{
			//dbExecutor = DBExecutor.GetInstanceThreadSafe;
			dbExecutor = new DBExecutor();
		}

		public List<LU_ScreenDetail> Get(Int32? screenDetailId = null)
		{
			try
			{
				List<LU_ScreenDetail> LU_ScreenDetailLst = new List<LU_ScreenDetail>();
				Parameters[] colparameters = new Parameters[1]{
				new Parameters("@paramScreenDetailId", screenDetailId, DbType.Int32, ParameterDirection.Input)
				};
				LU_ScreenDetailLst = dbExecutor.FetchData<LU_ScreenDetail>(CommandType.StoredProcedure, "wsp_LU_ScreenDetail_Get", colparameters);
				return LU_ScreenDetailLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public List<LU_ScreenDetail> GetByScreenId(Int32 screenId)
		{
			try
			{
				List<LU_ScreenDetail> s_ScreenDetailLst = new List<LU_ScreenDetail>();
				Parameters[] colparameters = new Parameters[1]{
				new Parameters("@ScreenId", screenId, DbType.Int32, ParameterDirection.Input)
				};
				s_ScreenDetailLst = dbExecutor.FetchData<LU_ScreenDetail>(CommandType.StoredProcedure, "s_ScreenDetail_GetByScreenId", colparameters);
				return s_ScreenDetailLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public List<LU_ScreenDetail> GetDynamic(string whereCondition,string orderByExpression)
		{
			try
			{
				List<LU_ScreenDetail> LU_ScreenDetailLst = new List<LU_ScreenDetail>();
				Parameters[] colparameters = new Parameters[2]{
				new Parameters("@paramWhereCondition", whereCondition, DbType.String, ParameterDirection.Input),
				new Parameters("@paramOrderByExpression", orderByExpression, DbType.String, ParameterDirection.Input),
				};
				LU_ScreenDetailLst = dbExecutor.FetchData<LU_ScreenDetail>(CommandType.StoredProcedure, "wsp_LU_ScreenDetail_GetDynamic", colparameters);
				return LU_ScreenDetailLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public List<LU_ScreenDetail> GetPaged(int startRecordNo, int rowPerPage, string whereClause, string sortColumn, string sortOrder, ref int rows)
		{
			try
			{
				List<LU_ScreenDetail> LU_ScreenDetailLst = new List<LU_ScreenDetail>();
				Parameters[] colparameters = new Parameters[5]{
				new Parameters("@StartRecordNo", startRecordNo, DbType.Int32, ParameterDirection.Input),
				new Parameters("@RowPerPage", rowPerPage, DbType.Int32, ParameterDirection.Input),
				new Parameters("@WhereClause", whereClause, DbType.String, ParameterDirection.Input),
				new Parameters("@SortColumn", sortColumn, DbType.String, ParameterDirection.Input),
				new Parameters("@SortOrder", sortOrder, DbType.String, ParameterDirection.Input),
				};
				LU_ScreenDetailLst = dbExecutor.FetchDataRef<LU_ScreenDetail>(CommandType.StoredProcedure, "LU_ScreenDetail_GetPaged", colparameters, ref rows);
				return LU_ScreenDetailLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public string Post(LU_ScreenDetail _LU_ScreenDetail, string transactionType)
		{
			string ret = string.Empty;
			try
			{
				Parameters[] colparameters = new Parameters[4]{
				new Parameters("@paramScreenDetailId", _LU_ScreenDetail.ScreenDetailId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramScreenId", _LU_ScreenDetail.ScreenId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramFunctionId", _LU_ScreenDetail.FunctionId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramTransactionType", transactionType, DbType.String, ParameterDirection.Input)
				};
				dbExecutor.ManageTransaction(TransactionType.Open);
				ret = dbExecutor.ExecuteScalarString(true, CommandType.StoredProcedure, "wsp_LU_ScreenDetail_Post", colparameters, true);
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
