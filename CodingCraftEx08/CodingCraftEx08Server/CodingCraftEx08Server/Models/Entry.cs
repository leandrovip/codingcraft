using System;

namespace CodingCraftEx08Server.Models
{
    public class Entry
    {
        public string Title { get; set; }
        public DateTime Published { get; set; }
        public DateTime Updated { get; set; }
        public Author Author { get; set; }
        public Properties Properties { get; set; }
    }

    public class Author
    {
        public string Name { get; set; }
    }

    public class Properties
    {
        public string Id { get; set; }
        public string Version { get; set; }
        public string Authors { get; set; }
        public string Owners { get; set; }
        public string ProjectUrl { get; set; }
        public string ReleaseNotes { get; set; }
        public string Tags { get; set; }
    }
}