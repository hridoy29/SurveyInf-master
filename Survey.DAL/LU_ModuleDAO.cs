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
	public class LU_ModuleDAO : IDisposable
	{
		private static volatile LU_ModuleDAO instance;
		private static readonly object lockObj = new object();
		public static LU_ModuleDAO GetInstance()
		{
			if (instance == null)
			{
				instance = new LU_ModuleDAO();
			}
			return instance;
		}
		public static LU_ModuleDAO GetInstanceThreadSafe
		{
			get
			{
				if (instance == null)
				{
					lock (lockObj)
					{
						if (instance == null)
						{
							instance = new LU_ModuleDAO();
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

		public LU_ModuleDAO()
		{
			//dbExecutor = DBExecutor.GetInstanceThreadSafe;
			dbExecutor = new DBExecutor();
		}

		public List<LU_Module> Get(Int32? moduleId = null)
		{
			try
			{
				List<LU_Module> LU_ModuleLst = new List<LU_Module>();
				Parameters[] colparameters = new Parameters[1]{
				new Parameters("@paramModuleId", moduleId, DbType.Int32, ParameterDirection.Input)
				};
				LU_ModuleLst = dbExecutor.FetchData<LU_Module>(CommandType.StoredProcedure, "wsp_LU_Module_Get", colparameters);
				return LU_ModuleLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public List<LU_Module> GetDynamic(string whereCondition,string orderByExpression)
		{
			try
			{
				List<LU_Module> LU_ModuleLst = new List<LU_Module>();
				Parameters[] colparameters = new Parameters[2]{
				new Parameters("@paramWhereCondition", whereCondition, DbType.String, ParameterDirection.Input),
				new Parameters("@paramOrderByExpression", orderByExpression, DbType.String, ParameterDirection.Input),
				};
				LU_ModuleLst = dbExecutor.FetchData<LU_Module>(CommandType.StoredProcedure, "wsp_LU_Module_GetDynamic", colparameters);
				return LU_ModuleLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public List<LU_Module> GetPaged(int startRecordNo, int rowPerPage, string whereClause, string sortColumn, string sortOrder, ref int rows)
		{
			try
			{
				List<LU_Module> LU_ModuleLst = new List<LU_Module>();
				Parameters[] colparameters = new Parameters[5]{
				new Parameters("@StartRecordNo", startRecordNo, DbType.Int32, ParameterDirection.Input),
				new Parameters("@RowPerPage", rowPerPage, DbType.Int32, ParameterDirection.Input),
				new Parameters("@WhereClause", whereClause, DbType.String, ParameterDirection.Input),
				new Parameters("@SortColumn", sortColumn, DbType.String, ParameterDirection.Input),
				new Parameters("@SortOrder", sortOrder, DbType.String, ParameterDirection.Input),
				};
				LU_ModuleLst = dbExecutor.FetchDataRef<LU_Module>(CommandType.StoredProcedure, "LU_Module_GetPaged", colparameters, ref rows);
				return LU_ModuleLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public string Post(LU_Module _LU_Module, string transactionType)
		{
			string ret = string.Empty;
			try
			{
				Parameters[] colparameters = new Parameters[3]{
				new Parameters("@paramModuleId", _LU_Module.ModuleId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramModuleName", _LU_Module.ModuleName, DbType.String, ParameterDirection.Input),
				new Parameters("@paramTransactionType", transactionType, DbType.String, ParameterDirection.Input)
				};
				dbExecutor.ManageTransaction(TransactionType.Open);
				ret = dbExecutor.ExecuteScalarString(true, CommandType.StoredProcedure, "wsp_LU_Module_Post", colparameters, true);
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
