using System;
using System.Collections.Generic;
using System.Text;

namespace BookShop.model.data
{
    public abstract class Event
    {
        public DateTime EventTime { get; set; }

        protected Event(DateTime eventTime)
        {
            EventTime = eventTime;
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Event other = (Event)obj;
                return (this.EventTime.Equals(other.EventTime));
            }
        }
    }
}
