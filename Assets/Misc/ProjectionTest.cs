using UnityEngine;
using UnityEngine.VFX;

public sealed class ProjectionTest : MonoBehaviour
{
    [field:SerializeField]
    public Transform LinkedTo;

    void Update()
    {
        var axis1 = LinkedTo.TransformVector(Vector3.right);
        var axis2 = LinkedTo.TransformVector(Vector3.up);
        var axis3 = LinkedTo.TransformVector(Vector3.forward);

        var vfx = GetComponent<VisualEffect>();
        vfx.SetVector3("Axis1", axis1);
        vfx.SetVector3("Axis2", axis2);
        vfx.SetVector3("Axis3", axis3);
    }
}
