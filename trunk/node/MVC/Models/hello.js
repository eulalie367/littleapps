var Master = require("./master.js");

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
		
	return m;
}

