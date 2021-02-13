using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using System.Data.Common;
using DbExecutor;
using SurveyEntity;
using Survey.Entity;

namespace SurveyDAL
{
	public class LU_ASMDAO : IDisposable
	{
		private static volatile LU_ASMDAO instance;
		private static readonly object lockObj = new object();
		public static LU_ASMDAO GetInstance()
		{
			if (instance == null)
			{
				instance = new LU_ASMDAO();
			}
			return instance;
		}
		public static LU_ASMDAO GetInstanceThreadSafe
		{
			get
			{
				if (instance == null)
				{
					lock (lockObj)
					{
						if (instance == null)
						{
							instance = new LU_ASMDAO();
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

		public LU_ASMDAO()
		{
			//dbExecutor = DBExecutor.GetInstanceThreadSafe;
			dbExecutor = new DBExecutor();
		}

		public List<LU_ASM> Get(Int32? id = null)
		{
			try
			{
				List<LU_ASM> LU_ASMLst = new List<LU_ASM>();
				Parameters[] colparameters = new Parameters[1]{
				new Parameters("@paramId", id, DbType.Int32, ParameterDirection.Input)
				};
                LU_ASMLst = dbExecutor.FetchData<LU_ASM>(CommandType.StoredProcedure, "wsp_LU_ASM_Get", colparameters);
				return LU_ASMLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public List<LU_ASM> GetDynamic(string whereCondition,string orderByExpression)
		{
			try
			{
				List<LU_ASM> LU_ASMLst = new List<LU_ASM>();
				Parameters[] colparameters = new Parameters[2]{
				new Parameters("@paramWhereCondition", whereCondition, DbType.String, ParameterDirection.Input),
				new Parameters("@paramOrderByExpression", orderByExpression, DbType.String, ParameterDirection.Input),
				};
                LU_ASMLst = dbExecutor.FetchData<LU_ASM>(CommandType.StoredProcedure, "wsp_LU_ASM_GetDynamic", colparameters);
				return LU_ASMLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public List<LU_ASM> GetPaged(int startRecordNo, int rowPerPage, string whereClause, string sortColumn, string sortOrder, ref int rows)
		{
			try
			{
				List<LU_ASM> LU_ASMLst = new List<LU_ASM>();
				Parameters[] colparameters = new Parameters[5]{
				new Parameters("@StartRecordNo", startRecordNo, DbType.Int32, ParameterDirection.Input),
				new Parameters("@RowPerPage", rowPerPage, DbType.Int32, ParameterDirection.Input),
				new Parameters("@WhereClause", whereClause, DbType.String, ParameterDirection.Input),
				new Parameters("@SortColumn", sortColumn, DbType.String, ParameterDirection.Input),
				new Parameters("@SortOrder", sortOrder, DbType.String, ParameterDirection.Input),
				};
                LU_ASMLst = dbExecutor.FetchDataRef<LU_ASM>(CommandType.StoredProcedure, "wsp_LU_ASM_GetPaged", colparameters, ref rows);
				return LU_ASMLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public string Post(LU_ASM _LU_ASM, string transactionType)
		{
			string ret = string.Empty;
			try
			{
				Parameters[] colparameters = new Parameters[8]{
				new Parameters("@paramId", _LU_ASM.Id, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramName", _LU_ASM.Name, DbType.String, ParameterDirection.Input),
				new Parameters("@paramCreatorId", _LU_ASM.CreatorId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramCreationDate", _LU_ASM.CreationDate, DbType.DateTime, ParameterDirection.Input),
				new Parameters("@paramModifierId", _LU_ASM.ModifierId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramModificationDate", _LU_ASM.ModificationDate, DbType.DateTime, ParameterDirection.Input),
				new Parameters("@paramIsActive", _LU_ASM.IsActive, DbType.Boolean, ParameterDirection.Input),
				new Parameters("@paramTransactionType", transactionType, DbType.String, ParameterDirection.Input)
				};
				dbExecutor.ManageTransaction(TransactionType.Open);
				ret = dbExecutor.ExecuteScalarString(true, CommandType.StoredProcedure, "wsp_LU_ASM_Post", colparameters, true);
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
