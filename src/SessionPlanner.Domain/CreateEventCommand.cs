using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SessionPlanner.Domain
{
    public class CreateEventCommand
    {
        public CreateEventCommand()
        {

        }

        public CreateEventCommand(string name, DateTime startDate, DateTime endDate)
        {
            Name = name;
            StartDate = startDate;
            EndDate = endDate;
        }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public override bool Equals(object obj)
        {
            var command = obj as CreateEventCommand;
            return command != null &&
                   Name == command.Name &&
                   StartDate == command.StartDate &&
                   EndDate == command.EndDate;
        }

        public override int GetHashCode()
        {
            var hashCode = -1231911193;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + StartDate.GetHashCode();
            hashCode = hashCode * -1521134295 + EndDate.GetHashCode();
            return hashCode;
        }
    }
}