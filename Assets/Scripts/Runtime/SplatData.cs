using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public sealed class SplatData : ScriptableObject
{
    #region Public properties

    public int SplatCount => PositionArray.Length;
    public GraphicsBuffer PositionBuffer => GetCachedBuffers().position;
    public GraphicsBuffer RotationBuffer => GetCachedBuffers().rotation;
    public GraphicsBuffer ScaleBuffer => GetCachedBuffers().scale;
    public GraphicsBuffer ColorBuffer => GetCachedBuffers().color;

    #endregion

    #region Serialized data

    [field:SerializeField, HideInInspector]
    public Vector3[] PositionArray { get; set; }

    [field:SerializeField, HideInInspector]
    public Vector4[] RotationArray { get; set; }

    [field:SerializeField, HideInInspector]
    public Vector3[] ScaleArray { get; set; }

    [field:SerializeField, HideInInspector]
    public Color[] ColorArray { get; set; }

    #endregion

    #region GPU resource management

    (GraphicsBuffer position, GraphicsBuffer rotation,
     GraphicsBuffer scale, GraphicsBuffer color) _cachedBuffers;

    static unsafe GraphicsBuffer NewBuffer<T>(int count) where T : unmanaged
      => new GraphicsBuffer(GraphicsBuffer.Target.Structured, count, sizeof(T));

    (GraphicsBuffer position, GraphicsBuffer rotation,
     GraphicsBuffer scale, GraphicsBuffer color) GetCachedBuffers()
    {
        if (_cachedBuffers.position == null)
        {
            _cachedBuffers.position = NewBuffer<Vector3>(SplatCount);
            _cachedBuffers.rotation = NewBuffer<Vector4>(SplatCount);
            _cachedBuffers.scale = NewBuffer<Vector3>(SplatCount);
            _cachedBuffers.color = NewBuffer<Color>(SplatCount);
            _cachedBuffers.position.SetData(PositionArray);
            _cachedBuffers.rotation.SetData(RotationArray);
            _cachedBuffers.scale.SetData(ScaleArray);
            _cachedBuffers.color.SetData(ColorArray);
        }
        return _cachedBuffers;
    }

    #endregion

    #region ScriptableObject implementation

    void OnDisable()
    {
        _cachedBuffers.position?.Release();
        _cachedBuffers.rotation?.Release();
        _cachedBuffers.scale?.Release();
        _cachedBuffers.color?.Release();
        _cachedBuffers = (null, null, null, null);
    }

    #endregion
}
