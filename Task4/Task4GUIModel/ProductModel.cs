using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Task4GUIModel
{
    class ProductModel
    {
        private int productID;

        private string name;

        private string productNumber;

        private bool makeFlag;

        private bool finishedGoodsFlag;

        private string color;

        private short safetyStockLevel;

        private short reorderPoint;

        private decimal standardCost;

        private decimal listPrice;

        private string size;

        private string sizeUnitMeasureCode;

        private string weightUnitMeasureCode;

        private System.Nullable<decimal> weight;

        private int daysToManufacture;

        private string productLine;

        private string @class;

        private string style;

        private System.Nullable<int> productSubcategoryID;

        private System.Nullable<int> productModelID;

        private System.DateTime sellStartDate;
        
        private System.Nullable<System.DateTime> sellEndDate;

        private System.Nullable<System.DateTime> discontinuedDate;

        private System.Guid rowguid;

        private System.DateTime modifiedDate;

        public ProductModel()
        { }

        public ProductModel(int productID, string name, string productNumber, bool makeFlag, bool finishedGoodsFlag, string color, short safetyStockLevel, short reorderPoint, decimal standardCost, decimal listPrice, string size, string sizeUnitMeasureCode, string weightUnitMeasureCode, decimal? weight, int daysToManufacture, string productLine, string @class, string style, int? productSubcategoryID, int? productModelID, DateTime sellStartDate, DateTime? sellEndDate, DateTime? discontinuedDate, Guid rowguid, DateTime modifiedDate)
        {
            this.ProductID = productID;
            this.Name = name;
            this.ProductNumber = productNumber;
            this.MakeFlag = makeFlag;
            this.FinishedGoodsFlag = finishedGoodsFlag;
            this.Color = color;
            this.SafetyStockLevel = safetyStockLevel;
            this.ReorderPoint = reorderPoint;
            this.StandardCost = standardCost;
            this.ListPrice = listPrice;
            this.Size = size;
            this.SizeUnitMeasureCode = sizeUnitMeasureCode;
            this.WeightUnitMeasureCode = weightUnitMeasureCode;
            this.Weight = weight;
            this.DaysToManufacture = daysToManufacture;
            this.ProductLine = productLine;
            this.Class = @class;
            this.Style = style;
            this.ProductSubcategoryID = productSubcategoryID;
            this.ProductModelID = productModelID;
            this.SellStartDate = sellStartDate;
            this.SellEndDate = sellEndDate;
            this.DiscontinuedDate = discontinuedDate;
            this.Rowguid = rowguid;
            this.ModifiedDate = modifiedDate;
        }
        public event PropertyChangedEventHandler Handler;
        private void NotifyPropertyChanged([CallerMemberName] String property = "") {
            Handler?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public int ProductID { get => ProductID; set { ProductID = value; NotifyPropertyChanged("ProductID"); } }
        public string Name { get => name; set { name = value;NotifyPropertyChanged("Name"); } }
        public string ProductNumber { get => productNumber; set { productNumber = value; NotifyPropertyChanged("ProductNumber"); } }
        public bool MakeFlag { get => makeFlag; set { makeFlag = value; NotifyPropertyChanged("MakeFlag"); } }
        public bool FinishedGoodsFlag { get => finishedGoodsFlag; set { finishedGoodsFlag = value;NotifyPropertyChanged("FinishedGoodsFlag"); } }
        public string Color { get => color; set { color = value;NotifyPropertyChanged("Color"); } }
        public short SafetyStockLevel { get => safetyStockLevel; set { safetyStockLevel = value;NotifyPropertyChanged("SafetyStockLevel"); } }
        public short ReorderPoint { get => reorderPoint; set { reorderPoint = value;NotifyPropertyChanged("ReorderPoint"); } }
        public decimal StandardCost { get => standardCost; set { standardCost = value;NotifyPropertyChanged("StandardCost"); } }
        public decimal ListPrice { get => listPrice; set { listPrice = value;NotifyPropertyChanged("ListPrice"); } }
        public string Size { get => size; set { size = value;NotifyPropertyChanged("Size"); } }
        public string SizeUnitMeasureCode { get => sizeUnitMeasureCode; set { sizeUnitMeasureCode = value; NotifyPropertyChanged("SizeUnitMeasureCode"); } }
        public string WeightUnitMeasureCode { get => weightUnitMeasureCode; set { weightUnitMeasureCode = value;NotifyPropertyChanged("WeightUnitMeasureCode"); } }
        public decimal? Weight { get => weight; set { weight = value;NotifyPropertyChanged("Weight"); } }
        public int DaysToManufacture { get => daysToManufacture; set { daysToManufacture = value;NotifyPropertyChanged("DaysToManufacture"); } }
        public string ProductLine { get => productLine; set { productLine = value;NotifyPropertyChanged("ProductLine"); } }
        public string Class { get => @class; set { @class = value;NotifyPropertyChanged("Class"); } }
        public string Style { get => style; set { style = value;NotifyPropertyChanged("Style"); } }
        public int? ProductSubcategoryID { get => productSubcategoryID; set { productSubcategoryID = value;NotifyPropertyChanged("ProductdSubcategoryID"); } }
        public int? ProductModelID { get => productModelID; set { productModelID = value;NotifyPropertyChanged("ProductModelID"); } }
        public DateTime SellStartDate { get => sellStartDate; set { sellStartDate = value;NotifyPropertyChanged("SellStartDate"); } }
        public DateTime? SellEndDate { get => sellEndDate; set { sellEndDate = value;NotifyPropertyChanged("SellEndDate"); } }
        public DateTime? DiscontinuedDate { get => discontinuedDate; set { discontinuedDate = value;NotifyPropertyChanged("DiscontinuedDate"); } }
        public Guid Rowguid { get => rowguid; set { rowguid = value;NotifyPropertyChanged("Rowguid"); } }
        public DateTime ModifiedDate { get => modifiedDate; set { modifiedDate = value;NotifyPropertyChanged("ModifiedDate"); } }
    }

}
