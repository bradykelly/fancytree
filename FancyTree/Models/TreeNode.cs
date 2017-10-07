using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FancyTree.Models
{
    public class TreeNode
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("folder")]
        public bool IsFolder { get; set; }

        // NB Add to article's TreeNode.
        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonProperty("lazy")]
        public bool IsLazy { get; set; }

        [JsonProperty("children")]
        public List<TreeNode> Children { get; set; } = new List<TreeNode>();

        /// <summary>
        /// Override to make debugging easier with the debugger showing node info.
        /// </summary>
        /// <returns>The <c>Title</c> property, or if it is null, the <c>Key</c> property of this node.</returns>
        public override string ToString()
        {
            return Title ?? Key;
        }

        /// <summary>
        /// Builds a new <see cref="TreeNode"/> based on data provided in a <see cref="DirectoryInfo"/> object.
        /// </summary>
        /// <param name="dirInfo">The object to read data from for the new node.</param>
        /// <returns>A <see cref="TreeNode"/> based on <paramref name="dirInfo"/>.</returns>
        public static TreeNode FromDirInfo(DirectoryInfo dirInfo)
        {
            var node = new TreeNode
            {
                Title = dirInfo.Name,
                Key = dirInfo.FullName,
                Icon = "halflings glyphicons-folder-open",
                IsFolder = true,
                IsLazy = true,
                Children = null
            };
            
            return node;
        }
    }
}
