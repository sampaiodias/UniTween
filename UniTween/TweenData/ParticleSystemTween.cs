using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Tween Data/Experimental (W.I.P.)/Particle System")]
public class ParticleSystemTween : TweenData
{
    [Space(15)]
    public ParticleSystemCommand command;
    [ValidateInput("ValidateMode", "Tweening with this mode is currently not supported! Values will be assigned immediately instead (as if the duration was 0 seconds).", InfoMessageType.Info)]
    [HideIf("IsColor")]
    public ParticleSystemCurveMode mode;
    [ValidateInput("ValidateColorMode", "Tweening with this mode is currently not supported! Values will be assigned immediately instead (as if the duration was 0 seconds).", InfoMessageType.Info)]
    [ShowIf("IsColor")]
    public ParticleSystemGradientMode colorMode;

    [ShowIf("IsConstant")]
    public float to;
    [ShowIf("IsTwoConstants")]
    public float min;
    [ShowIf("IsTwoConstants")]
    public float max = 5;
    [ShowIf("IsCurveOrTwoCurves")]
    public float multiplier = 1;
    [ShowIf("IsCurve")]
    public AnimationCurve animationCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    [ShowIf("IsTwoCurves")]
    public AnimationCurve curveMin = AnimationCurve.EaseInOut(0, 0, 1, 1);
    [ShowIf("IsTwoCurves")]
    public AnimationCurve curveMax = AnimationCurve.EaseInOut(0, 0, 1, 1);
    [ShowIf("ShowColor")]
    public Color color = Color.white;
    [ShowIf("ShowColorMinMax")]
    public Color colorMin = Color.white;
    [ShowIf("ShowColorMinMax")]
    public Color colorMax = Color.black;
    [ShowIf("ShowGradient")]
    public Gradient gradient = new Gradient();
    [ShowIf("ShowGradientMinMax")]
    public Gradient gradientMin = new Gradient();
    [ShowIf("ShowGradientMinMax")]
    public Gradient gradientMax = new Gradient();

    public override Tween GetTween(UniTween.UniTweenTarget uniTweenTarget)
    {
        ParticleSystem ps = (ParticleSystem)GetComponent(uniTweenTarget);
        var main = ps.main;

        switch (command)
        {
            case ParticleSystemCommand.StartLifetime:
                switch (mode)
                {
                    case ParticleSystemCurveMode.Constant:
                        return DOTween.To(() => main.startLifetime.constant, x => main.startLifetime = x, to, duration);
                    case ParticleSystemCurveMode.TwoConstants:
                        Sequence sqLifetimeTwoConstants = DOTween.Sequence();
                        sqLifetimeTwoConstants.AppendCallback(() => main.startLifetime = new ParticleSystem.MinMaxCurve(min, max));
                        return sqLifetimeTwoConstants;
                    case ParticleSystemCurveMode.Curve:
                        Sequence sqLifetimeCurve = DOTween.Sequence();
                        sqLifetimeCurve.AppendCallback(() => main.startLifetime = new ParticleSystem.MinMaxCurve(multiplier, animationCurve));
                        return sqLifetimeCurve;
                    case ParticleSystemCurveMode.TwoCurves:
                        Sequence sqLifetimeTwoCurves = DOTween.Sequence();
                        sqLifetimeTwoCurves.AppendCallback(() => main.startLifetime = new ParticleSystem.MinMaxCurve(multiplier, curveMin, curveMax));
                        return sqLifetimeTwoCurves;
                }
                break;
            case ParticleSystemCommand.StartSpeed:
                switch (mode)
                {
                    case ParticleSystemCurveMode.Constant:
                        return DOTween.To(() => main.startSpeed.constant, x => main.startSpeed = x, to, duration);
                    case ParticleSystemCurveMode.TwoConstants:
                        Sequence sqSpeedTwoConstants = DOTween.Sequence();
                        sqSpeedTwoConstants.AppendCallback(() => main.startSpeed = new ParticleSystem.MinMaxCurve(min, max));
                        return sqSpeedTwoConstants;
                    case ParticleSystemCurveMode.Curve:
                        Sequence sqSpeedCurve = DOTween.Sequence();
                        sqSpeedCurve.AppendCallback(() => main.startSpeed = new ParticleSystem.MinMaxCurve(multiplier, animationCurve));
                        return sqSpeedCurve;
                    case ParticleSystemCurveMode.TwoCurves:
                        Sequence sqSpeedTwoCurves = DOTween.Sequence();
                        sqSpeedTwoCurves.AppendCallback(() => main.startSpeed = new ParticleSystem.MinMaxCurve(multiplier, curveMin, curveMax));
                        return sqSpeedTwoCurves;
                }
                break;
            case ParticleSystemCommand.StartSize:
                switch (mode)
                {
                    case ParticleSystemCurveMode.Constant:
                        Sequence sqSize = DOTween.Sequence();
                        sqSize.AppendCallback(() => main.startSize3D = false);
                        sqSize.Append(DOTween.To(() => main.startSize.constant, x => main.startSize = x, to, duration));
                        return sqSize;
                    case ParticleSystemCurveMode.TwoConstants:
                        Sequence sqSizeTwoConstants = DOTween.Sequence();
                        sqSizeTwoConstants.AppendCallback(() => main.startSize = new ParticleSystem.MinMaxCurve(min, max));
                        return sqSizeTwoConstants;
                    case ParticleSystemCurveMode.Curve:
                        Sequence sqSizeCurve = DOTween.Sequence();
                        sqSizeCurve.AppendCallback(() => main.startSize = new ParticleSystem.MinMaxCurve(multiplier, animationCurve));
                        return sqSizeCurve;
                    case ParticleSystemCurveMode.TwoCurves:
                        Sequence sqSizeTwoCurves = DOTween.Sequence();
                        sqSizeTwoCurves.AppendCallback(() => main.startSize = new ParticleSystem.MinMaxCurve(multiplier, curveMin, curveMax));
                        return sqSizeTwoCurves;
                }
                break;
            case ParticleSystemCommand.StartSizeX:
                switch (mode)
                {
                    case ParticleSystemCurveMode.Constant:
                        Sequence sqSizeX = DOTween.Sequence();
                        sqSizeX.AppendCallback(() => main.startSize3D = true);
                        sqSizeX.Append(DOTween.To(() => main.startSizeX.constant, x => main.startSizeX = x, to, duration));
                        return sqSizeX;
                    case ParticleSystemCurveMode.TwoConstants:
                        Sequence sqSizeXTwoConstants = DOTween.Sequence();
                        sqSizeXTwoConstants.AppendCallback(() => main.startSizeX = new ParticleSystem.MinMaxCurve(min, max));
                        return sqSizeXTwoConstants;
                    case ParticleSystemCurveMode.Curve:
                        Sequence sqSizeXCurve = DOTween.Sequence();
                        sqSizeXCurve.AppendCallback(() => main.startSizeX = new ParticleSystem.MinMaxCurve(multiplier, animationCurve));
                        return sqSizeXCurve;
                    case ParticleSystemCurveMode.TwoCurves:
                        Sequence sqSizeXTwoCurves = DOTween.Sequence();
                        sqSizeXTwoCurves.AppendCallback(() => main.startSizeX = new ParticleSystem.MinMaxCurve(multiplier, curveMin, curveMax));
                        return sqSizeXTwoCurves;
                }
                break;
            case ParticleSystemCommand.StartSizeY:
                switch (mode)
                {
                    case ParticleSystemCurveMode.Constant:
                        Sequence sqSizeY = DOTween.Sequence();
                        sqSizeY.AppendCallback(() => main.startSize3D = true);
                        sqSizeY.Append(DOTween.To(() => main.startSizeY.constant, x => main.startSizeY = x, to, duration));
                        return sqSizeY;
                    case ParticleSystemCurveMode.TwoConstants:
                        Sequence sqSizeYTwoConstants = DOTween.Sequence();
                        sqSizeYTwoConstants.AppendCallback(() => main.startSizeY = new ParticleSystem.MinMaxCurve(min, max));
                        return sqSizeYTwoConstants;
                    case ParticleSystemCurveMode.Curve:
                        Sequence sqSizeYCurve = DOTween.Sequence();
                        sqSizeYCurve.AppendCallback(() => main.startSizeY = new ParticleSystem.MinMaxCurve(multiplier, animationCurve));
                        return sqSizeYCurve;
                    case ParticleSystemCurveMode.TwoCurves:
                        Sequence sqSizeYTwoCurves = DOTween.Sequence();
                        sqSizeYTwoCurves.AppendCallback(() => main.startSizeY = new ParticleSystem.MinMaxCurve(multiplier, curveMin, curveMax));
                        return sqSizeYTwoCurves;
                }
                break;
            case ParticleSystemCommand.StartSizeZ:
                switch (mode)
                {
                    case ParticleSystemCurveMode.Constant:
                        Sequence sqSizeZ = DOTween.Sequence();
                        sqSizeZ.AppendCallback(() => main.startSize3D = true);
                        sqSizeZ.Append(DOTween.To(() => main.startSizeZ.constant, x => main.startSizeZ = x, to, duration));
                        return sqSizeZ;
                    case ParticleSystemCurveMode.TwoConstants:
                        Sequence sqSizeZTwoConstants = DOTween.Sequence();
                        sqSizeZTwoConstants.AppendCallback(() => main.startSizeZ = new ParticleSystem.MinMaxCurve(min, max));
                        return sqSizeZTwoConstants;
                    case ParticleSystemCurveMode.Curve:
                        Sequence sqSizeZCurve = DOTween.Sequence();
                        sqSizeZCurve.AppendCallback(() => main.startSizeZ = new ParticleSystem.MinMaxCurve(multiplier, animationCurve));
                        return sqSizeZCurve;
                    case ParticleSystemCurveMode.TwoCurves:
                        Sequence sqSizeZTwoCurves = DOTween.Sequence();
                        sqSizeZTwoCurves.AppendCallback(() => main.startSizeZ = new ParticleSystem.MinMaxCurve(multiplier, curveMin, curveMax));
                        return sqSizeZTwoCurves;
                }
                break;
            case ParticleSystemCommand.StartRotation:
                switch (mode)
                {
                    case ParticleSystemCurveMode.Constant:
                        Sequence sqRotation = DOTween.Sequence();
                        sqRotation.AppendCallback(() => main.startRotation3D = false);
                        sqRotation.Append(DOTween.To(() => main.startRotation.constant, x => main.startRotation = x, Mathf.Deg2Rad * to, duration));
                        return sqRotation;
                    case ParticleSystemCurveMode.TwoConstants:
                        Sequence sqRotationTwoConstants = DOTween.Sequence();
                        sqRotationTwoConstants.AppendCallback(() => main.startRotation = new ParticleSystem.MinMaxCurve(min, max));
                        return sqRotationTwoConstants;
                    case ParticleSystemCurveMode.Curve:
                        Sequence sqRotationCurve = DOTween.Sequence();
                        sqRotationCurve.AppendCallback(() => main.startRotation = new ParticleSystem.MinMaxCurve(multiplier, animationCurve));
                        return sqRotationCurve;
                    case ParticleSystemCurveMode.TwoCurves:
                        Sequence sqRotationTwoCurves = DOTween.Sequence();
                        sqRotationTwoCurves.AppendCallback(() => main.startRotation = new ParticleSystem.MinMaxCurve(multiplier, curveMin, curveMax));
                        return sqRotationTwoCurves;
                }
                break;
            case ParticleSystemCommand.StartRotationX:
                switch (mode)
                {
                    case ParticleSystemCurveMode.Constant:
                        Sequence sqRotationX = DOTween.Sequence();
                        sqRotationX.AppendCallback(() => main.startRotation3D = true);
                        sqRotationX.Append(DOTween.To(() => main.startRotationX.constant, x => main.startRotationX = x, Mathf.Deg2Rad * to, duration));
                        return sqRotationX;
                    case ParticleSystemCurveMode.TwoConstants:
                        Sequence sqRotationXTwoConstants = DOTween.Sequence();
                        sqRotationXTwoConstants.AppendCallback(() => main.startRotationX = new ParticleSystem.MinMaxCurve(min, max));
                        return sqRotationXTwoConstants;
                    case ParticleSystemCurveMode.Curve:
                        Sequence sqRotationXCurve = DOTween.Sequence();
                        sqRotationXCurve.AppendCallback(() => main.startRotationX = new ParticleSystem.MinMaxCurve(multiplier, animationCurve));
                        return sqRotationXCurve;
                    case ParticleSystemCurveMode.TwoCurves:
                        Sequence sqRotationXTwoCurves = DOTween.Sequence();
                        sqRotationXTwoCurves.AppendCallback(() => main.startRotationX = new ParticleSystem.MinMaxCurve(multiplier, curveMin, curveMax));
                        return sqRotationXTwoCurves;
                }
                break;
            case ParticleSystemCommand.StartRotationY:
                switch (mode)
                {
                    case ParticleSystemCurveMode.Constant:
                        Sequence sqRotationY = DOTween.Sequence();
                        sqRotationY.AppendCallback(() => main.startRotation3D = true);
                        sqRotationY.Append(DOTween.To(() => main.startRotationY.constant, x => main.startRotationY = x, Mathf.Deg2Rad * to, duration));
                        return sqRotationY;
                    case ParticleSystemCurveMode.TwoConstants:
                        Sequence sqRotationYTwoConstants = DOTween.Sequence();
                        sqRotationYTwoConstants.AppendCallback(() => main.startRotationY = new ParticleSystem.MinMaxCurve(min, max));
                        sqRotationYTwoConstants.AppendCallback(() => main.startRotation3D = true);
                        return sqRotationYTwoConstants;
                    case ParticleSystemCurveMode.Curve:
                        Sequence sqRotationYCurve = DOTween.Sequence();
                        sqRotationYCurve.AppendCallback(() => main.startRotationY = new ParticleSystem.MinMaxCurve(multiplier, animationCurve));
                        sqRotationYCurve.AppendCallback(() => main.startRotation3D = true);
                        return sqRotationYCurve;
                    case ParticleSystemCurveMode.TwoCurves:
                        Sequence sqRotationYTwoCurves = DOTween.Sequence();
                        sqRotationYTwoCurves.AppendCallback(() => main.startRotationY = new ParticleSystem.MinMaxCurve(multiplier, curveMin, curveMax));
                        sqRotationYTwoCurves.AppendCallback(() => main.startRotation3D = true);
                        return sqRotationYTwoCurves;
                }
                break;
            case ParticleSystemCommand.StartRotationZ:
                switch (mode)
                {
                    case ParticleSystemCurveMode.Constant:
                        Sequence sqRotationZ = DOTween.Sequence();
                        sqRotationZ.AppendCallback(() => main.startRotation3D = true);
                        sqRotationZ.Append(DOTween.To(() => main.startRotationZ.constant, x => main.startRotationZ = x, Mathf.Deg2Rad * to, duration));
                        return sqRotationZ;
                    case ParticleSystemCurveMode.TwoConstants:
                        Sequence sqRotationZTwoConstants = DOTween.Sequence();
                        sqRotationZTwoConstants.AppendCallback(() => main.startRotationZ = new ParticleSystem.MinMaxCurve(min, max));
                        sqRotationZTwoConstants.AppendCallback(() => main.startRotation3D = true);
                        return sqRotationZTwoConstants;
                    case ParticleSystemCurveMode.Curve:
                        Sequence sqRotationZCurve = DOTween.Sequence();
                        sqRotationZCurve.AppendCallback(() => main.startRotationZ = new ParticleSystem.MinMaxCurve(multiplier, animationCurve));
                        sqRotationZCurve.AppendCallback(() => main.startRotation3D = true);
                        return sqRotationZCurve;
                    case ParticleSystemCurveMode.TwoCurves:
                        Sequence sqRotationZTwoCurves = DOTween.Sequence();
                        sqRotationZTwoCurves.AppendCallback(() => main.startRotationZ = new ParticleSystem.MinMaxCurve(multiplier, curveMin, curveMax));
                        sqRotationZTwoCurves.AppendCallback(() => main.startRotation3D = true);
                        return sqRotationZTwoCurves;
                }
                break;
            case ParticleSystemCommand.StartColor:
                switch (colorMode)
                {
                    case ParticleSystemGradientMode.Color:
                        return DOTween.To(() => main.startColor.color, x => main.startColor = x, color, duration);
                    case ParticleSystemGradientMode.Gradient:
                        Sequence sqColorGradient = DOTween.Sequence();
                        sqColorGradient.AppendCallback(() => main.startColor = new ParticleSystem.MinMaxGradient(gradient));
                        return sqColorGradient;
                    case ParticleSystemGradientMode.TwoColors:
                        Sequence sqColorTwoColors = DOTween.Sequence();
                        sqColorTwoColors.AppendCallback(() => main.startColor = new ParticleSystem.MinMaxGradient(colorMin, colorMax));
                        return sqColorTwoColors;
                    case ParticleSystemGradientMode.TwoGradients:
                        Sequence sqColorTwoGradients = DOTween.Sequence();
                        sqColorTwoGradients.AppendCallback(() => main.startColor = new ParticleSystem.MinMaxGradient(gradientMin, gradientMax));
                        return sqColorTwoGradients;
                    case ParticleSystemGradientMode.RandomColor:
                        var randomColorMode = main.startColor;
                        Sequence sqColorRandom = DOTween.Sequence();
                        sqColorRandom.AppendCallback(() => randomColorMode.mode = ParticleSystemGradientMode.RandomColor);
                        sqColorRandom.AppendCallback(() => main.startColor = new ParticleSystem.MinMaxGradient(gradient));
                        return sqColorRandom;
                }
                break;
            case ParticleSystemCommand.GravityModifier:
                switch (mode)
                {
                    case ParticleSystemCurveMode.Constant:
                        return DOTween.To(() => main.gravityModifier.constant, x => main.gravityModifier = x, to, duration);
                    case ParticleSystemCurveMode.TwoConstants:
                        Sequence sqGravityTwoConstants = DOTween.Sequence();
                        sqGravityTwoConstants.AppendCallback(() => main.gravityModifier = new ParticleSystem.MinMaxCurve(min, max));
                        return sqGravityTwoConstants;
                    case ParticleSystemCurveMode.Curve:
                        Sequence sqGravityCurve = DOTween.Sequence();
                        sqGravityCurve.AppendCallback(() => main.gravityModifier = new ParticleSystem.MinMaxCurve(multiplier, animationCurve));
                        return sqGravityCurve;
                    case ParticleSystemCurveMode.TwoCurves:
                        Sequence sqGravityTwoCurves = DOTween.Sequence();
                        sqGravityTwoCurves.AppendCallback(() => main.gravityModifier = new ParticleSystem.MinMaxCurve(multiplier, curveMin, curveMax));
                        return sqGravityTwoCurves;
                }
                break;
            case ParticleSystemCommand.RateOverTime:
                var emissionTime = ps.emission;
                switch (mode)
                {
                    case ParticleSystemCurveMode.Constant:
                        return DOTween.To(() => emissionTime.rateOverTime.constant, x => emissionTime.rateOverTime = x, to, duration);
                    case ParticleSystemCurveMode.TwoConstants:
                        Sequence sqRateTimeTwoConstants = DOTween.Sequence();
                        sqRateTimeTwoConstants.AppendCallback(() => emissionTime.rateOverTime = new ParticleSystem.MinMaxCurve(min, max));
                        return sqRateTimeTwoConstants;
                    case ParticleSystemCurveMode.Curve:
                        Sequence sqRateTimeCurve = DOTween.Sequence();
                        sqRateTimeCurve.AppendCallback(() => emissionTime.rateOverTime = new ParticleSystem.MinMaxCurve(multiplier, animationCurve));
                        return sqRateTimeCurve;
                    case ParticleSystemCurveMode.TwoCurves:
                        Sequence sqRateTimeTwoCurves = DOTween.Sequence();
                        sqRateTimeTwoCurves.AppendCallback(() => emissionTime.rateOverTime = new ParticleSystem.MinMaxCurve(multiplier, curveMin, curveMax));
                        return sqRateTimeTwoCurves;
                }
                break;
            case ParticleSystemCommand.RateOverDistance:
                var emissionDistance = ps.emission;
                switch (mode)
                {
                    case ParticleSystemCurveMode.Constant:
                        return DOTween.To(() => emissionDistance.rateOverDistance.constant, x => emissionDistance.rateOverDistance = x, to, duration);
                    case ParticleSystemCurveMode.TwoConstants:
                        Sequence sqRateDistanceTwoConstants = DOTween.Sequence();
                        sqRateDistanceTwoConstants.AppendCallback(() => emissionDistance.rateOverDistance = new ParticleSystem.MinMaxCurve(min, max));
                        return sqRateDistanceTwoConstants;
                    case ParticleSystemCurveMode.Curve:
                        Sequence sqRateDistanceCurve = DOTween.Sequence();
                        sqRateDistanceCurve.AppendCallback(() => emissionDistance.rateOverDistance = new ParticleSystem.MinMaxCurve(multiplier, animationCurve));
                        return sqRateDistanceCurve;
                    case ParticleSystemCurveMode.TwoCurves:
                        Sequence sqRateDistanceTwoCurves = DOTween.Sequence();
                        sqRateDistanceTwoCurves.AppendCallback(() => emissionDistance.rateOverDistance = new ParticleSystem.MinMaxCurve(multiplier, curveMin, curveMax));
                        return sqRateDistanceTwoCurves;
                }
                break;
        }
        return null;
    }

    private bool IsConstant()
    {
        return IsColor() == false && mode == ParticleSystemCurveMode.Constant;
    }

    private bool IsCurve()
    {
        return IsColor() == false && mode == ParticleSystemCurveMode.Curve;
    }

    private bool IsTwoConstants()
    {
        return IsColor() == false && mode == ParticleSystemCurveMode.TwoConstants;
    }

    private bool IsTwoCurves()
    {
        return IsColor() == false && mode == ParticleSystemCurveMode.TwoCurves;
    }

    private bool IsCurveOrTwoCurves()
    {
        return IsColor() == false && mode == ParticleSystemCurveMode.Curve || mode == ParticleSystemCurveMode.TwoCurves;
    }

    private bool IsColor()
    {
        return command == ParticleSystemCommand.StartColor;
    }

    private bool ShowColor()
    {
        return IsColor() && colorMode == ParticleSystemGradientMode.Color;
    }

    private bool ShowColorMinMax()
    {
        return IsColor() && colorMode == ParticleSystemGradientMode.TwoColors;
    }

    private bool ShowGradient()
    {
        return IsColor() && (colorMode == ParticleSystemGradientMode.Gradient || colorMode == ParticleSystemGradientMode.RandomColor);
    }

    private bool ShowGradientMinMax()
    {
        return IsColor() && colorMode == ParticleSystemGradientMode.TwoGradients;
    }

    private bool ValidateMode(ParticleSystemCurveMode mode)
    {
        return mode == ParticleSystemCurveMode.Constant;
    }

    private bool ValidateColorMode(ParticleSystemGradientMode mode)
    {
        return mode == ParticleSystemGradientMode.Color;
    }

    public enum ParticleSystemCommand
    {
        StartLifetime = 0,
        StartSpeed = 100,
        StartSize = 200,
        StartSizeX = 250,
        StartSizeY = 251,
        StartSizeZ = 252,
        StartRotation = 300,
        StartRotationX = 350,
        StartRotationY = 351,
        StartRotationZ = 352,
        StartColor = 400,
        GravityModifier = 500,
        RateOverTime = 600,
        RateOverDistance = 700,
    }
}
