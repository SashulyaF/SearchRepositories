using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebAppSearchGitHub.Models;
using WebAppSearchGitHub.Repository;

namespace WebAppSearchGitHub.Controllers
{
    /// <summary>    
    /// controller for actions to work with repositories of gitHub 
    /// </summary> 
    public class HomeController : Controller
    {
        #region
        private const string BOOKMARKS = "bookmarks";
        
        private static List<GitHubReposModel> _currentListRepositories = new List<GitHubReposModel>();
        #endregion

        #region actions

        /// <summary>    
        /// Displays a list of repositories from gitHUb by search params.    
        /// </summary>   
        public async Task<ActionResult> IndexAsync(string searchParams = "")
        {
            if (!string.IsNullOrEmpty(searchParams))
            {
                searchParams += "&sort=stars&order=desc";
                _currentListRepositories = await RepositoryHelper.GetRepositoriesAsync(searchParams);
            }
            
            return View("Index", new GitHubRepositoriesModel { _repositories = _currentListRepositories });
        }


        /// <summary>    
        /// Add Bookmark by repositoryId   
        /// </summary>   
        public void AddToBookmarks(int repositoryId)
        {
            if (repositoryId <= 0)
                return;

            var bookmarks = Session[BOOKMARKS] as List<GitHubReposModel>;
            if (bookmarks == null)
                bookmarks = new List<GitHubReposModel>();

            GitHubReposModel repository = _currentListRepositories.Where(r => r.RepositoryID == repositoryId).SingleOrDefault();
            if (repository == null)
                return;

            bookmarks.Add(repository);

            Session[BOOKMARKS] = bookmarks;
            return;
        }

        // <summary>    
        /// Clean session (bookmarks).    
        /// </summary> 
        public void DeleteAllBookmarks()
        {
            Session[BOOKMARKS] = null;
            return;
        }

        // <summary>    
        /// Delete Bookmark by repositoryId 
        /// </summary> 
        public ActionResult DeleteBookmark(int repositoryId)
        {
            GitHubRepositoriesModel gitHubRepositories = new GitHubRepositoriesModel();

            var bookmarks = Session[BOOKMARKS] as List<GitHubReposModel>;
            if (repositoryId <= 0 
                || bookmarks == null || bookmarks.Count <= 0)
                return PartialView("~/Views/Shared/PartialViews/_Bookmarks.cshtml", gitHubRepositories);

            GitHubReposModel repository = bookmarks.Where(r => r.RepositoryID == repositoryId).SingleOrDefault();
            if (repository == null || repository.RepositoryID <= 0)
                return PartialView("~/Views/Shared/PartialViews/_Bookmarks.cshtml", gitHubRepositories);

            bookmarks.Remove(repository);
            Session[BOOKMARKS] = bookmarks;
            gitHubRepositories._repositories = bookmarks;

            return PartialView("~/Views/Shared/PartialViews/_Bookmarks.cshtml", gitHubRepositories);
        }

        /// <summary>    
        /// Displays a list of bookmarks in session.    
        /// </summary>  
        public ActionResult Bookmarks()
        {
            ViewBag.Message = "Your Bookmarks";

            GitHubRepositoriesModel gitHubRepositories = new GitHubRepositoriesModel();

            var bookmarks = Session[BOOKMARKS] as List<GitHubReposModel>;
            if (bookmarks != null && bookmarks.Count > 0)
                gitHubRepositories._repositories = bookmarks;

            return View(gitHubRepositories);
        }

        #endregion
    }
}