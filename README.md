# SplatVFX

![gif](https://github.com/keijiro/SplatVFX/assets/343936/19fa65e7-7db5-4151-84d1-70966a27d2d1)
![gif](https://github.com/keijiro/SplatVFX/assets/343936/2267b740-0b91-41e0-9036-5b07adae90e0)

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

## How to try your own `.splat` file in the editor with the visual effect.

- Download the github repo.
- Follow the instructions above to create your own `.splat` file.
- Rename the `.splat` file to `bicycle.splat`.
- Drag & drop it in the VFX/Asset folder (where you will also see the `bicycle.splat.meta` file.
- Open the folder "VFX" via the Unity Hub to open the right project.
- Open the "VFX" scene (present in the 'Assets' folder).
- Enter play mode.
- Optional: Depending on your splat, you might want to try this for better results: In the 'VFX' scene, unroll the "Camera Pivot" object hierarchy and move the "Direction" game object around to modify the camera position. You can also edit the 'Camera' object's FOV via the inspector (Try to keep it between 45 and 90 degrees for best results). 

## How to increase the capacity.

The default VFX Graph (`Splat.vfx`) supports up to 8 million points. You must
increase the capacity when your `.splat` file has more points. Duplicate
`Splat.vfx` into your project and edit it to change the capacity value in the
Initialize Particle context.

![capacity](https://github.com/keijiro/SplatVFX/assets/343936/f8fe53b1-9173-4db7-b8b8-fbc0c00949d5)

You can check how many points are in a `.splat` file on Inspector.

![count](https://github.com/keijiro/SplatVFX/assets/343936/d6793722-d088-4904-b297-71f802fe617c)

# Limitations

- Typically, `.splat` files are trained with the reference rasterizer running
  on the sRGB color space. It causes artifacts when using the Linear Lighting
  Mode in Unity. You can remedy it by grading in post-processing, but it's
  impossible to get perfect results with manual tweaks.

- The Gaussian projection algorithm used in the VFX Graph is far from perfect.
  It causes many artifacts, including sudden pops with camera motion.
