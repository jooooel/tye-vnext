// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using tye_vnext.cli.ConfigModel;

namespace tye_vnext.cli.Core
{
    public class ApplicationFactoryFilter
    {
        public Func<ConfigService, bool>? ServicesFilter { get; set; }
        // public Func<ConfigIngress, bool>? IngressFilter { get; set; }

        public static ApplicationFactoryFilter? GetApplicationFactoryFilter(string[] tags) => GetApplicationFactoryFilter(tags, null);

        public static ApplicationFactoryFilter? GetApplicationFactoryFilter(string[]? tags, string[]? services)
        {
            tags ??= Array.Empty<string>();
            services ??= Array.Empty<string>();

            if (!tags.Any() && !services.Any())
            {
                return null;
            }

            return new ApplicationFactoryFilter
            {
                ServicesFilter = GetServiceFilter(tags, services),
                // IngressFilter = GetIngressFilter(tags, services)
            };
        }

        private static Func<ConfigService, bool>? GetServiceFilter(string[] tags, string[] services)
        {
            return service => 
                services.Any(s => service.Name.Equals(s, StringComparison.OrdinalIgnoreCase)) || 
                tags.Any(t => service.Tags.Contains(t));
        }
        
        // private static Func<ConfigIngress, bool>? GetIngressFilter(string[] tags, string[] services)
        // {
        //     return service => 
        //         services.Any(s => service.Name.Equals(s, StringComparison.OrdinalIgnoreCase)) || 
        //         tags.Any(t => service.Tags.Contains(t));
        // }
    }
}
