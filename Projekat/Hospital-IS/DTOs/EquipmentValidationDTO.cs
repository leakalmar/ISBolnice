using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Hospital_IS.DTOs
{
    public class EquipmentValidationDTO : ValidationBase
    {
        private string _name = "Unesite ime opreme";
        private string _quantity = "Unesite kolicinu";
        private string _producerName = "Unesite ime proizvodjaca";

        public String Quantity
        {
            get
            {
                return _quantity;
            }
            set
            {
                if (value != _quantity)
                {

                    _quantity = value;
                    OnPropertyChanged("Quantity");
                }
            }
        }

        public String ProducerName
        {
            get
            {
                return _producerName;
            }
            set
            {
                if (value != _producerName)
                {

                    _producerName = value;
                    OnPropertyChanged("ProducerName");
                }
            }
        }

        public String Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (value != _name)
                {

                    _name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        protected override void ValidateSelf()
        {
            if (string.IsNullOrWhiteSpace(this._name))
            {
                this.ValidationErrors["Name"] = "Polje je obavezno";
             
            }
              
            if (string.IsNullOrWhiteSpace(this._producerName))
            {
                this.ValidationErrors["ProducerName"] = "Polje je obavezno";

            }
           
            if (string.IsNullOrWhiteSpace(this._quantity))
            {
                this.ValidationErrors["Quantity"] = "Polje je obavezno";

            }
            if (!this._quantity.All(Char.IsDigit))
            {
                this.ValidationErrors["Quantity"] = "Unesite samo brojeve";
            }
           
        }
    }
}
