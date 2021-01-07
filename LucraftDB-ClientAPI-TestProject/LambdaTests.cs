using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Lucraft.Database.Client.Test
{
    public class LambdaTests
    {

        public static string LambdaToString<T>(Expression<Func<T, bool>> expression)
        {

            var replacements = new Dictionary<string, object>();
            WalkExpression(replacements, expression);

            string body = expression.Body.ToString();

            //foreach (var parm in expression.Parameters)
            //{
            //    var parmName = parm.Name;
            //    var parmTypeName = parm.Type.Name;
            //    body = body.Replace(parmName + ".", parmTypeName + ".");
            //}

            string paramName = expression.Parameters[0].Name;

            foreach (var property in typeof(T).GetProperties())
            {
                var attributes = (DatabaseProperty[])property.GetCustomAttributes(typeof(DatabaseProperty), false);
                foreach (var attribute in attributes)
                {
                    body = body.Replace(paramName + "." + property.Name, attribute.name);
                }
            }
            body = body.Replace(paramName + ".", "").Replace("AndAlso", "&&").Replace("OrElse", "||");

            foreach (var replacement in replacements)
            {
                if (replacement.Value is string)
                    body = body.Replace(replacement.Key, $"\"{replacement.Value}\"");
                else
                    body = body.Replace(replacement.Key, replacement.Value.ToString());
            }

            return body;
        }

        private static void WalkExpression(Dictionary<string, object> replacements, Expression expression)
        {
            switch (expression.NodeType)
            {
                case ExpressionType.MemberAccess:
                    string replacementExpression = expression.ToString();
                    if (replacementExpression.Contains("value("))
                    {
                        object replacementValue = Expression.Lambda(expression).Compile().DynamicInvoke();
                        if (!replacements.ContainsKey(replacementExpression))
                            replacements.Add(replacementExpression, replacementValue);
                    }
                    break;

                case ExpressionType.GreaterThan:
                case ExpressionType.GreaterThanOrEqual:
                case ExpressionType.LessThan:
                case ExpressionType.LessThanOrEqual:
                case ExpressionType.OrElse:
                case ExpressionType.AndAlso:
                case ExpressionType.Equal:
                    var binExpr = expression as BinaryExpression;
                    WalkExpression(replacements, binExpr.Left);
                    WalkExpression(replacements, binExpr.Right);
                    break;

                case ExpressionType.Call:
                    var mcexp = expression as MethodCallExpression;
                    foreach (var argument in mcexp.Arguments)
                        WalkExpression(replacements, argument);
                    break;

                case ExpressionType.Lambda:
                    var lexp = expression as LambdaExpression;
                    WalkExpression(replacements, lexp.Body);
                    break;

                case ExpressionType.Constant:
                    //do nothing
                    break;

                default:
                    Trace.WriteLine("Unknown type");
                    break;
            }
        }
    }
}
