using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;

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
        
        public PhoneList(SerializationInfo info, StreamingContext context)
        {
            contacts = (LinkedList<Contact>)info.GetValue("Contacts", typeof(LinkedList<Contact>));
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
        
        public void Serialize()
        {
            
            Type[] types = new Type[] 
            { 
                typeof(LinkedList<Contact>), 
                typeof(Contact)
            };
            
            using (FileStream fileStream = new FileStream("PhoneList.bin", FileMode.Create))
            {
                DataContractSerializer serializer = new DataContractSerializer(typeof(PhoneList), types);
                serializer.WriteObject(fileStream, this);
            }

        }

        public void Deserialize()
        {
            
            Type[] types = new Type[] 
            { 
                typeof(LinkedList<Contact>), 
                typeof(Contact)
            };
            
            using (FileStream fileStream = new FileStream("PhoneList.bin", FileMode.Open))
            {
                DataContractSerializer serializer = new DataContractSerializer(typeof(PhoneList), types);
                PhoneList loaded = (PhoneList)serializer.ReadObject(fileStream);
                contacts = loaded.contacts;
            }

        }
    }
}