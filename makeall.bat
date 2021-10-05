@echo off
del *.log /s

REM This file creates the all of the examples at once. Quick and dirty.
REM While very portable, it is not the most elegant way to do things.
REM If you ever find yourself doing things like this from a command 
REM line, then check into using a `make` (or `nmake`) failitiy.

REM First, make the libraries that a few things depend upon.
cd library
call vbcustom FreeTools2.vb library
call vbcustom MyResearch.vb library
call vbcustom MySerializedResearch.vb library

move *.dll ..

cd ..

rem for %%i in (*.vb) do call vbnet %%i exe 1

REM No need to build these...
REM call vbnet StartCalc.vb exe 1
REM call vbnet ReDimError.vb exe 1

REM Things that do note nees any of our special libraries
call vbnet Array1.vb exe 1
call vbnet CallExample.vb exe 1
call vbnet DoLoop.vb exe 1
call vbnet DoUntil.vb exe 1
call vbnet DoUntilPrefix.vb exe 1
call vbnet DoWhile.vb exe 1
call vbnet FileDir.vb exe 1
call vbnet fmcalc.vb exe 1
call vbnet fmcalc1.vb exe 1
call vbnet fmcalc2.vb exe 1
call vbnet fmcalc3.vb exe 1
call vbnet fmcalc_setup.vb exe 1
call vbnet ForEach.vb exe 1
call vbnet ForNext.vb exe 1
call vbnet ForNextStep.vb exe 1
call vbnet Goto.vb exe 1
call vbnet Hello1.vb exe 1
call vbnet Hello2.vb exe 1
call vbnet HelloQualified.vb exe 1
call vbnet IfElse.vb exe 1
call vbnet ifend.vb exe 1
call vbnet LengthBug.vb exe 1
call vbnet LengthBugFix.vb exe 1
call vbnet LineBreaks.vb exe 1
call vbnet MyCalendarOne.TestCase.vb exe 1
call vbnet MySchedule1.vb exe 1
call vbnet MySchedule2.vb exe 1
call vbnet Nothing.vb exe 1
call vbnet Property1.vb exe 1
call vbnet SelectCase.vb exe 1
call vbnet SelectCaseExceptional.vb exe 1
call vbnet Shadows1.vb exe 1
call vbnet StringSplit.vb exe 1
call vbnet TickTackTwister.vb exe 1
call vbnet While.vb exe 1
call vbnet VBNote0.vb exe 1
call vbnet VBNote1.vb exe 1
call vbnet VBNote2.vb exe 1
call vbnet VBNote3.vb exe 1
call vbnet VbCal0.vb exe 1
call vbnet VbCal1.vb exe 1
call vbnet VbCalCon1.vb exe 1

REM Things that need MyResearch and / or FreeTools2
call vbnet VbCalCon2.vb exe 2
call vbnet VBCalCon3.vb exe 2

dir *.log /s /os > report.txt
dir *.exe /s /os >> report.txt
dir *.dll /s /os >> report.txt

@echo on
notepad report.txt

