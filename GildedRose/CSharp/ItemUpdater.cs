using System;
using System.Collections.Generic;
using System.Linq;

namespace GildedRose
{
    public class ItemUpdater
    {
        private const string NAME_AGED_BRIE = "Aged Brie";
        private const string NAME_BACKSTAGE = "Backstage passes to a TAFKAL80ETC concert";
        private const string NAME_CONJURED = "Conjured Mana Cake";
        private const string NAME_SULFURAS = "Sulfuras, Hand of Ragnaros";

        private Dictionary<Predicate<Item>, Func<Item, int>> m_QualityRules;
        private Dictionary<Predicate<Item>, Func<Item, int>> m_SellInRules;

        public ItemUpdater()
        {
            CreateQualityRules();
            CreateSellInRules();
        }

        public void Update(Item item)
        {
            item.Quality = GetUpdatedQuality(item);
            item.SellIn = GetUpdatedSellIn(item);
        }

        private void CreateQualityRules()
        {
            m_QualityRules = new Dictionary<Predicate<Item>, Func<Item, int>>();
            m_QualityRules.Add(item => item.Name == NAME_SULFURAS, item => item.Quality);
            m_QualityRules.Add(item => item.Name == NAME_AGED_BRIE && item.SellIn > 0, item => Math.Min(item.Quality + 1, 50));
            m_QualityRules.Add(item => item.Name == NAME_AGED_BRIE, item => Math.Min(item.Quality + 2, 50));
            m_QualityRules.Add(item => item.Name == NAME_BACKSTAGE && item.SellIn > 10, item => item.Quality + 1);
            m_QualityRules.Add(item => item.Name == NAME_BACKSTAGE && item.SellIn > 5, item => item.Quality + 2);
            m_QualityRules.Add(item => item.Name == NAME_BACKSTAGE && item.SellIn > 0, item => item.Quality + 3);
            m_QualityRules.Add(item => item.Name == NAME_BACKSTAGE, item => 0);
            m_QualityRules.Add(item => item.Name == NAME_CONJURED && item.SellIn > 0, item => item.Quality - 2);
            m_QualityRules.Add(item => item.Name == NAME_CONJURED, item => item.Quality - 4);
            m_QualityRules.Add(item => item.SellIn > 0, item => item.Quality - 1);
            m_QualityRules.Add(item => true, item => item.Quality - 2);
        }

        private void CreateSellInRules()
        {
            m_SellInRules = new Dictionary<Predicate<Item>, Func<Item, int>>();
            m_SellInRules.Add(item => item.Name == NAME_SULFURAS, item => item.SellIn);
            m_SellInRules.Add(item => true, item => item.SellIn - 1);
        }

        private int GetUpdatedQuality(Item item)
        {
            var ruleKey = m_QualityRules.Keys.First(predicate => predicate(item));
            return m_QualityRules[ruleKey](item);
        }

        private int GetUpdatedSellIn(Item item)
        {
            var ruleKey = m_SellInRules.Keys.First(predicate => predicate(item));
            return m_SellInRules[ruleKey](item);
        }
    }
}