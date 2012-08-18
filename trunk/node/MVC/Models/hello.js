var Master = require("./master.js");
var controllers = requireindex("./Controllers");


var visitors = 0;
module.exports = function()
{
	var m = new Master();
	
	m.hello = new function()
	{
		this.title = "Joe";
		this.calc = function() 
		{
			return visitors++;
		}
		return this;
	}
	
	m.menu = new function()
	{
		var retVal = "";
		foreach(p in controllers)
		{
			console.log(p);
			console.log(controllers[p]);
		}
	}
	
	return m;
}
