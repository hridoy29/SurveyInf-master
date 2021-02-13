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
	public class LU_EmployeeDAO : IDisposable
	{
		private static volatile LU_EmployeeDAO instance;
		private static readonly object lockObj = new object();
		public static LU_EmployeeDAO GetInstance()
		{
			if (instance == null)
			{
				instance = new LU_EmployeeDAO();
			}
			return instance;
		}
		public static LU_EmployeeDAO GetInstanceThreadSafe
		{
			get
			{
				if (instance == null)
				{
					lock (lockObj)
					{
						if (instance == null)
						{
							instance = new LU_EmployeeDAO();
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

		public LU_EmployeeDAO()
		{
			//dbExecutor = DBExecutor.GetInstanceThreadSafe;
			dbExecutor = new DBExecutor();
		}

		public List<LU_Employee> Get(Int32? id = null)
		{
			try
			{
				List<LU_Employee> LU_EmployeeLst = new List<LU_Employee>();
				Parameters[] colparameters = new Parameters[1]{
				new Parameters("@paramId", id, DbType.Int32, ParameterDirection.Input)
				};
				LU_EmployeeLst = dbExecutor.FetchData<LU_Employee>(CommandType.StoredProcedure, "wsp_LU_Employee_Get", colparameters);
				return LU_EmployeeLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public List<LU_Employee> GetDynamic(string whereCondition,string orderByExpression)
		{
			try
			{
				List<LU_Employee> LU_EmployeeLst = new List<LU_Employee>();
				Parameters[] colparameters = new Parameters[2]{
				new Parameters("@paramWhereCondition", whereCondition, DbType.String, ParameterDirection.Input),
				new Parameters("@paramOrderByExpression", orderByExpression, DbType.String, ParameterDirection.Input),
				};
				LU_EmployeeLst = dbExecutor.FetchData<LU_Employee>(CommandType.StoredProcedure, "wsp_LU_Employee_GetDynamic", colparameters);
				return LU_EmployeeLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public List<LU_Employee> GetPaged(int startRecordNo, int rowPerPage, string whereClause, string sortColumn, string sortOrder, ref int rows)
		{
			try
			{
				List<LU_Employee> LU_EmployeeLst = new List<LU_Employee>();
				Parameters[] colparameters = new Parameters[5]{
				new Parameters("@StartRecordNo", startRecordNo, DbType.Int32, ParameterDirection.Input),
				new Parameters("@RowPerPage", rowPerPage, DbType.Int32, ParameterDirection.Input),
				new Parameters("@WhereClause", whereClause, DbType.String, ParameterDirection.Input),
				new Parameters("@SortColumn", sortColumn, DbType.String, ParameterDirection.Input),
				new Parameters("@SortOrder", sortOrder, DbType.String, ParameterDirection.Input),
				};
				LU_EmployeeLst = dbExecutor.FetchDataRef<LU_Employee>(CommandType.StoredProcedure, "LU_Employee_GetPaged", colparameters, ref rows);
				return LU_EmployeeLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public string Post(LU_Employee _LU_Employee, string transactionType)
		{
			string ret = string.Empty;
			try
			{
				Parameters[] colparameters = new Parameters[9]{
				new Parameters("@paramId", _LU_Employee.Id, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramName", _LU_Employee.Name, DbType.String, ParameterDirection.Input),
				new Parameters("@paramEmployeeTypeId", _LU_Employee.EmployeeTypeId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramCreatorId", _LU_Employee.CreatorId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramCreationDate", _LU_Employee.CreationDate, DbType.DateTime, ParameterDirection.Input),
				new Parameters("@paramModifierId", _LU_Employee.ModifierId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramModificationDate", _LU_Employee.ModificationDate, DbType.DateTime, ParameterDirection.Input),
				new Parameters("@paramIsActive", _LU_Employee.IsActive, DbType.Boolean, ParameterDirection.Input),
				new Parameters("@paramTransactionType", transactionType, DbType.String, ParameterDirection.Input)
				};
				dbExecutor.ManageTransaction(TransactionType.Open);
				ret = dbExecutor.ExecuteScalarString(true, CommandType.StoredProcedure, "wsp_LU_Employee_Post", colparameters, true);
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
