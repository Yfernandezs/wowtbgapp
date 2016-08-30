using MvvmHelpers;

using System;
using System.Collections.Generic;
using System.Text;

namespace WoWTBGapp.DataObjects
{
    public class RequirementImageData : ObservableObject, IBaseDataObject
    {
        public string _id { get; set; }

        public string Name { get; set; }

        public int Value { get; set; }

        public string ImageURL { get; set; }
    }
}
