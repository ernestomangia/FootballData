using System;
using System.Linq;
using System.Net;
using System.Threading;
using FootballData.ExternalServices.Exceptions;
using RestSharp;

namespace FootballData.ExternalServices
{
    public abstract class AbstractRestService : IAbstractRestService
    {
        #region Properties

        protected abstract string ApiUrl { get; }
        protected abstract string ApiToken { get; }

        protected int RequestsAvailable { get; set; }
        protected int CounterResetInSeconds { get; set; }
        protected DateTime? LastRequestDate { get; set; }

        #endregion

        #region Public Methods

        public TEntity Get<TEntity>(string resourceUrl, object parameters = null) where TEntity : new()
        {
            var client = GetRestClient();

            var request = new RestRequest(resourceUrl, Method.GET);

            if (parameters != null)
                request.AddObject(parameters);

            // Check if there are requests available
            if (RequestsAvailable == 0 && LastRequestDate.HasValue)
            {
                // Calculate when will the external counter be reset
                // Add 2 extra seconds to avoid sync issues with the service (e.g. if the counter is resetting right now)
                var counterResetsDate = LastRequestDate.Value.AddSeconds(CounterResetInSeconds + 2);
                var dateTimeNow = DateTime.Now;

                // Check if we need to wait for the counter to be reset
                if (counterResetsDate > dateTimeNow)
                    Thread.Sleep(counterResetsDate - dateTimeNow);
            }

            var response = client.Execute<TEntity>(request);

            CheckResponse(response);
            UpdateHeaderVariables(response);

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

        protected virtual void UpdateHeaderVariables(IRestResponse response)
        {
            var requestsAvailableHeader = response.Headers.FirstOrDefault(h => h.Name == "X-Requests-Available");
            var counterResetHeader = response.Headers.FirstOrDefault(h => h.Name == "X-RequestCounter-Reset");

            if (requestsAvailableHeader != null)
                RequestsAvailable = int.Parse(requestsAvailableHeader.Value.ToString());

            if (counterResetHeader != null)
                CounterResetInSeconds = int.Parse(counterResetHeader.Value.ToString());

            LastRequestDate = DateTime.Now;
        }

        #endregion

        #region Private Methods

        private static void CheckResponse(IRestResponse response)
        {
            if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.NoContent)
                return;

            throw new RestServiceException(response.ErrorMessage);
        }

        #endregion
    }
}
