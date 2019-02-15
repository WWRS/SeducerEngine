using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Effects;

namespace WPF
{
    public class GridEffect : ShaderEffect
    {
        public double Time
        {
            get => (double) GetValue(TimeProperty);
            set => SetValue(TimeProperty, value);
        }

        public static readonly DependencyProperty TimeProperty = DependencyProperty.Register("Time", typeof(double), typeof(GridEffect), new UIPropertyMetadata(0d, PixelShaderConstantCallback(0)));

        public double ResolutionRatio
        {
            get => (double) GetValue(ResolutionRatioProperty);
            set => SetValue(ResolutionRatioProperty, value);
        } 
        
        public static readonly DependencyProperty ResolutionRatioProperty = DependencyProperty.Register("ResolutionRatio", typeof(double), typeof(GridEffect), new UIPropertyMetadata(SystemParameters.FullPrimaryScreenWidth / SystemParameters.FullPrimaryScreenHeight, PixelShaderConstantCallback(1)));

        public GridEffect()
        {
            PixelShader = new PixelShader
            {
                UriSource = new Uri(Path.Combine(Directory.GetCurrentDirectory(), "Assets/grid.ps"))
            };

            UpdateShaderValue(TimeProperty);
            UpdateShaderValue(ResolutionRatioProperty);
        }
    }
}