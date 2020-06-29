// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;

namespace FFETech.Demo.IdentityServer.Models
{
    public class GrantsViewModel
    {
        #region Properties

        public IEnumerable<GrantViewModel> Grants { get; set; }

        #endregion
    }

    public class GrantViewModel
    {
        #region Properties

        public string ClientId { get; set; }

        public string ClientName { get; set; }

        public string ClientUrl { get; set; }

        public string ClientLogoUrl { get; set; }

        public string Description { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Expires { get; set; }

        public IEnumerable<string> IdentityGrantNames { get; set; }

        public IEnumerable<string> ApiGrantNames { get; set; }

        #endregion
    }
}