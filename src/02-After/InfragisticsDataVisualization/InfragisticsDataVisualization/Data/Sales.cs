using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Infragistics.Samples.Data.Models
{
    public class SalesDataSample : INotifyPropertyChanged
    {
        public SalesDataSample()
        {

            //Set the sales target this year
            salesTargetThisYear = 100000000;

            //Generate Sales data
            salesData = SalesDataGenerator.GenerateSales(100000);

            //Get Sales Amount grouped-by ProductName filtered by this year
            var queryForGroupByProduct = from x in salesData
                                         where x.Date.Year == DateTime.Today.Year
                                         group x by x.ProductName into A
                                         select new SalesAmountByProduct { ProductName = A.Key, AmountOfSale = A.Sum(a => (int)a.AmountOfSale) };
            salesAmountByProduct = new ObservableCollection<SalesAmountByProduct>(queryForGroupByProduct.OrderByDescending(x => x.AmountOfSale));

            //Get Sales Amount grouped-by ProductName filtered by this year
            totalSalesThisYear = salesData
                .Where(x => x.Date.Year == DateTime.Today.Year)
                .Select(d => d.AmountOfSale)
                .Sum();

            //Get top 30 large deals filtered by this year
            var queryTop30LargeDeals = salesData
                .Where(x => x.Date.Year == DateTime.Today.Year)
                .Select(x => x)
                .OrderByDescending(x => x.AmountOfSale)
                .Take(30);
            top30LargeDeals = new ObservableCollection<Sale>(queryTop30LargeDeals);

            //Get monthly sales amount filtered by this year
            var queryMonthlySalesAmount = from x in salesData
                                          where x.Date.Year == DateTime.Today.Year
                                          group x by x.Date.Month into A
                                          orderby A.Key
                                          select new MonthlySale { Month = A.Key, AmountOfSale = A.Sum(a => (int)a.AmountOfSale) };
            monthlySalesAmount = new ObservableCollection<MonthlySale>(queryMonthlySalesAmount);

        }


        public event PropertyChangedEventHandler PropertyChanged;

        private int salesTargetThisYear;
        public int SalesTargetThisYear
        {
            get { return salesTargetThisYear; }
        }

        //All Sales Data
        private ObservableCollection<Sale> salesData;
        public ObservableCollection<Sale> SalesData
        {
            get { return salesData; }
        }

        //Sales Amount By Product
        private ObservableCollection<SalesAmountByProduct> salesAmountByProduct;
        public ObservableCollection<SalesAmountByProduct> SalesAmountByProductData
        {
            get { return salesAmountByProduct; }
        }

        //Amount Sales This Year
        private int totalSalesThisYear;
        public int TotalSalesThisYear
        {
            get { return totalSalesThisYear; }
        }

        //Top 30 Large Deals This Year
        private ObservableCollection<Sale> top30LargeDeals;
        public ObservableCollection<Sale> Top30LargeDeals
        {
            get { return top30LargeDeals; }
        }

        //Monthly Sales Amount
        private ObservableCollection<MonthlySale> monthlySalesAmount;
        public ObservableCollection<MonthlySale> MonthlySalesAmount
        {
            get { return monthlySalesAmount; }
        }
    }
    public class Sale
    {
        public Sale()
        {
            this.Product = new Product();
            this.Seller = new Seller();
        }
        internal Product Product { get; set; }
        internal Seller Seller { get; set; }

        [DisplayName("SalesPerson")]
        public string SalesPerson
        {
            get { return Seller.Name; }
            set { Seller.Name = value; }
        }

        [DisplayName("Date")]
        public DateTime Date { get; set; }

        [DisplayName("City")]
        public string City
        {
            get { return Seller.City; }
            set { Seller.City = value; }
        }

        [DisplayName("ProductName")]
        public string ProductName
        {
            get { return Product.Name; }
            set { Product.Name = value; }
        }
        internal double Value { get; set; }
        internal string Quarter { get; set; }

        [DisplayName("QTY")]
        public int NumberOfUnits { get; set; }

        [DisplayName("UnitPrice")]
        public double UnitPrice
        {
            get { return Product.UnitPrice; }
            set { Product.UnitPrice = value; }
        }

        [DisplayName("SalesAmount")]
        public int AmountOfSale
        {
            get { return (int)UnitPrice * NumberOfUnits; }
            set { Product.UnitPrice = value / NumberOfUnits; }
        }
    }
    public class Seller
    {
        public string Name { get; set; }
        public string City { get; set; }
    }
    public class Product
    {
        public Product() { }
        public string Name { get; set; }
        public double UnitPrice { get; set; }
    }
    public class MonthlySale
    {
        public MonthlySale()
        {
        }
        private int month;
        public int Month
        {
            get { return month; }
            set { month = value; }
        }
        private int amountOfSale;
        public int AmountOfSale
        {
            get { return amountOfSale; }
            set { amountOfSale = value; }
        }
    }
    public class SalesAmountByProduct
    {
        public SalesAmountByProduct()
        {
        }
        private string productName;
        public string ProductName
        {
            get { return productName; }
            set { productName = value; }
        }
        private int amountOfSale;
        public int AmountOfSale
        {
            get { return amountOfSale; }
            set { amountOfSale = value; }
        }
    }
    public class SalesDataGenerator
    {
        private static String[] _products;
        private static String[] _sellerNames;
        private static String[] _cities;
        private static readonly Random Random = new Random();
        public static ObservableCollection<Sale> GenerateSales(int numberOfSales)
        {
            _products = "Apple;Grape;Orage;Banana".Split(';');
            _sellerNames = "Ellen Adams;Lisa Andrews;William Fox;Walter Harp;Jessica Oxley;Misty Shock;Chris Meyer;Jay Calvin".Split(';');
            _cities = "Tokyo;Shanghai;Beijing;Singapore;New York;Seoul".Split(';');
            //_products = "リンゴ;みかん;ぶどう;梨".Split(';');
            //_sellerNames = "高橋 真紀;東海 誠一;田中 秀美;木下 和子;明山 典央;正門 恵子;松沢 彩子;森上 偉久馬;古田 哲也".Split(';');
            //_cities = "東京;神奈川;埼玉;茨木;千葉;栃木;群馬;山梨".Split(';');
            ObservableCollection<Sale> sales = new ObservableCollection<Sale>();

            for (double i = 0; i < numberOfSales; i++)
            {
                Sale sale = new Sale
                {
                    Quarter = "Q " + i,
                    Value = GetRandomPrice(),
                    Date = GetRandomDate(),
                    Product = GerRandomProduct(),
                    NumberOfUnits = GetRandomNumUnits(),
                    Seller = GetRandomSeller()
                };
                sales.Add(sale);
            }
            return sales;
        }
        #region Get Random Objects
        private static Seller GetRandomSeller()
        {
            return new Seller
            {
                City = GetRandomCity(),
                Name = GetRandomSellerName()
            };
        }
        private static string GetRandomSellerName()
        {
            Random a = new Random(Random.Next());
            int length = _sellerNames.Length;
            int RandomMaxLength = a.Next(length) % 2 == 0 ? a.Next(length) : length;
            return _sellerNames[a.Next(RandomMaxLength)];
        }
        private static string GetRandomCity()
        {
            Random a = new Random(Random.Next());
            int length = _cities.Length;
            int RandomMaxLength = a.Next(length) % 2 == 0 ? a.Next(length) : length;
            return _cities[a.Next(RandomMaxLength)];
        }
        private static int GetRandomNumUnits()
        {
            Random a = new Random(Random.Next());
            return a.Next(1, 30);
        }
        private static Product GerRandomProduct()
        {
            return new Product
            {
                Name = GetRandomProductName(),
                UnitPrice = GetRandomPrice()
            };
        }
        private static double GetRandomPrice()
        {
            Random a = new Random(Random.Next());
            return a.NextDouble() * 300;
        }
        private static string GetRandomProductName()
        {
            Random a = new Random(Random.Next());
            int length = _products.Length;
            int RandomMaxLength = a.Next(length) % 2 == 0 ? a.Next(length) : length;
            return _products[a.Next(RandomMaxLength)];
        }
        private static DateTime GetRandomDate()
        {
            Random a = new Random(Random.Next());
            int day = a.Next(1, 28);
            int month = a.Next(1, 13);
            int year = a.Next(DateTime.Today.Year - 3, DateTime.Today.Year + 1);
            return new DateTime(year, month, day);
        }
        #endregion
    }
}