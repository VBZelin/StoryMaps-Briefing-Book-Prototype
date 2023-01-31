using System;
using System.Collections;
using Esri.ArcGISRuntime.Portal;
using Esri.ArcGISRuntime.Security;

namespace ArcGIS.StoryMaps.BriefingBook.Shared.CustomComponents.ArcGISRuntime
{
    public class ArcGISPortalManager
    {
        public ArcGISPortal SignedInPortal { get; private set; }

        private SignInType _tempSignInType = SignInType.Unknown;

        public ArcGISPortalManager()
        {
        }

        public void SetSignedInPortal(ArcGISPortal securedPortal)
        {
            SignedInPortal = securedPortal;
        }

        public void ResetSignInPortal()
        {
            SignedInPortal = null;
            _tempSignInType = SignInType.Unknown;
        }

        public async Task<SignInType> CheckAuthenticationType(string portalUrl)
        {
            try
            {
                ServerInfo serverInfo = new ServerInfo(new Uri(portalUrl));

                AuthenticationManager.Current.RegisterServer(serverInfo);
                AuthenticationManager.Current.ChallengeHandler = new ChallengeHandler(CheckAuthenticationType);

                ArcGISPortal securedPortal = await ArcGISPortal.CreateAsync(new Uri(portalUrl), true);

                return SignInType.OAuth;
            }
            catch (HttpRequestException e)
            {
                if (e.Message.Contains("403") || e.Message.Contains("401"))
                {
                    return _tempSignInType;
                }

                return SignInType.Unknown;
            }
            catch (UriFormatException)
            {
                return SignInType.Unknown;
            }
            catch (Exception)
            {
                return SignInType.Unknown;
            }
        }

        private Task<Credential> CheckAuthenticationType(CredentialRequestInfo credentialRequestInfo)
        {
            _tempSignInType = SignInType.Unknown;

            switch (credentialRequestInfo.AuthenticationType)
            {
                case AuthenticationType.Token:
                    _tempSignInType = SignInType.OAuth;

                    break;

                case AuthenticationType.Certificate:
                    _tempSignInType = SignInType.PKI;

                    break;

                case AuthenticationType.NetworkCredential:
                    _tempSignInType = SignInType.IWA;

                    break;
            }

            return null;
        }
    }
}

