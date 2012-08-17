exports.index = function(request, response, filename, callback)
{
	
	var model = {
	  title: "Joe",
	  calc: function() {
		 return 2 + 4;
	  }
	};
	callback(model, filename, response, ".mu");
}

exports.test = function()
{
	return "/Hello/Test";
}
		exports.test.test = function()
	{
		return "/Hello/Test/Test";
	}

