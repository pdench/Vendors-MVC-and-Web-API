using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace VendorTest2.Models
{
    public class VendorTest2Context : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public VendorTest2Context() : base("name=VendorTest2Context")
        {
        }

        public System.Data.Entity.DbSet<VendorTest2.Models.Vendor> Vendors { get; set; }
    
    }
}
