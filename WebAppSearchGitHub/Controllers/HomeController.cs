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
        // string key for bookmarks in session
        private const string BOOKMARKS = "bookmarks";
        
        // list with repositories by search params
        private static List<GitHubReposModel> _currentListRepositories;
        #endregion

        #region actions

        /// <summary>    
        /// Displays a list of repositories from gitHUb by search params.    
        /// </summary>   
        public async Task<ActionResult> IndexAsync(string searchParams = "")
        {
            // create empty current list of repositories
            _currentListRepositories = new List<GitHubReposModel>();

            // check searchparams 
            if (!string.IsNullOrEmpty(searchParams))
            {
                // add params for sort and order
                searchParams += "&sort=stars&order=desc";

                // get list of repositories by search params from gitHub
                _currentListRepositories = await RepositoryHelper.GetRepositoriesAsync(searchParams);
            }
            
            // create GitHubRepositoriesModel object with list of repositories by search params and return it
            // if search pparams string is empty: return empty list of repositories
            return View("Index", new GitHubRepositoriesModel { _repositories = _currentListRepositories });
        }


        /// <summary>    
        /// Add repository object to session.    
        /// </summary>   
        public void AddToBookmarks(int repositoryId)
        {
            // check param of repositoryId 
            //(if param is inncorrect: return)
            if (repositoryId <= 0)
                return;

            // get all bookmarks from session and check object 
            //(if object is null: create empty list)
            var bookmarks = Session[BOOKMARKS] as List<GitHubReposModel>;
            if (bookmarks == null)
                bookmarks = new List<GitHubReposModel>();

            // get repository object by repositoryId  from current list of repositories (result of search) 
            //(if object is null: return)
            GitHubReposModel repository = _currentListRepositories.Where(r => r.RepositoryID == repositoryId).SingleOrDefault();
            if (repository == null)
                return;

            // if object is not null: add repository object to list bookmarks of session
            bookmarks.Add(repository);

            // write new list of bookmarks to session and return
            Session[BOOKMARKS] = bookmarks;
            return;
        }

        // <summary>    
        /// Clean session (bookmarks).    
        /// </summary> 
        public void CleanAllBookmarks()
        {
            Session[BOOKMARKS] = null;
            return;
        }

        // <summary>    
        /// Remove repository object from session.    
        /// </summary> 
        public ActionResult CleanBookmark(int repositoryId)
        {
            // get all bookmarks from session and check object 
            //(if object is null: create empty list)
            var bookmarks = Session[BOOKMARKS] as List<GitHubReposModel>;
            if (bookmarks == null)
                bookmarks = new List<GitHubReposModel>();

            // check param of repositoryId 
            //(if param is inncorrect: return exists list of bookmarks)
            if (repositoryId <= 0)
                return PartialView("BookmarksPartialView", new GitHubRepositoriesModel { _repositories = bookmarks });

            // get repository object by repositoryId  from bookmarks in session 
            //(if object is null: return exists list of bookmarks)
            GitHubReposModel repository = bookmarks.Where(r => r.RepositoryID == repositoryId).SingleOrDefault();
            if (repository == null)
                return PartialView("BookmarksPartialView", new GitHubRepositoriesModel { _repositories = bookmarks });

            // if object is not null: remove repository object from bookmarks
            bookmarks.Remove(repository);

            // write new list of bookmarks in session and return it
            Session[BOOKMARKS] = bookmarks;
            return PartialView("BookmarksPartialView", new GitHubRepositoriesModel { _repositories = bookmarks });
        }

        /// <summary>    
        /// Displays a list of bookmarks in session.    
        /// </summary>  
        public ActionResult Bookmarks()
        {
            ViewBag.Message = "Your Bookmarks";

            // get all bookmarks from session
            var bookmarks = Session[BOOKMARKS] as List<GitHubReposModel>;

            // create GitHubRepositoriesModel object with list of bookmarks
            GitHubRepositoriesModel gitHubRepositories = new GitHubRepositoriesModel();
            gitHubRepositories._repositories = bookmarks != null
                ? bookmarks
                : new List<GitHubReposModel>();

            return View(gitHubRepositories);
        }

        #endregion
    }
}