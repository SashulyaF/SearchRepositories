
namespace WebAppSearchGitHub.Models
{
    public class GitHubReposModel
    {
        #region properties
        public int RepositoryID { get; set; } = 0;

        public string RepositoryName { get; set; } = string.Empty;

        public string RepositoryUrl { get; set; } = string.Empty;

        public string OwnerImageUrl { get; set; } = string.Empty;

        public string OwnerGitHubUrl { get; set; } = string.Empty;

        #endregion
    }
}