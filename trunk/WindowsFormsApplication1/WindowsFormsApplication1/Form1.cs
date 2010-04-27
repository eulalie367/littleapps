using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Data.Linq.Mapping;
namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DataClasses1DataContext dc = new DataClasses1DataContext();
            //if (dc.DatabaseExists())
            //{
            //    dc.DeleteDatabase();
            //    dc.CreateDatabase();
            //}
            //CREATE OBJECT, easily editable
            ParentTable pt = new ParentTable
            {
                //do crud
                Child1 = new Child1()
                {
                    baby1 = new baby1 { dateTimeField = DateTime.Now, intField = 0, varcharField = "somethingelse" },
                    codevalue = new codevalue { codeSetId = 1, display = "Some Text1" },
                    dateTimeField = DateTime.Now,
                    varcharField = "Hello",
                    intField = 0
                },
                Child2 = new Child2()
                {
                    baby1 = new baby1 { dateTimeField = DateTime.Now, intField = 0, varcharField = "babybaby" },
                    codevalue = new codevalue { codeSetId = 1, display = "Some Text2" },
                    dateTimeField = DateTime.Now,
                    varcharField = "Hello",
                    intField = 0
                },
                varcharField = "Changed Text",
                dateTimeField = DateTime.Now,
                intField = 123,
                codevalue = new codevalue { codeSetId = 1, display = "Some Text2" }
            };

            dc.UpdateOrInsert(pt);






            //Bottom Up Check for existance of each entity
            //Insert if doesn't exist
            //update if does
            //dc.ParentTables.InsertOnSubmit(pt);
            //dc.SubmitChanges(System.Data.Linq.ConflictMode.FailOnFirstConflict);
            string a = "";
        }
    }
}
