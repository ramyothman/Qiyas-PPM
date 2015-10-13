using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;


namespace Qiyas.BusinessLogicLayer
{
    public static class DataTableHelper
    {
        // Helper function for ADO.Net Bulkcopy to transfer a IEnumerable list to a datatable
        // Adapted from: http://msdn.microsoft.com/en-us/library/bb396189.aspx
        public static DataTable CopyToDataTable<T>(this IEnumerable<T> source)
        {
            return new DataTableCreator<T>().CreateDataTable(source, null, null);
        }

        public static DataTable CopyToDataTable<T>(this IEnumerable<T> source, DataTable table, LoadOption? options)
        {
            return new DataTableCreator<T>().CreateDataTable(source, table, options);
        }

        public static void BulkCopyToDatabase<T>(this IEnumerable<T> source, string tableName, System.Data.Linq.DataContext databaseContext) where T : class
        {
            if (tableName.Contains("."))
            {
                string[] splitTableName = tableName.Split('.');
                tableName = string.Format("[{0}].[{1}]", splitTableName[0], splitTableName[1]);
            }
            using (var dataTable = DataTableHelper.CopyToDataTable(source))
            {
                using (var bulkCopy = new SqlBulkCopy(databaseContext.Connection.ConnectionString, SqlBulkCopyOptions.KeepIdentity & SqlBulkCopyOptions.KeepNulls))
                {
                    foreach (DataColumn dc in dataTable.Columns)
                    {
                        if (dc.ColumnName == "lastException")
                            continue;
                        if (dc.ColumnName == "isNew")
                            continue;
                        if (dc.ColumnName == "context")
                            continue;
                        if (dc.ColumnName == "HasObject")
                            continue;
                        if (dc.ColumnName == "entity")
                            continue;
                        bulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping(dc.ColumnName, dc.ColumnName));
                    }
                        

                    //  We could use "dataTable.TableName" in the following line, but this does sometimes have problems, as 
                    //  LINQ-to-SQL will drop trailing "s" off table names, so try to insert into [Product], rather than [Products]
                    
                    bulkCopy.DestinationTableName = tableName;  
                    bulkCopy.WriteToServer(dataTable);
                }
            }
        }

        public static bool SaveBulkItemsList<T, C>(this IEnumerable<T> source, IEnumerable<C> sourceChild, string tableName, string tableNameChild) 
        {
            bool IsSuccessSave = false;
            SqlTransaction transaction = null;
            try
            {
                #region Table Names
                if (tableName.Contains("."))
                {
                    string[] splitTableName = tableName.Split('.');
                    tableName = string.Format("[{0}].[{1}]", splitTableName[0], splitTableName[1]);
                }

                if (tableNameChild.Contains("."))
                {
                    string[] splitTableName = tableNameChild.Split('.');
                    tableNameChild = string.Format("[{0}].[{1}]", splitTableName[0], splitTableName[1]);
                }
                #endregion

                using (Qiyas.DataAccessLayer.QiyasLinqDataContext databaseContext = new DataAccessLayer.QiyasLinqDataContext())
                {
                    var connectionString = databaseContext.Connection.ConnectionString;
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        using (transaction = connection.BeginTransaction())
                        {
                            var DT_tbl_FirstTable = DataTableHelper.CopyToDataTable(source);
                            var DT_tbl_SecondTable = DataTableHelper.CopyToDataTable(sourceChild);
                            using (SqlBulkCopy bulkCopy_tbl_FirstTable = new SqlBulkCopy(connection, SqlBulkCopyOptions.KeepIdentity, transaction))
                            {


                                bulkCopy_tbl_FirstTable.BatchSize = 5000;
                                bulkCopy_tbl_FirstTable.DestinationTableName = tableName;
                                List<DataColumn> pkList = new List<DataColumn>();
                                foreach (DataColumn dc in DT_tbl_FirstTable.Columns)
                                {
                                    if (dc.ColumnName == "lastException")
                                        continue;
                                    if (dc.ColumnName == "isNew")
                                        continue;
                                    if (dc.ColumnName == "context")
                                        continue;
                                    if (dc.ColumnName == "HasObject")
                                        continue;
                                    if (dc.ColumnName == "entity")
                                        continue;
                                    if (dc.ColumnName == "ModelandNumber")
                                        continue;
                                    if (dc.ColumnName == "Speciality")
                                        continue;
                                    if (dc.ColumnName == "PackageTypeName")
                                        continue;
                                    if (dc.ColumnName == "LastBookCount")
                                        continue;
                                    if (dc.ColumnName == "ItemModels")
                                        continue;
                                    if (dc.ColumnName == "BookPackItemID")
                                    {
                                        dc.Unique = true;
                                        dc.AutoIncrement = true;
                                        dc.AutoIncrementStep = 1;
                                        pkList.Add(dc);
                                    }
                                    bulkCopy_tbl_FirstTable.ColumnMappings.Add(new SqlBulkCopyColumnMapping(dc.ColumnName, dc.ColumnName));
                                }

                                DT_tbl_FirstTable.PrimaryKey = pkList.ToArray();
                                //bulkCopy_tbl_FirstTable.ColumnMappings.Add("ID", "ID");
                                //bulkCopy_tbl_FirstTable.ColumnMappings.Add("UploadFileID", "UploadFileID");
                                //bulkCopy_tbl_FirstTable.ColumnMappings.Add("Active", "Active");
                                //bulkCopy_tbl_FirstTable.ColumnMappings.Add("CreatedUserID", "CreatedUserID");
                                //bulkCopy_tbl_FirstTable.ColumnMappings.Add("CreatedDate", "CreatedDate");
                                //bulkCopy_tbl_FirstTable.ColumnMappings.Add("UpdatedUserID", "UpdatedUserID");
                                //bulkCopy_tbl_FirstTable.ColumnMappings.Add("UpdatedDate", "UpdatedDate");
                                bulkCopy_tbl_FirstTable.WriteToServer(DT_tbl_FirstTable);
                            }

                            using (SqlBulkCopy bulkCopy_tbl_SecondTable = new SqlBulkCopy(connection, SqlBulkCopyOptions.KeepIdentity, transaction))
                            {

                                bulkCopy_tbl_SecondTable.BatchSize = 5000;
                                bulkCopy_tbl_SecondTable.DestinationTableName = tableNameChild;
                                List<DataColumn> pkList = new List<DataColumn>();
                                foreach (DataColumn dc in DT_tbl_SecondTable.Columns)
                                {
                                    if (dc.ColumnName == "lastException")
                                        continue;
                                    if (dc.ColumnName == "isNew")
                                        continue;
                                    if (dc.ColumnName == "context")
                                        continue;
                                    if (dc.ColumnName == "HasObject")
                                        continue;
                                    if (dc.ColumnName == "entity")
                                        continue;
                                    if (dc.ColumnName == "BookPackItemModelID")
                                    {
                                        continue;
                                    }
                                    bulkCopy_tbl_SecondTable.ColumnMappings.Add(new SqlBulkCopyColumnMapping(dc.ColumnName, dc.ColumnName));
                                }

                                DT_tbl_SecondTable.PrimaryKey = pkList.ToArray();
                                //bulkCopy_tbl_SecondTable.ColumnMappings.Add("ID", "ID");
                                //bulkCopy_tbl_SecondTable.ColumnMappings.Add("UploadFileDetailID", "UploadFileDetailID");
                                //bulkCopy_tbl_SecondTable.ColumnMappings.Add("CompaignFieldMasterID", "CompaignFieldMasterID");
                                //bulkCopy_tbl_SecondTable.ColumnMappings.Add("Value", "Value");
                                //bulkCopy_tbl_SecondTable.ColumnMappings.Add("Active", "Active");
                                //bulkCopy_tbl_SecondTable.ColumnMappings.Add("CreatedUserID", "CreatedUserID");
                                //bulkCopy_tbl_SecondTable.ColumnMappings.Add("CreatedDate", "CreatedDate");
                                //bulkCopy_tbl_SecondTable.ColumnMappings.Add("UpdatedUserID", "UpdatedUserID");
                                //bulkCopy_tbl_SecondTable.ColumnMappings.Add("UpdatedDate", "UpdatedDate");
                                bulkCopy_tbl_SecondTable.WriteToServer(DT_tbl_SecondTable);
                            }


                            transaction.Commit();
                            IsSuccessSave = true;
                        }
                        connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                if (transaction != null)
                    transaction.Rollback();



                if (ex.InnerException != null)
                {

                }
            }
            return IsSuccessSave;
        }
        

        public static void BulkCopyToDatabase<T>(this IEnumerable<T> source, string schema, string tableName, System.Data.Linq.DataContext databaseContext) where T : class
        {
            using (var dataTable = DataTableHelper.CopyToDataTable(source))
            {
                using (var bulkCopy = new SqlBulkCopy(databaseContext.Connection.ConnectionString, SqlBulkCopyOptions.KeepIdentity & SqlBulkCopyOptions.KeepNulls))
                {
                    foreach (DataColumn dc in dataTable.Columns)
                        bulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping(dc.ColumnName, dc.ColumnName));

                    //  We could use "dataTable.TableName" in the following line, but this does sometimes have problems, as 
                    //  LINQ-to-SQL will drop trailing "s" off table names, so try to insert into [Product], rather than [Products]

                    bulkCopy.DestinationTableName = string.Format("[{0}].[{1}]", schema, tableName);
                    bulkCopy.WriteToServer(dataTable);
                }
            }
        }
    }
}
