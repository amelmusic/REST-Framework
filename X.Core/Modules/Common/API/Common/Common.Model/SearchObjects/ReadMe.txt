Container for extending search objects
Eg:

If we have model named User, then its corresponding search object will be UserSearchObject
In order to add new parameters to search object we can simply declare partial class

partial UserSearchObject 
{
	public string UsernameContainingACharacter {get; set;}
}