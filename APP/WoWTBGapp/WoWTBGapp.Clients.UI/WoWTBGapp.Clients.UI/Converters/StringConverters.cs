using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WoWTBGapp.DataAccess.Abstractions;
using Xamarin.Forms;

namespace WoWTBGapp.Clients.UI
{
    /// <summary>
    /// Text Formatting for Card Effects.
    /// </summary>
    class CardEffectsTextFormattingConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return string.Empty;
            }

            var text = value as string;

            bool boldText = text.StartsWith("**") && text.EndsWith("**");

            bool containsBoldTags = text.Contains("**");

            bool containsDices = text.Contains("$$RED$$") || text.Contains("$$GREEN$$") || text.Contains("$$BLUE$$");

            var fs = new FormattedString();

            var fontSize = 14; //Device.OS == TargetPlatform.

            if (containsDices)
            {
                //text = text.Replace("$$RED$$", "$$RED$$⛰$$RED$$").Replace("$$GREEN$$", "$$GREEN$$⛰$$GREEN$$").Replace("$$BLUE$$", "$$BLUE$$⛰$$BLUE$$").Replace("$$BLACK$$", "$$BLACK$$⛰$$BLACK$$");
                text = text.Replace("$$RED$$", "⛰").Replace("$$GREEN$$", "⛰").Replace("$$BLUE$$", "⛰").Replace("$$BLACK$$", "⛰");
                text = text.Replace("_1", "➊").Replace("_2", "➋").Replace("_3", "➌").Replace("_4", "➍").Replace("_5", "➎").Replace("_6", "➏").Replace("_7", "➐").Replace("_8", "➑").Replace("_9", "➒").Replace("_10", "➓");
            }

            if (boldText)
            {
                fs.Spans.Add(new Span { Text = text.Replace("**", string.Empty), ForegroundColor = Color.Black, Font = Font.SystemFontOfSize(fontSize), FontAttributes = FontAttributes.Bold });
            }
            else if (containsBoldTags)
            {
                var sections = text.Split(new string[] { "**" }, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < sections.Length; i++)
                {
                    var attribute = FontAttributes.None;

                    if (IsOdd(i))
                    {
                        attribute = FontAttributes.Bold;
                    }

                    fs.Spans.Add(new Span { Text = sections[i], ForegroundColor = Color.Black, Font = Font.SystemFontOfSize(fontSize), FontAttributes = attribute });
                }
            }
            else
            {
                fs.Spans.Add(new Span { Text = text, ForegroundColor = Color.Black, Font = Font.SystemFontOfSize(fontSize) });
            }

            //fs.Spans.Add(new Span { Text = "⛰⟁🔺ᐃ▲⏹➊➋➌➍➎➏➐➑➒➓", ForegroundColor = Color.Red, Font = Font.SystemFontOfSize(14) });
            //fs.Spans.Add(new Span { Text = " second ", ForegroundColor = Color.Blue, Font = Font.SystemFontOfSize(28) });
            //fs.Spans.Add(new Span { Text = " third.", ForegroundColor = Color.Yellow, Font = Font.SystemFontOfSize(14) });

            return fs;

        }

        public static bool IsOdd(int value)
        {
            return value % 2 != 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Check with the server for an image for the specific requirement.
    /// </summary>
    class RequirementImageURLConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return string.Empty;
            }

            var name = value as string;
            var reqValue = (int)parameter;

            string URL = string.Empty;

            var DataAccessManager = DependencyService.Get<IAccessManager>();

            if (DataAccessManager != null)
            {
                var response = DataAccessManager.GetDataAsync(null, $"api/requierementimage/{name}/{value}").Result;

                if (response.IsSuccessful)
                {
                    URL = response.Data;
                }
            }

            return URL;
        }
        
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
