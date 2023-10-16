using UnityEngine;

namespace SplatVfx {

public sealed class SplatDataSetter : MonoBehaviour
{
    [field:SerializeField]
    public SplatData SplatData { get; set; }
}

} // namespace SplatVfx
