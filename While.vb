Imports System
Module Module1
    Sub Main()
        Dim cmdArgs As String() = Environment.GetCommandLineArgs()
        Dim i As Integer = 0
        While (i < cmdArgs.Length)
            If cmdArgs(i) = "/debug" Then
                Console.WriteLine( _
                  cmdArgs(i) & _
                  " detected")
            Else
                Console.WriteLine("   Got " & cmdArgs(i) & "...")
            End If
            i = i + 1 ' Manual loop control!
        End While
    End Sub
End Module ' While.vb