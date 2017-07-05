@echo on
net use \\SRVRDSapps101\c$ /user:cg55\admsd

xcopy /d /y C:\Users\DUPUIS.S\Desktop\ATE55\ATE55\ATE55\bin\Debug\ATE55.exe \\SRVRDSapps101\c$\AppsMeuseInterne\ATE55\ATE55.exe

xcopy /d /y C:\Users\DUPUIS.S\Desktop\ATE55\ATE55\ATE55\bin\Debug\ATE55.exe.config \\SRVRDSapps101\c$\AppsMeuseInterne\ATE55\ATE55.exe.config

xcopy /d /y C:\Users\DUPUIS.S\Desktop\ATE55\ATE55\ATE55\bin\Debug\UserAD.dll \\SRVRDSapps101\c$\AppsMeuseInterne\ATE55\UserAD.dll

xcopy /d /y C:\Users\DUPUIS.S\Desktop\ATE55\ATE55\ATE55\bin\Debug\Microsoft.Office.Interop.Excel.dll \\SRVRDSapps101\c$\AppsMeuseInterne\ATE55\Microsoft.Office.Interop.Excel.dll

xcopy /d /y C:\Users\DUPUIS.S\Desktop\ATE55\ATE55\ATE55\bin\Debug\Microsoft.Office.Interop.Word.dll \\SRVRDSapps101\c$\AppsMeuseInterne\ATE55\Microsoft.Office.Interop.Word.dll

xcopy /d /y C:\Users\DUPUIS.S\Desktop\ATE55\ATE55\ATE55\bin\Debug\Microsoft.Office.Interop.Outlook.dll \\SRVRDSapps101\c$\AppsMeuseInterne\ATE55\Microsoft.Office.Interop.Outlook.dll

xcopy /d /y C:\Users\DUPUIS.S\Desktop\ATE55\ATE55\ATE55\bin\Debug\TemplatesWord\*.dotx \\SRVRDSapps101\c$\AppsMeuseInterne\ATE55\TemplatesWord\*.dotx

xcopy /d /y C:\Users\DUPUIS.S\Desktop\ATE55\ATE55\ATE55\bin\Debug\TemplatesWord\*.xltx \\SRVRDSapps101\c$\AppsMeuseInterne\ATE55\TemplatesWord\*.xltx

@echo on
pause


