Imports System
Module Module1
    Sub Main()
        Dim cmdArgs As String() = Environment.GetCommandLineArgs()
        Dim i As Integer
	    Dim len As Integer = cmdArgs.Length
        len = len - 1
        For i = 0 To len
            If cmdArgs(i) = "/debug" Then
                Console.WriteLine( _
                  cmdArgs(i) & _
                  " detected")
            Else
                Console.WriteLine("   Got " & cmdArgs(i) & "...")
            End If
        Next
    End Sub
End Module ' LineBreaks.vb