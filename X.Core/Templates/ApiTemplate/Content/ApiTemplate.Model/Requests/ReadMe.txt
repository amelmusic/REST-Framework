Extend request classes here. For eg, if we have model named User, than corresponding insert request will be UserInsertRequest

In order to extend it, we can simply create partial class:

partial UserInsertRequest {
	//some additional properties
}