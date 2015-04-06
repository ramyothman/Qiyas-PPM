using System.Collections.Generic;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Configuration;
using System;
using System.Collections;
using System.Data.Linq.Mapping;
using System.IO;
using System.Xml.Linq;

namespace Qiyas.BusinessLogicLayer
{
    public static class Caching
    {
        public static List<T> FromCache<T>(this System.Linq.IQueryable<T> q, System.Data.Linq.DataContext dc)
        {
            try
            {
                string CacheId = q.ToString() + "?";
                foreach (System.Data.Common.DbParameter dbp in dc.GetCommand(q).Parameters)
                {
                    CacheId += dbp.ParameterName + "=" + dbp.Value.ToString() + "&";
                }
                List<T> objCache;
                //if (Environment.StackTrace.Contains("CMS\\"))
                //{
                    objCache = q.ToList();
                    return objCache;
                //}
                //else
                //{
                    //System.Threading.Thread.Sleep(500);
                    objCache = (List<T>)System.Web.HttpRuntime.Cache.Get(CacheId);
                //}
                if (objCache == null)
                {
                    List<string> tablesNames = dc.Mapping.GetTables().Where(t => (t.TableName.Contains("[")) ? CacheId.Contains(t.TableName.Substring(4)) : CacheId.Contains("[" + t.TableName.Substring(4) + "]")).Select(t => t.TableName.Substring(4)).ToList();
                    string connStr = dc.Connection.ConnectionString;
                    using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(connStr))
                    {
                        conn.Open();
                        System.Web.Caching.SqlCacheDependencyAdmin.EnableNotifications(connStr);
                        System.Web.Caching.SqlCacheDependency sqldep;
                        AggregateCacheDependency aggDep = new AggregateCacheDependency();
                        foreach (string tableName in tablesNames)
                        {
                            if (!System.Web.Caching.SqlCacheDependencyAdmin.GetTablesEnabledForNotifications(connStr).Contains(tableName))
                                System.Web.Caching.SqlCacheDependencyAdmin.EnableTableForNotifications(connStr, tableName);
                            if (tableName.Contains("Comment") || tableName.Contains("PollDetail"))
                                sqldep = new System.Web.Caching.SqlCacheDependency("PlatformCacheSmallPollTime", tableName);
                            else
                                sqldep = new System.Web.Caching.SqlCacheDependency("PlatformCache", tableName);
                            aggDep.Add(sqldep);
                        }

                        objCache = q.ToList();
                        if (objCache != null)
                            System.Web.HttpRuntime.Cache.Insert(CacheId, objCache, aggDep, DateTime.Now.AddDays(1), System.Web.Caching.Cache.NoSlidingExpiration);
                    }

                }
                //Return the created (or cached) List
                return objCache;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static int GetCountFromCache<T>(this System.Linq.IQueryable<T> q, System.Data.Linq.DataContext dc)
        {
            try
            {
                string CacheId = q.ToString() + "Count?";
                foreach (System.Data.Common.DbParameter dbp in dc.GetCommand(q).Parameters)
                {
                    CacheId += dbp.ParameterName + "=" + dbp.Value.ToString() + "&";
                }
                object count;
                //if (Environment.StackTrace.Contains("CMS\\"))
                //{
                    count = q.Count();
                    return (int)count;
                //}
                //else
                //{
                    //System.Threading.Thread.Sleep(500);
                    count = System.Web.HttpRuntime.Cache.Get(CacheId);
               // }
                if (count == null)
                {
                    List<string> tablesNames = dc.Mapping.GetTables().Where(t => CacheId.Contains("[" + t.TableName.Substring(4) + "]")).Select(t => t.TableName.Substring(4)).ToList();
                    string connStr = dc.Connection.ConnectionString;
                    using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(connStr))
                    {
                        conn.Open();
                        System.Web.Caching.SqlCacheDependencyAdmin.EnableNotifications(connStr);
                        System.Web.Caching.SqlCacheDependency sqldep;
                        AggregateCacheDependency aggDep = new AggregateCacheDependency();
                        foreach (string tableName in tablesNames)
                        {
                            if (!System.Web.Caching.SqlCacheDependencyAdmin.GetTablesEnabledForNotifications(connStr).Contains(tableName))
                                System.Web.Caching.SqlCacheDependencyAdmin.EnableTableForNotifications(connStr, tableName);
                            if (tableName.Contains("Comment") || tableName.Contains("PollDetail"))
                                sqldep = new System.Web.Caching.SqlCacheDependency("PlatformCacheSmallPollTime", tableName);
                            else
                                sqldep = new System.Web.Caching.SqlCacheDependency("PlatformCache", tableName);
                            aggDep.Add(sqldep);
                        }

                        count = q.Count();
                        System.Web.HttpRuntime.Cache.Insert(CacheId, count, aggDep, DateTime.Now.AddDays(1), System.Web.Caching.Cache.NoSlidingExpiration);
                    }

                }
                //Return the created (or cached) List
                return (int)count;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #region cached tables
        //public static List<T> FromCache<T>(this System.Linq.IOrderedQueryable<T> q, System.Data.Linq.DataContext dc)
        //{
        //    try
        //    {
        //        string CacheId = q.ToString() + "?";
        //        foreach (System.Data.Common.DbParameter dbp in dc.GetCommand(q).Parameters)
        //        {
        //            CacheId += dbp.ParameterName + "=" + dbp.Value.ToString() + "&";
        //        }
        //        List<T> objCache = (List<T>)System.Web.HttpRuntime.Cache.Get(CacheId);

        //        if (objCache == null)
        //        {
        //            /////////No cache... implement new SqlCacheDependeny//////////
        //            //1. Get connstring from DataContext


        //            string connStr = dc.Connection.ConnectionString;
        //            //2. Get SqlCommand from DataContext and the LinqQuery                     
        //            string sqlCmd = dc.GetCommand(q).CommandText;

        //            //3. Create Conn to use in SqlCacheDependency
        //            using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(connStr))
        //            {
        //                conn.Open();
        //                //4. Create Command to use in SqlCacheDependency
        //                using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(sqlCmd, conn))
        //                {
        //                    //5.0 Add all parameters provided by the Linq Query
        //                    foreach (System.Data.Common.DbParameter dbp in dc.GetCommand(q).Parameters)
        //                    {
        //                        cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter(dbp.ParameterName, dbp.Value));
        //                    }
        //                    //5.1 Enable DB for Notifications... Only needed once per DB...
        //                    System.Web.Caching.SqlCacheDependencyAdmin.EnableNotifications(connStr);
        //                    //5.2 Get ElementType for the query
        //                    string NotificationTable = q.ElementType.Name;
        //                    try
        //                    {
        //                        NotificationTable = NotificationTable.Substring(0, NotificationTable.IndexOf("Entity"));
        //                    }
        //                    catch
        //                    {
        //                        string temp = q.ElementType.BaseType.FullName.Substring(q.ElementType.BaseType.FullName.IndexOf("Data.") + 5).Substring(0, q.ElementType.BaseType.FullName.Substring(q.ElementType.BaseType.FullName.IndexOf("Data.") + 5).IndexOf(","));
        //                        NotificationTable = temp.Substring(0, temp.IndexOf("Entity"));
        //                    }
        //                    //5.3 Enable the elementtype for notification (if not done!)
        //                    if (!System.Web.Caching.SqlCacheDependencyAdmin.GetTablesEnabledForNotifications(connStr).Contains(NotificationTable))
        //                        System.Web.Caching.SqlCacheDependencyAdmin.EnableTableForNotifications(connStr, NotificationTable);
        //                    //6. Create SqlCacheDependency
        //                    //string FullTableName=typeof(T).ToString();
        //                    System.Web.Caching.SqlCacheDependency sqldep = new System.Web.Caching.SqlCacheDependency("PlatformCache", NotificationTable);
        //                    // - removed 090506 - 7. Refresh the LinqQuery from DB so that we will not use the current Linq cache
        //                    // - removed 090506 - dc.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues, q);                            
        //                    //8. Execute SqlCacheDepency query...
        //                    cmd.ExecuteNonQuery();
        //                    //9. Execute LINQ-query to have something to cache...
        //                    objCache = q.ToList();
        //                    //10. Cache the result but use the already created objectCache. Or else the Linq-query will be executed once more...
        //                    System.Web.HttpRuntime.Cache.Insert(CacheId, objCache, sqldep, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 1), System.Web.Caching.Cache.NoSlidingExpiration);
        //                }
        //            }
        //        }
        //        //Return the created (or cached) List
        //        return objCache;
        //    }
        //    catch (System.Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public static List<T> FromCache<T>(this System.Data.Linq.Table<T> q, System.Data.Linq.DataContext dc) where T : class
        //{
        //    try
        //    {
        //        string CacheId = q.ToString() + "?";
        //        foreach (System.Data.Common.DbParameter dbp in dc.GetCommand(q).Parameters)
        //        {
        //            CacheId += dbp.ParameterName + "=" + dbp.Value.ToString() + "&";
        //        }
        //        List<T> objCache = (List<T>)System.Web.HttpRuntime.Cache.Get(CacheId);

        //        if (objCache == null)
        //        {
        //            /////////No cache... implement new SqlCacheDependeny//////////
        //            //1. Get connstring from DataContext


        //            string connStr = dc.Connection.ConnectionString;
        //            //2. Get SqlCommand from DataContext and the LinqQuery                     
        //            string sqlCmd = dc.GetCommand(q).CommandText;

        //            //3. Create Conn to use in SqlCacheDependency
        //            using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(connStr))
        //            {
        //                conn.Open();
        //                //4. Create Command to use in SqlCacheDependency
        //                // using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(sqlCmd, conn))
        //                {
        //                    ////5.0 Add all parameters provided by the Linq Query
        //                    //foreach (System.Data.Common.DbParameter dbp in dc.GetCommand(q).Parameters)
        //                    //{
        //                    //    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter(dbp.ParameterName, dbp.Value));
        //                    //}
        //                    //5.1 Enable DB for Notifications... Only needed once per DB...
        //                    System.Web.Caching.SqlCacheDependencyAdmin.EnableNotifications(connStr);
        //                    //5.2 Get ElementType for the query                            
        //                    string NotificationTable = q.Context.Mapping.GetTable(typeof(T)).TableName;
        //                    try
        //                    {
        //                        NotificationTable = NotificationTable.Substring(0, NotificationTable.IndexOf("Entity"));
        //                    }
        //                    catch { }
        //                    //5.3 Enable the elementtype for notification (if not done!)
        //                    if (!System.Web.Caching.SqlCacheDependencyAdmin.GetTablesEnabledForNotifications(connStr).Contains(NotificationTable))
        //                        System.Web.Caching.SqlCacheDependencyAdmin.EnableTableForNotifications(connStr, NotificationTable);
        //                    //6. Create SqlCacheDependency
        //                    //string FullTableName=typeof(T).ToString();
        //                    System.Web.Caching.SqlCacheDependency sqldep = new System.Web.Caching.SqlCacheDependency("PlatformCache", NotificationTable);
        //                    // - removed 090506 - 7. Refresh the LinqQuery from DB so that we will not use the current Linq cache
        //                    // - removed 090506 - dc.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues, q);                            
        //                    //8. Execute SqlCacheDepency query...
        //                    //cmd.ExecuteNonQuery();
        //                    //9. Execute LINQ-query to have something to cache...
        //                    objCache = q.ToList();
        //                    //10. Cache the result but use the already created objectCache. Or else the Linq-query will be executed once more...
        //                    System.Web.HttpRuntime.Cache.Insert(CacheId, objCache, sqldep, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 1), System.Web.Caching.Cache.NoSlidingExpiration);
        //                }
        //            }
        //        }
        //        //Return the created (or cached) List
        //        return objCache;
        //    }
        //    catch (System.Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        #endregion
        public static T FromCache_First<T>(this System.Linq.IQueryable<T> q, System.Data.Linq.DataContext dc)
        {
            try
            {
                string CacheId = q.ToString() + "?";
                foreach (System.Data.Common.DbParameter dbp in dc.GetCommand(q).Parameters)
                {
                    CacheId += dbp.ParameterName + "=" + dbp.Value.ToString() + "&";
                }
                T objCache;
                //if (Environment.StackTrace.Contains("CMS\\"))
                //{
                    objCache = q.FirstOrDefault();
                    return objCache;
                //}
                //else
                //{
                    objCache = (T)System.Web.HttpRuntime.Cache.Get(CacheId);
               // }
                if (objCache == null)
                {
                    List<string> tablesNames = dc.Mapping.GetTables().Where(t => CacheId.Contains("[" + t.TableName.Substring(4) + "]")).Select(t => t.TableName.Substring(4)).ToList();
                    string connStr = dc.Connection.ConnectionString;
                    using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(connStr))
                    {
                        conn.Open();
                        System.Web.Caching.SqlCacheDependencyAdmin.EnableNotifications(connStr);
                        System.Web.Caching.SqlCacheDependency sqldep;
                        AggregateCacheDependency aggDep = new AggregateCacheDependency();
                        foreach (string tableName in tablesNames)
                        {
                            if (!System.Web.Caching.SqlCacheDependencyAdmin.GetTablesEnabledForNotifications(connStr).Contains(tableName))
                                System.Web.Caching.SqlCacheDependencyAdmin.EnableTableForNotifications(connStr, tableName);
                            if (tableName.Contains("Comment") || tableName.Contains("PollDetail"))
                                sqldep = new System.Web.Caching.SqlCacheDependency("PlatformCacheSmallPollTime", tableName);
                            else
                                sqldep = new System.Web.Caching.SqlCacheDependency("PlatformCache", tableName);
                            aggDep.Add(sqldep);
                        }
                        objCache = q.FirstOrDefault();
                        if (objCache != null)
                            System.Web.HttpRuntime.Cache.Insert(CacheId, objCache, aggDep, DateTime.Now.AddDays(1), System.Web.Caching.Cache.NoSlidingExpiration);
                    }
                }
                return objCache;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        public static object FromCache(System.Data.Linq.DataContext dc, string cacheId)
        {
            try
            {
                object objCache;
                objCache = System.Web.HttpRuntime.Cache.Get(cacheId);
                return objCache;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static void AddToCache(System.Data.Linq.DataContext dc, object cachedObject, string cacheId, List<string> notificationTables)
        {
            try
            {
                return;
                if (cachedObject != null)
                {
                    //List<string> tablesNames = dc.Mapping.GetTables().Where(t => CacheId.Contains("[" + t.TableName.Substring(4) + "]")).Select(t => t.TableName.Substring(4)).ToList();
                    string connStr = dc.Connection.ConnectionString;
                    using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(connStr))
                    {
                        conn.Open();
                        System.Web.Caching.SqlCacheDependencyAdmin.EnableNotifications(connStr);
                        System.Web.Caching.SqlCacheDependency sqldep;
                        AggregateCacheDependency aggDep = new AggregateCacheDependency();
                        foreach (string tableName in notificationTables)
                        {
                            if (!System.Web.Caching.SqlCacheDependencyAdmin.GetTablesEnabledForNotifications(connStr).Contains(tableName))
                                System.Web.Caching.SqlCacheDependencyAdmin.EnableTableForNotifications(connStr, tableName);
                            if (tableName.Contains("Comment") || tableName.Contains("PollDetail"))
                                sqldep = new System.Web.Caching.SqlCacheDependency("PlatformCacheSmallPollTime", tableName);
                            else
                                sqldep = new System.Web.Caching.SqlCacheDependency("PlatformCache", tableName);
                            aggDep.Add(sqldep);
                        }
                        System.Web.HttpRuntime.Cache.Insert(cacheId, cachedObject, aggDep, DateTime.Now.AddDays(1), System.Web.Caching.Cache.NoSlidingExpiration);
                    }

                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        public static object GetFileFromCache(string FilePath)
        {
            try
            {

                if (FilePath != null)
                {
                    object objCache = System.Web.HttpRuntime.Cache.Get(FilePath);
                    if (objCache == null)
                    {

                        CacheDependency dependancy = new System.Web.Caching.CacheDependency(FilePath);
                        XDocument document = XDocument.Load(FilePath);
                        System.Web.HttpRuntime.Cache.Insert(FilePath, document, dependancy, DateTime.Now.AddDays(1), System.Web.Caching.Cache.NoSlidingExpiration);
                        return document;
                    }
                    else
                        return objCache;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static List<object> GetFilesFromCache(string DirectoryPath)
        {
            try
            {

                if (DirectoryPath != null)
                {
                    DirectoryInfo dir = new DirectoryInfo(DirectoryPath);
                    List<object> files = new List<object>();
                    object objCache;
                    foreach (DirectoryInfo directory in dir.GetDirectories())
                    {
                        GetFilesFromCache(directory.FullName);
                        foreach (FileInfo fi in directory.GetFiles())
                        {

                            objCache = System.Web.HttpRuntime.Cache.Get(fi.FullName);
                            if (objCache == null)
                            {

                                CacheDependency dependancy = new System.Web.Caching.CacheDependency(fi.FullName);
                                XDocument document = XDocument.Load(fi.FullName);
                                System.Web.HttpRuntime.Cache.Insert(fi.FullName, document, dependancy, DateTime.Now.AddDays(1), System.Web.Caching.Cache.NoSlidingExpiration);
                                files.Add(document);
                            }
                            else
                                files.Add(objCache);
                        }
                    }
                    foreach (FileInfo fi in dir.GetFiles())
                    {

                        objCache = System.Web.HttpRuntime.Cache.Get(fi.FullName);
                        if (objCache == null)
                        {

                            CacheDependency dependancy = new System.Web.Caching.CacheDependency(fi.FullName);
                            XDocument document = XDocument.Load(fi.FullName);
                            System.Web.HttpRuntime.Cache.Insert(fi.FullName, document, dependancy, DateTime.Now.AddDays(1), System.Web.Caching.Cache.NoSlidingExpiration);
                            files.Add(document);
                        }
                        else
                            files.Add(objCache);
                    }


                    return files;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static object GetObjectFromCache(string cacheId)
        {
            try
            {
                object objCache;
                objCache = System.Web.HttpRuntime.Cache.Get(cacheId);
                return objCache;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static void AddObjectToCache(object cachedObject, string cacheId)
        {
            try
            {                
                if (cachedObject != null)
                {
                    System.Web.HttpRuntime.Cache.Insert(cacheId, cachedObject, null, DateTime.Now.AddDays(1), System.Web.Caching.Cache.NoSlidingExpiration);
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }


}
