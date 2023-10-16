using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.VFX.Utility;

namespace SplatVfx {

[AddComponentMenu("VFX/Property Binders/Splat Data Binder")]
[VFXBinder("Splat Data")]
public sealed class VFXSplatDataBinder : VFXBinderBase
{
    public SplatData SplatData = null;

    public string PositionBufferProperty
      { get => (string)_positionBufferProperty;
        set => _positionBufferProperty = value; }

    public string AxisBufferProperty
      { get => (string)_axisBufferProperty;
        set => _axisBufferProperty = value; }

    public string ColorBufferProperty
      { get => (string)_colorBufferProperty;
        set => _colorBufferProperty = value; }

    [VFXPropertyBinding("UnityEngine.GraphicsBuffer"), SerializeField]
    ExposedProperty _positionBufferProperty = "PositionBuffer";

    [VFXPropertyBinding("UnityEngine.GraphicsBuffer"), SerializeField]
    ExposedProperty _axisBufferProperty = "AxisBuffer";

    [VFXPropertyBinding("UnityEngine.GraphicsBuffer"), SerializeField]
    ExposedProperty _colorBufferProperty = "ColorBuffer";

    public override bool IsValid(VisualEffect component)
      => SplatData != null &&
         component.HasGraphicsBuffer(_positionBufferProperty) &&
         component.HasGraphicsBuffer(_axisBufferProperty) &&
         component.HasGraphicsBuffer(_colorBufferProperty);

    public override void UpdateBinding(VisualEffect component)
    {
        component.SetGraphicsBuffer(_positionBufferProperty, SplatData.PositionBuffer);
        component.SetGraphicsBuffer(_axisBufferProperty, SplatData.AxisBuffer);
        component.SetGraphicsBuffer(_colorBufferProperty, SplatData.ColorBuffer);
    }

    public override string ToString()
      => $"Splat Data : {_positionBufferProperty}, {_axisBufferProperty}, "
       + $"{_colorBufferProperty}";
}

} // namespace SplatVfx
