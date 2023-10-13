using UnityEngine;
using UnityEditor;
using UnityEditor.AssetImporters;
using System;
using System.IO;
using System.Runtime.InteropServices;

[ScriptedImporter(1, "splat")]
public sealed class SplatImporter : ScriptedImporter
{
    #region ScriptedImporter implementation

    public override void OnImportAsset(AssetImportContext context)
    {
        var gameObject = new GameObject();
        var data = ImportAsSplatData(context.assetPath);

        var setter = gameObject.AddComponent<SplatDataSetter>();
        setter.SplatData = data;

        context.AddObjectToAsset("prefab", gameObject);
        if (data != null) context.AddObjectToAsset("data", data);

        context.SetMainObject(gameObject);
    }

    #endregion

    #region Reader implementation

    static Vector4 Byte4ToVector4(byte x, byte y, byte z, byte w)
      => (new Vector4(x, y, z, w) - Vector4.one * 128) / 128.0f;

    SplatData ImportAsSplatData(string path)
    {
        var read = ReadArrays(path);
        var data = ScriptableObject.CreateInstance<SplatData>();
        data.PositionArray = read.position;
        data.RotationArray = read.rotation;
        data.ScaleArray = read.scale;
        data.ColorArray = read.color;
        data.name = Path.GetFileNameWithoutExtension(path);
        return data;
    }

    (Vector3[] position, Vector4[] rotation, Vector3[] scale, Color[] color)
        ReadArrays(string path)
    {
        var bytes = (Span<byte>)File.ReadAllBytes(path);
        var floats = MemoryMarshal.Cast<byte, float>(bytes);
        var count = bytes.Length / 32;

        var position = new Vector3[count];
        var rotation = new Vector4[count];
        var scale = new Vector3[count];
        var color = new Color[count];

        for (var i = 0; i < count; i++)
        {
            var bslice = bytes.Slice(i * 32);
            var fslice = floats.Slice(i * 8);

            position[i] = new Vector3(fslice[0], fslice[1], fslice[2]);
            scale[i] = new Vector3(fslice[3], fslice[4], fslice[5]);
            color[i] = new Color32(bslice[24], bslice[25], bslice[26], bslice[27]);
            rotation[i] = Byte4ToVector4(bslice[28], bslice[29], bslice[30], bslice[31]);

            if (i < 100) Debug.Log(scale[i]);
        }

        return (position, rotation, scale, color);
    }

    #endregion
}
