var mvc = require("mvc");


mvc.server.port = 80;
mvc.server.debug = true;
mvc.server.excludes += "|server.js";
mvc.server.defaultPath = "hello";

mvc.server.start();
