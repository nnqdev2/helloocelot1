using System;
using System.Runtime.CompilerServices;
using Serilog;

namespace msone.Extensions
{
    public static class LoggerExtensions
    {
        public static ILogger Here(this ILogger logger,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
        {
            return logger
                .ForContext("MemberName", memberName)
                .ForContext("FilePath", sourceFilePath)
                .ForContext("LineNumber", sourceLineNumber);
        }

        public static int WordCountNga(this String str)
        {
            return str.Split(new char[] { ' ', '.', '?' },
                             StringSplitOptions.RemoveEmptyEntries).Length;
        }
    }
}
