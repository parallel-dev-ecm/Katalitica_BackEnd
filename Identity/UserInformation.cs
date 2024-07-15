namespace Identity
{
    public class UserInformation
    {
        public string? cn { get; set; }
        public string? givenName { get; set; }
        public string? distinguishedName { get; set; }
        public string? sAMAccountName { get; set; }
        public IEnumerable<string>? memberOf { get; set; }
    }
}
