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
	public class TRN_PermissionDAO : IDisposable
	{
		private static volatile TRN_PermissionDAO instance;
		private static readonly object lockObj = new object();
		public static TRN_PermissionDAO GetInstance()
		{
			if (instance == null)
			{
				instance = new TRN_PermissionDAO();
			}
			return instance;
		}
		public static TRN_PermissionDAO GetInstanceThreadSafe
		{
			get
			{
				if (instance == null)
				{
					lock (lockObj)
					{
						if (instance == null)
						{
							instance = new TRN_PermissionDAO();
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

		public TRN_PermissionDAO()
		{
			//dbExecutor = DBExecutor.GetInstanceThreadSafe;
			dbExecutor = new DBExecutor();
		}

		public List<TRN_Permission> Get(Int64? permissionId = null)
		{
			try
			{
				List<TRN_Permission> TRN_PermissionLst = new List<TRN_Permission>();
				Parameters[] colparameters = new Parameters[1]{
				new Parameters("@paramPermissionId", permissionId, DbType.Int64, ParameterDirection.Input)
				};
				TRN_PermissionLst = dbExecutor.FetchData<TRN_Permission>(CommandType.StoredProcedure, "wsp_TRN_Permission_Get", colparameters);
				return TRN_PermissionLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public List<TRN_Permission> GetDynamic(string whereCondition,string orderByExpression)
		{
			try
			{
				List<TRN_Permission> TRN_PermissionLst = new List<TRN_Permission>();
				Parameters[] colparameters = new Parameters[2]{
				new Parameters("@paramWhereCondition", whereCondition, DbType.String, ParameterDirection.Input),
				new Parameters("@paramOrderByExpression", orderByExpression, DbType.String, ParameterDirection.Input),
				};
				TRN_PermissionLst = dbExecutor.FetchData<TRN_Permission>(CommandType.StoredProcedure, "wsp_TRN_Permission_GetDynamic", colparameters);
				return TRN_PermissionLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public List<TRN_Permission> GetPaged(int startRecordNo, int rowPerPage, string whereClause, string sortColumn, string sortOrder, ref int rows)
		{
			try
			{
				List<TRN_Permission> TRN_PermissionLst = new List<TRN_Permission>();
				Parameters[] colparameters = new Parameters[5]{
				new Parameters("@StartRecordNo", startRecordNo, DbType.Int32, ParameterDirection.Input),
				new Parameters("@RowPerPage", rowPerPage, DbType.Int32, ParameterDirection.Input),
				new Parameters("@WhereClause", whereClause, DbType.String, ParameterDirection.Input),
				new Parameters("@SortColumn", sortColumn, DbType.String, ParameterDirection.Input),
				new Parameters("@SortOrder", sortOrder, DbType.String, ParameterDirection.Input),
				};
				TRN_PermissionLst = dbExecutor.FetchDataRef<TRN_Permission>(CommandType.StoredProcedure, "TRN_Permission_GetPaged", colparameters, ref rows);
				return TRN_PermissionLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public Int64 Post(TRN_Permission _TRN_Permission, string transactionType)
		{
			
			Int64 ret = 0;
			try
			{
				//@PermissionId
				Parameters[] colparameters = new Parameters[8]{
				new Parameters("@PermissionId", _TRN_Permission.PermissionId, DbType.Int64, ParameterDirection.Input),
				new Parameters("@UserGroupId", _TRN_Permission.UserGroupId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@ScreenId", _TRN_Permission.ScreenId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@CanView", _TRN_Permission.CanView, DbType.Boolean, ParameterDirection.Input),
				new Parameters("@CreatorId", _TRN_Permission.CreatorId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@CreateDate", _TRN_Permission.CreateDate, DbType.DateTime, ParameterDirection.Input),
				new Parameters("@UpdatorId", _TRN_Permission.UpdatorId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@UpdateDate", _TRN_Permission.UpdateDate, DbType.DateTime, ParameterDirection.Input)
				};
				dbExecutor.ManageTransaction(TransactionType.Open);
				ret = dbExecutor.ExecuteScalar64(true, CommandType.StoredProcedure, "s_Permission_Create", colparameters, true);
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
		public int DeleteByRoleId(Int64 RoleId)
		{
			try
			{
				
				int ret = 0;
				Parameters[] colparameters = new Parameters[1]{
				new Parameters("@RoleId", RoleId, DbType.Int32, ParameterDirection.Input)
				};
				ret = dbExecutor.ExecuteNonQuery(CommandType.StoredProcedure, "s_Permission_DeleteByRoleId", colparameters, true);
				return ret;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
