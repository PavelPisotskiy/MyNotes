using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyNotes.Themes.CustomTextBlock
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:CustomTextBlock"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:CustomTextBlock;assembly=CustomTextBlock"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:CustomControl1/>
    ///
    /// </summary>
    public class CustomTextBlock : TextBlock
    {
        public Brush FindTextColor
        {
            get { return (Brush)GetValue(FindTextColorProperty); }
            set { SetValue(FindTextColorProperty, value); }
        }

        public static readonly DependencyProperty FindTextColorProperty =
            DependencyProperty.Register("FindTextColor", typeof(Brush), typeof(CustomTextBlock), new PropertyMetadata(new SolidColorBrush(Colors.Red)));

        public string FindText
        {
            get { return (string)GetValue(FindTextProperty); }
            set { SetValue(FindTextProperty, value); }
        }

        public static readonly DependencyProperty FindTextProperty =
            DependencyProperty.Register("FindText", typeof(string), typeof(CustomTextBlock), new PropertyMetadata("", PropertyChanged));

        private static void PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CustomTextBlock tb = (CustomTextBlock)d;

            if(tb.TextEffects != null)
            {
                tb.TextEffects.Clear();
            }else
            {
                tb.TextEffects = new TextEffectCollection();
            }

            foreach (Match match in FindWord(tb.Text, tb.FindText))
            {
                TextEffect effect = new TextEffect();
                effect.Foreground = tb.FindTextColor;
                effect.PositionStart = match.Index;
                effect.PositionCount = match.Length;

                tb.TextEffects.Add(effect);
            }

        }

        private static MatchCollection FindWord(string text, string word)
        {
            Regex reg = new Regex(word.Trim().ToLower(), RegexOptions.Compiled | RegexOptions.IgnoreCase);
            return reg.Matches(text.ToLower());
        }
    }
}
