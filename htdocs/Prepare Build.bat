::This uses collabnet svn, make sure it is installed.
::http://www.collab.net/downloads/subversion/
::
::
::This will export the latest from htdocs, and zip it
::
::TODO:Make this change out the connection string, might need to use a powershell
::
"C:\csvn\bin\svn.exe" export https://svn.vmldev.com/repos/SAP/SAPSolutionMatch/trunk/htdocs _tmp
cd _tmp
set filename=sapsolutionmatch_%date:~-4,4%%date:~-10,2%%date:~-7,2%_%time:~0,2%%time:~3,2%.zip
7z a -tzip %filename% *
7z d -r %filename% web.config
move %filename% ../
cd ..
rd /S /Q _tmp
