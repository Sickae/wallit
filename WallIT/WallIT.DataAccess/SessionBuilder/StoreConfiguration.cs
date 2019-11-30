using FluentNHibernate;
using FluentNHibernate.Automapping;
using System;
using WallIT.DataAccess.Entities.Base;
using WallIT.DataAccess.Helpers;

namespace WallIT.DataAccess.SessionBuilder
{
    public class StoreConfiguration : DefaultAutomappingConfiguration
    {
        public override bool ShouldMap(Type type)
        {
            return type.IsSubclassOf(typeof(EntityBase));
        }

        public override string GetComponentColumnPrefix(Member member)
        {
            var result = NameConverter.ConvertName(member.Name) + "_";
            return result;
        }
    }
}
