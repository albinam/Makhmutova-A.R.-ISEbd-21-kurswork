using System;
using System.Collections.Generic;
using System.Text;
using TravelAgencyBusinessLogic.BusinessLogic;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TravelAgencyBusinessLogic.BindingModel;
using System.Runtime.Serialization.Json;
using System.IO;
using TravelAgencyDatabaseImplement.Models;
using System.Xml;

namespace TravelAgencyDatabaseImplement.Implements
{
    public class BackUpLogic : BackUpAbstractLogic
    {
        protected override Assembly GetAssembly()
        {
            return typeof(BackUpLogic).Assembly;
        }
        protected override List<PropertyInfo> GetFullList()
        {
            using (var context = new TravelAgencyDatabase())
            {
                Type type = context.GetType();
                return type.GetProperties().Where(x =>
               x.PropertyType.FullName.StartsWith("Microsoft.EntityFrameworkCore.DbSet")).ToList();
            }
        }
        protected override List<T> GetList<T>()
        {
            using (var context = new TravelAgencyDatabase())
            {
                return context.Set<T>().ToList();
            }
        }
    }
}