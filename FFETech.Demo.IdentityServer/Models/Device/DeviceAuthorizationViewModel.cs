// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
namespace FFETech.Demo.IdentityServer.Models
{
    public class DeviceAuthorizationViewModel : ConsentViewModel
    {
        #region Properties

        public string UserCode { get; set; }

        public bool ConfirmUserCode { get; set; }

        #endregion
    }
}