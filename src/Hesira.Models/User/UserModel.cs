namespace Hesira.Models.User
{

    public class UserModel
    {

        public long Id { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Email { get; set; }

        public bool Enabled { get; set; }

        public bool SidebarMode { get; set; }

        public string CNP { get; set; }

        public string Password { get; set; }

        public bool IsAdmin { get; set; }

        public bool IsPatient { get; set; }

        public bool IsDoctor { get; set; }

        public UserProfileModel Profile { get; set; }

        public long? Id_Profile { get; set; }

    }

}
