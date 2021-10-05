' Note that this command requires a name of a folder as a command 
' line parameter. Note also that folder names end with a backslash!
' e.g: C:\WINDOWS\
'
Imports System
Imports Microsoft.VisualBasic

Module Module1
    Sub Main()
        Dim aryStrings() As String = Environment.GetCommandLineArgs()
        If (aryStrings.GetLength(0) <> 2) Then  ' Tell them how to use us
            Console.WriteLine("Usage: {0} drive:\directory", aryStrings(0))
            Return
        End If
        Dim TheName As String = Dir(aryStrings(1))
        Do While TheName <> ""
            Console.WriteLine("   File " & aryStrings(1) & TheName)
            TheName = Dir()
        Loop
    End Sub
End Module  ' FileDir.vb
