namespace Test
{
    public class StudentEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int Number { get; set; }
        public DateTime? DateTime { get; set; }

        public override string ToString()
        {
            var props = typeof(StudentEntity).GetProperties();
            var str = "";
            foreach (var prop in props)
            {
                var value = prop.GetValue(this);
                value = value == null ? "null" : value.ToString();
                var name = prop.Name;
                str = str + $"{name}: {value} ";
            }
            return str;
        }
    }

    public class StudentModel
    {
        public StudentModel()
        {
            DateTime = default(DateTime);
        }

        public Guid? Id { get; set; }
        public DateTime? Date { get; set; }
        public string? Name { get; set; }
        public DateTime? DateTime { get; set; }
        public int? Number { get; set; }

        public override string ToString()
        {
            var props = typeof(StudentEntity).GetProperties();
            var str = "";
            foreach (var prop in props)
            {
                var value = prop.GetValue(this);
                var name = prop.Name;
                str.Concat($"{name}:{value}, ");
            }
            return str;
        }
    }
}