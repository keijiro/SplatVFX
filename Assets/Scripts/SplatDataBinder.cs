using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.VFX.Utility;

[AddComponentMenu("VFX/Property Binders/Splat Data Binder")]
[VFXBinder("Splat Data")]
class VFXSplatDataBinder : VFXBinderBase
{
    [SerializeField] SplatData _data = null;

    public string PositionBufferProperty
      { get => (string)_positionBufferProperty;
        set => _positionBufferProperty = value; }

    public string RotationBufferProperty
      { get => (string)_rotationBufferProperty;
        set => _rotationBufferProperty = value; }

    public string ScaleBufferProperty
      { get => (string)_scaleBufferProperty;
        set => _scaleBufferProperty = value; }

    public string ColorBufferProperty
      { get => (string)_colorBufferProperty;
        set => _colorBufferProperty = value; }

    [VFXPropertyBinding("UnityEngine.GraphicsBuffer"), SerializeField]
    ExposedProperty _positionBufferProperty = "PositionBuffer";

    [VFXPropertyBinding("UnityEngine.GraphicsBuffer"), SerializeField]
    ExposedProperty _rotationBufferProperty = "RotationBuffer";

    [VFXPropertyBinding("UnityEngine.GraphicsBuffer"), SerializeField]
    ExposedProperty _scaleBufferProperty = "ScaleBuffer";

    [VFXPropertyBinding("UnityEngine.GraphicsBuffer"), SerializeField]
    ExposedProperty _colorBufferProperty = "ColorBuffer";

    public override bool IsValid(VisualEffect component)
      => _data != null &&
         component.HasGraphicsBuffer(_positionBufferProperty) &&
         component.HasGraphicsBuffer(_rotationBufferProperty) &&
         component.HasGraphicsBuffer(_scaleBufferProperty) &&
         component.HasGraphicsBuffer(_colorBufferProperty);

    public override void UpdateBinding(VisualEffect component)
    {
        component.SetGraphicsBuffer(_positionBufferProperty, _data.PositionBuffer);
        component.SetGraphicsBuffer(_rotationBufferProperty, _data.RotationBuffer);
        component.SetGraphicsBuffer(_scaleBufferProperty, _data.ScaleBuffer);
        component.SetGraphicsBuffer(_colorBufferProperty, _data.ColorBuffer);
    }

    public override string ToString()
      => $"Splat Data : {_positionBufferProperty}, {_rotationBufferProperty}, "
       + $"{_scaleBufferProperty}, {_colorBufferProperty}";
}
