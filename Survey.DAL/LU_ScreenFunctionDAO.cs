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
	public class LU_ScreenFunctionDAO : IDisposable
	{
		private static volatile LU_ScreenFunctionDAO instance;
		private static readonly object lockObj = new object();
		public static LU_ScreenFunctionDAO GetInstance()
		{
			if (instance == null)
			{
				instance = new LU_ScreenFunctionDAO();
			}
			return instance;
		}
		public static LU_ScreenFunctionDAO GetInstanceThreadSafe
		{
			get
			{
				if (instance == null)
				{
					lock (lockObj)
					{
						if (instance == null)
						{
							instance = new LU_ScreenFunctionDAO();
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

		public LU_ScreenFunctionDAO()
		{
			//dbExecutor = DBExecutor.GetInstanceThreadSafe;
			dbExecutor = new DBExecutor();
		}

		public List<LU_ScreenFunction> Get(Int32? functionId = null)
		{
			try
			{
				List<LU_ScreenFunction> LU_ScreenFunctionLst = new List<LU_ScreenFunction>();
				Parameters[] colparameters = new Parameters[1]{
				new Parameters("@paramFunctionId", functionId, DbType.Int32, ParameterDirection.Input)
				};
				LU_ScreenFunctionLst = dbExecutor.FetchData<LU_ScreenFunction>(CommandType.StoredProcedure, "wsp_LU_ScreenFunction_Get", colparameters);
				return LU_ScreenFunctionLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public List<LU_ScreenFunction> GetDynamic(string whereCondition,string orderByExpression)
		{
			try
			{
				List<LU_ScreenFunction> LU_ScreenFunctionLst = new List<LU_ScreenFunction>();
				Parameters[] colparameters = new Parameters[2]{
				new Parameters("@paramWhereCondition", whereCondition, DbType.String, ParameterDirection.Input),
				new Parameters("@paramOrderByExpression", orderByExpression, DbType.String, ParameterDirection.Input),
				};
				LU_ScreenFunctionLst = dbExecutor.FetchData<LU_ScreenFunction>(CommandType.StoredProcedure, "wsp_LU_ScreenFunction_GetDynamic", colparameters);
				return LU_ScreenFunctionLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public List<LU_ScreenFunction> GetPaged(int startRecordNo, int rowPerPage, string whereClause, string sortColumn, string sortOrder, ref int rows)
		{
			try
			{
				List<LU_ScreenFunction> LU_ScreenFunctionLst = new List<LU_ScreenFunction>();
				Parameters[] colparameters = new Parameters[5]{
				new Parameters("@StartRecordNo", startRecordNo, DbType.Int32, ParameterDirection.Input),
				new Parameters("@RowPerPage", rowPerPage, DbType.Int32, ParameterDirection.Input),
				new Parameters("@WhereClause", whereClause, DbType.String, ParameterDirection.Input),
				new Parameters("@SortColumn", sortColumn, DbType.String, ParameterDirection.Input),
				new Parameters("@SortOrder", sortOrder, DbType.String, ParameterDirection.Input),
				};
				LU_ScreenFunctionLst = dbExecutor.FetchDataRef<LU_ScreenFunction>(CommandType.StoredProcedure, "LU_ScreenFunction_GetPaged", colparameters, ref rows);
				return LU_ScreenFunctionLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public string Post(LU_ScreenFunction _LU_ScreenFunction, string transactionType)
		{
			string ret = string.Empty;
			try
			{
				Parameters[] colparameters = new Parameters[3]{
				new Parameters("@paramFunctionId", _LU_ScreenFunction.FunctionId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramFunctionName", _LU_ScreenFunction.FunctionName, DbType.String, ParameterDirection.Input),
				new Parameters("@paramTransactionType", transactionType, DbType.String, ParameterDirection.Input)
				};
				dbExecutor.ManageTransaction(TransactionType.Open);
				ret = dbExecutor.ExecuteScalarString(true, CommandType.StoredProcedure, "wsp_LU_ScreenFunction_Post", colparameters, true);
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
