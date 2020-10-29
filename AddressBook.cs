// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AddressBook.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator Name="Dheer Singh Meena"/>
// --------------------------------------------------------------------------------------------------------------------
using CsvHelper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
namespace AddressBookSystemFileIO_Day_21
{
    public class AddressBook
    {
        //creating a new list to store contact details entered by the user
        private List<Contact> list = new List<Contact>();
        //create a dictionary(generic collection) to store keyvalue pair
        private Dictionary<string, Contact> d = new Dictionary<string, Contact>();
        //create a city dictionary to store city details
        public static Dictionary<string, Contact> cityDictionary = new Dictionary<string, Contact>();
        //create a state dictionary to store state details
        public static Dictionary<string, Contact> stateDictionary = new Dictionary<string, Contact>();
        public List<Contact> GetList()
        {
            return list;
        }
        public Dictionary<string, Contact> GetDictionary()
        {
            return d;
        }
        //method to add address to the list
        public void AddAddress(string kname, Contact c)
        {
            list.Add(c);
            d.Add(kname, c);

        }
        //method to view contact details using key name
        public Contact ViewByKeyName(string kname)
        {
            //iterating the keyvalue pair using for each loop
            foreach (KeyValuePair<string, Contact> kvp in d)
            {
                if (kvp.Key == kname)
                    return kvp.Value;
            }
            return null;
        }
        public List<Contact> ViewAddressBook(int input)
        {
            if (input == 1)
            {
                list = list.OrderBy(c => c.name).ToList();
            }
            else if (input == 2)
            {
                list = list.OrderBy(c => c.city).ToList();
            }
            else if (input == 3)
            {
                list = list.OrderBy(c => c.state).ToList();
            }
            if (input == 4)
            {
                list = list.OrderBy(c => c.zip).ToList();
            }
            return list;
        }
        //method to edit the contact details
        public void EditNumber(string ename, string newnumber)
        {
            Boolean flag = false;
            foreach (Contact cc in list)
            {
                if (cc.name.Equals(ename))
                {
                    flag = true;
                    cc.phoneNo = newnumber;
                    Console.WriteLine("Number edited successfully");
                    break;
                }
            }
            if (!flag)
            {
                Console.WriteLine("No such name found!!!");
            }


        }
        //method to delete the contact from the phonebook
        public void RemoveContact(string rname)
        {
            Boolean flag = false;
            foreach (Contact cc in list)
            {
                if (cc.name.Equals(rname))
                {
                    flag = true;
                    list.Remove(cc);
                    Console.WriteLine("Number removed successfully");
                    break;
                }
            }
            if (!flag)
            {
                Console.WriteLine("No such name found!!!");
            }
        }
        //method to check for any duplicate entry 
        public bool CheckForDuplicateEntry(string name)
        {
            foreach (Contact c in list)
            {
                if (c.name.Equals(name))
                {
                    return true;
                }
            }
            return false;
        }
        //UC8-Ability to search Person in a City or State across the multiple address book
        public List<Contact> SearchPeopleByCityOrState(string location)
        {
            //using foreach loop to add city name entered by the user into the city dictionary
            foreach (Contact c in list)
            {
                cityDictionary.Add(c.city, c);
            }
            //using foreach loop to add state name entered by the user into the state dictionary
            foreach (Contact c in list)
            {
                stateDictionary.Add(c.state, c);
            }
            //creating a new list as list of people to store the people of similar city together
            List<Contact> listofpeople = new List<Contact>();
            //iterating the key value pair in the city dictionary using foreach loop
            foreach (KeyValuePair<string, Contact> kvp in cityDictionary)
            {
                // adding the key value pair into the list
                if (kvp.Key.Equals(location))
                {
                    listofpeople.Add(kvp.Value);
                }
            }
            //iterating the key value pair in the state dictionary using foreach loop
            foreach (KeyValuePair<string, Contact> kvp in stateDictionary)
            {
                // adding the key value pair into the list
                if (kvp.Key.Equals(location))
                {
                    listofpeople.Add(kvp.Value);
                }
            }
            return listofpeople;
        }
        //method to view address by city name
        public void AddressByCity()
        {
            //creating hashset with name cityset
            HashSet<string> citySet = new HashSet<string>();
            //using for each loop to iterate the list and add city into hashset
            foreach (Contact c in list)
            {

                citySet.Add(c.city);
            }
            //iterating the hashset to display contact details with city name
            foreach (string s in citySet)
            {
                Console.WriteLine("Contacts with address " + s + " are : ");
                Console.WriteLine();
                foreach (Contact cc in list)
                {

                    if (cc.city.Equals(s))
                        Console.WriteLine("Name : " + cc.name + "  Address : " + cc.address + "  ZIP : " + cc.zip + "  Contact No : " + cc.phoneNo + "  EmailID : " + cc.email);

                }
                Console.WriteLine();
                Console.WriteLine();
            }
        }
        //method to view address by state name
        public void AddressByState()
        {
            //creating hashset with name stateset
            HashSet<string> stateSet = new HashSet<string>();
            //using for each loop to iterate the list and add state into hashset
            foreach (Contact c in list)
            {

                stateSet.Add(c.state);
            }
            //iterating the hashset to display contact details with state name
            foreach (string s in stateSet)
            {
                Console.WriteLine("Contacts with address " + s + " are : ");
                Console.WriteLine();
                foreach (Contact cc in list)
                {

                    if (cc.state.Equals(s))
                        Console.WriteLine("Name : " + cc.name + "  Address : " + cc.address + "  ZIP : " + cc.zip + "  Contact No : " + cc.phoneNo + "  EmailID : " + cc.email);

                }
                Console.WriteLine();
                Console.WriteLine();
            }

        }

        /// <summary>
        /// UC15 Ability to read or write  the address book with person contact as Jsonfile.
        /// </summary>
        public void JsonReadAllText()
        {
            //Initializing variable to store file path.
            string importFilePath = @"C:\Users\dheer1998meena\source\repos\AddressBookSystemFileIO_Day-21\AddressBookSystemFileIO_Day-21\Json\export.json";
            IList<Contact> lists = JsonConvert.DeserializeObject<List<Contact>>(File.ReadAllText(importFilePath));
            //Iterating using foreach loop contact data in lists.
            foreach (Contact contact in lists)
            {
                Console.WriteLine("Name :" + contact.name);
                Console.WriteLine("Address :" + contact.address);
                Console.WriteLine("City :" + contact.city);
                Console.WriteLine("State :" + contact.state);
                Console.WriteLine("Zip :" + contact.zip);
                Console.WriteLine("Contact no. :" + contact.phoneNo);
                Console.WriteLine("Email :" + contact.email);
                Console.WriteLine();
            }
        }
    }
}    
    

