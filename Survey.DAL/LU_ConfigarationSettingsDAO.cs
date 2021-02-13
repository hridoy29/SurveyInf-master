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
	public class LU_ConfigarationSettingsDAO : IDisposable
	{
		private static volatile LU_ConfigarationSettingsDAO instance;
		private static readonly object lockObj = new object();
		public static LU_ConfigarationSettingsDAO GetInstance()
		{
			if (instance == null)
			{
				instance = new LU_ConfigarationSettingsDAO();
			}
			return instance;
		}
		public static LU_ConfigarationSettingsDAO GetInstanceThreadSafe
		{
			get
			{
				if (instance == null)
				{
					lock (lockObj)
					{
						if (instance == null)
						{
							instance = new LU_ConfigarationSettingsDAO();
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

		public LU_ConfigarationSettingsDAO()
		{
			//dbExecutor = DBExecutor.GetInstanceThreadSafe;
			dbExecutor = new DBExecutor();
		}

		public List<LU_ConfigarationSettings> Get(Int32? id = null)
		{
			try
			{
				List<LU_ConfigarationSettings> LU_ConfigarationSettingsLst = new List<LU_ConfigarationSettings>();
				Parameters[] colparameters = new Parameters[1]{
				new Parameters("@paramId", id, DbType.Int32, ParameterDirection.Input)
				};
				LU_ConfigarationSettingsLst = dbExecutor.FetchData<LU_ConfigarationSettings>(CommandType.StoredProcedure, "wsp_LU_ConfigarationSettings_Get", colparameters);
				return LU_ConfigarationSettingsLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public List<LU_ConfigarationSettings> GetDynamic(string whereCondition,string orderByExpression)
		{
			try
			{
				List<LU_ConfigarationSettings> LU_ConfigarationSettingsLst = new List<LU_ConfigarationSettings>();
				Parameters[] colparameters = new Parameters[2]{
				new Parameters("@paramWhereCondition", whereCondition, DbType.String, ParameterDirection.Input),
				new Parameters("@paramOrderByExpression", orderByExpression, DbType.String, ParameterDirection.Input),
				};
				LU_ConfigarationSettingsLst = dbExecutor.FetchData<LU_ConfigarationSettings>(CommandType.StoredProcedure, "wsp_LU_ConfigarationSettings_GetDynamic", colparameters);
				return LU_ConfigarationSettingsLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public List<LU_ConfigarationSettings> GetPaged(int startRecordNo, int rowPerPage, string whereClause, string sortColumn, string sortOrder, ref int rows)
		{
			try
			{
				List<LU_ConfigarationSettings> LU_ConfigarationSettingsLst = new List<LU_ConfigarationSettings>();
				Parameters[] colparameters = new Parameters[5]{
				new Parameters("@StartRecordNo", startRecordNo, DbType.Int32, ParameterDirection.Input),
				new Parameters("@RowPerPage", rowPerPage, DbType.Int32, ParameterDirection.Input),
				new Parameters("@WhereClause", whereClause, DbType.String, ParameterDirection.Input),
				new Parameters("@SortColumn", sortColumn, DbType.String, ParameterDirection.Input),
				new Parameters("@SortOrder", sortOrder, DbType.String, ParameterDirection.Input),
				};
				LU_ConfigarationSettingsLst = dbExecutor.FetchDataRef<LU_ConfigarationSettings>(CommandType.StoredProcedure, "LU_ConfigarationSettings_GetPaged", colparameters, ref rows);
				return LU_ConfigarationSettingsLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public string Post(LU_ConfigarationSettings _LU_ConfigarationSettings, string transactionType)
		{
			string ret = string.Empty;
			try
			{
				Parameters[] colparameters = new Parameters[9]{
				new Parameters("@paramId", _LU_ConfigarationSettings.Id, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramConfig1", _LU_ConfigarationSettings.Config1, DbType.String, ParameterDirection.Input),
				new Parameters("@paramCreatorId", _LU_ConfigarationSettings.CreatorId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramCreationDate", _LU_ConfigarationSettings.CreationDate, DbType.DateTime, ParameterDirection.Input),
				new Parameters("@paramModifierId", _LU_ConfigarationSettings.ModifierId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramEmployeeTypeId", _LU_ConfigarationSettings.EmployeeTypeId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramModificationDate", _LU_ConfigarationSettings.ModificationDate, DbType.DateTime, ParameterDirection.Input),
				new Parameters("@paramIsActive", _LU_ConfigarationSettings.IsActive, DbType.Boolean, ParameterDirection.Input),
				new Parameters("@paramTransactionType", transactionType, DbType.String, ParameterDirection.Input)
				};
				dbExecutor.ManageTransaction(TransactionType.Open);
				ret = dbExecutor.ExecuteScalarString(true, CommandType.StoredProcedure, "wsp_LU_ConfigarationSettings_Post", colparameters, true);
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
