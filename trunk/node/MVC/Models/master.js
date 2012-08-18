var mvc = require("mvc");

module.exports = function()
{
	this.Title = "Page Title";
	this.Menu = new function()
	{
		var retVal = "";
		return mvc.SiteMap();
	}

	return this;
}
