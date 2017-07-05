@echo on
net use \\SRVRDSapps102\c$ /user:cg55\admsd

xcopy /d /y C:\Users\DUPUIS.S\Desktop\ATE55\ATE55\ATE55\bin\Debug\ATE55.exe \\SRVRDSapps102\c$\AppsMeuseInterne\ATE55\ATE55.exe

xcopy /d /y C:\Users\DUPUIS.S\Desktop\ATE55\ATE55\ATE55\bin\Debug\ATE55.exe.config \\SRVRDSapps102\c$\AppsMeuseInterne\ATE55\ATE55.exe.config

xcopy /d /y C:\Users\DUPUIS.S\Desktop\ATE55\ATE55\ATE55\bin\Debug\UserAD.dll \\SRVRDSapps102\c$\AppsMeuseInterne\ATE55\UserAD.dll

xcopy /d /y C:\Users\DUPUIS.S\Desktop\ATE55\ATE55\ATE55\bin\Debug\Microsoft.Office.Interop.Excel.dll \\SRVRDSapps102\c$\AppsMeuseInterne\ATE55\Microsoft.Office.Interop.Excel.dll

xcopy /d /y C:\Users\DUPUIS.S\Desktop\ATE55\ATE55\ATE55\bin\Debug\Microsoft.Office.Interop.Word.dll \\SRVRDSapps102\c$\AppsMeuseInterne\ATE55\Microsoft.Office.Interop.Word.dll

xcopy /d /y C:\Users\DUPUIS.S\Desktop\ATE55\ATE55\ATE55\bin\Debug\Microsoft.Office.Interop.Outlook.dll \\SRVRDSapps102\c$\AppsMeuseInterne\ATE55\Microsoft.Office.Interop.Outlook.dll

xcopy /d /y C:\Users\DUPUIS.S\Desktop\ATE55\ATE55\ATE55\bin\Debug\TemplatesWord\*.dotx \\SRVRDSapps102\c$\AppsMeuseInterne\ATE55\TemplatesWord\*.dotx

xcopy /d /y C:\Users\DUPUIS.S\Desktop\ATE55\ATE55\ATE55\bin\Debug\TemplatesWord\*.xltx \\SRVRDSapps102\c$\AppsMeuseInterne\ATE55\TemplatesWord\*.xltx

@echo on
pause


