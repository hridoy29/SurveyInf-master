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
	public class LU_DistributorlistDAO : IDisposable
	{
		private static volatile LU_DistributorlistDAO instance;
		private static readonly object lockObj = new object();
		public static LU_DistributorlistDAO GetInstance()
		{
			if (instance == null)
			{
				instance = new LU_DistributorlistDAO();
			}
			return instance;
		}
		public static LU_DistributorlistDAO GetInstanceThreadSafe
		{
			get
			{
				if (instance == null)
				{
					lock (lockObj)
					{
						if (instance == null)
						{
							instance = new LU_DistributorlistDAO();
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

		public LU_DistributorlistDAO()
		{
			//dbExecutor = DBExecutor.GetInstanceThreadSafe;
			dbExecutor = new DBExecutor();
		}

		public List<LU_Distributorlist> Get(Int32? id = null)
		{
			try
			{
				List<LU_Distributorlist> LU_DistributorlistLst = new List<LU_Distributorlist>();
				Parameters[] colparameters = new Parameters[1]{
				new Parameters("@paramId", id, DbType.Int32, ParameterDirection.Input)
				};
                LU_DistributorlistLst = dbExecutor.FetchData<LU_Distributorlist>(CommandType.StoredProcedure, "wsp_LU_DistributorList_Get", colparameters);
				return LU_DistributorlistLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public List<LU_Distributorlist> GetDynamic(string whereCondition,string orderByExpression)
		{
			try
			{
				List<LU_Distributorlist> LU_DistributorlistLst = new List<LU_Distributorlist>();
				Parameters[] colparameters = new Parameters[2]{
				new Parameters("@paramWhereCondition", whereCondition, DbType.String, ParameterDirection.Input),
				new Parameters("@paramOrderByExpression", orderByExpression, DbType.String, ParameterDirection.Input),
				};
                LU_DistributorlistLst = dbExecutor.FetchData<LU_Distributorlist>(CommandType.StoredProcedure, "wsp_LU_DistributorList_GetDynamic", colparameters);
				return LU_DistributorlistLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public List<LU_Distributorlist> GetPaged(int startRecordNo, int rowPerPage, string whereClause, string sortColumn, string sortOrder, ref int rows)
		{
			try
			{
				List<LU_Distributorlist> LU_DistributorlistLst = new List<LU_Distributorlist>();
				Parameters[] colparameters = new Parameters[5]{
				new Parameters("@StartRecordNo", startRecordNo, DbType.Int32, ParameterDirection.Input),
				new Parameters("@RowPerPage", rowPerPage, DbType.Int32, ParameterDirection.Input),
				new Parameters("@WhereClause", whereClause, DbType.String, ParameterDirection.Input),
				new Parameters("@SortColumn", sortColumn, DbType.String, ParameterDirection.Input),
				new Parameters("@SortOrder", sortOrder, DbType.String, ParameterDirection.Input),
				};
                LU_DistributorlistLst = dbExecutor.FetchDataRef<LU_Distributorlist>(CommandType.StoredProcedure, "wsp_LU_DistributorList_GetPaged", colparameters, ref rows);
				return LU_DistributorlistLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public string Post(LU_Distributorlist _LU_Distributorlist, string transactionType)
		{
			string ret = string.Empty;
			try
			{
				Parameters[] colparameters = new Parameters[8]{
				new Parameters("@paramId", _LU_Distributorlist.Id, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramName", _LU_Distributorlist.Name, DbType.String, ParameterDirection.Input),
				new Parameters("@paramCreatorId", _LU_Distributorlist.CreatorId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramCreationDate", _LU_Distributorlist.CreationDate, DbType.DateTime, ParameterDirection.Input),
				new Parameters("@paramModifierId", _LU_Distributorlist.ModifierId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramModificationDate", _LU_Distributorlist.ModificationDate, DbType.DateTime, ParameterDirection.Input),
				new Parameters("@paramIsActive", _LU_Distributorlist.IsActive, DbType.Boolean, ParameterDirection.Input),
				new Parameters("@paramTransactionType", transactionType, DbType.String, ParameterDirection.Input)
				};
				dbExecutor.ManageTransaction(TransactionType.Open);
				ret = dbExecutor.ExecuteScalarString(true, CommandType.StoredProcedure, "wsp_LU_DistributorList_Post", colparameters, true);
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
