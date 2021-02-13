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
	public class LU_DeviceInfoDAO : IDisposable
	{
		private static volatile LU_DeviceInfoDAO instance;
		private static readonly object lockObj = new object();
		public static LU_DeviceInfoDAO GetInstance()
		{
			if (instance == null)
			{
				instance = new LU_DeviceInfoDAO();
			}
			return instance;
		}
		public static LU_DeviceInfoDAO GetInstanceThreadSafe
		{
			get
			{
				if (instance == null)
				{
					lock (lockObj)
					{
						if (instance == null)
						{
							instance = new LU_DeviceInfoDAO();
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

		public LU_DeviceInfoDAO()
		{
			//dbExecutor = DBExecutor.GetInstanceThreadSafe;
			dbExecutor = new DBExecutor();
		}

		public List<LU_DeviceInfo> Get(Int32? id = null)
		{
			try
			{
				List<LU_DeviceInfo> LU_DeviceInfoLst = new List<LU_DeviceInfo>();
				Parameters[] colparameters = new Parameters[1]{
				new Parameters("@paramId", id, DbType.Int32, ParameterDirection.Input)
				};
				LU_DeviceInfoLst = dbExecutor.FetchData<LU_DeviceInfo>(CommandType.StoredProcedure, "wsp_LU_DeviceInfo_Get", colparameters);
				return LU_DeviceInfoLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public List<LU_DeviceInfo> GetDynamic(string whereCondition,string orderByExpression)
		{
			try
			{
				List<LU_DeviceInfo> LU_DeviceInfoLst = new List<LU_DeviceInfo>();
				Parameters[] colparameters = new Parameters[2]{
				new Parameters("@paramWhereCondition", whereCondition, DbType.String, ParameterDirection.Input),
				new Parameters("@paramOrderByExpression", orderByExpression, DbType.String, ParameterDirection.Input),
				};
				LU_DeviceInfoLst = dbExecutor.FetchData<LU_DeviceInfo>(CommandType.StoredProcedure, "wsp_LU_DeviceInfo_GetDynamic", colparameters);
				return LU_DeviceInfoLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public List<LU_DeviceInfo> GetPaged(int startRecordNo, int rowPerPage, string whereClause, string sortColumn, string sortOrder, ref int rows)
		{
			try
			{
				List<LU_DeviceInfo> LU_DeviceInfoLst = new List<LU_DeviceInfo>();
				Parameters[] colparameters = new Parameters[5]{
				new Parameters("@StartRecordNo", startRecordNo, DbType.Int32, ParameterDirection.Input),
				new Parameters("@RowPerPage", rowPerPage, DbType.Int32, ParameterDirection.Input),
				new Parameters("@WhereClause", whereClause, DbType.String, ParameterDirection.Input),
				new Parameters("@SortColumn", sortColumn, DbType.String, ParameterDirection.Input),
				new Parameters("@SortOrder", sortOrder, DbType.String, ParameterDirection.Input),
				};
				LU_DeviceInfoLst = dbExecutor.FetchDataRef<LU_DeviceInfo>(CommandType.StoredProcedure, "LU_DeviceInfo_GetPaged", colparameters, ref rows);
				return LU_DeviceInfoLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public string Post(LU_DeviceInfo _LU_DeviceInfo, string transactionType)
		{
			string ret = string.Empty;
			try
			{
				Parameters[] colparameters = new Parameters[10]{
				new Parameters("@paramId", _LU_DeviceInfo.Id, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramName", _LU_DeviceInfo.Name, DbType.String, ParameterDirection.Input),
				new Parameters("@paramUniqueNumber", _LU_DeviceInfo.UniqueNumber, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramDeviceType", _LU_DeviceInfo.DeviceType, DbType.String, ParameterDirection.Input),
				new Parameters("@paramCreatorId", _LU_DeviceInfo.CreatorId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramCreationDate", _LU_DeviceInfo.CreationDate, DbType.DateTime, ParameterDirection.Input),
				new Parameters("@paramModifierId", _LU_DeviceInfo.ModifierId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramModificationDate", _LU_DeviceInfo.ModificationDate, DbType.DateTime, ParameterDirection.Input),
				new Parameters("@paramIsActive", _LU_DeviceInfo.IsActive, DbType.Boolean, ParameterDirection.Input),
				new Parameters("@paramTransactionType", transactionType, DbType.String, ParameterDirection.Input)
				};
				dbExecutor.ManageTransaction(TransactionType.Open);
				ret = dbExecutor.ExecuteScalarString(true, CommandType.StoredProcedure, "wsp_LU_DeviceInfo_Post", colparameters, true);
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
