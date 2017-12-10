using System;
using System.Collections.Generic;
using System.Configuration.Provider;
using System.Linq;
using System.Web;

namespace WebAppDms.Common
{
    public class LoggingProviderCollection : ProviderCollection
    {
        /// <summary>
        /// 获取具有指定名称的提供程序。
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>具有指定名称的提供程序。</returns>
        public new LoggingProvider this[string name]
        {
            get { return (LoggingProvider)base[name]; }
        }

        /// <summary>
        /// 向集合中添加提供程序。
        /// </summary>
        /// <param name="provider">要添加的提供程序。</param>
        public override void Add(ProviderBase provider)
        {
            if (provider == null)
            {
                throw new ArgumentNullException("provider");
            }

            if (!(provider is LoggingProvider))
            {
                throw new ArgumentException("Invalid provider type", "provider");
            }

            base.Add(provider);
        }
    }
}