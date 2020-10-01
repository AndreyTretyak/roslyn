﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#nullable enable

using System;
using System.Linq;
using Microsoft.CodeAnalysis.Shared.Extensions;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Completion.Providers
{
    [Flags]
    internal enum OperatorPosition
    {
        None = 0,
        Prefix = 1,
        Infix = 2,
        Postfix = 4,
    }

    internal static class OperatorSymbolExtensions
    {
        internal static string GetOperatorSignOfOperator(this IMethodSymbol m)
        {
            return m.Name switch
            {
                // binary
                WellKnownMemberNames.AdditionOperatorName => "+",
                WellKnownMemberNames.BitwiseAndOperatorName => "&",
                WellKnownMemberNames.BitwiseOrOperatorName => "|",
                WellKnownMemberNames.DivisionOperatorName => "/",
                WellKnownMemberNames.EqualityOperatorName => "==",
                WellKnownMemberNames.ExclusiveOrOperatorName => "^",
                WellKnownMemberNames.GreaterThanOperatorName => ">",
                WellKnownMemberNames.GreaterThanOrEqualOperatorName => ">=",
                WellKnownMemberNames.InequalityOperatorName => "!=",
                WellKnownMemberNames.LeftShiftOperatorName => "<<",
                WellKnownMemberNames.LessThanOperatorName => "<",
                WellKnownMemberNames.LessThanOrEqualOperatorName => "<=",
                WellKnownMemberNames.ModulusOperatorName => "%",
                WellKnownMemberNames.MultiplyOperatorName => "*",
                WellKnownMemberNames.RightShiftOperatorName => ">>",
                WellKnownMemberNames.SubtractionOperatorName => "-",

                // Unary
                WellKnownMemberNames.DecrementOperatorName => "--",
                WellKnownMemberNames.FalseOperatorName => "false",
                WellKnownMemberNames.IncrementOperatorName => "++",
                WellKnownMemberNames.LogicalNotOperatorName => "!",
                WellKnownMemberNames.OnesComplementOperatorName => "~",
                WellKnownMemberNames.TrueOperatorName => "true",
                WellKnownMemberNames.UnaryNegationOperatorName => "-",
                WellKnownMemberNames.UnaryPlusOperatorName => "+",

                var name => throw ExceptionUtilities.UnexpectedValue(name),
            };
        }

        internal static int GetOperatorSortIndex(this IMethodSymbol m)
        {
            return m.Name switch
            {
                // comparison and negation
                WellKnownMemberNames.EqualityOperatorName => 0, // ==
                WellKnownMemberNames.InequalityOperatorName => 1, // !=
                WellKnownMemberNames.GreaterThanOperatorName => 2, // >
                WellKnownMemberNames.GreaterThanOrEqualOperatorName => 3, // >=
                WellKnownMemberNames.LessThanOperatorName => 4, // <
                WellKnownMemberNames.LessThanOrEqualOperatorName => 5, // <=
                WellKnownMemberNames.LogicalNotOperatorName => 6, // !
                // mathematical
                WellKnownMemberNames.AdditionOperatorName => 7, // +
                WellKnownMemberNames.SubtractionOperatorName => 8, // -
                WellKnownMemberNames.MultiplyOperatorName => 9, // *
                WellKnownMemberNames.DivisionOperatorName => 10, // /
                WellKnownMemberNames.ModulusOperatorName => 11, // %
                WellKnownMemberNames.IncrementOperatorName => 12, // ++
                WellKnownMemberNames.DecrementOperatorName => 13, // --
                WellKnownMemberNames.UnaryPlusOperatorName => 14, // +
                WellKnownMemberNames.UnaryNegationOperatorName => 15, // -
                // bit operations
                WellKnownMemberNames.BitwiseAndOperatorName => 16, // &
                WellKnownMemberNames.BitwiseOrOperatorName => 17, // |
                WellKnownMemberNames.ExclusiveOrOperatorName => 18, // ^
                WellKnownMemberNames.LeftShiftOperatorName => 19, // <<
                WellKnownMemberNames.RightShiftOperatorName => 20, // >>
                WellKnownMemberNames.OnesComplementOperatorName => 21, // ~
                // true false
                WellKnownMemberNames.FalseOperatorName => 22, // false
                WellKnownMemberNames.TrueOperatorName => 23, // true

                var name => throw ExceptionUtilities.UnexpectedValue(name),
            };
        }

        public static string GetOperatorName(this IMethodSymbol m)
        {
            return m.Name switch
            {
                // binary
                WellKnownMemberNames.AdditionOperatorName => "a + b",
                WellKnownMemberNames.BitwiseAndOperatorName => "a & b",
                WellKnownMemberNames.BitwiseOrOperatorName => "a | b",
                WellKnownMemberNames.DivisionOperatorName => "a / b",
                WellKnownMemberNames.EqualityOperatorName => "a == b",
                WellKnownMemberNames.ExclusiveOrOperatorName => "a ^ b",
                WellKnownMemberNames.GreaterThanOperatorName => "a > b",
                WellKnownMemberNames.GreaterThanOrEqualOperatorName => "a >= b",
                WellKnownMemberNames.InequalityOperatorName => "a != b",
                WellKnownMemberNames.LeftShiftOperatorName => "a << b",
                WellKnownMemberNames.LessThanOperatorName => "a < b",
                WellKnownMemberNames.LessThanOrEqualOperatorName => "a <= b",
                WellKnownMemberNames.ModulusOperatorName => "a % b",
                WellKnownMemberNames.MultiplyOperatorName => "a * b",
                WellKnownMemberNames.RightShiftOperatorName => "a >> b",
                WellKnownMemberNames.SubtractionOperatorName => "a - b",

                // Unary
                WellKnownMemberNames.DecrementOperatorName => "a--",
                WellKnownMemberNames.FalseOperatorName => "false",
                WellKnownMemberNames.IncrementOperatorName => "a++",
                WellKnownMemberNames.LogicalNotOperatorName => "!a",
                WellKnownMemberNames.OnesComplementOperatorName => "~a",
                WellKnownMemberNames.TrueOperatorName => "true",
                WellKnownMemberNames.UnaryNegationOperatorName => "-a",
                WellKnownMemberNames.UnaryPlusOperatorName => "+a",

                var name => throw ExceptionUtilities.UnexpectedValue(name),
            };
        }

        internal static OperatorPosition GetOperatorPosition(this IMethodSymbol m)
        {
            switch (m.Name)
            {
                // binary
                case WellKnownMemberNames.AdditionOperatorName:
                case WellKnownMemberNames.BitwiseAndOperatorName:
                case WellKnownMemberNames.BitwiseOrOperatorName:
                case WellKnownMemberNames.DivisionOperatorName:
                case WellKnownMemberNames.EqualityOperatorName:
                case WellKnownMemberNames.ExclusiveOrOperatorName:
                case WellKnownMemberNames.GreaterThanOperatorName:
                case WellKnownMemberNames.GreaterThanOrEqualOperatorName:
                case WellKnownMemberNames.InequalityOperatorName:
                case WellKnownMemberNames.LeftShiftOperatorName:
                case WellKnownMemberNames.LessThanOperatorName:
                case WellKnownMemberNames.LessThanOrEqualOperatorName:
                case WellKnownMemberNames.ModulusOperatorName:
                case WellKnownMemberNames.MultiplyOperatorName:
                case WellKnownMemberNames.RightShiftOperatorName:
                case WellKnownMemberNames.SubtractionOperatorName:
                    return OperatorPosition.Infix;
                // Unary
                case WellKnownMemberNames.DecrementOperatorName:
                case WellKnownMemberNames.IncrementOperatorName:
                    return OperatorPosition.Prefix | OperatorPosition.Postfix;
                case WellKnownMemberNames.FalseOperatorName:
                case WellKnownMemberNames.TrueOperatorName:
                    return OperatorPosition.None;
                case WellKnownMemberNames.LogicalNotOperatorName:
                case WellKnownMemberNames.OnesComplementOperatorName:
                case WellKnownMemberNames.UnaryNegationOperatorName:
                case WellKnownMemberNames.UnaryPlusOperatorName:
                    return OperatorPosition.Prefix;
                default:
                    throw ExceptionUtilities.UnexpectedValue(m.Name);
            }
        }

        internal static bool IsLiftable(this IMethodSymbol symbol)
        {
            // https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/expressions#lifted-operators

            // Common for all:
            if (symbol.IsUserDefinedOperator() && symbol.Parameters.All(p => p.Type.IsValueType))
            {
                switch (symbol.Name)
                {
                    // Unary
                    case WellKnownMemberNames.UnaryPlusOperatorName:
                    case WellKnownMemberNames.IncrementOperatorName:
                    case WellKnownMemberNames.UnaryNegationOperatorName:
                    case WellKnownMemberNames.DecrementOperatorName:
                    case WellKnownMemberNames.LogicalNotOperatorName:
                    case WellKnownMemberNames.OnesComplementOperatorName:
                        return symbol.Parameters.Length == 1 && symbol.ReturnType.IsValueType;
                    // Binary 
                    case WellKnownMemberNames.AdditionOperatorName:
                    case WellKnownMemberNames.SubtractionOperatorName:
                    case WellKnownMemberNames.MultiplyOperatorName:
                    case WellKnownMemberNames.DivisionOperatorName:
                    case WellKnownMemberNames.ModulusOperatorName:
                    case WellKnownMemberNames.BitwiseAndOperatorName:
                    case WellKnownMemberNames.BitwiseOrOperatorName:
                    case WellKnownMemberNames.ExclusiveOrOperatorName:
                    case WellKnownMemberNames.LeftShiftOperatorName:
                    case WellKnownMemberNames.RightShiftOperatorName:
                        return symbol.Parameters.Length == 2 && symbol.ReturnType.IsValueType;
                    // Equality + Relational 
                    case WellKnownMemberNames.EqualityOperatorName:
                    case WellKnownMemberNames.InequalityOperatorName:

                    case WellKnownMemberNames.LessThanOperatorName:
                    case WellKnownMemberNames.GreaterThanOperatorName:
                    case WellKnownMemberNames.LessThanOrEqualOperatorName:
                    case WellKnownMemberNames.GreaterThanOrEqualOperatorName:
                        return symbol.Parameters.Length == 2 && symbol.ReturnType.SpecialType == SpecialType.System_Boolean;
                }
            }

            return false;
        }
    }
}
