﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace tye_vnext.cli.Core
{
    public abstract class LaunchedServiceBuilder : ServiceBuilder
    {
        public LaunchedServiceBuilder(string name, ServiceSource source)
            : base(name, source)
        {
        }

        public List<EnvironmentVariableBuilder> EnvironmentVariables { get; } = new List<EnvironmentVariableBuilder>();

        public int Replicas { get; set; } = 1;
    }
}
