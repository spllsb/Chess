namespace Chess.Core.Domain
{
    public class Drill
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }


        protected Drill()
        {
        }
        public Drill(string name, string description, string category)
        {
            Name = name;
            Description = description;
            Category = category;
        }
    }
}