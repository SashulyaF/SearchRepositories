using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WebAppSearchGitHub.Models;

namespace WebAppSearchGitHub.Repository
{
    public static class RepositoryHelper
    {
        #region
        private const string GITHUB_API_SEARCH = "https://api.github.com/search/repositories?q=";
        private const string GITHUB_API_USERAGENT = "http://developer.github.com/v3/#user-agent-required";
        private const string GITHUB_API_MEDIATYPE_SEARCH = "application/vnd.github.v3+json";
        #endregion

        #region private functions

        /// <summary>    
        /// Fetch a list of repositories by search params from github    
        /// </summary>  
        private static async Task<List<GitHubReposModel>> FillData(string searchParams = "")
        {
            List<GitHubReposModel> repositories = new List<GitHubReposModel>();

            try
            {
                if (!string.IsNullOrEmpty(searchParams))
                {
                    ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                    var client = new HttpClient();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(GITHUB_API_MEDIATYPE_SEARCH));
                    client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", GITHUB_API_USERAGENT);

                    var jsonData = await client.GetStringAsync(GITHUB_API_SEARCH + searchParams);
                    if (jsonData != null)
                    {
                        dynamic data = JsonConvert.DeserializeObject(jsonData);
                        if (data != null && data.items != null)
                        {
                            if (data.items.Count > 0)
                            {
                                foreach (var item in data.items)
                                {
                                    repositories.Add(new GitHubReposModel()
                                    {
                                        RepositoryID = item.id,
                                        RepositoryName = item.name,
                                        RepositoryUrl = item.html_url,
                                        OwnerImageUrl = item.owner != null ? item.owner.avatar_url : "",
                                        OwnerGitHubUrl = item.owner != null ? item.owner.html_url : ""
                                    });
                                }
                            }
                        }
                    }
                }
            }
            catch(Exception)
            {
                return repositories;
            }
            return repositories;
        }
        #endregion

        #region public functions
        public static async Task<List<GitHubReposModel>> GetRepositoriesAsync(string searchParams = "")
        {
            return await FillData(searchParams);
        }

        #endregion
    }
}