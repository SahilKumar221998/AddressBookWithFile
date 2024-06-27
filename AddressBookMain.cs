using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBookWithDictionary
{
    public class AddressBookMain
    {
        IAdressBookService addressBook = new AddressBookServiceImpl();
        public void personChoices()
        {
            Console.WriteLine("---------------------WELCOME TO ADDRESS BOOK----------------------------");
            while (true)
            {
                Console.WriteLine("------------------------------------------------------------------------");
                Console.WriteLine("Please Select form the menu below:- ");
                Console.WriteLine("------------------------------------------------------------------------");
                Console.WriteLine("1.ADD\n2.UPDATE\n3.DELETE\n4.USER RECORDS\n5.Search On Basis Of City \n6.Search on The Basis Of State\n7.LOGOUT");
                int choice = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("-------------------------------------------------------------------------");
                if (choice == 7)
                    break;

                switch (choice)
                {
                    case 1: addressBook.addPerson(); break;
                    case 2: addressBook.editPerson(); break;
                    case 3: addressBook.removePerson(); break;
                    case 4: addressBook.showUserDetails(); break;
                    case 5:addressBook.searchOnTheBasisOfCity(); break;
                    case 6:addressBook.searchOnTheBasisOfState(); break;
                    default: Console.WriteLine("Invalid choice"); break;
                }


            }
        }
       public static void Main(string[] args)
        {
            AddressBookMain address = new AddressBookMain();
            address.personChoices();


        }
    }
}

