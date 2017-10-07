using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Bootstrap.Models
{
    // TODO This is not the full node class as specified by the Bootstrap plugin.
    public class TreeNode
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonProperty("selectedIcon")]
        public string SelectedIcon { get; set; }

        [JsonProperty("nodes")]
        public List<TreeNode> Nodes { get; set; } = new List<TreeNode>();

        public static TreeNode FromDirInfo(DirectoryInfo directoryInfo)
        {
            var node = new TreeNode();
            node.Text = directoryInfo.Name;
            node.Icon = "glyphicons glyphicons-folder-minus";
            node.SelectedIcon = "glyphicons glyphicons-folder-plus";
            return node;
        }
    }   
}
