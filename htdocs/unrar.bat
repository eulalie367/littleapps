mkdir "extracted"
FOR /R %%D in (*.rar *.zip) DO "C:\Program Files\WinRAR\rar.exe" x -y -o- "%%D" extracted
pause
