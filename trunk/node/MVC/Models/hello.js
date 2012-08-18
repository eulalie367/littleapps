var Master = require("./master.js");
var requireindex = require('requireindex');
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
		console.log(fetchMenu(controllers, { path: "/" }));
	}
	
	return m;
}

//this is only getting the modules for some reason
function fetchMenu(object, map)
{
	var hasChildren = false;
	for(var p in object)
	{
		hasChildren = true;
		map = 
		{
			path: map.path + p,
			children: fetchMenu(object[p], map)
		};
	}
	return hasChildren ? map : null;
}
