using Unity.Burst;
using Unity.Mathematics;
using UnityEngine;
using UnityEditor;
using UnityEditor.AssetImporters;
using System;
using System.IO;
using System.Runtime.InteropServices;

[ScriptedImporter(1, "splat"), BurstCompile]
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

    SplatData ImportAsSplatData(string path)
    {
        var data = ScriptableObject.CreateInstance<SplatData>();
        data.name = Path.GetFileNameWithoutExtension(path);

        var arrays = LoadDataArrays(path);
        data.PositionArray = arrays.position;
        data.RotationArray = arrays.rotation;
        data.ScaleArray = arrays.scale;
        data.ColorArray = arrays.color;

        return data;
    }

#pragma warning disable CS0649

    struct ReadData
    {
        public float px, py, pz;
        public float sx, sy, sz;
        public byte r, g, b, a;
        public byte rx, ry, rz, rw;
    }

#pragma warning restore CS0649

    (Vector3[] position, Vector4[] rotation, Vector3[] scale, Color[] color)
        LoadDataArrays(string path)
    {
        var bytes = (Span<byte>)File.ReadAllBytes(path);
        var count = bytes.Length / 32;

        var source = MemoryMarshal.Cast<byte, ReadData>(bytes);

        var position = new Vector3[count];
        var rotation = new Vector4[count];
        var scale = new Vector3[count];
        var color = new Color[count];

        for (var i = 0; i < count; i++)
        {
            ParseReadData(source[i],
                          out position[i],
                          out rotation[i],
                          out scale[i],
                          out color[i]);
        }

        return (position, rotation, scale, color);
    }

    [BurstCompile]
    void ParseReadData(in ReadData src,
                       out Vector3 position,
                       out Vector4 rotation,
                       out Vector3 scale,
                       out Color color)
    {
        position = math.float3(src.px, src.py, src.pz);
        rotation = (math.float4(src.rx, src.ry, src.rz, src.rw) - 128) / 128;
        scale = math.float3(src.sx, src.sy, src.sz);
        color = (Vector4)math.float4(src.r, src.g, src.b, src.a) / 255;
    }

    #endregion
}
