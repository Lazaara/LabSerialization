using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace LabSerialization
{
    internal class Program
    {
        
        public static void Main(string[] args)
        {
            PhoneList phoneList = new PhoneList();

            phoneList.Deserialize();
            
            /*Contact contactOne = new Contact("Tuan", "0833666722", "tuancui@gmail.com", "Tuan lop IT");
            Contact contactTwo = new Contact("Hoang", "0817702217", "phuonghoang@gmail.com", "Cung phong");
            Contact contactThree = new Contact("Hua", "0913648955", "vanhua@gmail.com", "Hua lop triet hoc'");
            Contact contactFour = new Contact("Josh", "0824225359", "josh1999@gmail.com", "Josh o gan nha");
            Contact contactFive = new Contact("Hoang", "0886644332", "hoangcute@gmail.com", "Hoang banh mi");
            
            phoneList.Add(contactOne);
            phoneList.Add(contactTwo);
            phoneList.Add(contactThree);
            phoneList.Add(contactFour);
            phoneList.Add(contactThree);
            phoneList.Add(contactFive);
        
            
            phoneList.Serialize();
            */
            /*
            FileStream fileStream = new FileStream("student.bin", FileMode.Create);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(fileStream, phoneList);
            fileStream.Close();
            */
            
            Console.WriteLine("Truoc khi xoa duplicate: ");
            phoneList.Display();
            Console.WriteLine();
            
            Console.WriteLine("Sau khi xoa duplicate: ");
            phoneList.CheckDuplicate();
            phoneList.Display();
            Console.WriteLine();

            Console.WriteLine("Sau khi xoa Tuan khoi danh ba: ");
            phoneList.Remove("Tuan", "0833666722");
            phoneList.Display();
            Console.WriteLine();
            
            Console.WriteLine("Tim Hoang: ");
            foreach (Contact contact in phoneList.Find("Hoang"))
            {
                Console.WriteLine(contact.ToString());
            }
            
            
            Console.ReadLine();
        }


    }
    
}