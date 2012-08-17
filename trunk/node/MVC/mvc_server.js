var libpath = require("path"),
   http = require("http"),
   fs = require("fs"),
   url = require("url"),
   mime = require("mime"),
   ext = require("ext"),
   requireindex = require('requireindex'),
   controllers = requireindex("./Controllers");
   
var path = ".";
var port = 8080;
var debug = true;
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
		//should we look for index.html?
		 var defaultDoc = filename + "/index.html";
		 libpath.exists(defaultDoc, function (exists)
		 {
			if(exists)//possibly a static file
			{
		      serveStaticFile(defaultDoc);
			}
			else//assume mvc
			{
				filename = filename.toLowerCase();
				var modName = filename.split("/");
				var methodTree = modName.slice(1);
				modName = modName[0];

				//does module exits?
				if(typeof(controllers[modName]) != "undefined")
				{
					//should we call the index method?
					if(methodTree.length < 1)
					{
						return200(controllers[modName]["index"]());
					}
					else
					{
						var method = controllers[modName];
						for(i=0;i < methodTree.length;i++)
						{
							method = method[methodTree[i]];
						}
						if(typeof(method) == "function")
						{
							return200(method());
						}
						else
						{
							return404();
						}
					}
				}
				else
				{
					return404();
				}
			}
		 });
	}
	else //file
	{
      serveStaticFile(filename);
	}
	
}).listen(port);


function serveStaticFile(filename)
{
   libpath.exists(filename, function (exists)
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
   });
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
