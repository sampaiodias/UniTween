# UniTween

UniTween is a Tween framework that enables programmers and artists to create *almost* any kind of Tween in a workflow that is easy to learn, fun to use, and with great maintainability.

* **Zero-coding:** Make simple or complex tweens only by working on the Unity Edity.
* **Productivity:** In our tests it is around 10 times faster to create a tween using UniTween than coding it.
* **Reusability:** Tweens created with UniTween can be used multiple times in one or more sequences (ScriptableObject).
* **Price:** Unlike DOTween Pro (paid license per seat), UniTween is free and can do much more without coding!

## How To Install
* Import DOTween to your project (version 1.1.640 or higher)
* Import Odin - Inspector and Serializer to your project (version 1.0.6.1 or higher)
* Download the latest .unitypackage file [from the Packages folder](https://github.com/sampaiodias/UniTween/tree/master/Unity%20Packages) and open it on your project.

## How To Use
1. Right-click a folder in your project and create a TweenData with the type that corresponds to the component you want to tween.
2. Edit the TweenData file to change what will happen to the component
3. Add a UniTween Sequence component to a GameObject
4. Click on the '+' button and select the operation you want (Append, Join, AppendInterval or AppendCallback).
5. Drag the TweenData you want to use
6. Drag the component/GameObject you want to manipulate
7. Go back to step 4 if you want to add more tweens to this Sequence.
8. To play the Sequence, call it using the Play() or Play(string id) methods. With an ID you don't need a direct reference to the object that contains the UniTween Sequence component (don't forget to set the ID on the UniTween Sequence component).
9. OPTIONAL: You can set the other options such as loops and automatic play on the UniTween Sequence aswell.

*A video tutorial is also coming soon...*

### Components available to Tween (TweenData)
* AudioMixer
* AudioSource
* Camera
* CanvasGroup
* Image
* Light
* LineRenderer
* Material
* Outline
* RectTween
* Rigidbody
* Rigidbody 2D
* SpriteRenderer
* Text
* Transform

## Special Thanks
* [Sirenix](http://sirenix.net/odininspector/) for their amazing support for Odin
* [Paullo Cesar "PC"](https://github.com/paullocesarpc)
* [Allan Oliveira](https://github.com/allanolivei)

## License

Copyright 2018 Lucas Sampaio Dias

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
