using Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hospital_IS.DTOs
{
    public class RoomNumberValidation : ValidationBase
    {
        private String _roomNumber = "";

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


        protected override void ValidateSelf()
        {
          
                bool isEmpty = false;
              
                if (string.IsNullOrWhiteSpace(this._roomNumber))
                {
                    this.ValidationErrors["RoomNumber"] = "Polje je obavezno";
                    isEmpty = true;
                }
                if (!this._roomNumber.All(Char.IsDigit) && isEmpty == false)
                {
                    this.ValidationErrors["RoomNumber"] = "Unesite samo brojeve";
                }
                else if (isEmpty == false)
                {
                  
                  
                    bool isUnique = RoomController.Instance.CheckIfRoomNumberIsUnique(Convert.ToInt32(this._roomNumber));

                    if (isUnique == false )
                    {

                        this.ValidationErrors["RoomNumber"] = "Broj nije jedinstven";
                    }

                }
        

            }
        }
}
