' Program: StartCalc: R&D Version 0
' This program shows how to start a program in the system directory
Imports System
Imports Microsoft.VisualBasic
Imports FreeTools2

Module Module1
    Public Sub Main()
        Dim sPath As String
        If (FreeTools2.Handy.GetSystem32Directory(sPath) = False) Then
            Console.Error.WriteLine(sPath)  ' Standard Error Device!
            Return
        End If
        sPath = sPath & "\calc.exe"
        Dim ProcID As Integer
        Try
            ProcID = Shell(sPath, AppWinStyle.NormalFocus)
        Catch
            Console.Error.WriteLine(sPath)
            Return
        End Try
        Console.Error.WriteLine("Started process: " & ProcID)
    End Sub
End Module

