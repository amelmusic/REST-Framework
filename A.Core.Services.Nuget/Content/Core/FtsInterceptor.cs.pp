using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace $rootnamespace$.Core //DD
{
    public class FtsInterceptor : IDbCommandInterceptor
    {
        private const string FullTextPrefix = "-FTSPREFIX-";
        public static string Fts(string search)
        {
            return $"({FullTextPrefix}{search})";
        }
        public void NonQueryExecuting(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
        }
        public void NonQueryExecuted(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
        }
        public void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            RewriteFullTextQuery(command);
        }
        public void ReaderExecuted(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
        }
        public void ScalarExecuting(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            RewriteFullTextQuery(command);
        }
        public void ScalarExecuted(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
        }
        public static void RewriteFullTextQuery(DbCommand cmd)
        {
            string text = cmd.CommandText;
            for (int i = 0; i < cmd.Parameters.Count; i++)
            {
                DbParameter parameter = cmd.Parameters[i];

                if (parameter.DbType == DbType.String
                    || parameter.DbType == DbType.AnsiString
                    || parameter.DbType == DbType.StringFixedLength
                    || parameter.DbType == DbType.AnsiStringFixedLength
                    )
                {
                    if (parameter.Value == DBNull.Value)
                        continue;
                    var value = (string)parameter.Value;
                    if (value.IndexOf(FullTextPrefix, StringComparison.Ordinal) >= 0)
                    {

                        if (value.Any(Char.IsWhiteSpace) || value.Any(x => x == '*'))
                        {
                            value = PreProcessSearchKey(value);
                        }
                        parameter.Size = 4096;
                        parameter.DbType = DbType.AnsiStringFixedLength;
                        value = value.Replace(FullTextPrefix, ""); // remove prefix we added n linq query
                        value = value.Replace("%", ""); // remove %% escaping by linq translator from string.Contains to sql LIKE
                        value = Regex.Replace(value, @"\s+", " "); //replace multiplespaces
                        value = Regex.Replace(value, @"[^a-zA-Z0-9 - * ""]", "").Trim();//remove non-alphanumeric characters and trim spaces

                        parameter.Value = value;
                        cmd.CommandText = Regex.Replace(text,
                            $@"\[(\w*)\].\[(\w*)\]\s*LIKE\s*@{parameter.ParameterName}\s?(?:ESCAPE N?'~')",
                            $@"contains([$1].[$2], @{parameter.ParameterName})");
                        if (text == cmd.CommandText)
                            throw new Exception("FTS was not replaced on: " + text);
                        text = cmd.CommandText;
                    }
                }
            }
        }

        public static string PreProcessSearchKey(string searchKey)
        {
            var splitedKeyWords = searchKey.Split(null); //split from whitespaces

            // string[] addDoubleQuotes = new string[splitedKeyWords.Length];

            for (int j = 0; j < splitedKeyWords.Length; j++)
            {
                splitedKeyWords[j] = $"\"{splitedKeyWords[j]}\"";
            }

            return string.Join(" OR ", splitedKeyWords);
        }
    }
}
