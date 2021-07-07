using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Comportamentos_EF_Core
{
    public class MySqlServerQuerySqlGenerator : SqlServerQuerySqlGenerator
    {
        public MySqlServerQuerySqlGenerator(QuerySqlGeneratorDependencies dependencies) :base(dependencies)
        {

        }

        protected override Expression VisitTableValuedFunction(TableValuedFunctionExpression tableValuedFunctionExpression)
        {
            var table = base.VisitTableValuedFunction(tableValuedFunctionExpression);
            Sql.Append("WITH (NOLOCK)");
            return table;
        }
    }

    public class MySqlServerQuerySqlGeneratorFactory : SqlServerQuerySqlGeneratorFactory
    {
        private readonly QuerySqlGeneratorDependencies _dependencies;
        public MySqlServerQuerySqlGeneratorFactory(QuerySqlGeneratorDependencies dependencies) :base(dependencies)
        {
            _dependencies = dependencies;
        }

        public override QuerySqlGenerator Create()
        {
            return new MySqlServerQuerySqlGenerator(_dependencies);
        }
    }
}
