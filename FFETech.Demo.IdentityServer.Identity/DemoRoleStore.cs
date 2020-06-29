using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;

namespace FFETech.Demo.IdentityServer.Identity
{
    public class DemoRoleStore : IRoleStore<DemoRole>
    {
        #region Fields

        private List<DemoRole> _roles = new List<DemoRole>()
        {
            new DemoRole {Name = "admin", NormalizedName = "ADMIN" }
        };
        private bool disposedValue;

        #endregion

        #region Constructors

        public DemoRoleStore()
        {
        }

        #endregion

        #region Public Methods

        public DemoRole CreateInstance(string name)
        {
            return new DemoRole { Name = name };
        }

        public Task<IdentityResult> CreateAsync(DemoRole role, CancellationToken cancellationToken)
        {
            try
            {
                _roles.Add(role);
                return Task.FromResult(IdentityResult.Success);
            }
            catch (Exception ex)
            {
                return Task.FromResult(IdentityResult.Failed(new IdentityError { Description = ex.Message }));
            }
        }

        public Task<IdentityResult> UpdateAsync(DemoRole role, CancellationToken cancellationToken)
        {
            try
            {
                if (_roles.Contains(role))
                    _roles.Add(role);

                return Task.FromResult(IdentityResult.Success);
            }
            catch (Exception ex)
            {
                return Task.FromResult(IdentityResult.Failed(new IdentityError { Description = ex.Message }));
            }
        }

        public Task<IdentityResult> DeleteAsync(DemoRole role, CancellationToken cancellationToken)
        {
            try
            {
                _roles.Remove(role);
                return Task.FromResult(IdentityResult.Success);
            }
            catch (Exception ex)
            {
                return Task.FromResult(IdentityResult.Failed(new IdentityError { Description = ex.Message }));
            }
        }

        public Task<DemoRole> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            return Task.FromResult(_roles.FirstOrDefault(u => u.Id == roleId));
        }

        public Task<DemoRole> FindByNameAsync(string normalizedName, CancellationToken cancellationToken)
        {
            return Task.FromResult(_roles.FirstOrDefault(u => u.NormalizedName == normalizedName));
        }

        public Task<string> GetRoleIdAsync(DemoRole role, CancellationToken cancellationToken)
        {
            return Task.Run(() => role.Id);
        }

        public Task<string> GetRoleNameAsync(DemoRole role, CancellationToken cancellationToken)
        {
            return Task.Run(() => role.Name);
        }

        public Task SetRoleNameAsync(DemoRole role, string roleName, CancellationToken cancellationToken)
        {
            return Task.Run(() => role.Name = roleName);
        }

        public Task<string> GetNormalizedRoleNameAsync(DemoRole role, CancellationToken cancellationToken)
        {
            return Task.Run(() => role.NormalizedName);
        }

        public Task SetNormalizedRoleNameAsync(DemoRole role, string normalizedName, CancellationToken cancellationToken)
        {
            return Task.Run(() => role.NormalizedName = normalizedName);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion

        #region Protected Methods

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                }

                disposedValue = true;
            }
        }

        #endregion
    }
}