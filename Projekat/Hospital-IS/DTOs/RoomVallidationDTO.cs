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
        private String _roomNumber ;
        private String _roomFloor ;
        private String _surfaceArea ;
        private String _bedNumber ;


         public String RoomNumber
        {
            get
            {
                return _roomNumber;
            }
            set
            {


                _roomNumber = value;
              

                OnPropertyChanged("RoomNumber");

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

        protected override void ValidateSelf()
        {
            if (string.IsNullOrWhiteSpace(this._roomNumber))
            {
                this.ValidationErrors["RoomNumber"] = "Polje je obavezno";
            }
            if(!this._roomNumber.All(Char.IsDigit))
            {
                this.ValidationErrors["RoomNumber"] = "Unesite samo brojeve";
            }
            else
            {
                bool isUnique = RoomController.Instance.CheckIfRoomNumberIsUnique(Convert.ToInt32(this._roomNumber));
        
                if (isUnique == false)
                {
                   
                    this.ValidationErrors["RoomNumber"] = "Broj nije jedinstven";
                }
                
            }
            if (string.IsNullOrWhiteSpace(this._roomFloor))
            {
                this.ValidationErrors["RoomFloor"] = "Polje je obavezno";
            
            }
            if (!this._roomFloor.All(Char.IsDigit))
            {
                this.ValidationErrors["RoomFloor"] = "Unesite samo brojeve";
            }
            else
            {
                int roomFloor = Convert.ToInt32(this._roomFloor);
                if(roomFloor < 0 || roomFloor > 10)
                {
                    this.ValidationErrors["RoomFloor"] = "Maksimalan broj spratova je 10";
                }
            }
            if (string.IsNullOrWhiteSpace(this._bedNumber))
            {
                this.ValidationErrors["BedNumber"] = "Polje je obavezno";

            }
            if (!this._bedNumber.All(Char.IsDigit))
            {
                this.ValidationErrors["BedNumber"] = "Unesite samo brojeve";
            }
            else
            {
                int roomFloor = Convert.ToInt32(this._bedNumber);
                if (roomFloor <2 || roomFloor > 50)
                {
                    this.ValidationErrors["BedNumber"] = "Opseg broja kreveta je izmedju 2 i 50";
                }
            }
            if (string.IsNullOrWhiteSpace(this._surfaceArea))
            {
                this.ValidationErrors["SurfaceArea"] = "Polje je obavezno";

            }
            if (!this._surfaceArea.All(Char.IsDigit))
            {
                this.ValidationErrors["SurfaceArea"] = "Unesite samo brojeve";
            }
            else
            {
                int roomFloor = Convert.ToInt32(this._bedNumber);
                if (roomFloor < 5 || roomFloor > 10000)
                {
                    this.ValidationErrors["SurfaceArea"] = "Opseg povrsine je izmedju 5 i 10000";
                }
            }
        }

    }
}
