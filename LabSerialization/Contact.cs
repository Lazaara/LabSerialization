using System;
using System.Runtime.Serialization;

namespace LabSerialization
{
    [Serializable]
    public class Contact : ISerializable
    {
        private string name;
        private string number;
        private string email;
        private string note;

        public Contact(string name, string number, string email, string note)
        {
            this.name = name;
            this.number = number;
            this.email = email;
            this.note = note;
        }
        
        public Contact(SerializationInfo info, StreamingContext context)
        {
            name = info.GetString("name");
            number = info.GetString("number");
            email = info.GetString("email");
            note = info.GetString("note");
        }

        public Contact()
        {
            name = "";
            number = "";
            email = "";
            note = "";
        }

        public string GetName()
        {
            return name;
        }

        public string GetNumber()
        {
            return number;
        }

        public string GetEmail()
        {
            return email;
        }

        public string GetNote()
        {
            return note;
        }

        public void SetName(string input)
        {
            name = input;
        }

        public void SetNumber(string input)
        {
            number = input;
        }

        public void SetEmail(string input)
        {
            email = input;
        }

        public void SetNote(string input)
        {
            note = input;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("name", name);
            info.AddValue("number", number);
            info.AddValue("email", email);
            info.AddValue("note", note);
        }



        public override string ToString()
        {
            return $"{name} | {number} | {email} | {note}";
        }
    }
}