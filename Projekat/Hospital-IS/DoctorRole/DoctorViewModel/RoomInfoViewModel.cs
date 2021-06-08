using Enums;
using Hospital_IS.DoctorRole.Commands;
using Model;

//MVVM
namespace Hospital_IS.DoctorRole.DoctorViewModel
{
    public class RoomInfoViewModel : BindableBase
    {
        #region Fields
        private Room selectedRoom;
        private string type;

        public Room SelectedRoom
        {
            get { return selectedRoom; }
            set
            {
                selectedRoom = value;
                SetType();
                OnPropertyChanged("SelectedRoom");
            }
        }

        public string Type
        {
            get { return type; }
            set
            {
                type = value;
                OnPropertyChanged("Type");
            }
        }
        #endregion

        #region Commands
        #endregion

        #region Actions
        #endregion

        #region Methods
        public void SetType()
        {
            if(SelectedRoom != null)
            {
                if (selectedRoom.Type.Equals(RoomType.ConsultingRoom))
                {
                    Type = "Soba za konsultacije";
                }
                else if (selectedRoom.Type.Equals(RoomType.OperationRoom))
                {
                    Type = "Operaciona sala";
                }
                else if (selectedRoom.Type.Equals(RoomType.RecoveryRoom))
                {
                    Type = "Soba za oporavak";
                }
                else if (selectedRoom.Type.Equals(RoomType.StorageRoom))
                {
                    Type = "Magacin";
                }
            }
            
        }
        #endregion

        #region Constructor
        public RoomInfoViewModel()
        {
           
        }
        #endregion
    }
}
