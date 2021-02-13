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
	public class LU_OutletTypeDAO : IDisposable
	{
		private static volatile LU_OutletTypeDAO instance;
		private static readonly object lockObj = new object();
		public static LU_OutletTypeDAO GetInstance()
		{
			if (instance == null)
			{
				instance = new LU_OutletTypeDAO();
			}
			return instance;
		}
		public static LU_OutletTypeDAO GetInstanceThreadSafe
		{
			get
			{
				if (instance == null)
				{
					lock (lockObj)
					{
						if (instance == null)
						{
							instance = new LU_OutletTypeDAO();
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

		public LU_OutletTypeDAO()
		{
			//dbExecutor = DBExecutor.GetInstanceThreadSafe;
			dbExecutor = new DBExecutor();
		}

		public List<LU_OutletType> Get(Int32? id = null)
		{
			try
			{
				List<LU_OutletType> LU_OutletTypeLst = new List<LU_OutletType>();
				Parameters[] colparameters = new Parameters[1]{
				new Parameters("@paramId", id, DbType.Int32, ParameterDirection.Input)
				};
				LU_OutletTypeLst = dbExecutor.FetchData<LU_OutletType>(CommandType.StoredProcedure, "wsp_LU_OutletType_Get", colparameters);
				return LU_OutletTypeLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public List<LU_OutletType> GetDynamic(string whereCondition,string orderByExpression)
		{
			try
			{
				List<LU_OutletType> LU_OutletTypeLst = new List<LU_OutletType>();
				Parameters[] colparameters = new Parameters[2]{
				new Parameters("@paramWhereCondition", whereCondition, DbType.String, ParameterDirection.Input),
				new Parameters("@paramOrderByExpression", orderByExpression, DbType.String, ParameterDirection.Input),
				};
				LU_OutletTypeLst = dbExecutor.FetchData<LU_OutletType>(CommandType.StoredProcedure, "wsp_LU_OutletType_GetDynamic", colparameters);
				return LU_OutletTypeLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public List<LU_OutletType> GetPaged(int startRecordNo, int rowPerPage, string whereClause, string sortColumn, string sortOrder, ref int rows)
		{
			try
			{
				List<LU_OutletType> LU_OutletTypeLst = new List<LU_OutletType>();
				Parameters[] colparameters = new Parameters[5]{
				new Parameters("@StartRecordNo", startRecordNo, DbType.Int32, ParameterDirection.Input),
				new Parameters("@RowPerPage", rowPerPage, DbType.Int32, ParameterDirection.Input),
				new Parameters("@WhereClause", whereClause, DbType.String, ParameterDirection.Input),
				new Parameters("@SortColumn", sortColumn, DbType.String, ParameterDirection.Input),
				new Parameters("@SortOrder", sortOrder, DbType.String, ParameterDirection.Input),
				};
				LU_OutletTypeLst = dbExecutor.FetchDataRef<LU_OutletType>(CommandType.StoredProcedure, "LU_OutletType_GetPaged", colparameters, ref rows);
				return LU_OutletTypeLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public string Post(LU_OutletType _LU_OutletType, string transactionType)
		{
			string ret = string.Empty;
			try
			{
				Parameters[] colparameters = new Parameters[8]{
				new Parameters("@paramId", _LU_OutletType.Id, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramName", _LU_OutletType.Name, DbType.String, ParameterDirection.Input),
				new Parameters("@paramCreatorId", _LU_OutletType.CreatorId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramCreationDate", _LU_OutletType.CreationDate, DbType.DateTime, ParameterDirection.Input),
				new Parameters("@paramModifierId", _LU_OutletType.ModifierId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramModificationDate", _LU_OutletType.ModificationDate, DbType.DateTime, ParameterDirection.Input),
				new Parameters("@paramIsActive", _LU_OutletType.IsActive, DbType.Boolean, ParameterDirection.Input),
				new Parameters("@paramTransactionType", transactionType, DbType.String, ParameterDirection.Input)
				};
				dbExecutor.ManageTransaction(TransactionType.Open);
				ret = dbExecutor.ExecuteScalarString(true, CommandType.StoredProcedure, "wsp_LU_OutletType_Post", colparameters, true);
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
