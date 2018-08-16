using System.Net;
using System.Net.Http.Headers;
using FootballData.ExternalServices.Exceptions;
using RestSharp;
using RestSharp.Authenticators;

namespace FootballData.ExternalServices
{
    public abstract class AbstractRestService : IAbstractRestService
    {
        #region Properties

        protected abstract string ApiUrl { get; }
        protected abstract string ApiToken { get; }

        #endregion

        #region Public Methods

        public TEntity Get<TEntity>(string resourceUrl, object parameters = null) where TEntity : new()
        {
            var client = GetRestClient();

            var request = new RestRequest(resourceUrl, Method.GET);

            if (parameters != null)
                request.AddObject(parameters);

            var response = client.Execute<TEntity>(request);

            CheckFromError(response);

            return response.Data;
        }

        #endregion

        #region Protected Methods

        protected virtual RestClient GetRestClient()
        {
            var client = new RestClient(ApiUrl);

            client.AddDefaultHeader("X-Auth-Token", ApiToken);

            return client;
        }

        #endregion

        #region Private Methods

        private static void CheckFromError(IRestResponse response)
        {
            if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.NoContent)
                return;

            throw new RestServiceException(response.ErrorMessage);
        }

        #endregion
    }
}
