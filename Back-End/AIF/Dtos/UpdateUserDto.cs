namespace AIF.Dtos
{ 
public class UpdateUserDto
{
    public string OldUsername { get; set; }
    public string OldEmail { get; set; }
    public string NewUsername { get; set; }
    public string NewEmail { get; set; }
}
}