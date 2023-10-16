using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.VFX.Utility;

[AddComponentMenu("VFX/Property Binders/Projection Test Binder")]
[VFXBinder("Projection Test")]
public sealed class VFXProjectionTestBinder : VFXBinderBase
{
    [field:SerializeField]
    public Transform LinkedTo;

    public override bool IsValid(VisualEffect component)
      => LinkedTo != null &&
         component.HasVector3("Axis1") &&
         component.HasVector3("Axis2") &&
         component.HasVector3("Axis3");

    public override void UpdateBinding(VisualEffect component)
    {
        var axis1 = LinkedTo.TransformVector(Vector3.right);
        var axis2 = LinkedTo.TransformVector(Vector3.up);
        var axis3 = LinkedTo.TransformVector(Vector3.forward);
        component.SetVector3("Axis1", axis1);
        component.SetVector3("Axis2", axis2);
        component.SetVector3("Axis3", axis3);
    }

    public override string ToString()
      => $"Projection Test : Axis1, Axis2, Axis3";
}
