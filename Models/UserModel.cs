namespace Katalitica_API.Models
{
    public class UserModel
    {
        public string userId { get; set; }
        public string userEmail { get; set; }
        public string userPassword { get; set; }
        public int userType { get; set; }

        public static List<UserModel> testDB()
        {
            List<UserModel> list = new List<UserModel>()

            {

                //Admin.userType = 1
                new UserModel
                {
                    userId = "123",
                    userEmail = "test@gmail.com",
                    userPassword = "pswd",
                    userType = 0

                },
                new UserModel
                {
                    userId = "324",
                    userEmail = "adminEmail@gmail.com",
                    userPassword = "adminPswd",
                    userType = 1

                }
            };

            return list;
        }
    }
}
