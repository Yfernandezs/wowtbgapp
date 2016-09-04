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

            var text = (value as string); //.Replace("+", "⁺");

            //bool boldText = text.StartsWith("**") && text.EndsWith("**");

            //bool containsBoldTags = text.Contains("**");

            //bool containsDices = text.Contains("$$RED$$") || text.Contains("$$GREEN$$") || text.Contains("$$BLUE$$");

            var fs = new FormattedString();

            ProcessStringToFormat(fs, text);

            //var fontSize = 14; //Device.OS == TargetPlatform.

            //if (containsDices)
            //{
            //    //text = text.Replace("$$RED$$", "$$RED$$⛰$$RED$$").Replace("$$GREEN$$", "$$GREEN$$⛰$$GREEN$$").Replace("$$BLUE$$", "$$BLUE$$⛰$$BLUE$$").Replace("$$BLACK$$", "$$BLACK$$⛰$$BLACK$$");
            //    text = text.Replace("$$RED$$", "⛰").Replace("$$GREEN$$", "⛰").Replace("$$BLUE$$", "⛰").Replace("$$BLACK$$", "⛰");
            //    text = text.Replace("_1", "➊").Replace("_2", "➋").Replace("_3", "➌").Replace("_4", "➍").Replace("_5", "➎").Replace("_6", "➏").Replace("_7", "➐").Replace("_8", "➑").Replace("_9", "➒").Replace("_10", "➓");
            //}

            //if (boldText)
            //{
            //    var textSection = text.Replace("**", string.Empty);
            //    fs.Spans.Add(new Span { Text = text.Replace("**", string.Empty), ForegroundColor = Color.Black, Font = Font.SystemFontOfSize(fontSize), FontAttributes = FontAttributes.Bold });
            //    AddFormatString(fs, textSection, )
            //}
            //else if (containsBoldTags)
            //{
            //    var sections = text.Split(new string[] { "**" }, StringSplitOptions.RemoveEmptyEntries);

            //    for (int i = 0; i < sections.Length; i++)
            //    {
            //        var attribute = FontAttributes.None;

            //        if (IsOdd(i))
            //        {
            //            attribute = FontAttributes.Bold;
            //        }

            //        fs.Spans.Add(new Span { Text = sections[i], ForegroundColor = Color.Black, Font = Font.SystemFontOfSize(fontSize), FontAttributes = attribute });
            //    }
            //}
            //else
            //{
            //    fs.Spans.Add(new Span { Text = text, ForegroundColor = Color.Black, Font = Font.SystemFontOfSize(fontSize) });
            //}

            //fs.Spans.Add(new Span { Text = "⛰⟁🔺ᐃ▲⏹➊➋➌➍➎➏➐➑➒➓⁺ᐩ●", ForegroundColor = Color.Red, Font = Font.SystemFontOfSize(14) });
            //fs.Spans.Add(new Span { Text = " second ", ForegroundColor = Color.Blue, Font = Font.SystemFontOfSize(28) });
            //fs.Spans.Add(new Span { Text = " third.", ForegroundColor = Color.Yellow, Font = Font.SystemFontOfSize(14) });

            return fs;
        }

        public static void ProcessStringToFormat(FormattedString fs, string text, int state = 0)
        {
            var textSection = text;
            var indexStartSubstring = 0;
            var currentProcessState = -1;
            var colorFormat = Color.Black;

            switch (state)
            {
                case 0: // No state, open search.
                    bool boldTextSection = textSection.StartsWith("**") && text.EndsWith("**");
                    var indexOfSemicolon = textSection.IndexOf(":") + 1;
                    var indexOfDoubleStar = textSection.IndexOf("**");
                    var indexOfDollarSimbol = textSection.IndexOf("$$");

                    if (boldTextSection)
                    {
                        // Remove the Star simbols.
                        textSection = textSection.Substring(2, textSection.Length - 4);

                        text = textSection;
                        indexStartSubstring = text.Length;
                        currentProcessState = 0;

                        AddFormatString(fs, textSection, colorFormat, FontAttributes.Bold);
                    }
                    else if ((indexOfSemicolon < indexOfDoubleStar && indexOfSemicolon < indexOfDollarSimbol) ||
                        (indexOfSemicolon < indexOfDollarSimbol && indexOfDoubleStar < 0) ||
                        (indexOfSemicolon < indexOfDoubleStar && indexOfDollarSimbol < 0) ||
                        (indexOfSemicolon >= 0 && indexOfDoubleStar == indexOfDollarSimbol))
                    {
                        textSection = textSection.Substring(0, indexOfSemicolon);
                        indexStartSubstring = indexOfSemicolon;
                        currentProcessState = 1;

                        AddFormatString(fs, textSection, colorFormat, FontAttributes.Italic);

                        AddFormatString(fs, "\n", colorFormat, FontAttributes.Italic);
                    }
                    else if ((indexOfDoubleStar < indexOfDollarSimbol && indexOfDoubleStar >= 0) || 
                        (indexOfDoubleStar >= 0 && indexOfDollarSimbol < 0))
                    {
                        textSection = textSection.Substring(0, indexOfDoubleStar);
                        indexStartSubstring = indexOfDoubleStar + 2;
                        currentProcessState = 2;

                        AddFormatString(fs, textSection, colorFormat);
                    }
                    else if ((indexOfDollarSimbol < indexOfDoubleStar && indexOfDollarSimbol >= 0) ||
                        (indexOfDollarSimbol >= 0 && indexOfDoubleStar < 0))
                    {
                        textSection = textSection.Substring(0, indexOfDollarSimbol);
                        indexStartSubstring = indexOfDollarSimbol;
                        currentProcessState = 3;

                        AddFormatString(fs, textSection, colorFormat);
                    }

                    break;
                case 1: // ":" found first, open search without bold or italic sections.
                    //text = "\n" + text;
                    //textSection = text;

                    indexOfDoubleStar = textSection.IndexOf("**");
                    indexOfDollarSimbol = textSection.IndexOf("$$");

                    if ((indexOfDoubleStar < indexOfDollarSimbol && indexOfDoubleStar >= 0) ||
                        (indexOfDoubleStar >= 0 && indexOfDollarSimbol < 0))
                    {
                        textSection = textSection.Substring(0, indexOfDoubleStar);
                        indexStartSubstring = indexOfDoubleStar + 2;
                        currentProcessState = 2;

                        AddFormatString(fs, textSection, colorFormat);
                    }
                    else if ((indexOfDollarSimbol < indexOfDoubleStar && indexOfDollarSimbol >= 0) ||
                        (indexOfDollarSimbol >= 0 && indexOfDoubleStar < 0))
                    {
                        textSection = textSection.Substring(0, indexOfDollarSimbol);
                        indexStartSubstring = indexOfDollarSimbol;
                        currentProcessState = 3;

                        AddFormatString(fs, textSection, colorFormat);
                    }

                    break;
                case 2:  // "**" found first.
                    indexOfDoubleStar = textSection.IndexOf("**");

                    textSection = textSection.Substring(0, indexOfDoubleStar);
                    indexStartSubstring = indexOfDoubleStar + 2;
                    currentProcessState = 1;

                    AddFormatString(fs, textSection, colorFormat, FontAttributes.Bold);
                    break;
                case 3: // "$$" found first. "⛰⟁🔺ᐃ▲⏹➊➋➌➍➎➏➐➑➒➓⁺ᐩ●"
                    var redColor = Color.FromHex("#E63900");
                    var greenColor = Color.FromHex("#39AC39");
                    var blueColor = Color.FromHex("#0099E6");

                    var diceColorText = textSection.Substring(0, textSection.Length >= 12 ? 12 : textSection.Length);
                    var actualDiceColorText = string.Empty;
                    var diceColor = diceColorText.Contains("$$RED$$") ? redColor : 
                                                    (diceColorText.Contains("$$GREEN$$") ? greenColor : 
                                                                    (diceColorText.Contains("$$BLUE$$") ? blueColor : Color.Black));
                    
                    var hasAdditionalValue = diceColorText.Contains("_");
                    var hasPlusSign = diceColorText.Contains("+");
                    var additionalValue = hasAdditionalValue ? diceColorText.Substring(diceColorText.IndexOf("_"), 2) : string.Empty;

                    actualDiceColorText = diceColor == redColor ? "$$RED$$" :
                                                    (diceColor == greenColor ? "$$GREEN$$" :
                                                                    (diceColor == blueColor ?  "$$BLUE$$" : "$$BLACK$$"));

                    if (hasAdditionalValue)
                    {
                        if (additionalValue.Equals("_."))
                        {
                            textSection = "●";

                            actualDiceColorText += "_.";
                        }
                        else
                        {
                            var number = int.Parse(additionalValue.Substring(1, 1));

                            textSection = "⛰";

                            switch (number)
                            {
                                case 1:
                                    textSection += "➊";
                                    break;
                                case 2:
                                    textSection += "➋";
                                    break;
                                case 3:
                                    textSection += "➌";
                                    break;
                                case 4:
                                    textSection += "➍";
                                    break;
                                case 5:
                                    textSection += "➎";
                                    break;
                                case 6:
                                    textSection += "➏";
                                    break;
                                case 7:
                                    textSection += "➐";
                                    break;
                                case 8:
                                    textSection += "➑";
                                    break;
                                case 9:
                                    textSection += "➒";
                                    break;
                                case 10:
                                    textSection += "➓";
                                    break;
                            }

                            if (hasPlusSign)
                            {
                                textSection += "⁺";

                                actualDiceColorText += $"_{number}+";
                            }
                            else
                            {
                                actualDiceColorText += $"_{number}";
                            }
                        }
                    }
                    else
                    {
                        textSection = "⛰";
                    }

                    AddFormatString(fs, textSection, diceColor);

                    indexStartSubstring = actualDiceColorText.Length;
                    currentProcessState = 1;
                    break;

            }

            if(currentProcessState == -1)
            {
                indexStartSubstring = textSection.Length;

                AddFormatString(fs, textSection, colorFormat);
            }

            textSection = text.Substring(indexStartSubstring);

            if (!string.IsNullOrEmpty(textSection))
            {
                ProcessStringToFormat(fs, textSection, currentProcessState);
            }
        }

        public static void AddFormatString(FormattedString fs, string text, Color color, FontAttributes attribute = FontAttributes.None, double fontSize = 14, 
                                            string fontFamily = "sans-serif-light")
        {
            if(fs != null)
            {
                var span = new Span { Text = text, ForegroundColor = color, FontSize = fontSize, FontFamily = fontFamily, FontAttributes = attribute };

                fs.Spans.Add(span);
            }
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
    /// 
    /// This Converter is NOT being used.
    /// </summary>
    //class RequirementImageURLConverter : IValueConverter
    //{
    //    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        if (value == null)
    //        {
    //            return string.Empty;
    //        }

    //        var name = value as string;
    //        var reqValue = (int)parameter;

    //        string URL = string.Empty;

    //        var DataAccessManager = DependencyService.Get<IAccessManager>();

    //        if (DataAccessManager != null)
    //        {
    //            var response = DataAccessManager.GetDataAsync(null, $"api/requierementimage/{name}/{value}").Result;

    //            if (response.IsSuccessful)
    //            {
    //                URL = response.Data;
    //            }
    //        }

    //        return URL;
    //    }
        
    //    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
