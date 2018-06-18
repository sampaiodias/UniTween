# UniTween

UniTween is a Tween framework for Unity that enables programmers and artists to create *almost* any kind of Tween in a workflow that is easy to learn, fun to use, and with great maintainability.

* **Zero-coding:** Make simple or complex tweens only by working on the Unity Edity.
* **Productivity:** In our tests it is up to 10 times faster to create a tween using UniTween than coding it.
* **Reusability:** Tweens created with UniTween can be used multiple times in one or more sequences (ScriptableObject).

To see how it works, check the video demonstration clicking on the image below.
[![VideoDemo](https://i.imgur.com/o5mHYgK.png)](https://g.redditmedia.com/9AE7zanHgRsB0xVy-1Dnh-9ooWvVQpSXYLhBk0luxOk.gif?fm=mp4&mp4-fragmented=false&s=14bf19bb2ad19f0c9c380e0abc10aeac)

## Requirements
* [Unity](https://unity3d.com/get-unity/download/archive) version 5.6.3 or higher (2017.1 or higher is recommended). Older versions may work but were not tested.
* [DOTween](https://assetstore.unity.com/packages/tools/animation/dotween-hotween-v2-27676) (Free or Pro) version 1.1.640 or higher
* [Odin - Inspector and Serializer](https://assetstore.unity.com/packages/tools/utilities/odin-inspector-and-serializer-89041) version 1.0.6.1 or higher
* Optional ([Extensions](#extensions)): TextMesh Pro

## How To Install
* Import DOTween to your project (version 1.1.640 or higher)
* Import Odin - Inspector and Serializer to your project (version 1.0.6.8 or higher)
* Download the latest .unitypackage file [from the Releases folder](https://github.com/sampaiodias/UniTween/tree/master/Releases) and open it on your project.

## How To Use
1. Right-click a folder in your project and create a TweenData with the type that corresponds to the component you want to tween.
2. Edit the TweenData file to change what will happen to the component
3. Add a UniTween Sequence component to a GameObject
4. Click on the '+' button and select the operation you want (Append, Join, AppendInterval or AppendCallback).
5. Drag the TweenData you want to use
6. Drag the component/GameObject you want to manipulate
7. Go back to step 4 if you want to add more tweens to this Sequence.
8. To play the Sequence, change settings of the UniTween Sequence to play it on Start or Enable. Via script you can call it using the Play() or Play(string id) methods.
9. OPTIONAL: You can set the other options such as loops and time scale on the UniTween Sequence settings.
10. OPTIONAL: You can also manage all Sequences in your scene using the Sequence Explorer (Tools/UniTween/Sequence Explorer)

Check the [Examples folder](https://github.com/sampaiodias/UniTween/tree/master/Examples) to find more information about how to use UniTween.

For more information about specific tweens and how to use them, [please refer to DOTween's official documentation](http://dotween.demigiant.com/documentation.php).

## Extensions
Extensions are extra functionality that are available by using other plugins installed in your project. To use them, add the following "symbols" to the Scripting Define Symbols of your project (File/Build Settings/Player Settings.../Other Settings):
* TextMesh Pro: UNITWEEN_TEXTMESH

Example:
![Extension Symbol Example](https://i.imgur.com/oLwPm3k.png "Extension Symbol Example")

### Components available to tween (TweenData)
* AudioMixer
* AudioSource
* Camera
* CanvasGroup
* Graphic
* Image
* LayoutElement
* Light
* LineRenderer
* MeshRenderer (Material)
* Outline
* RectTransform
* Rigidbody
* Rigidbody2D
* ScrollRect
* Slider
* SpriteRenderer
* Text
* TrailRenderer
* Transform
* TextMeshPro ([Extension](#extensions))
* TextMeshProUGUI ([Extension](#extensions))

## Special Thanks
* [DEMIGIANT](http://demigiant.com/) for creating DOTween and releasing a free version
* [Sirenix](http://sirenix.net) for their amazing support
* [LabTIME](http://www.labtime.ufg.br/)
* [Paullo Cesar "PC"](https://github.com/paullocesarpc)
* [Allan Oliveira](https://github.com/allanolivei)

## Changelog

### Version 1.0.3.0
* Added support for TextMeshPro (works for DOTween free version)! To use it, check the [Extensions](#extensions) section.

### Version 1.0.2.3
* Added "Ignore Unity Time Scale" and "Update Time" options to UniTween Sequence

### Version 1.0.2.2
* Added a Random Variance option for AppendInterval to create intervals with random amounts.
* Added tooltips to help beginners understand the concepts of TweenData and Operations (Append, Join, etc.)
* TweenData files and UniTween Sequence components now have a link to their appropriate documentation on the web (to see it click on the blue book on the upper right corner of the inspector) 

### Version 1.0.2.1
* Fixed instability of Play and PlayBackwards when trying to play callbacks (UnityEvent)

### Version 1.0.2
* Added complete support for PlayBackwards, Rewind and Resume
* Improved GUI for UniTween Sequence
* Improved documentation for the public methods of UniTween Sequence
* The GetTween(UniTween uniTween) method of UniTween Sequence is now private.

### Version 1.0.1
* Added new TweenData types: Graphic, LayoutElement, ScrollRect, Slider, TrailRenderer
* Added TimeScale support for Sequences

## License

**MIT License**

Copyright 2018 Lucas Sampaio Dias

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
