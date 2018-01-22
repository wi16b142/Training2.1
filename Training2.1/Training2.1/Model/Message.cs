using System;

namespace Training2._1.Model
{
    public class Message
    {
        string text;
        DateTime timestamp;

        public Message(string text)
        {
            this.Text = text;
            this.Timestamp = DateTime.Now;
        }

        public string Text { get => text; set => text = value; }
        public DateTime Timestamp { get => timestamp; set => timestamp = value; }
    }
}
