using System;
using System.Collections.Generic;
using System.Linq;
using Grigorii.Tatarinov.UnityCoordinator;
using ModestTree;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using UnityEngine.UIElements;
using TreeView = UnityEngine.UIElements.TreeView;

namespace Coordinator.Editor
{
    [Serializable]
    public class TestItem
    {
        public int value;

        public TestItem(int value)
        {
            this.value = value;
        }
    }

    public class CoordinatorTreeViewItem
    {
        public CoordinatorTreeViewItem(string coordinatorName)
        {
            CoordinatorName = coordinatorName;
        }

        public string CoordinatorName { get; set; }
    }
    
    public class CoordinatorBrowserEditor : EditorWindow
    {
        [SerializeField] private VisualTreeAsset coordinatorBrowserTree;

        private TreeView _treeView;
        
        [MenuItem("Tools/CoordinatorsBrowser")]
        public static void ShowEditor()
        {
            var window = GetWindow<CoordinatorBrowserEditor>();
            window.titleContent = new GUIContent("Coordinators browser");
        }

        private void CreateGUI()
        {
            coordinatorBrowserTree.CloneTree(rootVisualElement);
            
            SetupButton();
            BuildRoot();
        }

        private void SetupButton()
        {
            var button = rootVisualElement.Q<Button>();
            button.clicked += BuildRoot;
        }

        private void BuildRoot()
        {
            _treeView = rootVisualElement.Q<TreeView>();

            var children = new List<TreeViewItemData<CoordinatorTreeViewItem>>();
            var id = 0;
            
            CoordinatorTracker.ForEachCoordinator((coordinator) =>
            {
                id++;
                var coordinatorItem = new CoordinatorTreeViewItem(coordinator.GetType().Name);
                var coordinatorChildren = GetAllChildren(coordinator).ToList();
                children.Add(new TreeViewItemData<CoordinatorTreeViewItem>(id, coordinatorItem, coordinatorChildren));
            });
            
            _treeView.SetRootItems(children);
            _treeView.makeItem = () => new Label();
            _treeView.bindItem = (element, index) =>
                ((Label) element).text = _treeView.GetItemDataForIndex<CoordinatorTreeViewItem>(index).CoordinatorName;
        }

        private IEnumerable<TreeViewItemData<CoordinatorTreeViewItem>> GetAllChildren(ICoordinator coordinator)
        {
            foreach (var child in coordinator.Children)
            {
                var item = new CoordinatorTreeViewItem(child.GetType().Name);
                yield return new TreeViewItemData<CoordinatorTreeViewItem>(item.GetHashCode(), item, GetAllChildren(child).ToList());
            }
        }
    }
}