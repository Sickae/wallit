using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;
using System.Globalization;
using WallIT.DataAccess.Helpers;

namespace WallIT.DataAccess.Conventions
{
    public class CustomTableNameConvention : IClassConvention
    {
        public void Apply(IClassInstance instance)
        {
            instance.Table(string.Format(CultureInfo.InvariantCulture, "{0}", NameConverter.ConvertName(instance.EntityType.Name)));
        }
    }
}
