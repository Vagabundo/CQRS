using System.Data;

namespace CQRS.Application.Abstractions.Data;

public interface ISqlConnectionFactory
{
    IDbConnection CreateConnection();
}
