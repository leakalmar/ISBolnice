namespace Controllers
{

    public class AdministrationEmployeeController
    {
        private static AdministrationEmployeeController instance = null;
        public static AdministrationEmployeeController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AdministrationEmployeeController();
                }
                return instance;
            }
        }

        private AdministrationEmployeeController()
        {

        }
    }
}
