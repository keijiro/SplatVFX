# SplatVFX

![gif](https://github.com/keijiro/SplatVFX/assets/343936/19fa65e7-7db5-4151-84d1-70966a27d2d1)

*SplatVFX* is an experimental implementation of [3D Gaussian Splatting] with
Unity VFX Graph.

[3D Gaussian Splatting]: https://repo-sam.inria.fr/fungraph/3d-gaussian-splatting/

## FAQ

### Is it ready for use?

**No.** I made many compromisations to implement it on VFX Graph. I strongly
recommend trying other solutions like [this one].

[this one]: https://github.com/aras-p/UnityGaussianSplatting

## How to try it

- Download the test splat file ([bicycle.splat]).
- Put the file in the `URP/Assets` folder.
- Open `URP/Assets/Test.unity` and start the Play Mode.

[bicycle.splat]: https://huggingface.co/cakewalk/splat-data/resolve/main/bicycle.splat
