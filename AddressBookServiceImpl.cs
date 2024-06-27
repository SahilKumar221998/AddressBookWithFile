using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AddressBookWithDictionary
{
    public class AddressBookServiceImpl : IAdressBookService
    {
        string firstName;
        string lastName;
        string address;
        string city;
        string state;
        long phone_Number;
        string email;
        bool isPresent;
        int zip;
        string pattern;
        string temp;
        string message = "";
        Regex regex;
        private const string path = "D:\\C#\\AddressBookFile\\AddressBookData\\AddressBook.csv";
        ContactPerson contactPerson;
        Dictionary<char,List<ContactPerson>> dictionary=new Dictionary<char,List<ContactPerson>>(); 
       
        //Raed all data from csv file
        public Dictionary<char, List<ContactPerson>> ReadCsvFile(string path)
        {
            var lines = File.ReadAllLines(path);
            foreach (var line in lines)
            {
                var values = line.Split(new char[] { ',' });
                char key = values[0][0];
                var contact = new ContactPerson(
                    values[0],  // FirstName
                    values[1],  // LastName
                    values[2],  // Address
                    values[3],  // City
                    values[4],  // State
                    Convert.ToInt32(values[5]),  // Zip
                    Convert.ToInt64(values[6]),  // Phone_Number
                    values[7]   // Email
                );

                if (!dictionary.ContainsKey(key))
                {
                    dictionary[key] = new List<ContactPerson>();
                }
                dictionary[key].Add(contact);
            }
            return dictionary;
        }

        // Append all data to csv file
        private void SaveToFile(ContactPerson contact)
        {
            var contactDetails = $"{contact.FirstName},{contact.LastName},{contact.Address},{contact.City},{contact.State},{contact.Zip},{contact.Phone_Number},{contact.Email}";
            File.AppendAllText(path, contactDetails+Environment.NewLine);
        }


        public void addPerson()
        { 

            Console.WriteLine("Enter the firstName");
            firstName=Console.ReadLine();
            message = "Ex:-Sahil";
            pattern = "([A-Z]{1})([a-z]){3,20}$";
            regex = new Regex(pattern);
            firstName = userCredentialsCheck(firstName);
            Dictionary<char, List<ContactPerson>> readData =ReadCsvFile(path);
            while (true)
            {
                isPresent = false;
                foreach (var ch in readData)
                {
                    List<ContactPerson> list = ch.Value;
                    foreach (var person in list)
                    {
                        if (person.FirstName.Equals(firstName))
                        {
                            isPresent = true;
                            try
                            {
                                throw new InvalidUserException("Name is Already Present!!!!.Try Another Name");
                            }
                            catch (Exception ex) { Console.WriteLine(ex.Message); }
                        }
                    }
                }
                if (isPresent == false)
                    break;
                Console.WriteLine("Enter your firstName :-");
                firstName = Console.ReadLine();
            }
            Console.WriteLine("Enter your lastName :- ");
            lastName = Console.ReadLine();
            message = "Ex:-Kumar";
            lastName = userCredentialsCheck(lastName);

            Console.WriteLine("Enter your address :- ");
            address = Console.ReadLine();
            message = "Ex:-Banglore,Karnataka,zip-421101";
            pattern = @"[\w][^\w]*$";
            regex = new Regex(pattern);
            address = userCredentialsCheck(address);


            Console.WriteLine("Enter your city :- ");
            city = Console.ReadLine();
            message = "Ex:-Banglore";
            pattern = @"[\w\W]{4,16}";
            regex = new Regex(pattern);
            city = userCredentialsCheck(city);

            Console.WriteLine("Enter your state :- ");
            state = Console.ReadLine();
            pattern = @"[\w\W]*";
            message = "Ex-Karnataka";
            regex = new Regex(pattern);
            state = userCredentialsCheck(state);

            Console.WriteLine("Enter six Digit Zip Code :- ");
            zip = Convert.ToInt32(Console.ReadLine());
            message = "Ex:-112345";
            pattern = @"[1-9]{1}[0-9]{5}$";
            regex = new Regex(pattern);
            temp = zip.ToString();
            temp = userCredentialsCheck(temp);
            zip = Convert.ToInt32(temp);

            Console.WriteLine("Eneter the phone_Number :- ");
            phone_Number = long.Parse(Console.ReadLine());
            message = "Ex:-7788901122";
            pattern = @"[6-9][0-9]{9}$";
            regex = new Regex(pattern);
            temp = phone_Number.ToString();
            temp = userCredentialsCheck(temp);
            phone_Number = long.Parse(temp);


            Console.WriteLine("Enter the email_Id :- ");
            email = Console.ReadLine();
            message = "Ex:-Sahilja@gmail.com";
            pattern = "^([\\w\\.\\-]+)@([\\w\\-]+)((\\.(\\w){2,3})+)$";
            regex = new Regex(pattern);
            email = userCredentialsCheck(email);
            contactPerson = new ContactPerson(firstName, lastName, address, city, state, zip, phone_Number, email);
            char initial = firstName[0];
            if (!dictionary.ContainsKey(initial))
            {
                dictionary[initial] = new List<ContactPerson>();
            }
            dictionary[initial].Add(contactPerson);

            SaveToFile(contactPerson); //Method to add data to csv file

            Console.WriteLine(contactPerson.ToString());
            Console.WriteLine("________________________________________________");
            Console.WriteLine("Added Successfully");
        }

        //To update a contact person
        public void editPerson()
        {
            Dictionary<char, List<ContactPerson>> readData = ReadCsvFile(path);
            Console.WriteLine("Enter the firstname");
            string name=Console.ReadLine();
            if (!readData.ContainsKey(name[0]))
            {
                try
                {
                    throw new InvalidUserException("No Data Found!!!!");
                }
                catch(Exception e) { Console.WriteLine(e.Message); }
            }
            List<ContactPerson> persons = readData[name[0]];
            foreach (var contact in persons)
            {
                if (contact.FirstName.Equals(name))
                {
                    Console.WriteLine("Select from Options to Update:-");
                    Console.WriteLine("1.FirstName\n2.LastName\n3.Address\n4.City\n5.State\n6.Zip\n7.Phone_Nmumber\n8.Email\n9.LogOut");
                    int choice = Convert.ToInt32(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            Console.WriteLine("Enter the firstName");
                            firstName = Console.ReadLine();
                            message = "Ex:-Sahil";
                            pattern = "([A-Z]{1})([a-z]){3,20}$";
                            regex = new Regex(pattern);
                            firstName = userCredentialsCheck(firstName);
                            while (true)
                            {
                                isPresent = false;
                                foreach (var ch in readData)
                                {
                                    List<ContactPerson> list = ch.Value;
                                    foreach (var per in list)
                                    {
                                        if (per.FirstName.Equals(firstName))
                                        {
                                            isPresent = true;
                                            try
                                            {
                                                throw new InvalidUserException("Name is Already Present!!!!.Try Another Name");
                                            }
                                            catch (Exception ex) { Console.WriteLine(ex.Message); }
                                        }
                                    }
                                }
                                if (isPresent == false)
                                    break;
                                Console.WriteLine("Enter your firstName :-");
                                firstName = Console.ReadLine();
                            }
                            contact.FirstName = firstName;
                            break;
                        case 2:
                            Console.WriteLine("Enter your lastName :- ");
                            lastName = Console.ReadLine();
                            message = "Ex:-Kumar";
                            pattern = "([A-Z]{1})([a-z]){3,20}$";
                            regex = new Regex(pattern);
                            contact.LastName= userCredentialsCheck(lastName);
                            break;
                        case 3:
                            Console.WriteLine("Enter your address :- ");
                            address = Console.ReadLine();
                            message = "Ex:-Banglore,Karnataka,zip-421101";
                            pattern = @"[\w][^\w]*$";
                            regex = new Regex(pattern);
                            contact.Address= userCredentialsCheck(address);
                            break;
                        case 4:
                            Console.WriteLine("Enter your city :- ");
                            city = Console.ReadLine();
                            message = "Ex:-Banglore";
                            pattern = @"[\w\W]{4,16}";
                            regex = new Regex(pattern);
                            contact.City= userCredentialsCheck(city);
                            break;
                        case 5:
                            Console.WriteLine("Enter your state :- ");
                            state = Console.ReadLine();
                            pattern = @"[\w\W]*";
                            message = "Ex-Karnataka";
                            regex = new Regex(pattern);
                            contact.State= userCredentialsCheck(state);
                            break;
                        case 6:
                            Console.WriteLine("Enter six Digit Zip Code :- ");
                            zip = Convert.ToInt32(Console.ReadLine());
                            message = "Ex:-112345";
                            pattern = @"[1-9]{1}[0-9]{5}$";
                            regex = new Regex(pattern);
                            temp = zip.ToString();
                            temp = userCredentialsCheck(temp);
                            contact.Zip = Convert.ToInt32(temp);
                            break;
                        case 7:
                            Console.WriteLine("Eneter the phone_Number :- ");
                            phone_Number = long.Parse(Console.ReadLine());
                            message = "Ex:-7788901122";
                            pattern = @"[6-9][0-9]{9}$";
                            regex = new Regex(pattern);
                            temp = phone_Number.ToString();
                            temp = userCredentialsCheck(temp);
                            contact.Phone_Number = long.Parse(temp);
                            break;
                        case 8:
                            Console.WriteLine("Enter the email_Id :- ");
                            email = Console.ReadLine();
                            message = "Ex:-Sahilja@gmail.com";
                            pattern = "^([\\w\\.\\-]+)@([\\w\\-]+)((\\.(\\w){2,3})+)$";
                            regex = new Regex(pattern);
                            contact.Email= userCredentialsCheck(email);
                            break;
                        case 9:
                            break;

                                
                    }
                    SaveToFile(contact);
                    Console.WriteLine(contact.ToString());
                    return;

                }

            }
                
                Console.WriteLine("Updated Succesfully");
                Console.WriteLine("_______________________________________");
            
            
        }

        public void removePerson()
        {
            Console.WriteLine("Enter your FirstName");
            string name=Console.ReadLine();
            if (!dictionary.ContainsKey(name[0]))
                if (!dictionary.ContainsKey(name[0]))
                {
                    try
                    {
                        throw new InvalidUserException("!!!!!!!!Invalid User!!!!!!!!");
                    }
                    catch (Exception e) { Console.WriteLine(e.ToString()); }
                }
            List<ContactPerson> persons = dictionary[name[0]];
            ContactPerson contactPersonRemove= null;
            foreach (var contact in persons)
            {
                if (contact.FirstName.Equals(name))
                {
                   contactPersonRemove = contact;
                    break;
                }

            }
            if(contactPersonRemove != null) {
                persons.Remove(contactPersonRemove);
                Console.WriteLine("Removed Sucessfully");
            }
            else
            {
                try
                {
                    throw new InvalidUserException($"!!INVALID USER!!||{message}");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        public void showUserDetails()
        {
            Dictionary<char, List<ContactPerson>> readData = ReadCsvFile(path);
            foreach(var item in readData)
            {
                Console.WriteLine(item.Key);
                List<ContactPerson> list = item.Value;
                foreach (var contact in list) { 
                  Console.WriteLine(contact.ToString()); 
                }
            }
            readData.Clear();
        }

        public string userCredentialsCheck(string name)
        {
            bool isPresent = true;
            while (true)
            {
                isPresent = false;
                if (!(regex.IsMatch(name)))
                {

                    isPresent = true;
                    try
                    {
                        throw new InvalidUserException($"!!INVALID USER!!||{message}");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
                if (isPresent == false)
                    break;
                Console.WriteLine("Enter Again with Correct Credentials :-");
                name = Console.ReadLine();
            }
            return name;
        }

        // Search Contact Details by using City
       public void searchOnTheBasisOfCity()
        {
            Dictionary<char, List<ContactPerson>> readData = ReadCsvFile(path);
            Console.WriteLine("Enter the name of City");
            string city=Console.ReadLine();
            foreach(var contact in readData)
            {
                List<ContactPerson> list= contact.Value;    
                foreach (var contactPerson in list)
                {
                    if (contactPerson.City.Equals(city))
                    {
                        Console.WriteLine(contactPerson.ToString());
                        return;
                    }
                   
                }
               
            }
            try
            {
                throw new InvalidUserException("No Data Found!!!!!");
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
        }
       public void searchOnTheBasisOfState()
        {
            Dictionary<char, List<ContactPerson>> readData = ReadCsvFile(path);
            Console.WriteLine("Enter the name of State");
            string state = Console.ReadLine();
            foreach (var contact in readData)
            {
                List<ContactPerson> list = contact.Value;
                foreach (var contactPerson in list)
                {
                    if (contactPerson.State.Equals(state))
                    {
                        Console.WriteLine(contactPerson.ToString());
                        return;
                    }
                    
                }
                  
            }
            try
            {
                throw new InvalidUserException("No Data Found!!!!!");
            }
            catch (Exception e) { Console.WriteLine(e.Message); }

        }
    }
}
