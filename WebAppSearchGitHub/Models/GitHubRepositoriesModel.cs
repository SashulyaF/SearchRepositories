using System.Collections.Generic;

namespace WebAppSearchGitHub.Models
{
    public class GitHubRepositoriesModel
    {
        #region properties
        public List<GitHubReposModel> _repositories { get; set; } = new List<GitHubReposModel>();

        public bool BookmarksExists {
            get {
                return _repositories != null && _repositories.Count > 0;
            }
        }

        #endregion
    }
}