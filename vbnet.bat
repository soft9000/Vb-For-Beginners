@echo off
if "%3" == "1" goto special1
if "%3" == "2" goto special2
if "%3" == "3" goto special3

:special1
REM Nothing special here...
@echo on
vbc %1 /nologo /target:%2 /r:system.dll > %1.log
@echo off
goto done

:special2
REM Uses our first set of research spaces...
@echo on
vbc %1 /nologo /target:%2 /r:system.dll /r:MyResearch.dll /r:freetools2.dll  /r:mscorlib.dll /r:Microsoft.VisualBasic.dll > %1.log
@echo off
goto done

:special3
REM To use the freetools2 space, only...
@echo on
vbc %1 /nologo /target:%2 /r:system.dll /r:freetools2.dll  /r:mscorlib.dll /r:Microsoft.VisualBasic.dll > %1.log
@echo off
goto done

:done