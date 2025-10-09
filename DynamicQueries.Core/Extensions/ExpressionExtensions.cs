using DynamicQueries.Core.Resources;
using System.Linq.Expressions;

namespace DynamicQueries.Core.Extensions
{
    public static class ExpressionExtensions
    {
        // p.BasePrice < 100, "AND", p.Name.Contains("Queso")
        public static Expression MakeJoinExpression(this Expression left,
            string joinWith, Expression right)
        {
            // podemos aplicar politicas como por ejermplo
            joinWith = joinWith?.ToUpper() ?? "And";
            ExpressionType ExpressionType = joinWith switch
            {
                "AND" or "&&" => ExpressionType.AndAlso,
                "OR" or "||" => ExpressionType.OrElse
,
                _ => throw new ArgumentException(string.Format(
                Messages.UnkownJoinWithNextValueTemplate, joinWith))
            };

            return Expression.MakeBinary(ExpressionType, left, right);

        }
    }
}
