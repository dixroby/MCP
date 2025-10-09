using DynamicQueries.Core.Resources;
using System.Linq.Expressions;
using System.Reflection;

namespace DynamicQueries.Core.Extensions
{
    public static class ParameterExpressionExtensions
    {
        // p => p.BasePrize < 100
        public static Expression MakePropertyFilterExpression(this ParameterExpression p, 
            string propertyName,
            string operation,
            string value)
        {
            PropertyInfo Property = p.Type.GetPropertyByName(propertyName);

            Expression Left = Expression.Property(p, Property);

            object TypedValue = Convert.ChangeType(value, Property.PropertyType);

            Expression Right = Expression.Constant(TypedValue);

            // es un equal, es un not equal, es un greater than, lessThan or contains.
            ExpressionType ExpressionType = operation switch
            {
                "==" => ExpressionType.Equal,
                "!=" => ExpressionType.NotEqual,
                ">" => ExpressionType.GreaterThan,
                ">=" => ExpressionType.GreaterThanOrEqual,
                "<" => ExpressionType.LessThan,
                "<=" => ExpressionType.LessThanOrEqual,
                "contains" when Property.PropertyType == typeof(string) => 
                ExpressionType.Call,
                _ => throw new NotSupportedException(string.Format(
                    Messages.OperationsIsNotSupportedTemplate, operation))
            };

            Expression Result;


            if (ExpressionType == ExpressionType.Call)
            {
                var LeftLower = Expression.Call(Left, nameof(string.ToLower), Type.EmptyTypes);

                var RightLower = Expression.Call(Right, nameof(string.ToLower), Type.EmptyTypes);

                Result = Expression.Call(LeftLower, nameof(string.Contains), Type.EmptyTypes, RightLower);
            }
            else
                Result = Expression.MakeBinary(ExpressionType, Left, Right);

            return Result;

        }

        // p => p.BasePrize < 100
        // La lambda puede ser func o action, 
        public static LambdaExpression MakeFuncLambdaExpression(
            this ParameterExpression p,
            Expression body, 
            Type funcReturnType)
        {
            // lambda generico abierto, pero quiero el generico cerrado, necesito el makegenricType
            Type LambdaType = typeof(Func<,>).MakeGenericType(p.Type, funcReturnType);

            LambdaExpression Lambda = Expression.Lambda(LambdaType, body, p);
            // p => p.BasePrize < 100

            return Lambda;

        }

    }
}
