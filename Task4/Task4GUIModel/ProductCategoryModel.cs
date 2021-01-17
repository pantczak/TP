using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Task4GUIModel //TODO REMOVE chyba zbedne -> ProperProductCategoryModel
{
    public class ProductCategoryModel : INotifyPropertyChanged
    {
        public ProductCategoryModel()
        {
        }

        public ProductCategoryModel(int productCategoryId, string name, DateTime modifiedDate)
        {
            _productCategoryId = productCategoryId;
            _name = name;
            _modifiedDate = modifiedDate;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private int _productCategoryId;

        private string _name;

        private DateTime _modifiedDate;


        public int ProductCategoryId
        {
            get => _productCategoryId;
            set
            {
                _productCategoryId = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public DateTime ModifiedDate
        {
            get => _modifiedDate;
            set
            {
                _modifiedDate = value;
                OnPropertyChanged();
            }
        }

        //TODO czy to tak?? check

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}