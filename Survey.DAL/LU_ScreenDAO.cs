using DbExecutor;
using Survey.Entity;
using SurveyEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Survey.DAL
{

	public class LU_ScreenDAO
	{
		private static volatile LU_ScreenDAO instance;
		private static readonly object lockObj = new object();
		public static LU_ScreenDAO GetInstance()
		{
			if (instance == null)
			{
				instance = new LU_ScreenDAO();
			}
			return instance;
		}
		public static LU_ScreenDAO GetInstanceThreadSafe
		{
			get
			{
				if (instance == null)
				{
					lock (lockObj)
					{
						if (instance == null)
						{
							instance = new LU_ScreenDAO();
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

		public LU_ScreenDAO()
		{
			//dbExecutor = DBExecutor.GetInstanceThreadSafe;
			dbExecutor = new DBExecutor();
		}

		public List<LU_Screen> Get(Int32? id = null)
		{
			try
			{
				List<LU_Screen> LU_ScreenLst = new List<LU_Screen>();
				Parameters[] colparameters = new Parameters[1]{
				new Parameters("@ScreenId", id, DbType.Int32, ParameterDirection.Input)
				};
				LU_ScreenLst = dbExecutor.FetchData<LU_Screen>(CommandType.StoredProcedure, "s_Screen_Get", colparameters);
				return LU_ScreenLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public List<LU_Screen> GetDynamic(string whereCondition, string orderByExpression)
		{
			try
			{
				List<LU_Screen> LU_ScreenLst = new List<LU_Screen>();
				Parameters[] colparameters = new Parameters[2]{
				new Parameters("@paramWhereCondition", whereCondition, DbType.String, ParameterDirection.Input),
				new Parameters("@paramOrderByExpression", orderByExpression, DbType.String, ParameterDirection.Input),
				};
				LU_ScreenLst = dbExecutor.FetchData<LU_Screen>(CommandType.StoredProcedure, "wsp_LU_Screen_GetDynamic", colparameters);
				return LU_ScreenLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public List<LU_Screen> GetPaged(int startRecordNo, int rowPerPage, string whereClause, string sortColumn, string sortOrder, ref int rows)
		{
			try
			{
				List<LU_Screen> LU_ScreenLst = new List<LU_Screen>();
				Parameters[] colparameters = new Parameters[5]{
				new Parameters("@StartRecordNo", startRecordNo, DbType.Int32, ParameterDirection.Input),
				new Parameters("@RowPerPage", rowPerPage, DbType.Int32, ParameterDirection.Input),
				new Parameters("@WhereClause", whereClause, DbType.String, ParameterDirection.Input),
				new Parameters("@SortColumn", sortColumn, DbType.String, ParameterDirection.Input),
				new Parameters("@SortOrder", sortOrder, DbType.String, ParameterDirection.Input),
				};
				LU_ScreenLst = dbExecutor.FetchDataRef<LU_Screen>(CommandType.StoredProcedure, "LU_Screen_GetPaged", colparameters, ref rows);
				return LU_ScreenLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public string Post(LU_Screen _LU_Screen, string transactionType)
		{
			string ret = string.Empty;
			//try
			//{
			//	Parameters[] colparameters = new Parameters[12]{
			//	new Parameters("@paramId", _LU_Screen.Id, DbType.Int32, ParameterDirection.Input),
			//	new Parameters("@paramUserGroupId", _LU_Screen.UserGroupId, DbType.Int32, ParameterDirection.Input),
			//	new Parameters("@paramName", _LU_Screen.Name, DbType.String, ParameterDirection.Input),
			//	new Parameters("@paramEmail", _LU_Screen.Email, DbType.String, ParameterDirection.Input),
			//	new Parameters("@paramMobileNo", _LU_Screen.MobileNo, DbType.String, ParameterDirection.Input),
			//	new Parameters("@paramPassword", _LU_Screen.Password, DbType.String, ParameterDirection.Input),
			//	new Parameters("@paramCreatorId", _LU_Screen.CreatorId, DbType.Int32, ParameterDirection.Input),
			//	new Parameters("@paramCreationDate", _LU_Screen.CreationDate, DbType.DateTime, ParameterDirection.Input),
			//	new Parameters("@paramModifierId", _LU_Screen.ModifierId, DbType.Int32, ParameterDirection.Input),
			//	new Parameters("@paramModificationDate", _LU_Screen.ModificationDate, DbType.DateTime, ParameterDirection.Input),
			//	new Parameters("@paramIsActive", _LU_Screen.IsActive, DbType.Boolean, ParameterDirection.Input),
			//	new Parameters("@paramTransactionType", transactionType, DbType.String, ParameterDirection.Input)
			//	};
			//	dbExecutor.ManageTransaction(TransactionType.Open);
			//	ret = dbExecutor.ExecuteScalarString(true, CommandType.StoredProcedure, "wsp_LU_Screen_Post", colparameters, true);
			//	dbExecutor.ManageTransaction(TransactionType.Commit);
			//}
			//catch (DBConcurrencyException except)
			//{
			//	dbExecutor.ManageTransaction(TransactionType.Rollback);
			//	throw except;
			//}
			//catch (Exception ex)
			//{
			//	dbExecutor.ManageTransaction(TransactionType.Rollback);
			//	throw ex;
			//}
			return ret;
		}
	}
}

