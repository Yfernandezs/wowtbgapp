using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WoWTBGapp.DataObjects;

namespace WoWTBGapp.Clients.Portable
{
    public interface IUpdateUIWindows
    {
        void UpdateItemCardsPageList(ObservableRangeCollection<Grouping<string, ItemCard>> CardsGrouped, IEnumerable<Grouping<string, ItemCard>> Cards);
    }
}
