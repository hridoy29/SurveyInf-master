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
	public class LU_UserGroupDAO : IDisposable
	{
		private static volatile LU_UserGroupDAO instance;
		private static readonly object lockObj = new object();
		public static LU_UserGroupDAO GetInstance()
		{
			if (instance == null)
			{
				instance = new LU_UserGroupDAO();
			}
			return instance;
		}
		public static LU_UserGroupDAO GetInstanceThreadSafe
		{
			get
			{
				if (instance == null)
				{
					lock (lockObj)
					{
						if (instance == null)
						{
							instance = new LU_UserGroupDAO();
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

		public LU_UserGroupDAO()
		{
			//dbExecutor = DBExecutor.GetInstanceThreadSafe;
			dbExecutor = new DBExecutor();
		}

		public List<LU_UserGroup> Get(Int32? id = null)
		{
			try
			{
				List<LU_UserGroup> LU_UserGroupLst = new List<LU_UserGroup>();
				Parameters[] colparameters = new Parameters[1]{
				new Parameters("@paramId", id, DbType.Int32, ParameterDirection.Input)
				};
				LU_UserGroupLst = dbExecutor.FetchData<LU_UserGroup>(CommandType.StoredProcedure, "wsp_LU_UserGroup_Get", colparameters);
				return LU_UserGroupLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public List<LU_UserGroup> GetDynamic(string whereCondition,string orderByExpression)
		{
			try
			{
				List<LU_UserGroup> LU_UserGroupLst = new List<LU_UserGroup>();
				Parameters[] colparameters = new Parameters[2]{
				new Parameters("@paramWhereCondition", whereCondition, DbType.String, ParameterDirection.Input),
				new Parameters("@paramOrderByExpression", orderByExpression, DbType.String, ParameterDirection.Input),
				};
				LU_UserGroupLst = dbExecutor.FetchData<LU_UserGroup>(CommandType.StoredProcedure, "wsp_LU_UserGroup_GetDynamic", colparameters);
				return LU_UserGroupLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public List<LU_UserGroup> GetPaged(int startRecordNo, int rowPerPage, string whereClause, string sortColumn, string sortOrder, ref int rows)
		{
			try
			{
				List<LU_UserGroup> LU_UserGroupLst = new List<LU_UserGroup>();
				Parameters[] colparameters = new Parameters[5]{
				new Parameters("@StartRecordNo", startRecordNo, DbType.Int32, ParameterDirection.Input),
				new Parameters("@RowPerPage", rowPerPage, DbType.Int32, ParameterDirection.Input),
				new Parameters("@WhereClause", whereClause, DbType.String, ParameterDirection.Input),
				new Parameters("@SortColumn", sortColumn, DbType.String, ParameterDirection.Input),
				new Parameters("@SortOrder", sortOrder, DbType.String, ParameterDirection.Input),
				};
				LU_UserGroupLst = dbExecutor.FetchDataRef<LU_UserGroup>(CommandType.StoredProcedure, "LU_UserGroup_GetPaged", colparameters, ref rows);
				return LU_UserGroupLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public string Post(LU_UserGroup _LU_UserGroup, string transactionType)
		{
			string ret = string.Empty;
			try
			{
				Parameters[] colparameters = new Parameters[8]{
				new Parameters("@paramId", _LU_UserGroup.Id, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramName", _LU_UserGroup.Name, DbType.String, ParameterDirection.Input),
				new Parameters("@paramCreatorId", _LU_UserGroup.CreatorId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramCreationDate", _LU_UserGroup.CreationDate, DbType.DateTime, ParameterDirection.Input),
				new Parameters("@paramModifierId", _LU_UserGroup.ModifierId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramModificationDate", _LU_UserGroup.ModificationDate, DbType.DateTime, ParameterDirection.Input),
				new Parameters("@paramIsActive", _LU_UserGroup.IsActive, DbType.Boolean, ParameterDirection.Input),
				new Parameters("@paramTransactionType", transactionType, DbType.String, ParameterDirection.Input)
				};
				dbExecutor.ManageTransaction(TransactionType.Open);
				ret = dbExecutor.ExecuteScalarString(true, CommandType.StoredProcedure, "wsp_LU_UserGroup_Post", colparameters, true);
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
