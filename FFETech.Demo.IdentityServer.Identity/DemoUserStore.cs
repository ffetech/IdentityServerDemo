using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FFETech.Demo.IdentityServer.Config;
using Microsoft.AspNetCore.Identity;

namespace FFETech.Demo.IdentityServer.Identity
{
    public class DemoUserStore : IUserStore<DemoUser>, IUserPasswordStore<DemoUser>
    {
        #region Fields

        private List<DemoUser> _users = new List<DemoUser>()
        {
            new DemoUser {UserName = GlobalConfig.DefaultUserName, NormalizedUserName = GlobalConfig.DefaultUserName.ToUpper(), Email = GlobalConfig.DefaultUserName, PasswordHash = GlobalConfig.DefaultUserPasswordHash }
        };
        private bool disposedValue;

        #endregion

        #region Constructors

        public DemoUserStore()
        {
        }

        #endregion

        #region Public Methods

        public DemoUser CreateInstance(string userName, string email)
        {
            return new DemoUser { UserName = userName, Email = email };
        }

        public Task<IdentityResult> CreateAsync(DemoUser user, CancellationToken cancellationToken)
        {
            try
            {
                _users.Add(user);
                return Task.FromResult(IdentityResult.Success);
            }
            catch (Exception ex)
            {
                return Task.FromResult(IdentityResult.Failed(new IdentityError { Description = ex.Message }));
            }
        }

        public Task<IdentityResult> UpdateAsync(DemoUser user, CancellationToken cancellationToken)
        {
            try
            {
                if (_users.Contains(user))
                    _users.Add(user);

                return Task.FromResult(IdentityResult.Success);
            }
            catch (Exception ex)
            {
                return Task.FromResult(IdentityResult.Failed(new IdentityError { Description = ex.Message }));
            }
        }

        public Task<IdentityResult> DeleteAsync(DemoUser user, CancellationToken cancellationToken)
        {
            try
            {
                _users.Remove(user);
                return Task.FromResult(IdentityResult.Success);
            }
            catch (Exception ex)
            {
                return Task.FromResult(IdentityResult.Failed(new IdentityError { Description = ex.Message }));
            }
        }

        public Task<DemoUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            return Task.FromResult(_users.FirstOrDefault(u => u.Id == userId));
        }

        public Task<DemoUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            return Task.FromResult(_users.FirstOrDefault(u => u.NormalizedUserName == normalizedUserName));
        }

        public Task<string> GetUserIdAsync(DemoUser user, CancellationToken cancellationToken)
        {
            return Task.Run(() => user.Id);
        }

        public Task<string> GetUserNameAsync(DemoUser user, CancellationToken cancellationToken)
        {
            return Task.Run(() => user.UserName);
        }

        public Task SetUserNameAsync(DemoUser user, string userName, CancellationToken cancellationToken)
        {
            return Task.Run(() => user.UserName = userName);
        }

        public Task<string> GetNormalizedUserNameAsync(DemoUser user, CancellationToken cancellationToken)
        {
            return Task.Run(() => user.NormalizedUserName);
        }

        public Task SetNormalizedUserNameAsync(DemoUser user, string normalizedUserName, CancellationToken cancellationToken)
        {
            return Task.Run(() => user.NormalizedUserName = normalizedUserName);
        }

        public Task<string> GetPasswordHashAsync(DemoUser user, CancellationToken cancellationToken)
        {
            return Task.Run(() => user.PasswordHash);
        }

        public Task SetPasswordHashAsync(DemoUser user, string passwordHash, CancellationToken cancellationToken)
        {
            return Task.Run(() => user.PasswordHash = passwordHash);
        }

        public Task<bool> HasPasswordAsync(DemoUser user, CancellationToken cancellationToken)
        {
            return Task.Run(() => (user.PasswordHash?.Length ?? 0) > 0);
        }

        // // TODO: Finalizer nur überschreiben, wenn "Dispose(bool disposing)" Code für die Freigabe nicht verwalteter Ressourcen enthält
        // ~DemoUserStore()
        // {
        //     // Ändern Sie diesen Code nicht. Fügen Sie Bereinigungscode in der Methode "Dispose(bool disposing)" ein.
        //     Dispose(disposing: false);
        // }
        public void Dispose()
        {
            // Ändern Sie diesen Code nicht. Fügen Sie Bereinigungscode in der Methode "Dispose(bool disposing)" ein.
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