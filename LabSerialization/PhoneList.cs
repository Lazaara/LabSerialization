using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace LabSerialization
{
    [Serializable]
    public class PhoneList : ISerializable
    {
        private LinkedList<Contact> contacts;
        
        public PhoneList()
        {
            contacts = new LinkedList<Contact>();
        }

        public void Add(Contact contact)
        {
            contacts.AddLast(contact);
        }

        public List<Contact> Find(string name)
        {
            List<Contact> result = new List<Contact>();

            foreach (var c in contacts)
            {
                if (c.GetName().IndexOf(name, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    result.Add(c);
                }
            }

            return result;
        }

        public void CheckDuplicate()
        {
            var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var node = contacts.First;

            while (node != null)
            {
                var next = node.Next;
                string key = $"{node.Value.GetName()}#{node.Value.GetNumber()}";

                if (seen.Contains(key))
                {
                    contacts.Remove(node);
                }
                else
                {
                    seen.Add(key);
                }

                node = next;
            }
        }

        public void Remove(string name, string number)
        {
            var node = contacts.First;
            while (node != null)
            {
                if (node.Value.GetName().Equals(name, StringComparison.OrdinalIgnoreCase) &&
                    node.Value.GetNumber().Equals(number))
                {
                    contacts.Remove(node);
                    return;
                }

                node = node.Next;
            }
        }

        public int Count()
        {
            return contacts.Count;
        }

        public void Display()
        {
            foreach (var c in contacts)
            {
                Console.WriteLine(c.ToString());
            }
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Contacts", contacts);
        }

        public PhoneList(SerializationInfo info, StreamingContext context)
        {
            contacts = (LinkedList<Contact>)info.GetValue("Contacts", typeof(LinkedList<Contact>));
        }

        public void Serialize()
        {
            
            Type[] knownTypes = new Type[] 
            { 
                typeof(LinkedList<Contact>), 
                typeof(Contact)
            };
            
            using (FileStream fileStream = new FileStream("PhoneList.bin", FileMode.Create))
            {
                DataContractSerializer serializer = new DataContractSerializer(typeof(PhoneList), knownTypes);
                serializer.WriteObject(fileStream, this);
            }

            /*FileStream fileStream = new FileStream("PhoneList.bin", FileMode.Create);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(fileStream, contacts);
            fileStream.Close();*/
        }

        public void Deserialize()
        {
            
            Type[] knownTypes = new Type[] 
            { 
                typeof(LinkedList<Contact>), 
                typeof(Contact)
            };
            
            using (FileStream fileStream = new FileStream("PhoneList.bin", FileMode.Open))
            {
                DataContractSerializer serializer = new DataContractSerializer(typeof(PhoneList), knownTypes);
                PhoneList loaded = (PhoneList)serializer.ReadObject(fileStream);
                contacts = loaded.contacts;
            }
            
            /*FileStream fileStream = new FileStream("student.bin", FileMode.Open);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            contacts = (LinkedList<Contact>)binaryFormatter.Deserialize(fileStream);
            fileStream.Close();*/
        }
    }
}