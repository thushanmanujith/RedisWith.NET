namespace Core.DTO
{
    public class UserDto
    {
        public UserDto(string name, int age, string profession)
        {
            Name = name;
            Age = age;
            Profession = profession;
        }

        public string Name { get; set; }
        public int Age { get; set; }
        public string Profession { get; set; }
    }
}
