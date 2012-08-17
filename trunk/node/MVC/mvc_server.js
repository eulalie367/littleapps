var libpath = require("path"),
   http = require("http"),
   fs = require("fs"),
   url = require("url"),
   mime = require("mime"),
   ext = require("ext"),
   requireindex = require('requireindex'),
   controllers = requireindex("./Controllers");
   
//move to config file.
var port = 80;
var debug = true;
var excludes = "mvc_server.js|node_modules|Controllers|Models|Views";
var defaultPath = "hello";//use ./ for /index.html


var path = ".";
var request, response;

http.createServer(function (req, res)
{
	request = req;
	response = res;
   var uri = url.parse(request.url).pathname;
   var filename = libpath.join(path, uri);

   //kill the process; comment out for production
   if (debug && filename == "kill")
   {
      process.kill();
   }
   
	//is this a path or a static file?
	var extname = libpath.extname(filename);
	if(ext.isNullOrEmpty(extname)) //path
	{
	console.log(filename);
		if(filename == "./")
		{
			filename = defaultPath;
		}
		serveMVC(filename);
	}
	else //file
	{
      serveStaticFile(filename);
	}
	
}).listen(port);


function serveMVC(filename)
{
		//should we look for index.html?
		 var defaultDoc = filename + "index.html";
		 libpath.exists(defaultDoc, function (exists)
		 {
			if(exists)//possibly a static file
			{
		      serveStaticFile(defaultDoc);
			}
			else//assume mvc
			{
				filename = filename.toLowerCase();
				var methodTree = filename.split("/");

				if(methodTree.length > 0)//does module exits?
				{
					var method = controllers;
					for(i=0;i < methodTree.length;i++)//move through the tree
					{
						method = method[methodTree[i]];
					}
					
					if(typeof(method) == "function")
					{
						return200(method(request, response));
					}
					else if (typeof(method) == "object" && typeof(method["index"]) == "function")//should we call the index method?
					{
						return200(method["index"](request, response));
					}
					else
					{
						return404();
					}
				}
				else
				{
					return404();
				}
			}
		 });
}
function serveStaticFile(filename)
{
	if(!filename.match(new RegExp(excludes,"gi")))
	{
		/*
			All files that can be served should be lowercase.
			The rule of thumb is that all server code should be named in sentence case; on a unix/linux system filenames are case sensitive.
			We are doing a regex to exclude specific files, because windows doesn't care about casing.
		*/
		filename = filename.toLowerCase();
		libpath.exists(filename, function (exists)
		{
			if(exists)
			{
				fs.readFile(filename, "binary", function (err, file)
				{
					if (err)
					{
						response.writeHead(500,
						{
						   "Content-Type": "text/plain"
						});
						response.write(err + "\n");
						response.end();
						return;
					}
					var type = mime.lookup(filename);
					response.writeHead(200,
					{
						"Content-Type": type
					});
					response.write(file, "binary");
					response.end();
					return;
				});
			}
			else
			{
				return return404();
			}
		});
   }
   else
   {
   	return return404();
   }
}

function return404()
{
	response.writeHead(404,
	{
		"Content-Type": "text/plain"
	});
	response.write("404 Not Found\n");
	response.end();
	return;

}
function return200(text)
{
	response.writeHead(200,
	{
		"Content-Type": "text/html"
	});
	response.write(text);
	response.end();
	return;
}
