using System;

namespace KamranWali.CodeOptPro.CustomClasses
{
    [Serializable]
    public class FolderSelectData
    {
        public string name;
        public string path;

        public FolderSelectData(string name, string path)
        {
            this.name = name;
            this.path = path;
        }

        public FolderSelectData() : this("", "") { }
    }
}