using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace MyNotes.Localization
{
    public class Language
    {
        private string originalName;
        private string letfLanguageTag;
        private BitmapImage imageFlag;

        public Language(string originalName, string letfLanguageTag, BitmapImage imageFlag)
        {
            this.originalName = originalName;
            this.letfLanguageTag = letfLanguageTag;
            this.imageFlag = imageFlag;
        }

        public string OriginalName
        {
            get { return originalName; }
        }

        public string LetfLanguageTag
        {
            get { return letfLanguageTag; }
        }

        public BitmapImage ImageFlag
        {
            get { return imageFlag; }
        }
    }
}
