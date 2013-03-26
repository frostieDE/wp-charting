using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace WPCharting.Helper
{
    class AccentColors : List<SolidColorBrush>
    {
        public AccentColors()
        {
            this.Add(ToSolidColorBrush(0xFFA4C400));
            this.Add(ToSolidColorBrush(0xFF60A917));
            this.Add(ToSolidColorBrush(0xFF008A00));
            this.Add(ToSolidColorBrush(0xFF00ABA9));
            this.Add(ToSolidColorBrush(0xFF1BA1E2));
            this.Add(ToSolidColorBrush(0xFF0050EF));
            this.Add(ToSolidColorBrush(0xFF6A00FF));
            this.Add(ToSolidColorBrush(0xFFAA00FF));
            this.Add(ToSolidColorBrush(0xFFF472D0));
            this.Add(ToSolidColorBrush(0xFFD80073));
            this.Add(ToSolidColorBrush(0xFFA20025));
            this.Add(ToSolidColorBrush(0xFFE51400));
            this.Add(ToSolidColorBrush(0xFFFA6800));
            this.Add(ToSolidColorBrush(0xFFF0A30A));
            this.Add(ToSolidColorBrush(0xFFD8C100));
            this.Add(ToSolidColorBrush(0xFF825A2C));
            this.Add(ToSolidColorBrush(0xFF6D8764));
            this.Add(ToSolidColorBrush(0xFF647687));
            this.Add(ToSolidColorBrush(0xFF76608A));
            this.Add(ToSolidColorBrush(0xFF7A3D3F));
        }

        private static Color ToColor(uint argb)
        {
            return Color.FromArgb((byte)((argb & 0xff000000) >> 0x18),
                                  (byte)((argb & 0xff0000) >> 0x10),
                                  (byte)((argb & 0xff00) >> 8),
                                  (byte)(argb & 0xff));
        }

        private static SolidColorBrush ToSolidColorBrush(uint argb)
        {
            return new SolidColorBrush(ToColor(argb));
        }

        public static Color GetColorFromHexa(string hexaColor)
        {
            return Color.FromArgb(
                    Convert.ToByte(hexaColor.Substring(1, 2), 16),
                    Convert.ToByte(hexaColor.Substring(3, 2), 16),
                    Convert.ToByte(hexaColor.Substring(5, 2), 16),
                    Convert.ToByte(hexaColor.Substring(7, 2), 16)
            );
        }
    }
}
