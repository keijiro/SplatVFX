# SplatVFX

![gif](https://github.com/keijiro/SplatVFX/assets/343936/19fa65e7-7db5-4151-84d1-70966a27d2d1)

*SplatVFX* is an experimental implementation of [3D Gaussian Splatting] with
Unity VFX Graph.

[3D Gaussian Splatting]: https://repo-sam.inria.fr/fungraph/3d-gaussian-splatting/

## FAQ

### Is it ready for use?

**No.** I made many compromises to implement it with VFX Graph. I recommend
trying other solutions like [UnityGaussianSplatting].

[UnityGaussianSplatting]: https://github.com/aras-p/UnityGaussianSplatting

## How to try the samples.

- Download the sample `.splat` file ([bicycle.splat]) and put it in the
  `URP/Assets` directory.
- Open `URP/Assets/Test.unity` and start Play Mode.

[bicycle.splat]: https://huggingface.co/cakewalk/splat-data/resolve/main/bicycle.splat

## How to create a `.splat` file.

`.splat` is an ad-hoc file format used in antimatter15's
[WebGL Gaussian Splat Viewer]. You can convert a `.ply` file into `.splat` by
dragging and dropping it into a viewer window.

[WebGL Gaussian Splat Viewer]: https://github.com/antimatter15/splat


