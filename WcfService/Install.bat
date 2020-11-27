cd /d "%~dp0"
sc stop CoreCustomsService
sc delete CoreCustomsService
nssm install CoreCustomsService Application "%~dp0\WcfService.exe"
nssm set CoreCustomsService Application "%~dp0\WcfService.exe"
sc start CoreCustomsService 
pause