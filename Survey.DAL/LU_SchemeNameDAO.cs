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
	public class LU_SchemeNameDAO : IDisposable
	{
		private static volatile LU_SchemeNameDAO instance;
		private static readonly object lockObj = new object();
		public static LU_SchemeNameDAO GetInstance()
		{
			if (instance == null)
			{
				instance = new LU_SchemeNameDAO();
			}
			return instance;
		}
		public static LU_SchemeNameDAO GetInstanceThreadSafe
		{
			get
			{
				if (instance == null)
				{
					lock (lockObj)
					{
						if (instance == null)
						{
							instance = new LU_SchemeNameDAO();
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

		public LU_SchemeNameDAO()
		{
			//dbExecutor = DBExecutor.GetInstanceThreadSafe;
			dbExecutor = new DBExecutor();
		}

		public List<LU_SchemeName> Get(Int32? id = null)
		{
			try
			{
				List<LU_SchemeName> LU_SchemeNameLst = new List<LU_SchemeName>();
				Parameters[] colparameters = new Parameters[1]{
				new Parameters("@paramId", id, DbType.Int32, ParameterDirection.Input)
				};
				LU_SchemeNameLst = dbExecutor.FetchData<LU_SchemeName>(CommandType.StoredProcedure, "wsp_LU_SchemeName_Get", colparameters);
				return LU_SchemeNameLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public List<LU_SchemeName> GetDynamic(string whereCondition,string orderByExpression)
		{
			try
			{
				List<LU_SchemeName> LU_SchemeNameLst = new List<LU_SchemeName>();
				Parameters[] colparameters = new Parameters[2]{
				new Parameters("@paramWhereCondition", whereCondition, DbType.String, ParameterDirection.Input),
				new Parameters("@paramOrderByExpression", orderByExpression, DbType.String, ParameterDirection.Input),
				};
				LU_SchemeNameLst = dbExecutor.FetchData<LU_SchemeName>(CommandType.StoredProcedure, "wsp_LU_SchemeName_GetDynamic", colparameters);
				return LU_SchemeNameLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public List<LU_SchemeName> GetPaged(int startRecordNo, int rowPerPage, string whereClause, string sortColumn, string sortOrder, ref int rows)
		{
			try
			{
				List<LU_SchemeName> LU_SchemeNameLst = new List<LU_SchemeName>();
				Parameters[] colparameters = new Parameters[5]{
				new Parameters("@StartRecordNo", startRecordNo, DbType.Int32, ParameterDirection.Input),
				new Parameters("@RowPerPage", rowPerPage, DbType.Int32, ParameterDirection.Input),
				new Parameters("@WhereClause", whereClause, DbType.String, ParameterDirection.Input),
				new Parameters("@SortColumn", sortColumn, DbType.String, ParameterDirection.Input),
				new Parameters("@SortOrder", sortOrder, DbType.String, ParameterDirection.Input),
				};
				LU_SchemeNameLst = dbExecutor.FetchDataRef<LU_SchemeName>(CommandType.StoredProcedure, "LU_SchemeName_GetPaged", colparameters, ref rows);
				return LU_SchemeNameLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public string Post(LU_SchemeName _LU_SchemeName, string transactionType)
		{
			string ret = string.Empty;
			try
			{
				Parameters[] colparameters = new Parameters[10]{
				new Parameters("@paramId", _LU_SchemeName.Id, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramName", _LU_SchemeName.Name, DbType.String, ParameterDirection.Input),
				new Parameters("@paramActivateFromDate", _LU_SchemeName.ActivateFromDate, DbType.Date, ParameterDirection.Input),
				new Parameters("@paramActivateToDate", _LU_SchemeName.ActivateToDate, DbType.Date, ParameterDirection.Input),
				new Parameters("@paramCreatorId", _LU_SchemeName.CreatorId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramCreationDate", _LU_SchemeName.CreationDate, DbType.DateTime, ParameterDirection.Input),
				new Parameters("@paramModifierId", _LU_SchemeName.ModifierId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramModificationDate", _LU_SchemeName.ModificationDate, DbType.DateTime, ParameterDirection.Input),
				new Parameters("@paramIsActive", _LU_SchemeName.IsActive, DbType.Boolean, ParameterDirection.Input),
				new Parameters("@paramTransactionType", transactionType, DbType.String, ParameterDirection.Input)
				};
				dbExecutor.ManageTransaction(TransactionType.Open);
				ret = dbExecutor.ExecuteScalarString(true, CommandType.StoredProcedure, "wsp_LU_SchemeName_Post", colparameters, true);
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
