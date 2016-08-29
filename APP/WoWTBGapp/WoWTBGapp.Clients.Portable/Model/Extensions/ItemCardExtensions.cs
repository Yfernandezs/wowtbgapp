using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WoWTBGapp.DataObjects;

namespace WoWTBGapp.Clients.Portable
{
    public static class ItemCardExtensions
    {
        const string typeArmor = "Armor";
        const string typeBag = "Bag";
        const string typeRanged = "Ranged";
        const string typeMelee = "Melee";

        public static IEnumerable<Grouping<string, ItemCard>> GroupByPrimaryType(this IEnumerable<ItemCard> cards)
        {
            return from c in cards
                   orderby c.Types.Exists(x => x.Equals(typeBag)), c.Types.Exists(x => x.Equals(typeArmor)), c.Types.Exists(x => x.Equals(typeRanged)), c.Types.Exists(x => x.Equals(typeMelee))
                   group c by c.GetSortName()
                into cardGroup
                   select new Grouping<string, ItemCard>(cardGroup.Key, cardGroup);
        }

        public static string GetSortName(this ItemCard card)
        {
            //if (!e.StartTime.HasValue || !e.EndTime.HasValue)
            //    return "TBA";

            //var start = e.StartTime.Value.ToEasternTimeZone();

            //if (DateTime.Today.Year == start.Year)
            //{
            //    if (DateTime.Today.DayOfYear == start.DayOfYear)
            //        return $"Today";

            //    if (DateTime.Today.DayOfYear + 1 == start.DayOfYear)
            //        return $"Tomorrow";
            //}

            //var monthDay = start.ToString("M");

            //return $"{monthDay}";

            string PrimaryType = string.Empty;

            if (card.Types.Exists(x => x.Equals(typeBag)))
            {
                PrimaryType = typeBag;
            }
            else if (card.Types.Exists(x => x.Equals(typeArmor)))
            {
                PrimaryType = typeArmor;
            }
            else if (card.Types.Exists(x => x.Equals(typeRanged)))
            {
                PrimaryType = typeRanged;
            }
            else if (card.Types.Exists(x => x.Equals(typeMelee)))
            {
                PrimaryType = typeMelee;
            }

            return PrimaryType;
        }

    }
}
