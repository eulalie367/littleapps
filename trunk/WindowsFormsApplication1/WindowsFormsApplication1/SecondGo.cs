using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq.Mapping;
using System.Text;
using System.Data.Linq;
using System.Reflection;

namespace WindowsFormsApplication1
{
    public static class SecondGo
    {
        /// <summary>
        /// This will insert/update the IEnumerable oject pt against the current DataContext
        /// </summary>
        /// <param name="pt"></param>
        /// <returns></returns>
        public static bool UpdateOrInsert(this DataContext context, object Table)
        {
            //cast object as indexableobject
            List<IndexedObject> objs = new List<IndexedObject>();
            objs = IndexedObject.DoIndexing(Table, objs, true);

            //get object relations set
            IEnumerable<ParentChildRelation> rels = ParentChildRelation.OrderByRelation(context);

            while (rels.Count() > 0)//while there is a rel that isn't updated 
            {
                //update insert to any table that !isUpdated and !hasChildren
                foreach (ParentChildRelation rel in rels.Where(r => !r.hasChildren))
                {

                    IndexedObject val = objs.Where(v => v.ObjectName == rel.ObjectName).Single();
                    Type t = val.Value.GetType();

                    //get properties that aren't generic; non-generic values are real db values
                    var props = from p in t.GetProperties().Where(pr => !pr.PropertyType.IsGenericType)
                                                      select new //select queries
                                                      {
                                                          Value = p.GetValue(val.Value,null),
                                                          Name = p.Name,
                                                          DbString = p.Name + " = " + p.GetValue(val.Value, null).ToString()
                                                      };


//                    System.Collections.IEnumerable a = context.ExecuteQuery(t, "select * from " + rel.ObjectName, vals);

                    //update if record exists
                    context.GetChangeSet().Updates.Add(val.Value);
                    string aasdf = "asdf";
                    //insert if record doesn't exist
                    context.GetChangeSet().Inserts.Add(val.Value);
                    rel.isUpdated = true;
                    //update object with keys from db; the correct keys won't exist before this step

                    //remove tableName from all ParentChildRelation Lists

                }
                //reset updated
                rels = rels.Where(r => !r.isUpdated);
            }
            return true;
        }
        public class IndexedObject
        {
            public string ObjectName { get; set; }
            public object Value { get; set; }
            public static List<IndexedObject>DoIndexing(object baseObj, List<IndexedObject> currentList, bool AddCurrent)
            {
                Type t = baseObj.GetType();
                if (AddCurrent)
                {
                    currentList.Add(new IndexedObject { ObjectName = t.Name, Value = baseObj });
                }
                foreach (PropertyInfo p in t.GetProperties().Where(p => p.PropertyType.Namespace == t.Namespace))// only in the baseObj namespace
                {
                    //p is type of entity
                    int count = currentList.Where(cl => cl.ObjectName == p.Name).Count();
                    if (count < 1)// don't dupe them
                    {
                        object val = p.GetValue(baseObj, null);
                        currentList.Add(new IndexedObject { ObjectName = p.Name, Value = val });
                        //get children
                        currentList = DoIndexing(val, currentList,false);
                    }
                }
                return currentList;
            }
        }
        public class ParentChildRelation
        {
            public string TableName { get; set; }
            public string ObjectName { get; set; }
            public Type EntityType { get; set; }
            public bool isUpdated { get; set; }
            public bool isChild
            {
                get
                {
                    return (Parents != null && Parents.Count > 0);
                }
            }

            public bool hasChildren
            {
                get
                {
                    return (Children != null && Children.Count > 0);
                }
            }
            public List<string> Children { get; set; }
            public List<string> Parents { get; set; }

            /// <summary>
            /// This will order a datacontext by its relations; DataMembers have parents and children, this aids in inserts. 
            /// </summary>
            /// <returns>Class retval.  Just look at it</returns>
            public static List<ParentChildRelation> OrderByRelation(DataContext context)
            {
                List<ParentChildRelation> retVal = new List<ParentChildRelation>();

                //get all tables in map
                IEnumerable<MetaTable> tbls = context.Mapping.GetTables();
                foreach (MetaTable tbl in tbls)
                {
                    //get All Entity Names
                    ParentChildRelation retTbl = new ParentChildRelation
                    {
                        TableName = tbl.TableName,
                        ObjectName = tbl.RowType.Name,
                        EntityType = tbl.RowType.Type
                    };
                    GetAssociations(tbl, retTbl);

                    retVal.Add(retTbl);
                }
                //foreach (MetaTable tbl in tbls)
                //{
                //    //get Entities
                //}

                return retVal;
            }
            public static ParentChildRelation GetAssociations(MetaTable tbl, ParentChildRelation retTbl)
            {
                IEnumerable<MetaAssociation> assoc = tbl.RowType.Associations;
                retTbl.Parents = new List<string>();
                retTbl.Children = new List<string>();
                foreach (MetaAssociation a in assoc)
                {
                    if (a.IsForeignKey)
                    {
                        //is a child of tbl
                        retTbl.Children.Add(a.OtherType.Name);

                    }
                    else if (!a.IsForeignKey)
                    {
                        //is a parent of tbl
                        retTbl.Parents.Add(a.OtherType.Name);
                    }
                }
                return retTbl;
            }
        }
    }
}
