using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Bootstrap.Models;
using FancyTree.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace FancyTree.Controllers
{
    public class FancyTreeController : Controller
    {
        private readonly FileTreeConfig _config;
        private readonly ILogger<FancyTreeController> _logger;

        /// <summary>
        /// Creates a new instance of a <see cref="FancyTreeController"/> with injected dependencies.
        /// </summary>
        /// <param name="config"></param>
        public FancyTreeController(IOptions<FileTreeConfig> config, ILogger<FancyTreeController> logger)
        {
            _config = config.Value;
            _logger = logger;
        }

        /// <summary>
        /// Returns JSON representing the contents of a folder on the server file system.
        /// </summary>
        /// <param name="parent">The root folder for the tree.</param>
        /// <remarks>
        /// This action is only invoked by the FancyTree ajax and lazy loading mechanisms.
        /// </remarks>
        [HttpGet]
        [ActionName("DirData")]
        public IActionResult DirectoryData(string parent = "")
        {
            // NB parent must be relative. Can't navigate above basedir. Plus what if BaseView is null.
            var info = new DirectoryInfo(Path.Combine(_config.BaseDir, parent));
            List<DirectoryInfo> directories = new List<DirectoryInfo>();
            try
            {
                directories = info.GetDirectories().ToList();
            }
            catch (UnauthorizedAccessException ux)
            {
                // Folder access auth is not reliable as there are a number of reasons
                // we may not be able to access a folder. If we don't have access, we aren't interested,
                // because we don't know what folder caused the exception, out of those returned by GetDirectories().
                // NB Message to user to try again.
                // NB Log.
                ////throw;
                // TDOO Much better than BadRequest.
                ////return BadRequest(ux.Message);
            }
            var ret = directories.Select(TreeNode.FromDirInfo).ToList();
            var json = JsonConvert.SerializeObject(ret, Formatting.Indented);
            System.IO.File.WriteAllText(@"C:\Test\Logs\FancyTree.json", json);
            return Json(ret);
        }
    }
}