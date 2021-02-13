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
	public class LU_ChallanTypeDAO : IDisposable
	{
		private static volatile LU_ChallanTypeDAO instance;
		private static readonly object lockObj = new object();
		public static LU_ChallanTypeDAO GetInstance()
		{
			if (instance == null)
			{
				instance = new LU_ChallanTypeDAO();
			}
			return instance;
		}
		public static LU_ChallanTypeDAO GetInstanceThreadSafe
		{
			get
			{
				if (instance == null)
				{
					lock (lockObj)
					{
						if (instance == null)
						{
							instance = new LU_ChallanTypeDAO();
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

		public LU_ChallanTypeDAO()
		{
			//dbExecutor = DBExecutor.GetInstanceThreadSafe;
			dbExecutor = new DBExecutor();
		}

		public List<LU_ChallanType> Get(Int32? id = null)
		{
			try
			{
				List<LU_ChallanType> LU_ChallanTypeLst = new List<LU_ChallanType>();
				Parameters[] colparameters = new Parameters[1]{
				new Parameters("@paramId", id, DbType.Int32, ParameterDirection.Input)
				};
				LU_ChallanTypeLst = dbExecutor.FetchData<LU_ChallanType>(CommandType.StoredProcedure, "wsp_LU_ChallanType_Get", colparameters);
				return LU_ChallanTypeLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public List<LU_ChallanType> GetDynamic(string whereCondition,string orderByExpression)
		{
			try
			{
				List<LU_ChallanType> LU_ChallanTypeLst = new List<LU_ChallanType>();
				Parameters[] colparameters = new Parameters[2]{
				new Parameters("@paramWhereCondition", whereCondition, DbType.String, ParameterDirection.Input),
				new Parameters("@paramOrderByExpression", orderByExpression, DbType.String, ParameterDirection.Input),
				};
				LU_ChallanTypeLst = dbExecutor.FetchData<LU_ChallanType>(CommandType.StoredProcedure, "wsp_LU_ChallanType_GetDynamic", colparameters);
				return LU_ChallanTypeLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public List<LU_ChallanType> GetPaged(int startRecordNo, int rowPerPage, string whereClause, string sortColumn, string sortOrder, ref int rows)
		{
			try
			{
				List<LU_ChallanType> LU_ChallanTypeLst = new List<LU_ChallanType>();
				Parameters[] colparameters = new Parameters[5]{
				new Parameters("@StartRecordNo", startRecordNo, DbType.Int32, ParameterDirection.Input),
				new Parameters("@RowPerPage", rowPerPage, DbType.Int32, ParameterDirection.Input),
				new Parameters("@WhereClause", whereClause, DbType.String, ParameterDirection.Input),
				new Parameters("@SortColumn", sortColumn, DbType.String, ParameterDirection.Input),
				new Parameters("@SortOrder", sortOrder, DbType.String, ParameterDirection.Input),
				};
				LU_ChallanTypeLst = dbExecutor.FetchDataRef<LU_ChallanType>(CommandType.StoredProcedure, "LU_ChallanType_GetPaged", colparameters, ref rows);
				return LU_ChallanTypeLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public string Post(LU_ChallanType _LU_ChallanType, string transactionType)
		{
			string ret = string.Empty;
			try
			{
				Parameters[] colparameters = new Parameters[8]{
				new Parameters("@paramId", _LU_ChallanType.Id, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramName", _LU_ChallanType.Name, DbType.String, ParameterDirection.Input),
				new Parameters("@paramCreatorId", _LU_ChallanType.CreatorId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramCreationDate", _LU_ChallanType.CreationDate, DbType.DateTime, ParameterDirection.Input),
				new Parameters("@paramModifierId", _LU_ChallanType.ModifierId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramModificationDate", _LU_ChallanType.ModificationDate, DbType.DateTime, ParameterDirection.Input),
				new Parameters("@paramIsActive", _LU_ChallanType.IsActive, DbType.Boolean, ParameterDirection.Input),
				new Parameters("@paramTransactionType", transactionType, DbType.String, ParameterDirection.Input)
				};
				dbExecutor.ManageTransaction(TransactionType.Open);
				ret = dbExecutor.ExecuteScalarString(true, CommandType.StoredProcedure, "wsp_LU_ChallanType_Post", colparameters, true);
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
