namespace Chess.Core.Domain
{
    public class Drill
    {
        public int Id { get; protected set; }
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public string Category { get; protected set; }
        public string StartPosition { get; protected set;}
        public string FileName { get; protected set;}
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