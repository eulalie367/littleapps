var Model = require("../Models/page.js");

var templates = function(request, response, filename, callback)
{

	var m = new Model();
	m.Title = "Home";
	callback(m, "home", response);
	
	return this;
}

templates.test = function(request, response, filename, callback)
{
	var m = new Model();
	m.Title = "Jame's Home";
	//callback(model, filename, response, ".mu");
	callback(m, "home", response);

	return this;
}


templates.test.test = function(request, response, filename, callback)
{
	var m = new Model();
	m.Title = "/Hello/Test/Test";
	callback(m, "home", response);

	return this;
}





module.exports = templates;
exports.test = templates.test;
exports.test.test = templates.test.test;

