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
	public class LU_CommentsTypeDAO : IDisposable
	{
		private static volatile LU_CommentsTypeDAO instance;
		private static readonly object lockObj = new object();
		public static LU_CommentsTypeDAO GetInstance()
		{
			if (instance == null)
			{
				instance = new LU_CommentsTypeDAO();
			}
			return instance;
		}
		public static LU_CommentsTypeDAO GetInstanceThreadSafe
		{
			get
			{
				if (instance == null)
				{
					lock (lockObj)
					{
						if (instance == null)
						{
							instance = new LU_CommentsTypeDAO();
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

		public LU_CommentsTypeDAO()
		{
			//dbExecutor = DBExecutor.GetInstanceThreadSafe;
			dbExecutor = new DBExecutor();
		}

		public List<LU_CommentsType> Get(Int32? id = null)
		{
			try
			{
				List<LU_CommentsType> LU_CommentsTypeLst = new List<LU_CommentsType>();
				Parameters[] colparameters = new Parameters[1]{
				new Parameters("@paramId", id, DbType.Int32, ParameterDirection.Input)
				};
				LU_CommentsTypeLst = dbExecutor.FetchData<LU_CommentsType>(CommandType.StoredProcedure, "wsp_LU_CommentsType_Get", colparameters);
				return LU_CommentsTypeLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public List<LU_CommentsType> GetDynamic(string whereCondition,string orderByExpression)
		{
			try
			{
				List<LU_CommentsType> LU_CommentsTypeLst = new List<LU_CommentsType>();
				Parameters[] colparameters = new Parameters[2]{
				new Parameters("@paramWhereCondition", whereCondition, DbType.String, ParameterDirection.Input),
				new Parameters("@paramOrderByExpression", orderByExpression, DbType.String, ParameterDirection.Input),
				};
				LU_CommentsTypeLst = dbExecutor.FetchData<LU_CommentsType>(CommandType.StoredProcedure, "wsp_LU_CommentsType_GetDynamic", colparameters);
				return LU_CommentsTypeLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public List<LU_CommentsType> GetPaged(int startRecordNo, int rowPerPage, string whereClause, string sortColumn, string sortOrder, ref int rows)
		{
			try
			{
				List<LU_CommentsType> LU_CommentsTypeLst = new List<LU_CommentsType>();
				Parameters[] colparameters = new Parameters[5]{
				new Parameters("@StartRecordNo", startRecordNo, DbType.Int32, ParameterDirection.Input),
				new Parameters("@RowPerPage", rowPerPage, DbType.Int32, ParameterDirection.Input),
				new Parameters("@WhereClause", whereClause, DbType.String, ParameterDirection.Input),
				new Parameters("@SortColumn", sortColumn, DbType.String, ParameterDirection.Input),
				new Parameters("@SortOrder", sortOrder, DbType.String, ParameterDirection.Input),
				};
				LU_CommentsTypeLst = dbExecutor.FetchDataRef<LU_CommentsType>(CommandType.StoredProcedure, "LU_CommentsType_GetPaged", colparameters, ref rows);
				return LU_CommentsTypeLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public string Post(LU_CommentsType _LU_CommentsType, string transactionType)
		{
			string ret = string.Empty;
			try
			{
				Parameters[] colparameters = new Parameters[8]{
				new Parameters("@paramId", _LU_CommentsType.Id, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramCommentsType", _LU_CommentsType.CommentsType, DbType.String, ParameterDirection.Input),
				new Parameters("@paramCreatorId", _LU_CommentsType.CreatorId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramCreationDate", _LU_CommentsType.CreationDate, DbType.DateTime, ParameterDirection.Input),
				new Parameters("@paramModifierId", _LU_CommentsType.ModifierId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramModificationDate", _LU_CommentsType.ModificationDate, DbType.DateTime, ParameterDirection.Input),
				new Parameters("@paramIsActive", _LU_CommentsType.IsActive, DbType.Boolean, ParameterDirection.Input),
				new Parameters("@paramTransactionType", transactionType, DbType.String, ParameterDirection.Input)
				};
				dbExecutor.ManageTransaction(TransactionType.Open);
				ret = dbExecutor.ExecuteScalarString(true, CommandType.StoredProcedure, "wsp_LU_CommentsType_Post", colparameters, true);
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
