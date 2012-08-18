var Model = require("../Models/hello.js");

var hello = function(request, response, filename, callback)
{
	var m = new Model();
	m.Title = "Home";
	callback(m, filename, response);
	
	return this;
}

hello.test = function(request, response, filename, callback)
{
	var m = new Model();
	m.Title = "Jame's Home";
	m.hello.title = "James";
	//callback(model, filename, response, ".mu");
	callback(m, "hello", response);

	return this;
}


hello.test.test = function(request, response, filename, callback)
{
	var m = new Model();
	m.Title = "/Hello/Test/Test";
	callback(m, "hello", response);

	return this;
}








//make sure to export the main method; it should be named the same as the file
module.exports = hello;
