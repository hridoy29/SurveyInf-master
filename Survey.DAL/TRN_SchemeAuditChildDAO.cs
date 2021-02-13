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
	public class TRN_SchemeAuditChildDAO : IDisposable
	{
		private static volatile TRN_SchemeAuditChildDAO instance;
		private static readonly object lockObj = new object();
		public static TRN_SchemeAuditChildDAO GetInstance()
		{
			if (instance == null)
			{
				instance = new TRN_SchemeAuditChildDAO();
			}
			return instance;
		}
		public static TRN_SchemeAuditChildDAO GetInstanceThreadSafe
		{
			get
			{
				if (instance == null)
				{
					lock (lockObj)
					{
						if (instance == null)
						{
							instance = new TRN_SchemeAuditChildDAO();
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

		public TRN_SchemeAuditChildDAO()
		{
			//dbExecutor = DBExecutor.GetInstanceThreadSafe;
			dbExecutor = new DBExecutor();
		}

		public List<TRN_SchemeAuditChild> Get(Int32? id = null)
		{
			try
			{
				List<TRN_SchemeAuditChild> TRN_SchemeAuditChildLst = new List<TRN_SchemeAuditChild>();
				Parameters[] colparameters = new Parameters[1]{
				new Parameters("@paramId", id, DbType.Int32, ParameterDirection.Input)
				};
				TRN_SchemeAuditChildLst = dbExecutor.FetchData<TRN_SchemeAuditChild>(CommandType.StoredProcedure, "wsp_TRN_SchemeAuditChild_Get", colparameters);
				return TRN_SchemeAuditChildLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public List<TRN_SchemeAuditChild> GetDynamic(string whereCondition,string orderByExpression)
		{
			try
			{
				List<TRN_SchemeAuditChild> TRN_SchemeAuditChildLst = new List<TRN_SchemeAuditChild>();
				Parameters[] colparameters = new Parameters[2]{
				new Parameters("@paramWhereCondition", whereCondition, DbType.String, ParameterDirection.Input),
				new Parameters("@paramOrderByExpression", orderByExpression, DbType.String, ParameterDirection.Input),
				};
				TRN_SchemeAuditChildLst = dbExecutor.FetchData<TRN_SchemeAuditChild>(CommandType.StoredProcedure, "wsp_TRN_SchemeAuditChild_GetDynamic", colparameters);
				return TRN_SchemeAuditChildLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public List<TRN_SchemeAuditChild> GetPaged(int startRecordNo, int rowPerPage, string whereClause, string sortColumn, string sortOrder, ref int rows)
		{
			try
			{
				List<TRN_SchemeAuditChild> TRN_SchemeAuditChildLst = new List<TRN_SchemeAuditChild>();
				Parameters[] colparameters = new Parameters[5]{
				new Parameters("@StartRecordNo", startRecordNo, DbType.Int32, ParameterDirection.Input),
				new Parameters("@RowPerPage", rowPerPage, DbType.Int32, ParameterDirection.Input),
				new Parameters("@WhereClause", whereClause, DbType.String, ParameterDirection.Input),
				new Parameters("@SortColumn", sortColumn, DbType.String, ParameterDirection.Input),
				new Parameters("@SortOrder", sortOrder, DbType.String, ParameterDirection.Input),
				};
				TRN_SchemeAuditChildLst = dbExecutor.FetchDataRef<TRN_SchemeAuditChild>(CommandType.StoredProcedure, "TRN_SchemeAuditChild_GetPaged", colparameters, ref rows);
				return TRN_SchemeAuditChildLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public string Post(TRN_SchemeAuditChild _TRN_SchemeAuditChild, string transactionType)
		{
			string ret = string.Empty;
			try
			{
				Parameters[] colparameters = new Parameters[5]{
				new Parameters("@paramId", _TRN_SchemeAuditChild.Id, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramNumber", _TRN_SchemeAuditChild.Number, DbType.String, ParameterDirection.Input),
				new Parameters("@paramImageLocation", _TRN_SchemeAuditChild.ImageLocation, DbType.String, ParameterDirection.Input),
				new Parameters("@paramIsSignature", _TRN_SchemeAuditChild.IsSignature, DbType.Int16, ParameterDirection.Input),
				new Parameters("@paramTransactionType", transactionType, DbType.String, ParameterDirection.Input)
				};
				dbExecutor.ManageTransaction(TransactionType.Open);
				ret = dbExecutor.ExecuteScalarString(true, CommandType.StoredProcedure, "wsp_TRN_SchemeAuditChild_Post", colparameters, true);
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
