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
	public class LU_SchemeMediaTypeDAO : IDisposable
	{
		private static volatile LU_SchemeMediaTypeDAO instance;
		private static readonly object lockObj = new object();
		public static LU_SchemeMediaTypeDAO GetInstance()
		{
			if (instance == null)
			{
				instance = new LU_SchemeMediaTypeDAO();
			}
			return instance;
		}
		public static LU_SchemeMediaTypeDAO GetInstanceThreadSafe
		{
			get
			{
				if (instance == null)
				{
					lock (lockObj)
					{
						if (instance == null)
						{
							instance = new LU_SchemeMediaTypeDAO();
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

		public LU_SchemeMediaTypeDAO()
		{
			//dbExecutor = DBExecutor.GetInstanceThreadSafe;
			dbExecutor = new DBExecutor();
		}

		public List<LU_SchemeMediaType> Get(Int32? id = null)
		{
			try
			{
				List<LU_SchemeMediaType> LU_SchemeMediaTypeLst = new List<LU_SchemeMediaType>();
				Parameters[] colparameters = new Parameters[1]{
				new Parameters("@paramId", id, DbType.Int32, ParameterDirection.Input)
				};
				LU_SchemeMediaTypeLst = dbExecutor.FetchData<LU_SchemeMediaType>(CommandType.StoredProcedure, "wsp_LU_SchemeMediaType_Get", colparameters);
				return LU_SchemeMediaTypeLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public List<LU_SchemeMediaType> GetDynamic(string whereCondition,string orderByExpression)
		{
			try
			{
				List<LU_SchemeMediaType> LU_SchemeMediaTypeLst = new List<LU_SchemeMediaType>();
				Parameters[] colparameters = new Parameters[2]{
				new Parameters("@paramWhereCondition", whereCondition, DbType.String, ParameterDirection.Input),
				new Parameters("@paramOrderByExpression", orderByExpression, DbType.String, ParameterDirection.Input),
				};
				LU_SchemeMediaTypeLst = dbExecutor.FetchData<LU_SchemeMediaType>(CommandType.StoredProcedure, "wsp_LU_SchemeMediaType_GetDynamic", colparameters);
				return LU_SchemeMediaTypeLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public List<LU_SchemeMediaType> GetPaged(int startRecordNo, int rowPerPage, string whereClause, string sortColumn, string sortOrder, ref int rows)
		{
			try
			{
				List<LU_SchemeMediaType> LU_SchemeMediaTypeLst = new List<LU_SchemeMediaType>();
				Parameters[] colparameters = new Parameters[5]{
				new Parameters("@StartRecordNo", startRecordNo, DbType.Int32, ParameterDirection.Input),
				new Parameters("@RowPerPage", rowPerPage, DbType.Int32, ParameterDirection.Input),
				new Parameters("@WhereClause", whereClause, DbType.String, ParameterDirection.Input),
				new Parameters("@SortColumn", sortColumn, DbType.String, ParameterDirection.Input),
				new Parameters("@SortOrder", sortOrder, DbType.String, ParameterDirection.Input),
				};
				LU_SchemeMediaTypeLst = dbExecutor.FetchDataRef<LU_SchemeMediaType>(CommandType.StoredProcedure, "LU_SchemeMediaType_GetPaged", colparameters, ref rows);
				return LU_SchemeMediaTypeLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public string Post(LU_SchemeMediaType _LU_SchemeMediaType, string transactionType)
		{
			string ret = string.Empty;
			try
			{
				Parameters[] colparameters = new Parameters[8]{
				new Parameters("@paramId", _LU_SchemeMediaType.Id, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramName", _LU_SchemeMediaType.Name, DbType.String, ParameterDirection.Input),
				new Parameters("@paramCreatorId", _LU_SchemeMediaType.CreatorId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramCreationDate", _LU_SchemeMediaType.CreationDate, DbType.DateTime, ParameterDirection.Input),
				new Parameters("@paramModifierId", _LU_SchemeMediaType.ModifierId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramModificationDate", _LU_SchemeMediaType.ModificationDate, DbType.DateTime, ParameterDirection.Input),
				new Parameters("@paramIsActive", _LU_SchemeMediaType.IsActive, DbType.Boolean, ParameterDirection.Input),
				new Parameters("@paramTransactionType", transactionType, DbType.String, ParameterDirection.Input)
				};
				dbExecutor.ManageTransaction(TransactionType.Open);
				ret = dbExecutor.ExecuteScalarString(true, CommandType.StoredProcedure, "wsp_LU_SchemeMediaType_Post", colparameters, true);
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
