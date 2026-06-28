using UnityEditor;

namespace Assets.Scripts.Road
{
    [CustomEditor(typeof(MyLoftRoadBehaviour))]
    [CanEditMultipleObjects]
    class SplineWidthEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();

            base.OnInspectorGUI();

            if (EditorGUI.EndChangeCheck())
            {
                EditorApplication.delayCall += () =>
                {
                    foreach (var target in targets)
                        ((MyLoftRoadBehaviour)target).LoftAllRoads();
                };
            }
        }
    }
}