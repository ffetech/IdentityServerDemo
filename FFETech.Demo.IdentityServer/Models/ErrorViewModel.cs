// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityServer4.Models;

namespace FFETech.Demo.IdentityServer.Models
{
    public class ErrorViewModel
    {
        #region Constructors

        public ErrorViewModel()
        {
        }

        public ErrorViewModel(string error)
        {
            Error = new ErrorMessage { Error = error };
        }

        #endregion

        #region Properties

        public ErrorMessage Error { get; set; }

        #endregion
    }
}