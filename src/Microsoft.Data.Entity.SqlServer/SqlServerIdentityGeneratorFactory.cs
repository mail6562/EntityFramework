// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.Data.Entity.Identity;
using Microsoft.Data.Entity.Metadata;

namespace Microsoft.Data.Entity.SqlServer
{
    public class SqlServerIdentityGeneratorFactory : DefaultIdentityGeneratorFactory
    {
        public override IIdentityGenerator Create(IProperty property)
        {
            switch (property.ValueGenerationStrategy)
            {
                case ValueGenerationStrategy.Client:
                    if (property.PropertyType == typeof(Guid))
                    {
                        return new SequentialGuidIdentityGenerator();
                    }
                    goto default;

                case ValueGenerationStrategy.StoreIdentity:
                    if (property.PropertyType == typeof(int))
                    {
                        return new TemporaryIdentityGenerator();
                    }
                    goto default;

                case ValueGenerationStrategy.StoreSequence:
                    if (property.PropertyType == typeof(long))
                    {
                        return new SequenceIdentityGenerator(new SqlServerSimpleCommandExecutor("TODO: Connection string"));
                    }
                    goto default;

                default:
                    return base.Create(property);
            }
        }
    }
}
