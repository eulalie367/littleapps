﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Reflection;

using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data.Configuration;

namespace System.Data
{
    public class SqlHelper
    {
        private static string connString;
        public static string ConnString
        {
            set
            {
                connString = value;
            }
            get
            {
                if (string.IsNullOrEmpty(connString))
                    connString = Config.Get<DataConfig>().ConnectionStrings["Sitefinity"].ConnectionString;
                return connString;
            }
        }
        public static object ExecuteScalar(string sql)
        {
            return ExecuteScalar(sql, null, CommandType.Text);
        }
        public static object ExecuteScalar(string sql, SqlParameter[] Params, CommandType commandType)
        {
            object retVal = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnString))
                {
                    using (SqlCommand com = new SqlCommand(sql, conn))
                    {
                        com.CommandType = commandType;

                        if (Params != null)
                            com.Parameters.AddRange(Params);

                        conn.Open();

                        retVal = com.ExecuteScalar();

                        conn.Close();
                    }
                }
            }
            catch (SqlException e)
            {
                //TODO:add error handling
            }

            return retVal;
        }
        public static int ExecuteNonQuery(string sql)
        {
            return ExecuteNonQuery(sql, null, CommandType.Text);
        }
        public static int ExecuteNonQuery(string sql, SqlParameter[] Params, CommandType commandType)
        {
            int retVal = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnString))
                {
                    using (SqlCommand com = new SqlCommand(sql, conn))
                    {
                        com.CommandType = commandType;
                        if (Params != null && Params.Length > 0)
                            com.Parameters.AddRange(Params);

                        conn.Open();

                        retVal = com.ExecuteNonQuery();

                        conn.Close();
                    }
                }
            }
            catch (SqlException e)
            {
                //TODO:add error handling
                throw new Exception(e.Message);
            }

            return retVal;
        }
        /// <summary>
        /// This will give all results the correct type
        /// You will need to make sure that the properties in the class you pass are named the same as the dbcolumn
        /// I would suggest using a tool like sqlmetal to generate your classes.
        /// </summary>
        /// <typeparam name="t"></typeparam>
        /// <param name="sql"></param>
        /// <param name="Params"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public static List<t> FillEntities<t>(string sql) where t : new()
        {
            return FillEntities<t>(sql, null, CommandType.Text);
        }
        /// <summary>
        /// This will give all results the correct type
        /// You will need to make sure that the properties in the class you pass are named the same as the dbcolumn
        /// I would suggest using a tool like sqlmetal to generate your classes.
        /// </summary>
        /// <typeparam name="t"></typeparam>
        /// <param name="sql"></param>
        /// <param name="Params"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public static List<t> FillEntities<t>(string sql, SqlParameter[] Params, CommandType commandType) where t : new()
        {
            List<t> retVal = new List<t>();
            t tmp = default(t);
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnString))
                {
                    using (SqlCommand com = new SqlCommand(sql, conn))
                    {
                        com.CommandType = commandType;
                        if (Params != null && Params.Length > 0)
                            com.Parameters.AddRange(Params);

                        conn.Open();

                        using (SqlDataReader reader = com.ExecuteReader())
                        {
                            string colName = "";
                            object value = null;
                            Type type = typeof(t);
                            PropertyInfo prop = null;
                            while (reader.Read())
                            {
                                tmp = new t();
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    colName = reader.GetName(i);
                                    colName = colName.Replace(" ", "");//spaces don't work in names, SqlMetal will just remove them... So will we.
                                    value = reader[i];

                                    if (string.IsNullOrEmpty(colName))
                                        prop = type.GetProperties()[i];
                                    else
                                        prop = type.GetProperty(colName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);


                                    SetPropetyValue(tmp, value, prop);
                                }
                                retVal.Add(tmp);
                            }
                            reader.Close();
                        }
                        conn.Close();
                    }
                }
            }
            catch (SqlException e)
            {
                //TODO:add error handling
            }


            return retVal;
        }
        /// <summary>
        /// This will give all results the correct type
        /// You will need to make sure that the properties in the class you pass are named the same as the dbcolumn
        /// I would suggest using a tool like sqlmetal to generate your classes.
        /// </summary>
        /// <typeparam name="t"></typeparam>
        /// <param name="sql"></param>
        /// <param name="Params"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public static t FillEntity<t>(string sql) where t : new()
        {
            return FillEntity<t>(sql, null, CommandType.Text);
        }
        /// <summary>
        /// This will give all results the correct type
        /// You will need to make sure that the properties in the class you pass are named the same as the dbcolumn
        /// I would suggest using a tool like sqlmetal to generate your classes.
        /// </summary>
        /// <typeparam name="t"></typeparam>
        /// <param name="sql"></param>
        /// <param name="Params"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public static t FillEntity<t>(string sql, SqlParameter[] Params, CommandType commandType) where t : new()
        {
            t tmp = default(t);
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnString))
                {
                    using (SqlCommand com = new SqlCommand(sql, conn))
                    {
                        com.CommandType = commandType;
                        if (Params != null && Params.Length > 0)
                            com.Parameters.AddRange(Params);

                        conn.Open();

                        using (SqlDataReader reader = com.ExecuteReader())
                        {
                            string colName = "";
                            object value = null;
                            Type type = typeof(t);
                            PropertyInfo prop = null;

                            if (reader.Read())
                            {
                                tmp = new t();
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    colName = reader.GetName(i);
                                    value = reader[i];

                                    prop = type.GetProperty(colName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                                    SetPropetyValue(tmp, value, prop);
                                }
                            }
                            reader.Close();
                        }
                        conn.Close();
                    }
                }
            }
            catch (SqlException e)
            {
                //TODO:add error handling
            }


            return tmp;
        }

        private static void SetPropetyValue(object tmp, object value, PropertyInfo prop)
        {
            //add logic here for any DB type that doesn't parse corretly, such as an XElement.

            if (!(value is DBNull) && prop != null)
            {
                if (prop.PropertyType != typeof(System.Xml.Linq.XElement))//this really comes back as a string from the db.
                    prop.SetValue(tmp, value, null);
                else
                {
                    if (value != null)
                    {
                        string tmpVal = value.ToString();
                        if (!string.IsNullOrEmpty(tmpVal))
                        {
                            try
                            {
                                System.Xml.Linq.XElement elem = System.Xml.Linq.XElement.Parse(tmpVal);
                                prop.SetValue(tmp, elem, null);
                            }
                            catch 
                            {
                                try//make sure there is a root node
                                {
                                    System.Xml.Linq.XElement elem = System.Xml.Linq.XElement.Parse(string.Format("<rootasdf>{0}</rootasdf>", tmpVal));
                                    prop.SetValue(tmp, elem, null);
                                }
                                catch
                                { }
                            }
                        }
                    }
                }
            }
        }
    }
}
