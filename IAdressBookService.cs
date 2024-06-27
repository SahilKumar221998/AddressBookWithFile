using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBookWithDictionary
{
   public interface IAdressBookService
    {
        public Dictionary<char, List<ContactPerson>> ReadCsvFile(string path);
        void addPerson();
        void editPerson();
        void removePerson();
        void showUserDetails();
        string userCredentialsCheck(string name);

        void searchOnTheBasisOfCity();
        void searchOnTheBasisOfState();
    }
}
