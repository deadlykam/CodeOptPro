using KamranWali.CodeOptPro.CustomClasses;
using System.Collections.Generic;
using UnityEngine;

namespace KamranWali.CodeOptPro.ScriptableObjects
{
    [CreateAssetMenu(fileName = "FolderSelectSettings",
                     menuName = "CodeOptPro/ScriptableObjects/Managers/" +
                                "FolderSelectSettings",
                     order = 1)]
    public class FolderSelectSettings : BaseScriptableObject
    {
        [SerializeField] private List<FolderSelectData> _data;

        private string _toString;
        private int _index;
        private FolderSelectData _swap;

        /// <summary>
        /// This method adds a folder selection to the data.
        /// </summary>
        /// <param name="name">The name of the folder, of type string</param>
        /// <param name="path">The path to the folder, of type string</param>
        public void AddFolder(string name, string path) => _data.Add(new FolderSelectData(name, path));

        /// <summary>
        /// This method updates the selected folder.
        /// </summary>
        /// <param name="name">The name of the folder to be updated, of type string</param>
        /// <param name="path">The path of the folder to be updated, of type string</param>
        /// <param name="index">The indexth data to be updated, of type int</param>
        public void UpdateFolder(string name, string path, int index) 
        {
            _data[index].name = name;
            _data[index].path = path;
        }

        /// <summary>
        /// This method gets the folder selection data.
        /// </summary>
        /// <param name="index">The indexth folder selection data to get, of type int</param>
        /// <returns>The selected folder data, of type FolderSelectData</returns>
        public FolderSelectData GetFolder(int index) => _data[index];

        /// <summary>
        /// This method swaps two elements.
        /// </summary>
        /// <param name="a">The index of element 1 to be swapped, of type int</param>
        /// <param name="b">The index of element 2 to be swapped, of type int</param>
        public void SwapFolders(int a, int b)
        {
            _swap = _data[a];
            _data[a] = _data[b];
            _data[b] = _swap;
            _swap = null;
        }

        /// <summary>
        /// This method moves the indexth element up.
        /// </summary>
        /// <param name="index">The indexth element to move up, of type int</param>
        public void SwapFolderUp(int index) => SwapFolders(index - 1, index);

        /// <summary>
        /// This method moves the indexth element down.
        /// </summary>
        /// <param name="index">The indexth element to move down, of type int</param>
        public void SwapFolderDown(int index) => SwapFolders(index + 1, index);

        /// <summary>
        /// This method removes the selected folder from the data.
        /// </summary>
        /// <param name="index">The indexth folder selection data to remove, of type int</param>
        public void RemoveFolder(int index) => _data.RemoveAt(index);

        /// <summary>
        /// Total number of elements in the data.
        /// </summary>
        /// <returns>Total number of elements in the data, of type int</returns>
        public int GetSize() => _data.Count;

        protected override string GetToStringValues()
        {
            _toString = "DATA";
            for (_index = 0; _index < GetSize(); _index++) _toString += $"\n name = {_data[_index].name}, path = {_data[_index].path}";
            return _toString;
        }
    }
}