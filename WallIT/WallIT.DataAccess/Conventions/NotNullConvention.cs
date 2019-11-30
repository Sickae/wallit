using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;
using WallIT.Common.Attributes;

namespace WallIT.DataAccess.Conventions
{
    public class NotNullConvention : AttributePropertyConvention<NotNullAttribute>
    {
        protected override void Apply(NotNullAttribute attribute, IPropertyInstance instance)
        {
            instance.Not.Nullable();
        }
    }
}
