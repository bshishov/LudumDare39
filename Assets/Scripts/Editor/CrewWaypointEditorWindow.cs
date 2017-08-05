using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Editor
{
    public class CrewWaypointEditorWindow : EditorWindow
    {
        private bool _bindTwoWay = true;
        private List<CrewWaypoint> _selectedWaypoints = new List<CrewWaypoint>();

        void Init()
        {
            titleContent = new GUIContent("CrewWaypoint editor");
            Selection.selectionChanged += SelectionChanged;
        }

        private void SelectionChanged()
        {
            _selectedWaypoints = Selection.gameObjects.Select(g => g.GetComponent<CrewWaypoint>()).Where(w => w != null).ToList();
        }

        [MenuItem("Window/Waypoint Editor")]
        public static void ShowWindow()
        {
            var window = EditorWindow.GetWindow<CrewWaypointEditorWindow>();
            window.Init();
        }

        void OnGUI()
        {
            GUILayout.Label("Connection binding", EditorStyles.boldLabel);
            _bindTwoWay = EditorGUILayout.Toggle("Two way binding", _bindTwoWay);

            if (GUILayout.Button("Bind"))
            {
                if (_selectedWaypoints.Count >= 2)
                {
                    for (var i = 1; i < _selectedWaypoints.Count; i++)
                    {
                        var a = _selectedWaypoints[i - 1];
                        var b = _selectedWaypoints[i];
                        BindPair(a, b);
                    }
                }
            }

            if (GUILayout.Button("Unbind"))
            {
                if (_selectedWaypoints.Count >= 2)
                {
                    for (var i = 0; i < _selectedWaypoints.Count - 1; i++)
                    {
                        var a = _selectedWaypoints[i];

                        for (var j = i + 1; j < _selectedWaypoints.Count; j++)
                        {
                            var b = _selectedWaypoints[j];
                            UnBindPair(a, b);
                        }
                    }
                }
                else if(_selectedWaypoints.Count == 1)
                {
                    _selectedWaypoints[0].Connections.Clear();
                }
            }

            GUILayout.Label(string.Format("Selected {0} components", _selectedWaypoints.Count()), EditorStyles.boldLabel);
            foreach (var waypoint in _selectedWaypoints)
            {
                GUILayout.Label(waypoint.name);
            }
        }

        void BindPair(CrewWaypoint a, CrewWaypoint b)
        {
            EditorUtility.SetDirty(a.gameObject);
            if (!a.Connections.Contains(b))
                a.Connections.Add(b);

            var wa = new SerializedObject(a);
            wa.ApplyModifiedProperties();

            if (_bindTwoWay)
            {
                EditorUtility.SetDirty(b.gameObject);
                if (!b.Connections.Contains(a))
                    b.Connections.Add(a);

                var wb = new SerializedObject(a);
                wb.ApplyModifiedProperties();
            }

            var prop = wa.FindProperty("Connections");
            prop.InsertArrayElementAtIndex(0);
            prop.GetArrayElementAtIndex(0);

        }

        void UnBindPair(CrewWaypoint a, CrewWaypoint b)
        {
            if (a.Connections.Contains(b))
                a.Connections.Remove(b);
            var wa = new SerializedObject(a);
            wa.ApplyModifiedProperties();

            if (_bindTwoWay)
            {
                if (b.Connections.Contains(a))
                    b.Connections.Remove(a);
                var wb = new SerializedObject(a);
                wb.ApplyModifiedProperties();
            }
        }
    }
}
