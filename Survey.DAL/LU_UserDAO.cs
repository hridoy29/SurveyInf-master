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
	public class LU_UserDAO : IDisposable
	{
		private static volatile LU_UserDAO instance;
		private static readonly object lockObj = new object();
		public static LU_UserDAO GetInstance()
		{
			if (instance == null)
			{
				instance = new LU_UserDAO();
			}
			return instance;
		}
		public static LU_UserDAO GetInstanceThreadSafe
		{
			get
			{
				if (instance == null)
				{
					lock (lockObj)
					{
						if (instance == null)
						{
							instance = new LU_UserDAO();
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

		public LU_UserDAO()
		{
			//dbExecutor = DBExecutor.GetInstanceThreadSafe;
			dbExecutor = new DBExecutor();
		}

		public List<LU_User> Get(Int32? id = null)
		{
			try
			{
				List<LU_User> LU_UserLst = new List<LU_User>();
				Parameters[] colparameters = new Parameters[1]{
				new Parameters("@paramId", id, DbType.Int32, ParameterDirection.Input)
				};
				LU_UserLst = dbExecutor.FetchData<LU_User>(CommandType.StoredProcedure, "wsp_LU_User_Get", colparameters);
				return LU_UserLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public List<LU_User> GetDynamic(string whereCondition,string orderByExpression)
		{
			try
			{
				List<LU_User> LU_UserLst = new List<LU_User>();
				Parameters[] colparameters = new Parameters[2]{
				new Parameters("@paramWhereCondition", whereCondition, DbType.String, ParameterDirection.Input),
				new Parameters("@paramOrderByExpression", orderByExpression, DbType.String, ParameterDirection.Input),
				};
				LU_UserLst = dbExecutor.FetchData<LU_User>(CommandType.StoredProcedure, "wsp_LU_User_GetDynamic", colparameters);
				return LU_UserLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public List<LU_User> GetPaged(int startRecordNo, int rowPerPage, string whereClause, string sortColumn, string sortOrder, ref int rows)
		{
			try
			{
				List<LU_User> LU_UserLst = new List<LU_User>();
				Parameters[] colparameters = new Parameters[5]{
				new Parameters("@StartRecordNo", startRecordNo, DbType.Int32, ParameterDirection.Input),
				new Parameters("@RowPerPage", rowPerPage, DbType.Int32, ParameterDirection.Input),
				new Parameters("@WhereClause", whereClause, DbType.String, ParameterDirection.Input),
				new Parameters("@SortColumn", sortColumn, DbType.String, ParameterDirection.Input),
				new Parameters("@SortOrder", sortOrder, DbType.String, ParameterDirection.Input),
				};
				LU_UserLst = dbExecutor.FetchDataRef<LU_User>(CommandType.StoredProcedure, "LU_User_GetPaged", colparameters, ref rows);
				return LU_UserLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public string Post(LU_User _LU_User, string transactionType)
		{
			string ret = string.Empty;
			try
			{
				Parameters[] colparameters = new Parameters[12]{
				new Parameters("@paramId", _LU_User.Id, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramUserGroupId", _LU_User.UserGroupId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramName", _LU_User.Name, DbType.String, ParameterDirection.Input),
				new Parameters("@paramEmail", _LU_User.Email, DbType.String, ParameterDirection.Input),
				new Parameters("@paramMobileNo", _LU_User.MobileNo, DbType.String, ParameterDirection.Input),
				new Parameters("@paramPassword", _LU_User.Password, DbType.String, ParameterDirection.Input),
				new Parameters("@paramCreatorId", _LU_User.CreatorId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramCreationDate", _LU_User.CreationDate, DbType.DateTime, ParameterDirection.Input),
				new Parameters("@paramModifierId", _LU_User.ModifierId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramModificationDate", _LU_User.ModificationDate, DbType.DateTime, ParameterDirection.Input),
				new Parameters("@paramIsActive", _LU_User.IsActive, DbType.Boolean, ParameterDirection.Input),
				new Parameters("@paramTransactionType", transactionType, DbType.String, ParameterDirection.Input)
				};
				dbExecutor.ManageTransaction(TransactionType.Open);
				ret = dbExecutor.ExecuteScalarString(true, CommandType.StoredProcedure, "wsp_LU_User_Post", colparameters, true);
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
