using System;
using System.Net.Sockets;
using Hesira.Models.Address;


namespace Hesira.Models.User
{

    public class UserProfileModel
    {
        public long Id { get; set; }

        // Id-ul deținătorului acestui profil
        public long Id_User { get; set; }

        // Ziua de naștere
        public DateTime BirthDay { get; set; }

        // Id-ul entității de tip adresă
        public long? Id_Address { get; set; }

        public string PhoneNumber { get; set; }

        public int Gender { get; set; }

        public string Profession { get; set; }

        // Id-ul țării a cărui cetățean este
        public int Id_Citizenship { get; set; }

        // Statusul CNAS
        public int? Id_State { get; set; }

        // Modelul domain pentru Adresă
        public AddressModel Address { get; set; }
    }

}
