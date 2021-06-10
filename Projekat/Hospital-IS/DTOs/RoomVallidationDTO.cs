using Controllers;
using Hospital_IS.ManagerViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Hospital_IS.DTOs
{
    public class RoomVallidationDTO : ValidationBase
    {
        private String _roomNumber = "";
        private String _roomFloor = "";
        private String _surfaceArea = "";
        private String _bedNumber = "";


         public String RoomNumber
        {
            get
            {
                return _roomNumber;
            }
            set
            {
                if (value != _roomNumber)
                {
                    _roomNumber = value;
                    OnPropertyChanged("RoomNumber");
                }

            }
        }




        public String RoomFloor
        {
            get
            {
                return _roomFloor;
            }
            set
            {
                if (value != _roomFloor)
                {
                    _roomFloor = value;
                    OnPropertyChanged("RoomFloor");
                }

            }
        }


        public String SurfaceArea
        {
            get
            {
                return _surfaceArea;
            }
            set
            {
                if (value != _surfaceArea)
                {
                    _surfaceArea = value;
                    OnPropertyChanged("SurfaceArea");
                }

            }
        }

        public String BedNumber
        {
            get
            {
                return _bedNumber;
            }
            set
            {
                if (value != _bedNumber)
                {
                    _bedNumber = value;
                    OnPropertyChanged("BedNumber");

                }

            }
        }

        public int OldNumber { get; set; }

        protected override void ValidateSelf()
        {
            bool isEmpty = false;
            bool isEmpty1 = false;
            bool isEmpty2 = false;
            bool isEmpty3 = false;
            if (string.IsNullOrWhiteSpace(this._roomNumber))
            {
                this.ValidationErrors["RoomNumber"] = "Polje je obavezno";
                isEmpty = true;
            }
            if(!this._roomNumber.All(Char.IsDigit) && isEmpty==false)
            {
                this.ValidationErrors["RoomNumber"] = "Unesite samo brojeve";
            }
            else if(isEmpty == false)
            {
                bool isSame = false;
                if (OldNumber.ToString().Equals(this._roomNumber))
                {
                    isSame = true;   
                }
                bool isUnique = RoomController.Instance.CheckIfRoomNumberIsUnique(Convert.ToInt32(this._roomNumber));
        
                if (isUnique == false && isSame == false)
                {
                   
                    this.ValidationErrors["RoomNumber"] = "Broj nije jedinstven";
                }
                
            }
            if (string.IsNullOrWhiteSpace(this._roomFloor))
            {
                isEmpty1 = true;
                this.ValidationErrors["RoomFloor"] = "Polje je obavezno";
            
            }
            if (!this._roomFloor.All(Char.IsDigit) && isEmpty1 == false)
            {
                this.ValidationErrors["RoomFloor"] = "Unesite samo brojeve";
            }
            else if (isEmpty1 == false)
            {
                int roomFloor = Convert.ToInt32(this._roomFloor);
                if(roomFloor < 0 || roomFloor > 10)
                {
                    this.ValidationErrors["RoomFloor"] = "Maksimalan broj spratova je 10";
                }
            }
            if (string.IsNullOrWhiteSpace(this._bedNumber))
            {
                isEmpty2 = true; 
                this.ValidationErrors["BedNumber"] = "Polje je obavezno";

            }

            if (!this._bedNumber.All(Char.IsDigit) && isEmpty2 == false)
            {
                this.ValidationErrors["BedNumber"] = "Unesite samo brojeve";
            }
            else if (isEmpty2 == false)
            {
                int bedNumber = Convert.ToInt32(this._bedNumber);
                if (bedNumber < 0)
                {
                    this.ValidationErrors["BedNumber"] = "Unesite 0  ako soba nema kreveta";
                }
            }
            if (string.IsNullOrWhiteSpace(this._surfaceArea))
            {
                isEmpty3 = true;
                this.ValidationErrors["SurfaceArea"] = "Polje je obavezno";


            }
            if (!this._surfaceArea.All(Char.IsDigit) && isEmpty3 == false)
            {
                this.ValidationErrors["SurfaceArea"] = "Unesite samo brojeve";
            }
            else if(isEmpty3 == false)
            {
                int surfaceArea = Convert.ToInt32(this._surfaceArea);
                if (surfaceArea < 5 || surfaceArea > 10000)
                {
                    this.ValidationErrors["SurfaceArea"] = "Opseg povrsine je izmedju 5 i 10000";
                }
            }
        }

    }
}
