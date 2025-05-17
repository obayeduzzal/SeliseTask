using Dapper;
using Newtonsoft.Json;
using System;
using System.Data;

namespace TTM.Core.Infrastructure.Dapper;

public class DapperJsonObjectTypeHandler : SqlMapper.ITypeHandler
{
    public void SetValue(IDbDataParameter parameter, object value)
    {
        parameter.Value = value == null
            ? DBNull.Value
            : JsonConvert.SerializeObject(value);

        parameter.DbType = DbType.String;
    }

    public object Parse(Type destinationType, object value)
    {
        return JsonConvert.DeserializeObject(value.ToString()!, destinationType)!;
    }
}

public class DapperSqlDateOnlyTypeHandler : SqlMapper.TypeHandler<DateOnly>
{
    public override void SetValue(IDbDataParameter parameter, DateOnly date)
        => parameter.Value = date.ToDateTime(new TimeOnly(0, 0));

    public override DateOnly Parse(object value)
        => DateOnly.FromDateTime((DateTime)value);
}

public class DapperSqlTimeOnlyTypeHandler : SqlMapper.TypeHandler<TimeOnly>
{
    public override void SetValue(IDbDataParameter parameter, TimeOnly time)
         => parameter.Value = time.ToString();

    public override TimeOnly Parse(object value)
         => TimeOnly.FromTimeSpan((TimeSpan)value);
}
