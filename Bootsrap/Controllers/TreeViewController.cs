using System;
using System.Collections.Generic;
using System.IO;
using Bootstrap.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Bootstrap.Controllers
{
    public class TreeViewController : Controller
    {
        private FileTreeConfig _config;

        public TreeViewController(IOptions<FileTreeConfig> config)
        {
            _config = config.Value;
        }

        public IActionResult TreeData(string dir = "")
        {
            var browsingRoot = Path.Combine(_config.BaseDir, dir);
            var nodes = new List<TreeNode>();
            nodes.AddRange(RecurseDirectory(browsingRoot));
            return Json(nodes);
        }

        private List<TreeNode> RecurseDirectory(string directory)
        {
            var ret = new List<TreeNode>();
            var dirInfo = new DirectoryInfo(directory);

            try
            {
                var directories = dirInfo.GetDirectories("*", SearchOption.TopDirectoryOnly);
                foreach (var dir in directories)
                {
                    if (dir.FullName.ToLower() == dirInfo.FullName)
                    {
                        continue;
                    }
                    var thisNode = TreeNode.FromDirInfo(dir);
                    thisNode.Nodes.AddRange(RecurseDirectory(dir.FullName));
                    ret.Add(thisNode);
                }
            }
            catch (UnauthorizedAccessException ux)
            {
                // NB Log.
            }

            return ret;
        }
    }
}