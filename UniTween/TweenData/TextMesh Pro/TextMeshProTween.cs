#if UNITWEEN_TEXTMESH
namespace UniTween.Data
{
    using DG.Tweening;
    using Sirenix.OdinInspector;
    using System.Collections.Generic;
    using TMPro;
    using UniTween.Core;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Tween Data/TextMesh Pro (3D Text)")]
    public class TextMeshProTween : TweenData
    {
        [Space(15)]
        public TextMeshCommand command;

        [ShowIf("IsCustomPropertyTween")]
        [Tooltip("The shader's property used by the material of the TextMesh Pro component. Be sure to check if the current shader supports the property you want to tween (e.g.: mobile shaders have only a few properties). \n\nProperty Examples: _FaceColor, _Bevel")]
        public string property;
        [ShowIf("ShowColor")]
        public Color color;
        [ShowIf("ShowTo")]
        public float to;
        [HideIf("HideCharacters")]
        public int characters;
        [ShowIf("IsTextTween")]
        public string newText;
        [ShowIf("IsTextTween")]
        public bool richTextEnabled = true;
        [ShowIf("IsTilingTween")]
        public Vector2 tiling;
        [ShowIf("IsVectorTween")]
        public Vector4 vector;

        public override Tween GetTween(UniTweenObject.UniTweenTarget uniTweenTarget)
        {
            List<TextMeshPro> texts = (List<TextMeshPro>)GetComponent(uniTweenTarget);
            Sequence tweens = DOTween.Sequence();
            foreach (var t in texts)
            {
                tweens.Join(GetTween(t));
            }
            return tweens;
        }

        public Tween GetTween(TextMeshPro text)
        {
            switch (command)
            {
                case TextMeshCommand.Scale:
                    return DOTween.To(() => text.fontSize, x => text.fontSize = x, text.fontSize * to, duration);
                case TextMeshCommand.Color:
                    return DOTween.To(() => text.color, x => text.color = x, color, duration);
                case TextMeshCommand.FaceColor:
                    return text.fontMaterial.DOColor(color, "_FaceColor", duration);
                case TextMeshCommand.FaceFade:
                    return DOTween.To(() => text.faceColor, x => text.faceColor = x, new Vector4(text.faceColor.r, text.faceColor.g, text.faceColor.b, to), duration);
                case TextMeshCommand.Fade:
                    return DOTween.To(() => text.alpha, x => text.alpha = x, to, duration);
                case TextMeshCommand.FontSize:
                    return DOTween.To(() => text.fontSize, x => text.fontSize = x, to, duration);
                case TextMeshCommand.GlowColor:
                    return text.fontMaterial.DOColor(color, "_GlowColor", duration);
                case TextMeshCommand.MaxVisibleCharacters:
                    return DOTween.To(() => text.maxVisibleCharacters, x => text.maxVisibleCharacters = x, characters, duration);
                case TextMeshCommand.OutlineColor:
                    return text.fontMaterial.DOColor(color, "_OutlineColor", duration);
                case TextMeshCommand.Text:
                    Sequence sqText = DOTween.Sequence();
                    if (customEase)
                        sqText.SetEase(curve);
                    else
                        sqText.SetEase(ease);
                    sqText.AppendCallback(() => text.richText = richTextEnabled);
                    sqText.Append(DOTween.To(() => text.text, x => text.text = x, newText, duration));
                    return sqText;
                case TextMeshCommand.ColorProperty:
                    return text.fontMaterial.DOColor(color, property, duration);
                case TextMeshCommand.FloatProperty:
                    return text.fontMaterial.DOFloat(to, property, duration);
                case TextMeshCommand.TilingProperty:
                    return text.fontMaterial.DOTiling(tiling, property, duration);
                case TextMeshCommand.VectorProperty:
                    return text.fontMaterial.DOVector(vector, property, duration);
            }
            return null;
        }

        private bool ShowTo()
        {
            return command == TextMeshCommand.FaceFade
                || command == TextMeshCommand.Fade
                || command == TextMeshCommand.FloatProperty
                || command == TextMeshCommand.FontSize
                || command == TextMeshCommand.Scale;
        }

        private bool ShowColor()
        {
            return command.ToString().Contains("Color");
        }

        private bool HideCharacters()
        {
            return command != TextMeshCommand.MaxVisibleCharacters;
        }

        private bool IsTextTween()
        {
            return command == TextMeshCommand.Text;
        }

        private bool IsCustomPropertyTween()
        {
            return command.ToString().Contains("Property");
        }

        private bool IsTilingTween()
        {
            return command == TextMeshCommand.TilingProperty;
        }

        private bool IsVectorTween()
        {
            return command == TextMeshCommand.VectorProperty;
        }

        public enum TextMeshCommand
        {
            Scale,
            Color,
            FaceColor,
            FaceFade,
            Fade,
            FontSize,
            GlowColor,
            MaxVisibleCharacters,
            OutlineColor,
            Text,
            ColorProperty = 1000,
            FloatProperty = 1001,
            TilingProperty = 1002,
            VectorProperty = 1003
        }
    }
}
#endif