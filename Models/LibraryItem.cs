namespace TCSA.OOP.LibraryManagementSystem.Models
{
    internal abstract class LibraryItem
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string Location { set; get; }

        protected LibraryItem(int id, string name, string location)
        {
            Id = id;
            Name = name;
            Location = location;
        }
        public abstract void DisplayDetails();

    }
}
