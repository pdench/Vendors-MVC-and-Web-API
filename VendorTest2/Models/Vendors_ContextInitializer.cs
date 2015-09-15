using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VendorTest2.Models;
using System.Data.Entity;

namespace VendorTest2.Models
{
    public class Vendors_ContextInitializer:DropCreateDatabaseAlways<VendorTest2Context>
    {
        protected override void Seed(VendorTest2Context context)
        {

            var vendors = new List<Vendor> {
                new Vendor() { VendorCode="VC6000170188", VendorName="KINGSTOWN CORP", ValidFrom=new DateTime(2013, 1, 1) , ValidThru=new DateTime(2013, 12, 31)  },
                new Vendor() { VendorCode="VC6000157616", VendorName="WORCESTER SAND & GRAVEL CO INC", ValidFrom=new DateTime(2013, 1, 1), ValidThru=new DateTime(2013, 12, 31) },
                new Vendor() { VendorCode="VC6000165473", VendorName="P A LANDERS INC", ValidFrom=new DateTime(2013, 1, 1), ValidThru= new DateTime(2013, 12, 31)},
                new Vendor() { VendorCode="VC6000249704", VendorName="INTERNATIONAL SALT CO LLC", ValidFrom=new DateTime(2013, 1, 1), ValidThru= new DateTime(2013, 12, 31)},
                new Vendor() { VendorCode="VC0000280761", VendorName="AMERICAN ROCK SALT CO LLC", ValidFrom=new DateTime(2013, 1, 1), ValidThru=new DateTime(2013, 12, 31) },
                new Vendor() { VendorCode="VC6000243027", VendorName="CARGILL INC", ValidFrom=new DateTime(2009, 4, 12), ValidThru= new DateTime(2009, 12, 31)},
                new Vendor() { VendorCode="VC6000179030", VendorName="EASTERN SALT CO INC", ValidFrom=new DateTime(2014, 1, 1), ValidThru=new DateTime(2014, 12, 31) },
                new Vendor() { VendorCode="VC6000062189", VendorName="GRANITE STATE MINERALS INC", ValidFrom=new DateTime(2014, 1, 1), ValidThru= new DateTime(2014, 12, 31)},
                new Vendor() { VendorCode="VC6000158222", VendorName="ALL STATES ASPHALT INC", ValidFrom=new DateTime(2013, 1, 1), ValidThru=new DateTime(2013, 12, 31) },
                new Vendor() { VendorCode="VC0000311898", VendorName="INNOVATIVE MUNICIPAL PRODUCTS (US) INC", ValidFrom=new DateTime(2013, 1, 1), ValidThru=new DateTime(2013, 12, 31) },
                new Vendor() { VendorCode="VC6000158470", VendorName="W J GRAVES CONSTRUCTION CO INC", ValidFrom=new DateTime(2015, 1, 1), ValidThru=new DateTime(2015, 12, 31) },
                new Vendor() { VendorCode="VC0000181525", VendorName="PYNE SAND & STONE CO. INC.", ValidFrom=new DateTime(2015, 1, 1), ValidThru=new DateTime(2015, 12, 31) }                
            };

            vendors.ForEach(b => context.Vendors.Add(b));
            context.SaveChanges();
            
            base.Seed(context);
        }
    }
}