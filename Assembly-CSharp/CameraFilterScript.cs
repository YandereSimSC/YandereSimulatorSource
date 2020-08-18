﻿using System;
using UnityEngine;

// Token: 0x020000F9 RID: 249
public class CameraFilterScript : MonoBehaviour
{
	// Token: 0x06000AA9 RID: 2729 RVA: 0x00059268 File Offset: 0x00057468
	private void Start()
	{
		this.Anomaly = base.gameObject.AddComponent<CameraFilterPack_3D_Anomaly>();
		this.Anomaly.enabled = false;
		this.Binary = base.gameObject.AddComponent<CameraFilterPack_3D_Binary>();
		this.Binary.enabled = false;
		this.BlackHole3D = base.gameObject.AddComponent<CameraFilterPack_3D_BlackHole>();
		this.BlackHole3D.enabled = false;
		this.Computer = base.gameObject.AddComponent<CameraFilterPack_3D_Computer>();
		this.Computer.enabled = false;
		this.Distortion = base.gameObject.AddComponent<CameraFilterPack_3D_Distortion>();
		this.Distortion.enabled = false;
		this.FogSmoke = base.gameObject.AddComponent<CameraFilterPack_3D_Fog_Smoke>();
		this.FogSmoke.enabled = false;
		this.Ghost = base.gameObject.AddComponent<CameraFilterPack_3D_Ghost>();
		this.Ghost.enabled = false;
		this.Inverse = base.gameObject.AddComponent<CameraFilterPack_3D_Inverse>();
		this.Inverse.enabled = false;
		this.Matrix = base.gameObject.AddComponent<CameraFilterPack_3D_Matrix>();
		this.Matrix.enabled = false;
		this.Mirror3D = base.gameObject.AddComponent<CameraFilterPack_3D_Mirror>();
		this.Mirror3D.enabled = false;
		this.Myst = base.gameObject.AddComponent<CameraFilterPack_3D_Myst>();
		this.Myst.enabled = false;
		this.ScanScene = base.gameObject.AddComponent<CameraFilterPack_3D_Scan_Scene>();
		this.ScanScene.enabled = false;
		this.Shield = base.gameObject.AddComponent<CameraFilterPack_3D_Shield>();
		this.Shield.enabled = false;
		this.Snow = base.gameObject.AddComponent<CameraFilterPack_3D_Snow>();
		this.Snow.enabled = false;
		this.AAABlood = base.gameObject.AddComponent<CameraFilterPack_AAA_Blood>();
		this.AAABlood.enabled = false;
		this.AAABloodOnScreen = base.gameObject.AddComponent<CameraFilterPack_AAA_BloodOnScreen>();
		this.AAABloodOnScreen.enabled = false;
		this.AAABloodHit = base.gameObject.AddComponent<CameraFilterPack_AAA_Blood_Hit>();
		this.AAABloodHit.enabled = false;
		this.AAABloodPlus = base.gameObject.AddComponent<CameraFilterPack_AAA_Blood_Plus>();
		this.AAABloodPlus.enabled = false;
		this.SuperComputer = base.gameObject.AddComponent<CameraFilterPack_AAA_SuperComputer>();
		this.SuperComputer.enabled = false;
		this.SuperHexagon = base.gameObject.AddComponent<CameraFilterPack_AAA_SuperHexagon>();
		this.SuperHexagon.enabled = false;
		this.WaterDrop = base.gameObject.AddComponent<CameraFilterPack_AAA_WaterDrop>();
		this.WaterDrop.enabled = false;
		this.WaterDropPro = base.gameObject.AddComponent<CameraFilterPack_AAA_WaterDropPro>();
		this.WaterDropPro.enabled = false;
		this.AlienVision = base.gameObject.AddComponent<CameraFilterPack_Alien_Vision>();
		this.AlienVision.enabled = false;
		this.FXAA = base.gameObject.AddComponent<CameraFilterPack_Antialiasing_FXAA>();
		this.FXAA.enabled = false;
		this.Fog = base.gameObject.AddComponent<CameraFilterPack_Atmosphere_Fog>();
		this.Fog.enabled = false;
		this.Rain = base.gameObject.AddComponent<CameraFilterPack_Atmosphere_Rain>();
		this.Rain.enabled = false;
		this.RainPro = base.gameObject.AddComponent<CameraFilterPack_Atmosphere_Rain_Pro>();
		this.RainPro.enabled = false;
		this.RainPro3D = base.gameObject.AddComponent<CameraFilterPack_Atmosphere_Rain_Pro_3D>();
		this.RainPro3D.enabled = false;
		this.Snow8bits = base.gameObject.AddComponent<CameraFilterPack_Atmosphere_Snow_8bits>();
		this.Snow8bits.enabled = false;
		this.Blend = base.gameObject.AddComponent<CameraFilterPack_Blend2Camera_Blend>();
		this.Blend.enabled = false;
		this.BlueScreen = base.gameObject.AddComponent<CameraFilterPack_Blend2Camera_BlueScreen>();
		this.BlueScreen.enabled = false;
		this.Color = base.gameObject.AddComponent<CameraFilterPack_Blend2Camera_Color>();
		this.Color.enabled = false;
		this.ColorBurn = base.gameObject.AddComponent<CameraFilterPack_Blend2Camera_ColorBurn>();
		this.ColorBurn.enabled = false;
		this.ColorDodge = base.gameObject.AddComponent<CameraFilterPack_Blend2Camera_ColorDodge>();
		this.ColorDodge.enabled = false;
		this.ColorKey = base.gameObject.AddComponent<CameraFilterPack_Blend2Camera_ColorKey>();
		this.ColorKey.enabled = false;
		this.Darken = base.gameObject.AddComponent<CameraFilterPack_Blend2Camera_Darken>();
		this.Darken.enabled = false;
		this.DarkerColor = base.gameObject.AddComponent<CameraFilterPack_Blend2Camera_DarkerColor>();
		this.DarkerColor.enabled = false;
		this.Difference = base.gameObject.AddComponent<CameraFilterPack_Blend2Camera_Difference>();
		this.Difference.enabled = false;
		this.Divide = base.gameObject.AddComponent<CameraFilterPack_Blend2Camera_Divide>();
		this.Divide.enabled = false;
		this.Exclusion = base.gameObject.AddComponent<CameraFilterPack_Blend2Camera_Exclusion>();
		this.Exclusion.enabled = false;
		this.GreenScreen = base.gameObject.AddComponent<CameraFilterPack_Blend2Camera_GreenScreen>();
		this.GreenScreen.enabled = false;
		this.HardLight = base.gameObject.AddComponent<CameraFilterPack_Blend2Camera_HardLight>();
		this.HardLight.enabled = false;
		this.HardMix = base.gameObject.AddComponent<CameraFilterPack_Blend2Camera_HardMix>();
		this.HardMix.enabled = false;
		this.Blend2CameraHue = base.gameObject.AddComponent<CameraFilterPack_Blend2Camera_Hue>();
		this.Blend2CameraHue.enabled = false;
		this.Lighten = base.gameObject.AddComponent<CameraFilterPack_Blend2Camera_Lighten>();
		this.Lighten.enabled = false;
		this.LighterColor = base.gameObject.AddComponent<CameraFilterPack_Blend2Camera_LighterColor>();
		this.LighterColor.enabled = false;
		this.LinearBurn = base.gameObject.AddComponent<CameraFilterPack_Blend2Camera_LinearBurn>();
		this.LinearBurn.enabled = false;
		this.LinearDodge = base.gameObject.AddComponent<CameraFilterPack_Blend2Camera_LinearDodge>();
		this.LinearDodge.enabled = false;
		this.LinearLight = base.gameObject.AddComponent<CameraFilterPack_Blend2Camera_LinearLight>();
		this.LinearLight.enabled = false;
		this.Luminosity = base.gameObject.AddComponent<CameraFilterPack_Blend2Camera_Luminosity>();
		this.Luminosity.enabled = false;
		this.Multiply = base.gameObject.AddComponent<CameraFilterPack_Blend2Camera_Multiply>();
		this.Multiply.enabled = false;
		this.Overlay = base.gameObject.AddComponent<CameraFilterPack_Blend2Camera_Overlay>();
		this.Overlay.enabled = false;
		this.PhotoshopFilters = base.gameObject.AddComponent<CameraFilterPack_Blend2Camera_PhotoshopFilters>();
		this.PhotoshopFilters.enabled = false;
		this.PinLight = base.gameObject.AddComponent<CameraFilterPack_Blend2Camera_PinLight>();
		this.PinLight.enabled = false;
		this.Saturation = base.gameObject.AddComponent<CameraFilterPack_Blend2Camera_Saturation>();
		this.Saturation.enabled = false;
		this.Screen = base.gameObject.AddComponent<CameraFilterPack_Blend2Camera_Screen>();
		this.Screen.enabled = false;
		this.SoftLight = base.gameObject.AddComponent<CameraFilterPack_Blend2Camera_SoftLight>();
		this.SoftLight.enabled = false;
		this.SplitScreen = base.gameObject.AddComponent<CameraFilterPack_Blend2Camera_SplitScreen>();
		this.SplitScreen.enabled = false;
		this.SplitScreen3D = base.gameObject.AddComponent<CameraFilterPack_Blend2Camera_SplitScreen3D>();
		this.SplitScreen3D.enabled = false;
		this.Subtract = base.gameObject.AddComponent<CameraFilterPack_Blend2Camera_Subtract>();
		this.Subtract.enabled = false;
		this.VividLight = base.gameObject.AddComponent<CameraFilterPack_Blend2Camera_VividLight>();
		this.VividLight.enabled = false;
		this.Blizzard = base.gameObject.AddComponent<CameraFilterPack_Blizzard>();
		this.Blizzard.enabled = false;
		this.Bloom = base.gameObject.AddComponent<CameraFilterPack_Blur_Bloom>();
		this.Bloom.enabled = false;
		this.BlurHole = base.gameObject.AddComponent<CameraFilterPack_Blur_BlurHole>();
		this.BlurHole.enabled = false;
		this.Blurry = base.gameObject.AddComponent<CameraFilterPack_Blur_Blurry>();
		this.Blurry.enabled = false;
		this.Dithering2x2 = base.gameObject.AddComponent<CameraFilterPack_Blur_Dithering2x2>();
		this.Dithering2x2.enabled = false;
		this.DitherOffset = base.gameObject.AddComponent<CameraFilterPack_Blur_DitherOffset>();
		this.DitherOffset.enabled = false;
		this.Focus = base.gameObject.AddComponent<CameraFilterPack_Blur_Focus>();
		this.Focus.enabled = false;
		this.GaussianBlur = base.gameObject.AddComponent<CameraFilterPack_Blur_GaussianBlur>();
		this.GaussianBlur.enabled = false;
		this.Movie = base.gameObject.AddComponent<CameraFilterPack_Blur_Movie>();
		this.Movie.enabled = false;
		this.BlurNoise = base.gameObject.AddComponent<CameraFilterPack_Blur_Noise>();
		this.BlurNoise.enabled = false;
		this.Radial = base.gameObject.AddComponent<CameraFilterPack_Blur_Radial>();
		this.Radial.enabled = false;
		this.RadialFast = base.gameObject.AddComponent<CameraFilterPack_Blur_Radial_Fast>();
		this.RadialFast.enabled = false;
		this.Regular = base.gameObject.AddComponent<CameraFilterPack_Blur_Regular>();
		this.Regular.enabled = false;
		this.Steam = base.gameObject.AddComponent<CameraFilterPack_Blur_Steam>();
		this.Steam.enabled = false;
		this.TiltShift = base.gameObject.AddComponent<CameraFilterPack_Blur_Tilt_Shift>();
		this.TiltShift.enabled = false;
		this.TiltShiftHole = base.gameObject.AddComponent<CameraFilterPack_Blur_Tilt_Shift_Hole>();
		this.TiltShiftHole.enabled = false;
		this.TiltShiftV = base.gameObject.AddComponent<CameraFilterPack_Blur_Tilt_Shift_V>();
		this.TiltShiftV.enabled = false;
		this.BrokenScreen = base.gameObject.AddComponent<CameraFilterPack_Broken_Screen>();
		this.BrokenScreen.enabled = false;
		this.BrokenSimple = base.gameObject.AddComponent<CameraFilterPack_Broken_Simple>();
		this.BrokenSimple.enabled = false;
		this.Spliter = base.gameObject.AddComponent<CameraFilterPack_Broken_Spliter>();
		this.Spliter.enabled = false;
		this.ThermalVision = base.gameObject.AddComponent<CameraFilterPack_Classic_ThermalVision>();
		this.ThermalVision.enabled = false;
		this.AdjustColorRGB = base.gameObject.AddComponent<CameraFilterPack_Colors_Adjust_ColorRGB>();
		this.AdjustColorRGB.enabled = false;
		this.AdjustFullColors = base.gameObject.AddComponent<CameraFilterPack_Colors_Adjust_FullColors>();
		this.AdjustFullColors.enabled = false;
		this.AdjustPreFilters = base.gameObject.AddComponent<CameraFilterPack_Colors_Adjust_PreFilters>();
		this.AdjustPreFilters.enabled = false;
		this.BleachBypass = base.gameObject.AddComponent<CameraFilterPack_Colors_BleachBypass>();
		this.BleachBypass.enabled = false;
		this.Brightness = base.gameObject.AddComponent<CameraFilterPack_Colors_Brightness>();
		this.Brightness.enabled = false;
		this.DarkColor = base.gameObject.AddComponent<CameraFilterPack_Colors_DarkColor>();
		this.DarkColor.enabled = false;
		this.HSV = base.gameObject.AddComponent<CameraFilterPack_Colors_HSV>();
		this.HSV.enabled = false;
		this.HUERotate = base.gameObject.AddComponent<CameraFilterPack_Colors_HUE_Rotate>();
		this.HUERotate.enabled = false;
		this.NewPosterize = base.gameObject.AddComponent<CameraFilterPack_Colors_NewPosterize>();
		this.NewPosterize.enabled = false;
		this.RgbClamp = base.gameObject.AddComponent<CameraFilterPack_Colors_RgbClamp>();
		this.RgbClamp.enabled = false;
		this.Threshold = base.gameObject.AddComponent<CameraFilterPack_Colors_Threshold>();
		this.Threshold.enabled = false;
		this.AdjustLevels = base.gameObject.AddComponent<CameraFilterPack_Color_Adjust_Levels>();
		this.AdjustLevels.enabled = false;
		this.BrightContrastSaturation = base.gameObject.AddComponent<CameraFilterPack_Color_BrightContrastSaturation>();
		this.BrightContrastSaturation.enabled = false;
		this.ChromaticAberration = base.gameObject.AddComponent<CameraFilterPack_Color_Chromatic_Aberration>();
		this.ChromaticAberration.enabled = false;
		this.ChromaticPlus = base.gameObject.AddComponent<CameraFilterPack_Color_Chromatic_Plus>();
		this.ChromaticPlus.enabled = false;
		this.Contrast = base.gameObject.AddComponent<CameraFilterPack_Color_Contrast>();
		this.Contrast.enabled = false;
		this.GrayScale = base.gameObject.AddComponent<CameraFilterPack_Color_GrayScale>();
		this.GrayScale.enabled = false;
		this.Invert = base.gameObject.AddComponent<CameraFilterPack_Color_Invert>();
		this.Invert.enabled = false;
		this.ColorNoise = base.gameObject.AddComponent<CameraFilterPack_Color_Noise>();
		this.ColorNoise.enabled = false;
		this.ColorRGB = base.gameObject.AddComponent<CameraFilterPack_Color_RGB>();
		this.ColorRGB.enabled = false;
		this.Sepia = base.gameObject.AddComponent<CameraFilterPack_Color_Sepia>();
		this.Sepia.enabled = false;
		this.Switching = base.gameObject.AddComponent<CameraFilterPack_Color_Switching>();
		this.Switching.enabled = false;
		this.YUV = base.gameObject.AddComponent<CameraFilterPack_Color_YUV>();
		this.YUV.enabled = false;
		this.Normal = base.gameObject.AddComponent<CameraFilterPack_Convert_Normal>();
		this.Normal.enabled = false;
		this.Aspiration = base.gameObject.AddComponent<CameraFilterPack_Distortion_Aspiration>();
		this.Aspiration.enabled = false;
		this.BigFace = base.gameObject.AddComponent<CameraFilterPack_Distortion_BigFace>();
		this.BigFace.enabled = false;
		this.BlackHole = base.gameObject.AddComponent<CameraFilterPack_Distortion_BlackHole>();
		this.BlackHole.enabled = false;
		this.Dissipation = base.gameObject.AddComponent<CameraFilterPack_Distortion_Dissipation>();
		this.Dissipation.enabled = false;
		this.Dream = base.gameObject.AddComponent<CameraFilterPack_Distortion_Dream>();
		this.Dream.enabled = false;
		this.Dream2 = base.gameObject.AddComponent<CameraFilterPack_Distortion_Dream2>();
		this.Dream2.enabled = false;
		this.FishEye = base.gameObject.AddComponent<CameraFilterPack_Distortion_FishEye>();
		this.FishEye.enabled = false;
		this.Flag = base.gameObject.AddComponent<CameraFilterPack_Distortion_Flag>();
		this.Flag.enabled = false;
		this.Flush = base.gameObject.AddComponent<CameraFilterPack_Distortion_Flush>();
		this.Flush.enabled = false;
		this.HalfSphere = base.gameObject.AddComponent<CameraFilterPack_Distortion_Half_Sphere>();
		this.HalfSphere.enabled = false;
		this.Heat = base.gameObject.AddComponent<CameraFilterPack_Distortion_Heat>();
		this.Heat.enabled = false;
		this.Lens = base.gameObject.AddComponent<CameraFilterPack_Distortion_Lens>();
		this.Lens.enabled = false;
		this.DistortionNoise = base.gameObject.AddComponent<CameraFilterPack_Distortion_Noise>();
		this.DistortionNoise.enabled = false;
		this.ShockWave = base.gameObject.AddComponent<CameraFilterPack_Distortion_ShockWave>();
		this.ShockWave.enabled = false;
		this.ShockWaveManual = base.gameObject.AddComponent<CameraFilterPack_Distortion_ShockWaveManual>();
		this.ShockWaveManual.enabled = false;
		this.Twist = base.gameObject.AddComponent<CameraFilterPack_Distortion_Twist>();
		this.Twist.enabled = false;
		this.TwistSquare = base.gameObject.AddComponent<CameraFilterPack_Distortion_Twist_Square>();
		this.TwistSquare.enabled = false;
		this.DistortionWaterDrop = base.gameObject.AddComponent<CameraFilterPack_Distortion_Water_Drop>();
		this.DistortionWaterDrop.enabled = false;
		this.WaveHorizontal = base.gameObject.AddComponent<CameraFilterPack_Distortion_Wave_Horizontal>();
		this.WaveHorizontal.enabled = false;
		this.BluePrint = base.gameObject.AddComponent<CameraFilterPack_Drawing_BluePrint>();
		this.BluePrint.enabled = false;
		this.CellShading = base.gameObject.AddComponent<CameraFilterPack_Drawing_CellShading>();
		this.CellShading.enabled = false;
		this.CellShading2 = base.gameObject.AddComponent<CameraFilterPack_Drawing_CellShading2>();
		this.CellShading2.enabled = false;
		this.Comics = base.gameObject.AddComponent<CameraFilterPack_Drawing_Comics>();
		this.Comics.enabled = false;
		this.Crosshatch = base.gameObject.AddComponent<CameraFilterPack_Drawing_Crosshatch>();
		this.Crosshatch.enabled = false;
		this.Curve = base.gameObject.AddComponent<CameraFilterPack_Drawing_Curve>();
		this.Curve.enabled = false;
		this.EnhancedComics = base.gameObject.AddComponent<CameraFilterPack_Drawing_EnhancedComics>();
		this.EnhancedComics.enabled = false;
		this.Halftone = base.gameObject.AddComponent<CameraFilterPack_Drawing_Halftone>();
		this.Halftone.enabled = false;
		this.Laplacian = base.gameObject.AddComponent<CameraFilterPack_Drawing_Laplacian>();
		this.Laplacian.enabled = false;
		this.Lines = base.gameObject.AddComponent<CameraFilterPack_Drawing_Lines>();
		this.Lines.enabled = false;
		this.Manga = base.gameObject.AddComponent<CameraFilterPack_Drawing_Manga>();
		this.Manga.enabled = false;
		this.Manga2 = base.gameObject.AddComponent<CameraFilterPack_Drawing_Manga2>();
		this.Manga2.enabled = false;
		this.Manga3 = base.gameObject.AddComponent<CameraFilterPack_Drawing_Manga3>();
		this.Manga3.enabled = false;
		this.Manga4 = base.gameObject.AddComponent<CameraFilterPack_Drawing_Manga4>();
		this.Manga4.enabled = false;
		this.Manga5 = base.gameObject.AddComponent<CameraFilterPack_Drawing_Manga5>();
		this.Manga5.enabled = false;
		this.MangaColor = base.gameObject.AddComponent<CameraFilterPack_Drawing_Manga_Color>();
		this.MangaColor.enabled = false;
		this.MangaFlash = base.gameObject.AddComponent<CameraFilterPack_Drawing_Manga_Flash>();
		this.MangaFlash.enabled = false;
		this.MangaFlashWhite = base.gameObject.AddComponent<CameraFilterPack_Drawing_Manga_FlashWhite>();
		this.MangaFlashWhite.enabled = false;
		this.MangaFlashColor = base.gameObject.AddComponent<CameraFilterPack_Drawing_Manga_Flash_Color>();
		this.MangaFlashColor.enabled = false;
		this.NewCellShading = base.gameObject.AddComponent<CameraFilterPack_Drawing_NewCellShading>();
		this.NewCellShading.enabled = false;
		this.Paper = base.gameObject.AddComponent<CameraFilterPack_Drawing_Paper>();
		this.Paper.enabled = false;
		this.Paper2 = base.gameObject.AddComponent<CameraFilterPack_Drawing_Paper2>();
		this.Paper2.enabled = false;
		this.Paper3 = base.gameObject.AddComponent<CameraFilterPack_Drawing_Paper3>();
		this.Paper3.enabled = false;
		this.Toon = base.gameObject.AddComponent<CameraFilterPack_Drawing_Toon>();
		this.Toon.enabled = false;
		this.BlackLine = base.gameObject.AddComponent<CameraFilterPack_Edge_BlackLine>();
		this.BlackLine.enabled = false;
		this.Edgefilter = base.gameObject.AddComponent<CameraFilterPack_Edge_Edge_filter>();
		this.Edgefilter.enabled = false;
		this.Golden = base.gameObject.AddComponent<CameraFilterPack_Edge_Golden>();
		this.Golden.enabled = false;
		this.Neon = base.gameObject.AddComponent<CameraFilterPack_Edge_Neon>();
		this.Neon.enabled = false;
		this.Sigmoid = base.gameObject.AddComponent<CameraFilterPack_Edge_Sigmoid>();
		this.Sigmoid.enabled = false;
		this.Sobel = base.gameObject.AddComponent<CameraFilterPack_Edge_Sobel>();
		this.Sobel.enabled = false;
		this.Rotation = base.gameObject.AddComponent<CameraFilterPack_EXTRA_Rotation>();
		this.Rotation.enabled = false;
		this.SHOWFPS = base.gameObject.AddComponent<CameraFilterPack_EXTRA_SHOWFPS>();
		this.SHOWFPS.enabled = false;
		this.EyesVision1 = base.gameObject.AddComponent<CameraFilterPack_EyesVision_1>();
		this.EyesVision1.enabled = false;
		this.EyesVision2 = base.gameObject.AddComponent<CameraFilterPack_EyesVision_2>();
		this.EyesVision2.enabled = false;
		this.ColorPerfection = base.gameObject.AddComponent<CameraFilterPack_Film_ColorPerfection>();
		this.ColorPerfection.enabled = false;
		this.Grain = base.gameObject.AddComponent<CameraFilterPack_Film_Grain>();
		this.Grain.enabled = false;
		this.FlyVision = base.gameObject.AddComponent<CameraFilterPack_Fly_Vision>();
		this.FlyVision.enabled = false;
		this.FX8bits = base.gameObject.AddComponent<CameraFilterPack_FX_8bits>();
		this.FX8bits.enabled = false;
		this.FX8bitsgb = base.gameObject.AddComponent<CameraFilterPack_FX_8bits_gb>();
		this.FX8bitsgb.enabled = false;
		this.Ascii = base.gameObject.AddComponent<CameraFilterPack_FX_Ascii>();
		this.Ascii.enabled = false;
		this.DarkMatter = base.gameObject.AddComponent<CameraFilterPack_FX_DarkMatter>();
		this.DarkMatter.enabled = false;
		this.DigitalMatrix = base.gameObject.AddComponent<CameraFilterPack_FX_DigitalMatrix>();
		this.DigitalMatrix.enabled = false;
		this.DigitalMatrixDistortion = base.gameObject.AddComponent<CameraFilterPack_FX_DigitalMatrixDistortion>();
		this.DigitalMatrixDistortion.enabled = false;
		this.DotCircle = base.gameObject.AddComponent<CameraFilterPack_FX_Dot_Circle>();
		this.DotCircle.enabled = false;
		this.Drunk = base.gameObject.AddComponent<CameraFilterPack_FX_Drunk>();
		this.Drunk.enabled = false;
		this.Drunk2 = base.gameObject.AddComponent<CameraFilterPack_FX_Drunk2>();
		this.Drunk2.enabled = false;
		this.EarthQuake = base.gameObject.AddComponent<CameraFilterPack_FX_EarthQuake>();
		this.EarthQuake.enabled = false;
		this.Funk = base.gameObject.AddComponent<CameraFilterPack_FX_Funk>();
		this.Funk.enabled = false;
		this.Glitch1 = base.gameObject.AddComponent<CameraFilterPack_FX_Glitch1>();
		this.Glitch1.enabled = false;
		this.Glitch2 = base.gameObject.AddComponent<CameraFilterPack_FX_Glitch2>();
		this.Glitch2.enabled = false;
		this.Glitch3 = base.gameObject.AddComponent<CameraFilterPack_FX_Glitch3>();
		this.Glitch3.enabled = false;
		this.Grid = base.gameObject.AddComponent<CameraFilterPack_FX_Grid>();
		this.Grid.enabled = false;
		this.Hexagon = base.gameObject.AddComponent<CameraFilterPack_FX_Hexagon>();
		this.Hexagon.enabled = false;
		this.HexagonBlack = base.gameObject.AddComponent<CameraFilterPack_FX_Hexagon_Black>();
		this.HexagonBlack.enabled = false;
		this.Hypno = base.gameObject.AddComponent<CameraFilterPack_FX_Hypno>();
		this.Hypno.enabled = false;
		this.InverChromiLum = base.gameObject.AddComponent<CameraFilterPack_FX_InverChromiLum>();
		this.InverChromiLum.enabled = false;
		this.FXMirror = base.gameObject.AddComponent<CameraFilterPack_FX_Mirror>();
		this.FXMirror.enabled = false;
		this.FXPlasma = base.gameObject.AddComponent<CameraFilterPack_FX_Plasma>();
		this.FXPlasma.enabled = false;
		this.FXPsycho = base.gameObject.AddComponent<CameraFilterPack_FX_Psycho>();
		this.FXPsycho.enabled = false;
		this.Scan = base.gameObject.AddComponent<CameraFilterPack_FX_Scan>();
		this.Scan.enabled = false;
		this.Screens = base.gameObject.AddComponent<CameraFilterPack_FX_Screens>();
		this.Screens.enabled = false;
		this.Spot = base.gameObject.AddComponent<CameraFilterPack_FX_Spot>();
		this.Spot.enabled = false;
		this.superDot = base.gameObject.AddComponent<CameraFilterPack_FX_superDot>();
		this.superDot.enabled = false;
		this.ZebraColor = base.gameObject.AddComponent<CameraFilterPack_FX_ZebraColor>();
		this.ZebraColor.enabled = false;
		this.GlassesOn = base.gameObject.AddComponent<CameraFilterPack_Glasses_On>();
		this.GlassesOn.enabled = false;
		this.GlassesOn2 = base.gameObject.AddComponent<CameraFilterPack_Glasses_On_2>();
		this.GlassesOn2.enabled = false;
		this.GlassesOn3 = base.gameObject.AddComponent<CameraFilterPack_Glasses_On_3>();
		this.GlassesOn3.enabled = false;
		this.GlassesOn4 = base.gameObject.AddComponent<CameraFilterPack_Glasses_On_4>();
		this.GlassesOn4.enabled = false;
		this.GlassesOn5 = base.gameObject.AddComponent<CameraFilterPack_Glasses_On_5>();
		this.GlassesOn5.enabled = false;
		this.GlassesOn6 = base.gameObject.AddComponent<CameraFilterPack_Glasses_On_6>();
		this.GlassesOn6.enabled = false;
		this.Mozaic = base.gameObject.AddComponent<CameraFilterPack_Glitch_Mozaic>();
		this.Mozaic.enabled = false;
		this.Glow = base.gameObject.AddComponent<CameraFilterPack_Glow_Glow>();
		this.Glow.enabled = false;
		this.GlowColor = base.gameObject.AddComponent<CameraFilterPack_Glow_Glow_Color>();
		this.GlowColor.enabled = false;
		this.Ansi = base.gameObject.AddComponent<CameraFilterPack_Gradients_Ansi>();
		this.Ansi.enabled = false;
		this.Desert = base.gameObject.AddComponent<CameraFilterPack_Gradients_Desert>();
		this.Desert.enabled = false;
		this.ElectricGradient = base.gameObject.AddComponent<CameraFilterPack_Gradients_ElectricGradient>();
		this.ElectricGradient.enabled = false;
		this.FireGradient = base.gameObject.AddComponent<CameraFilterPack_Gradients_FireGradient>();
		this.FireGradient.enabled = false;
		this.GradientsHue = base.gameObject.AddComponent<CameraFilterPack_Gradients_Hue>();
		this.GradientsHue.enabled = false;
		this.NeonGradient = base.gameObject.AddComponent<CameraFilterPack_Gradients_NeonGradient>();
		this.NeonGradient.enabled = false;
		this.GradientsRainbow = base.gameObject.AddComponent<CameraFilterPack_Gradients_Rainbow>();
		this.GradientsRainbow.enabled = false;
		this.Stripe = base.gameObject.AddComponent<CameraFilterPack_Gradients_Stripe>();
		this.Stripe.enabled = false;
		this.Tech = base.gameObject.AddComponent<CameraFilterPack_Gradients_Tech>();
		this.Tech.enabled = false;
		this.Therma = base.gameObject.AddComponent<CameraFilterPack_Gradients_Therma>();
		this.Therma.enabled = false;
		this.LightRainbow = base.gameObject.AddComponent<CameraFilterPack_Light_Rainbow>();
		this.LightRainbow.enabled = false;
		this.LightRainbow2 = base.gameObject.AddComponent<CameraFilterPack_Light_Rainbow2>();
		this.LightRainbow2.enabled = false;
		this.Water = base.gameObject.AddComponent<CameraFilterPack_Light_Water>();
		this.Water.enabled = false;
		this.Water2 = base.gameObject.AddComponent<CameraFilterPack_Light_Water2>();
		this.Water2.enabled = false;
		this.Lut = base.gameObject.AddComponent<CameraFilterPack_Lut_2_Lut>();
		this.Lut.enabled = false;
		this.LutExtra = base.gameObject.AddComponent<CameraFilterPack_Lut_2_Lut_Extra>();
		this.LutExtra.enabled = false;
		this.Mask = base.gameObject.AddComponent<CameraFilterPack_Lut_Mask>();
		this.Mask.enabled = false;
		this.PlayWith = base.gameObject.AddComponent<CameraFilterPack_Lut_PlayWith>();
		this.PlayWith.enabled = false;
		this.Plus = base.gameObject.AddComponent<CameraFilterPack_Lut_Plus>();
		this.Plus.enabled = false;
		this.LutSimple = base.gameObject.AddComponent<CameraFilterPack_Lut_Simple>();
		this.LutSimple.enabled = false;
		this.TestMode = base.gameObject.AddComponent<CameraFilterPack_Lut_TestMode>();
		this.TestMode.enabled = false;
		this.NewGlitch1 = base.gameObject.AddComponent<CameraFilterPack_NewGlitch1>();
		this.NewGlitch1.enabled = false;
		this.NewGlitch2 = base.gameObject.AddComponent<CameraFilterPack_NewGlitch2>();
		this.NewGlitch2.enabled = false;
		this.NewGlitch3 = base.gameObject.AddComponent<CameraFilterPack_NewGlitch3>();
		this.NewGlitch3.enabled = false;
		this.NewGlitch4 = base.gameObject.AddComponent<CameraFilterPack_NewGlitch4>();
		this.NewGlitch4.enabled = false;
		this.NewGlitch5 = base.gameObject.AddComponent<CameraFilterPack_NewGlitch5>();
		this.NewGlitch5.enabled = false;
		this.NewGlitch6 = base.gameObject.AddComponent<CameraFilterPack_NewGlitch6>();
		this.NewGlitch6.enabled = false;
		this.NewGlitch7 = base.gameObject.AddComponent<CameraFilterPack_NewGlitch7>();
		this.NewGlitch7.enabled = false;
		this.NightVisionFX = base.gameObject.AddComponent<CameraFilterPack_NightVisionFX>();
		this.NightVisionFX.enabled = false;
		this.NightVision4 = base.gameObject.AddComponent<CameraFilterPack_NightVision_4>();
		this.NightVision4.enabled = false;
		this.TV = base.gameObject.AddComponent<CameraFilterPack_Noise_TV>();
		this.TV.enabled = false;
		this.TV2 = base.gameObject.AddComponent<CameraFilterPack_Noise_TV_2>();
		this.TV2.enabled = false;
		this.TV3 = base.gameObject.AddComponent<CameraFilterPack_Noise_TV_3>();
		this.TV3.enabled = false;
		this.NightVision1 = base.gameObject.AddComponent<CameraFilterPack_Oculus_NightVision1>();
		this.NightVision1.enabled = false;
		this.NightVision2 = base.gameObject.AddComponent<CameraFilterPack_Oculus_NightVision2>();
		this.NightVision2.enabled = false;
		this.NightVision3 = base.gameObject.AddComponent<CameraFilterPack_Oculus_NightVision3>();
		this.NightVision3.enabled = false;
		this.NightVision5 = base.gameObject.AddComponent<CameraFilterPack_Oculus_NightVision5>();
		this.NightVision5.enabled = false;
		this.ThermaVision = base.gameObject.AddComponent<CameraFilterPack_Oculus_ThermaVision>();
		this.ThermaVision.enabled = false;
		this.Cutting1 = base.gameObject.AddComponent<CameraFilterPack_OldFilm_Cutting1>();
		this.Cutting1.enabled = false;
		this.Cutting2 = base.gameObject.AddComponent<CameraFilterPack_OldFilm_Cutting2>();
		this.Cutting2.enabled = false;
		this.DeepOilPaintHQ = base.gameObject.AddComponent<CameraFilterPack_Pixelisation_DeepOilPaintHQ>();
		this.DeepOilPaintHQ.enabled = false;
		this.Dot = base.gameObject.AddComponent<CameraFilterPack_Pixelisation_Dot>();
		this.Dot.enabled = false;
		this.OilPaint = base.gameObject.AddComponent<CameraFilterPack_Pixelisation_OilPaint>();
		this.OilPaint.enabled = false;
		this.OilPaintHQ = base.gameObject.AddComponent<CameraFilterPack_Pixelisation_OilPaintHQ>();
		this.OilPaintHQ.enabled = false;
		this.Sweater = base.gameObject.AddComponent<CameraFilterPack_Pixelisation_Sweater>();
		this.Sweater.enabled = false;
		this.Pixelisation = base.gameObject.AddComponent<CameraFilterPack_Pixel_Pixelisation>();
		this.Pixelisation.enabled = false;
		this.RainFX = base.gameObject.AddComponent<CameraFilterPack_Rain_RainFX>();
		this.RainFX.enabled = false;
		this.RealVHS = base.gameObject.AddComponent<CameraFilterPack_Real_VHS>();
		this.RealVHS.enabled = false;
		this.Loading = base.gameObject.AddComponent<CameraFilterPack_Retro_Loading>();
		this.Loading.enabled = false;
		this.Sharpen = base.gameObject.AddComponent<CameraFilterPack_Sharpen_Sharpen>();
		this.Sharpen.enabled = false;
		this.Bubble = base.gameObject.AddComponent<CameraFilterPack_Special_Bubble>();
		this.Bubble.enabled = false;
		this.TV50 = base.gameObject.AddComponent<CameraFilterPack_TV_50>();
		this.TV50.enabled = false;
		this.TV80 = base.gameObject.AddComponent<CameraFilterPack_TV_80>();
		this.TV80.enabled = false;
		this.ARCADE = base.gameObject.AddComponent<CameraFilterPack_TV_ARCADE>();
		this.ARCADE.enabled = false;
		this.ARCADE2 = base.gameObject.AddComponent<CameraFilterPack_TV_ARCADE_2>();
		this.ARCADE2.enabled = false;
		this.ARCADEFast = base.gameObject.AddComponent<CameraFilterPack_TV_ARCADE_Fast>();
		this.ARCADEFast.enabled = false;
		this.Artefact = base.gameObject.AddComponent<CameraFilterPack_TV_Artefact>();
		this.Artefact.enabled = false;
		this.BrokenGlass = base.gameObject.AddComponent<CameraFilterPack_TV_BrokenGlass>();
		this.BrokenGlass.enabled = false;
		this.BrokenGlass2 = base.gameObject.AddComponent<CameraFilterPack_TV_BrokenGlass2>();
		this.BrokenGlass2.enabled = false;
		this.Chromatical = base.gameObject.AddComponent<CameraFilterPack_TV_Chromatical>();
		this.Chromatical.enabled = false;
		this.Chromatical2 = base.gameObject.AddComponent<CameraFilterPack_TV_Chromatical2>();
		this.Chromatical2.enabled = false;
		this.CompressionFX = base.gameObject.AddComponent<CameraFilterPack_TV_CompressionFX>();
		this.CompressionFX.enabled = false;
		this.Distorted = base.gameObject.AddComponent<CameraFilterPack_TV_Distorted>();
		this.Distorted.enabled = false;
		this.Horror = base.gameObject.AddComponent<CameraFilterPack_TV_Horror>();
		this.Horror.enabled = false;
		this.LED = base.gameObject.AddComponent<CameraFilterPack_TV_LED>();
		this.LED.enabled = false;
		this.MovieNoise = base.gameObject.AddComponent<CameraFilterPack_TV_MovieNoise>();
		this.MovieNoise.enabled = false;
		this.TVNoise = base.gameObject.AddComponent<CameraFilterPack_TV_Noise>();
		this.TVNoise.enabled = false;
		this.Old = base.gameObject.AddComponent<CameraFilterPack_TV_Old>();
		this.Old.enabled = false;
		this.OldMovie = base.gameObject.AddComponent<CameraFilterPack_TV_Old_Movie>();
		this.OldMovie.enabled = false;
		this.OldMovie2 = base.gameObject.AddComponent<CameraFilterPack_TV_Old_Movie_2>();
		this.OldMovie2.enabled = false;
		this.PlanetMars = base.gameObject.AddComponent<CameraFilterPack_TV_PlanetMars>();
		this.PlanetMars.enabled = false;
		this.Posterize = base.gameObject.AddComponent<CameraFilterPack_TV_Posterize>();
		this.Posterize.enabled = false;
		this.TVRgb = base.gameObject.AddComponent<CameraFilterPack_TV_Rgb>();
		this.TVRgb.enabled = false;
		this.Tiles = base.gameObject.AddComponent<CameraFilterPack_TV_Tiles>();
		this.Tiles.enabled = false;
		this.Vcr = base.gameObject.AddComponent<CameraFilterPack_TV_Vcr>();
		this.Vcr.enabled = false;
		this.TVVHS = base.gameObject.AddComponent<CameraFilterPack_TV_VHS>();
		this.TVVHS.enabled = false;
		this.VHSRewind = base.gameObject.AddComponent<CameraFilterPack_TV_VHS_Rewind>();
		this.VHSRewind.enabled = false;
		this.Video3D = base.gameObject.AddComponent<CameraFilterPack_TV_Video3D>();
		this.Video3D.enabled = false;
		this.Videoflip = base.gameObject.AddComponent<CameraFilterPack_TV_Videoflip>();
		this.Videoflip.enabled = false;
		this.Vignetting = base.gameObject.AddComponent<CameraFilterPack_TV_Vignetting>();
		this.Vignetting.enabled = false;
		this.Vintage = base.gameObject.AddComponent<CameraFilterPack_TV_Vintage>();
		this.Vintage.enabled = false;
		this.WideScreenCircle = base.gameObject.AddComponent<CameraFilterPack_TV_WideScreenCircle>();
		this.WideScreenCircle.enabled = false;
		this.WideScreenHorizontal = base.gameObject.AddComponent<CameraFilterPack_TV_WideScreenHorizontal>();
		this.WideScreenHorizontal.enabled = false;
		this.WideScreenHV = base.gameObject.AddComponent<CameraFilterPack_TV_WideScreenHV>();
		this.WideScreenHV.enabled = false;
		this.WideScreenVertical = base.gameObject.AddComponent<CameraFilterPack_TV_WideScreenVertical>();
		this.WideScreenVertical.enabled = false;
		this.Tracking = base.gameObject.AddComponent<CameraFilterPack_VHS_Tracking>();
		this.Tracking.enabled = false;
		this.Aura = base.gameObject.AddComponent<CameraFilterPack_Vision_Aura>();
		this.Aura.enabled = false;
		this.AuraDistortion = base.gameObject.AddComponent<CameraFilterPack_Vision_AuraDistortion>();
		this.AuraDistortion.enabled = false;
		this.VisionBlood = base.gameObject.AddComponent<CameraFilterPack_Vision_Blood>();
		this.VisionBlood.enabled = false;
		this.VisionBloodFast = base.gameObject.AddComponent<CameraFilterPack_Vision_Blood_Fast>();
		this.VisionBloodFast.enabled = false;
		this.Crystal = base.gameObject.AddComponent<CameraFilterPack_Vision_Crystal>();
		this.Crystal.enabled = false;
		this.Drost = base.gameObject.AddComponent<CameraFilterPack_Vision_Drost>();
		this.Drost.enabled = false;
		this.VisionHellBlood = base.gameObject.AddComponent<CameraFilterPack_Vision_Hell_Blood>();
		this.VisionHellBlood.enabled = false;
		this.VisionPlasma = base.gameObject.AddComponent<CameraFilterPack_Vision_Plasma>();
		this.VisionPlasma.enabled = false;
		this.VisionPsycho = base.gameObject.AddComponent<CameraFilterPack_Vision_Psycho>();
		this.VisionPsycho.enabled = false;
		this.VisionRainbow = base.gameObject.AddComponent<CameraFilterPack_Vision_Rainbow>();
		this.VisionRainbow.enabled = false;
		this.SniperScore = base.gameObject.AddComponent<CameraFilterPack_Vision_SniperScore>();
		this.SniperScore.enabled = false;
		this.Tunnel = base.gameObject.AddComponent<CameraFilterPack_Vision_Tunnel>();
		this.Tunnel.enabled = false;
		this.Warp = base.gameObject.AddComponent<CameraFilterPack_Vision_Warp>();
		this.Warp.enabled = false;
		this.Warp2 = base.gameObject.AddComponent<CameraFilterPack_Vision_Warp2>();
		this.Warp2.enabled = false;
		this.ScanScene.AutoAnimatedNear = true;
	}

	// Token: 0x06000AAA RID: 2730 RVA: 0x0005B444 File Offset: 0x00059644
	private void Update()
	{
		if (Input.GetKey(KeyCode.LeftShift))
		{
			if (Input.GetKeyDown("z"))
			{
				this.FilterID -= 10;
				this.UpdateFilter();
			}
			if (Input.GetKeyDown("x"))
			{
				this.FilterID += 10;
				this.UpdateFilter();
			}
		}
		else
		{
			if (Input.GetKeyDown("z"))
			{
				this.FilterID--;
				this.UpdateFilter();
			}
			if (Input.GetKeyDown("x"))
			{
				this.FilterID++;
				this.UpdateFilter();
			}
		}
		if (this.DisplayTimer < 2f)
		{
			this.DisplayTimer += Time.deltaTime;
			this.NameLabel.transform.localPosition = Vector3.Lerp(this.NameLabel.transform.localPosition, new Vector3(1245f, 0f, 0f), Time.deltaTime * 10f);
			return;
		}
		this.Speed += Time.deltaTime * 10f;
		this.NameLabel.transform.localPosition = Vector3.MoveTowards(this.NameLabel.transform.localPosition, Vector3.zero, this.Speed);
	}

	// Token: 0x06000AAB RID: 2731 RVA: 0x0005B590 File Offset: 0x00059790
	private void UpdateFilter()
	{
		this.NameLabel.transform.localPosition = Vector3.zero;
		this.DisplayTimer = 0f;
		this.Speed = 0f;
		this.Anomaly.enabled = false;
		this.Binary.enabled = false;
		this.BlackHole3D.enabled = false;
		this.Computer.enabled = false;
		this.Distortion.enabled = false;
		this.FogSmoke.enabled = false;
		this.Ghost.enabled = false;
		this.Inverse.enabled = false;
		this.Matrix.enabled = false;
		this.Mirror3D.enabled = false;
		this.Myst.enabled = false;
		this.ScanScene.enabled = false;
		this.Shield.enabled = false;
		this.Snow.enabled = false;
		this.AAABlood.enabled = false;
		this.AAABloodOnScreen.enabled = false;
		this.AAABloodHit.enabled = false;
		this.AAABloodPlus.enabled = false;
		this.SuperComputer.enabled = false;
		this.SuperHexagon.enabled = false;
		this.WaterDrop.enabled = false;
		this.WaterDropPro.enabled = false;
		this.AlienVision.enabled = false;
		this.FXAA.enabled = false;
		this.Fog.enabled = false;
		this.Rain.enabled = false;
		this.RainPro.enabled = false;
		this.RainPro3D.enabled = false;
		this.Snow8bits.enabled = false;
		this.Blend.enabled = false;
		this.BlueScreen.enabled = false;
		this.Color.enabled = false;
		this.ColorBurn.enabled = false;
		this.ColorDodge.enabled = false;
		this.ColorKey.enabled = false;
		this.Darken.enabled = false;
		this.DarkerColor.enabled = false;
		this.Difference.enabled = false;
		this.Divide.enabled = false;
		this.Exclusion.enabled = false;
		this.GreenScreen.enabled = false;
		this.HardLight.enabled = false;
		this.HardMix.enabled = false;
		this.Blend2CameraHue.enabled = false;
		this.Lighten.enabled = false;
		this.LighterColor.enabled = false;
		this.LinearBurn.enabled = false;
		this.LinearDodge.enabled = false;
		this.LinearLight.enabled = false;
		this.Luminosity.enabled = false;
		this.Multiply.enabled = false;
		this.Overlay.enabled = false;
		this.PhotoshopFilters.enabled = false;
		this.PinLight.enabled = false;
		this.Saturation.enabled = false;
		this.Screen.enabled = false;
		this.SoftLight.enabled = false;
		this.SplitScreen.enabled = false;
		this.SplitScreen3D.enabled = false;
		this.Subtract.enabled = false;
		this.VividLight.enabled = false;
		this.Blizzard.enabled = false;
		this.Bloom.enabled = false;
		this.BlurHole.enabled = false;
		this.Blurry.enabled = false;
		this.Dithering2x2.enabled = false;
		this.DitherOffset.enabled = false;
		this.Focus.enabled = false;
		this.GaussianBlur.enabled = false;
		this.Movie.enabled = false;
		this.BlurNoise.enabled = false;
		this.Radial.enabled = false;
		this.RadialFast.enabled = false;
		this.Regular.enabled = false;
		this.Steam.enabled = false;
		this.TiltShift.enabled = false;
		this.TiltShiftHole.enabled = false;
		this.TiltShiftV.enabled = false;
		this.BrokenScreen.enabled = false;
		this.BrokenSimple.enabled = false;
		this.Spliter.enabled = false;
		this.ThermalVision.enabled = false;
		this.AdjustColorRGB.enabled = false;
		this.AdjustFullColors.enabled = false;
		this.AdjustPreFilters.enabled = false;
		this.BleachBypass.enabled = false;
		this.Brightness.enabled = false;
		this.DarkColor.enabled = false;
		this.HSV.enabled = false;
		this.HUERotate.enabled = false;
		this.NewPosterize.enabled = false;
		this.RgbClamp.enabled = false;
		this.Threshold.enabled = false;
		this.AdjustLevels.enabled = false;
		this.BrightContrastSaturation.enabled = false;
		this.ChromaticAberration.enabled = false;
		this.ChromaticPlus.enabled = false;
		this.Contrast.enabled = false;
		this.GrayScale.enabled = false;
		this.Invert.enabled = false;
		this.ColorNoise.enabled = false;
		this.ColorRGB.enabled = false;
		this.Sepia.enabled = false;
		this.Switching.enabled = false;
		this.YUV.enabled = false;
		this.Normal.enabled = false;
		this.Aspiration.enabled = false;
		this.BigFace.enabled = false;
		this.BlackHole.enabled = false;
		this.Dissipation.enabled = false;
		this.Dream.enabled = false;
		this.Dream2.enabled = false;
		this.FishEye.enabled = false;
		this.Flag.enabled = false;
		this.Flush.enabled = false;
		this.HalfSphere.enabled = false;
		this.Heat.enabled = false;
		this.Lens.enabled = false;
		this.DistortionNoise.enabled = false;
		this.ShockWave.enabled = false;
		this.ShockWaveManual.enabled = false;
		this.Twist.enabled = false;
		this.TwistSquare.enabled = false;
		this.DistortionWaterDrop.enabled = false;
		this.WaveHorizontal.enabled = false;
		this.BluePrint.enabled = false;
		this.CellShading.enabled = false;
		this.CellShading2.enabled = false;
		this.Comics.enabled = false;
		this.Crosshatch.enabled = false;
		this.Curve.enabled = false;
		this.EnhancedComics.enabled = false;
		this.Halftone.enabled = false;
		this.Laplacian.enabled = false;
		this.Lines.enabled = false;
		this.Manga.enabled = false;
		this.Manga2.enabled = false;
		this.Manga3.enabled = false;
		this.Manga4.enabled = false;
		this.Manga5.enabled = false;
		this.MangaColor.enabled = false;
		this.MangaFlash.enabled = false;
		this.MangaFlashWhite.enabled = false;
		this.MangaFlashColor.enabled = false;
		this.NewCellShading.enabled = false;
		this.Paper.enabled = false;
		this.Paper2.enabled = false;
		this.Paper3.enabled = false;
		this.Toon.enabled = false;
		this.BlackLine.enabled = false;
		this.Edgefilter.enabled = false;
		this.Golden.enabled = false;
		this.Neon.enabled = false;
		this.Sigmoid.enabled = false;
		this.Sobel.enabled = false;
		this.Rotation.enabled = false;
		this.SHOWFPS.enabled = false;
		this.EyesVision1.enabled = false;
		this.EyesVision2.enabled = false;
		this.ColorPerfection.enabled = false;
		this.Grain.enabled = false;
		this.FlyVision.enabled = false;
		this.FX8bits.enabled = false;
		this.FX8bitsgb.enabled = false;
		this.Ascii.enabled = false;
		this.DarkMatter.enabled = false;
		this.DigitalMatrix.enabled = false;
		this.DigitalMatrixDistortion.enabled = false;
		this.DotCircle.enabled = false;
		this.Drunk.enabled = false;
		this.Drunk2.enabled = false;
		this.EarthQuake.enabled = false;
		this.Funk.enabled = false;
		this.Glitch1.enabled = false;
		this.Glitch2.enabled = false;
		this.Glitch3.enabled = false;
		this.Grid.enabled = false;
		this.Hexagon.enabled = false;
		this.HexagonBlack.enabled = false;
		this.Hypno.enabled = false;
		this.InverChromiLum.enabled = false;
		this.FXMirror.enabled = false;
		this.FXPlasma.enabled = false;
		this.FXPsycho.enabled = false;
		this.Scan.enabled = false;
		this.Screens.enabled = false;
		this.Spot.enabled = false;
		this.superDot.enabled = false;
		this.ZebraColor.enabled = false;
		this.GlassesOn.enabled = false;
		this.GlassesOn2.enabled = false;
		this.GlassesOn3.enabled = false;
		this.GlassesOn4.enabled = false;
		this.GlassesOn5.enabled = false;
		this.GlassesOn6.enabled = false;
		this.Mozaic.enabled = false;
		this.Glow.enabled = false;
		this.GlowColor.enabled = false;
		this.Ansi.enabled = false;
		this.Desert.enabled = false;
		this.ElectricGradient.enabled = false;
		this.FireGradient.enabled = false;
		this.GradientsHue.enabled = false;
		this.NeonGradient.enabled = false;
		this.GradientsRainbow.enabled = false;
		this.Stripe.enabled = false;
		this.Tech.enabled = false;
		this.Therma.enabled = false;
		this.LightRainbow.enabled = false;
		this.LightRainbow2.enabled = false;
		this.Water.enabled = false;
		this.Water2.enabled = false;
		this.Lut.enabled = false;
		this.LutExtra.enabled = false;
		this.Mask.enabled = false;
		this.PlayWith.enabled = false;
		this.Plus.enabled = false;
		this.LutSimple.enabled = false;
		this.TestMode.enabled = false;
		this.NewGlitch1.enabled = false;
		this.NewGlitch2.enabled = false;
		this.NewGlitch3.enabled = false;
		this.NewGlitch4.enabled = false;
		this.NewGlitch5.enabled = false;
		this.NewGlitch6.enabled = false;
		this.NewGlitch7.enabled = false;
		this.NightVisionFX.enabled = false;
		this.NightVision4.enabled = false;
		this.TV.enabled = false;
		this.TV2.enabled = false;
		this.TV3.enabled = false;
		this.NightVision1.enabled = false;
		this.NightVision2.enabled = false;
		this.NightVision3.enabled = false;
		this.NightVision5.enabled = false;
		this.ThermaVision.enabled = false;
		this.Cutting1.enabled = false;
		this.Cutting2.enabled = false;
		this.DeepOilPaintHQ.enabled = false;
		this.Dot.enabled = false;
		this.OilPaint.enabled = false;
		this.OilPaintHQ.enabled = false;
		this.Sweater.enabled = false;
		this.Pixelisation.enabled = false;
		this.RainFX.enabled = false;
		this.RealVHS.enabled = false;
		this.Loading.enabled = false;
		this.Sharpen.enabled = false;
		this.Bubble.enabled = false;
		this.TV50.enabled = false;
		this.TV80.enabled = false;
		this.ARCADE.enabled = false;
		this.ARCADE2.enabled = false;
		this.ARCADEFast.enabled = false;
		this.Artefact.enabled = false;
		this.BrokenGlass.enabled = false;
		this.BrokenGlass2.enabled = false;
		this.Chromatical.enabled = false;
		this.Chromatical2.enabled = false;
		this.CompressionFX.enabled = false;
		this.Distorted.enabled = false;
		this.Horror.enabled = false;
		this.LED.enabled = false;
		this.MovieNoise.enabled = false;
		this.TVNoise.enabled = false;
		this.Old.enabled = false;
		this.OldMovie.enabled = false;
		this.OldMovie2.enabled = false;
		this.PlanetMars.enabled = false;
		this.Posterize.enabled = false;
		this.TVRgb.enabled = false;
		this.Tiles.enabled = false;
		this.Vcr.enabled = false;
		this.TVVHS.enabled = false;
		this.VHSRewind.enabled = false;
		this.Video3D.enabled = false;
		this.Videoflip.enabled = false;
		this.Vignetting.enabled = false;
		this.Vintage.enabled = false;
		this.WideScreenCircle.enabled = false;
		this.WideScreenHorizontal.enabled = false;
		this.WideScreenHV.enabled = false;
		this.WideScreenVertical.enabled = false;
		this.Tracking.enabled = false;
		this.Aura.enabled = false;
		this.AuraDistortion.enabled = false;
		this.VisionBlood.enabled = false;
		this.VisionBloodFast.enabled = false;
		this.Crystal.enabled = false;
		this.Drost.enabled = false;
		this.VisionHellBlood.enabled = false;
		this.VisionPlasma.enabled = false;
		this.VisionPsycho.enabled = false;
		this.VisionRainbow.enabled = false;
		this.SniperScore.enabled = false;
		this.Tunnel.enabled = false;
		this.Warp.enabled = false;
		this.Warp2.enabled = false;
		if (this.FilterID > this.FilterMax)
		{
			this.FilterID = 0;
		}
		if (this.FilterID < 0)
		{
			this.FilterID = this.FilterMax;
		}
		while (this.FilterSkips[this.FilterID])
		{
			if (Input.GetKeyDown("z"))
			{
				this.FilterID--;
			}
			else
			{
				this.FilterID++;
			}
		}
		this.NameLabel.text = string.Concat(new object[]
		{
			"#",
			this.FilterID,
			" - ",
			this.FilterNames[this.FilterID]
		});
		switch (this.FilterID)
		{
		case 1:
			this.Anomaly.enabled = true;
			return;
		case 2:
			this.Binary.enabled = true;
			return;
		case 3:
			this.BlackHole3D.enabled = true;
			return;
		case 4:
			this.Computer.enabled = true;
			return;
		case 5:
			this.Distortion.enabled = true;
			return;
		case 6:
			this.FogSmoke.enabled = true;
			return;
		case 7:
			this.Ghost.enabled = true;
			return;
		case 8:
			this.Inverse.enabled = true;
			return;
		case 9:
			this.Matrix.enabled = true;
			return;
		case 10:
			this.Mirror3D.enabled = true;
			return;
		case 11:
			this.Myst.enabled = true;
			return;
		case 12:
			this.ScanScene.enabled = true;
			return;
		case 13:
			this.Shield.enabled = true;
			return;
		case 14:
			this.Snow.enabled = true;
			return;
		case 15:
			this.AAABlood.enabled = true;
			return;
		case 16:
			this.AAABloodOnScreen.enabled = true;
			return;
		case 17:
			this.AAABloodHit.enabled = true;
			return;
		case 18:
			this.AAABloodPlus.enabled = true;
			return;
		case 19:
			this.SuperComputer.enabled = true;
			return;
		case 20:
			this.SuperHexagon.enabled = true;
			return;
		case 21:
			this.WaterDrop.enabled = true;
			return;
		case 22:
			this.WaterDropPro.enabled = true;
			return;
		case 23:
			this.AlienVision.enabled = true;
			return;
		case 24:
			this.FXAA.enabled = true;
			return;
		case 25:
			this.Fog.enabled = true;
			return;
		case 26:
			this.Rain.enabled = true;
			return;
		case 27:
			this.RainPro.enabled = true;
			return;
		case 28:
			this.RainPro3D.enabled = true;
			return;
		case 29:
			this.Snow8bits.enabled = true;
			return;
		case 30:
			this.Blend.enabled = true;
			return;
		case 31:
			this.BlueScreen.enabled = true;
			return;
		case 32:
			this.Color.enabled = true;
			return;
		case 33:
			this.ColorBurn.enabled = true;
			return;
		case 34:
			this.ColorDodge.enabled = true;
			return;
		case 35:
			this.ColorKey.enabled = true;
			return;
		case 36:
			this.Darken.enabled = true;
			return;
		case 37:
			this.DarkerColor.enabled = true;
			return;
		case 38:
			this.Difference.enabled = true;
			return;
		case 39:
			this.Divide.enabled = true;
			return;
		case 40:
			this.Exclusion.enabled = true;
			return;
		case 41:
			this.GreenScreen.enabled = true;
			return;
		case 42:
			this.HardLight.enabled = true;
			return;
		case 43:
			this.HardMix.enabled = true;
			return;
		case 44:
			this.Blend2CameraHue.enabled = true;
			return;
		case 45:
			this.Lighten.enabled = true;
			return;
		case 46:
			this.LighterColor.enabled = true;
			return;
		case 47:
			this.LinearBurn.enabled = true;
			return;
		case 48:
			this.LinearDodge.enabled = true;
			return;
		case 49:
			this.LinearLight.enabled = true;
			return;
		case 50:
			this.Luminosity.enabled = true;
			return;
		case 51:
			this.Multiply.enabled = true;
			return;
		case 52:
			this.Overlay.enabled = true;
			return;
		case 53:
			this.PhotoshopFilters.enabled = true;
			return;
		case 54:
			this.PinLight.enabled = true;
			return;
		case 55:
			this.Saturation.enabled = true;
			return;
		case 56:
			this.Screen.enabled = true;
			return;
		case 57:
			this.SoftLight.enabled = true;
			return;
		case 58:
			this.SplitScreen.enabled = true;
			return;
		case 59:
			this.SplitScreen3D.enabled = true;
			return;
		case 60:
			this.Subtract.enabled = true;
			return;
		case 61:
			this.VividLight.enabled = true;
			return;
		case 62:
			this.Blizzard.enabled = true;
			return;
		case 63:
			this.Bloom.enabled = true;
			return;
		case 64:
			this.BlurHole.enabled = true;
			return;
		case 65:
			this.Blurry.enabled = true;
			return;
		case 66:
			this.Dithering2x2.enabled = true;
			return;
		case 67:
			this.DitherOffset.enabled = true;
			return;
		case 68:
			this.Focus.enabled = true;
			return;
		case 69:
			this.GaussianBlur.enabled = true;
			return;
		case 70:
			this.Movie.enabled = true;
			return;
		case 71:
			this.BlurNoise.enabled = true;
			return;
		case 72:
			this.Radial.enabled = true;
			return;
		case 73:
			this.RadialFast.enabled = true;
			return;
		case 74:
			this.Regular.enabled = true;
			return;
		case 75:
			this.Steam.enabled = true;
			return;
		case 76:
			this.TiltShift.enabled = true;
			return;
		case 77:
			this.TiltShiftHole.enabled = true;
			return;
		case 78:
			this.TiltShiftV.enabled = true;
			return;
		case 79:
			this.BrokenScreen.enabled = true;
			return;
		case 80:
			this.BrokenSimple.enabled = true;
			return;
		case 81:
			this.Spliter.enabled = true;
			return;
		case 82:
			this.ThermalVision.enabled = true;
			return;
		case 83:
			this.AdjustColorRGB.enabled = true;
			return;
		case 84:
			this.AdjustFullColors.enabled = true;
			return;
		case 85:
			this.AdjustPreFilters.enabled = true;
			return;
		case 86:
			this.BleachBypass.enabled = true;
			return;
		case 87:
			this.Brightness.enabled = true;
			return;
		case 88:
			this.DarkColor.enabled = true;
			return;
		case 89:
			this.HSV.enabled = true;
			return;
		case 90:
			this.HUERotate.enabled = true;
			return;
		case 91:
			this.NewPosterize.enabled = true;
			return;
		case 92:
			this.RgbClamp.enabled = true;
			return;
		case 93:
			this.Threshold.enabled = true;
			return;
		case 94:
			this.AdjustLevels.enabled = true;
			return;
		case 95:
			this.BrightContrastSaturation.enabled = true;
			return;
		case 96:
			this.ChromaticAberration.enabled = true;
			return;
		case 97:
			this.ChromaticPlus.enabled = true;
			return;
		case 98:
			this.Contrast.enabled = true;
			return;
		case 99:
			this.GrayScale.enabled = true;
			return;
		case 100:
			this.Invert.enabled = true;
			return;
		case 101:
			this.ColorNoise.enabled = true;
			return;
		case 102:
			this.ColorRGB.enabled = true;
			return;
		case 103:
			this.Sepia.enabled = true;
			return;
		case 104:
			this.Switching.enabled = true;
			return;
		case 105:
			this.YUV.enabled = true;
			return;
		case 106:
			this.Normal.enabled = true;
			return;
		case 107:
			this.Aspiration.enabled = true;
			return;
		case 108:
			this.BigFace.enabled = true;
			return;
		case 109:
			this.BlackHole.enabled = true;
			return;
		case 110:
			this.Dissipation.enabled = true;
			return;
		case 111:
			this.Dream.enabled = true;
			return;
		case 112:
			this.Dream2.enabled = true;
			return;
		case 113:
			this.FishEye.enabled = true;
			return;
		case 114:
			this.Flag.enabled = true;
			return;
		case 115:
			this.Flush.enabled = true;
			return;
		case 116:
			this.HalfSphere.enabled = true;
			return;
		case 117:
			this.Heat.enabled = true;
			return;
		case 118:
			this.Lens.enabled = true;
			return;
		case 119:
			this.DistortionNoise.enabled = true;
			return;
		case 120:
			this.ShockWave.enabled = true;
			return;
		case 121:
			this.ShockWaveManual.enabled = true;
			return;
		case 122:
			this.Twist.enabled = true;
			return;
		case 123:
			this.TwistSquare.enabled = true;
			return;
		case 124:
			this.DistortionWaterDrop.enabled = true;
			return;
		case 125:
			this.WaveHorizontal.enabled = true;
			return;
		case 126:
			this.BluePrint.enabled = true;
			return;
		case 127:
			this.CellShading.enabled = true;
			return;
		case 128:
			this.CellShading2.enabled = true;
			return;
		case 129:
			this.Comics.enabled = true;
			return;
		case 130:
			this.Crosshatch.enabled = true;
			return;
		case 131:
			this.Curve.enabled = true;
			return;
		case 132:
			this.EnhancedComics.enabled = true;
			return;
		case 133:
			this.Halftone.enabled = true;
			return;
		case 134:
			this.Laplacian.enabled = true;
			return;
		case 135:
			this.Lines.enabled = true;
			return;
		case 136:
			this.Manga.enabled = true;
			return;
		case 137:
			this.Manga2.enabled = true;
			return;
		case 138:
			this.Manga3.enabled = true;
			return;
		case 139:
			this.Manga4.enabled = true;
			return;
		case 140:
			this.Manga5.enabled = true;
			return;
		case 141:
			this.MangaColor.enabled = true;
			return;
		case 142:
			this.MangaFlash.enabled = true;
			return;
		case 143:
			this.MangaFlashWhite.enabled = true;
			return;
		case 144:
			this.MangaFlashColor.enabled = true;
			return;
		case 145:
			this.NewCellShading.enabled = true;
			return;
		case 146:
			this.Paper.enabled = true;
			return;
		case 147:
			this.Paper2.enabled = true;
			return;
		case 148:
			this.Paper3.enabled = true;
			return;
		case 149:
			this.Toon.enabled = true;
			return;
		case 150:
			this.BlackLine.enabled = true;
			return;
		case 151:
			this.Edgefilter.enabled = true;
			return;
		case 152:
			this.Golden.enabled = true;
			return;
		case 153:
			this.Neon.enabled = true;
			return;
		case 154:
			this.Sigmoid.enabled = true;
			return;
		case 155:
			this.Sobel.enabled = true;
			return;
		case 156:
			this.Rotation.enabled = true;
			return;
		case 157:
			this.SHOWFPS.enabled = true;
			return;
		case 158:
			this.EyesVision1.enabled = true;
			return;
		case 159:
			this.EyesVision2.enabled = true;
			return;
		case 160:
			this.ColorPerfection.enabled = true;
			return;
		case 161:
			this.Grain.enabled = true;
			return;
		case 162:
			this.FlyVision.enabled = true;
			return;
		case 163:
			this.FX8bits.enabled = true;
			return;
		case 164:
			this.FX8bitsgb.enabled = true;
			return;
		case 165:
			this.Ascii.enabled = true;
			return;
		case 166:
			this.DarkMatter.enabled = true;
			return;
		case 167:
			this.DigitalMatrix.enabled = true;
			return;
		case 168:
			this.DigitalMatrixDistortion.enabled = true;
			return;
		case 169:
			this.DotCircle.enabled = true;
			return;
		case 170:
			this.Drunk.enabled = true;
			return;
		case 171:
			this.Drunk2.enabled = true;
			return;
		case 172:
			this.EarthQuake.enabled = true;
			return;
		case 173:
			this.Funk.enabled = true;
			return;
		case 174:
			this.Glitch1.enabled = true;
			return;
		case 175:
			this.Glitch2.enabled = true;
			return;
		case 176:
			this.Glitch3.enabled = true;
			return;
		case 177:
			this.Grid.enabled = true;
			return;
		case 178:
			this.Hexagon.enabled = true;
			return;
		case 179:
			this.HexagonBlack.enabled = true;
			return;
		case 180:
			this.Hypno.enabled = true;
			return;
		case 181:
			this.InverChromiLum.enabled = true;
			return;
		case 182:
			this.FXMirror.enabled = true;
			return;
		case 183:
			this.FXPlasma.enabled = true;
			return;
		case 184:
			this.FXPsycho.enabled = true;
			return;
		case 185:
			this.Scan.enabled = true;
			return;
		case 186:
			this.Screens.enabled = true;
			return;
		case 187:
			this.Spot.enabled = true;
			return;
		case 188:
			this.superDot.enabled = true;
			return;
		case 189:
			this.ZebraColor.enabled = true;
			return;
		case 190:
			this.GlassesOn.enabled = true;
			return;
		case 191:
			this.GlassesOn2.enabled = true;
			return;
		case 192:
			this.GlassesOn3.enabled = true;
			return;
		case 193:
			this.GlassesOn4.enabled = true;
			return;
		case 194:
			this.GlassesOn5.enabled = true;
			return;
		case 195:
			this.GlassesOn6.enabled = true;
			return;
		case 196:
			this.Mozaic.enabled = true;
			return;
		case 197:
			this.Glow.enabled = true;
			return;
		case 198:
			this.GlowColor.enabled = true;
			return;
		case 199:
			this.Ansi.enabled = true;
			return;
		case 200:
			this.Desert.enabled = true;
			return;
		case 201:
			this.ElectricGradient.enabled = true;
			return;
		case 202:
			this.FireGradient.enabled = true;
			return;
		case 203:
			this.GradientsHue.enabled = true;
			return;
		case 204:
			this.NeonGradient.enabled = true;
			return;
		case 205:
			this.GradientsRainbow.enabled = true;
			return;
		case 206:
			this.Stripe.enabled = true;
			return;
		case 207:
			this.Tech.enabled = true;
			return;
		case 208:
			this.Therma.enabled = true;
			return;
		case 209:
			this.LightRainbow.enabled = true;
			return;
		case 210:
			this.LightRainbow2.enabled = true;
			return;
		case 211:
			this.Water.enabled = true;
			return;
		case 212:
			this.Water2.enabled = true;
			return;
		case 213:
			this.Lut.enabled = true;
			return;
		case 214:
			this.LutExtra.enabled = true;
			return;
		case 215:
			this.Mask.enabled = true;
			return;
		case 216:
			this.PlayWith.enabled = true;
			return;
		case 217:
			this.Plus.enabled = true;
			return;
		case 218:
			this.LutSimple.enabled = true;
			return;
		case 219:
			this.TestMode.enabled = true;
			return;
		case 220:
			this.NewGlitch1.enabled = true;
			return;
		case 221:
			this.NewGlitch2.enabled = true;
			return;
		case 222:
			this.NewGlitch3.enabled = true;
			return;
		case 223:
			this.NewGlitch4.enabled = true;
			return;
		case 224:
			this.NewGlitch5.enabled = true;
			return;
		case 225:
			this.NewGlitch6.enabled = true;
			return;
		case 226:
			this.NewGlitch7.enabled = true;
			return;
		case 227:
			this.NightVisionFX.enabled = true;
			return;
		case 228:
			this.NightVision4.enabled = true;
			return;
		case 229:
			this.TV.enabled = true;
			return;
		case 230:
			this.TV2.enabled = true;
			return;
		case 231:
			this.TV3.enabled = true;
			return;
		case 232:
			this.NightVision1.enabled = true;
			return;
		case 233:
			this.NightVision2.enabled = true;
			return;
		case 234:
			this.NightVision3.enabled = true;
			return;
		case 235:
			this.NightVision5.enabled = true;
			return;
		case 236:
			this.ThermaVision.enabled = true;
			return;
		case 237:
			this.Cutting1.enabled = true;
			return;
		case 238:
			this.Cutting2.enabled = true;
			return;
		case 239:
			this.DeepOilPaintHQ.enabled = true;
			return;
		case 240:
			this.Dot.enabled = true;
			return;
		case 241:
			this.OilPaint.enabled = true;
			return;
		case 242:
			this.OilPaintHQ.enabled = true;
			return;
		case 243:
			this.Sweater.enabled = true;
			return;
		case 244:
			this.Pixelisation.enabled = true;
			return;
		case 245:
			this.RainFX.enabled = true;
			return;
		case 246:
			this.RealVHS.enabled = true;
			return;
		case 247:
			this.Loading.enabled = true;
			return;
		case 248:
			this.Sharpen.enabled = true;
			return;
		case 249:
			this.Bubble.enabled = true;
			return;
		case 250:
			this.TV50.enabled = true;
			return;
		case 251:
			this.TV80.enabled = true;
			return;
		case 252:
			this.ARCADE.enabled = true;
			return;
		case 253:
			this.ARCADE2.enabled = true;
			return;
		case 254:
			this.ARCADEFast.enabled = true;
			return;
		case 255:
			this.Artefact.enabled = true;
			return;
		case 256:
			this.BrokenGlass.enabled = true;
			return;
		case 257:
			this.BrokenGlass2.enabled = true;
			return;
		case 258:
			this.Chromatical.enabled = true;
			return;
		case 259:
			this.Chromatical2.enabled = true;
			return;
		case 260:
			this.CompressionFX.enabled = true;
			return;
		case 261:
			this.Distorted.enabled = true;
			return;
		case 262:
			this.Horror.enabled = true;
			return;
		case 263:
			this.LED.enabled = true;
			return;
		case 264:
			this.MovieNoise.enabled = true;
			return;
		case 265:
			this.TVNoise.enabled = true;
			return;
		case 266:
			this.Old.enabled = true;
			return;
		case 267:
			this.OldMovie.enabled = true;
			return;
		case 268:
			this.OldMovie2.enabled = true;
			return;
		case 269:
			this.PlanetMars.enabled = true;
			return;
		case 270:
			this.Posterize.enabled = true;
			return;
		case 271:
			this.TVRgb.enabled = true;
			return;
		case 272:
			this.Tiles.enabled = true;
			return;
		case 273:
			this.Vcr.enabled = true;
			return;
		case 274:
			this.TVVHS.enabled = true;
			return;
		case 275:
			this.VHSRewind.enabled = true;
			return;
		case 276:
			this.Video3D.enabled = true;
			return;
		case 277:
			this.Videoflip.enabled = true;
			return;
		case 278:
			this.Vignetting.enabled = true;
			return;
		case 279:
			this.Vintage.enabled = true;
			return;
		case 280:
			this.WideScreenCircle.enabled = true;
			return;
		case 281:
			this.WideScreenHorizontal.enabled = true;
			return;
		case 282:
			this.WideScreenHV.enabled = true;
			return;
		case 283:
			this.WideScreenVertical.enabled = true;
			return;
		case 284:
			this.Tracking.enabled = true;
			return;
		case 285:
			this.Aura.enabled = true;
			return;
		case 286:
			this.AuraDistortion.enabled = true;
			return;
		case 287:
			this.VisionBlood.enabled = true;
			return;
		case 288:
			this.VisionBloodFast.enabled = true;
			return;
		case 289:
			this.Crystal.enabled = true;
			return;
		case 290:
			this.Drost.enabled = true;
			return;
		case 291:
			this.VisionHellBlood.enabled = true;
			return;
		case 292:
			this.VisionPlasma.enabled = true;
			return;
		case 293:
			this.VisionPsycho.enabled = true;
			return;
		case 294:
			this.VisionRainbow.enabled = true;
			return;
		case 295:
			this.SniperScore.enabled = true;
			return;
		case 296:
			this.Tunnel.enabled = true;
			return;
		case 297:
			this.Warp.enabled = true;
			return;
		case 298:
			this.Warp2.enabled = true;
			return;
		default:
			return;
		}
	}

	// Token: 0x04000B6A RID: 2922
	private CameraFilterPack_3D_Anomaly Anomaly;

	// Token: 0x04000B6B RID: 2923
	private CameraFilterPack_3D_Binary Binary;

	// Token: 0x04000B6C RID: 2924
	private CameraFilterPack_3D_BlackHole BlackHole3D;

	// Token: 0x04000B6D RID: 2925
	private CameraFilterPack_3D_Computer Computer;

	// Token: 0x04000B6E RID: 2926
	private CameraFilterPack_3D_Distortion Distortion;

	// Token: 0x04000B6F RID: 2927
	private CameraFilterPack_3D_Fog_Smoke FogSmoke;

	// Token: 0x04000B70 RID: 2928
	private CameraFilterPack_3D_Ghost Ghost;

	// Token: 0x04000B71 RID: 2929
	private CameraFilterPack_3D_Inverse Inverse;

	// Token: 0x04000B72 RID: 2930
	private CameraFilterPack_3D_Matrix Matrix;

	// Token: 0x04000B73 RID: 2931
	private CameraFilterPack_3D_Mirror Mirror3D;

	// Token: 0x04000B74 RID: 2932
	private CameraFilterPack_3D_Myst Myst;

	// Token: 0x04000B75 RID: 2933
	private CameraFilterPack_3D_Scan_Scene ScanScene;

	// Token: 0x04000B76 RID: 2934
	private CameraFilterPack_3D_Shield Shield;

	// Token: 0x04000B77 RID: 2935
	private CameraFilterPack_3D_Snow Snow;

	// Token: 0x04000B78 RID: 2936
	private CameraFilterPack_AAA_Blood AAABlood;

	// Token: 0x04000B79 RID: 2937
	private CameraFilterPack_AAA_BloodOnScreen AAABloodOnScreen;

	// Token: 0x04000B7A RID: 2938
	private CameraFilterPack_AAA_Blood_Hit AAABloodHit;

	// Token: 0x04000B7B RID: 2939
	private CameraFilterPack_AAA_Blood_Plus AAABloodPlus;

	// Token: 0x04000B7C RID: 2940
	private CameraFilterPack_AAA_SuperComputer SuperComputer;

	// Token: 0x04000B7D RID: 2941
	private CameraFilterPack_AAA_SuperHexagon SuperHexagon;

	// Token: 0x04000B7E RID: 2942
	private CameraFilterPack_AAA_WaterDrop WaterDrop;

	// Token: 0x04000B7F RID: 2943
	private CameraFilterPack_AAA_WaterDropPro WaterDropPro;

	// Token: 0x04000B80 RID: 2944
	private CameraFilterPack_Alien_Vision AlienVision;

	// Token: 0x04000B81 RID: 2945
	private CameraFilterPack_Antialiasing_FXAA FXAA;

	// Token: 0x04000B82 RID: 2946
	private CameraFilterPack_Atmosphere_Fog Fog;

	// Token: 0x04000B83 RID: 2947
	private CameraFilterPack_Atmosphere_Rain Rain;

	// Token: 0x04000B84 RID: 2948
	private CameraFilterPack_Atmosphere_Rain_Pro RainPro;

	// Token: 0x04000B85 RID: 2949
	private CameraFilterPack_Atmosphere_Rain_Pro_3D RainPro3D;

	// Token: 0x04000B86 RID: 2950
	private CameraFilterPack_Atmosphere_Snow_8bits Snow8bits;

	// Token: 0x04000B87 RID: 2951
	private CameraFilterPack_Blend2Camera_Blend Blend;

	// Token: 0x04000B88 RID: 2952
	private CameraFilterPack_Blend2Camera_BlueScreen BlueScreen;

	// Token: 0x04000B89 RID: 2953
	private CameraFilterPack_Blend2Camera_Color Color;

	// Token: 0x04000B8A RID: 2954
	private CameraFilterPack_Blend2Camera_ColorBurn ColorBurn;

	// Token: 0x04000B8B RID: 2955
	private CameraFilterPack_Blend2Camera_ColorDodge ColorDodge;

	// Token: 0x04000B8C RID: 2956
	private CameraFilterPack_Blend2Camera_ColorKey ColorKey;

	// Token: 0x04000B8D RID: 2957
	private CameraFilterPack_Blend2Camera_Darken Darken;

	// Token: 0x04000B8E RID: 2958
	private CameraFilterPack_Blend2Camera_DarkerColor DarkerColor;

	// Token: 0x04000B8F RID: 2959
	private CameraFilterPack_Blend2Camera_Difference Difference;

	// Token: 0x04000B90 RID: 2960
	private CameraFilterPack_Blend2Camera_Divide Divide;

	// Token: 0x04000B91 RID: 2961
	private CameraFilterPack_Blend2Camera_Exclusion Exclusion;

	// Token: 0x04000B92 RID: 2962
	private CameraFilterPack_Blend2Camera_GreenScreen GreenScreen;

	// Token: 0x04000B93 RID: 2963
	private CameraFilterPack_Blend2Camera_HardLight HardLight;

	// Token: 0x04000B94 RID: 2964
	private CameraFilterPack_Blend2Camera_HardMix HardMix;

	// Token: 0x04000B95 RID: 2965
	private CameraFilterPack_Blend2Camera_Hue Blend2CameraHue;

	// Token: 0x04000B96 RID: 2966
	private CameraFilterPack_Blend2Camera_Lighten Lighten;

	// Token: 0x04000B97 RID: 2967
	private CameraFilterPack_Blend2Camera_LighterColor LighterColor;

	// Token: 0x04000B98 RID: 2968
	private CameraFilterPack_Blend2Camera_LinearBurn LinearBurn;

	// Token: 0x04000B99 RID: 2969
	private CameraFilterPack_Blend2Camera_LinearDodge LinearDodge;

	// Token: 0x04000B9A RID: 2970
	private CameraFilterPack_Blend2Camera_LinearLight LinearLight;

	// Token: 0x04000B9B RID: 2971
	private CameraFilterPack_Blend2Camera_Luminosity Luminosity;

	// Token: 0x04000B9C RID: 2972
	private CameraFilterPack_Blend2Camera_Multiply Multiply;

	// Token: 0x04000B9D RID: 2973
	private CameraFilterPack_Blend2Camera_Overlay Overlay;

	// Token: 0x04000B9E RID: 2974
	private CameraFilterPack_Blend2Camera_PhotoshopFilters PhotoshopFilters;

	// Token: 0x04000B9F RID: 2975
	private CameraFilterPack_Blend2Camera_PinLight PinLight;

	// Token: 0x04000BA0 RID: 2976
	private CameraFilterPack_Blend2Camera_Saturation Saturation;

	// Token: 0x04000BA1 RID: 2977
	private CameraFilterPack_Blend2Camera_Screen Screen;

	// Token: 0x04000BA2 RID: 2978
	private CameraFilterPack_Blend2Camera_SoftLight SoftLight;

	// Token: 0x04000BA3 RID: 2979
	private CameraFilterPack_Blend2Camera_SplitScreen SplitScreen;

	// Token: 0x04000BA4 RID: 2980
	private CameraFilterPack_Blend2Camera_SplitScreen3D SplitScreen3D;

	// Token: 0x04000BA5 RID: 2981
	private CameraFilterPack_Blend2Camera_Subtract Subtract;

	// Token: 0x04000BA6 RID: 2982
	private CameraFilterPack_Blend2Camera_VividLight VividLight;

	// Token: 0x04000BA7 RID: 2983
	private CameraFilterPack_Blizzard Blizzard;

	// Token: 0x04000BA8 RID: 2984
	private CameraFilterPack_Blur_Bloom Bloom;

	// Token: 0x04000BA9 RID: 2985
	private CameraFilterPack_Blur_BlurHole BlurHole;

	// Token: 0x04000BAA RID: 2986
	private CameraFilterPack_Blur_Blurry Blurry;

	// Token: 0x04000BAB RID: 2987
	private CameraFilterPack_Blur_Dithering2x2 Dithering2x2;

	// Token: 0x04000BAC RID: 2988
	private CameraFilterPack_Blur_DitherOffset DitherOffset;

	// Token: 0x04000BAD RID: 2989
	private CameraFilterPack_Blur_Focus Focus;

	// Token: 0x04000BAE RID: 2990
	private CameraFilterPack_Blur_GaussianBlur GaussianBlur;

	// Token: 0x04000BAF RID: 2991
	private CameraFilterPack_Blur_Movie Movie;

	// Token: 0x04000BB0 RID: 2992
	private CameraFilterPack_Blur_Noise BlurNoise;

	// Token: 0x04000BB1 RID: 2993
	private CameraFilterPack_Blur_Radial Radial;

	// Token: 0x04000BB2 RID: 2994
	private CameraFilterPack_Blur_Radial_Fast RadialFast;

	// Token: 0x04000BB3 RID: 2995
	private CameraFilterPack_Blur_Regular Regular;

	// Token: 0x04000BB4 RID: 2996
	private CameraFilterPack_Blur_Steam Steam;

	// Token: 0x04000BB5 RID: 2997
	private CameraFilterPack_Blur_Tilt_Shift TiltShift;

	// Token: 0x04000BB6 RID: 2998
	private CameraFilterPack_Blur_Tilt_Shift_Hole TiltShiftHole;

	// Token: 0x04000BB7 RID: 2999
	private CameraFilterPack_Blur_Tilt_Shift_V TiltShiftV;

	// Token: 0x04000BB8 RID: 3000
	private CameraFilterPack_Broken_Screen BrokenScreen;

	// Token: 0x04000BB9 RID: 3001
	private CameraFilterPack_Broken_Simple BrokenSimple;

	// Token: 0x04000BBA RID: 3002
	private CameraFilterPack_Broken_Spliter Spliter;

	// Token: 0x04000BBB RID: 3003
	private CameraFilterPack_Classic_ThermalVision ThermalVision;

	// Token: 0x04000BBC RID: 3004
	private CameraFilterPack_Colors_Adjust_ColorRGB AdjustColorRGB;

	// Token: 0x04000BBD RID: 3005
	private CameraFilterPack_Colors_Adjust_FullColors AdjustFullColors;

	// Token: 0x04000BBE RID: 3006
	private CameraFilterPack_Colors_Adjust_PreFilters AdjustPreFilters;

	// Token: 0x04000BBF RID: 3007
	private CameraFilterPack_Colors_BleachBypass BleachBypass;

	// Token: 0x04000BC0 RID: 3008
	private CameraFilterPack_Colors_Brightness Brightness;

	// Token: 0x04000BC1 RID: 3009
	private CameraFilterPack_Colors_DarkColor DarkColor;

	// Token: 0x04000BC2 RID: 3010
	private CameraFilterPack_Colors_HSV HSV;

	// Token: 0x04000BC3 RID: 3011
	private CameraFilterPack_Colors_HUE_Rotate HUERotate;

	// Token: 0x04000BC4 RID: 3012
	private CameraFilterPack_Colors_NewPosterize NewPosterize;

	// Token: 0x04000BC5 RID: 3013
	private CameraFilterPack_Colors_RgbClamp RgbClamp;

	// Token: 0x04000BC6 RID: 3014
	private CameraFilterPack_Colors_Threshold Threshold;

	// Token: 0x04000BC7 RID: 3015
	private CameraFilterPack_Color_Adjust_Levels AdjustLevels;

	// Token: 0x04000BC8 RID: 3016
	private CameraFilterPack_Color_BrightContrastSaturation BrightContrastSaturation;

	// Token: 0x04000BC9 RID: 3017
	private CameraFilterPack_Color_Chromatic_Aberration ChromaticAberration;

	// Token: 0x04000BCA RID: 3018
	private CameraFilterPack_Color_Chromatic_Plus ChromaticPlus;

	// Token: 0x04000BCB RID: 3019
	private CameraFilterPack_Color_Contrast Contrast;

	// Token: 0x04000BCC RID: 3020
	private CameraFilterPack_Color_GrayScale GrayScale;

	// Token: 0x04000BCD RID: 3021
	private CameraFilterPack_Color_Invert Invert;

	// Token: 0x04000BCE RID: 3022
	private CameraFilterPack_Color_Noise ColorNoise;

	// Token: 0x04000BCF RID: 3023
	private CameraFilterPack_Color_RGB ColorRGB;

	// Token: 0x04000BD0 RID: 3024
	private CameraFilterPack_Color_Sepia Sepia;

	// Token: 0x04000BD1 RID: 3025
	private CameraFilterPack_Color_Switching Switching;

	// Token: 0x04000BD2 RID: 3026
	private CameraFilterPack_Color_YUV YUV;

	// Token: 0x04000BD3 RID: 3027
	private CameraFilterPack_Convert_Normal Normal;

	// Token: 0x04000BD4 RID: 3028
	private CameraFilterPack_Distortion_Aspiration Aspiration;

	// Token: 0x04000BD5 RID: 3029
	private CameraFilterPack_Distortion_BigFace BigFace;

	// Token: 0x04000BD6 RID: 3030
	private CameraFilterPack_Distortion_BlackHole BlackHole;

	// Token: 0x04000BD7 RID: 3031
	private CameraFilterPack_Distortion_Dissipation Dissipation;

	// Token: 0x04000BD8 RID: 3032
	private CameraFilterPack_Distortion_Dream Dream;

	// Token: 0x04000BD9 RID: 3033
	private CameraFilterPack_Distortion_Dream2 Dream2;

	// Token: 0x04000BDA RID: 3034
	private CameraFilterPack_Distortion_FishEye FishEye;

	// Token: 0x04000BDB RID: 3035
	private CameraFilterPack_Distortion_Flag Flag;

	// Token: 0x04000BDC RID: 3036
	private CameraFilterPack_Distortion_Flush Flush;

	// Token: 0x04000BDD RID: 3037
	private CameraFilterPack_Distortion_Half_Sphere HalfSphere;

	// Token: 0x04000BDE RID: 3038
	private CameraFilterPack_Distortion_Heat Heat;

	// Token: 0x04000BDF RID: 3039
	private CameraFilterPack_Distortion_Lens Lens;

	// Token: 0x04000BE0 RID: 3040
	private CameraFilterPack_Distortion_Noise DistortionNoise;

	// Token: 0x04000BE1 RID: 3041
	private CameraFilterPack_Distortion_ShockWave ShockWave;

	// Token: 0x04000BE2 RID: 3042
	private CameraFilterPack_Distortion_ShockWaveManual ShockWaveManual;

	// Token: 0x04000BE3 RID: 3043
	private CameraFilterPack_Distortion_Twist Twist;

	// Token: 0x04000BE4 RID: 3044
	private CameraFilterPack_Distortion_Twist_Square TwistSquare;

	// Token: 0x04000BE5 RID: 3045
	private CameraFilterPack_Distortion_Water_Drop DistortionWaterDrop;

	// Token: 0x04000BE6 RID: 3046
	private CameraFilterPack_Distortion_Wave_Horizontal WaveHorizontal;

	// Token: 0x04000BE7 RID: 3047
	private CameraFilterPack_Drawing_BluePrint BluePrint;

	// Token: 0x04000BE8 RID: 3048
	private CameraFilterPack_Drawing_CellShading CellShading;

	// Token: 0x04000BE9 RID: 3049
	private CameraFilterPack_Drawing_CellShading2 CellShading2;

	// Token: 0x04000BEA RID: 3050
	private CameraFilterPack_Drawing_Comics Comics;

	// Token: 0x04000BEB RID: 3051
	private CameraFilterPack_Drawing_Crosshatch Crosshatch;

	// Token: 0x04000BEC RID: 3052
	private CameraFilterPack_Drawing_Curve Curve;

	// Token: 0x04000BED RID: 3053
	private CameraFilterPack_Drawing_EnhancedComics EnhancedComics;

	// Token: 0x04000BEE RID: 3054
	private CameraFilterPack_Drawing_Halftone Halftone;

	// Token: 0x04000BEF RID: 3055
	private CameraFilterPack_Drawing_Laplacian Laplacian;

	// Token: 0x04000BF0 RID: 3056
	private CameraFilterPack_Drawing_Lines Lines;

	// Token: 0x04000BF1 RID: 3057
	private CameraFilterPack_Drawing_Manga Manga;

	// Token: 0x04000BF2 RID: 3058
	private CameraFilterPack_Drawing_Manga2 Manga2;

	// Token: 0x04000BF3 RID: 3059
	private CameraFilterPack_Drawing_Manga3 Manga3;

	// Token: 0x04000BF4 RID: 3060
	private CameraFilterPack_Drawing_Manga4 Manga4;

	// Token: 0x04000BF5 RID: 3061
	private CameraFilterPack_Drawing_Manga5 Manga5;

	// Token: 0x04000BF6 RID: 3062
	private CameraFilterPack_Drawing_Manga_Color MangaColor;

	// Token: 0x04000BF7 RID: 3063
	private CameraFilterPack_Drawing_Manga_Flash MangaFlash;

	// Token: 0x04000BF8 RID: 3064
	private CameraFilterPack_Drawing_Manga_FlashWhite MangaFlashWhite;

	// Token: 0x04000BF9 RID: 3065
	private CameraFilterPack_Drawing_Manga_Flash_Color MangaFlashColor;

	// Token: 0x04000BFA RID: 3066
	private CameraFilterPack_Drawing_NewCellShading NewCellShading;

	// Token: 0x04000BFB RID: 3067
	private CameraFilterPack_Drawing_Paper Paper;

	// Token: 0x04000BFC RID: 3068
	private CameraFilterPack_Drawing_Paper2 Paper2;

	// Token: 0x04000BFD RID: 3069
	private CameraFilterPack_Drawing_Paper3 Paper3;

	// Token: 0x04000BFE RID: 3070
	private CameraFilterPack_Drawing_Toon Toon;

	// Token: 0x04000BFF RID: 3071
	private CameraFilterPack_Edge_BlackLine BlackLine;

	// Token: 0x04000C00 RID: 3072
	private CameraFilterPack_Edge_Edge_filter Edgefilter;

	// Token: 0x04000C01 RID: 3073
	private CameraFilterPack_Edge_Golden Golden;

	// Token: 0x04000C02 RID: 3074
	private CameraFilterPack_Edge_Neon Neon;

	// Token: 0x04000C03 RID: 3075
	private CameraFilterPack_Edge_Sigmoid Sigmoid;

	// Token: 0x04000C04 RID: 3076
	private CameraFilterPack_Edge_Sobel Sobel;

	// Token: 0x04000C05 RID: 3077
	private CameraFilterPack_EXTRA_Rotation Rotation;

	// Token: 0x04000C06 RID: 3078
	private CameraFilterPack_EXTRA_SHOWFPS SHOWFPS;

	// Token: 0x04000C07 RID: 3079
	private CameraFilterPack_EyesVision_1 EyesVision1;

	// Token: 0x04000C08 RID: 3080
	private CameraFilterPack_EyesVision_2 EyesVision2;

	// Token: 0x04000C09 RID: 3081
	private CameraFilterPack_Film_ColorPerfection ColorPerfection;

	// Token: 0x04000C0A RID: 3082
	private CameraFilterPack_Film_Grain Grain;

	// Token: 0x04000C0B RID: 3083
	private CameraFilterPack_Fly_Vision FlyVision;

	// Token: 0x04000C0C RID: 3084
	private CameraFilterPack_FX_8bits FX8bits;

	// Token: 0x04000C0D RID: 3085
	private CameraFilterPack_FX_8bits_gb FX8bitsgb;

	// Token: 0x04000C0E RID: 3086
	private CameraFilterPack_FX_Ascii Ascii;

	// Token: 0x04000C0F RID: 3087
	private CameraFilterPack_FX_DarkMatter DarkMatter;

	// Token: 0x04000C10 RID: 3088
	private CameraFilterPack_FX_DigitalMatrix DigitalMatrix;

	// Token: 0x04000C11 RID: 3089
	private CameraFilterPack_FX_DigitalMatrixDistortion DigitalMatrixDistortion;

	// Token: 0x04000C12 RID: 3090
	private CameraFilterPack_FX_Dot_Circle DotCircle;

	// Token: 0x04000C13 RID: 3091
	private CameraFilterPack_FX_Drunk Drunk;

	// Token: 0x04000C14 RID: 3092
	private CameraFilterPack_FX_Drunk2 Drunk2;

	// Token: 0x04000C15 RID: 3093
	private CameraFilterPack_FX_EarthQuake EarthQuake;

	// Token: 0x04000C16 RID: 3094
	private CameraFilterPack_FX_Funk Funk;

	// Token: 0x04000C17 RID: 3095
	private CameraFilterPack_FX_Glitch1 Glitch1;

	// Token: 0x04000C18 RID: 3096
	private CameraFilterPack_FX_Glitch2 Glitch2;

	// Token: 0x04000C19 RID: 3097
	private CameraFilterPack_FX_Glitch3 Glitch3;

	// Token: 0x04000C1A RID: 3098
	private CameraFilterPack_FX_Grid Grid;

	// Token: 0x04000C1B RID: 3099
	private CameraFilterPack_FX_Hexagon Hexagon;

	// Token: 0x04000C1C RID: 3100
	private CameraFilterPack_FX_Hexagon_Black HexagonBlack;

	// Token: 0x04000C1D RID: 3101
	private CameraFilterPack_FX_Hypno Hypno;

	// Token: 0x04000C1E RID: 3102
	private CameraFilterPack_FX_InverChromiLum InverChromiLum;

	// Token: 0x04000C1F RID: 3103
	private CameraFilterPack_FX_Mirror FXMirror;

	// Token: 0x04000C20 RID: 3104
	private CameraFilterPack_FX_Plasma FXPlasma;

	// Token: 0x04000C21 RID: 3105
	private CameraFilterPack_FX_Psycho FXPsycho;

	// Token: 0x04000C22 RID: 3106
	private CameraFilterPack_FX_Scan Scan;

	// Token: 0x04000C23 RID: 3107
	private CameraFilterPack_FX_Screens Screens;

	// Token: 0x04000C24 RID: 3108
	private CameraFilterPack_FX_Spot Spot;

	// Token: 0x04000C25 RID: 3109
	private CameraFilterPack_FX_superDot superDot;

	// Token: 0x04000C26 RID: 3110
	private CameraFilterPack_FX_ZebraColor ZebraColor;

	// Token: 0x04000C27 RID: 3111
	private CameraFilterPack_Glasses_On GlassesOn;

	// Token: 0x04000C28 RID: 3112
	private CameraFilterPack_Glasses_On_2 GlassesOn2;

	// Token: 0x04000C29 RID: 3113
	private CameraFilterPack_Glasses_On_3 GlassesOn3;

	// Token: 0x04000C2A RID: 3114
	private CameraFilterPack_Glasses_On_4 GlassesOn4;

	// Token: 0x04000C2B RID: 3115
	private CameraFilterPack_Glasses_On_5 GlassesOn5;

	// Token: 0x04000C2C RID: 3116
	private CameraFilterPack_Glasses_On_6 GlassesOn6;

	// Token: 0x04000C2D RID: 3117
	private CameraFilterPack_Glitch_Mozaic Mozaic;

	// Token: 0x04000C2E RID: 3118
	private CameraFilterPack_Glow_Glow Glow;

	// Token: 0x04000C2F RID: 3119
	private CameraFilterPack_Glow_Glow_Color GlowColor;

	// Token: 0x04000C30 RID: 3120
	private CameraFilterPack_Gradients_Ansi Ansi;

	// Token: 0x04000C31 RID: 3121
	private CameraFilterPack_Gradients_Desert Desert;

	// Token: 0x04000C32 RID: 3122
	private CameraFilterPack_Gradients_ElectricGradient ElectricGradient;

	// Token: 0x04000C33 RID: 3123
	private CameraFilterPack_Gradients_FireGradient FireGradient;

	// Token: 0x04000C34 RID: 3124
	private CameraFilterPack_Gradients_Hue GradientsHue;

	// Token: 0x04000C35 RID: 3125
	private CameraFilterPack_Gradients_NeonGradient NeonGradient;

	// Token: 0x04000C36 RID: 3126
	private CameraFilterPack_Gradients_Rainbow GradientsRainbow;

	// Token: 0x04000C37 RID: 3127
	private CameraFilterPack_Gradients_Stripe Stripe;

	// Token: 0x04000C38 RID: 3128
	private CameraFilterPack_Gradients_Tech Tech;

	// Token: 0x04000C39 RID: 3129
	private CameraFilterPack_Gradients_Therma Therma;

	// Token: 0x04000C3A RID: 3130
	private CameraFilterPack_Light_Rainbow LightRainbow;

	// Token: 0x04000C3B RID: 3131
	private CameraFilterPack_Light_Rainbow2 LightRainbow2;

	// Token: 0x04000C3C RID: 3132
	private CameraFilterPack_Light_Water Water;

	// Token: 0x04000C3D RID: 3133
	private CameraFilterPack_Light_Water2 Water2;

	// Token: 0x04000C3E RID: 3134
	private CameraFilterPack_Lut_2_Lut Lut;

	// Token: 0x04000C3F RID: 3135
	private CameraFilterPack_Lut_2_Lut_Extra LutExtra;

	// Token: 0x04000C40 RID: 3136
	private CameraFilterPack_Lut_Mask Mask;

	// Token: 0x04000C41 RID: 3137
	private CameraFilterPack_Lut_PlayWith PlayWith;

	// Token: 0x04000C42 RID: 3138
	private CameraFilterPack_Lut_Plus Plus;

	// Token: 0x04000C43 RID: 3139
	private CameraFilterPack_Lut_Simple LutSimple;

	// Token: 0x04000C44 RID: 3140
	private CameraFilterPack_Lut_TestMode TestMode;

	// Token: 0x04000C45 RID: 3141
	private CameraFilterPack_NewGlitch1 NewGlitch1;

	// Token: 0x04000C46 RID: 3142
	private CameraFilterPack_NewGlitch2 NewGlitch2;

	// Token: 0x04000C47 RID: 3143
	private CameraFilterPack_NewGlitch3 NewGlitch3;

	// Token: 0x04000C48 RID: 3144
	private CameraFilterPack_NewGlitch4 NewGlitch4;

	// Token: 0x04000C49 RID: 3145
	private CameraFilterPack_NewGlitch5 NewGlitch5;

	// Token: 0x04000C4A RID: 3146
	private CameraFilterPack_NewGlitch6 NewGlitch6;

	// Token: 0x04000C4B RID: 3147
	private CameraFilterPack_NewGlitch7 NewGlitch7;

	// Token: 0x04000C4C RID: 3148
	private CameraFilterPack_NightVisionFX NightVisionFX;

	// Token: 0x04000C4D RID: 3149
	private CameraFilterPack_NightVision_4 NightVision4;

	// Token: 0x04000C4E RID: 3150
	private CameraFilterPack_Noise_TV TV;

	// Token: 0x04000C4F RID: 3151
	private CameraFilterPack_Noise_TV_2 TV2;

	// Token: 0x04000C50 RID: 3152
	private CameraFilterPack_Noise_TV_3 TV3;

	// Token: 0x04000C51 RID: 3153
	private CameraFilterPack_Oculus_NightVision1 NightVision1;

	// Token: 0x04000C52 RID: 3154
	private CameraFilterPack_Oculus_NightVision2 NightVision2;

	// Token: 0x04000C53 RID: 3155
	private CameraFilterPack_Oculus_NightVision3 NightVision3;

	// Token: 0x04000C54 RID: 3156
	private CameraFilterPack_Oculus_NightVision5 NightVision5;

	// Token: 0x04000C55 RID: 3157
	private CameraFilterPack_Oculus_ThermaVision ThermaVision;

	// Token: 0x04000C56 RID: 3158
	private CameraFilterPack_OldFilm_Cutting1 Cutting1;

	// Token: 0x04000C57 RID: 3159
	private CameraFilterPack_OldFilm_Cutting2 Cutting2;

	// Token: 0x04000C58 RID: 3160
	private CameraFilterPack_Pixelisation_DeepOilPaintHQ DeepOilPaintHQ;

	// Token: 0x04000C59 RID: 3161
	private CameraFilterPack_Pixelisation_Dot Dot;

	// Token: 0x04000C5A RID: 3162
	private CameraFilterPack_Pixelisation_OilPaint OilPaint;

	// Token: 0x04000C5B RID: 3163
	private CameraFilterPack_Pixelisation_OilPaintHQ OilPaintHQ;

	// Token: 0x04000C5C RID: 3164
	private CameraFilterPack_Pixelisation_Sweater Sweater;

	// Token: 0x04000C5D RID: 3165
	private CameraFilterPack_Pixel_Pixelisation Pixelisation;

	// Token: 0x04000C5E RID: 3166
	private CameraFilterPack_Rain_RainFX RainFX;

	// Token: 0x04000C5F RID: 3167
	private CameraFilterPack_Real_VHS RealVHS;

	// Token: 0x04000C60 RID: 3168
	private CameraFilterPack_Retro_Loading Loading;

	// Token: 0x04000C61 RID: 3169
	private CameraFilterPack_Sharpen_Sharpen Sharpen;

	// Token: 0x04000C62 RID: 3170
	private CameraFilterPack_Special_Bubble Bubble;

	// Token: 0x04000C63 RID: 3171
	private CameraFilterPack_TV_50 TV50;

	// Token: 0x04000C64 RID: 3172
	private CameraFilterPack_TV_80 TV80;

	// Token: 0x04000C65 RID: 3173
	private CameraFilterPack_TV_ARCADE ARCADE;

	// Token: 0x04000C66 RID: 3174
	private CameraFilterPack_TV_ARCADE_2 ARCADE2;

	// Token: 0x04000C67 RID: 3175
	private CameraFilterPack_TV_ARCADE_Fast ARCADEFast;

	// Token: 0x04000C68 RID: 3176
	private CameraFilterPack_TV_Artefact Artefact;

	// Token: 0x04000C69 RID: 3177
	private CameraFilterPack_TV_BrokenGlass BrokenGlass;

	// Token: 0x04000C6A RID: 3178
	private CameraFilterPack_TV_BrokenGlass2 BrokenGlass2;

	// Token: 0x04000C6B RID: 3179
	private CameraFilterPack_TV_Chromatical Chromatical;

	// Token: 0x04000C6C RID: 3180
	private CameraFilterPack_TV_Chromatical2 Chromatical2;

	// Token: 0x04000C6D RID: 3181
	private CameraFilterPack_TV_CompressionFX CompressionFX;

	// Token: 0x04000C6E RID: 3182
	private CameraFilterPack_TV_Distorted Distorted;

	// Token: 0x04000C6F RID: 3183
	private CameraFilterPack_TV_Horror Horror;

	// Token: 0x04000C70 RID: 3184
	private CameraFilterPack_TV_LED LED;

	// Token: 0x04000C71 RID: 3185
	private CameraFilterPack_TV_MovieNoise MovieNoise;

	// Token: 0x04000C72 RID: 3186
	private CameraFilterPack_TV_Noise TVNoise;

	// Token: 0x04000C73 RID: 3187
	private CameraFilterPack_TV_Old Old;

	// Token: 0x04000C74 RID: 3188
	private CameraFilterPack_TV_Old_Movie OldMovie;

	// Token: 0x04000C75 RID: 3189
	private CameraFilterPack_TV_Old_Movie_2 OldMovie2;

	// Token: 0x04000C76 RID: 3190
	private CameraFilterPack_TV_PlanetMars PlanetMars;

	// Token: 0x04000C77 RID: 3191
	private CameraFilterPack_TV_Posterize Posterize;

	// Token: 0x04000C78 RID: 3192
	private CameraFilterPack_TV_Rgb TVRgb;

	// Token: 0x04000C79 RID: 3193
	private CameraFilterPack_TV_Tiles Tiles;

	// Token: 0x04000C7A RID: 3194
	private CameraFilterPack_TV_Vcr Vcr;

	// Token: 0x04000C7B RID: 3195
	private CameraFilterPack_TV_VHS TVVHS;

	// Token: 0x04000C7C RID: 3196
	private CameraFilterPack_TV_VHS_Rewind VHSRewind;

	// Token: 0x04000C7D RID: 3197
	private CameraFilterPack_TV_Video3D Video3D;

	// Token: 0x04000C7E RID: 3198
	private CameraFilterPack_TV_Videoflip Videoflip;

	// Token: 0x04000C7F RID: 3199
	private CameraFilterPack_TV_Vignetting Vignetting;

	// Token: 0x04000C80 RID: 3200
	private CameraFilterPack_TV_Vintage Vintage;

	// Token: 0x04000C81 RID: 3201
	private CameraFilterPack_TV_WideScreenCircle WideScreenCircle;

	// Token: 0x04000C82 RID: 3202
	private CameraFilterPack_TV_WideScreenHorizontal WideScreenHorizontal;

	// Token: 0x04000C83 RID: 3203
	private CameraFilterPack_TV_WideScreenHV WideScreenHV;

	// Token: 0x04000C84 RID: 3204
	private CameraFilterPack_TV_WideScreenVertical WideScreenVertical;

	// Token: 0x04000C85 RID: 3205
	private CameraFilterPack_VHS_Tracking Tracking;

	// Token: 0x04000C86 RID: 3206
	private CameraFilterPack_Vision_Aura Aura;

	// Token: 0x04000C87 RID: 3207
	private CameraFilterPack_Vision_AuraDistortion AuraDistortion;

	// Token: 0x04000C88 RID: 3208
	private CameraFilterPack_Vision_Blood VisionBlood;

	// Token: 0x04000C89 RID: 3209
	private CameraFilterPack_Vision_Blood_Fast VisionBloodFast;

	// Token: 0x04000C8A RID: 3210
	private CameraFilterPack_Vision_Crystal Crystal;

	// Token: 0x04000C8B RID: 3211
	private CameraFilterPack_Vision_Drost Drost;

	// Token: 0x04000C8C RID: 3212
	private CameraFilterPack_Vision_Hell_Blood VisionHellBlood;

	// Token: 0x04000C8D RID: 3213
	private CameraFilterPack_Vision_Plasma VisionPlasma;

	// Token: 0x04000C8E RID: 3214
	private CameraFilterPack_Vision_Psycho VisionPsycho;

	// Token: 0x04000C8F RID: 3215
	private CameraFilterPack_Vision_Rainbow VisionRainbow;

	// Token: 0x04000C90 RID: 3216
	private CameraFilterPack_Vision_SniperScore SniperScore;

	// Token: 0x04000C91 RID: 3217
	private CameraFilterPack_Vision_Tunnel Tunnel;

	// Token: 0x04000C92 RID: 3218
	private CameraFilterPack_Vision_Warp Warp;

	// Token: 0x04000C93 RID: 3219
	private CameraFilterPack_Vision_Warp2 Warp2;

	// Token: 0x04000C94 RID: 3220
	public UILabel NameLabel;

	// Token: 0x04000C95 RID: 3221
	public float DisplayTimer;

	// Token: 0x04000C96 RID: 3222
	public float Speed;

	// Token: 0x04000C97 RID: 3223
	public int FilterMax;

	// Token: 0x04000C98 RID: 3224
	private int FilterID;

	// Token: 0x04000C99 RID: 3225
	public string[] FilterNames;

	// Token: 0x04000C9A RID: 3226
	public bool[] FilterSkips;
}
