﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Tenplex.Controls
{
    public class Grouping<TKey, TElement> : ObservableCollection<TElement>, IGrouping<TKey, TElement>
    {
        public Grouping(TKey key)
        {
            this.Key = key;
        }

        public Grouping(TKey key, IEnumerable<TElement> items)
            : this(key)
        {
            foreach (var item in items)
            {
                this.Add(item);
            }
        }

        public TKey Key { get; }
    }
}
