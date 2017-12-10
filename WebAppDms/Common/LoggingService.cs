using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppDms.Common
{
    public static class LoggingService
    {
        /// <summary>
        /// 日志提供程序
        /// </summary>
        public static LoggingProvider Provider
        {
            get { return LoggingProvider.Current; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is debug enabled.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is debug enabled; otherwise, <c>false</c>.
        /// </value>
        public static bool IsDebugEnabled
        {
            get
            {
                return Provider.IsDebugEnabled;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is info enabled.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is info enabled; otherwise, <c>false</c>.
        /// </value>
        public static bool IsInfoEnabled
        {
            get
            {
                return Provider.IsInfoEnabled;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is warn enabled.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is warn enabled; otherwise, <c>false</c>.
        /// </value>
        public static bool IsWarnEnabled
        {
            get
            {
                return Provider.IsWarnEnabled;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is error enabled.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is error enabled; otherwise, <c>false</c>.
        /// </value>
        public static bool IsErrorEnabled
        {
            get
            {
                return Provider.IsErrorEnabled;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is fatal enabled.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is fatal enabled; otherwise, <c>false</c>.
        /// </value>
        public static bool IsFatalEnabled
        {
            get
            {
                return Provider.IsFatalEnabled;
            }
        }

        /// <summary>
        /// 调试信息
        /// </summary>
        /// <param name="message">信息</param>
        public static void Debug(object message)
        {
            Provider.Debug(message);
        }

        /// <summary>
        /// 调试信息
        /// </summary>
        /// <param name="format">格式</param>
        /// <param name="args">参数</param>
        public static void DebugFormatted(string format, params object[] args)
        {
            Provider.DebugFormatted(format, args);
        }

        /// <summary>
        /// 一般信息
        /// </summary>
        /// <param name="message">信息</param>
        public static void Info(object message)
        {
            Provider.Info(message);
        }

        /// <summary>
        /// 一般信息
        /// </summary>
        /// <param name="format">格式</param>
        /// <param name="args">参数</param>
        public static void InfoFormatted(string format, params object[] args)
        {
            Provider.InfoFormatted(format, args);
        }

        /// <summary>
        /// 警告信息
        /// </summary>
        /// <param name="message">信息</param>
        public static void Warn(object message)
        {
            Provider.Warn(message);
        }

        /// <summary>
        /// 警告信息
        /// </summary>
        /// <param name="message">信息</param>
        /// <param name="exception">异常</param>
        public static void Warn(object message, Exception exception)
        {
            Provider.Warn(message, exception);
        }

        /// <summary>
        /// 警告信息
        /// </summary>
        /// <param name="format">格式</param>
        /// <param name="args">参数</param>
        public static void WarnFormatted(string format, params object[] args)
        {
            Provider.WarnFormatted(format, args);
        }

        /// <summary>
        /// 错误信息
        /// </summary>
        /// <param name="message">信息</param>
        public static void Error(object message)
        {
            Provider.Error(message);
        }

        /// <summary>
        /// 错误信息
        /// </summary>
        /// <param name="message">信息</param>
        /// <param name="exception">异常</param>
        public static void Error(object message, Exception exception)
        {
            Provider.Error(message, exception);
        }

        /// <summary>
        /// 错误信息
        /// </summary>
        /// <param name="format">信息</param>
        /// <param name="args">参数</param>
        public static void ErrorFormatted(string format, params object[] args)
        {
            Provider.ErrorFormatted(format, args);
        }

        /// <summary>
        /// 致命错误信息
        /// </summary>
        /// <param name="message">信息</param>
        public static void Fatal(object message)
        {
            Provider.Fatal(message);
        }

        /// <summary>
        /// 致命错误信息
        /// </summary>
        /// <param name="message">信息</param>
        /// <param name="exception">异常</param>
        public static void Fatal(object message, Exception exception)
        {
            Provider.Fatal(message, exception);
        }

        /// <summary>
        /// 致命错误信息
        /// </summary>
        /// <param name="format">格式</param>
        /// <param name="args">参数</param>
        public static void FatalFormatted(string format, params object[] args)
        {
            Provider.FatalFormatted(format, args);
        }
    }
}