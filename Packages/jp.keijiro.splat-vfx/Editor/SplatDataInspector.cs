using UnityEngine;
using UnityEditor;

namespace SplatVfx.Editor {

[CustomEditor(typeof(SplatData))]
public sealed class SplatDataInspector : UnityEditor.Editor
{
    public override void OnInspectorGUI()
    {
        var count = ((SplatData)target).SplatCount;
        EditorGUILayout.LabelField("Splat Count", $"{count:N0}");
    }
}

} // namespace SplatVfx.Editor
