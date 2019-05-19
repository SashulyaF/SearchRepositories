
namespace WebAppSearchGitHub.Models
{
    public class GitHubReposModel
    {
        #region properties
        public int RepositoryID { get; set; }

        public string RepositoryName { get; set; }

        public string RepositoryUrl { get; set; }

        public string OwnerImageUrl { get; set; }

        public string OwnerGitHubUrl { get; set; }

        #endregion
    }
}