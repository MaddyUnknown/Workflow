using Dapper;
using System.Data;

namespace Workflow.API.Infrastructure.DataAccess.Mappers
{
    public class NullableDateOnlyMapper : SqlMapper.TypeHandler<DateOnly?>
    {
        public override DateOnly? Parse(object value)
        {
            return (value == null || value == DBNull.Value) ? null : DateOnly.FromDateTime((DateTime)value);
        }

        public override void SetValue(IDbDataParameter parameter, DateOnly? value)
        {
            parameter.Value = (!value.HasValue) ? DBNull.Value : value.Value.ToDateTime(TimeOnly.MinValue);
            parameter.DbType = DbType.Date;
        }
    }
}
