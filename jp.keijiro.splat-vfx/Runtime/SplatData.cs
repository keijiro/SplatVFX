using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace SplatVfx {

public sealed class SplatData : ScriptableObject
{
    #region Public properties

    public int SplatCount => PositionArray.Length;
    public GraphicsBuffer PositionBuffer => GetCachedBuffers().position;
    public GraphicsBuffer AxisBuffer => GetCachedBuffers().axis;
    public GraphicsBuffer ColorBuffer => GetCachedBuffers().color;

    #endregion

    #region Serialized data

    [field:SerializeField] public Vector3[] PositionArray { get; set; }
    [field:SerializeField] public Vector3[] AxisArray { get; set; }
    [field:SerializeField] public Color[] ColorArray { get; set; }

    #endregion

    #region Public methods

    public void ReleaseGpuResources()
    {
        _cachedBuffers.position?.Release();
        _cachedBuffers.axis?.Release();
        _cachedBuffers.color?.Release();
        _cachedBuffers = (null, null, null);
    }

    #endregion

    #region GPU resource management

    (GraphicsBuffer position, GraphicsBuffer axis, GraphicsBuffer color)
      _cachedBuffers;

    static unsafe GraphicsBuffer NewBuffer<T>(int count) where T : unmanaged
      => new GraphicsBuffer(GraphicsBuffer.Target.Structured, count, sizeof(T));

    (GraphicsBuffer position, GraphicsBuffer axis, GraphicsBuffer color)
      GetCachedBuffers()
    {
        if (_cachedBuffers.position == null)
        {
            _cachedBuffers.position = NewBuffer<Vector3>(SplatCount);
            _cachedBuffers.axis = NewBuffer<Vector3>(SplatCount * 3);
            _cachedBuffers.color = NewBuffer<Color>(SplatCount);
            _cachedBuffers.position.SetData(PositionArray);
            _cachedBuffers.axis.SetData(AxisArray);
            _cachedBuffers.color.SetData(ColorArray);
        }
        return _cachedBuffers;
    }

    #endregion

    #region ScriptableObject implementation

    void OnDisable() => ReleaseGpuResources();

    #endregion
}

} // namespace SplatVfx
