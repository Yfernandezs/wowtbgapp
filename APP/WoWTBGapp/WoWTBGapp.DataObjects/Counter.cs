using MvvmHelpers;

using System;

namespace WoWTBGapp.DataObjects
{
    public class Counter : ObservableObject
    {
        public string Name { get; set; }

        public int Value { get; set; }
    }
}
