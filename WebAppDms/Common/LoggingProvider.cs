using System;
using System.Collections.Generic;
using System.Configuration.Provider;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace WebAppDms.Common
{
    public abstract class LoggingProvider : ProviderBase
    {
        /// <summary>
        /// 缓存提供程序
        /// </summary>
        private static LoggingProvider currentProvider;

        /// <summary>
        /// lock
        /// </summary>
        private static object syncLock = new object();

        /// <summary>
        /// 当前配置的缓存提供程序
        /// </summary>
        public static LoggingProvider Current
        {
            get
            {
                if (currentProvider == null)
                {
                    lock (syncLock)
                    {
                        if (currentProvider == null)
                        {
                            var section = (LoggingProviderSection)System.Configuration.ConfigurationManager.GetSection("Log4Net/loggingProvider");

                            var langProviders = new LoggingProviderCollection();
                            ProvidersHelper.InstantiateProviders(section.Providers, langProviders, typeof(LoggingProvider));
                            currentProvider = langProviders[section.DefaultProvider];

                            if (currentProvider == null)
                            {
                                throw new ProviderException("未能加载默认的LoggingProvider");
                            }
                        }
                    }
                }

                return currentProvider;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is debug enabled.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is debug enabled; otherwise, <c>false</c>.
        /// </value>
        public abstract bool IsDebugEnabled { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is info enabled.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is info enabled; otherwise, <c>false</c>.
        /// </value>
        public abstract bool IsInfoEnabled { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is warn enabled.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is warn enabled; otherwise, <c>false</c>.
        /// </value>
        public abstract bool IsWarnEnabled { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is error enabled.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is error enabled; otherwise, <c>false</c>.
        /// </value>
        public abstract bool IsErrorEnabled { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is fatal enabled.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is fatal enabled; otherwise, <c>false</c>.
        /// </value>
        public abstract bool IsFatalEnabled { get; }

        /// <summary>
        /// 调试信息
        /// </summary>
        /// <param name="message">信息</param>
        public abstract void Debug(object message);

        /// <summary>
        /// 调试信息
        /// </summary>
        /// <param name="format">格式</param>
        /// <param name="args">参数</param>
        public abstract void DebugFormatted(string format, params object[] args);

        /// <summary>
        /// 一般信息
        /// </summary>
        /// <param name="message">信息</param>
        public abstract void Info(object message);

        /// <summary>
        /// 一般信息
        /// </summary>
        /// <param name="format">格式</param>
        /// <param name="args">参数</param>
        public abstract void InfoFormatted(string format, params object[] args);

        /// <summary>
        /// 警告信息
        /// </summary>
        /// <param name="message">信息</param>
        public abstract void Warn(object message);

        /// <summary>
        /// 警告信息
        /// </summary>
        /// <param name="message">信息</param>
        /// <param name="exception">异常</param>
        public abstract void Warn(object message, Exception exception);

        /// <summary>
        /// 警告信息
        /// </summary>
        /// <param name="format">格式</param>
        /// <param name="args">参数</param>
        public abstract void WarnFormatted(string format, params object[] args);

        /// <summary>
        /// 错误信息
        /// </summary>
        /// <param name="message">信息</param>
        public abstract void Error(object message);

        /// <summary>
        /// 错误信息
        /// </summary>
        /// <param name="message">信息</param>
        /// <param name="exception">异常</param>
        public abstract void Error(object message, Exception exception);

        /// <summary>
        /// 错误信息
        /// </summary>
        /// <param name="format">信息</param>
        /// <param name="args">参数</param>
        public abstract void ErrorFormatted(string format, params object[] args);

        /// <summary>
        /// 致命错误信息
        /// </summary>
        /// <param name="message">信息</param>
        public abstract void Fatal(object message);

        /// <summary>
        /// 致命错误信息
        /// </summary>
        /// <param name="message">信息</param>
        /// <param name="exception">异常</param>
        public abstract void Fatal(object message, Exception exception);

        /// <summary>
        /// 致命错误信息
        /// </summary>
        /// <param name="format">格式</param>
        /// <param name="args">参数</param>
        public abstract void FatalFormatted(string format, params object[] args);
    }
}