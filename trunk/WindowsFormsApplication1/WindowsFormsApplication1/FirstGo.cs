using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;

namespace WindowsFormsApplication1
{
    class FirstGo
    {
        private void BottomUpRecursion(object pt)
        {
            Type parentType = pt.GetType();
            foreach (PropertyInfo p in parentType.GetProperties())
            {
                Type childType = p.PropertyType;
                //Are they in the same assembly?
                if (parentType.Assembly == childType.Assembly)
                {
                    string a = "asdf";
                }
            }
        }
        public void GetChangedItems(System.Data.Linq.DataContext context, object pt)
        {
            //get all tables in map
            IEnumerable<MetaTable> tbls = context.Mapping.GetTables();
            //make sure there is an object associated with the table
            tbls = tbls.Where(t => t.RowType.CanInstantiate == true);
            tbls = tbls.OrderBy(t => t.RowType.DataMembers.Count());
            foreach (MetaTable tbl in tbls)
            {
                //get primarykey field
                string t = tbl.TableName;
                Type tType = tbl.GetType();
                IEnumerable<MetaDataMember> memPK = tbl.RowType.DataMembers.Where(dm => dm.IsPrimaryKey);
                string pk = memPK.Single().Name;

                //get primarykey value
                Type objType = pt.GetType();
                PropertyInfo objPropPK = objType.GetProperty(pk);
                PropertyInfo tmpPropPK;
                object objTbl = pt;
                while (objPropPK == null)//tbl is not a member of the top level object
                {
                    tmpPropPK = objType.GetProperty(t.Split(".".ToCharArray())[1]);
                    if (tmpPropPK == null)
                        throw new Exception("Can't find the table " + t + ".  check the relations on the object.  I'll get to this eventually");
                    


                    objPropPK = tmpPropPK.PropertyType.GetProperty(pk);
                }
                object pkVal = objPropPK.GetValue(pt, null);
                string query = "";

                //set up query
                object[] objParams = null;
                if (pkVal == null)//pk is null.  Check all params just to make sure
                {
                    PropertyInfo[] props = objType.GetProperties();                    
                }
                else
                {
                    string objTblID = objType.ToString();
                    query = "where " + pk + " = " + objTblID;
                }
                
                //run query
                var matches = context.ExecuteQuery(tbl.GetType(), query, objParams);
                
                //check for existance

                //add existance and MetaTable to OverloadedMetaTable[]
            }
            //run relations recursion
        }
        private class OverloadedMetaTable
        {
            public bool Exists { get; set; }
        }
    }
}
