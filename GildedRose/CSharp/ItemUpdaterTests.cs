using NUnit.Framework;

namespace GildedRose
{
    [TestFixture]
    public class ItemUpdaterTests
    {
        private ItemUpdater m_Updater;

        [SetUp]
        public void SetUp()
        {
            m_Updater = new ItemUpdater();
        }

        [TestCase("+5 Dexterity Vest", 10, 9)]
        [TestCase("+5 Dexterity Vest", 0, -1)]
        [TestCase("Elixir of the Mongoose", 5, 4)]
        [TestCase("Elixir of the Mongoose", 0, -1)]
        [TestCase("Sulfuras, Hand of Ragnaros", 5, 5)]
        [TestCase("Sulfuras, Hand of Ragnaros", 0, 0)]
        [TestCase("Aged Brie", 2, 1)]
        [TestCase("Aged Brie", 0, -1)]
        [TestCase("Backstage passes to a TAFKAL80ETC concert", 15, 14)]
        [TestCase("Backstage passes to a TAFKAL80ETC concert", 0, -1)]
        [TestCase("Conjured Mana Cake", 3, 2)]
        [TestCase("Conjured Mana Cake", 0, -1)]
        public void Update_ForGivenItem_ShouldUpdateSellInAsExpected(string name, int sellIn, int expectedSellIn)
        {
            const int Quality = 0;
            var item = CreateItem(name, sellIn, Quality);
            m_Updater.Update(item);
            Assert.AreEqual(expectedSellIn, item.SellIn);
        }

        [TestCase("+5 Dexterity Vest", 1, 8, 7)]
        [TestCase("+5 Dexterity Vest", 0, 8, 6)]
        [TestCase("Elixir of the Mongoose", 1, 8, 7)]
        [TestCase("Elixir of the Mongoose", 0, 8, 6)]
        [TestCase("Sulfuras, Hand of Ragnaros", 1, 8, 8)]
        [TestCase("Sulfuras, Hand of Ragnaros", 0, 8, 8)]
        [TestCase("Aged Brie", 1, 8, 9)]
        [TestCase("Aged Brie", 0, 8, 10)]
        [TestCase("Aged Brie", 1, 50, 50)]
        [TestCase("Backstage passes to a TAFKAL80ETC concert", 0, 0, 0)]
        [TestCase("Backstage passes to a TAFKAL80ETC concert", 1, 10, 13)]
        [TestCase("Backstage passes to a TAFKAL80ETC concert", 2, 10, 13)]
        [TestCase("Backstage passes to a TAFKAL80ETC concert", 3, 10, 13)]
        [TestCase("Backstage passes to a TAFKAL80ETC concert", 4, 10, 13)]
        [TestCase("Backstage passes to a TAFKAL80ETC concert", 5, 10, 13)]
        [TestCase("Backstage passes to a TAFKAL80ETC concert", 6, 10, 12)]
        [TestCase("Backstage passes to a TAFKAL80ETC concert", 7, 10, 12)]
        [TestCase("Backstage passes to a TAFKAL80ETC concert", 8, 10, 12)]
        [TestCase("Backstage passes to a TAFKAL80ETC concert", 9, 10, 12)]
        [TestCase("Backstage passes to a TAFKAL80ETC concert", 10, 10, 12)]
        [TestCase("Backstage passes to a TAFKAL80ETC concert", 11, 10, 11)]
        [TestCase("Conjured Mana Cake", 1, 8, 6)]
        [TestCase("Conjured Mana Cake", 0, 8, 4)]
        public void Update_ForGivenItem_ShouldUpdateQualityAsExpected(string name, int sellIn, int quality, int expectedQuality)
        {
            var item = CreateItem(name, sellIn, quality);
            m_Updater.Update(item);
            Assert.AreEqual(expectedQuality, item.Quality);
        }

        private static Item CreateItem(string name, int sellIn, int quality)
        {
            return new Item { Name = name, SellIn = sellIn, Quality = quality };
        }
    }
}