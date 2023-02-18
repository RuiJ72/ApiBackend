namespace LinqSnippets
{
    public class Enterprise
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public Employee[] Employees { get; set; } = new Employee[0];
    }
}