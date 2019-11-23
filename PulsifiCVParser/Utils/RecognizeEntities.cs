using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics.Models;
using Microsoft.Rest;
using PulsifiCVParser.Models;

namespace PulsifiCVParser.Helpers
{
    public static class RecognizeEntities
    {
        public static async Task<StandardCVForm> RunAsync(string endpoint, string key, string text)
        {
            var credentials = new ApiKeyServiceClientCredentials(key);
            var client = new TextAnalyticsClient(credentials)
            {
                Endpoint = endpoint
            };

            // The documents to be submitted for entity recognition. The ID can be any value.
            var inputDocument = new MultiLanguageBatchInput(
                new List<MultiLanguageInput>
                {
                    new MultiLanguageInput("1", text, "en")
                });

            var entitiesResult = await client.EntitiesBatchAsync(inputDocument);
            if (entitiesResult.Errors.Count > 0)
            {
                throw new Exception(entitiesResult.Errors[0].Message);
            }

            var parsedCV = new StandardCVForm() { Experiences = new List<Experience>() };

            // Getting name
            var recognizedPersons = entitiesResult.Documents[0].Entities.FirstOrDefault(e => e.Type == "Person");
            if (recognizedPersons != null)
            {
                var person = recognizedPersons.Matches.FirstOrDefault(m => m.EntityTypeScore > 0.7);
                parsedCV.Name = person != null ? person.Text : "<not present>";
            }

            var recognizedEmails = entitiesResult.Documents[0].Entities.FirstOrDefault(e => e.Type == "Email");
            if (recognizedEmails != null)
            {
                var email = recognizedEmails.Matches.FirstOrDefault(m => m.EntityTypeScore > 0.7);
                parsedCV.Email = email != null ? email.Text : "<not present>";
            }

            var recognizedPhones = entitiesResult.Documents[0].Entities.FirstOrDefault(e => e.Type == "Phone_Number");
            if (recognizedPhones != null)
            {
                var phone = recognizedPhones.Matches.FirstOrDefault(m => m.EntityTypeScore > 0.7);
                parsedCV.Phone = phone != null ? phone.Text : "<not present>";
            }

            var recognizedLocations = entitiesResult.Documents[0].Entities.Where(e => e.Type == "Location");
            for (var i = 0; i < recognizedLocations.Count() && i < 5; i++)
            {
                parsedCV.Address += recognizedLocations.ElementAt(i).Matches[0].Text;
            }

            var companies = entitiesResult.Documents[0].Entities.Where(e => e.Type == "Organization");
            foreach (var range in entitiesResult.Documents[0].Entities.Where(e => e.SubType == "DateRange"))
            {
                var period = range.Matches.FirstOrDefault(m => m.EntityTypeScore > 0.6);
                if(period != null)
                {
                    var experience = new Experience()
                    {
                        Period = period.Text
                    };

                    if (companies.Any())
                    {
                        // getting the first occurance of a company entity after the period
                        var company = companies.FirstOrDefault(c => c.Matches.Any(m => m.Offset > period.Offset));
                        experience.Company = company.Matches[0].Text;
                    }


                    parsedCV.Experiences.Add(experience);
                }
            }


            return parsedCV;
        }
    }

    /// <summary>
    /// Allows authentication to the API using a basic apiKey mechanism
    /// </summary>
    class ApiKeyServiceClientCredentials : ServiceClientCredentials
    {
        private readonly string subscriptionKey;

        /// <summary>
        /// Creates a new instance of the ApiKeyServiceClientCredentails class
        /// </summary>
        /// <param name="subscriptionKey">The subscription key to authenticate and authorize as</param>
        public ApiKeyServiceClientCredentials(string subscriptionKey)
        {
            this.subscriptionKey = subscriptionKey;
        }

        /// <summary>
        /// Add the Basic Authentication Header to each outgoing request
        /// </summary>
        /// <param name="request">The outgoing request</param>
        /// <param name="cancellationToken">A token to cancel the operation</param>
        public override Task ProcessHttpRequestAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            request.Headers.Add("Ocp-Apim-Subscription-Key", this.subscriptionKey);

            return base.ProcessHttpRequestAsync(request, cancellationToken);
        }
    }
}
