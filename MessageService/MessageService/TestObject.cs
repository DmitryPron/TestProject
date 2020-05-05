namespace MessageService
{
    public class TestObject
    {
        public TestObject(string name, int count)
        {
            Name = name;
            Count = count;
            Id = 0;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int Count { get; set; }
    }
}
